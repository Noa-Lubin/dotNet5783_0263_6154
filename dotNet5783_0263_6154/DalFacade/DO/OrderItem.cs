using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

/// <summary>
/// Structure for OrderItem
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// ProductID
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// ProductID
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// OrderID
    /// </summary>
    public int OrderID { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    public int Amount { get; set; }


    /// <summary>
    /// ToString function
    /// </summary>
    /// <returns>The item details string in the order</returns>
    public override string ToString() => $@"
        Product ID: {ProductID}
        OrderID: {OrderID}
    	Price: {Price}
    	Amount: {Amount}
";
}
