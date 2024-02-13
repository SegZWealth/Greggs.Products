using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greggs.Products.Core.Common
{
    [Serializable]
    public class GreggsGenericException : Exception
    {
        public string ErrorCode { get; set; }

        public GreggsGenericException(string message) : base(message)
        { }

        public GreggsGenericException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode; 
        }
    }
}
