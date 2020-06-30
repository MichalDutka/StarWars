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

            DbContextOptions<StarWarsContext> options = new DbContextOptionsBuilder<StarWarsContext>().UseInMemoryDatabase("TestCreate").Options;

            using (StarWarsContext context = new StarWarsContext(options))
            {
                context.Set<Character>().AddRange(characters);
                await context.SaveChangesAsync();

                Repository<Character> repository = new Repository<Character>(context);

                Character result = await repository.Create(newCharacter);

                Assert.AreEqual(3, context.Set<Character>().Count());
                Assert.AreEqual(newCharacter, result);
            }

        }


        [TestMethod]
        public async Task TestReadAll()
        {
            DbContextOptions<StarWarsContext> options = new DbContextOptionsBuilder<StarWarsContext>().UseInMemoryDatabase("TestReadAll").Options;

            using (StarWarsContext context = new StarWarsContext(options))
            {
                context.Set<Character>().AddRange(characters);
                await context.SaveChangesAsync();

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
            DbContextOptions<StarWarsContext> options = new DbContextOptionsBuilder<StarWarsContext>().UseInMemoryDatabase("TestRead").Options;

            using (StarWarsContext context = new StarWarsContext(options))
            {
                context.Set<Character>().AddRange(characters);
                await context.SaveChangesAsync();

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

            DbContextOptions<StarWarsContext> options = new DbContextOptionsBuilder<StarWarsContext>().UseInMemoryDatabase("TestUpdate").Options;

            using (StarWarsContext context = new StarWarsContext(options))
            {
                context.Set<Character>().AddRange(characters);
                await context.SaveChangesAsync();

                Repository<Character> repository = new Repository<Character>(context);

                Character result = await repository.Update(1, updatedCharacter);

                Assert.AreEqual(updatedCharacter, result);
            }
        }


        [TestMethod]
        public async Task TestDelete()
        {
            DbContextOptions<StarWarsContext> options = new DbContextOptionsBuilder<StarWarsContext>().UseInMemoryDatabase("TestDelete").Options;

            using (StarWarsContext context = new StarWarsContext(options))
            {
                context.Set<Character>().AddRange(characters);
                await context.SaveChangesAsync();

                Repository<Character> repository = new Repository<Character>(context);

                Character result = await repository.Delete(2);

                Assert.AreEqual(1, context.Set<Character>().Count());
                Assert.AreEqual(characters[1], result);
            }
        }
    }
}
