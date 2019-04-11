using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SampleWebApiAspNetCore.Dtos;
using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Helpers;
using SampleWebApiAspNetCore.Models;
using SampleWebApiAspNetCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleWebApiAspNetCore.v1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUrlHelper _urlHelper;

        public OrdersController(IUrlHelper urlHelper, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = nameof(GetAllOrders))]
        public ActionResult GetAllOrders([FromQuery] QueryParameters queryParameters)
        {
            List<OrderItem> orderItems = _orderRepository.GetAll(queryParameters).ToList();

            int allItemCount = _orderRepository.Count();

            var paginationMetadata = new
            {
                totalCount = allItemCount,
                pageSize = queryParameters.PageCount,
                currentPage = queryParameters.Page,
                totalPages = queryParameters.GetTotalPages(allItemCount)
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            List<LinkDto> links = CreateLinksForCollection(queryParameters, allItemCount);

            IEnumerable<dynamic> toReturn = orderItems.Select(x => ExpandSingleOrderItem(x));

            return Ok(new
            {
                value = toReturn,
                links = links
            });
        }

        [HttpGet]
        [Route("{order_num:int}", Name = nameof(GetSingleOrder))]
        public ActionResult GetSingleOrder(int Order_Num)
        {
            OrderItem orderitem = _orderRepository.GetSingle(Order_Num);

            if (orderitem == null)
            {
                return NotFound();
            }

            return Ok(ExpandSingleOrderItem(orderitem));
        }

        [HttpPost(Name = nameof(AddOrder))]
        public ActionResult<OrderItemDto> AddOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            if (orderCreateDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrderItem toAdd = Mapper.Map<OrderItem>(orderCreateDto);

            _orderRepository.Add(toAdd);

            if (!_orderRepository.Save())
            {
                throw new Exception("Creating a orderitem failed on save.");
            }

            OrderItem orderItem = _orderRepository.GetSingle(toAdd.Order_Num);

            return CreatedAtRoute(nameof(GetSingleOrder), new { order_num = orderItem.Order_Num },
                Mapper.Map<OrderItemDto>(orderItem));
        }

        [HttpPatch("{order_num:int}", Name = nameof(PartiallyUpdateOrder))]
        public ActionResult<OrderItemDto> PartiallyUpdateOrder(int order_num, [FromBody] JsonPatchDocument<OrderUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            OrderItem existingEntity = _orderRepository.GetSingle(order_num);

            if (existingEntity == null)
            {
                return NotFound();
            }

            OrderUpdateDto orderUpdateDto = Mapper.Map<OrderUpdateDto>(existingEntity);
            patchDoc.ApplyTo(orderUpdateDto, ModelState);

            TryValidateModel(orderUpdateDto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(orderUpdateDto, existingEntity);
            OrderItem updated = _orderRepository.Update(order_num, existingEntity);

            if (!_orderRepository.Save())
            {
                throw new Exception("Updating a orderitem failed on save.");
            }

            return Ok(Mapper.Map<OrderItemDto>(updated));
        }

        [HttpDelete]
        [Route("{order_num:int}", Name = nameof(RemoveOrder))]
        public ActionResult RemoveOrder(int order_num)
        {
            OrderItem orderitem = _orderRepository.GetSingle(order_num);

            if (orderitem == null)
            {
                return NotFound();
            }

            _orderRepository.Delete(order_num);

            if (!_orderRepository.Save())
            {
                throw new Exception("Deleting a orderitem failed on save.");
            }

            return NoContent();
        }

        [HttpPut]
        [Route("{order_num:int}", Name = nameof(UpdateOrder))]
        public ActionResult<OrderItemDto> UpdateOrder(int order_num, [FromBody]OrderUpdateDto orderUpdateDto)
        {
            if (orderUpdateDto == null)
            {
                return BadRequest();
            }

            OrderItem existingOrderItem = _orderRepository.GetSingle(order_num);

            if (existingOrderItem == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(orderUpdateDto, existingOrderItem);

            _orderRepository.Update(order_num, existingOrderItem);

            if (!_orderRepository.Save())
            {
                throw new Exception("Updating a orderitem failed on save.");
            }

            return Ok(Mapper.Map<OrderItemDto>(existingOrderItem));
        }

        [HttpGet("GetRandomOrders", Name = nameof(GetRandomMeal))]
        public ActionResult GetRandomMeal()
        {
            ICollection<OrderItem> orderItems = _orderRepository.GetRandomOrders();

            IEnumerable<OrderItemDto> dtos = orderItems
                .Select(x => Mapper.Map<OrderItemDto>(x));

            List<LinkDto> links = new List<LinkDto>
            {

                // self 
                new LinkDto(_urlHelper.Link(nameof(GetRandomMeal), null), "self", "GET")
            };

            return Ok(new
            {
                value = dtos,
                links = links
            });
        }

        private List<LinkDto> CreateLinksForCollection(QueryParameters queryParameters, int totalCount)
        {
            List<LinkDto> links = new List<LinkDto>
            {

                // self 
                new LinkDto(_urlHelper.Link(nameof(GetAllOrders), new
                {
                    pagecount = queryParameters.PageCount,
                    page = queryParameters.Page,
                    orderby = queryParameters.OrderBy
                }), "self", "GET"),

                new LinkDto(_urlHelper.Link(nameof(GetAllOrders), new
                {
                    pagecount = queryParameters.PageCount,
                    page = 1,
                    orderby = queryParameters.OrderBy
                }), "first", "GET"),

                new LinkDto(_urlHelper.Link(nameof(GetAllOrders), new
                {
                    pagecount = queryParameters.PageCount,
                    page = queryParameters.GetTotalPages(totalCount),
                    orderby = queryParameters.OrderBy
                }), "last", "GET")
            };

            if (queryParameters.HasNext(totalCount))
            {
                links.Add(new LinkDto(_urlHelper.Link(nameof(GetAllOrders), new
                {
                    pagecount = queryParameters.PageCount,
                    page = queryParameters.Page + 1,
                    orderby = queryParameters.OrderBy
                }), "next", "GET"));
            }

            if (queryParameters.HasPrevious())
            {
                links.Add(new LinkDto(_urlHelper.Link(nameof(GetAllOrders), new
                {
                    pagecount = queryParameters.PageCount,
                    page = queryParameters.Page - 1,
                    orderby = queryParameters.OrderBy
                }), "previous", "GET"));
            }

            links.Add(
               new LinkDto(_urlHelper.Link(nameof(AddOrder), null),
               "create_order",
               "POST"));

            return links;
        }

        private dynamic ExpandSingleOrderItem(OrderItem orderItem)
        {
            IEnumerable<LinkDto> links = GetLinks(orderItem.Order_Num);
            OrderItemDto item = Mapper.Map<OrderItemDto>(orderItem);

            IDictionary<string, object> resourceToReturn = item.ToDynamic() as IDictionary<string, object>;
            resourceToReturn.Add("links", links);

            return resourceToReturn;
        }

        private IEnumerable<LinkDto> GetLinks(int order_num)
        {
            List<LinkDto> links = new List<LinkDto>
            {
                new LinkDto(_urlHelper.Link(nameof(GetSingleOrder), new { order_num = order_num }),
              "self",
              "GET"),

                new LinkDto(_urlHelper.Link(nameof(RemoveOrder), new { order_num = order_num }),
              "delete_order",
              "DELETE"),

                new LinkDto(_urlHelper.Link(nameof(AddOrder), null),
              "create_order",
              "POST"),

                new LinkDto(_urlHelper.Link(nameof(UpdateOrder), new { order_num = order_num }),
               "update_order",
               "PUT")
            };

            return links;
        }
    }
}
