using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefasConsoleApp.Helpers
{
    public class LogHelper
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()!.DeclaringType);

        static LogHelper()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        public static void Info(string mensagem)
        {
            log.Info(mensagem);
        }

        public static void Debug(string mensagem)
        {
            log.Debug(mensagem);
        }

        public static void Warn(string mensagem)
        {
            log.Warn(mensagem);
        }

        public static void Error(string mensagem, Exception? ex = null)
        {
            log.Error(mensagem, ex);
        }
    }
}
