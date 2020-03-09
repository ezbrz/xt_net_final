using Epam.Mercato.Entities;
using Epam.Mercato.Handlers;
using System.Collections.Generic;

namespace Epam.Mercato.BLL.Interfaces
{
    public interface IProductLogic
    {
        bool AddNewProduct(Product newProduct);
        Dictionary<int, Product> GetAll();
        Dictionary<int, Product> GetAll(int category);
        Dictionary<int, Product> GetAll(Filter filter);
        bool DeleteById(int id);
        Product GetById(int id);
        bool EditProduct(int id, Product product);
        Dictionary<int, string> GetCategories();
        Dictionary<int, string> GetCountries();
    }
}
