using CASTROBAR_API.Dtos;

namespace CASTROBAR_API.Utilities
{
    public interface IEmailUtility
    {
        void SendEmail(EmailDto request); // Añadir proveedor
    }
}
