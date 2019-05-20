using Deloitte.Foundation.Testing.UI.Utilities.Sitecore;
using Sitecore.Configuration;
using Sitecore.Shell.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deloitte.Foundation.Testing.UI.Sitecore.Commands
{
    public class ShowPreviewResultsCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            var database = Factory.GetDatabase("master");
            var contextItem = SitecoreCommandHelper.GetContextItem(context, database);

            ModalDialogHelper.ShowTestingModalDialog(contextItem.ID, contextItem.Language, PageMode.Preview);
        }
    }
}