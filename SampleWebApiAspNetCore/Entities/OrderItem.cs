using System;
using System.ComponentModel.DataAnnotations;

namespace SampleWebApiAspNetCore.Entities
{
    public class OrderItem
    {
        [Required]
        public int BU_ID { get; set; }
        public int CompanyNo { get; set; }
        public string RDDGeneratedSource { get; set; }
        public string Name { get; set; }
        public string OrderStage { get; set; }
        public string OrderDescription { get; set; }
        public string OrderStatus { get; set; }
        public string Segment { get; set; }
        [Key]
        public int Order_Num { get; set; }
        public DateTime Created { get; set; }
        public DateTime RddGenerateDateTime { get; set; }
        public DateTime RevisedDeliveryDateTime { get; set; }
        public DateTime MinimumRevisedDeliveryDateTime { get; set; }
        public DateTime RevisedShipmentDateTime { get; set; }
        public DateTime MinimumRevisedShipmentDateTime { get; set; }
    }
}
