﻿#pragma checksum "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "66AB2E174618676317FBA963AA089F7DB42C20EA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



public partial class OrderDetails : System.Web.SessionState.IRequiresSessionState {
    
    
    #line 19 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
    protected global::System.Web.UI.WebControls.LoginName LoginName1;
    
    #line default
    #line hidden
    
    
    #line 20 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
    protected global::System.Web.UI.WebControls.Label Label2;
    
    #line default
    #line hidden
    
    
    #line 21 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
    protected global::System.Web.UI.WebControls.LoginName LoginName2;
    
    #line default
    #line hidden
    
    
    #line 23 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
    protected global::System.Web.UI.WebControls.LinkButton lblLogout;
    
    #line default
    #line hidden
    
    
    #line 22 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
    protected global::System.Web.UI.HtmlControls.HtmlForm Form1;
    
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
    
    #line 393 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Linq;
    
    #line default
    #line hidden
    
    #line 400 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.Security;
    
    #line default
    #line hidden
    
    #line 391 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.ComponentModel.DataAnnotations;
    
    #line default
    #line hidden
    
    #line 389 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Collections.Generic;
    
    #line default
    #line hidden
    
    #line 395 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Text.RegularExpressions;
    
    #line default
    #line hidden
    
    #line 19 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
    using System.Web.UI.WebControls;
    
    #line default
    #line hidden
    
    #line 406 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Xml.Linq;
    
    #line default
    #line hidden
    
    #line 19 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
    using System.Web.UI;
    
    #line default
    #line hidden
    
    #line 405 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.UI.HtmlControls;
    
    #line default
    #line hidden
    
    #line 396 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web;
    
    #line default
    #line hidden
    
    #line 392 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Configuration;
    
    #line default
    #line hidden
    
    #line 387 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System;
    
    #line default
    #line hidden
    
    #line 394 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Text;
    
    #line default
    #line hidden
    
    #line 401 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.Profile;
    
    #line default
    #line hidden
    
    #line 397 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.Caching;
    
    #line default
    #line hidden
    
    #line 388 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Collections;
    
    #line default
    #line hidden
    
    #line 19 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
    using System.Web.UI.WebControls.WebParts;
    
    #line default
    #line hidden
    
    #line 19 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
    using System.Web.UI.WebControls.Expressions;
    
    #line default
    #line hidden
    
    #line 390 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Collections.Specialized;
    
    #line default
    #line hidden
    
    #line 399 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.SessionState;
    
    #line default
    #line hidden
    
    #line 19 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
    using System.Web.DynamicData;
    
    #line default
    #line hidden
    
    
    [System.Runtime.CompilerServices.CompilerGlobalScopeAttribute()]
    public class orderdetails_aspx : global::OrderDetails, System.Web.IHttpHandler {
        
        private static System.Reflection.MethodInfo @__PageInspector_SetTraceDataMethod = global::ASP.orderdetails_aspx.@__PageInspector_LoadHelper("SetTraceData");
        
        private static bool @__initialized;
        
        private static object @__fileDependencies;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public orderdetails_aspx() {
            string[] dependencies;
            
            #line 912304 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx.cs"
            ((global::System.Web.UI.Page)(this)).AppRelativeVirtualPath = "~/OrderDetails.aspx";
            
