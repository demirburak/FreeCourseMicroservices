using FreeCourse.Services.FakePayment.Model;
using FreeCourse.Shared.Dtos;
using FreeCourse.SharedCore7.ControllerBases;
using FreeCourse.SharedCore7.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public FakePaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            //Ödeme ile ilgili işlem senaryoları geri dönebilir.

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand()
            {
                BuyerId = paymentDto.Order.BuyerId,
                Address = new Address()
                {
                    District = paymentDto.Order.Address.District,
                    Line = paymentDto.Order.Address.Line,
                    Province = paymentDto.Order.Address.Province,
                    Street = paymentDto.Order.Address.Street,
                    ZipCode = paymentDto.Order.Address.ZipCode
                },

            };

            foreach (var item in paymentDto.Order.OrderItems)
            {
                createOrderMessageCommand.OrderItems.Add(new OrderItem()
                {
                    PictureUrl = item.PictureUrl,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName
                });
            }

            await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);

            return CreateActionResultInstance(Shared.Dtos.Response<NoContent>.Success(200));
        }


    }
}
