using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NutritionalRecipeBook.Api.Controllers;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Api.UnitTests
{
    public class Tests
    {
       // private Mock<ILogger<DummyController>> _loggerMock;
      //  private Mock<IDummyService> _dummyServiceMock;
       // private DummyController _dummyController;

        [SetUp]
        public void Setup()
        {
          //  _loggerMock = new Mock<ILogger<DummyController>>();
           // _dummyServiceMock = new Mock<IDummyService>();
           // _dummyController = new DummyController(_dummyServiceMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetDummyDetails_ValidIdParameter_ReturnsOkResultWithBaseEntity()
        {
            //Arrange
            //var id = Guid.NewGuid();
      
            //_dummyServiceMock.Setup(x => x.GetDummy(id)).Returns(Task.FromResult(new BaseEntity { Id = id }));

            //Act
           // var result = await _dummyController.Details(id) as OkObjectResult;

            //Assert
            //Assert.NotNull(result);
           // Assert.That(result.StatusCode, Is.EqualTo(200));
           // Assert.That(((BaseEntity)result.Value)?.Id ?? Guid.Empty, Is.EqualTo(id));
            //_dummyServiceMock.Verify(x => x.GetDummy(It.IsAny<Guid>()), Times.Once);
        }
    }
}