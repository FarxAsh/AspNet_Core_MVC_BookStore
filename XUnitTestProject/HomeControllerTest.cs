using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using WebApplication6.Controllers;
using WebApplication6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace XUnitTestProject
{
    public class HomeControllerTest
    {

       
        [Fact]
        public void IndexGetAllAuthorsTest()
        {
            //Arrange
            var mockDbContext = new Mock<BookStoreContext>();
            mockDbContext.Setup(context => context.Author.Where(x => x.FirstName != "").ToList()).Returns(GetTestAuthors());
            var controller = new HomeController(mockDbContext.Object);

            //Act
            var result = controller.Index();

            //Assert

            Assert.IsType<ViewResult>(result);
         
        }

        public List<Author> GetTestAuthors()
        {
            List <Author> authors = new List<Author>();
            authors.Add(new Author { FirstName = "Tolstoy" });
            authors.Add(new Author { FirstName = "Gogol" });
            return authors;
        }
    }
}
