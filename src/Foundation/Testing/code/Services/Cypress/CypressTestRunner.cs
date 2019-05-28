using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using Deloitte.Foundation.Testing.UI.Models;
using Deloitte.Foundation.Testing.UI.Utilities.Translation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sitecore.Mvc.Extensions;

namespace Deloitte.Foundation.Testing.UI.Services.Cypress
{
    public class CypressTestRunner : IRenderingTestingService
    {
        private readonly List<string> _options;
        private readonly string _cypressTestRunnerUrl;
        private readonly JObject _cypressConfiguration;

        public CypressTestRunner(string cypressTestRunnerUrl)
        {
            _options = new List<string>();
            _cypressConfiguration = new JObject();
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


            var encodedData = Encoding.UTF8.GetBytes(GetCypressConfiguration(pageRenderings));
            var request = (HttpWebRequest)WebRequest.Create(_cypressTestRunnerUrl);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/json";

            var requestStream = request.GetRequestStream();
            requestStream.Write(encodedData, 0, encodedData.Length);

            var response = (HttpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();
            var streamReader = new StreamReader(stream);
            var result = streamReader.ReadToEnd();
            requestStream.Dispose();
        }

        public void AddCypressSettings(XmlNode xmlNode)
        {
            var json = CypressXmlToJsonConverter.Convert(xmlNode);

            _cypressConfiguration.Merge(json);
        }

        private string GetCypressConfiguration(List<HelixRenderingDefinition> renderings)
        {
            var json = JToken.FromObject(new
            {
                env = new
                {
                    pageRenderings = renderings
                }
            });

            _cypressConfiguration.Merge(json);

            return _cypressConfiguration.ToString();
        }
    }
}