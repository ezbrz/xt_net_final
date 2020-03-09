using Epam.Mercato.BLL.Interfaces;
using Epam.Mercato.DAO.Interfaces;
using Epam.Mercato.Entities;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Epam.Mercato.BLL
{
    public class ModeratorLogic : IModeratorLogic
    {
        private readonly IModeratorDAO _moderatorDAO;
        public ModeratorLogic(IModeratorDAO moderatorDAO)
        {
            _moderatorDAO = moderatorDAO;
        }
        public bool AddNewModerator(string login, string password, Role role)
        {
            if (_moderatorDAO.IsUniqueLogin(login)&&IsValidLogin(login)&&IsValidPassword(password))
            {
                SHA512 shaM = new SHA512Managed();
                string result = string.Join("", shaM.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return _moderatorDAO.AddNewModerator(new Moderator() {Login=login, ModeratorRole = role, Password=result });
            }
            else
            {
                return false;
            }
        }
        public bool IsValidLogin(string login)
        {
            string pattern = @"^[a-zA-Z][\w\-]{4,}[a-zA-Z0-9]$";
            return Regex.IsMatch(login, pattern);
        }
        public bool IsValidPassword(string password)
        {
            string pattern = @"^[a-zA-Z0-9\!\.\$]{8,}$";
            return Regex.IsMatch(password, pattern);
        }
        public Dictionary<int, Moderator> GetAll()
        {
            return _moderatorDAO.GetAll();
        }
        public bool DeleteById(int id)
        {
            return _moderatorDAO.DeleteById(id);
        }
        public Moderator GetById(int id)
        {
            return _moderatorDAO.GetById(id);
        }
        public bool IsUniqueLogin(string login)
        {
            return _moderatorDAO.IsUniqueLogin(login);
        }
        public bool EditModerator(int id, string login, string password, Role role)
        {
            if ((_moderatorDAO.GetById(id).Login==login||_moderatorDAO.IsUniqueLogin(login)) && IsValidLogin(login) && IsValidPassword(password))
            {
                SHA512 shaM = new SHA512Managed();
                string result = string.Join("", shaM.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return _moderatorDAO.EditModerator(id, new Moderator() { Login = login, ModeratorRole = role, Password = result });
            }
            else
            {
                return false;
            }
        }
        public string GetPassword(int id)
        {
            return _moderatorDAO.GetPassword(id);
        }
    }
}
