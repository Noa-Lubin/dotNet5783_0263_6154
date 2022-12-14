namespace DO;

/// <summary>
/// Structure for Product
/// </summary>
public struct Product
{

    /// <summary>
    /// ID
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Category
    /// </summary>
    public Enums.Category Category { get; set; }

    /// <summary>
    /// InStock
    /// </summary>
    public int InStock { get; set; }


    /// <summary>
    /// ToString function
    /// </summary>
    /// <returns>The product details string</returns>
    public override string ToString() => $@"
        Product ID: {ID} : {Name}
        category: {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
";
}
