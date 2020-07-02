using StarWars.DTO;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Model
{
    public class Character
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Planet { get; set; }
        public string[] Episodes { get; set; }
        public string[] Friends { get; set; }

        public Character() { }

        public Character(CharacterDTO characterDTO)
        {
            Name = characterDTO.Name;
            Planet = characterDTO.Planet;
            Episodes = characterDTO.Episodes;
            Friends = characterDTO.Friends;
        }
    }
}
