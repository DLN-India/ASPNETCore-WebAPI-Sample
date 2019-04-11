using System;

namespace SampleWebApiAspNetCore.Dtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Order_Num { get; set; }
        public DateTime Created { get; set; }
    }
}
