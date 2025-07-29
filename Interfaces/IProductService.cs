namespace WebApplication2.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<Model.Product>> GetAllAsync();
        public Task<Model.Product?> GetByIdAsync(int id);
        public Task<Model.Product> CreateAsync(Model.Product product);
        public Task<bool> UpdateAsync(int id, Model.Product product);
        public Task<bool> DeleteAsync(int id);
    }
}
