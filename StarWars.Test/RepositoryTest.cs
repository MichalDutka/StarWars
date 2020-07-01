using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StarWars.Data;
using StarWars.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarWars.Test
{
    [TestClass]
    public class RepositoryTest
    {
        private List<Character> characters = new List<Character>()
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

        [TestMethod]
        public async Task TestCreate()
        {
            Character newCharacter = new Character()
            {
                Name = "Leia Organa",
                Episodes = new string[]
                {
                    "NEWHOPE",
                    "EMPIRE",
                    "JEDI"
                },
                Planet = "Alderaan",
                Friends = new string[]
                {
                    "Luke Skywalker",
                    "Han Solo",
                    "C-3PO",
                    "R2-D2"
                }
            };

            using (StarWarsContext context = await CreateContext("TestCreate"))
            {
                Repository<Character> repository = new Repository<Character>(context);

                Character result = await repository.Create(newCharacter);

                Assert.AreEqual(3, context.Set<Character>().Count());
                Assert.AreEqual(newCharacter, result);
            }

        }


        [TestMethod]
        public async Task TestReadAll()
        {
            using (StarWarsContext context = await CreateContext("TestReadAll"))
            {
                Repository<Character> repository = new Repository<Character>(context);

                List<Character> result = (await repository.ReadAll()).ToList();

                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(characters[0], result[0]);
                Assert.AreEqual(characters[1], result[1]);
            }
        }

        [TestMethod]
        public async Task TestRead()
        {
            using (StarWarsContext context = await CreateContext("TestRead"))
            {
                Repository<Character> repository = new Repository<Character>(context);

                Character result = await repository.Read(1);

                Assert.AreEqual(characters[0], result);
            }
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            Character updatedCharacter = characters[0];
            updatedCharacter.Name = "Lucas Skywalker";

            using (StarWarsContext context = await CreateContext("TestUpdate"))
            {
                Repository<Character> repository = new Repository<Character>(context);

                Character result = await repository.Update(1, updatedCharacter);

                Assert.AreEqual(updatedCharacter, result);
            }
        }


        [TestMethod]
        public async Task TestDelete()
        {       
            using (StarWarsContext context = await CreateContext("TestDelete"))
            {
                Repository<Character> repository = new Repository<Character>(context);

                Character result = await repository.Delete(2);

                Assert.AreEqual(1, context.Set<Character>().Count());
                Assert.AreEqual(characters[1], result);
            }
        }

        private async Task<StarWarsContext> CreateContext(string databaseName)
        {
            DbContextOptions<StarWarsContext> options = new DbContextOptionsBuilder<StarWarsContext>().UseInMemoryDatabase(databaseName).Options;

            StarWarsContext context = new StarWarsContext(options);

            context.Set<Character>().AddRange(characters);
            await context.SaveChangesAsync();

            return context;
        }
    }
}
