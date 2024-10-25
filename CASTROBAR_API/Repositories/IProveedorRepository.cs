using CASTROBAR_API.Dtos;

namespace CASTROBAR_API.Repositories
{
    public interface IProveedorRepository
    {
        Task<int> AgregarProveedorAsync(ProveedorRequestDto proveedorRequest);
        Task<List<PreveedorResponseDto>> ObtenerTodosLosProveedoresAsync();
        Task EditarProveedorAsync(int idProveedor, ProveedorRequestDto proveedorDto);
    }
}
