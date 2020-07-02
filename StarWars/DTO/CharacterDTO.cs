using StarWars.Model;
using System.Collections.Generic;

namespace StarWars.DTO
{
    public class CharacterDTO
    {
        public string Name { get; set; }
        public string Planet { get; set; }
        public string[] Episodes { get; set; }
        public string[] Friends { get; set; }

        public CharacterDTO(Character character)
        {
            Name = character.Name;
            Planet = character.Planet;
            Episodes = new List<string>(character.Episodes).ToArray();
            Friends = new List<string>(character.Friends).ToArray();
        }

        CharacterDTO() { }
    }
}