            #line default
            #line hidden
            if ((global::ASP.orderdetails_aspx.@__initialized == false)) {
                dependencies = new string[2];
                dependencies[0] = "~/OrderDetails.aspx";
                dependencies[1] = "~/OrderDetails.aspx.cs";
                global::ASP.orderdetails_aspx.@__fileDependencies = this.GetWrappedFileDependencies(dependencies);
                global::ASP.orderdetails_aspx.@__initialized = true;
            }
            this.Server.ScriptTimeout = 30000000;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control2() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("\r\n\r\n<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3" +
                    ".org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n\r\n<html xmlns=\"http://www.w3.org/1" +
                    "999/xhtml\" >\r\n");
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        104,
                        175,
                        true});
            return @__ctrl;
        }
        
        private static System.Reflection.MethodInfo @__PageInspector_LoadHelper(string helperName) {
            System.Type helperClass = System.Type.GetType("Microsoft.VisualStudio.Web.PageInspector.Runtime.WebForms.TraceHelpers, Microsoft" +
                    ".VisualStudio.Web.PageInspector.Tracing, Version=14.0.0.0, Culture=neutral, Publ" +
                    "icKeyToken=b03f5f7f11d50a3a", false, false);
            if ((helperClass != null)) {
                return helperClass.GetMethod(helperName);
            }
            return null;
        }
        
        private void @__PageInspector_SetTraceData(object[] parameters) {
            if ((global::ASP.orderdetails_aspx.@__PageInspector_SetTraceDataMethod != null)) {
                global::ASP.orderdetails_aspx.@__PageInspector_SetTraceDataMethod.Invoke(null, parameters);
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control5() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("Xml Order Creation and Integration");
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        313,
                        34,
                        true});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlTitle @__BuildControl__control4() {
            global::System.Web.UI.HtmlControls.HtmlTitle @__ctrl;
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTitle();
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl1;
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl1 = this.@__BuildControl__control5();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        306,
                        49,
                        false});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlHead @__BuildControl__control3() {
            global::System.Web.UI.HtmlControls.HtmlHead @__ctrl;
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlHead("head");
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlTitle @__ctrl1;
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl1 = this.@__BuildControl__control4();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        279,
                        85,
                        false});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control6() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl(@"
<body bottommargin=""0"" leftmargin=""0""  background=""background.jpg"" text=""red"" style=""width:100%"">
    
<table width=""100%"" height=""94"" cellpadding=""0"" cellspacing=""0"" border=""0"">
	<tr valign=""top"">
<!-- the LOGO is below, edit blanklogo.jpg / or .psd and save as a different
name to create a logo.  Then change the src="""" below to the new file name -->
		<td width=""150"" height=""94""><img src=""templogo.jpg"" width=""150"" height=""94"" border=""0"" alt=""""></td>
<!-- END OF LOGO ----------------------------------------------------------->
		<td width=""368"" height=""94""><img src=""topbar1.jpg"" width=""368"" height=""94"" border=""0"" alt=""""></td>
		<td width=""100%"" height=""94"" background=""topbar1bg.jpg"" align=right>&nbsp; 
<!--		");
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        364,
                        730,
                        true});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.LoginName @__BuildControlLoginName1() {
            global::System.Web.UI.WebControls.LoginName @__ctrl;
            
            #line 19 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.LoginName();
            
            #line default
            #line hidden
            this.LoginName1 = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 19 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.ID = "LoginName1";
            
            #line default
            #line hidden
            
            #line 19 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Font.Size = global::System.Web.UI.WebControls.FontUnit.Medium;
            
            #line default
            #line hidden
            
            #line 19 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("style", "z-index: 100; left: 581px;  top: 60px");
            
            #line default
            #line hidden
            
            #line 19 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(108D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1094,
                        127,
                        false});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control7() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("-->\r\n                ");
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1221,
                        21,
                        true});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.Label @__BuildControlLabel2() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            
            #line 20 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            
            #line default
            #line hidden
            this.Label2 = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 20 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.ID = "Label2";
            
            #line default
            #line hidden
            
            #line 20 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Text = "Welcome ";
            
            #line default
            #line hidden
            
            #line 20 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Font.Size = global::System.Web.UI.WebControls.FontUnit.Medium;
            
            #line default
            #line hidden
            
            #line 20 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Font.Bold = true;
            
            #line default
            #line hidden
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1242,
                        102,
                        false});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control8() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("\r\n\t\t    ");
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1344,
                        8,
                        true});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.LoginName @__BuildControlLoginName2() {
            global::System.Web.UI.WebControls.LoginName @__ctrl;
            
            #line 21 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.LoginName();
            
            #line default
            #line hidden
            this.LoginName2 = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 21 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.ID = "LoginName2";
            
            #line default
            #line hidden
            
            #line 21 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Font.Size = global::System.Web.UI.WebControls.FontUnit.Medium;
            
            #line default
            #line hidden
            
            #line 21 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(108D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            
            #line 21 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Font.Bold = true;
            
            #line default
            #line hidden
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1352,
                        98,
                        false});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control9() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("\r\n            ");
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1450,
                        14,
                        true});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control10() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("\r\n                ");
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1496,
                        18,
                        true});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control11() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("Logout");
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1691,
                        6,
                        true});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControllblLogout() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            
            #line 23 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            
            #line default
            #line hidden
            this.lblLogout = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 23 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.ID = "lblLogout";
            
            #line default
            #line hidden
            
            #line 23 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Font.Bold = false;
            
            #line default
            #line hidden
            
            #line 23 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Font.Names = new string[] {
                    "verdana"};
            
            #line default
            #line hidden
            
            #line 23 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Font.Size = global::System.Web.UI.WebControls.FontUnit.Small;
            
            #line default
            #line hidden
            
            #line 23 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Height = new System.Web.UI.WebControls.Unit(22D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            
            #line 23 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(55D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl1;
            
            #line 23 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl1 = this.@__BuildControl__control11();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 23 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            
            #line 23 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.Click += new System.EventHandler(this.lblLogout_Click);
            
            #line default
            #line hidden
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1514,
                        200,
                        false});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control12() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("\r\n            ");
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1714,
                        14,
                        true});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlForm @__BuildControlForm1() {
            global::System.Web.UI.HtmlControls.HtmlForm @__ctrl;
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlForm();
            
            #line default
            #line hidden
            this.Form1 = @__ctrl;
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl.ID = "Form1";
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl1;
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl1 = this.@__BuildControl__control10();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.LinkButton @__ctrl2;
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl2 = this.@__BuildControllblLogout();
            
            #line default
            #line hidden
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl2);
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl3;
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl3 = this.@__BuildControl__control12();
            
            #line default
            #line hidden
            
            #line 22 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl3);
            
            #line default
            #line hidden
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1464,
                        271,
                        false});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control13() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("\r\n\t\t\r\n\t\t\r\n\t\t</td>\r\n\t</tr>\r\n</table>\r\n<table width=\"100%\" height=\"91\" cellpadding=" +
                    "\"0\" cellspacing=\"0\" border=\"0\">\r\n\t<tr valign=\"top\">\r\n\t\t<td width=\"613\" height=\"9" +
                    "1\"><img src=\"2maincolorarea.jpg\" width=\"613\" height=\"91\" border=\"0\" alt=\"\"></td>" +
                    "\r\n\t\t<td width=\"640\" height=\"80\" background=\"img1.jpg\">&nbsp;</td>\r\n\t</tr>\r\n</tab" +
                    "le>\r\n<table width=\"100%\" height=\"33\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">" +
                    "\r\n\t<tr valign=\"top\">\r\n<!-- IMPORTANT.. edit blankbutton1.jpg or .psd and save as" +
                    " a different\r\nname to create the FIRST button on this menu.  Then change the src" +
                    "=\"\" below to the new file name.\r\nThe blankbutton.jpg is a different graphic and " +
                    "shouldn\'t be edited for placement here -->\r\n\t\t<!--<td width=\"246\" height=\"33\"><a" +
                    " href=\"\"><img src=\"tempbuttonsh.jpg\" width=\"407\" height=\"33\" border=\"0\" alt=\"\"><" +
                    "/a></td>-->\r\n<!-- end of first button code -->\r\n\t\t<td width=\"368\" height=\"33\"><i" +
                    "mg src=\"3buttonarea.jpg\" width=\"427\" height=\"33\" border=\"0\" alt=\"\"></td>\r\n\t\t<td " +
                    "width=\"100%\" height=\"33\" background=\"3buttonareabg.jpg\">&nbsp;</td>\r\n\t</tr>\r\n</t" +
                    "able>\r\n\r\n\r\n\r\n<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width: 100" +
                    "%\">\r\n\t<tr valign=\"top\">\r\n\r\n\t\t<td width=\"207\" style=\"height: 188px\" align=right>\r" +
                    "\n\r\n<a href=\"\"><img src=\"tempbuttons/home.jpg\" width=\"207\" height=\"33\" border=\"0\"" +
                    " alt=\"\"></a><BR>\r\n<a href=\"OrderDetails.aspx\"><img src=\"tempbuttons/ogen.jpg\" wi" +
                    "dth=\"207\" height=\"33\" border=\"0\" alt=\"\" id=\"IMG3\" language=\"javascript\" onclick=" +
                    "\"return IMG2_onclick()\"></a><BR>\r\n<a href=\"ViewLog.aspx\"><img src=\"tempbuttons/o" +
                    "viw.jpg\" width=\"207\" height=\"33\" border=\"0\" alt=\"\" id=\"IMG4\" language=\"javascrip" +
                    "t\" onclick=\"return IMG1_onclick()\"></a><BR>\r\n<a href=\"\"><img src=\"tempbuttons/Ab" +
                    "tus.jpg\" width=\"207\" border=\"0\" alt=\"\" style=\"height: 29px; z-index: 101; left: " +
                    "0px;  top: 410px;\"></a><BR>\r\n<a href=\"ChangePassword.aspx\"><img src=\"tempbuttons" +
                    "/ChangePassword.jpg\" width=\"207\" height=\"33\" border=\"0\" alt=\"\" style=\"z-index: 1" +
                    "00; left: 0px;  top: 377px\"></a><BR>\r\n<a href=\"UpdateOrder.aspx\"><img src=\"tempb" +
                    "uttons/UpdateOrder.jpg\" width=\"207\" border=\"0\" alt=\"\" style=\"height: 32px; z-ind" +
                    "ex: 102; left: 0px;  top: 348px;\"></a><BR>\r\n\t\t</td>\r\n\r\n\t\r\n\t\r\n\t\t<td width=\"20\" st" +
                    "yle=\"height: 188px\">&nbsp;&nbsp;&nbsp;</td>\r\n\r\n\t\t<td width=\"100%\" style=\"height:" +
                    " 500px;\">\r\n\t\t<iframe src=\"OrderCreator.aspx\" id=\"result\"  width=\'100%\' height=\'1" +
                    "00%\' frameborder=\"0\" scrolling=\"auto\" ></iframe>\r\n</td>\r\n\t\t\r\n</tr>\r\n\t\t</table>\t\t" +
                    "\r\n\t\t\t\t\t\t\t\r\n\r\n\r\n</body>\r\n</html>\r\n");
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1735,
                        2434,
                        true});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlTree(orderdetails_aspx @__ctrl) {
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            this.InitializeCulture();
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl1;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl1 = this.@__BuildControl__control2();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlHead @__ctrl2;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl2 = this.@__BuildControl__control3();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl2);
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl3;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl3 = this.@__BuildControl__control6();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl3);
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.LoginName @__ctrl4;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl4 = this.@__BuildControlLoginName1();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl4);
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl5;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl5 = this.@__BuildControl__control7();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl5);
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.Label @__ctrl6;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl6 = this.@__BuildControlLabel2();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl6);
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl7;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl7 = this.@__BuildControl__control8();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl7);
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.LoginName @__ctrl8;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl8 = this.@__BuildControlLoginName2();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl8);
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl9;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl9 = this.@__BuildControl__control9();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl9);
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlForm @__ctrl10;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl10 = this.@__BuildControlForm1();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl10);
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl11;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__ctrl11 = this.@__BuildControl__control13();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx"
            @__parser.AddParsedSubObject(@__ctrl11);
            
            #line default
            #line hidden
        }
        
        
        #line 912304 "D:\WinCVS\ThimeXMLORDER\OrderDetails.aspx.cs"
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void FrameworkInitialize() {
            base.FrameworkInitialize();
            this.@__BuildControlTree(this);
            this.AddWrappedFileDependencies(global::ASP.orderdetails_aspx.@__fileDependencies);
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