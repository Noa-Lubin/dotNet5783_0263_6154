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
       if( DataSource.productList.Any(p => p?.ID == product.ID)  == true)
            throw new Duplication("This product is already exist");
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
        Product pro = DataSource.productList.FirstOrDefault(p => p?.ID == id) ??
        //  if this product does not exist in array
        throw new Exception("This product is not exist");
        DataSource.productList.Remove(pro);
    }

    /// <summary>
    /// this product return a product by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>a product</returns>
    /// <exception cref="Exception"></exception>
    public Product Get(int id)
    {
        //List<Product> product = DataSource.productList;
        return DataSource.productList.FirstOrDefault(p => p?.ID == id) ??
        //if this product does not exist in array
        throw new Exception("this product does not exist");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product GetByPredicat(Func<Product?, bool> func)
    {
        return DataSource.productList.FirstOrDefault(func) ??
    //if this product does not exist in array
    throw new Exception("this order does not exist");
    }


    /// <summary>
    /// this function return an array of all the products
    /// </summary>
    /// <returns>array of all the products</returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func = null)
    {
        return func is null ? DataSource.productList.Select(p => p) :
             DataSource.productList.Where(func);
    }
    
    /// <summary>
    /// update a product with new data
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product product)
    {
        Product pro = DataSource.productList.FirstOrDefault(p => p?.ID == product.ID) ??
        //  if this product does not exist in array
        throw new Exception("This product is not exist");
        DataSource.productList.Remove(pro);
        DataSource.productList.Add(product);
    }
}
