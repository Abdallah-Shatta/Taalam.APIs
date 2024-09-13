using E_Learning.BL.DTO.Payment;

namespace E_Learning.BL.Managers.PaymentManager
{
    public interface IPaymentManager
    {
        Task<BasePaymentResponseDTO<string>> GetOnlineCardIFrame(int price, int userId);
        Task<BasePaymentResponseDTO<string>> GetMoblieWalletUrl(int price, int userId, string walletMobileNumber);
    }
}
