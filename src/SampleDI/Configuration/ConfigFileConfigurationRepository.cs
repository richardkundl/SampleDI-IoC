using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Configuration
{
    public class ConfigFileConfigurationRepository : IConfigurationRepository
    {
        private static T ConvertTo<T>(string value)
        {
            if (typeof(Enum).IsAssignableFrom(typeof(T)))
            {
                return (T)Enum.Parse(typeof(T), value);
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public T GetConfigurationValue<T>(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (value == null)
            {
                throw new KeyNotFoundException("Key '" + key + "' not found.");
            }

            try
            {
                return ConvertTo<T>(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetConfigurationValue<T>(string key, T defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (value == null)
            {
                return defaultValue;
            }

            try
            {
                return ConvertTo<T>(value);
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }
    }
}
