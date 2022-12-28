using BlApi;
using BO;
using DO;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using static BO.Enums;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        DalApi.IDal? _myDal = DalApi.Factory.Get();

        /// <summary>
        /// Help func - casting object from DO.Product? to BO.ProductForList
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private BO.ProductForList casting(DO.Product? p)
        {
            BO.ProductForList pr = new BO.ProductForList()
            {
                Name = p?.Name,
                Price = p?.Price ?? 0,
                Category = (Category?)p?.Category,
                IdProduct = p?.ID ?? 0
            };
            return pr;
        }

        /// <summary>
        /// Add a new product to the list of all products
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="Duplication"></exception>
        /// <exception cref="IncorrectData"></exception>
        public void AddProduct(BO.Product product)
        {
            //Request from the data layer of all products
            IEnumerable<DO.Product?> products = _myDal!.product.GetAll();
            bool exist = false;
            exist = products.Any(p => p?.ID == product.ID);
            if (exist == true)
            {
                throw new BO.Duplication("This product is already exist");
            }
            if (product.Name == " ")
                throw new IncorrectData("Name is incorrect");
            if (product.ID <= 0)
                throw new IncorrectData("ID of product is incorrect");
            if (product.Price <= 0)
                throw new IncorrectData("price is incorrect");
            if (product.InStock < 0)
                throw new IncorrectData("Amount in stock is incorrect");
            DO.Product p = new DO.Product()
            {
                ID = product.ID,
                Price = product.Price,
                InStock = product.InStock,
                Name = product!.Name,
                Category = (DO.Enums.Category)product!.Category
            };
            try
            {
                _myDal?.product.Add(p);
            }
            catch
            {
                throw new BO.NotFound("Add failed");
            }


        }
        /// <summary>
        /// Delete a product from the list of all products
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ExistInOrder"></exception>
        /// 
        public void DeleteProduct(int idProduct)
        {
            //Request from the data layer of all orders
            IEnumerable<DO.Order?> orders = _myDal!.order.GetAll();

            bool isExsistProduct = orders.Any(currenOrder => _myDal.orderItem.GetAll(x=>x?.OrderID == currenOrder?.ID)
            .Any(item => item?.ProductID == idProduct));
            if (!isExsistProduct)
                throw new ExistInOrder("This product cannot be deleted");
            try { _myDal.product.Delete(idProduct); }
            catch { throw new BO.NotFound("A non-existent product cannot be deleted"); }

        }
       
        /// <summary>
        /// Build a list of products of the ProductForList type and return the built list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductForList> GetAllProducts(Func<DO.Product?, bool>? func = null)
        {
            IEnumerable<DO.Product?> allProducts = _myDal!.product.GetAll(func);
            //List<BO.ProductForList> products = new List<BO.ProductForList>();
            //the loop go at all products
            return allProducts.Select(p => casting(p));
        }

        /// <summary>
        /// Get Product by ID
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectData"></exception>
        /// <exception cref="NotFound"></exception>
        public BO.Product GetProduct(int idProduct)
        {
            if (idProduct < 1)
                throw new IncorrectData("ID of product is Incorrect");
            DO.Product p;
            try
            {
                p = _myDal!.product.Get(idProduct);
            }
            catch
            {
                throw new BO.NotFound("Product is not exist");
            }
            BO.Product product = new BO.Product() //create a new object to return
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                InStock = p.InStock,
                Category = (BO.Enums.Category?)(p.Category),
            };
            return product;
        }
        /// <summary>
        /// Build a ProductItem object based on the received data and calculate missing information and return a built productItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectData"></exception>
        /// <exception cref="NotFound"></exception>
        public ProductItem GetProductForList(int id, BO.Cart cart)
        {
            int amount = 0;
            if (id < 1)
                throw new IncorrectData("ID of product is Incorrect");
            DO.Product p;
            try
            {
                p = _myDal!.product.Get(id);
            }
            catch
            {
                throw new BO.NotFound("Product is not exist");
            }

            //for know the amount
            BO.OrderItem? oI = cart.Items?.FirstOrDefault(p => p?.IdProduct == id);
            amount = oI?.AmountInCart ?? throw new BO.NotFound("This product is not exist in your cart");
            //checking if this is exist in stock or not
            bool exist = false;
            if (p.InStock > 0)
                exist = true;

            BO.ProductItem productItem = new BO.ProductItem()
            {
                IdProduct = id,
                Name = p.Name,
                Price = p.Price,
                Category = (BO.Enums.Category?)(p.Category),
                InStock = exist, //we calculated
                Amount = amount //we calculated
            };
            return productItem;
        }
        /// <summary>
        /// update details of product 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="IncorrectData"></exception>
        /// <exception cref="NotFound"></exception>
        public void UpdateProduct(BO.Product product)
        {
            //Test Data - if the new data is correct
            if (product.Name == " ")
                throw new IncorrectData("Name is incorrect");
            if (product.Price <= 0)
                throw new IncorrectData("price is incorrect");
            if (product.InStock < 0)
                throw new IncorrectData("Amount in stock is incorrect");
            DO.Product p = new DO.Product() //create a new product to update
            {
                ID = product.ID,
                Price = product.Price,
                InStock = product.InStock,
                Name = product?.Name ?? throw new IncorrectData("This product is wrong, name is incorrect"),
                Category = (DO.Enums.Category)(product?.Category?? throw new IncorrectData("This product is wrong, Category is incorrect"))
            };
            try
            {
                _myDal?.product.Update(p);
            }
            catch 
            {
                throw new BO.NotFound("The update failed");/*??Exception????*/
            }
        }

    }

}