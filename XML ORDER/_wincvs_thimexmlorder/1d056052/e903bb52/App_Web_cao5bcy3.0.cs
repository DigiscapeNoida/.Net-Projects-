﻿#pragma checksum "D:\WinCVS\ThimeXMLORDER\LogData.aspx" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AAB226A61BFCC02AE9E6BC447A56582689508143"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



public partial class LogData : System.Web.SessionState.IRequiresSessionState {
    
    
    #line 6 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
    protected global::System.Web.UI.WebControls.Label lblmsg;
    
    #line default
    #line hidden
    
    
    #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
    protected global::System.Web.UI.WebControls.GridView gvLog;
    
    #line default
    #line hidden
    
    
    #line 4 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
    protected global::System.Web.UI.HtmlControls.HtmlForm form1;
    
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
    
    #line 3 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
    using System.Web.UI.WebControls.Expressions;
    
    #line default
    #line hidden
    
    #line 388 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Collections;
    
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
    
    #line 3 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
    using System.Web.UI;
    
    #line default
    #line hidden
    
    #line 389 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Collections.Generic;
    
    #line default
    #line hidden
    
    #line 393 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Linq;
    
    #line default
    #line hidden
    
    #line 1 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
    using ASP;
    
    #line default
    #line hidden
    
    #line 399 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.SessionState;
    
    #line default
    #line hidden
    
    #line 392 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Configuration;
    
    #line default
    #line hidden
    
    #line 396 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web;
    
    #line default
    #line hidden
    
    #line 3 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
    using System.Web.DynamicData;
    
    #line default
    #line hidden
    
    #line 397 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.Caching;
    
    #line default
    #line hidden
    
    #line 3 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
    using System.Web.UI.WebControls;
    
    #line default
    #line hidden
    
    #line 391 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.ComponentModel.DataAnnotations;
    
    #line default
    #line hidden
    
    #line 400 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.Security;
    
    #line default
    #line hidden
    
    #line 387 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System;
    
    #line default
    #line hidden
    
    #line 3 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
    using System.Web.UI.WebControls.WebParts;
    
    #line default
    #line hidden
    
    #line 395 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Text.RegularExpressions;
    
    #line default
    #line hidden
    
    #line 390 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Collections.Specialized;
    
    #line default
    #line hidden
    
    #line 406 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Xml.Linq;
    
    #line default
    #line hidden
    
    #line 405 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.UI.HtmlControls;
    
    #line default
    #line hidden
    
    
    [System.Runtime.CompilerServices.CompilerGlobalScopeAttribute()]
    public class logdata_aspx : global::LogData, System.Web.IHttpHandler {
        
        private static System.Reflection.MethodInfo @__PageInspector_SetTraceDataMethod = global::ASP.logdata_aspx.@__PageInspector_LoadHelper("SetTraceData");
        
        private static bool @__initialized;
        
        private static object @__fileDependencies;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public logdata_aspx() {
            string[] dependencies;
            
            #line 912304 "D:\WinCVS\ThimeXMLORDER\LogData.aspx.cs"
            ((global::System.Web.UI.Page)(this)).AppRelativeVirtualPath = "~/LogData.aspx";
            
            #line default
            #line hidden
            if ((global::ASP.logdata_aspx.@__initialized == false)) {
                dependencies = new string[6];
                dependencies[0] = "~/LogData.aspx";
                dependencies[1] = "~/FullMaster.master";
                dependencies[2] = "~/FullMaster.master.cs";
                dependencies[3] = "~/MasterPage.master";
                dependencies[4] = "~/MasterPage.master.cs";
                dependencies[5] = "~/LogData.aspx.cs";
                global::ASP.logdata_aspx.@__fileDependencies = this.GetWrappedFileDependencies(dependencies);
                global::ASP.logdata_aspx.@__initialized = true;
            }
            this.Server.ScriptTimeout = 30000000;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control2() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("\r\n    ");
            @__ctrl.TemplateControl = this;
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        208,
                        6,
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
            if ((global::ASP.logdata_aspx.@__PageInspector_SetTraceDataMethod != null)) {
                global::ASP.logdata_aspx.@__PageInspector_SetTraceDataMethod.Invoke(null, parameters);
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control3() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("\r\n    <div>\r\n        ");
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        246,
                        21,
                        true});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.Label @__BuildControllblmsg() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            
            #line default
            #line hidden
            this.lblmsg = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.ID = "lblmsg";
            
            #line default
            #line hidden
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.ForeColor = global::System.Drawing.Color.Red;
            
            #line default
            #line hidden
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.Font.Bold = true;
            
