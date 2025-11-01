using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Core.Exceptions;

namespace Utils.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath = "weatherHub_app.log";

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                await _next(context); 
            }
            catch (ValidationException vex)
            {
                sw.Stop();
                var warningLog = $"WARNING {DateTime.Now}: {context.Request.Method} {context.Request.Path} - {vex.Message}";
                Console.WriteLine(warningLog);
                await File.AppendAllTextAsync(_logFilePath, warningLog + "\n");
                throw; 
            }
            catch (Exception ex)
            {
                sw.Stop();
                var errorLog = $"ERROR {DateTime.Now}: {context.Request.Method} {context.Request.Path} - {ex.Message}";
                Console.WriteLine(errorLog);
                await File.AppendAllTextAsync(_logFilePath, errorLog + "\n");
                throw; 
            }
            finally
            {
                sw.Stop();
                var infoLog = $"INFO {DateTime.Now}: {context.Request.Method} {context.Request.Path} responded {context.Response.StatusCode} in {sw.ElapsedMilliseconds}ms";
                Console.WriteLine(infoLog);
                await File.AppendAllTextAsync(_logFilePath, infoLog + "\n");
            }
        }
    }
}
