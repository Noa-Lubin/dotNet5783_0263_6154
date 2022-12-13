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
        public IEnumerable<ProductForList> GetAllProducts(Func<DO.Product?, bool> func = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public ProductItem GetProductById(int id, Cart c);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(Product product);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ProductItem GetProductForList(int id, BO.Cart cart);
    }
}
