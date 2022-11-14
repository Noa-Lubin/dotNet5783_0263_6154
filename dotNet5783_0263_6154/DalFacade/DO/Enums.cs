namespace DO;

/// <summary>
/// Structure for Enums
/// </summary>
public struct Enums
{
    public enum Category { miniDonuts, general, belgianWaffles, bigDonuts, desserts, cupcakes, specials };
    public enum Choice { Exit, Product, Order, OrderItem };
    public enum ChoiceOrder { Add, Delete, Update, GetOrder, GetOrders };
    public enum ChoiceOrderItem { Add, Delete, Update, GetOrderItem, GetOrderItems };
    public enum ChoiceProduct { Add, Delete, Update, GetProduct, GetProducts };
}
