using log4net;
using log4net.Core;
using SampleDI.Configuration;
using SampleDI.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Logging
{
    public class Log4NetLoggingService : ILoggingService
    {
        private readonly IConfigurationRepository _configurationRepository;

        private readonly IContextService _contextService;

        private string _log4netConfigFileName;

        public Log4NetLoggingService(IConfigurationRepository configurationRepository, IContextService contextService)
        {
            if (configurationRepository == null)
            {
                throw new ArgumentNullException("ConfigurationRepository");
            }

            if (contextService == null)
            {
                throw new ArgumentNullException("ContextService");
            }

            this._configurationRepository = configurationRepository;
            this._contextService = contextService;
            this._log4netConfigFileName = this._configurationRepository.GetConfigurationValue<string>("Log4NetSettingsFile");

            if (string.IsNullOrEmpty(_log4netConfigFileName))
            {
                throw new ApplicationException("Log4Net settings file missing from the configuration!");
            }

            SetupLogger();
        }

        private void SetupLogger()
        {
            var fileInfo = new FileInfo(this._contextService.GetContextulFullFilePath(this._log4netConfigFileName));
            if (!fileInfo.Exists)
            {
                throw new ApplicationException(string.Concat("Log4Net settings file ", this._log4netConfigFileName, " not found"));
            }

            log4net.Config.XmlConfigurator.ConfigureAndWatch(fileInfo);
        }

        private string GetUserName()
        {
            return this._contextService.GetUserName();
        }

        private void AddProperties(object logSource, Exception exception, LoggingEvent loggingEvent)
        {
            loggingEvent.Properties["UserName"] = this.GetUserName();
            try
            {
                var contextProperties = this._contextService.GetContextProperties();
                if (contextProperties != null)
                {
                    try
                    {
                        loggingEvent.Properties["UserAgent"] = contextProperties.UserAgent;
                        loggingEvent.Properties["RemoteHost"] = contextProperties.RemoteHost;
                        loggingEvent.Properties["Path"] = contextProperties.Path;
                        loggingEvent.Properties["Query"] = contextProperties.Query;
                        loggingEvent.Properties["RefererUrl"] = contextProperties.Referrer;
                        loggingEvent.Properties["RequestId"] = contextProperties.RequestId;
                        loggingEvent.Properties["SessionId"] = contextProperties.SessionId;
                    }
                    catch (Exception)
                    {
                    }
                }

                loggingEvent.Properties["ExceptionType"] = exception == null ? string.Empty : exception.GetType().ToString();
                loggingEvent.Properties["ExceptionMessage"] = exception == null ? string.Empty : exception.Message;
                loggingEvent.Properties["ExceptionStackTrace"] = exception == null ? string.Empty : exception.StackTrace;
                loggingEvent.Properties["LogSource"] = logSource.GetType().ToString();
            }
            catch (Exception ex)
            {
                var type = typeof(Log4NetLoggingService);
                var logger = LogManager.GetLogger(type);
                logger.Logger.Log(type, Level.Fatal, "Exception when extracting properties: " + ex.Message, ex);
            }
        }

        private void LogMessageWithProperties(object logSource, string message, Level level, Exception exception)
        {
            var logger = LogManager.GetLogger(logSource.GetType());

            var loggingEvent = new LoggingEvent(logSource.GetType(), logger.Logger.Repository, logger.Logger.Name, level, message, null);
            this.AddProperties(logSource, exception, loggingEvent);
            try
            {
                logger.Logger.Log(loggingEvent);
            }
            catch (AggregateException ae)
            {
                ae.Handle(x => { return true; });
            }
            catch (Exception) { }
        }

        public void LogInfo(object logSource, string message, Exception exception = null)
        {
            this.LogMessageWithProperties(logSource, message, Level.Info, exception);
        }

        public void LogWarning(object logSource, string message, Exception exception = null)
        {
            this.LogMessageWithProperties(logSource, message, Level.Warn, exception);
        }

        public void LogError(object logSource, string message, Exception exception = null)
        {
            this.LogMessageWithProperties(logSource, message, Level.Error, exception);
        }

        public void LogFatal(object logSource, string message, Exception exception = null)
        {
            this.LogMessageWithProperties(logSource, message, Level.Fatal, exception);
        }
    }
}
