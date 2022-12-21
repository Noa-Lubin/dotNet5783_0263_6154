using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

/// <summary>
/// Structure for Order
/// </summary>
public struct Order
{

    /// <summary>
    /// ID
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// CustomerName
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// CustomerEmail
    /// </summary>
    public string? CustomerEmail { get; set; }

    /// <summary>
    /// CustomerAdress
    /// </summary>
    public string? CustomerAdress { get; set; }

    /// <summary>
    /// OrderDate
    /// </summary>
    public DateTime? OrderDate { get; set; }

    /// <summary>
    /// ShipDate
    /// </summary>
    public DateTime? ShipDate { get; set; }

    /// <summary>
    /// DeliveryrDate
    /// </summary>
    public DateTime? DeliveryrDate { get; set; }


    /// <summary>
    /// ToString function
    /// </summary>
    /// <returns>The order details string</returns>
    public override string ToString() => $@"
ID={ID} of {CustomerName}, 
CustomerEmail: {CustomerEmail}
    	CustomerAdress: {CustomerAdress}
    	OrderDate: {OrderDate}
    	ShipDate: {ShipDate}
    	DeliveryrDate: {DeliveryrDate}
";
}
