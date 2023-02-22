using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface IProduct
    {
        /// <summary>
        /// Request a list of products from the data layer and build a list of products based on the data
        /// </summary>
        /// <returns>the new list</returns>
        public IEnumerable<ProductForList?> GetAllProducts(Func<DO.Product?, bool>? func = null);

       /// <summary>
       /// Get product by id
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public Product GetProduct(int id);

        /// <summary>
        /// add new product to list
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product);

        /// <summary>
        /// delete a product from list
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id);

        /// <summary>
        /// update a specific product
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(Product product);

        /// <summary>
        /// Get product of ProductForList
        /// </summary>
        /// <returns></returns>
        public ProductItem GetProductForList(int id, BO.Cart cart);
        public IEnumerable<ProductItem> GetCatalog(Func<DO.Product?, bool>? func = null);
        public IEnumerable<BO.ProductForList?> PopularItems();

    }
}
