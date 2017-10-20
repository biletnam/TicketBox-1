using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TicketBox.Domain.Abstract;
using TicketBox.Domain.Entities;

namespace TicketBox.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new List<Event>
            {
                new Event { Id = 1, Name="Event 1", Description="desc", Location="City", SpecialEvent = 0, TimeEvent=DateTime.Now},
                new Event { Id = 2, Name="Event 2", Description="desc 2", Location="City 2", SpecialEvent = 1, TimeEvent=DateTime.Now },
                new Event { Id = 3, Name="Event 3", Description="desc 3", Location="City 3", SpecialEvent = 2, TimeEvent=DateTime.Now }
            });
            kernel.Bind<IEventRepository>().ToConstant(mock.Object);
        }
    }
}