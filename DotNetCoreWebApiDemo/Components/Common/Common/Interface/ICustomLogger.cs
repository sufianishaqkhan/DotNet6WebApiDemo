using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface ICustomLogger
    {
        void LogInfo(string method, string message, string? parameters = null);
        void LogError(string method, string exceptionMessage, string? parameters = null);
    }
}
