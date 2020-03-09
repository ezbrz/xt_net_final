using Epam.Mercato.DAO.Interfaces;
using Epam.Mercato.Entities;
using Epam.Mercato.Handlers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Mercato.DAL
{
    public class ProductDbDAO : IProductDAO
    {
        static readonly string path = DataMode.GetPath("DBconnection");
        private static Dictionary<int, Product> _products = new Dictionary<int, Product>();
        private static Dictionary<int, string> _categories = new Dictionary<int, string>();
        private static Dictionary<int, string> _countries = new Dictionary<int, string>();
        public bool AddNewProduct(Product newProduct)
        {
            var newId = GetAll().Keys.Count > 0
           ? GetAll().Keys.Max() + 1
           : 1;
            try
            {
                using (SqlConnection connection = new SqlConnection(path))
                {
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.AddProduct";
                    var nameParametr = new SqlParameter()
                    {
                        DbType = DbType.String,
                        ParameterName = "@name",
                        Value = newProduct.Name,
                        Direction = ParameterDirection.Input
                    };
                    var photoParametr = new SqlParameter()
                    {
                        DbType = DbType.Binary,
                        ParameterName = "@photo",
                        Value = newProduct.Photo,
                        IsNullable = true,
                        Direction = ParameterDirection.Input
                    };
                    var categoryParametr = new SqlParameter()
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@category",
                        Value = newProduct.Category,
                        Direction = ParameterDirection.Input
                    };
                    var producerParametr = new SqlParameter()
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@producer",
                        Value = newProduct.Producer,
                        Direction = ParameterDirection.Input
                    };
                    var countryParametr = new SqlParameter()
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@country",
                        Value = newProduct.Country,
                        Direction = ParameterDirection.Input
                    };
                    var priceParametr = new SqlParameter()
                    {
                        DbType = DbType.Double,
                        ParameterName = "@price",
                        Value = newProduct.Price,
                        Direction = ParameterDirection.Input
                    };
                    var characteristicsParametr = new SqlParameter()
                    {
                        DbType = DbType.String,
                        ParameterName = "@characteristics",
                        Value = newProduct.Characteristics,
                        Direction = ParameterDirection.Input
                    };
                    var idParametr = new SqlParameter()
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@id",
                        Value = newId,
                        Direction = ParameterDirection.Output
                    };
                    if (!newProduct.Photo.Any()) { photoParametr.Value = DBNull.Value; }
                    command.Parameters.Add(idParametr);
                    command.Parameters.Add(nameParametr);
                    command.Parameters.Add(categoryParametr);
                    command.Parameters.Add(producerParametr);
                    command.Parameters.Add(characteristicsParametr);
                    command.Parameters.Add(countryParametr);
                    command.Parameters.Add(priceParametr);
                    command.Parameters.Add(photoParametr);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(path))
                {
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.DeleteProduct";
                    var idParametr = new SqlParameter()
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@id",
                        Value = id,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(idParametr);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditProduct(int id, Product product)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(path))
                {
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.EditProduct";
                    var nameParametr = new SqlParameter()
                    {
                        DbType = DbType.String,
                        ParameterName = "@name",
                        Value = product.Name,
                        Direction = ParameterDirection.Input
                    };
                    var photoParametr = new SqlParameter()
                    {
                        DbType = DbType.Binary,
                        ParameterName = "@photo",
                        Value = product.Photo,
                        IsNullable = true,
                        Direction = ParameterDirection.Input
                    };
                    var categoryParametr = new SqlParameter()
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@category",
                        Value = product.Category,
                        Direction = ParameterDirection.Input
                    };
                    var producerParametr = new SqlParameter()
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@producer",
                        Value = product.Producer,
                        Direction = ParameterDirection.Input
                    };
                    var countryParametr = new SqlParameter()
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@country",
                        Value = product.Country,
                        Direction = ParameterDirection.Input
                    };
                    var priceParametr = new SqlParameter()
                    {
                        DbType = DbType.Double,
                        ParameterName = "@price",
                        Value = product.Price,
                        Direction = ParameterDirection.Input
                    };
                    var characteristicsParametr = new SqlParameter()
                    {
                        DbType = DbType.String,
                        ParameterName = "@characteristics",
                        Value = product.Characteristics,
                        Direction = ParameterDirection.Input
                    };
                    var idParametr = new SqlParameter()
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@id",
                        Value = id,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(idParametr);
                    command.Parameters.Add(nameParametr);
                    command.Parameters.Add(categoryParametr);
                    command.Parameters.Add(producerParametr);
                    command.Parameters.Add(characteristicsParametr);
                    command.Parameters.Add(countryParametr);
                    command.Parameters.Add(priceParametr);
                    command.Parameters.Add(photoParametr);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Dictionary<int, Product> GetAll()
        {
            _products.Clear();
            using (SqlConnection connection = new SqlConnection(path))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetProducts";
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product newProduct = new Product();

                    int newId = (int)reader["id"];
                    newProduct.Id = (int)newId;
                    newProduct.Name = reader["name"] as string;
                    newProduct.Category = (int)reader["category"];
                    newProduct.Producer = (int)reader["producer"];
                    newProduct.Characteristics = reader["characteristics"] as string;
                    newProduct.Country = (int)reader["country"];
                    newProduct.Photo = reader["photo"] as byte[];
                    newProduct.Price = (decimal)reader["price"];
                    _products.Add(newProduct.Id, newProduct);
                }
            }
            return _products;
        }

        public Dictionary<int, Product> GetAll(int category)
        {
            return GetAll().Where(x => x.Value.Category == category).ToDictionary(p=>p.Key, p=>p.Value);
        }

        public Product GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Key == id).Value;
        }
        public Dictionary<int, string> GetCategories()
        {
            _categories.Clear();
            using (SqlConnection connection = new SqlConnection(path))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetCategories";
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int newId = (int)reader["id"];
                    string name = reader["name"] as string;

                    _categories.Add(newId, name);
                }
            }
            return _categories;
        }
        public Dictionary<int, string> GetCountries()
        {
            _countries.Clear();
            using (SqlConnection connection = new SqlConnection(path))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetCountries";
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int newId = (int)reader["id"];
                    string name = reader["name"] as string;

                    _countries.Add(newId, name);
                }
            }
            return _countries;
        }
    }
}
