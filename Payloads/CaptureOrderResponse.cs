using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using static UniversityCateringService.Payloads.CreateOrderRequest;
using static UniversityCateringService.Payloads.CreateOrderResponse;

namespace UniversityCateringService.Payloads
{
    public sealed class CaptureOrderResponse
    {
        public string id { get; set; }
        public string status { get; set; }
        public PaymentSource payment_source { get; set; }
        public List<PurchaseUnit> purchase_units { get; set; }
        public Payer payer { get; set; }
        public List<Link> links { get; set; }

        public sealed class Payer
        {
            public Name name { get; set; }
            public string email_address { get; set; }
            public string payer_id { get; set; }
        }
        public sealed class Name
        {
            public string given_name { get; set; }
            public string surname { get; set; }
        }
        public sealed class PaymentSource
        {
            public Paypal paypal { get; set; }
        }

        public sealed class Paypal
        {
            public Name name { get; set; }
            public string email_address { get; set; }
            public string account_id { get; set; }
        }
    }
}
