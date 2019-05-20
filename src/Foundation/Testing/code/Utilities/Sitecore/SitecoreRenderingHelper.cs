using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using Deloitte.Foundation.Testing.UI.Models;
using Deloitte.Foundation.Testing.UI.Settings;
using Deloitte.Foundation.Testing.UI.Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Visualization;

namespace Deloitte.Foundation.Testing.UI.Utilities.Sitecore
{
    public class SitecoreHelixRenderingsLocator
    {
        private readonly Database _database;
        private readonly HelixSettings _helixSettings;

        public SitecoreHelixRenderingsLocator(Database database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _helixSettings = new HelixSettings(_database);
        }

        public IEnumerable<HelixRenderingDefinition> GetRenderingsHelixLocation(Item pageItem)
        {
            var renderingsHelixLocation = new List<HelixRenderingDefinition>();

            var renderings = pageItem.Visualization.GetRenderings(
                DeviceItem.ResolveDevice(_database), false);

            foreach (var renderingReference in renderings)
            {
                var renderingItem = _database.GetItem(renderingReference?.RenderingItem?.ID);

                if (renderingItem != null && ItemIsInHelixLayerRenderingFolder(renderingItem, out var helixLayer))
                {
                    renderingsHelixLocation.Add(new HelixRenderingDefinition
                    {
                        HelixLayer = helixLayer,
                        RenderingItemName = renderingItem.Name,
                        HelixModule = GetHelixModuleRenderingFolder(renderingItem)?.Name,
                        Datasource = renderingReference?.Settings?.DataSource
                    });
                }
            }

            return renderingsHelixLocation;
        }

        public bool ItemIsInHelixLayerRenderingFolder(Item item, out HelixLayer helixLayer)
        {
            helixLayer = HelixLayer.None;

            if (item != null && item.Axes.IsDescendantOf(_helixSettings.RenderingsFoundationFolderItem))
            {
                helixLayer = HelixLayer.Foundation;
            }else if (item != null && item.Axes.IsDescendantOf(_helixSettings.RenderingsFeatureFolderItem))
            {
                helixLayer = HelixLayer.Feature;
            }else if (item != null && item.Axes.IsDescendantOf(_helixSettings.RenderingsProjectFolderItem))
            {
                helixLayer = HelixLayer.Project;
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool ItemIsInHelixLayerRenderingFolder(Item item)
        {
            return ItemIsInHelixLayerRenderingFolder(item, out _);
        }

        public Item GetHelixModuleRenderingFolder(Item item)
        {
            if (item?.Parent?.Parent == null || !ItemIsInHelixLayerRenderingFolder(item) || ItemIsHelixLayerRenderingFolder(item))
                return null;

            return ItemIsHelixLayerRenderingFolder(item.Parent?.Parent) ? item.Parent : GetHelixModuleRenderingFolder(item.Parent);
        }

        public bool ItemIsHelixLayerRenderingFolder(Item item)
        {
            return item != null && 
                   (item.ID.Equals(_helixSettings.RenderingsFoundationFolderId) ||
                   item.ID.Equals(_helixSettings.RenderingsFeatureFolderId) ||
                   item.ID.Equals(_helixSettings.RenderingsProjectFolderId));
        }
    }
}