using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication6.Controllers;
using WebApplication6.Models;
using Xunit;

namespace ProjectTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexGetAllAuthorsTest()
        {
            //Arrange
            var mockDbContext = new Mock<BookStoreContext>();
            mockDbContext.Setup(context => context.Author.ToList()).Returns(GetTestAuthors());
            var controller = new HomeController(mockDbContext.Object);

            //Act
            var result = controller.Index();

            //Assert

            Assert.IsType<ViewResult>(result);

        }

        public List<Author> GetTestAuthors()
        {
            List<Author> authors = new List<Author>();
            authors.Add(new Author { FirstName = "Tolstoy" });
            authors.Add(new Author { FirstName = "Gogol" });
            return authors;
        }
    }
}
