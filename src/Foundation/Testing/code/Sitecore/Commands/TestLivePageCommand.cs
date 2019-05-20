using System;
using System.Linq;
using Deloitte.Foundation.Testing.UI.Factories;
using Deloitte.Foundation.Testing.UI.Services.Cypress;
using Deloitte.Foundation.Testing.UI.Utilities.Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Shell.Applications.Dialogs.ProgressBoxes;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace Deloitte.Foundation.Testing.UI.Sitecore.Commands
{
    public class TestLivePageCommand : Command
    {
        private readonly TestingServicesFactory _testingServicesFactory;

        public TestLivePageCommand(TestingServicesFactory testingServicesFactory)
        {
            _testingServicesFactory = testingServicesFactory ?? throw new ArgumentNullException(nameof(testingServicesFactory));
        }

        public override void Execute(CommandContext context)
        {
            var database = Factory.GetDatabase("web");
            var contextItem = SitecoreCommandHelper.GetContextItem(context, database);

            ProgressBox.Execute("Running Tests", "Test Live", new ProgressBoxMethod(RunTests), new object[] { database, contextItem });

            ModalDialogHelper.ShowTestingModalDialog(contextItem.ID, contextItem.Language, PageMode.Live);
        }

        private void RunTests(params object[] parameters)
        {
            var database = (Database)parameters[0];
            var contextItem = (Item) parameters[1];

            if (contextItem == null)
                throw new ArgumentException($"[{this.Name}] Context Item could not be found for '{this.Name}'.");

            var renderingsLocator = new SitecoreHelixRenderingsLocator(database);
            var renderings = renderingsLocator.GetRenderingsHelixLocation(contextItem);

            _testingServicesFactory.LiveTestingService.RunTests(renderings.ToList());
        }
    }
}