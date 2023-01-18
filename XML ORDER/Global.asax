<%@ Application Language="C#" %>

<script runat="server">

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
</script>


