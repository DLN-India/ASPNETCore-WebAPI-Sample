using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Repositories;
using System;
using System.Threading.Tasks;

namespace SampleWebApiAspNetCore.Services
{
    public class SeedDataService : ISeedDataService
    {
        public async Task Initialize(OrderDBContext context)
        {
            context.OrderItems.Add(new OrderItem()
            {
                Order_Num = 1000,
                Name = "TestOrder1",
                BU_ID = 202,
                OrderDescription = "EMEA_Test_Order1",
                RDDGeneratedSource = "DPE",
                CompanyNo = 202,
                OrderStage = "100",
                OrderStatus = "Pre-Production",
                Created = DateTime.Now,
                RddGenerateDateTime = DateTime.UtcNow,
                RevisedShipmentDateTime = DateTime.UtcNow.AddDays(1),
                MinimumRevisedDeliveryDateTime = DateTime.Now.AddDays(2),
                RevisedDeliveryDateTime = DateTime.Now.AddDays(2),
                MinimumRevisedShipmentDateTime = DateTime.Now.AddDays(1)
            });
            context.OrderItems.Add(new OrderItem()
            {
                Order_Num = 1001,
                Name = "TestOrder1",
                BU_ID = 202,
                OrderDescription = "EMEA_Test_Order1",
                RDDGeneratedSource = "DPE",
                CompanyNo = 202,
                OrderStage = "100",
                OrderStatus = "Pre-Production",
                Created = DateTime.Now,
                RddGenerateDateTime = DateTime.UtcNow,
                RevisedShipmentDateTime = DateTime.UtcNow.AddDays(1),
                MinimumRevisedDeliveryDateTime = DateTime.Now.AddDays(2),
                RevisedDeliveryDateTime = DateTime.Now.AddDays(2),
                MinimumRevisedShipmentDateTime = DateTime.Now.AddDays(1)
            });
            context.OrderItems.Add(new OrderItem()
            {
                Order_Num = 1020,
                Name = "TestOrder1",
                BU_ID = 202,
                OrderDescription = "EMEA_Test_Order1",
                RDDGeneratedSource = "DPE",
                CompanyNo = 202,
                OrderStage = "100",
                OrderStatus = "Pre-Production",
                Created = DateTime.Now,
                RddGenerateDateTime = DateTime.UtcNow,
                RevisedShipmentDateTime = DateTime.UtcNow.AddDays(1),
                MinimumRevisedDeliveryDateTime = DateTime.Now.AddDays(2),
                RevisedDeliveryDateTime = DateTime.Now.AddDays(2),
                MinimumRevisedShipmentDateTime = DateTime.Now.AddDays(1)
            });
            context.OrderItems.Add(new OrderItem()
            {
                Order_Num = 1024,
                Name = "TestOrder1",
                BU_ID = 202,
                OrderDescription = "EMEA_Test_Order1",
                RDDGeneratedSource = "DPE",
                CompanyNo = 202,
                OrderStage = "100",
                OrderStatus = "Pre-Production",
                Created = DateTime.Now,
                RddGenerateDateTime = DateTime.UtcNow,
                RevisedShipmentDateTime = DateTime.UtcNow.AddDays(1),
                MinimumRevisedDeliveryDateTime = DateTime.Now.AddDays(2),
                RevisedDeliveryDateTime = DateTime.Now.AddDays(2),
                MinimumRevisedShipmentDateTime = DateTime.Now.AddDays(1)
            });
            context.OrderItems.Add(new OrderItem()
            {
                Order_Num = 1109,
                Name = "TestOrder1",
                BU_ID = 202,
                OrderDescription = "EMEA_Test_Order1",
                RDDGeneratedSource = "DPE",
                CompanyNo = 202,
                OrderStage = "100",
                OrderStatus = "Pre-Production",
                Created = DateTime.Now,
                RddGenerateDateTime = DateTime.UtcNow,
                RevisedShipmentDateTime = DateTime.UtcNow.AddDays(1),
                MinimumRevisedDeliveryDateTime = DateTime.Now.AddDays(2),
                RevisedDeliveryDateTime = DateTime.Now.AddDays(2),
                MinimumRevisedShipmentDateTime = DateTime.Now.AddDays(1)
            });
            context.OrderItems.Add(new OrderItem()
            {
                Order_Num = 1209,
                Name = "TestOrder1",
                BU_ID = 202,
                OrderDescription = "EMEA_Test_Order1",
                RDDGeneratedSource = "DPE",
                CompanyNo = 202,
                OrderStage = "100",
                OrderStatus = "Pre-Production",
                Created = DateTime.Now,
                RddGenerateDateTime = DateTime.UtcNow,
                RevisedShipmentDateTime = DateTime.UtcNow.AddDays(1),
                MinimumRevisedDeliveryDateTime = DateTime.Now.AddDays(2),
                RevisedDeliveryDateTime = DateTime.Now.AddDays(2),
                MinimumRevisedShipmentDateTime = DateTime.Now.AddDays(1)
            });
            await context.SaveChangesAsync();
        }
    }
}
