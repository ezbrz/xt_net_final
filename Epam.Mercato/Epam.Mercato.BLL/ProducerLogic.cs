using Epam.Mercato.BLL.Interfaces;
using Epam.Mercato.DAO.Interfaces;
using Epam.Mercato.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Mercato.BLL
{
    public class ProducerLogic : IProducerLogic
    {
        private readonly IProducerDAO _producerDAO;
        public ProducerLogic(IProducerDAO producerDAO)
        {
            _producerDAO = producerDAO;
        }
        public bool AddNewProducer(Producer newProducer)
        {
            return _producerDAO.AddNewProducer(newProducer);
        }

        public bool DeleteById(int id)
        {
            return _producerDAO.DeleteById(id);
        }

        public bool EditProducer(int id, Producer producer)
        {
            return _producerDAO.EditProducer(id, producer);
        }

        public Dictionary<int, Producer> GetAll()
        {
            return _producerDAO.GetAll();
        }

        public Producer GetById(int id)
        {
            return _producerDAO.GetById(id);
        }
    }
}
