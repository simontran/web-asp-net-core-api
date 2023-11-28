using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using signalR.Hub;

namespace signalR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductOfferController : ControllerBase
    {
        private IHubContext<MessageHub, IMessageHubClient> messageHub;
        public ProductOfferController(IHubContext<MessageHub, IMessageHubClient> _messageHub)
        {
            messageHub = _messageHub;
        }
        //[HttpPost]
        //[Route("productoffers")]
        [HttpPost(Name = "ProductOffers")]
        public string Get()
        {
            List<string> offers = new List<string>();
            offers.Add("20% Off on IPhone 12");
            offers.Add("15% Off on HP Pavillion");
            offers.Add("25% Off on Samsung Smart TV");
            messageHub.Clients.All.SendOffersToUser(offers);
            return "Offers sent successfully to all users!";
        }
    }
}
