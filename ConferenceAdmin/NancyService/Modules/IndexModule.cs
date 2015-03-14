using Nancy;
using NancyService.Models;
using System.Collections.Generic;
using System.Linq;

namespace NancyService.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            // Serves Index.html as a static content.
            Get["/"] = parameters =>
            {
               return View["layout.html"];
            };

        }
    }
}