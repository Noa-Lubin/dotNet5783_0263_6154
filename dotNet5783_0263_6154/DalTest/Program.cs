﻿using DO;
using Dal;
using static DO.Enums;
using DalApi;

internal class Project
{
    static IDal myDal = new Dal.DalList();
    #region switchToProducts
    static void switchProduct()
    {
        Console.WriteLine("Product:");
        Console.WriteLine("0-Add");
        Console.WriteLine("1-Delete");
        Console.WriteLine("2-Update");
        Console.WriteLine("3-GetProduct ");
        Console.WriteLine("4-GetProducts ");
        Console.WriteLine("-1-Exit ");
        ChoiceProduct choice;
        int num;
        num = Convert.ToInt32(Console.ReadLine());
        choice = (ChoiceProduct)num;
        while (num >= 0)
        {
            switch (choice)
            {
                case ChoiceProduct.Add:
                    Console.WriteLine("Enter the product details for a new product");
                    Product product = new Product();
                    Console.WriteLine("Enter the  name of the product");
                    product.Name = Console.ReadLine();
                    Console.WriteLine("Enter the  ID of the product");
                    product.ID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the  price of the product");
                    product.Price = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the  category of the product");
                    Console.WriteLine("Choose from the following options:");
                    Console.WriteLine("miniDonuts, general, belgianWaffles, bigDonuts, desserts, cupcakes, specials");
                    Category c;
                    string input = Console.ReadLine();
                    bool check = Category.TryParse(input, out c);
                    if (!check)
                        throw new Exception("you press a wrong Ctegory");
                    product.Category = c;
                    // int category = Convert.ToInt32(Console.ReadLine());
                    //product.Category = (Category)category;
                    Console.WriteLine("Enter the  stock of the product");
                    product.InStock = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        myDal.product.Add(product);
                        //Console.WriteLine(myDal.product.Add(product));
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case ChoiceProduct.Delete:
                    Console.WriteLine("enter the id of the product that you want to delete");
                    int id = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        myDal.product.Delete(id);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case ChoiceProduct.Update:
                    Console.WriteLine("Enter the product details that you want to update ");
                    Product productUpdate = new Product();
                    Console.WriteLine("Enter the  ID of the product");
                    productUpdate.ID = Convert.ToInt32(Console.ReadLine());
                    //print the product before update
                    Console.WriteLine(myDal.product.Get(productUpdate.ID));
                    //insert values to updateProduct
                    Console.WriteLine("Enter the  name of the product");
                    productUpdate.Name = Console.ReadLine();
                    Console.WriteLine("Enter the  price of the product");
                    productUpdate.Price = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the  category of the product");
                    Category cUpdate;
                    string inputUpdate = Console.ReadLine();
                    bool checkUpdate = Category.TryParse(inputUpdate, out cUpdate);
                    if (!checkUpdate)
                        throw new Exception("you press a wrong Ctegory");
                    productUpdate.Category = cUpdate;
                    //int categoryUpdate = Convert.ToInt32(Console.ReadLine());
                    //productUpdate.Category = (Category)categoryUpdate;
                    Console.WriteLine("Enter the  stock of the product");
                    productUpdate.InStock = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        myDal.product.Update(productUpdate);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case ChoiceProduct.GetProduct:
                    Console.WriteLine("enter id of product that you want to search");
                    
                    int idGetP = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(myDal.product.Get(idGetP));
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case ChoiceProduct.GetProducts:
                    IEnumerable<Product?> newProduct = myDal.product.GetAll();
                    foreach (Product item in newProduct)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine();
                    }
                    break;

                default:
                    Console.WriteLine("you press wrong number");
                    break;
            }
            Console.WriteLine("Product:");
            Console.WriteLine("0-Add");
            Console.WriteLine("1-Delete");
            Console.WriteLine("2-Update");
            Console.WriteLine("3-GetProduct ");
            Console.WriteLine("4-GetProducts ");
            Console.WriteLine("-1-Exit ");
            num = Convert.ToInt32(Console.ReadLine());
            choice = (ChoiceProduct)num;
        }
    }
    #endregion
    #region switchToOrders
    static void switchOrder()
    {
        Console.WriteLine("Order:");
        Console.WriteLine("0-Add");
        Console.WriteLine("1-Delete");
        Console.WriteLine("2-Update");
        Console.WriteLine("3-GetOrder ");
        Console.WriteLine("4-GetOrders ");
        Console.WriteLine("-1-Exit ");
        ChoiceOrder choice;
        int num;
        num = Convert.ToInt32(Console.ReadLine());
        choice = (ChoiceOrder)num;
        while (num >= 0)
        {
            switch (choice)
            {
                case ChoiceOrder.Add:
                    Console.WriteLine("Enter the order details for a new order");
                    Order order = new Order();
                    Console.WriteLine("Enter your  name ");
                    order.CustomerName = Console.ReadLine();
                    Console.WriteLine("Enter your adress ");
                    order.CustomerAdress = Console.ReadLine();
                    Console.WriteLine("Enter your email ");
                    order.CustomerEmail = Console.ReadLine();
                    order.OrderDate = DateTime.Now;
                    order.DeliveryrDate = order.OrderDate.AddHours(2.5);
                    order.ShipDate = order.OrderDate.AddHours(1);
                    try
                    {
                        myDal.order.Add(order);
                        //Console.WriteLine(myDal.order.Add(order));
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrder.Delete:
                    Console.WriteLine("enter the id of the order that you want to delete");
                    int id = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        myDal.order.Delete(id);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrder.Update:
                    Console.WriteLine("Enter the order details that you want to update ");
                    Order orderUpdate = new Order();
                    Console.WriteLine("Enter the ID of order ");
                    orderUpdate.ID = Convert.ToInt32(Console.ReadLine());
                    //print the order before update
                    Console.WriteLine(myDal.order.Get(orderUpdate.ID));
                    //insert values to updateOrder
                    Console.WriteLine("Enter your name ");
                    orderUpdate.CustomerName = Console.ReadLine();
                    Console.WriteLine("Enter your adress ");
                    orderUpdate.CustomerAdress = Console.ReadLine();
                    Console.WriteLine("Enter your email ");
                    orderUpdate.CustomerEmail = Console.ReadLine();
                    //date
                    //orderUpdate.OrderDate = DateTime.Now;
                    DateTime orderDate;
                    Console.WriteLine("enter the date:");
                    DateTime.TryParse(Console.ReadLine(), out orderDate);
                    orderUpdate.OrderDate = orderDate;
                    orderUpdate.DeliveryrDate = orderUpdate.OrderDate.AddHours(2.5);
                    orderUpdate.ShipDate = orderUpdate.OrderDate.AddHours(1);
                    try
                    {
                        myDal.order.Update(orderUpdate);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrder.GetOrder:
                    Console.WriteLine("enter id of order that you want to search");
                    int idGet = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(myDal.order.Get(idGet));
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrder.GetOrders:
                    IEnumerable<Order?> newOrders = myDal.order.GetAll();
                    foreach (Order item in newOrders)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                default:
                    break;
            }
            Console.WriteLine("Order:");
            Console.WriteLine("0-Add");
            Console.WriteLine("1-Delete");
            Console.WriteLine("2-Update");
            Console.WriteLine("3-GetOrder ");
            Console.WriteLine("4-GetOrders ");
            Console.WriteLine("-1-Exit ");
            num = Convert.ToInt32(Console.ReadLine());
            choice = (ChoiceOrder)num;
        }
    }
    #endregion
    #region switchToOrderItems
    static void switchOrderItem()
    {
        Console.WriteLine("OrderItem:");
        Console.WriteLine("0-Add");
        Console.WriteLine("1-Delete");
        Console.WriteLine("2-Update");
        Console.WriteLine("3-GetOrderItem");
        Console.WriteLine("4-GetOrderItems");
        Console.WriteLine("5-get order item by product and order");
        Console.WriteLine("6-view all products in order");

        Console.WriteLine("-1-Exit ");
        ChoiceOrderItem choice;
        int num;
        num = Convert.ToInt32(Console.ReadLine());
        choice = (ChoiceOrderItem)num;
        while (num >= 0)
        {
            switch (choice)
            {
                case ChoiceOrderItem.Add:
                    Console.WriteLine("Enter the orderItem details for a new orderItem");
                    OrderItem orderItem = new OrderItem();
                    Console.WriteLine("Enter the order ID ");
                    orderItem.OrderID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the price ");
                    orderItem.Price = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the product ID ");
                    orderItem.ProductID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the amount of the products ");
                    orderItem.Amount = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        myDal.orderItem.Add(orderItem);
                        //Console.WriteLine(myDal.orderItem.Add(orderItem));
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrderItem.Delete:
                    Console.WriteLine("enter the id of the orderItem that you want to delete");
                    int id = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        myDal.orderItem.Delete(id);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrderItem.Update:
                    Console.WriteLine("Enter the orderItem details that you want to update ");
                    OrderItem orderItemUpdate = new OrderItem();
                    Console.WriteLine("Enter the orderItem ID ");
                    orderItemUpdate.ID = Convert.ToInt32(Console.ReadLine());
                    //print the orderItem before update
                    Console.WriteLine(myDal.orderItem.Get(orderItemUpdate.ID));
                    //insert values to updateOrderItem
                    Console.WriteLine("Enter the order ID ");
                    orderItemUpdate.OrderID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the price ");
                    orderItemUpdate.Price = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the product ID ");
                    orderItemUpdate.ProductID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the amount of the products");
                    orderItemUpdate.Amount = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        myDal.orderItem.Update(orderItemUpdate);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrderItem.GetOrderItem:
                    Console.WriteLine("enter id of orderItem that you want to search");
                    int idGetItem = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(myDal.orderItem.Get(idGetItem));
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;
                case ChoiceOrderItem.GetOrderItems:
                    IEnumerable<OrderItem?> newOrderItems = myDal.orderItem.GetAll();
                    foreach (OrderItem item in newOrderItems)
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case ChoiceOrderItem.GetOrderItembyProAndOrd:
                    Console.WriteLine("enter id of your order");
                    int idOrder = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter id of product");
                    int idProduct = int.Parse(Console.ReadLine());
                    Console.WriteLine(myDal.orderItem.GetItemByIds(idProduct,idOrder));
                    break;

                case ChoiceOrderItem.viewAllProducts:
                    Console.WriteLine("enter id of your order");
                    int orderId = int.Parse(Console.ReadLine());
                    IEnumerable<OrderItem?> orderItems= myDal.orderItem.AllProductsOfOrder(orderId);
                    foreach (OrderItem item in orderItems)
                    {
                        Console.WriteLine(item);
                    }
                    break;

                default:
                    break;
            }
            Console.WriteLine("OrderItem:");
            Console.WriteLine("0-Add");
            Console.WriteLine("1-Delete");
            Console.WriteLine("2-Update");
            Console.WriteLine("3-GetOrderItem ");
            Console.WriteLine("4-GetOrderItems ");
            Console.WriteLine("5-get order item by product and order ");
            Console.WriteLine("6-view all products in order ");
            Console.WriteLine("-1-Exit ");
            num = Convert.ToInt32(Console.ReadLine());
            choice = (ChoiceOrderItem)num;
        }
    }
    #endregion

    static void Main(string[] arg)
    {
        
        Choice choice;
        int num;
        Console.WriteLine("Shop Menu:");
        Console.WriteLine("0-Exit");
        Console.WriteLine("1-Product");
        Console.WriteLine("2-Order");
        Console.WriteLine("3-Cart ");
        num = Convert.ToInt32(Console.ReadLine());
        choice = (Choice)num;
        //You can only choose a number between 0 and 3
        while (num <= 3 && num >= 0)
        {
            switch (choice)
            {
                case Choice.Exit:
                    break;
                //Call the function that performs actions on the products (delete, add, update, etc.)
                case Choice.Product:
                    switchProduct();
                    break;
                //Call the function that performs actions on the orders (delete, add, update, etc.)
                case Choice.Order:
                    switchOrder();
                    break;
                //Call the function that performs actions on the orderItems (delete, add, update, etc.)
                case Choice.OrderItem:
                    switchOrderItem();
                    break;
                default:

                    break;
            }
            if (num == 0)//Exit the function without making another choice
                //Otherwise you will receive your choice
                return;
            else
            {
                Console.WriteLine("Shop Menu: 0-Exit" +
                           "1-Product" +
                           "2-Order" +
                           "3-Cart ");
                num = Convert.ToInt32(Console.ReadLine());
                choice = (Choice)num;
            }
        }

    }
}