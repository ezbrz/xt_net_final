using Epam.Mercato.BLL.Interfaces;
using Epam.Mercato.DAO.Interfaces;
using Epam.Mercato.Entities;
using Epam.Mercato.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Mercato.BLL
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductDAO _productDAO;
        private Dictionary<int, Product> _dataDictionary = new Dictionary<int, Product>();
        public ProductLogic(IProductDAO productDAO)
        {
            _productDAO = productDAO;
        }

        public bool AddNewProduct(Product newProduct)
        {
            return _productDAO.AddNewProduct(newProduct);
        }

        public bool DeleteById(int id)
        {
            return _productDAO.DeleteById(id);
        }

        public bool EditProduct(int id, Product product)
        {
            return _productDAO.EditProduct(id, product);
        }

        public Dictionary<int, Product> GetAll()
        {
            return _productDAO.GetAll();
        }

        public Dictionary<int, Product> GetAll(int category)
        {
            return _productDAO.GetAll(category);
        }
        public Dictionary<int, Product> GetAll(Filter filter)
        {
            _dataDictionary = GetAll();
            if (filter.OrderProperty == "category" && filter.IsDescending)
            {
                _dataDictionary = GetAll().OrderByDescending(x => x.Value.Category).ToDictionary(x=>x.Key,x=>x.Value);
            }
            if (filter.OrderProperty == "price" && filter.IsDescending)
            {
                _dataDictionary = GetAll().OrderByDescending(x => x.Value.Price).ToDictionary(x => x.Key, x => x.Value);
            }
            if (filter.OrderProperty == "producer" && filter.IsDescending)
            {
                _dataDictionary = GetAll().OrderByDescending(x => x.Value.Producer).ToDictionary(x => x.Key, x => x.Value);
            }
            if (filter.OrderProperty == "category" && !filter.IsDescending)
            {
                _dataDictionary = GetAll().OrderBy(x => x.Value.Category).ToDictionary(x => x.Key, x => x.Value);
            }
            if (filter.OrderProperty == "price" && !filter.IsDescending)
            {
                _dataDictionary = GetAll().OrderBy(x => x.Value.Price).ToDictionary(x => x.Key, x => x.Value);
            }
            if (filter.OrderProperty == "producer" && !filter.IsDescending)
            {
                _dataDictionary = GetAll().OrderBy(x => x.Value.Producer).ToDictionary(x => x.Key, x => x.Value);
            }
            if (filter.SearchPattern != null && filter.SearchPattern!="")
            {
                _dataDictionary = _dataDictionary.Where(x => x.Value.Name.Contains(filter.SearchPattern)).ToDictionary(x=>x.Key,x=>x.Value);
            }
            if (filter.Category != 0)
            {
                _dataDictionary = _dataDictionary.Where(x=>x.Value.Category==filter.Category).ToDictionary(x => x.Key, x => x.Value);
            }
            return _dataDictionary;
        }

        public Product GetById(int id)
        {
            return _productDAO.GetById(id);
        }

        public Dictionary<int, string> GetCategories()
        {
            return _productDAO.GetCategories();
        }

        public Dictionary<int, string> GetCountries()
        {
            return _productDAO.GetCountries();
        }
    }
}
