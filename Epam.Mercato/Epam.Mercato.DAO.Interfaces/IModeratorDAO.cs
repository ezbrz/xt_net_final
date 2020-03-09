using Epam.Mercato.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Mercato.DAO.Interfaces
{
    public interface IModeratorDAO
    {
        bool IsUniqueLogin(string login);
        bool AddNewModerator(Moderator newModerator);
        Dictionary<int, Moderator> GetAll();
        bool DeleteById(int id);
        Moderator GetById(int id);
        bool EditModerator(int id, Moderator moderator);
        string GetPassword(int id);
    }
}
