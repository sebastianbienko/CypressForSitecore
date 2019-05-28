using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Deloitte.Foundation.Testing.UI.Utilities.Translation
{
    public class CypressXmlToJsonConverter
    {
        public static JToken Convert(XmlNode xmlNode)
        {
            dynamic result = new ExpandoObject();

            ConvertXmlNode(xmlNode, ref result);

            return JToken.FromObject(result);
        }

        private static void ConvertXmlNode(XmlNode xmlNode, ref dynamic result)
        {
            if (xmlNode.HasChildNodes && !xmlNode.FirstChild.NodeType.Equals(XmlNodeType.Text))
            {
                if (string.Equals(xmlNode.Attributes["IsArray"]?.Value, "true",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    var childResults = new List<dynamic>();

                    foreach (XmlNode xmlChildNode in xmlNode.ChildNodes)
                    {
                        dynamic childResult = new ExpandoObject();
                        ConvertXmlNode(xmlChildNode, ref childResult);
                        childResults.Add(childResult);
                    }

                    ((IDictionary<string, object>)result)[xmlNode.Name] = childResults;
                }
                else
                {
                    dynamic childResult = new ExpandoObject();

                    foreach (XmlNode xmlChildNode in xmlNode.ChildNodes)
                    {
                        ConvertXmlNode(xmlChildNode, ref childResult);
                    }

                    ((IDictionary<string, object>)result)[xmlNode.Name] = childResult;
                }
            }
            else
            {
                if (xmlNode.Name.Equals("Entry", StringComparison.InvariantCultureIgnoreCase))
                {
                    result = xmlNode.InnerText;
                }
                else
                {
                    ((IDictionary<string, object>)result)[xmlNode.Name] = GetXmlNodeInner(xmlNode);
                }
            }
        }

        private static object GetXmlNodeInner(XmlNode xmlNode)
        {
            if (string.Equals(xmlNode.Attributes["IsBoolean"]?.Value, "true",
                StringComparison.InvariantCultureIgnoreCase))
            {
                return bool.Parse(xmlNode.InnerText);
            }
            else if (string.Equals(xmlNode.Attributes["IsInteger"]?.Value, "true",
                StringComparison.InvariantCultureIgnoreCase))
            {
                return int.Parse(xmlNode.InnerText);
            }
            else if (string.Equals(xmlNode.Attributes["IsFloat"]?.Value, "true",
                StringComparison.InvariantCultureIgnoreCase))
            {
                return float.Parse(xmlNode.InnerText);
            }
            else
            {
                return xmlNode.InnerText;
            }
        }
    }
}