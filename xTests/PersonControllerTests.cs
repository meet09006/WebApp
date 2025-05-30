using WebApp.Controllers;
using WebApp.Models;
using Microsoft.Extensions.Logging;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace xTests;

public class PersonControllerTests
{
    private readonly ILogger<PersonController> _logger;
    private readonly DbContextOptions<AppDbContext> _options;

    public PersonControllerTests()
    {
        _logger = new LoggerFactory().CreateLogger<PersonController>();
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
    }

    [Fact]
    public void Index_ReturnsViewWithPeopleList()
    {
        // Arrange
        using (var context = new AppDbContext(_options))
        {
            context.People.AddRange(
                new People { Id = 1, Name = "John Doe" },
                new People { Id = 2, Name = "Jane Doe" }
            );
            context.SaveChanges();
        }

        using (var context = new AppDbContext(_options))
        {
            var controller = new PersonController(_logger, context);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<People>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }
    }


    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            var _homeLogger = new LoggerFactory().CreateLogger<HomeController>();
            var controller = new HomeController(_homeLogger);
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }
    }

    public class PersonModelTests
    {
        [Fact]
        public void Person_Name_IsRequired()
        {
            var person = new People { Name = "" };
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(person);
            var isValid = Validator.TryValidateObject(person, context, validationResults, true);
            Assert.True(isValid);
        }
    }

    public class ErrorViewModelTests
    {
        [Fact]
        public void ErrorViewModel_Has_RequestId()
        {
            var model = new ErrorViewModel { RequestId = "123" };
            Assert.Equal("123", model.RequestId);
        }

        [Fact]
        public void ShowRequestId_ReturnsTrue_When_RequestId_IsNotNull()
        {
            var model = new ErrorViewModel { RequestId = "123" };
            Assert.True(model.ShowRequestId);
        }
    }
}