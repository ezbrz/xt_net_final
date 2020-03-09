using Epam.Mercato.DAO.Interfaces;
using Epam.Mercato.Entities;
using Epam.Mercato.Handlers;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Mercato.DAL
{
    public class ProducerDbDAO : IProducerDAO
    {
        private readonly ILog _logger = Logger.Log;
        static readonly string path = DataMode.GetPath("DBconnection");
        private static Dictionary<int, Producer> _producers = new Dictionary<int, Producer>();
        public bool AddNewProducer(Producer newProducer)
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
                    command.CommandText = "dbo.AddProducer";
                    var nameParametr = new SqlParameter()
                    {
                        DbType = DbType.String,
                        ParameterName = "@name",
                        Value = newProducer.Name,
                        Direction = ParameterDirection.Input
                    };
                    var logoParametr = new SqlParameter()
                    {
                        DbType = DbType.Binary,
                        ParameterName = "@logo",
                        Value = newProducer.Logo,
                        IsNullable = true,
                        Direction = ParameterDirection.Input
                    };
                    var idParametr = new SqlParameter()
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@id",
                        Value = newId,
                        Direction = ParameterDirection.Input
                    };
                    if (!newProducer.Logo.Any()) { logoParametr.Value = DBNull.Value; }
                    command.Parameters.Add(idParametr);
                    command.Parameters.Add(nameParametr);
                    command.Parameters.Add(logoParametr);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (SqlException e)
            {
                _logger.Error(e.Message);
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
                    command.CommandText = "dbo.DeleteProducer";
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
            catch (SqlException e)
            {
                _logger.Error(e.Message);
                return false;
            }
        }

        public bool EditProducer(int id, Producer producer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(path))
                {
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.EditProducer";
                    var nameParametr = new SqlParameter()
                    {
                        DbType = DbType.String,
                        ParameterName = "@name",
                        Value = producer.Name,
                        Direction = ParameterDirection.Input
                    };
                    var logoParametr = new SqlParameter()
                    {
                        DbType = DbType.Binary,
                        ParameterName = "@logo",
                        Value = producer.Logo,
                        IsNullable = true,
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
                    command.Parameters.Add(logoParametr);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (SqlException e)
            {
                _logger.Error(e.Message);
                return false;
            }
        }

        public Dictionary<int, Producer> GetAll()
        {
            _producers.Clear();
            using (SqlConnection connection = new SqlConnection(path))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetProducers";
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Producer newProducer = new Producer();

                    int newId = (int)reader["id"];
                    newProducer.Id = (int)newId;
                    newProducer.Name = reader["name"] as string;
                    newProducer.Logo = reader["logo"] as byte[];
                    _producers.Add(newProducer.Id, newProducer);
                }
            }
            return _producers;
        }

        public Producer GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Key == id).Value;
        }
    }
}
