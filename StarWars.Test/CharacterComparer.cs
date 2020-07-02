using StarWars.Model;
using System;
using System.Linq;

namespace StarWars.Test
{
    internal static class CharacterComparer
    {
        public static bool IsDataEqual(this Character original, Character other)
        {
            if (other == null)
            {
                return false;
            }
            return original.Name == other.Name
                && original.Planet == other.Planet
                && original.Episodes.SequenceEqual(other.Episodes)
                && original.Friends.SequenceEqual(other.Friends);
        }
    }
}
