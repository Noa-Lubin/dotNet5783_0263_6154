using DO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

public class DalProduct
{

    /// <summary>
    /// this function add new product to array
    /// </summary>
    /// <param name="product"></param>
    /// <returns>id of the product</returns>
    /// <exception cref="Exception"></exception>
    public static int Add(Product product)
    {

        if (DataSource.productsArr.Length - 1 != DataSource.config.NumOfProducts)
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


    /// <summary>
    /// this function delete product from the array
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public static void delete(int id)
    {
        for (int i = 0; i < DataSource.config.NumOfProducts; i++)
        {
            if (id == DataSource.productsArr[i].ID)
            {
                //delete product
                DataSource.productsArr[i] = DataSource.productsArr[DataSource.config.NumOfProducts-1];
                //update the stock of product
                DataSource.config.NumOfProducts--;
                return;
            }
        }
        //if this product does not exist in array
        throw new Exception("this product does not exist");
    }


    /// <summary>
    /// this product return a product by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>a product</returns>
    /// <exception cref="Exception"></exception>
    public static Product GetProduct(int id)
    {
        Product[] p = DataSource.productsArr;
        for (int i = 0; i < DataSource.config.NumOfProducts; i++)
        {
            if (id == p[i].ID)
            {

                return p[i];
            }
        }
        //if this id does not exist in array
        throw new Exception("this id does not exist");
    }


    /// <summary>
    /// this function return an array of all the products
    /// </summary>
    /// <returns>array of all the products</returns>
    public static Product[] GetProducts()
    {
        Product[] p = DataSource.productsArr;
        Product[] newProducts = new Product[DataSource.config.NumOfProducts];
        for (int i = 0; i < DataSource.config.NumOfProducts; i++)
        {
            newProducts[i] = p[i];
        }
        return newProducts;
    }


    /// <summary>
    /// update a product with new data
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="Exception"></exception>
    public static void Update(Product product)
    {
        for (int i = 0; i < DataSource.config.NumOfProducts; i++)
        {
            if (product.ID == DataSource.productsArr[i].ID)
            {
                DataSource.productsArr[i] = product;
                return;
                //return product.ID;
            }
        }
        //if this product does not exist in array
        throw new Exception("this product does not exist");

    }

}