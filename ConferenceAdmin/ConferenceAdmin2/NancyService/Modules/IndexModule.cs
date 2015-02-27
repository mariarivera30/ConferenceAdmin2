using Nancy;

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