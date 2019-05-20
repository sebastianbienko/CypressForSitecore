using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Deloitte.Foundation.Testing.UI.Factories;
using Deloitte.Foundation.Testing.UI.Services.Cypress;
using Deloitte.Foundation.Testing.UI.Utilities;
using Deloitte.Foundation.Testing.UI.Utilities.Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Shell.Applications.Dialogs.ProgressBoxes;
using Sitecore.Shell.Framework.Commands;

namespace Deloitte.Foundation.Testing.UI.Sitecore.Commands
{
    public class TestPreviewPageCommand : Command
    {
        private readonly TestingServicesFactory _testingServicesFactory;

        public TestPreviewPageCommand(TestingServicesFactory testingServicesFactory)
        {
            _testingServicesFactory = testingServicesFactory ?? throw new ArgumentNullException(nameof(testingServicesFactory));
        }

        public override void Execute(CommandContext context)
        {
            var database = Factory.GetDatabase("master");
            var contextItem = SitecoreCommandHelper.GetContextItem(context, database);

            ProgressBox.Execute("Running Tests", "Test Preview", new ProgressBoxMethod(RunTests), new object[] { database, contextItem });
            
            ModalDialogHelper.ShowTestingModalDialog(contextItem.ID, contextItem.Language, PageMode.Live);
        }

        private void RunTests(params object[] parameters)
        {
            var database = (Database)parameters[0];
            var contextItem = (Item)parameters[1];

            if (contextItem == null)
                throw new ArgumentException($"[{this.Name}] Context Item could not be found for '{this.Name}'.");

            var renderingsLocator = new SitecoreHelixRenderingsLocator(database);
            var renderings = renderingsLocator.GetRenderingsHelixLocation(contextItem);

            _testingServicesFactory.PreviewTestingService.RunTests(renderings.ToList());
        }
    }
}