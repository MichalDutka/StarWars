using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StarWars.Controllers;
using StarWars.DTO;
using StarWars.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Test
{
    [TestClass]
    public class CharacterControllerTest
    {
        [TestMethod]
        public async Task TestGetCharacters()
        {
            Mock<IRepository<Character>> mockRepository = new Mock<IRepository<Character>>();
            mockRepository.Setup(r => r.ReadAll()).ReturnsAsync(TestData.GetCharacters());

            CharactersController controller = new CharactersController(mockRepository.Object);
            ActionResult<IEnumerable<CharacterDTO>> response = await controller.GetCharacters();
            List<CharacterDTO> characters = response.Value.ToList();

            Assert.AreEqual(TestData.GetCharacters().Count, characters.Count);

            for (int i = 0; i < TestData.GetCharacters().Count; i++)
            {
                Assert.IsTrue(TestData.GetCharacters()[i].IsDataEqual(new Character(characters[i])));
            }
        }

        [TestMethod]
        public async Task TestGetCharactersPaged()
        {
            Mock<IRepository<Character>> mockRepository = new Mock<IRepository<Character>>();
            mockRepository.Setup(r => r.ReadAll()).ReturnsAsync(TestData.GetCharacters());

            CharactersController controller = new CharactersController(mockRepository.Object);
            ActionResult<IEnumerable<CharacterDTO>> response = await controller.GetCharactersPaged(1, 0);
            List<CharacterDTO> characters = response.Value.ToList();

            Assert.AreEqual(1, characters.Count);

            for (int i = 0; i < characters.Count; i++)
            {
                Assert.IsTrue(TestData.GetCharacters()[i].IsDataEqual(new Character(characters[i])));
            }
        }


        [TestMethod]
        public async Task TestGetCharacter()
        {
            Mock<IRepository<Character>> mockRepository = new Mock<IRepository<Character>>();
            mockRepository.Setup(r => r.Read(1)).ReturnsAsync(TestData.GetCharacters()[0]);

            CharactersController controller = new CharactersController(mockRepository.Object);
            ActionResult<CharacterDTO> response = await controller.GetCharacter(1);
            CharacterDTO character = response.Value;

            Assert.IsTrue(TestData.GetCharacters()[0].IsDataEqual(new Character(character)));
        }

        [TestMethod]
        public async Task TestPutCharacter()
        {
            Character updatedCharacter = TestData.GetCharacters()[0];
            updatedCharacter.Name = "Lucas Skywalker";

            CharacterDTO updatedCharacterDTO = new CharacterDTO(updatedCharacter);

            Mock<IRepository<Character>> mockRepository = new Mock<IRepository<Character>>();
            mockRepository.Setup(r => r.Update(It.IsAny<Character>())).ReturnsAsync(updatedCharacter);

            CharactersController controller = new CharactersController(mockRepository.Object);
            IActionResult responseNoContent = await controller.PutCharacter(1, updatedCharacterDTO);

            mockRepository.Setup(r => r.Update(It.IsAny<Character>())).Throws(new DbUpdateConcurrencyException());

            IActionResult responseNotFound = await controller.PutCharacter(5, updatedCharacterDTO);

            Assert.AreEqual(((StatusCodeResult)responseNoContent).StatusCode, 204);
            Assert.AreEqual(((StatusCodeResult)responseNotFound).StatusCode, 404);
        }

        [TestMethod]
        public async Task TestPostCharacter()
        {
            Mock<IRepository<Character>> mockRepository = new Mock<IRepository<Character>>();
            mockRepository.Setup(r => r.Create(It.IsAny<Character>())).ReturnsAsync(TestData.GetCharacters()[0]);

            CharactersController controller = new CharactersController(mockRepository.Object);
            ActionResult<CharacterDTO> response = await controller.PostCharacter(new CharacterDTO(TestData.GetCharacters()[0]));
            CharacterDTO character = ((CreatedAtActionResult)response.Result).Value as CharacterDTO;

            Assert.IsTrue(TestData.GetCharacters()[0].IsDataEqual(new Character(character)));
        }

        [TestMethod]
        public async Task TestDeleteCharacter()
        {
            Mock<IRepository<Character>> mockRepository = new Mock<IRepository<Character>>();
            mockRepository.Setup(r => r.Delete(1)).ReturnsAsync(TestData.GetCharacters()[0]);
            mockRepository.Setup(r => r.Delete(5)).ReturnsAsync((Character)null);

            CharactersController controller = new CharactersController(mockRepository.Object);
            ActionResult<CharacterDTO> responseDeleted = await controller.DeleteCharacter(1);
            CharacterDTO character = responseDeleted.Value;
            ActionResult<CharacterDTO> responseNotFound = await controller.DeleteCharacter(5);

            Assert.IsTrue(TestData.GetCharacters()[0].IsDataEqual(new Character(character)));
            Assert.AreEqual(((StatusCodeResult)responseNotFound.Result).StatusCode, 404);
        }

    }
}
