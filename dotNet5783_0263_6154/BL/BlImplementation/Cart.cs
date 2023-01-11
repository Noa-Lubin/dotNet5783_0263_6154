using BlApi;
using BO;
using DalApi;
using DO;
using System;
using System.Data;
using static BO.Enums;

namespace BlImplementation
{

    internal class Cart : ICart
    {
        DalApi.IDal? myDal = DalApi.Factory.Get();

        /// <summary>
        /// add a product to the cart and update the stock
        /// </summary>
        /// <param name="idProduct"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="outOfStock"></exception>
        public BO.Cart AddProductToCart(int idProduct, BO.Cart cart)
        {
            DO.Product p = myDal!.product.Get(idProduct); //Find the desired product by product code 
            if (p.InStock < 1) // Checking whether the product is out of stock
                // הודעה מתאימה שהמוצר אזל
                throw new outOfStock("The product is out of stock");
            if(cart.Items == null) cart.Items = new List<BO.OrderItem?>();
            DO.Product newProduct = new DO.Product()
            {
                ID = idProduct,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                InStock = p.InStock - 1,
            };
            myDal!.product.Delete(idProduct);
            myDal.product.Add(newProduct);
            bool existInCart = cart.Items?.Any(orderItem => orderItem?.IdProduct == idProduct) ?? false;
            if (!existInCart)//if this product is not exist in cart
            {
                BO.OrderItem newOrderItem = new BO.OrderItem()//creat a new OrderItem
                {
                    Name = p.Name,
                    Price = p.Price,
                    IdProduct = p.ID,
                    AmountInCart = 1,
                    TotalPrice = p.Price
                };
                cart.Items?.Add(newOrderItem);//add to list of cart
                cart.TotalPrice += p.Price;//update the total price of all the cart
                return cart;// return the cart
            }
            else//product is already in cart so only add one
            {
                BO.OrderItem? ord = cart.Items?.FirstOrDefault(orderItem => orderItem?.IdProduct == idProduct);//find this item in cart
                cart.Items?.Remove(ord);//delete it
                ord!.AmountInCart += 1;
                ord.TotalPrice += ord.Price;//for only one item
                cart.TotalPrice += p.Price;//update the total price of all the cart
                cart.Items?.Add(ord);//and add the updated
                return cart;
            }
        }

        private void DataIntegrityOfItem(BO.OrderItem orderItem, int idOrder)
        {

            if (orderItem.AmountInCart < 1) //Incorrect amount
                throw new IncorrectData("The amount in cart is incorrect");
            DO.Product p;
            try
            {
                p = myDal!.product.Get(orderItem.IdProduct);//Find the desired product by product id 
            }
            catch
            {
                throw new BO.NotFound("The product is not found");
            }

            if (p.InStock < orderItem.AmountInCart)//there is no in stock
                throw new outOfStock("There is no in stock");
            if (orderItem.Name == null) //there is no name
                throw new IncorrectData("Name is incorrect");
            if (orderItem.Price <= 0) //Incorrect price
                throw new IncorrectData("price is incorrect");

            //create a new OrderItem
            DO.OrderItem newOrderItem = new DO.OrderItem()
            {
                ProductID = orderItem?.IdProduct ?? 0,
                OrderID = idOrder,
                Price = orderItem?.Price ?? 0,
                Amount = orderItem?.AmountInCart ?? 0
            };
            myDal?.orderItem.Add(newOrderItem);
            p.InStock -= orderItem!.AmountInCart; //update in stock of this product
            myDal?.product.Update(p);
        }


        /// <summary>
        /// make an order - add a new order and all the orderItems in this order
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <exception cref="IncorrectData"></exception>
        /// <exception cref="NotFound"></exception>
        /// <exception cref="outOfStock"></exception>
        public void MakeOrder(BO.Cart cart, string? name, string? email, string? address)
        {
            if (name == null)
                throw new IncorrectData("Name is incorrect");
            if (email == null || !email.Contains('@'))
                throw new IncorrectData("Email is incorrect");
            if (address == null)
                throw new IncorrectData("Address is incorrect");
            DO.Order newOrder = new DO.Order()
            {
                CustomerName = name,
                CustomerAdress = address,
                CustomerEmail = email,
                OrderDate = DateTime.Now,
                ShipDate = DateTime.MinValue,
                DeliveryrDate = DateTime.MinValue,
            };
            int idOrder = myDal!.order.Add(newOrder);//add order to list of orders
            //Checking the integrity of the data
            cart.Items!.ForEach(item => DataIntegrityOfItem(item!, idOrder));
        
        }
        /// <summary>
        /// Updating the quantity of a product in the cart
        /// </summary>
        /// <param name="idProduct"></param>
        /// <param name="cart"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        /// <exception cref="NotFound"></exception>
        /// <exception cref="outOfStock"></exception>
        public BO.Cart UpdateAmountOfProduct(int idProduct, BO.Cart cart, int amount)
        {
            DO.Product p;
            try
            {
                p = myDal!.product.Get(idProduct); ;//find product by id
            }
            catch
            {
                throw new BO.NotFound("The product is not found");
            }
            BO.OrderItem? ord = cart.Items?.FirstOrDefault(orderItem => orderItem?.IdProduct == idProduct);//find this item in cart
            if (amount == 0)// if the user want to delete this product from cart Completely
            {
                cart.Items?.Remove(ord);//delete it              
                return cart;
            }
            if (p.InStock+ord!.AmountInCart < amount)//If there is no the quantity in stock
            {
                // If the quantity is not in stock
                throw new outOfStock("There is no in stock");//הודעה מתאימה שהמוצר לא במלאי אין מספיק
            }
            else//If the user wants to add or subtract from the quantity of the item
            {
                DO.Product newProduct = new DO.Product()
                {
                    ID = idProduct,
                    Name = p.Name,
                    Category = p.Category,
                    Price = p.Price,
                    InStock = p.InStock + ord.AmountInCart - amount,
                };
                myDal!.product.Delete(idProduct);
                myDal.product.Add(newProduct);

                cart.TotalPrice += (amount - ord?.AmountInCart ?? 0) * p.Price; //update the new price of the cart
                cart.Items?.Remove(ord);//delete this item
                //create a new OrderItem
                BO.OrderItem newOrderItem = new BO.OrderItem()
                {

                    Name = p.Name,
                    Price = p.Price,
                    IdProduct = p.ID,
                    AmountInCart = amount,
                    TotalPrice = amount * p.Price
                };
                cart.Items?.Add(newOrderItem); //add the new object
                return cart;
            }
        }
    }
}
