﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CompanyName.MyMeetings.API.Configuration.ExecutionContext
{
    internal class CorrelationMiddleware
    {
        internal const string CorrelationHeaderKey = "CorrelationId";
        private readonly RequestDelegate _next;

        public CorrelationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var correlationId = Guid.NewGuid();

            context.Request?.Headers.Add(CorrelationHeaderKey, correlationId.ToString());

            await _next.Invoke(context);
        }
    }
}