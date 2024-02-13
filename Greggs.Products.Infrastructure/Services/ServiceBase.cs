using Greggs.Products.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greggs.Products.Infrastructure.Services
{
    public abstract class ServiceBase
    {
        public IUnitOfWork _uow;
        protected ServiceBase(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
