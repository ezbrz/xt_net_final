using Epam.Mercato.DAO.Interfaces;
using Epam.Mercato.Entities;
using Epam.Mercato.Handlers;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace Epam.Mercato.DAL
{
    public class ModeratorDbDAO : IModeratorDAO
    {
        private readonly ILog _logger = Logger.Log;
        static readonly string path = DataMode.GetPath("DBconnection");
        private static Dictionary<int, Moderator> _moderators = new Dictionary<int, Moderator>();
        public bool IsUniqueLogin(string login)
        {
            if(GetAll().Values.FirstOrDefault(x=>x.Login==login)==null)
            {
                return true;
            }
            return false;
        }
        public Dictionary<int, Moderator> GetAll()
        {
            _moderators.Clear();
            using (SqlConnection connection = new SqlConnection(path))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetModerators";
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Moderator newModerator = new Moderator();
                    int newId = (int)reader["id"];
                    newModerator.Login = reader["login"] as string;
                    Enum.TryParse(reader["role"] as string, out Role newRole);
                    newModerator.ModeratorRole = newRole;
                    newModerator.Password = null;
                    _moderators.Add(newId, newModerator);
                }
            }
            return _moderators;
        }
        public bool AddNewModerator(Moderator newModerator)
        {
            var newId = GetAll().Keys.Count > 0
            ? GetAll().Keys.Max()+1
            : 1;
            try
            {
                using (SqlConnection connection = new SqlConnection(path))
                {
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.AddModerator";
                    var loginParametr = new SqlParameter()
                    {
                        DbType = DbType.String,
                        ParameterName = "@login",
                        Value = newModerator.Login,
                        Direction = ParameterDirection.Input
                    };
                    var passParametr = new SqlParameter()
                    {
                        DbType = DbType.String,
                        ParameterName = "@password",
                        Value = newModerator.Password,
                        Direction = ParameterDirection.Input
                    };
                    var roleParametr = new SqlParameter()
                    {
                        DbType = DbType.String,
                        ParameterName = "@role",
                        Value = newModerator.ModeratorRole,
                        Direction = ParameterDirection.Input
                    };
                    var idParametr = new SqlParameter()
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@id",
                        Value = newId,
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(idParametr);
                    command.Parameters.Add(loginParametr);
                    command.Parameters.Add(passParametr);
                    command.Parameters.Add(roleParametr);
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
        public Moderator GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Key == id).Value;
        }
        public string GetPassword(int id)
        {
            string password = null;
            using (SqlConnection connection = new SqlConnection(path))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.GetPassword";
                var idParametr = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@id",
                    Value = id,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(idParametr);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    password = reader["password"] as string;
                }
                return password;
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
                    command.CommandText = "dbo.DeleteModerator";
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
        public bool EditModerator(int id, Moderator moderator)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(path))
                {
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.EditModerator";
                    var loginParametr = new SqlParameter()
                    {
                        DbType = DbType.String,
                        ParameterName = "@login",
                        Value = moderator.Login,
                        Direction = ParameterDirection.Input
                    };
                    var passParametr = new SqlParameter()
                    {
                        DbType = DbType.String,
                        ParameterName = "@password",
                        Value = moderator.Password,
                        Direction = ParameterDirection.Input
                    };
                    var roleParametr = new SqlParameter()
                    {
                        DbType = DbType.String,
                        ParameterName = "@role",
                        Value = moderator.ModeratorRole,
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
                    command.Parameters.Add(loginParametr);
                    command.Parameters.Add(passParametr);
                    command.Parameters.Add(roleParametr);
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

    }
}
