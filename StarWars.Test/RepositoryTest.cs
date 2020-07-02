using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarWars.Data;
using StarWars.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Test
{
    [TestClass]
    public class RepositoryTest
    {

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
                Assert.IsTrue(newCharacter.IsDataEqual(result));
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
                Assert.IsTrue(TestData.GetCharacters()[0].IsDataEqual(result[0]));
                Assert.IsTrue(TestData.GetCharacters()[1].IsDataEqual(result[1]));
            }
        }

        [TestMethod]
        public async Task TestRead()
        {
            using (StarWarsContext context = await CreateContext("TestRead"))
            {
                Repository<Character> repository = new Repository<Character>(context);

                Character result = await repository.Read(1);
                Assert.IsTrue(TestData.GetCharacters()[0].IsDataEqual(result));
            }
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            using (StarWarsContext context = await CreateContext("TestUpdate"))
            {
                Character updatedCharacter = context.Set<Character>().Find(1);
                updatedCharacter.Name = "Lucas Skywalker";

                Repository<Character> repository = new Repository<Character>(context);

                Character result = await repository.Update(updatedCharacter);

                Assert.IsTrue(updatedCharacter.IsDataEqual(result));
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
                Assert.IsTrue(TestData.GetCharacters()[1].IsDataEqual(result));
            }
        }

        private async Task<StarWarsContext> CreateContext(string databaseName)
        {
            DbContextOptions<StarWarsContext> options = new DbContextOptionsBuilder<StarWarsContext>().UseInMemoryDatabase(databaseName).Options;

            StarWarsContext context = new StarWarsContext(options);

            context.Set<Character>().AddRange(TestData.GetCharacters());
            await context.SaveChangesAsync();

            return context;
        }
    }
}
