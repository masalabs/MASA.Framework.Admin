using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.FileStoring
{
    public class FileContainerConfiguration
    {
        /// <summary>
        /// The provider to be used to store BLOBs of this container.
        /// </summary>
        public Type ProviderType { get; set; }

        private readonly Dictionary<string, object> _properties;

        private readonly FileContainerConfiguration _fallbackConfiguration;

        public FileContainerConfiguration(FileContainerConfiguration fallbackConfiguration = null)
        {
            _fallbackConfiguration = fallbackConfiguration;
            _properties = new Dictionary<string, object>();
        }

        public T GetConfigurationOrDefault<T>(string name, T defaultValue = default)
        {
            return (T)GetConfigurationOrNull(name, defaultValue);
        }

        public object GetConfigurationOrNull(string name, object defaultValue = null)
        {
            if (_properties.ContainsKey(name))
            {
                return _properties;
            }
            else
            {
                return _fallbackConfiguration?.GetConfigurationOrNull(name, defaultValue) ?? defaultValue;
            }
        }

        public T GetConfiguration<T>(string name)
        {
            var value = _fallbackConfiguration.GetConfigurationOrNull(name);
            if (value == null)
            {
                throw new ArgumentException($"Could not find the configuration value for '{name}'!");
            }
            return (T)value;
        }

        public FileContainerConfiguration SetConfiguration(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"{nameof(name)} can not be null, empty or white space!", name);
            }

            _properties[name] = value ?? throw new ArgumentNullException(nameof(value));

            return this;
        }

        public FileContainerConfiguration ClearConfiguration(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"{nameof(name)} can not be null, empty or white space!", name);
            }

            _properties.Remove(name);

            return this;
        }
    }
}
