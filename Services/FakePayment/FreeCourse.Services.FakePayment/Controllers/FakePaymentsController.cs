using FreeCourse.Services.FakePayment.Model;
using FreeCourse.Shared.Dtos;
using FreeCourse.SharedCore7.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            //Ödeme ile ilgili işlem senaryoları geri dönebilir.

            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }


    }
}
