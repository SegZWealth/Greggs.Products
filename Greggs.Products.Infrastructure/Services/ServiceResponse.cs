using Greggs.Products.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greggs.Products.Infrastructure.Services
{
    public class ServiceResponse<T> : IServiceResponse<T>
    {
        public ServiceResponse(T response) : this()
        {
            Object = response;
        }

        public ServiceResponse()
        {
            ValidationErrors = new Dictionary<string, IEnumerable<string>>();
        }

        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public T Object { get; set; }

        public Dictionary<string, IEnumerable<string>> ValidationErrors { get; set; }
    }

}
