using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System;
using Greggs.Products.Infrastructure.Services;
using System.Linq;
using Greggs.Products.Api.Utils;
using Greggs.Products.Core.Common;

namespace Greggs.Products.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
      
        protected async Task<ServiceResponse<T>> HandleApiOperationAsync<T>(
       Func<Task<ServiceResponse<T>>> action, [CallerLineNumber] int lineNo = 0, [CallerMemberName] string method = "")
        {

            var serviceResponse = new ServiceResponse<T>
            {
                Code = HttpHelpers.GetStatusCodeValue(HttpStatusCode.OK),
                ShortDescription = "SUCCESS"
            };

            try
            {

                if (!ModelState.IsValid)
                    throw new GreggsGenericException("There were errors in your input, please correct them and try again.",
                        HttpHelpers.GetStatusCodeValue(HttpStatusCode.BadRequest));

                var actionResponse = await action();

                serviceResponse.Object = actionResponse.Object;
                serviceResponse.ShortDescription = actionResponse.ShortDescription ?? serviceResponse.ShortDescription;

            }
            catch (GreggsGenericException ex)
            {
                serviceResponse.ShortDescription = ex.Message;
                serviceResponse.Code = ex.ErrorCode;

                if (!ModelState.IsValid)
                {
                    serviceResponse.ValidationErrors = ModelState.ToDictionary(
                        m => {
                            var tokens = m.Key.Split('.');
                            return tokens.Length > 0 ? tokens[tokens.Length - 1] : tokens[0];
                        },
                        m => m.Value.Errors.Select(e => e.Exception?.Message ?? e.ErrorMessage)
                    );
                }
            }
            catch (Exception ex)
            {
                serviceResponse.ShortDescription = ex.Message;
                serviceResponse.Code = HttpHelpers.GetStatusCodeValue(HttpStatusCode.InternalServerError);
            }

            return serviceResponse;
        }
    }

}
