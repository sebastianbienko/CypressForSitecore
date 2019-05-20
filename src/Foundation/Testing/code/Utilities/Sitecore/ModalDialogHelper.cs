using System;
using System.Web;
using Deloitte.Foundation.Testing.UI.Constants;
using Deloitte.Foundation.Testing.UI.Settings;
using Deloitte.Foundation.Testing.UI.Sitecore;
using Sitecore;
using Sitecore.Data;
using Sitecore.Globalization;
using Sitecore.Text;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace Deloitte.Foundation.Testing.UI.Utilities.Sitecore
{
    public static class ModalDialogHelper
    {
        public static ClientCommand ShowTestingModalDialog(ID pageContextItemId, Language language, PageMode pageMode)
        {
            if (pageContextItemId.IsNull)
                throw new ArgumentNullException(nameof(pageContextItemId));

            if (language == null)
                throw new ArgumentNullException(nameof(language));

            if (pageMode == PageMode.None)
                throw new ArgumentException(nameof(pageMode));

            var urlString = new UrlString(UIUtil.GetUri(SitecoreCommandsConstants.TestPageControl));
            urlString.Append(SitecoreCommandsConstants.PageIdArgumentKey, pageContextItemId.ToString());
            urlString.Append(SitecoreCommandsConstants.PageLangArgumentKey, language.ToString());
            urlString.Append(SitecoreCommandsConstants.PageModeArgumentKey, pageMode.ToString());
            urlString.Append(SitecoreCommandsConstants.CypressReportUrlArgumentKey, HttpUtility.UrlEncode(CypressSettings.ReportUrl));

            var clientCommand = SheerResponse.ShowModalDialog(urlString.ToString(), "800px", "800px");
            clientCommand.Load += new EventHandler(ShowLoadingDialog);

            return clientCommand;
        }

        private static void ShowLoadingDialog(object sender, System.EventArgs e)
        {
            SheerResponse.ShowModalDialog("control:Progress");
        }
    }
}