using DO;
using System.Diagnostics;
using static Dal.DataSource;
using static DO.Enums;
namespace Dal;

internal static class DataSource
{
    private static readonly Random rnd = new();
    internal static class config
    {
        //counters for arrays
        static internal int NumOfOrders = 0;
        static internal int NumOfOrderItems = 0;
        static internal int NumOfProducts = 0;
        //run number to order
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;

        internal static int NextOrderNumber { get => s_nextOrderNumber++; }
        //run number to orderItem
        internal const int s_startOrderItem = 1000;
        private static int s_nextOrderItem = s_startOrderItem;

        internal static int NextOrderItem { get => s_nextOrderNumber++; }
    }

    internal static Order[] orderArr { get; } = new Order[100];
    internal static Product[] productsArr { get; } = new Product[50];
    internal static OrderItem[] orderItemArr { get; } = new OrderItem[200];

    static DataSource()
    {
        s_intialitize();
    }
    private static void s_intialitize()
    {
        //3 פונקציות
        createProducts();
        createOrders();
        createOrderItems();
    }
    //#region fill the products

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
    private static int[] inStock = new int[7] { 50, 90, 45, 30, 20, 17, 80 };
    //fill 10 product in array
    private static void createProducts()
    {
        for (int i = 0; i < 10; i++)
        {
            int category = rnd.Next(4);
            int name = rnd.Next(2);
            productsArr[i] = new Product
            {
                ID = i + 100000,
                Name = nameProduct[category, name],
                Price = rnd.Next(priceStart[category], priceTo[category]),

                InStock = rnd.Next(inStock[category]),
                Category = (Category)category
            };

            config.NumOfProducts++;
        }
    }
    //array to name f customer
    private static string[] customerName = { "Shira", "Noa", "Eli", "Daniel", "Avi" };
    //array to adress of customer
    private static string[] customerAddress = { "EvenGvirol", "Daniel", "Rashi", "ChoniHmeagel", "Narkis" };
    //fill 20 order to array

    private static void createOrders()
    {
        for (int i = 0; i < 20; i++)
        {
            int order = rnd.Next(5);
            orderArr[i] = new Order
            {
                ID = config.NextOrderNumber,
                CustomerName = customerName[order],
                CustomerAdress = customerAddress[rnd.Next(4)],
                CustomerEmail = customerName[order] + "@gmail.com",
            };
            //date and time
            orderArr[i].OrderDate = DateTime.MinValue + new TimeSpan(rnd.Next(2001, 2022) * 365, 0, 0, 0);
            if (i <= 0.8 * 20)
                orderArr[i].DeliveryrDate = DateTime.MinValue + new TimeSpan(3, 0, 0, 0);
            if (i <= 0.6 * 20)
                orderArr[i].ShipDate = DateTime.MinValue + new TimeSpan(2, 0, 0, 0);
            config.NumOfOrderItems++;
        }
    }

    //#region fill the orderItems
    //Help function - goes through the array of products and returns the price of a certain product
    private static double foundPrice(double id)
    {
        for (int i = 0; i < config.NumOfProducts; i++)
        {
            //if id is found
            if (id == productsArr[i].ID)
                //return the price of product
                return productsArr[i].Price;
        }
        return 0;
    }
    //fill  orderItem to array
    private static void createOrderItems()
    {
        int countItems = 0;//counter-that counts the number of items
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < rnd.Next(1, 5); j++)
            {
                orderItemArr[i] = new OrderItem
                {
                    ID = config.NextOrderNumber,
                    OrderID = i,
                    ProductID = rnd.Next(15),
                    Price = foundPrice(orderItemArr[i].ProductID),
                    Amount = i + rnd.Next(20, 100),
                };
                countItems++;
                config.NumOfOrderItems++;
            }
            //If 40 items have not been added to all orders, another (partial) round will be made
            if (i == 19 && countItems < 40)
                i = i - countItems;
        }
    }
}