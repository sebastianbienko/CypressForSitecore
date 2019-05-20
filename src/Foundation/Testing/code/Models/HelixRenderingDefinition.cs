using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Deloitte.Foundation.Testing.UI.Sitecore;
using Sitecore.Data;

namespace Deloitte.Foundation.Testing.UI.Models
{
    public class HelixRenderingDefinition
    {
        public string RenderingItemName { get; set; }
        public HelixLayer HelixLayer { get; set; }
        public string HelixModule { get; set; }
        public string Datasource { get; set; }
    }
}