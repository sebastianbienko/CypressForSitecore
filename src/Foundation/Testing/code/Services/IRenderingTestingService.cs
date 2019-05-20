using Deloitte.Foundation.Testing.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deloitte.Foundation.Testing.UI.Services
{
    public interface IRenderingTestingService
    {
        void RunAllTests();
        void RunTests(List<HelixRenderingDefinition> pageRenderings);
    }
}