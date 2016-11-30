using System;
using Nancy;
using Nancy.Conventions;

namespace hubbl.web {
	
	public class ApplicationBootstrapper : DefaultNancyBootstrapper {
		
		protected override void ConfigureConventions(NancyConventions nancyConventions) {
			nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("assets", @"assets"));
			base.ConfigureConventions(nancyConventions);
		}

	}
}

