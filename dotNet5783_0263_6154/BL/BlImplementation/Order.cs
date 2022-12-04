
using BO;
using DalApi;
using DO;
using static BO.Enums;


namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        static IDal myDal = new Dal.DalList();
        /// <summary>
        /// build a new list of OrderForList and return this
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderForList> GetAllOrders()
        {
            //List for the exist orders
            List<BO.Order> ordersList = new List<BO.Order>();
            //List for the orderForList to return
            List<BO.OrderForList> ordersForList = new List<BO.OrderForList>();
            //parameter for status of order
            BO.Enums.OrderStatus statusEnum = OrderStatus.approved;
            double sum = 0;

            //keep orders in the new list
            foreach (var item in myDal.order.GetAll())
            {
                //checkint what is the status of this order
                if (item.DeliveryrDate != default && item.DeliveryrDate <= DateTime.Now)
                    statusEnum = OrderStatus.provided;
                else if (item.ShipDate != default && item.ShipDate <= DateTime.Now)
                    statusEnum = OrderStatus.sent;
                List<BO.OrderItem> orderItemsList = new List<BO.OrderItem>();
                foreach (var item1 in myDal.orderItem.GetAll())
                {
                    if (item1.OrderID == item.ID)
                    {
                        //create a new Product object to keep the name of product in parameter
                        DO.Product p;
                        string nameProduct;
                        try
                        {
                            p = myDal.product.Get(item1.ProductID);
                            nameProduct = p.Name;
                        }
                        catch
                        {
                            throw new NotFound("Product is not found");
                        }
                        sum += (item1.Price * item1.Amount); //calculate the totalPrice of newOrder of BO
                        BO.OrderItem newOrderItem = new BO.OrderItem()
                        {
                            IdProduct = item1.ProductID,
                            Name = nameProduct,
                            Price = item1.Price,
                            AmountInCart = item1.Amount,
                            TotalPrice = item1.Price * item1.Amount//Calculation of the final price

                        };
                        orderItemsList.Add(newOrderItem);
                    }
                }

                BO.Order newOrder = new BO.Order()
                {
                    ID = item.ID,
                    CustomerName = item.CustomerName,
                    CustomerAdress = item.CustomerAdress,
                    CustomerEmail = item.CustomerEmail,
                    OrderDate = item.OrderDate,
                    ShipDate = item.ShipDate,
                    DeliveryrDate = item.DeliveryrDate,
                    Status = statusEnum,
                    TotalPrice = sum,
                    Items = orderItemsList
                };
                ordersList.Add(newOrder); //add to list
            }
            foreach (var item in ordersList) // pass all orderList
            {
                sum = 0; //reset
                int totalAmount = 0;
                foreach (var itemO in item.Items)
                {
                    sum += itemO.TotalPrice; //calculate sum
                    totalAmount += itemO.AmountInCart; //calculate amount
                }

                BO.OrderForList newOrderItem = new BO.OrderForList()
                {
                    Name = item.CustomerName,
                    IdOrder = item.ID,
                    status = statusEnum,
                    amount = totalAmount,
                    TotalPrice = sum
                };
                ordersForList.Add(newOrderItem); //add the new object to list

            }
            return ordersForList;//retuen the list of OrderForList
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
            DO.Order o = myDal.order.Get(idOrder);
            List<BO.OrderItem> orderItemList = new List<BO.OrderItem>(); //create list of OrderItem for the list of items in the new order
            //checkint what is the status of this order by the dates
            if (o.DeliveryrDate != default && o.DeliveryrDate <= DateTime.Today)
                statusEnum = OrderStatus.provided;
            else if (o.ShipDate != default && o.ShipDate <= DateTime.Today)
                statusEnum = OrderStatus.sent;
            //Goes through all the products of the received order
            foreach (var item in myDal.orderItem.AllProductsOfOrder(idOrder))
            {
                //create a new Product object to keep the name of product in parameter
                DO.Product p = myDal.product.Get(item.ProductID);
                string nameProduct = p.Name;
                BO.OrderItem newOrderItem = new BO.OrderItem()
                {
                    IdOrderItem = idOrder,
                    IdProduct = item.ProductID,
                    Name = nameProduct,
                    Price = item.Price,
                    AmountInCart = item.Amount,
                    TotalPrice = item.Price * item.Amount//Calculation of the final price

                };
                orderItemList.Add(newOrderItem);//add this order item to the list of all
            }
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
                Items = orderItemList
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
            DO.Order o = myDal.order.Get(idOrder);
            if (o.DeliveryrDate != default && o.DeliveryrDate < DateTime.Now) //If the order has already been delivered
            {
                throw new IncorrectDateOrder("The order has already been delivered");//ההזמנה כבר סופקה
            }
            if (o.ShipDate != default && o.ShipDate > DateTime.Now)//If the order has not yet been sent
            {
                throw new IncorrectDateOrder("The order has not been sent yet");// ההזמנה לא נשלחה עדיין
            }
            o.DeliveryrDate = DateTime.Now; //update the DeliveryrDate
            myDal.order.Update(o);//update the DeliveryrDate in date
            //I create a list of orderItems for the new order that has a figure that is a list of all the orderItems
            List<BO.OrderItem> orderItemList = new List<BO.OrderItem>();//A list for all orderItems
            foreach (var item in myDal.orderItem.AllProductsOfOrder(idOrder))
            {
                //create a new Product object to keep the name of product
                DO.Product p = myDal.product.Get(item.ProductID);
                string nameProduct = p.Name;
                BO.OrderItem newOrderItem = new BO.OrderItem()
                {
                    IdOrderItem = idOrder,
                    IdProduct = item.ProductID,
                    Name = nameProduct,
                    Price = item.Price,
                    AmountInCart = item.Amount,
                    TotalPrice = item.Price * item.Amount
                };
                orderItemList.Add(newOrderItem); //add the new orderItem to the list
            }
            BO.Order newOrder = new BO.Order() //create order to return
            {
                CustomerEmail = o.CustomerEmail,
                CustomerName = o.CustomerName,
                CustomerAdress = o.CustomerAdress,
                OrderDate = o.OrderDate,
                ShipDate = o.ShipDate,
                DeliveryrDate = o.DeliveryrDate,//now
                Status = BO.Enums.OrderStatus.provided,
                Items = orderItemList //the list I created before
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
                o = myDal.order.Get(idOrder);//checking if exist
            }
            catch
            {
                throw new NotFound("Order is not exist");
            }
            var tracking = new List<Tuple<DateTime, string>>();
            tracking.Add(new Tuple<DateTime, string>(o.OrderDate, "order approved"));
            if (o.ShipDate != default)
            {
                status = OrderStatus.sent;
                tracking.Add(new Tuple<DateTime, string>(o.ShipDate, "order shipped"));

                if (o.DeliveryrDate != default)
                {
                    status = OrderStatus.provided;
                    tracking.Add(new Tuple<DateTime, string>(o.DeliveryrDate, "order delivered"));
                }
            }
            BO.OrderTracking newOrderTracking = new BO.OrderTracking() //create a new OrderTracking
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
            DO.Order o = myDal.order.Get(idOrder);
            if (o.ShipDate != default && o.ShipDate < DateTime.Now)//If the order has already been sent
            {
                throw new IncorrectDateOrder("The order has already been sent");//ההזמנה כבר נשלחה
            }
            o.ShipDate = DateTime.Now;//update the ShipDate
            myDal.order.Update(o);//update the ShipDate in date
            //I create a list of orderItems for the new order that has a figure that is a list of all the orderItems
            List<BO.OrderItem> orderItemList = new List<BO.OrderItem>();//A list for all orderItems
            foreach (var item in myDal.orderItem.AllProductsOfOrder(idOrder))
            {
                //create a new Product object to keep the name of product
                DO.Product p = myDal.product.Get(item.ProductID);
                string nameProduct = p.Name;
                BO.OrderItem newOrderItem = new BO.OrderItem()
                {
                    IdOrderItem = idOrder,
                    IdProduct = item.ProductID,
                    Name = nameProduct,
                    Price = item.Price,
                    AmountInCart = item.Amount,
                    TotalPrice = item.Price * item.Amount

                };
                orderItemList.Add(newOrderItem);//add the new orderItem to the list
            }
            BO.Order newOrder = new BO.Order()
            {
                CustomerEmail = o.CustomerEmail,
                CustomerName = o.CustomerName,
                CustomerAdress = o.CustomerAdress,
                OrderDate = o.OrderDate,
                ShipDate = o.ShipDate, //now
                DeliveryrDate = o.DeliveryrDate,
                Status = BO.Enums.OrderStatus.sent,
                Items = orderItemList//the list I created before
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
            if (order.ShipDate != default && order.ShipDate <= DateTime.Now)
                //הודעה מתאימה שהמוצר כבר נשלח
                throw new IncorrectDateOrder("The order has already been sent");

            DO.Order o = myDal.order.Get(order.ID);

            DO.Order newOrder = new DO.Order() //create order to add
            {
                ID = order.ID,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                CustomerAdress = order.CustomerAdress,
                OrderDate = o.OrderDate,
                ShipDate = o.ShipDate,
                DeliveryrDate = o.DeliveryrDate,

            };
            myDal.order.Update(newOrder); //update by dal
        }
    }
}
