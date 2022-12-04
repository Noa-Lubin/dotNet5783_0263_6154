using DO;
namespace Dal;
using DalApi;

internal class DalProduct : IProduct
{

    /// <summary>
    /// this function add new product to array
    /// </summary>
    /// <param name="product"></param>
    /// <returns>id of the product</returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product product)
    {
        DataSource.productList.Add(product);
        return product.ID;

    }


    /// <summary>
    /// this function delete product from the array
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {

        foreach (var item in DataSource.productList)
        {
            if (item.ID == id)
            {
                DataSource.productList.Remove(item);
                return;
            }
        }

        //if this product does not exist in array
        throw new Exception("this product does not exist");
    }

    //public void Delete(int id)
    //{
    //    throw new NotImplementedException();
    //}

    //public Product Get(int id)
    //{
    //    throw new NotImplementedException();
    //}

    //public IEnumerable<Product> GetAll()
    //{
    //    throw new NotImplementedException();
    //}

    /// <summary>
    /// this product return a product by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>a product</returns>
    /// <exception cref="Exception"></exception>
    public  Product Get(int id)
    {
        //List<Product> product = DataSource.productList;
        foreach (var item in DataSource.productList)
        {
            if (item.ID == id)
                return item;
        }

        //if this product does not exist in array
        throw new Exception("this product does not exist");
        
    }


    /// <summary>
    /// this function return an array of all the products
    /// </summary>
    /// <returns>array of all the products</returns>
    public IEnumerable<Product> GetAll()
    {
        //        List<Product> product = DataSource._productList;
        List< Product> newProducts = new List<Product>();
        foreach (var item in DataSource.productList)
        {
            newProducts.Add(item);
        }
        return newProducts;
    }


    /// <summary>
    /// update a product with new data
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product product)
    {
        foreach (var item in DataSource.productList)
        {
            
            if (product.ID == item.ID)
            {
                DataSource.productList.Remove(item);
                DataSource.productList.Add(product);
                return;
            }
        }
      //  if this product does not exist in array
        throw new Exception("this product does not exist");
        
    }

    //public void Updete(Product entity)
    //{
    //    throw new NotImplementedException();
    //}
}
