using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Backend.Providers
{
    public class VersionErrorResponseProvider : IErrorResponseProvider
    {
        public IActionResult CreateResponse(ErrorResponseContext context)
        {
            return new ObjectResult(new MessageModel { Message = "Not Up-to-date api" })
            {
                StatusCode = (int)HttpStatusCode.MethodNotAllowed
            };
        }
    }
}
