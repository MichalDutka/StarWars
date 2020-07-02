using StarWars.Model;
using System.Collections.Generic;

namespace StarWars.Test
{
    public static class TestData
    {
        public static List<Character> GetCharacters()
        {
            return new List<Character>()
            {
                new Character()
                {
                    Id = 1,
                    Name = "Luke Skywalker",
                    Episodes = new string[]
                    {
                    "NEWHOPE",
                    "EMPIRE",
                    "JEDI"
                    },
                    Friends = new string[]
                    {
                    "Han Solo",
                    "Leia Organa",
                    "C-3PO",
                    "R2-D2"
                    }
                },
                new Character()
                {
                    Id = 2,
                    Name = "Darth Vader",
                    Episodes = new string[]
                    {
                        "NEWHOPE",
                        "EMPIRE",
                        "JEDI"
                    },
                    Friends = new string[]
                    {
                        "Wilhuff Tarkin"
                    }
                }
            };
        }

    }
}
