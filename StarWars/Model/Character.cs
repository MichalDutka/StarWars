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
        public string Planet { get; set; }
        public string[] Episodes { get; set; }
        public string[] Friends { get; set; }

        public override bool Equals(object obj)
        {
            Character other = obj as Character;
            if (other == null)
            {
                return false;
            }
            return Id == other.Id
                && Name == other.Name
                && Planet == other.Planet
                && Episodes.SequenceEqual(other.Episodes)
                && Friends.SequenceEqual(other.Friends);
        }
    }
}
