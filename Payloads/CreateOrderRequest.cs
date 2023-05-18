using Newtonsoft.Json;
using System.Net;
using static UniversityCateringService.Payloads.CreateOrderResponse;

namespace UniversityCateringService.Payloads
{
    public sealed class CreateOrderRequest
    {
        public string intent { get; set; }
        public List<PurchaseUnit> purchase_units { get; set; } = new();



        public sealed class PurchaseUnit
        {
           
            public List<Item> items { get; set; }
            public Amount amount { get; set; }
            public string reference_id { get; set; }
            public Shipping shipping { get; set; }
            public Payments payments { get; set; }

            public string invoice_id { get; set; }
        }

        public class Item
        {
            public string name { get; set; }

            public string description { get; set; }

            public string quantity { get; set; }

            public UnitAmount unit_amount { get; set; }
        }

        public class UnitAmount
        {         
            public string currency_code { get; set; }
            public string value { get; set; }
        }

        public class Amount
        {
            public string currency_code { get; set; }
            public string value { get; set; }
        }
        public sealed class Shipping
        {
            public Address address { get; set; }
        }

        public class Address
        {
            public string address_line_1 { get; set; }
            public string address_line_2 { get; set; }
            public string admin_area_2 { get; set; }
            public string admin_area_1 { get; set; }
            public string postal_code { get; set; }
            public string country_code { get; set; }
        }

        public sealed class Payments
        {
            public List<Capture> captures { get; set; }
        }
        public class Capture
        {
            public string id { get; set; }
            public string status { get; set; }
            public Amount amount { get; set; }
            public SellerProtection seller_protection { get; set; }
            public bool final_capture { get; set; }
            public string disbursement_mode { get; set; }
            public SellerReceivableBreakdown seller_receivable_breakdown { get; set; }
            public DateTime create_time { get; set; }
            public DateTime update_time { get; set; }
            public List<Link> links { get; set; }
        }

        public sealed class SellerProtection
        {
            public string status { get; set; }
            public List<string> dispute_categories { get; set; }
        }

        public sealed class SellerReceivableBreakdown
        {
            public Amount gross_amount { get; set; }
            public PaypalFee paypal_fee { get; set; }
            public Amount net_amount { get; set; }
        }

        public sealed class PaypalFee
        {
            public string currency_code { get; set; }
            public string value { get; set; }
        }
    }

}
