﻿using System.Web.Mvc;

namespace szalkszop.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
				new { controller = "Admin", action = "Index", id = "" },
				namespaces: new[] { "szalkszop.Areas.Admin.Controllers" }
			);
		}
    }
}
