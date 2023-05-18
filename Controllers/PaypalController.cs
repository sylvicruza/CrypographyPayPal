using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using System.Text.Json;
using System.Text;
using UniversityCateringService.Clients;
using UniversityCateringService.Payloads;

namespace UniversityCateringService.Controllers
{
    public class PaypalController : Controller
    {
        private readonly PaypalClient paypalClient;

        public PaypalController(PaypalClient _paypalClient)
        {
            paypalClient = _paypalClient;
        }

        // GET: PaypalController
        public ActionResult Index()
        {
            // Return Paypal Checkout JS SDK
            ViewBag.ClientId = paypalClient.ClientId;
            return View();
        }

        // GET: PaypalController/Cart
        public ActionResult Cart()
        {
            // Return Paypal Checkout JS SDK
            ViewBag.ClientId = paypalClient.ClientId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Order([FromBody] OrderRequestModel requestModel, CancellationToken cancellationToken)
        {
            try
            {
                var price = requestModel.Price;
                var currency = requestModel.Currency;
                var reference = requestModel.InvoiceId;
                var item = requestModel.Item;

                var response = await paypalClient.CreateOrder(price, currency, reference);

                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }

        public async Task<IActionResult> Capture(string orderId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await paypalClient.CaptureOrder(orderId);

                //var reference = response.purchase_units[0].reference_id;

                // Put your logic to save the transaction here
                // You can use the "reference" variable as a transaction key
              //  var transactionId = response.purchase_units[0].payments.captures[0].id;

                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }

        public IActionResult Success(string transactionId)
        {
          
            ViewBag.OrderId = transactionId;
            return View();
        }
    
}
}
