using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Services
{
    public interface IOrderService
    {
        void createOrder(Order order);
    }
}
