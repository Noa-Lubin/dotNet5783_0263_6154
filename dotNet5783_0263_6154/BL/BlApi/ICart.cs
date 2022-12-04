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
       /// 
       /// </summary>
       /// <param name="idProduct"></param>
       /// <param name="cart"></param>
       /// <returns></returns>
        public Cart AddProductToCart(int idProduct, Cart cart);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProduct"></param>
        /// <param name="cart"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public Cart UpdateAmountOfProduct(int idProduct, Cart cart, int amount);
        /// <summary>
        /// תקבל סל קניות (שהפעם כולל את פרטי הקונה - שם, כתובת דוא"ל, כתובת)
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        public void MakeOrder(Cart cart, string name, string email, string address);
    }
}
