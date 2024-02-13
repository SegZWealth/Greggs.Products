using System.Net;

namespace Greggs.Products.Api.Utils
{
    public static class HttpHelpers
    {
        public static string GetStatusCodeValue(this HttpStatusCode code)
        {
            return ((int)code).ToString();
        }
    }
}
