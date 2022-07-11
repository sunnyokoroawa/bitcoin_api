using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitcoin.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception.GetBaseException(), $"Uncaught exception occured {context.HttpContext.Request.Path}");
            //context.ExceptionHandled = true;

            //send email async

            //redirect
            //context.HttpContext.Response.Redirect("~/Account/LogOut", true);
        }
    }
}