            #line default
            #line hidden
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        267,
                        80,
                        false});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control4() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("\r\n        ");
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        347,
                        10,
                        true});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControl__control5(System.Web.UI.WebControls.TableItemStyle @__ctrl) {
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.BackColor = global::System.Drawing.Color.White;
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.ForeColor = ((System.Drawing.Color)(global::System.Drawing.Color.FromArgb(0, 0, 102)));
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControl__control6(System.Web.UI.WebControls.TableItemStyle @__ctrl) {
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.ForeColor = ((System.Drawing.Color)(global::System.Drawing.Color.FromArgb(0, 0, 102)));
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControl__control7(System.Web.UI.WebControls.TableItemStyle @__ctrl) {
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.BackColor = ((System.Drawing.Color)(global::System.Drawing.Color.FromArgb(102, 153, 153)));
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.Font.Bold = true;
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.ForeColor = global::System.Drawing.Color.White;
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControl__control8(System.Web.UI.WebControls.TableItemStyle @__ctrl) {
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.BackColor = global::System.Drawing.Color.White;
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.ForeColor = ((System.Drawing.Color)(global::System.Drawing.Color.FromArgb(0, 0, 102)));
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.HorizontalAlign = global::System.Web.UI.WebControls.HorizontalAlign.Left;
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControl__control9(System.Web.UI.WebControls.TableItemStyle @__ctrl) {
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.BackColor = ((System.Drawing.Color)(global::System.Drawing.Color.FromArgb(0, 102, 153)));
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.Font.Bold = true;
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.ForeColor = global::System.Drawing.Color.White;
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.GridView @__BuildControlgvLog() {
            global::System.Web.UI.WebControls.GridView @__ctrl;
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.GridView();
            
            #line default
            #line hidden
            this.gvLog = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.ID = "gvLog";
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("Style", "z-index: 100; left: 0px; \r\n            top: 0px; background-color:#669999; border" +
                    ":1; font-size: x-small; font-family: Verdana;");
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(100D, global::System.Web.UI.WebControls.UnitType.Percentage);
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.BackColor = global::System.Drawing.Color.White;
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.BorderColor = ((System.Drawing.Color)(global::System.Drawing.Color.FromArgb(204, 204, 204)));
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.BorderStyle = global::System.Web.UI.WebControls.BorderStyle.None;
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.BorderWidth = new System.Web.UI.WebControls.Unit(1D, global::System.Web.UI.WebControls.UnitType.Pixel);
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.CellPadding = 3;
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            this.@__BuildControl__control5(@__ctrl.FooterStyle);
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            this.@__BuildControl__control6(@__ctrl.RowStyle);
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            this.@__BuildControl__control7(@__ctrl.SelectedRowStyle);
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            this.@__BuildControl__control8(@__ctrl.PagerStyle);
            
            #line default
            #line hidden
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            this.@__BuildControl__control9(@__ctrl.HeaderStyle);
            
            #line default
            #line hidden
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        357,
                        681,
                        false});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control10() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("\r\n    </div>\r\n    ");
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1038,
                        18,
                        true});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlForm @__BuildControlform1() {
            global::System.Web.UI.HtmlControls.HtmlForm @__ctrl;
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlForm();
            
            #line default
            #line hidden
            this.form1 = @__ctrl;
            @__ctrl.TemplateControl = this;
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.ID = "form1";
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl1;
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl1 = this.@__BuildControl__control3();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.Label @__ctrl2;
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl2 = this.@__BuildControllblmsg();
            
            #line default
            #line hidden
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__parser.AddParsedSubObject(@__ctrl2);
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl3;
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl3 = this.@__BuildControl__control4();
            
            #line default
            #line hidden
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__parser.AddParsedSubObject(@__ctrl3);
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.GridView @__ctrl4;
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl4 = this.@__BuildControlgvLog();
            
            #line default
            #line hidden
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__parser.AddParsedSubObject(@__ctrl4);
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl5;
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl5 = this.@__BuildControl__control10();
            
            #line default
            #line hidden
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__parser.AddParsedSubObject(@__ctrl5);
            
            #line default
            #line hidden
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        214,
                        849,
                        false});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.LiteralControl @__BuildControl__control11() {
            global::System.Web.UI.LiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.LiteralControl("\r\n");
            @__ctrl.TemplateControl = this;
            this.@__PageInspector_SetTraceData(new object[] {
                        @__ctrl,
                        null,
                        1063,
                        2,
                        true});
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlLogOrder(System.Web.UI.Control @__ctrl) {
            global::System.Web.UI.LiteralControl @__ctrl1;
            
            #line 3 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl1 = this.@__BuildControl__control2();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 3 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlForm @__ctrl2;
            
            #line 3 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl2 = this.@__BuildControlform1();
            
            #line default
            #line hidden
            
            #line 3 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__parser.AddParsedSubObject(@__ctrl2);
            
            #line default
            #line hidden
            global::System.Web.UI.LiteralControl @__ctrl3;
            
            #line 3 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl3 = this.@__BuildControl__control11();
            
            #line default
            #line hidden
            
            #line 3 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__parser.AddParsedSubObject(@__ctrl3);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlTree(logdata_aspx @__ctrl) {
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            @__ctrl.MasterPageFile = "FullMaster.master";
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            this.InitializeCulture();
            
            #line default
            #line hidden
            
            #line 3 "D:\WinCVS\ThimeXMLORDER\LogData.aspx"
            this.AddContentTemplate("ContentBody", new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControlLogOrder)));
            
            #line default
            #line hidden
        }
        
        
        #line 912304 "D:\WinCVS\ThimeXMLORDER\LogData.aspx.cs"
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void FrameworkInitialize() {
            base.FrameworkInitialize();
            this.@__BuildControlTree(this);
            this.AddWrappedFileDependencies(global::ASP.logdata_aspx.@__fileDependencies);
            this.Request.ValidateInput();
        }
        
        #line default
        #line hidden
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override int GetTypeHashCode() {
            return 1504140143;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override void ProcessRequest(System.Web.HttpContext context) {
            base.ProcessRequest(context);
        }
    }
}
