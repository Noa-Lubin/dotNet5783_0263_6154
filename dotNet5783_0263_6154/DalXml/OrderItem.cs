using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal
{
    internal class OrderItem : IOrderItem
    {
        readonly string s_orderItems = "OrderItem";

        static DO.OrderItem createOrderItemFromXElement(XElement item)
        {
            return new DO.OrderItem()
            {
                ID = item.ToIntNullable("ID") ?? throw new FormatException("ID"),
                ProductID = item.ToIntNullable("ProductID") ?? throw new FormatException("ProductID"),
                OrderID = item.ToIntNullable("OrderID") ?? throw new FormatException("OrderID"),
                Price = item.ToDoubleNullable("Price") ?? throw new FormatException("Price"),
                Amount = item.ToIntNullable("Amount") ?? throw new FormatException("Amount"),
            };
        }

        /// <summary>
        /// The function add a new order item
        /// </summary>
        /// <param name="entity">order item</param>
        /// <returns>id of the new order item</returns>
        public int Add(DO.OrderItem entity)
        {
            XElement elementItem = XMLTools.LoadListFromXMLElement(s_orderItems);
            entity.ID = Config.NextOrderItemNumber();
            XElement xOrderItem = new XElement("OrderItem",
                                            new XElement("ID", entity.ID),
                                            new XElement("ProductID", entity.ProductID),
                                            new XElement("OrderID", entity.OrderID),
                                            new XElement("Price", entity.Price),
                                            new XElement("Amount", entity.Amount)
                                            );
            elementItem.Add(xOrderItem);
            XMLTools.SaveListToXMLElement(elementItem, s_orderItems);
            Config.SaveNextOrderItemNumber(entity.ID + 1);
            return entity.ID;
        }


        /// <summary>
        /// The function delete order item
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="DO.DalIdDoNotExistException"></exception>
        public void Delete(int id)
        {
            XElement elementItem = XMLTools.LoadListFromXMLElement(s_orderItems);
            XElement? oItem = (from o in elementItem.Elements()
                               where (o.ToIntNullable("ID") == id)
                               select o).FirstOrDefault();
            if (oItem == null)//doesnt exist
                throw new DO.DalIdDoNotExistException(id, "order item");
            oItem.Remove();
            XMLTools.SaveListToXMLElement(elementItem, s_orderItems);
        }


        /// <summary>
        /// The function return an order item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>order item</returns>
        /// <exception cref="DO.DalIdDoNotExistException"></exception>
        public DO.OrderItem Get(int id)
        {
            XElement? orderItemXElent = XMLTools.LoadListFromXMLElement(s_orderItems);
            XElement? oItem = (from o in orderItemXElent.Elements()
                               where (o.ToIntNullable("ID") == id)
                               select o).FirstOrDefault();
            if (oItem == null)//doesnt exist
                throw new DO.DalIdDoNotExistException(id, "order item");
            //else-exist
            return createOrderItemFromXElement(oItem);

        }


        /// <summary>
        /// The function return all the order items
        /// </summary>
        /// <param name="func"></param>
        /// <returns>All the order items</returns>
        public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? func = null)
        {
            XElement? orderItemXElent = XMLTools.LoadListFromXMLElement(s_orderItems);
            if (func != null)
            {
                return from o in orderItemXElent.Elements()
                       let item = createOrderItemFromXElement(o)
                       where func(item)
                       select (DO.OrderItem?)(item);
            }
            return from o in orderItemXElent.Elements()
                   select (DO.OrderItem?)(createOrderItemFromXElement(o));
        }


        /// <summary>
        /// The function return an order item by predicat
        /// </summary>
        /// <param name="func"></param>
        /// <returns>Order item by predicat</returns>
        /// <exception cref="DO.DalIdDoNotExistException"></exception>
        public DO.OrderItem GetByPredicat(Func<DO.OrderItem?, bool> func)
        {
            List<DO.OrderItem?> OrderItems = new List<DO.OrderItem?>();
            XElement OrderItemRootElem = XMLTools.LoadListFromXMLElement(s_orderItems);
            
            if (func != null)
                OrderItems = (from s in OrderItemRootElem.Elements()
                              select (DO.OrderItem?)createOrderItemFromXElement(s)).ToList();

            return OrderItems.FirstOrDefault(o => func(o)) ??
         throw new DO.NotFound("there is no order item with this id");
        }


        /// <summary>
        /// The function update an order item
        /// </summary>
        /// <param name="entity">order item</param>
        public void Update(DO.OrderItem entity)
        {
            Delete(entity.ID);
            Add(entity);
        }
    }
}
