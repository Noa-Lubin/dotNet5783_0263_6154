namespace BO
{
    /// <summary>
    /// Exception for entity not found or ID missing
    /// </summary>
    public class NotFound : Exception
    {
        public NotFound(string? message) : base(message) { }
    }
    /// <summary>
    ///     Exception of duplicate ID
    /// </summary>
    public class Duplication : Exception
    {
        public Duplication(string? message) : base(message) { }
    }
    /// <summary>
    /// Incorrect Data - if there is no name, email and more details or they are incorrect
    /// </summary>
    public class IncorrectData : Exception
    {
        public IncorrectData(string? message) : base(message) { }
    }
    /// <summary>
    /// outOfStock - there is no in stock
    /// </summary>
    public class outOfStock : Exception
    {
        public outOfStock(string? message) : base(message) { }
    }
    /// <summary>
    /// Incorrect dates
    /// </summary>
    public class IncorrectDateOrder : Exception
    {
        public IncorrectDateOrder (string? message) : base(message) { }
    }
    /// <summary>
    /// somthing is already exist
    /// </summary>
    public class ExistInOrder : Exception
    {
        public ExistInOrder(string? message) : base(message) { }
    }
}
