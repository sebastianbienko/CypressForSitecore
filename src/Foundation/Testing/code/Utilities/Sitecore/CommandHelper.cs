using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Shell.Framework.Commands;

namespace Deloitte.Foundation.Testing.UI.Utilities.Sitecore
{
    public static class SitecoreCommandHelper
    {
        public static Item GetContextItem(CommandContext commandContext, Database database)
        {
            var contextItem = commandContext?.Items?[0];

            return string.Equals(contextItem?.Database?.Name, database?.Name)
                ? contextItem
                : database?.GetItem(contextItem?.ID);
        }
    }
}