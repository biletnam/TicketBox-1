using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketBox.Domain.Abstract;
using Moq;
using TicketBox.Domain.Entities;
using System.Collections.Generic;
using TicketBox.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using TicketBox.WebUI.Models;
using System;
using TicketBox.WebUI.HtmlHelpers;

namespace TicketBox.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new List<Event>
            {
                new Event { EventId = 1, Name = "Игра1"},
                new Event { EventId = 2, Name = "Игра2"},
                new Event { EventId = 3, Name = "Игра3"},
                new Event { EventId = 4, Name = "Игра4"},
                new Event { EventId = 5, Name = "Игра5"}
            });
            EventController controller = new EventController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            IEnumerable<Event> result = (IEnumerable<Event>)controller.List(2).Model;

            // Утверждение (assert)
            List<Event> games = result.ToList();
            Assert.IsTrue(games.Count == 2);
            Assert.AreEqual(games[0].Name, "Игра4");
            Assert.AreEqual(games[1].Name, "Игра5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }
    }
}
