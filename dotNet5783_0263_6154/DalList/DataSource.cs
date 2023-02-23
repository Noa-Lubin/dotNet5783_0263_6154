using DO;
using static Dal.DataSource;
using static DO.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DalApi;

namespace Dal;

internal static class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }
    private static readonly Random rnd = new Random();
    internal static class config
    {

        //run number to order
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;

        internal static int _nextOrderNumber { get => s_nextOrderNumber++; }
        //run number to orderItem
        internal const int s_startOrderItem = 1000;
        private static int s_nextOrderItem = s_startOrderItem;

        internal static int _nextOrderItem { get => s_nextOrderItem++; }
    }
    internal static List<Order?> orderList { get; } = new List<Order?>();
    internal static List<OrderItem?> orderItemList { get; } = new List<OrderItem?>();
    internal static List<Product?> productList { get; } = new List<Product?>();

    private static void s_Initialize()
    {
        //Console.WriteLine("In Init");
        InitCreatProductToList();
        InitCreatOrderToList();
        InitCreatOrderItemToList();
    }
    #region fill the products

    private static string[,] nameProduct = new string[7, 4] {/*miniDnuts*/ { "OreoCase","NutellaCase", "LotusCase","MiniCase" }
        /*belgianWaffles*/ ,{"OreoWaffel","NutellaWaffel", "LotusCWaffel","Colorful"},
        /*general*/ {"Brauniz","StarDnuts", "HeartDonuts","BigBuston"},
        /*bigDonuts*/ {"BigCase","BigNutella", "BigLotus","BigOreo"},
       /*specials*/ {"America","Purim", "LoveCase","Chanuka" },
       /*cupcakes*/{"Colorful","Choclate","Fistuc","Orange"} ,
       /*desserts*/{"Oreo25","Malabi", "Lotus25","Mix" }  };
    //array to price range
    private static int[] priceStart = new int[7] { 12, 10, 15, 20, 25, 30, 22 };
    private static int[] priceTo = new int[7] { 35, 40, 50, 60, 57, 45, 70 };
    //array to stock
    private static int[] inStock = new int[7] { 50, 0, 45, 30, 20, 17, 80 };
    //fill 10 product in array
    private static void InitCreatProductToList()
    {
        int c = 0;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Product newProduct = new Product
                {
                    ID = c++ + 100000,
                    Name = nameProduct[i, j],
                    Price = rnd.Next(priceStart[i], priceTo[i]),
                    InStock = rnd.Next(inStock[i]),
                    Category = (Enums.Category)i
                };
                productList.Add(newProduct);

            }

        }
        //for (int i = 0; i < 12; i++)
        //{
        //    int category = rnd.Next(7);
        //    int name = rnd.Next(4);
        //    Product newProduct = new Product
        //    {
        //        ID = i + 100000,
        //        Name = nameProduct[category, name],
        //        Price = rnd.Next(priceStart[category], priceTo[category]),
        //        InStock = rnd.Next(inStock[category]),
        //        Category = (Category)category
        //    };

        //    productList.Add(newProduct);
        //}
    }
    #endregion

    #region fill the orders
    //array to name f customer
    private static string[] customerName = { "Shira", "Noa", "Eli", "Daniel", "Avi" };
    //array to adress of customer
    private static string[] customerAdress = { "EvenGvirol", "Daniel", "Rashi", "ChoniHmeagel", "Narkis" };
    //fill 20 order to array
    private static void InitCreatOrderToList()
    {
        for (int i = 0; i < 20; i++)
        {
            int order = rnd.Next(5);
            Order newOrder = new Order()
            {
                ID = config._nextOrderNumber,
                CustomerName = customerName[order],
                CustomerAdress = customerAdress[rnd.Next(5)],
                CustomerEmail = customerName[order] + "@gmail.com",
            };

            //date and time
            newOrder.OrderDate = DateTime.MinValue + new TimeSpan(rnd.Next(2001, 2022) * 365, 0, 0, 0);
            if (i <= 0.8 * 20)
                newOrder.DeliveryrDate = newOrder.OrderDate + new TimeSpan(3, 0, 0, 0);
            if (i <= 0.6 * 20)
                newOrder.ShipDate = newOrder.OrderDate + new TimeSpan(2, 0, 0, 0);
            orderList.Add(newOrder);
        }
    }
    #endregion

    #region fill the orderItems
    //fill  orderItem to array
    private static void InitCreatOrderItemToList()
    {
        //bool flag = false;
        double price = 0;
        for (int i = 0; i < 40; i++)
        {
            int product = rnd.Next(10) + 100000;
            
           Product p =  productList.FirstOrDefault(p => p?.ID == product)??
                throw new Exception("the product is not exist");
            price = p.Price;


            //foreach (var item in productList)
            //{
            //    if (item?.ID == product)
            //    {
            //        flag = true;
            //        price = item?.Price;
            //    }
            //}

            //if (!flag)
            //{
            //    throw new Exception("the product is not exist");
            //}

            OrderItem newOrderItem = new OrderItem()
            {
                ID = config._nextOrderItem,
                OrderID = (i / 2)+1000,
                ProductID = product,
                Amount = rnd.Next(10) + 1,
                Price = price
            };
            orderItemList.Add(newOrderItem);
        }
    }
}
#endregion
