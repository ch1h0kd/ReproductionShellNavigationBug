using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Models
{
    public class Client
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }

        public Client Clone() => MemberwiseClone() as Client;

    }
}
