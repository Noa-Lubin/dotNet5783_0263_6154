using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface ICart
    {
        /// <summary>
        /// This function add product to cart.
        /// </summary>
        /// <param name="idProduct">id of product</param>
        /// <param name="cart">cart</param>
        /// <returns>Updated cart</returns>
        public Cart AddProductToCart(int idProduct, Cart cart);


        /// <summary>
        /// This function updates the amount of a specific product in the cart.
        /// </summary>
        /// <param name="idProduct">id of product</param>
        /// <param name="cart">cart</param>
        /// <param name="amount">amount</param>
        /// <returns>Updated cart</returns>
        public Cart UpdateAmountOfProduct(int idProduct, Cart cart, int amount);


        /// <summary>
        /// תקבל סל קניות (שהפעם כולל את פרטי הקונה - שם, כתובת דוא"ל, כתובת) This function make an order.
        /// </summary>
        /// <param name="cart">cart</param>
        /// <param name="name">name</param>
        /// <param name="email">email</param>
        /// <param name="address">address</param>
        public int MakeOrder(Cart cart, string? name, string? email, string? address);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProduct"></param>
        /// <param name="cart"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public BO.Cart AddAndUpdate(int idProduct, Cart cart, int amount);
         

    }
}
