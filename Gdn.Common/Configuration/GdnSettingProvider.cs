using System.Collections.Generic;
using System.Configuration;
using Abp.Configuration;

namespace Gdn.Configuration
{
    /// <summary>
    /// Register this class into ABP setting providers to access AppSettings(in config file) from ISettingManager.
    /// </summary>
    public class GdnSettingProvider : SettingProvider
    {
        /// <inheritdoc />
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            var settings = ConfigurationManager.AppSettings;
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                yield return new SettingDefinition(key, settings[key] ?? string.Empty);
            }
        }
    }
}