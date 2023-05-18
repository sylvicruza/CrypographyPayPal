namespace UniversityCateringService.Payloads
{
    public class OrderRequestModel
    {
        public string InvoiceId { get; set; }
        public string Item { get; set; }

        public string Price { get; set; }

        public string Currency { get; set; }
        // Include other properties as needed
    }
}
