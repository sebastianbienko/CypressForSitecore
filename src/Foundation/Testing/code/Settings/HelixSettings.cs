using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Deloitte.Foundation.Testing.UI.Settings
{
    public class HelixSettings
    {
        private readonly Database _database;

        public HelixSettings(Database database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public readonly string RenderingsFoundationFolderSettingId = global::Sitecore.Configuration.Settings.GetSetting(
                "Deloitte.Foundation.Testing.UI.Rendering.Foundation.Folder.ID");

        public readonly string RenderingsFeatureFolderSettingId = global::Sitecore.Configuration.Settings.GetSetting(
                "Deloitte.Foundation.Testing.UI.Rendering.Feature.Folder.ID");

        public readonly string RenderingsProjectFolderSettingId = global::Sitecore.Configuration.Settings.GetSetting(
                "Deloitte.Foundation.Testing.UI.Rendering.Project.Folder.ID");

        public readonly string RenderingsFoundationFolderPath = "/sitecore/layout/Renderings/Foundation";

        public readonly string RenderingsFeatureFolderPath = "/sitecore/layout/Renderings/Feature";

        public readonly string RenderingsProjectFolderPath = "/sitecore/layout/Renderings/Project";

        public Item RenderingsFoundationFolderItem => !string.IsNullOrWhiteSpace(RenderingsFoundationFolderSettingId)
            ? _database.GetItem(RenderingsFoundationFolderSettingId)
            : _database.GetItem(RenderingsFoundationFolderPath);

        public Item RenderingsFeatureFolderItem => !string.IsNullOrWhiteSpace(RenderingsFeatureFolderSettingId)
            ? _database.GetItem(RenderingsFeatureFolderSettingId)
            : _database.GetItem(RenderingsFeatureFolderPath);

        public Item RenderingsProjectFolderItem => !string.IsNullOrWhiteSpace(RenderingsProjectFolderSettingId)
            ? _database.GetItem(RenderingsProjectFolderSettingId)
            : _database.GetItem(RenderingsProjectFolderPath);

        public ID RenderingsFoundationFolderId => !string.IsNullOrWhiteSpace(RenderingsFoundationFolderSettingId)
            ? ID.Parse(RenderingsFoundationFolderSettingId)
            : RenderingsFoundationFolderItem?.ID;

        public ID RenderingsFeatureFolderId => !string.IsNullOrWhiteSpace(RenderingsFeatureFolderSettingId)
            ? ID.Parse(RenderingsFeatureFolderSettingId)
            : RenderingsFeatureFolderItem?.ID;

        public ID RenderingsProjectFolderId => !string.IsNullOrWhiteSpace(RenderingsProjectFolderSettingId)
            ? ID.Parse(RenderingsProjectFolderSettingId)
            : RenderingsProjectFolderItem?.ID;
    }
}