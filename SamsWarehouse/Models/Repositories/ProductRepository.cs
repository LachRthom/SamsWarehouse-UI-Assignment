
namespace SamsWarehouse.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WarehouseDBContext _context;

        public ProductRepository(WarehouseDBContext context)
        {
            _context = context;
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Where(async => async.ProductId == id).FirstOrDefault();
        }

        public void UpdateProduct(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
        }
    }
}
