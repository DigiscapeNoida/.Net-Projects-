﻿#pragma checksum "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4708EFB8C68E4F6939EF575AF01F86B0C2EA5FD1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



public partial class ViewLog : System.Web.SessionState.IRequiresSessionState {
    
    
    #line 18 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
    protected global::System.Web.UI.WebControls.LoginName LoginName1;
    
    #line default
    #line hidden
    
    
    #line 19 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
    protected global::System.Web.UI.WebControls.Label Label2;
    
    #line default
    #line hidden
    
    
    #line 20 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
    protected global::System.Web.UI.WebControls.LoginName LoginName2;
    
    #line default
    #line hidden
    
    
    #line 22 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
    protected global::System.Web.UI.WebControls.LinkButton lblLogout;
    
    #line default
    #line hidden
    
    
    #line 21 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
    protected global::System.Web.UI.HtmlControls.HtmlForm Form2;
    
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
    
    #line 18 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
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
    
    #line 18 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
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
    
    #line 18 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
    using System.Web.UI.WebControls.WebParts;
    
    #line default
    #line hidden
    
    #line 283 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.SessionState;
    
    #line default
    #line hidden
    
    
    [System.Runtime.CompilerServices.CompilerGlobalScopeAttribute()]
    public class viewlog_aspx : global::ViewLog, System.Web.IHttpHandler {
        
        private static bool @__initialized;
        
        private static object @__stringResource;
        
        private static object @__fileDependencies;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public viewlog_aspx() {
            string[] dependencies;
            
            #line 912304 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx.cs"
            ((global::System.Web.UI.Page)(this)).AppRelativeVirtualPath = "~/ViewLog.aspx";
            
            #line default
            #line hidden
            if ((global::ASP.viewlog_aspx.@__initialized == false)) {
                global::ASP.viewlog_aspx.@__stringResource = this.ReadStringResource();
                dependencies = new string[2];
                dependencies[0] = "~/ViewLog.aspx";
                dependencies[1] = "~/ViewLog.aspx.cs";
                global::ASP.viewlog_aspx.@__fileDependencies = this.GetWrappedFileDependencies(dependencies);
                global::ASP.viewlog_aspx.@__initialized = true;
            }
            this.Server.ScriptTimeout = 30000000;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlTitle @__BuildControl__control3() {
            global::System.Web.UI.HtmlControls.HtmlTitle @__ctrl;
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTitle();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Xml Order Creation and Integration"));
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlHead @__BuildControl__control2() {
            global::System.Web.UI.HtmlControls.HtmlHead @__ctrl;
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlHead("head");
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlTitle @__ctrl1;
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl1 = this.@__BuildControl__control3();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.LoginName @__BuildControlLoginName1() {
            global::System.Web.UI.WebControls.LoginName @__ctrl;
            
            #line 18 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.LoginName();
            
            #line default
            #line hidden
            this.LoginName1 = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 18 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.ID = "LoginName1";
            
            #line default
            #line hidden
            
            #line 18 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Font.Size = global::System.Web.UI.WebControls.FontUnit.Medium;
            
            #line default
            #line hidden
            
            #line 18 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("style", "z-index: 100; left: 581px;  top: 60px");
            
            #line default
            #line hidden
            
            #line 18 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(108D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.Label @__BuildControlLabel2() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            
            #line 19 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            
            #line default
            #line hidden
            this.Label2 = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 19 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.ID = "Label2";
            
            #line default
            #line hidden
            
            #line 19 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Text = "Welcome ";
            
            #line default
            #line hidden
            
            #line 19 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Font.Size = global::System.Web.UI.WebControls.FontUnit.Medium;
            
            #line default
            #line hidden
            
            #line 19 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Font.Bold = true;
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.LoginName @__BuildControlLoginName2() {
            global::System.Web.UI.WebControls.LoginName @__ctrl;
            
            #line 20 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.LoginName();
            
            #line default
            #line hidden
            this.LoginName2 = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 20 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.ID = "LoginName2";
            
            #line default
            #line hidden
            
            #line 20 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Font.Size = global::System.Web.UI.WebControls.FontUnit.Medium;
            
            #line default
            #line hidden
            
            #line 20 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(108D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            
            #line 20 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Font.Bold = true;
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControllblLogout() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            
            #line default
            #line hidden
            this.lblLogout = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.ID = "lblLogout";
            
            #line default
            #line hidden
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Font.Bold = false;
            
            #line default
            #line hidden
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Font.Names = new string[] {
                    "verdana"};
            
            #line default
            #line hidden
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Font.Size = global::System.Web.UI.WebControls.FontUnit.Small;
            
            #line default
            #line hidden
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Height = new System.Web.UI.WebControls.Unit(22D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(55D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Logout"));
            
            #line default
            #line hidden
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.Click += new System.EventHandler(this.lblLogout_Click);
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlForm @__BuildControlForm2() {
            global::System.Web.UI.HtmlControls.HtmlForm @__ctrl;
            
            #line 21 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlForm();
            
            #line default
            #line hidden
            this.Form2 = @__ctrl;
            
            #line 21 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl.ID = "Form2";
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 21 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                "));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.LinkButton @__ctrl1;
            
            #line 21 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl1 = this.@__BuildControllblLogout();
            
            #line default
            #line hidden
            
            #line 21 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            
            #line 21 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("&nbsp;\r\n            "));
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlTree(viewlog_aspx @__ctrl) {
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            this.InitializeCulture();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\r\n<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3" +
                        ".org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n\r\n<html xmlns=\"http://www.w3.org/1" +
                        "999/xhtml\" >\r\n"));
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlHead @__ctrl1;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl1 = this.@__BuildControl__control2();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(this.CreateResourceBasedLiteralControl(15448, 709, true));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.LoginName @__ctrl2;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl2 = this.@__BuildControlLoginName1();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(@__ctrl2);
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("-->\r\n                "));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.Label @__ctrl3;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl3 = this.@__BuildControlLabel2();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(@__ctrl3);
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\t\t    "));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.LoginName @__ctrl4;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl4 = this.@__BuildControlLoginName2();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(@__ctrl4);
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            "));
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlForm @__ctrl5;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__ctrl5 = this.@__BuildControlForm2();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(@__ctrl5);
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx"
            @__parser.AddParsedSubObject(this.CreateResourceBasedLiteralControl(16157, 2488, true));
            
            #line default
            #line hidden
        }
        
        
        #line 912304 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx.cs"
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void FrameworkInitialize() {
            base.FrameworkInitialize();
            this.SetStringResourcePointer(global::ASP.viewlog_aspx.@__stringResource, 0);
            this.@__BuildControlTree(this);
            this.AddWrappedFileDependencies(global::ASP.viewlog_aspx.@__fileDependencies);
            this.Request.ValidateInput();
        }
        
        #line default
        #line hidden
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override int GetTypeHashCode() {
            return -549487307;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override void ProcessRequest(System.Web.HttpContext context) {
            base.ProcessRequest(context);
        }
    }
}
