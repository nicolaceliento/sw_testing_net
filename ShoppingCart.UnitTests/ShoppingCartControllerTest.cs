using NUnit.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Contracts;
using ShoppingCart.Controllers;
using ShoppingCart.Model;

namespace ShoppingCart.UnitTests
{
    public class ShoppingCartControllerTest
    {
        ShoppingCartController _controller;
        IShoppingCartService _service;

        /// <summary>
        /// OneTimeSetUp method that is called once to perform setup before any child tests are run
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _service = new ShoppingCartServiceFake();
            _controller = new ShoppingCartController(_service);
        }

        /// <summary>
        /// Verify GET returns some elements
        /// </summary>
        [Test]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        /// <summary>
        /// Verify GET returns elements > 1
        /// </summary>
        [Test]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;

            // Assert
            var items = okResult?.Value as List<ShoppingItem>;

            Assert.IsTrue(items?.Count > 1);
        }

        /// <summary>
        /// Verify GET by ID returns NotFound with ID not present
        /// </summary>
        [Test]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(notFoundResult.Result);
        }

        /// <summary>
        /// Verify GET by ID returns element with ID present
        /// </summary>
        [Test]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResult = _controller.Get(testGuid);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        /// <summary>
        /// Verify GET by ID returns element by ID with expected attribute values
        /// </summary>
        [Test]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResult = _controller.Get(testGuid).Result as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<ShoppingItem>(okResult?.Value, "Invalid type.");
            Assert.That((okResult?.Value as ShoppingItem)?.Id, Is.EqualTo(testGuid), $"Expected ID {testGuid} found ID {(okResult?.Value as ShoppingItem)?.Id}");
            Assert.That((okResult?.Value as ShoppingItem)?.Name, Is.EqualTo("Orange Juice"), $"Expected Name Orange Juice found {(okResult?.Value as ShoppingItem)?.Name}");
        }

        /// <summary>
        /// Verify POST with INVALID model state (missing required prop)
        /// </summary>
        [Test]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new ShoppingItem()
            {
                Manufacturer = "Guinness",
                Price = 12.00M
            };

            // Act
            var badResponse = _controller.Post(nameMissingItem);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(badResponse);
        }

        /// <summary>
        /// Verify POST with VALID model state (all required prop)
        /// </summary>
        [Test]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new ShoppingItem()
            {
                Name = "Guinness Original 6 Pack",
                Manufacturer = "Guinness",
                Price = 12.00M
            };

            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse?.Value as ShoppingItem;

            // Assert
            Assert.IsInstanceOf<ShoppingItem>(item, "Invalid type.");
            Assert.That(item?.Name, Is.EqualTo("Guinness Original 6 Pack"), $"Expected Name 'Guinness Original 6 Pack' found {item?.Name}");
        }

        /// <summary>
        /// Verify DELETE with not existing ID
        /// </summary>
        [Test]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();

            // Act
            var badResponse = _controller.Remove(notExistingGuid);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(badResponse);
        }

        [Test]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResponse = _controller.Remove(existingGuid);

            // Assert
            Assert.IsInstanceOf<OkResult>(okResponse);
        }

        [Test]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var existingGuid = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f");

            // Act
            var okResponse = _controller.Remove(existingGuid);

            // Assert
            Assert.That(_service.GetAllItems().Count(i => i.Id == existingGuid), Is.EqualTo(0));
        }
    }
}