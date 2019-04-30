using AutoPuTTY.Properties;
using AutoPuTTY.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Forms.Options
{
    abstract class CommonOptionsViewModel
    {
        private readonly XmlHelper xmlHelper;

        public CommonOptionsViewModel(string defaultSettingsConfigKey, string configKey, string changePathTitle, XmlHelper xmlHelper)
        {
            this.xmlHelper = xmlHelper;
            DefaultSettingsConfigKey = defaultSettingsConfigKey;
            ConfigKey = configKey;

            AppPath = xmlHelper.configGet(configKey);
            if (string.IsNullOrEmpty(AppPath))
            {
                AppPath = Settings.Default[defaultSettingsConfigKey].ToString();
            }
        }

        protected string DefaultSettingsConfigKey { get; set; }

        protected string ConfigKey { get; set; }

        protected string ChangePathTitle { get; set; }

        public string AppPath { get; set; }

        public bool ChangeAppPath()
        {
            var path = new FileSelector().SelectFile(
                ChangePathTitle,
                Resources.formOptions_exeFilter);

            if (!string.IsNullOrEmpty(path))
            {
                AppPath = path;
                Settings.Default[DefaultSettingsConfigKey] = path;
                xmlHelper.configSet(ConfigKey, path);

                return true;
            }

            return false;
        }
    }
}
