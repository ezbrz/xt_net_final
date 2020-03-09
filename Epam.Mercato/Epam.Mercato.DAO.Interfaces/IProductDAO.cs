using Epam.Mercato.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Mercato.DAO.Interfaces
{
    public interface IProductDAO
    {
        bool AddNewProduct(Product newProduct);
        Dictionary<int, Product> GetAll();
        Dictionary<int, Product> GetAll(int category);
        bool DeleteById(int id);
        Product GetById(int id);
        bool EditProduct(int id, Product product);
        Dictionary<int, string> GetCategories();
        Dictionary<int, string> GetCountries();
    }
}
