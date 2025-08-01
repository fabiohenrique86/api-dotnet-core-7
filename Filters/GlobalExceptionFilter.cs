﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace WebApplication2.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Unhandled exception");

            context.Result = new ObjectResult(new
            {
                Error = "An unexpected error occurred.",
                Details = context.Exception.ToString()
            })
            {
                StatusCode = HttpStatusCode.InternalServerError.GetHashCode()
            };

            context.ExceptionHandled = true;
        }
    }
}
