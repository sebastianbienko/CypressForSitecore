using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using Deloitte.Foundation.Testing.UI.Models;
using Newtonsoft.Json;
using Sitecore.Mvc.Extensions;

namespace Deloitte.Foundation.Testing.UI.Services.Cypress
{
    public class CypressTestRunner : IRenderingTestingService
    {
        private readonly List<KeyValuePair<string, string>> _options;
        private readonly string _cypressTestRunnerUrl;

        public CypressTestRunner(string cypressTestRunnerUrl)
        {
            _options = new List<KeyValuePair<string, string>>();
            _cypressTestRunnerUrl =
                cypressTestRunnerUrl ?? throw new ArgumentNullException(nameof(cypressTestRunnerUrl));
        }

        public void RunAllTests()
        {
            throw new NotImplementedException();
        }
        
        public void RunTests(List<HelixRenderingDefinition> pageRenderings)
        {
            if (pageRenderings == null)
                throw new ArgumentNullException(nameof(pageRenderings));

            if (!pageRenderings.Any())
                throw new ArgumentException($"'{nameof(pageRenderings)}' should not be empty.");

            var serializedPageRenderings = JsonConvert.SerializeObject(pageRenderings);
            serializedPageRenderings = serializedPageRenderings.Replace(" ", "-").Replace("\"", "\\\"");
            var additionalOptions = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("--env", $"''pageRenderings={serializedPageRenderings}''")
            };

            var queryString = GetQueryString(additionalOptions);
            var requestUrl = _cypressTestRunnerUrl;

            if (!string.IsNullOrWhiteSpace(queryString))
            {
                requestUrl = requestUrl + queryString;
            }
            
            var request = (HttpWebRequest)WebRequest.Create(requestUrl);
            var response = (HttpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();
            var streamReader = new StreamReader(stream);
            var result = streamReader.ReadToEnd();
        }

        public void AddOption(XmlNode xmlNode)
        {
            _options.Add(new KeyValuePair<string, string>(xmlNode.Attributes["name"].Value, xmlNode.InnerText));
        }

        private string GetQueryString(List<KeyValuePair<string, string>> additionalOptions)
        {
            var queryString = "";
            var options = new List<KeyValuePair<string, string>>();

            options.AddRange(additionalOptions);
            options.AddRange(_options);

            options = options.GroupBy(e => e.Key)
                .Select((e, v) => new KeyValuePair<string, string>(e.Key, string.Join(",", e.Select(o => o.Value)))).ToList();

            var joinedOptions = string.Join("&", options.Select((entry, index) => $"{index}={entry.Key} {HttpUtility.UrlEncode(entry.Value)}"));

            if (!string.IsNullOrWhiteSpace(joinedOptions))
            {
                queryString = $"?{joinedOptions}";
            }

            return queryString;
        }
    }
}