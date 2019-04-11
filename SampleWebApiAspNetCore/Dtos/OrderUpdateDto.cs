using System;

namespace SampleWebApiAspNetCore.Dtos
{
    public class OrderUpdateDto
    {

        public int CompanyNo { get; set; }
        public string RDDGeneratedSource { get; set; }
        public string OrderStage { get; set; }
        public string OrderDescription { get; set; }
        public string OrderStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime RddGenerateDateTime { get; set; }
        public DateTime RevisedDeliveryDateTime { get; set; }
        public DateTime MinimumRevisedDeliveryDateTime { get; set; }
        public DateTime RevisedShipmentDateTime { get; set; }
        public DateTime MinimumRevisedShipmentDateTime { get; set; }
    }
}
