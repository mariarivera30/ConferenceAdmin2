using Nancy;
using Nancy.Conventions;
using Nancy.Authentication.Token;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;

namespace NancyService
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            // Satic file service enabled in these directories.
            conventions.StaticContentsConventions.Add( StaticContentConventionBuilder.AddDirectory("App", @"App"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Scripts", @"Scripts"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Views", @"Views"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("css", @"css"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("fonts", @"fonts"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("img", @"img"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("js", @"js"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("sponsorlogos", @"sponsorlogos"));

        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            // Enable the token pipeline for Auth in Nancy.
            TokenAuthentication.Enable(pipelines, new TokenAuthenticationConfiguration(container.Resolve<ITokenizer>()));
           
        }

        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            Nancy.Json.JsonSettings.MaxJsonLength = int.MaxValue;
        }

       
    }
}