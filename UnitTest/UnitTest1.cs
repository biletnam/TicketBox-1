using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TicketBox.WebUI.Models;
using TicketBox.WebUI.Controllers;
using TicketBox.WebUI.ViewModel;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Event e1 = new Event() { Name = "Тетар 1", EventId = 1 };
            Event e2 = new Event() { Name = "Спорт 1", EventId = 2 };

            Cart cart = new Cart();
            cart.AddItem(e1, 1);
            cart.AddItem(e2, 1);
            List<CartLine> results = cart.Lines.ToList();

            Assert.AreEqual(results.Count(), 2);
        }
    }
}
