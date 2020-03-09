using Epam.Mercato.BLL.Interfaces;
using Epam.Mercato.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Mercato.BLL
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDAO _userDAO;
        public UserLogic(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }
    }
}
