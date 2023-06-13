using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository
{
    public class CustomLogger : ICustomLogger
    {
        //private readonly ILogger<CustomLogger> _logger;
        //public CustomLogger(ILogger<CustomLogger> logger)
        //{
        //    _logger = logger;
        //}
        //public void LogInfo(string method, string message, string? parameters = null)
        //{
        //    _logger.LogInformation("Method: " + method + " Parameters: " + parameters + " Message: " + message);
        //}
        //public void LogError(string method, string exceptionMessage, string? parameters = null)
        //{
        //    _logger.LogInformation("**************************" + method + "'s Exception ***********************************");
        //    _logger.LogError("Method: " + method + " Parameters: " + parameters + " Exception: " + exceptionMessage);
        //    _logger.LogInformation("**************************" + method + "'s Exception End ***********************************");
        //}
    }
}
