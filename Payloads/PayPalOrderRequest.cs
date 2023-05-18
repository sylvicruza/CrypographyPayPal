using static UniversityCateringService.Payloads.CreateOrderRequest;

namespace UniversityCateringService.Payloads
{
 
        public class UnitAmount
        {
            public string currency_code { get; set; }
            public string value { get; set; }
        }

        public class Item
        {
            public string name { get; set; }
            public string description { get; set; }
            public string quantity { get; set; }
            public UnitAmount unit_amount { get; set; }
          

    }

        public class Amount
        {
            public string currency_code { get; set; }
            public string value { get; set; }
            public Dictionary<string, UnitAmount> breakdown { get; set; }
        }

        public class PurchaseUnit
        {
        
            public string invoice_id { get; set; }
            public string reference_id { get; set; }
            public List<Item> items { get; set; }
            public Amount amount { get; set; }

        public Shipping shipping { get; set; }
        public Payments payments { get; set; }
    }

        public class ApplicationContext
        {
            public string return_url { get; set; }
            public string cancel_url { get; set; }
        }

        public class PayPalOrderRequest
        {
            public string intent { get; set; }
            public List<PurchaseUnit> purchase_units { get; set; }
            public ApplicationContext application_context { get; set; }
        }
    
   


}
