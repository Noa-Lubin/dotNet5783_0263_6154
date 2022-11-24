using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi;
public interface IDal
{
    IProduct product { get};
    IOrder order { get};
    IOrderItem orderItem { get};


}
