using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Epam.Mercato.Entities
{
    public static class Cart
    {
        public static Dictionary<Product, int> CartForUser = new Dictionary<Product, int>();
        public static string user;
        static Cart()
        {
        }
        public static void AddItem(Product prod, int count)
        {
            CartForUser.Add(prod, count);
        }
        public static void RemoveItem(Product prod)
        {
            CartForUser.Remove(prod);
        }
        public static void ClearCart()
        {
            CartForUser.Clear();
        }
        public static decimal Compute()
        {
            return CartForUser.Sum(x => x.Key.Price * x.Value);
        }
        public static void Purchase()
        {

        }
    }
}
