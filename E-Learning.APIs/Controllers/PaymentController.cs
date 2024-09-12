using E_Learning.BL.DTO.Payment;
using E_Learning.BL.Managers.PaymentManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.APIs.Controllers
{
    public class PaymentController : APIBaseController
    {
        private readonly IPaymentManager _paymentManager;
        /*------------------------------------------------------------------------*/
        public PaymentController(IPaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }
        /*------------------------------------------------------------------------*/
        [HttpGet("OnlineCardIFrame")]
        public async Task<ActionResult<BasePaymentResponseDTO<string>>> OnlineCardPayment(int price, int userId)
        {
            var response = await _paymentManager.GetOnlineCardIFrame(price, userId);
            if(!string.IsNullOrEmpty(response.ErrorMessage))
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        /*------------------------------------------------------------------------*/
        [HttpGet("MobileWalletUrl")]
        public async Task<ActionResult<BasePaymentResponseDTO<string>>> MobileWalletPayment(int price, int userId, string walletMobileNumber)
        {
            var response = await _paymentManager.GetMoblieWalletUrl(price, userId, walletMobileNumber);
            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
