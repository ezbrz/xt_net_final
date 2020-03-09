using Epam.Mercato.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Mercato.BLL.Interfaces
{
    public interface IProducerLogic
    {
        bool AddNewProducer(Producer newProducer);
        Dictionary<int, Producer> GetAll();
        bool DeleteById(int id);
        Producer GetById(int id);
        bool EditProducer(int id, Producer producer);
    }
}
