using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Deloitte.Foundation.Testing.UI.Services;
using Sitecore.Configuration;

namespace Deloitte.Foundation.Testing.UI.Factories
{
    public class TestingServicesFactory
    {
        private static readonly Lazy<IRenderingTestingService> _previewTestingService = new Lazy<IRenderingTestingService>(GetPreviewTestingService);
        private static readonly Lazy<IRenderingTestingService> _liveTestingService = new Lazy<IRenderingTestingService>(GetLiveTestingService);

        public IRenderingTestingService PreviewTestingService => _previewTestingService.Value;
        public IRenderingTestingService LiveTestingService => _liveTestingService.Value;

        private static IRenderingTestingService GetLiveTestingService()
        {
            return Factory.CreateObject("cypress/testRunners/liveTestRunner", true) as IRenderingTestingService;
        }

        private static IRenderingTestingService GetPreviewTestingService()
        {
            return Factory.CreateObject("cypress/testRunners/previewTestRunner", true) as IRenderingTestingService;
        }
    }
}