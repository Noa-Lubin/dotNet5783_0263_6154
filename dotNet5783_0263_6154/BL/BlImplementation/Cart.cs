using BlApi;
using BO;
using DalApi;
using DO;
using System.Data;
using static BO.Enums;

namespace BlImplementation
{

    internal class Cart : ICart
    {
        static IDal myDal = new Dal.DalList();
        /// <summary>
        /// add a product to the cart and update the stock
        /// </summary>
        /// <param name="idProduct"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="outOfStock"></exception>
        public BO.Cart AddProductToCart(int idProduct, BO.Cart cart)
        {
            DO.Product p = myDal.product.Get(idProduct); //Find the desired product by product code 
            foreach (var item in cart.Items) // checking if this product is already exist
            {
                if (item.IdProduct == idProduct) //check by id
                {
                    if (p.InStock >= 1)
                    {
                        item.AmountInCart += 1;
                        item.TotalPrice += item.Price;
                        cart.TotalPrice += p.Price;//update the total price of all the cart
                        return cart;
                    }
                    //הודעה מתאימה שהמוצר כבר קיים בסל ואין עוד ממנו במלאי
                    throw new outOfStock("The product is already in the cart and there is no more of it in stock");
                }
            }
            if (p.InStock < 1) // Checking whether the product is out of stock
                // הודעה מתאימה שהמוצר אזל
                throw new outOfStock("The product is out of stock");
            BO.OrderItem newOrderItem = new BO.OrderItem()//creat a new OrderItem
            {
                Name = p.Name,
                Price = p.Price,
                IdProduct = p.ID,
                AmountInCart = 1,
                TotalPrice = p.Price
            };
            cart.Items.Add(newOrderItem);//add to list of cart
            cart.TotalPrice += p.Price;//update the total price of all the cart
            return cart;// return the cart
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
        public void MakeOrder(BO.Cart cart, string name, string email, string address)
        {
            if (name == null)
                throw new IncorrectData("Name is incorrect");
            if (email == null || !email.Contains('@'))
                throw new IncorrectData("Email is incorrect");
            if (address == null)
                throw new IncorrectData("Address is incorrect");
            //Checking the integrity of the data
            foreach (var item in cart.Items)
            {
                if (item.AmountInCart < 1) //Incorrect amount
                    throw new IncorrectData("The amount in cart is incorrect");
                DO.Product p;
                try
                {
                    p = myDal.product.Get(item.IdProduct);//Find the desired product by product id 
                }
                catch
                {
                    throw new NotFound("The product is not found");
                }
                if (p.InStock < item.AmountInCart)//there is no in stock
                    throw new outOfStock("There is no in stock");
                if (item.Name == null) //there is no name
                    throw new IncorrectData("Name is incorrect");
                if (item.Price <= 0) //Incorrect price
                    throw new IncorrectData("price is incorrect");
            }
            DO.Order newOrder = new DO.Order()
            {
                CustomerName = name,
                CustomerAdress = address,
                CustomerEmail = email,
                OrderDate = DateTime.Now,
                ShipDate = DateTime.MinValue,
                DeliveryrDate = DateTime.MinValue,
            };
            int idOrder = myDal.order.Add(newOrder);//add order to list of orders
            foreach (var item in cart.Items)
            {
                //create a new order
                DO.OrderItem newOrderItem = new DO.OrderItem()
                {
                    ProductID = item.IdProduct,
                    OrderID = idOrder,
                    Price = item.Price,
                    Amount = item.AmountInCart,
                };
                myDal.orderItem.Add(newOrderItem);

                DO.Product p = myDal.product.Get(item.IdProduct); //Find the desired product by product code 
                p.InStock -= item.AmountInCart; //update in stock of this product
                myDal.product.Update(p);
            }

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
                p = myDal.product.Get(idProduct);//find product by id
            }
            catch
            {
                throw new NotFound("The product is not found");
            }
            if (amount == 0)// if the user want to delete this product from cart Completely
                foreach (var item in cart.Items) //Go through all the products in the cart
                {
                    if (item.IdProduct == idProduct)
                        cart.Items.Remove(item); //delete
                }
            if (p.InStock >= amount)//If there is the quantity in stock
            {
                foreach (var item in cart.Items)//Go through all the products in the cart
                {
                    if (item.IdProduct == idProduct)
                    {
                        cart.TotalPrice += amount - item.AmountInCart * p.Price; //update the new price of the cart
                        cart.Items.Remove(item);//delete this item
                        //create a new OrderItem
                        BO.OrderItem newOrderItem = new BO.OrderItem()
                        {
                            Name = p.Name,
                            Price = p.Price,
                            IdProduct = p.ID,
                            AmountInCart = amount,
                            TotalPrice = amount * p.Price
                        };
                        cart.Items.Add(newOrderItem); //add the new object
                        return cart;
                    }
                }
                return cart;
            }
            else
                // If the quantity is not in stock
                throw new outOfStock("There is no in stock");//הודעה מתאימה שהמוצר לא במלאי אין מספיק
        }
    }
}
