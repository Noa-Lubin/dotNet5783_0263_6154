using DO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

public class DalProduct
{
    //this function add new product to array

    public static int Add(Product product)
    {
        if (DataSource.orderArr.Length - 1 != DataSource.config.NumOfProducts)
        {

            for (int i = 0; i < DataSource.config.NumOfProducts; i++)
            {
                if (product.ID == DataSource.productsArr[i].ID)
                    throw new Exception("This product already exists in the array");
            }
            DataSource.productsArr[DataSource.config.NumOfProducts++] = product;
        }
        else
        {
            throw new Exception("There is no place in array");
        }
        return product.ID;
    }
    public static void delete(int id)
    {
        for (int i = 0; i < DataSource.config.NumOfProducts; i++)
        {
            if (id == DataSource.productsArr[i].ID)
            {
                //delete product
                DataSource.productsArr[i] = DataSource.productsArr[DataSource.config.NumOfProducts];
                //update the stock of product
                DataSource.config.NumOfProducts--;
                return;
            }
        }
        //if this product does not exist in array
        throw new Exception("this product does not exist");
    }
    //Returns a product by ID number
    public static Product GetProduct(int id)
    {
        for (int i = 0; i < DataSource.config.NumOfProducts; i++)
        {
            if (id == DataSource.productsArr[i].ID)
            {

                return DataSource.productsArr[i];
            }
        }
        //if this id does not exist in array
        throw new Exception("this id does not exist");
    }
    //return a list/array of all the products that in stock
    public static Product[] GetProducts()

    {
        Product[] newProducts = new Product[DataSource.config.NumOfProducts];
        for (int i = 0; i < DataSource.config.NumOfProducts; i++)
        {
            newProducts[i] = DataSource.productsArr[i];
        }
        return newProducts;
    }
    //Updates the product with new data 
    public static void Update(Product product)
    {
        for (int i = 0; i < DataSource.config.NumOfProducts; i++)
        {
            if (product.ID == DataSource.productsArr[i].ID)
            {
                DataSource.productsArr[i] = product;
                //return product.ID;
            }
        }
        //if this product does not exist in array
        throw new Exception("this product does not exist");

    }


}