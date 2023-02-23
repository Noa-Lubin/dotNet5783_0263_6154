using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
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
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }

    public class DalIdDoNotExistException : Exception//if not exist
    {
        public int EntityId;
        public string EntityName;
        public DalIdDoNotExistException(int id, string EName) : base() { EntityId = id; EntityName = EName; }
        public DalIdDoNotExistException(int id, string EName, string message) : base(message) { EntityId = id; EntityName = EName; }
        public DalIdDoNotExistException(int id, string EName, string message, Exception innerException) : base(message, innerException) { EntityId = id; EntityName = EName; }

        public override string ToString() => $"Id:{EntityId} of type {EntityName},does not exist";
    }
}
