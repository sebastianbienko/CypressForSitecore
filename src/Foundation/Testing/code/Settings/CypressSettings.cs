using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deloitte.Foundation.Testing.UI.Settings
{
    public static class CypressSettings
    {
        public static readonly string ReportUrl =
            global::Sitecore.Configuration.Settings.GetSetting("Deloitte.Foundation.Testing.UI.Cypress.Report.Url");
    }
}