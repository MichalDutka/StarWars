using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Model
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string[] Episodes { get; set; }
        public string[] Friends { get; set; }
    }
}
