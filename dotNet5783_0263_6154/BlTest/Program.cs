using BlApi;
using BO;
using DalApi;
using DO;
using static BO.Enums;

namespace BlTest
{
    internal class Program
    {
        static IBl myBl = new BlImplementation.Bl();
        private static BO.Cart boCart = new BO.Cart()
        {
            CustomerName = "shira Cohen",
            CustomerAdress = "remez 22",
            CustomerEmail = "shira6557@gmail.com",
            Items = new List<BO.OrderItem>(),
            TotalPrice = 0,
        };

        #region switchToProducts
        static void switchProduct()
        {
            int idProduct;
            // { AddProduct, DeleteProduct, GetAllProducts, GetProduct, GetProductForList, UpdateProduct }
            Console.WriteLine("Product:");
            Console.WriteLine("0-Add");
            Console.WriteLine("1-Delete");
            Console.WriteLine("2-GetAll");
            Console.WriteLine("3-Get");
            Console.WriteLine("4-GetProductForList ");
            Console.WriteLine("5-Update ");
            Console.WriteLine("-1-Exit ");
            ChoiceProduct choice;
            int num;
            num = Convert.ToInt32(Console.ReadLine());
            choice = (ChoiceProduct)num;
            while (num >= 0)
            {
                switch (choice)
                {
                    case ChoiceProduct.AddProduct:
                        Console.WriteLine("Enter the product details for a new product");
                        BO.Product product = new BO.Product();
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
                            myBl.Product.AddProduct(product);
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;

                    case ChoiceProduct.DeleteProduct:
                        Console.WriteLine("enter the id of the product that you want to delete");
                        idProduct = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            myBl.Product.DeleteProduct(idProduct);
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;

                    case ChoiceProduct.GetAllProducts:
                        IEnumerable<ProductForList> products = new List<ProductForList>();
                        try
                        {
                            products = myBl.Product.GetAllProducts();
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        foreach (var item in products)
                        {
                            Console.WriteLine(item);
                        }
                        break;

                    case ChoiceProduct.GetProduct:
                        Console.WriteLine("enter id of product that you want to search");

                        idProduct = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(myBl.Product.GetProduct(idProduct));
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;                   
                    case ChoiceProduct.GetProductForList:
                        Console.WriteLine("enter id of product");
                        idProduct = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(myBl.Product.GetProductForList(idProduct, boCart));
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;

                    case ChoiceProduct.UpdateProduct:
                        Console.WriteLine("enter id of product");
                        idProduct = Convert.ToInt32(Console.ReadLine());
                        
                        Console.WriteLine("Enter the product details for the update product");
                        BO.Product product1 = new BO.Product();
                        product1.ID = idProduct;
                        Console.WriteLine("Enter the  name of the product");
                        product1.Name = Console.ReadLine();
                        Console.WriteLine("Enter the  price of the product");
                        product1.Price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the  category of the product");
                        Console.WriteLine("Choose from the following options:");
                        Console.WriteLine("miniDonuts, general, belgianWaffles, bigDonuts, desserts, cupcakes, specials");
                        Category c1;
                        string input1 = Console.ReadLine();
                        bool check1 = Category.TryParse(input1, out c1);
                        if (!check1)
                            throw new Exception("you press a wrong Ctegory");
                        product1.Category = c1;
                        Console.WriteLine("Enter the  stock of the product");
                        product1.InStock = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            myBl.Product.UpdateProduct(product1);
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    default:
                        Console.WriteLine("you press wrong number");
                        break;

                }
                Console.WriteLine("Product:");
                Console.WriteLine("0-Add");
                Console.WriteLine("1-Delete");
                Console.WriteLine("2-GetAll");
                Console.WriteLine("3-Get");
                Console.WriteLine("4-GetProductForList ");
                Console.WriteLine("5-Update ");
                Console.WriteLine("-1-Exit ");
                num = Convert.ToInt32(Console.ReadLine());
                choice = (ChoiceProduct)num;
            }
        }
        #endregion
        #region switchToOrders
        static void switchOrder()
        {
            int orderID;
            // { GetAllOrders, GetOrder, OrderDeliveryUpdate, OrderOfTracking, ShippingUpdate, UpdateOrder };
            Console.WriteLine("Order:");
            Console.WriteLine("0-GetAllOrders");
            Console.WriteLine("1-GetOrder");
            Console.WriteLine("2-OrderDeliveryUpdate");
            Console.WriteLine("3-OrderOfTracking ");
            Console.WriteLine("4-ShippingUpdate ");
            Console.WriteLine("5-UpdateOrder ");
            Console.WriteLine("-1-Exit ");
            ChoiceOrder choice;
            int num;
            num = Convert.ToInt32(Console.ReadLine());
            choice = (ChoiceOrder)num;
            while (num >= 0)
            {
                switch (choice)
                {
                    case ChoiceOrder.GetAllOrders:
                        IEnumerable< OrderForList> orders = new List< OrderForList>();

                        try
                        {
                            orders = myBl.Order.GetAllOrders();
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        foreach (OrderForList item in orders)
                        {
                            Console.WriteLine(item);//ToString
                        }
                        break;
                    case ChoiceOrder.GetOrder:
                        Console.WriteLine("Enter ID of the order");
                        orderID = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            Console.WriteLine( myBl.Order.GetOrder(orderID));
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }                        
                        break;
                    case ChoiceOrder.OrderDeliveryUpdate:
                        Console.WriteLine("Enter ID of the order");
                        orderID = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(myBl.Order.OrderDeliveryUpdate(orderID));
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    case ChoiceOrder.OrderOfTracking:
                        Console.WriteLine("Enter ID of the order");
                        orderID = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(myBl.Order.OrderOfTracking(orderID));
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    case ChoiceOrder.ShippingUpdate:
                        Console.WriteLine("Enter ID of the order");
                        orderID = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(myBl.Order.ShippingUpdate(orderID));
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    case ChoiceOrder.UpdateOrder:
                        BO.Order order = new BO.Order();
                        Console.WriteLine("Enter ID of the order");
                        order.ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter name:");
                        order.CustomerName = Console.ReadLine();
                        Console.WriteLine("Enter address:");
                        order.CustomerAdress = Console.ReadLine();
                        Console.WriteLine("Enter email:");
                        order.CustomerEmail = Console.ReadLine();
                        if(order.ID<1)
                            throw new IncorrectData("ID is Incorrect");
                        if(order.CustomerName == null)
                            throw new IncorrectData("Name is Incorrect");
                        if(order.CustomerAdress == null)
                            throw new IncorrectData("Address is Incorrect");
                        if(order.CustomerEmail == null || !order.CustomerEmail.Contains('@'))
                            throw new IncorrectData("Email is Incorrect");
                        try
                        {
                            myBl.Order.UpdateOrder(order);
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                    default:
                        break;

                }
                Console.WriteLine("Order:");
                Console.WriteLine("0-GetAllOrders");
                Console.WriteLine("1-GetOrder");
                Console.WriteLine("2-OrderDeliveryUpdate");
                Console.WriteLine("3-OrderOfTracking ");
                Console.WriteLine("4-ShippingUpdate ");
                Console.WriteLine("5-UpdateOrder ");
                Console.WriteLine("-1-Exit ");
                num = Convert.ToInt32(Console.ReadLine());
                choice = (ChoiceOrder)num;
            }
        }
        #endregion
        #region switchToCart
        static void switchCart()
        {
            //{ AddProductToCart, MakeOrder, UpdateAmountOfProduct};
            Console.WriteLine("Cart:");
            Console.WriteLine("0-AddProductToCart");
            Console.WriteLine("1-MakeOrder");
            Console.WriteLine("2-UpdateAmountOfProduct");
            Console.WriteLine("-1-Exit ");
            ChoiceCart choice;
            int num;
            num = Convert.ToInt32(Console.ReadLine());
            choice = (ChoiceCart)num;
            while (num >= 0)
            {
                switch (choice)
                {
                    case ChoiceCart.AddProductToCart:     
                        Console.WriteLine("Enter ID of product");
                        int idProduct = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            myBl.Cart.AddProductToCart(idProduct,boCart);
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;

                    case ChoiceCart.MakeOrder:
                        Console.WriteLine("enter your name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("enter your email:");
                        string email =Console.ReadLine();
                        Console.WriteLine("enter your address:");
                       string address = Console.ReadLine();
                        try
                        {
                            myBl.Cart.MakeOrder(boCart, name, email, address);
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;

                    case ChoiceCart.UpdateAmountOfProduct:

                        Console.WriteLine("Enter ID of product:");
                       idProduct = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter new amount:");
                        int amount = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            myBl.Cart.UpdateAmountOfProduct(idProduct, boCart, amount);
                        }
                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }
                        break;
                   
                    default:
                        break;
                }

                Console.WriteLine("Cart:");
                Console.WriteLine("0-AddProductToCart");
                Console.WriteLine("1-MakeOrder");
                Console.WriteLine("2-UpdateAmountOfProduct");
                Console.WriteLine("-1-Exit ");
                num = Convert.ToInt32(Console.ReadLine());
                choice = (ChoiceCart)num;
                
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
                    //Call the function that performs actions on the products 
                    case Choice.Product:
                        switchProduct();
                        break;
                    //Call the function that performs actions on the orders 
                    case Choice.Order:
                        switchOrder();
                        break;
                    //Call the function that performs actions on the cart 
                    case Choice.Cart:
                        switchCart();
                        break;
                    default:

                        break;
                }
                if (num == 0)//Exit the function without making another choice
                             //Otherwise you will receive your choice
                    return;
                else
                {
                    Console.WriteLine("Shop Menu:");
                    Console.WriteLine("0-Exit");
                    Console.WriteLine("1-Product");
                    Console.WriteLine("2-Order");
                    Console.WriteLine("3-Cart ");
                    num = Convert.ToInt32(Console.ReadLine());
                    choice = (Choice)num;
                }
            }

        }
    }
}