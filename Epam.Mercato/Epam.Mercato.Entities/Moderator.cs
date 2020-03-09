using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Mercato.Entities
{
    public class Moderator
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Role ModeratorRole { get; set; }
    }
    public enum Role
    {
        administrator,
        moderator
    }
}
