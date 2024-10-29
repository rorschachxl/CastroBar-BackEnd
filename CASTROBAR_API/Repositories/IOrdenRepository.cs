namespace CASTROBAR_API.Repositories
{
    public interface IOrdenRepository
    {
        Task<int> AgregarOrdenAsync(int numeroMesa);
    }
}
