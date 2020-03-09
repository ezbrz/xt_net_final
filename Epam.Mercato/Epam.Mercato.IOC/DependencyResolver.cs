using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.Mercato.DAL;
using Epam.Mercato.BLL;
using Epam.Mercato.DAO.Interfaces;
using Epam.Mercato.BLL.Interfaces;

namespace Epam.Mercato.IOC
{
    public static class DependencyResolver
    {
        public static IUserDAO UserDAO;
        public static IProductDAO ProductDAO;
        public static IModeratorDAO ModeratorDAO;
        public static IProducerDAO ProducerDAO;

        private static IUserLogic _userLogic;
        private static IProductLogic _productLogic;
        private static IModeratorLogic _moderatorLogic;
        private static IProducerLogic _producerLogic;

        public static IUserLogic UserLogic => _userLogic ?? (_userLogic = new UserLogic(UserDAO));
        public static IProductLogic ProductLogic => _productLogic ?? (_productLogic = new ProductLogic(ProductDAO));
        public static IModeratorLogic ModeratorLogic => _moderatorLogic ?? (_moderatorLogic = new ModeratorLogic(ModeratorDAO));
        public static IProducerLogic ProducerLogic => _producerLogic ?? (_producerLogic = new ProducerLogic(ProducerDAO));

        static DependencyResolver()
        {
            UserDAO = new UserDbDAO();
            ProductDAO = new ProductDbDAO();
            ModeratorDAO = new ModeratorDbDAO();
            ProducerDAO = new ProducerDbDAO();
        }
    }
}
