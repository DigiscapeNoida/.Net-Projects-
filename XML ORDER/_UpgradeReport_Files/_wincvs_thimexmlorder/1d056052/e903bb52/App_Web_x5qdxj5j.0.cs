﻿#pragma checksum "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CEAAE480931F2F3A5BCAA64D60ED169A65E5E703"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



public partial class UCrDate {
    
    
    #line 5 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
    protected global::System.Web.UI.WebControls.DropDownList DDLDay;
    
    #line default
    #line hidden
    
    
    #line 6 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
    protected global::System.Web.UI.WebControls.DropDownList DDLMonth;
    
    #line default
    #line hidden
    
    
    #line 7 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
    protected global::System.Web.UI.WebControls.DropDownList DDLYear;
    
    #line default
    #line hidden
    
    protected System.Web.Profile.DefaultProfile Profile {
        get {
            return ((System.Web.Profile.DefaultProfile)(this.Context.Profile));
        }
    }
    
    protected ASP.global_asax ApplicationInstance {
        get {
            return ((ASP.global_asax)(this.Context.ApplicationInstance));
        }
    }
}
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
    
    #line 5 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
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
    
    #line 281 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web;
    
    #line default
    #line hidden
    
    #line 5 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
    using System.Web.UI;
    
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
    
    #line 5 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
    using System.Web.UI.WebControls.WebParts;
    
    #line default
    #line hidden
    
    #line 283 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.SessionState;
    
    #line default
    #line hidden
    
    
    [System.Runtime.CompilerServices.CompilerGlobalScopeAttribute()]
    public class usercontrol_ucrdate_ascx : global::UCrDate {
        
        private static bool @__initialized;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public usercontrol_ucrdate_ascx() {
            
            #line 912304 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx.cs"
            ((global::System.Web.UI.UserControl)(this)).AppRelativeVirtualPath = "~/UserControl/UCrDate.ascx";
            
            #line default
            #line hidden
            if ((global::ASP.usercontrol_ucrdate_ascx.@__initialized == false)) {
                global::ASP.usercontrol_ucrdate_ascx.@__initialized = true;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.DropDownList @__BuildControlDDLDay() {
            global::System.Web.UI.WebControls.DropDownList @__ctrl;
            
            #line 5 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl = new global::System.Web.UI.WebControls.DropDownList();
            
            #line default
            #line hidden
            this.DDLDay = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            
            #line 5 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl.ID = "DDLDay";
            
            #line default
            #line hidden
            
            #line 5 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl.Height = new System.Web.UI.WebControls.Unit(18D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            
            #line 5 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(36D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.DropDownList @__BuildControlDDLMonth() {
            global::System.Web.UI.WebControls.DropDownList @__ctrl;
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl = new global::System.Web.UI.WebControls.DropDownList();
            
            #line default
            #line hidden
            this.DDLMonth = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl.ID = "DDLMonth";
            
            #line default
            #line hidden
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl.Height = new System.Web.UI.WebControls.Unit(18D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(36D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.DropDownList @__BuildControlDDLYear() {
            global::System.Web.UI.WebControls.DropDownList @__ctrl;
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl = new global::System.Web.UI.WebControls.DropDownList();
            
            #line default
            #line hidden
            this.DDLYear = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl.ID = "DDLYear";
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl.Height = new System.Web.UI.WebControls.Unit(18D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(51D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlTree(usercontrol_ucrdate_ascx @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n    <table>\r\n        <tr style=\"vertical-align:middle\">\r\n        <td style=\"ver" +
                        "tical-align:middle\">\r\n            "));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.DropDownList @__ctrl1;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl1 = this.@__BuildControlDDLDay();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            "));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.DropDownList @__ctrl2;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl2 = this.@__BuildControlDDLMonth();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__parser.AddParsedSubObject(@__ctrl2);
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            "));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.DropDownList @__ctrl3;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__ctrl3 = this.@__BuildControlDDLYear();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__parser.AddParsedSubObject(@__ctrl3);
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n        </td>\r\n    </tr>\r\n    </table>\r\n"));
            
            #line default
            #line hidden
        }
        
        
        #line 912304 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx.cs"
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void FrameworkInitialize() {
            base.FrameworkInitialize();
            this.@__BuildControlTree(this);
        }
        
        #line default
        #line hidden
    }
}
