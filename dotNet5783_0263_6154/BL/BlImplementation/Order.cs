
using BO;
using DO;
using System;
using System.Collections.Generic;
using static BO.Enums;


namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        DalApi.IDal? _myDal = DalApi.Factory.Get();

        /// <summary>
        /// Help func - Casting object from DO.OrderItem? to BO.OrderItem
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectData"></exception>
        private BO.OrderItem Casting(DO.OrderItem? o)
        {
            //create a new Product object to keep the name of product
            DO.Product p = _myDal!.product.Get(o?.ProductID ?? throw new IncorrectData("ID of product is incorrect")); 
            string? nameProduct = p.Name;
            BO.OrderItem newOrderItem = new BO.OrderItem()
            {
                //OrderID = o?.OrderID ?? throw new IncorrectData("ID of orderItem is incorrect"),
                IdProduct = o?.ProductID ?? throw new IncorrectData("ID is incorrect"),
                Name = nameProduct,
                Price = o?.Price ?? throw new IncorrectData("Price is incorrect"),
                AmountInCart = o?.Amount ?? throw new IncorrectData("AmountInCart is incorrect"),
                TotalPrice = o?.Price ?? 0 * o?.Amount ?? 0//Calculation of the final price

            };
            return newOrderItem;
        }

        /// <summary>
        /// Help func - Casting object fromDO.Order? to BO.OrderForList
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectData"></exception>
        private BO.OrderForList castingOrder(DO.Order? o)
        {
            //parameter for status of order
            BO.Enums.OrderStatus statusEnum = OrderStatus.approved;
            //checkint what is the status of this order
            if (o?.DeliveryrDate != default)
                statusEnum = OrderStatus.provided;
            else if (o?.ShipDate != default )
                statusEnum = OrderStatus.sent;

            IEnumerable<DO.OrderItem?> orderItems = _myDal!.orderItem.GetAll(orderItem => orderItem?.OrderID == o?.ID);//list of all orderItems
            BO.OrderForList newOrderForList = new BO.OrderForList()
            {
                IdOrder = o?.ID ?? throw new IncorrectData("ID of orderItem is incorrect"),
                Name = o?.CustomerName ?? throw new IncorrectData("ID is incorrect"),
                status = statusEnum,
                amount = orderItems.Count(),
                TotalPrice = orderItems.Sum(orderItem => orderItem?.Price * orderItem?.Amount) ?? 0
            };
            return newOrderForList;
        }
        /// <summary>
        /// build a new list of OrderForList and return this
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderForList> GetAllOrders(Func<DO.Product?, bool>? func = null)
        {
            IEnumerable<DO.Order?> allOrders = _myDal!.order.GetAll(); 
            return allOrders.Select(p => castingOrder(p));
        }

        /// <summary>
        ///  Returns order details to the manager by ID
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectData"></exception>
        public BO.Order GetOrder(int idOrder)
        {
            BO.Enums.OrderStatus statusEnum = OrderStatus.approved;//parameter for status of order Initialized to approved
            if (idOrder < 1) //checking if the id is possitive
                throw new IncorrectData("ID of order is not incorrect");
            DO.Order o = _myDal!.order.Get(idOrder);
            List<BO.OrderItem> orderItemList = new List<BO.OrderItem>(); //create list of OrderItem for the list of items in the new order
            //checkint what is the status of this order by the dates
            if (o.DeliveryrDate != default)
                statusEnum = OrderStatus.provided;
            else if (o.ShipDate != default)
                statusEnum = OrderStatus.sent;
            //Goes through all the products of the received order
            orderItemList = _myDal!.orderItem.GetAll(x => x?.OrderID == idOrder).Select(ord => Casting(ord)).ToList();
            BO.Order newOrder = new BO.Order()
            {
                ID = idOrder,
                CustomerEmail = o.CustomerEmail,
                CustomerName = o.CustomerName,
                CustomerAdress = o.CustomerAdress,
                OrderDate = o.OrderDate,
                ShipDate = o.ShipDate,
                DeliveryrDate = o.DeliveryrDate,
                //TotalPrice = ,
                Status = statusEnum,
                Items = orderItemList as List<BO.OrderItem?>//the list I created before
            };
            return newOrder; //return the order
        }

        /// <summary>
        /// Update that the order has been delivered - update date and status and return the order
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectDateOrder"></exception>
        public BO.Order OrderDeliveryUpdate(int idOrder)
        {
            DO.Order o = _myDal!.order.Get(idOrder);
            if (o.DeliveryrDate != default ) //If the order has already been delivered
            {
                throw new IncorrectDateOrder("The order has already been delivered");//ההזמנה כבר סופקה
            }
            if (o.ShipDate != default)//If the order has not yet been sent
            {
                throw new IncorrectDateOrder("The order has not been sent yet");// ההזמנה לא נשלחה עדיין
            }
            o.DeliveryrDate = DateTime.Now; //update the DeliveryrDate
            _myDal?.order.Update(o);//update the DeliveryrDate in date
            //I create a list of orderItems for the new order that has a figure that is a list of all the orderItems
            List<BO.OrderItem> orderItemList = new List<BO.OrderItem>();//A list for all orderItems
            orderItemList = _myDal!.orderItem.GetAll(x=>x?.OrderID == idOrder).Select(ord => Casting(ord)).ToList(); 

            BO.Order newOrder = new BO.Order() //create order to return
            {
                CustomerEmail = o.CustomerEmail,
                CustomerName = o.CustomerName,
                CustomerAdress = o.CustomerAdress,
                OrderDate = o.OrderDate,
                ShipDate = o.ShipDate,
                DeliveryrDate = o.DeliveryrDate,//now
                Status = BO.Enums.OrderStatus.provided,
                Items = orderItemList as List<BO.OrderItem?>//the list I created before
            };
            return newOrder;
        }

        /// <summary>
        /// Construct a ProductItem object Calculate missing information and return a constructed Product object
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        /// <exception cref="NotFound"></exception>
        public OrderTracking OrderOfTracking(int idOrder)
        {
            BO.Enums.OrderStatus status = OrderStatus.approved;//Initialization status
            DO.Order o;
            try
            {
                o = _myDal!.order.Get(idOrder);//checking if exist
            }
            catch
            {
                throw new BO.NotFound("Order is not exist");
            }
            var tracking = new List<Tuple<DateTime?, string>>();
            tracking.Add(new Tuple<DateTime?, string>(o.OrderDate, "order approved"));
            if (o.ShipDate != default)
            {
                status = OrderStatus.sent;
                tracking.Add(new Tuple<DateTime?, string>(o.ShipDate, "order shipped"));

                if (o.DeliveryrDate != default)
                {
                    status = OrderStatus.provided;
                    tracking.Add(new Tuple<DateTime?, string>(o.DeliveryrDate, "order delivered"));
                }
            }
            BO.OrderTracking newOrderTracking = new OrderTracking() //create a new OrderTracking
            {
                IdOrder = idOrder,
                Tracking = tracking,
                Status = status
            };
            return newOrderTracking;
        }

        /// <summary>
        /// Update that the order has been sent - update date and status and return the order
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectDateOrder"></exception>
        public BO.Order ShippingUpdate(int idOrder)
        {
            DO.Order o = _myDal!.order.Get(idOrder);
            if (o.ShipDate != default )//If the order has already been sent
            {
                throw new IncorrectDateOrder("The order has already been sent");//ההזמנה כבר נשלחה
            }
            o.ShipDate = DateTime.Now;//update the ShipDate
            _myDal?.order.Update(o);//update the ShipDate in date
            //I create a list of orderItems for the new order that has a figure that is a list of all the orderItems
            List<BO.OrderItem> orderItemList = new List<BO.OrderItem>();//A list for all orderItems
            orderItemList = _myDal!.orderItem.GetAll(x => x?.OrderID == idOrder).Select(ord => Casting(ord)).ToList();
            BO.Order newOrder = new BO.Order()
            {
                CustomerEmail = o.CustomerEmail,
                CustomerName = o.CustomerName,
                CustomerAdress = o.CustomerAdress,
                OrderDate = o.OrderDate,
                ShipDate = o.ShipDate, //now
                DeliveryrDate = o.DeliveryrDate,
                Status = BO.Enums.OrderStatus.sent,
                Items = orderItemList as List<BO.OrderItem?>//the list I created before
            };
            return newOrder;
        }

        /// <summary>
        /// Bonus function to update details of order
        /// </summary>
        /// <param name="order"></param>
        /// <exception cref="IncorrectDateOrder"></exception>
        public void UpdateOrder(BO.Order order)
        {
            if (order.ShipDate != default)
                //הודעה מתאימה שהמוצר כבר נשלח
                throw new IncorrectDateOrder("The order has already been sent");

            DO.Order o = _myDal!.order.Get(order.ID);

            DO.Order newOrder = new DO.Order() //create order to add
            {
                ID = order.ID,
                CustomerEmail = order?.CustomerEmail ?? throw new IncorrectData("This order is wrong, mail is incorrect"),
                CustomerName = order?.CustomerName ?? throw new IncorrectData("This order is wrong, name is incorrect"),
                CustomerAdress = order?.CustomerAdress ?? throw new IncorrectData("This order is wrong, address is incorrect"),
                OrderDate = o.OrderDate,
                ShipDate = o.ShipDate,
                DeliveryrDate = o.DeliveryrDate,

            };
            _myDal?.order.Update(newOrder); //update by dal
        }
    }
}
