using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.AgenticRAG.DTOs;

namespace Telerik.Sitefinity.Frontend.AgenticRAG
{
    public class Helpers
    {
        private const string AdminApiBaseUrlPropertyName = "adminApiBaseUrl";
        private const string CdnHostNamePropertyName = "cdnHostName";

        public static string GetCdnUrl(string cdnFile)
        {
            if (string.IsNullOrEmpty(cdnFile))
                throw new ArgumentException("Invalid value for cdnFile", nameof(cdnFile));

            string cdnUrlFormatString = BuildCdnUrlFormatString();
            return string.Format(cdnUrlFormatString, cdnFile);
        }

        internal static string BuildCdnUrlFormatString()
        {
            var manager = ConfigManager.GetManager();
            var section = manager.GetSection("AgenticRAGConfig");
            var assistantProp = section["assistant"] as ConfigElement;

            string version = null;
            try
            {
                var versionInfo = RetrieveVersionInfo((string)assistantProp[AdminApiBaseUrlPropertyName]);
                version = versionInfo?.ProductVersion;
            }
            catch (Exception ex)
            {
                string logMessage = $"Error retrieving assistant version info. Please check the assistant configuration details: {ex.Message}";
                Log.Write(logMessage, ConfigurationPolicy.Trace);
            }

            string cdnHostName = (string)assistantProp[CdnHostNamePropertyName];
            if (string.IsNullOrEmpty(cdnHostName))
                throw new ArgumentException("CdnHostName is not configured in AgenticRAGConfig -> Assistant.");

            string versionSuffix = string.IsNullOrEmpty(version) ? string.Empty : $"?ver={version}";
            string placeholder = "{0}";

            return $"https://{cdnHostName}/{placeholder}{versionSuffix}";
        }

        private static VersionInfoDto RetrieveVersionInfo(string adminAPIBaseUrl)
        {
            var pathUrl = "/Version";
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri(new Uri(adminAPIBaseUrl),
                pathUrl)
            );

            var httpResponseMessage = new HttpClient().SendAsync(request).GetAwaiter().GetResult();
            httpResponseMessage.EnsureSuccessStatusCode();

            var result = httpResponseMessage.Content.ReadAsAsync<VersionInfoDto>().GetAwaiter().GetResult();

            return result;
        }
    }
}
