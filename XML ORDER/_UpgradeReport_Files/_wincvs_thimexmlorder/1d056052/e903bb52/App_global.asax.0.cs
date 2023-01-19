﻿#pragma checksum "D:\WinCVS\ThimeXMLORDER\global.asax" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B23763F0CD79124C22DF4F35E15D83E549320757"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP {
    
    #line 285 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.Profile;
    
    #line default
    #line hidden
    
    #line 280 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Text.RegularExpressions;
    
    #line default
    #line hidden
    
    #line 282 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.Caching;
    
    #line default
    #line hidden
    
    #line 278 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Configuration;
    
    #line default
    #line hidden
    
    #line 284 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.Security;
    
    #line default
    #line hidden
    
    #line 289 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.UI.HtmlControls;
    
    #line default
    #line hidden
    
    #line 287 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.UI.WebControls;
    
    #line default
    #line hidden
    
    #line 276 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Collections;
    
    #line default
    #line hidden
    
    #line 275 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System;
    
    #line default
    #line hidden
    
    #line 286 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.UI;
    
    #line default
    #line hidden
    
    #line 281 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web;
    
    #line default
    #line hidden
    
    #line 283 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.SessionState;
    
    #line default
    #line hidden
    
    #line 277 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Collections.Specialized;
    
    #line default
    #line hidden
    
    #line 279 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Text;
    
    #line default
    #line hidden
    
    #line 288 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.UI.WebControls.WebParts;
    
    #line default
    #line hidden
    
    
    [System.Runtime.CompilerServices.CompilerGlobalScopeAttribute()]
    public class global_asax : global::System.Web.HttpApplication {
        
        private static bool @__initialized;
        
        
        #line 3 "D:\WinCVS\ThimeXMLORDER\global.asax"
                       

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        Orders.ConfigDetails.Application3ConStr = ConfigurationManager.ConnectionStrings["Application3XMLOrder"].ConnectionString;
        Orders.ConfigDetails.TemplatePath    = Server.MapPath("") + "\\Template\\Template.xml";
        Orders.ConfigDetails.EMCTemplatePath = Server.MapPath("") + "\\Template\\EMCTemplate.xml";
        Orders.ConfigDetails.FMSPath         = ConfigurationManager.AppSettings["FMSPATH"];
        Orders.ConfigDetails.RootPath        = ConfigurationManager.AppSettings["ROOTPATH"];
        Orders.ConfigDetails.EMCFMSPath      = ConfigurationManager.AppSettings["EMCFMSPATH"];
        Orders.ConfigDetails.IPIPFMSPath     = ConfigurationManager.AppSettings["IPIPFMSPATH"];
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
    	String roles = null;
    	FormsIdentity identity = null;

	    if (Context.Request.IsAuthenticated)
	    {
            if (Context.User.Identity.AuthenticationType == "Forms")
            {
                // get the comma delimited list of roles from the user data
                // in the authentication ticket
                identity = (FormsIdentity)(Context.User.Identity);
                roles = identity.Ticket.UserData;

                // create a new user principal object with the current user identity
                // and the roles assigned to the user
                Context.User = new System.Security.Principal.GenericPrincipal(identity, roles.Split(','));
            }
            else
            {
                // application is improperly configured so throw an exception
                throw new ApplicationException("Application Must Be Configured For Forms Authentication");
		    } // if (Context.User.Identity.AuthenticationType = "Forms")
	    } // if (Context.Request.IsAuthenticated)
    }

        #line default
        #line hidden
        
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global_asax() {
            if ((global::ASP.global_asax.@__initialized == false)) {
                global::ASP.global_asax.@__initialized = true;
            }
        }
        
        protected System.Web.Profile.DefaultProfile Profile {
            get {
                return ((System.Web.Profile.DefaultProfile)(this.Context.Profile));
            }
        }
    }
}