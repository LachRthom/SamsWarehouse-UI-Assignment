namespace SamsWarehouse.Models.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);        
        void DeleteProduct(Product product);
    }
}
