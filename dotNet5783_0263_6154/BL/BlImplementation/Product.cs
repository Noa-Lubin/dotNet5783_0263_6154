using BlApi;
using BO;
using DalApi;
using DO;
using static BO.Enums;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        static IDal myDal = new Dal.DalList();
        /// <summary>
        /// Add a new product to the list of all products
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="Duplication"></exception>
        /// <exception cref="IncorrectData"></exception>
        public void AddProduct(BO.Product product)
        {
            foreach (var item in myDal.product.GetAll())
            {
                if (item.ID == product.ID)
                    throw new Duplication("This product is already exist");
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
                Name = product.Name,
                Category = (DO.Enums.Category)(product.Category)
            };
            myDal.product.Add(p);
        }
        /// <summary>
        /// Delete a product from the list of all products
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ExistInOrder"></exception>
        public void DeleteProduct(int id)
        {
            List<BO.Order> ordersList = new List<BO.Order>();
            List<BO.OrderItem> orderItemList = new List<BO.OrderItem>(); //create list of OrderItem for the list of items in the new order
            foreach (var item in myDal.order.GetAll())
            {
                //Goes through all the products of the received order
                foreach (var itemOrderItem in myDal.orderItem.AllProductsOfOrder(item.ID))
                {
                    //create a new Product object to keep the name of product in parameter
                    DO.Product p;
                    string nameProduct;
                    try
                    {
                        p = myDal.product.Get(itemOrderItem.ProductID);
                        nameProduct = p.Name;
                    }
                    catch
                    {
                        throw new NotFound("Product is not found");
                    }
                    BO.OrderItem newOrderItem = new BO.OrderItem()
                    {
                        IdOrderItem = item.ID,
                        IdProduct = itemOrderItem.ProductID,
                        Name = nameProduct,
                        Price = itemOrderItem.Price,
                        AmountInCart = itemOrderItem.Amount,
                        TotalPrice = itemOrderItem.Price * itemOrderItem.Amount//Calculation of the final price

                    };
                    orderItemList.Add(newOrderItem);//add this order item to the list of all
                    BO.Order newOrder = new BO.Order
                    {
                        ID = item.ID,
                        CustomerName = item.CustomerName,
                        CustomerAdress = item.CustomerAdress,
                        CustomerEmail = item.CustomerEmail,
                        OrderDate = item.OrderDate,
                        ShipDate = item.ShipDate,
                        DeliveryrDate = item.DeliveryrDate,
                        Items = orderItemList
                    };
                    ordersList.Add(newOrder);
                }
                foreach (var item1 in ordersList)//all orders
                {
                    foreach (var pItem in myDal.orderItem.AllProductsOfOrder(item1.ID))//all orderItems of every order
                    {
                        if (pItem.ProductID == id) //if the producte is exist in an orser
                            throw new ExistInOrder("Delete is faild. The product is exist in an order");
                    }
                }
            }
            myDal.product.Delete(id); //Delete
        }
        /// <summary>
        /// Build a list of products of the ProductForList type and return the built list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductForList> GetAllProducts()

        {
            List<BO.ProductForList> productsList = new List<BO.ProductForList>();
            foreach (var item in myDal.product.GetAll())
            {
                BO.ProductForList newProduct = new BO.ProductForList();
                newProduct.Name = item.Name;
                newProduct.Price = item.Price;
                newProduct.Category = (BO.Enums.Category)item.Category;
                newProduct.IdProduct = item.ID;
                productsList.Add(newProduct);
            }
            return productsList;
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
                p = myDal.product.Get(idProduct);
            }
            catch (Exception ex)
            {
                throw new NotFound("Product is not exist");
            }
            BO.Product product = new BO.Product() //create a new object to return
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                InStock = p.InStock,
                Category = (BO.Enums.Category)(p.Category),
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
                p = myDal.product.Get(id);
            }
            catch (Exception ex)
            {
                throw new NotFound("Product is not exist");
            }

            foreach (var item in cart.Items)//for know the amount
            {
                if (item.IdProduct == id)
                    amount = item.AmountInCart;
            }
            //checking if this is exist in stock or not
            bool exist = false;
            if (p.InStock > 0)
                exist = true;

            BO.ProductItem productItem = new BO.ProductItem()
            {
                IdProduct = id,
                Name = p.Name,
                Price = p.Price,
                Category = (BO.Enums.Category)(p.Category),
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
                Name = product.Name,
                Category = (DO.Enums.Category)(product.Category)
            };
            try
            {
                myDal.product.Update(p);
            }
            catch (Exception ex)
            {
                throw new NotFound("The update failed");/*??Exception????*/
            }
        }

    }

}




