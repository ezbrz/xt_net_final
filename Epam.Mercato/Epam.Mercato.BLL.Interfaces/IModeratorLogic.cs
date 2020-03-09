using Epam.Mercato.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Mercato.BLL.Interfaces
{
    public interface IModeratorLogic
    {
        bool AddNewModerator(string login, string password, Role role);
        bool IsValidLogin(string login);
        bool IsValidPassword(string password);
        bool IsUniqueLogin(string login);
        Dictionary<int, Moderator> GetAll();
        bool DeleteById(int id);
        Moderator GetById(int id);
        bool EditModerator(int id, string login, string password, Role role);
        string GetPassword(int id);
    }
}
