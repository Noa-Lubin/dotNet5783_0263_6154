using DalApi;
using DO;
using System.Net.Sockets;
using System.Xml;

namespace Dal
{
    internal class Product : IProduct
    {
        readonly string s_products = "Product";

        /// <summary>
        ///  The function add a new product
        /// </summary>
        /// <param name="entity">product</param>
        /// <returns>id of the new product</returns>
        /// <exception cref="Duplication"></exception>
        public int Add(DO.Product entity)
        {
            List<DO.Product?> lstProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
            bool x = lstProd.Any(prod => prod?.ID == entity.ID);
            if (x)
                throw new Duplication("Dal Id Already Exist");
            lstProd.Add(entity);
            XMLTools.SaveListToXMLSerializer<DO.Product>(lstProd, s_products);
            return entity.ID;
        }


        /// <summary>
        /// The function delete product
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="DalIdDoNotExistException"></exception>
        public void Delete(int id)
        {
            List<DO.Product?> lstProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
            DO.Product? addProduct = lstProd.FirstOrDefault(prod => prod?.ID == id) ?? throw new DalIdDoNotExistException(id, "product");
            int productIndex = lstProd.FindIndex(x => x?.ID == id);
            lstProd.RemoveAt(productIndex);
            XMLTools.SaveListToXMLSerializer<DO.Product>(lstProd, s_products);
        }


        /// <summary>
        /// The function return a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>product</returns>
        /// <exception cref="DalIdDoNotExistException"></exception>
        public DO.Product Get(int id)
        {
            List<DO.Product?> lstProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
            return lstProd.FirstOrDefault(prod => prod?.ID == id) ?? throw new DalIdDoNotExistException(id, "product");
        }


        /// <summary>
        ///  The function return all the products
        /// </summary>
        /// <param name="func"></param>
        /// <returns>All the products</returns>
        public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? func = null)
        {
            List<DO.Product?> lstProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
            if (func != null)
                return lstProd.Where(item => func(item));
            return lstProd.Select(item => item);
        }


        /// <summary>
        ///  The function return a product by predicat
        /// </summary>
        /// <param name="func"></param>
        /// <returns>product by predicat</returns>
        /// <exception cref="DO.NotFound"></exception>
        public DO.Product GetByPredicat(Func<DO.Product?, bool> func)
        {
            
            List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
            return listProducts.FirstOrDefault(myProduct => func(myProduct)) ??
                throw new DO.NotFound("there are no order with this id");
        }


        /// <summary>
        /// The function update a product
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotFound"></exception>
        public void Update(DO.Product entity)
        {
            List<DO.Product?> lstProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
            DO.Product? addProduct = lstProd.FirstOrDefault(prod => prod?.ID == entity.ID) ?? throw new NotFound("ProductID is not exist");
            int ProductIndex = lstProd.FindIndex(x => x?.ID == entity.ID);
            lstProd[ProductIndex] = entity;
            XMLTools.SaveListToXMLSerializer<DO.Product>(lstProd, s_products);
        }
    }
}
