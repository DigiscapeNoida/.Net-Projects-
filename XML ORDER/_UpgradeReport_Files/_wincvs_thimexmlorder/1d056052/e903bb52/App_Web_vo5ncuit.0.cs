﻿#pragma checksum "D:\WinCVS\ThimeXMLORDER\FullMaster.master" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C941DF15C158B3C715BB6D66FC99DBC628D8CC58"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



public partial class FullMaster {
    
    
    #line 3 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
    protected global::System.Web.UI.WebControls.ContentPlaceHolder TitleThis;
    
    #line default
    #line hidden
    
    
    #line 4 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
    protected global::System.Web.UI.WebControls.ContentPlaceHolder UserThis;
    
    #line default
    #line hidden
    
    
    #line 158 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
    protected global::System.Web.UI.WebControls.ContentPlaceHolder ContentBody;
    
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
    
    #line 284 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.Security;
    
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
    
    #line 279 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Text;
    
    #line default
    #line hidden
    
    #line 277 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Collections.Specialized;
    
    #line default
    #line hidden
    
    #line 3 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
    using System.Web.UI.WebControls;
    
    #line default
    #line hidden
    
    #line 276 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Collections;
    
    #line default
    #line hidden
    
    #line 3 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
    using System.Web.UI.WebControls.WebParts;
    
    #line default
    #line hidden
    
    #line 278 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Configuration;
    
    #line default
    #line hidden
    
    #line 275 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System;
    
    #line default
    #line hidden
    
    #line 1 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
    using ASP;
    
    #line default
    #line hidden
    
    #line 282 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.Caching;
    
    #line default
    #line hidden
    
    #line 3 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
    using System.Web.UI;
    
    #line default
    #line hidden
    
    #line 289 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.UI.HtmlControls;
    
    #line default
    #line hidden
    
    #line 280 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Text.RegularExpressions;
    
    #line default
    #line hidden
    
    #line 285 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.Profile;
    
    #line default
    #line hidden
    
    
    [System.Runtime.CompilerServices.CompilerGlobalScopeAttribute()]
    public class fullmaster_master : global::FullMaster {
        
        private System.Web.UI.ITemplate @__Template_TitleThis;
        
        private System.Web.UI.ITemplate @__Template_UserThis;
        
        private System.Web.UI.ITemplate @__Template_ContentBody;
        
        private static bool @__initialized;
        
        private static object @__stringResource;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public fullmaster_master() {
            
            #line 912304 "D:\WinCVS\ThimeXMLORDER\FullMaster.master.cs"
            ((global::System.Web.UI.MasterPage)(this)).AppRelativeVirtualPath = "~/FullMaster.master";
            
            #line default
            #line hidden
            if ((global::ASP.fullmaster_master.@__initialized == false)) {
                global::ASP.fullmaster_master.@__stringResource = this.ReadStringResource();
                global::ASP.fullmaster_master.@__initialized = true;
            }
            this.ContentPlaceHolders.Add("titlethis");
            this.ContentPlaceHolders.Add("userthis");
            this.ContentPlaceHolders.Add("contentbody");
        }
        
        [TemplateContainer(typeof(System.Web.UI.MasterPage))]
        [TemplateInstanceAttribute(System.Web.UI.TemplateInstance.Single)]
        public virtual System.Web.UI.ITemplate Template_TitleThis {
            get {
                return this.@__Template_TitleThis;
            }
            set {
                this.@__Template_TitleThis = value;
            }
        }
        
        [TemplateContainer(typeof(System.Web.UI.MasterPage))]
        [TemplateInstanceAttribute(System.Web.UI.TemplateInstance.Single)]
        public virtual System.Web.UI.ITemplate Template_UserThis {
            get {
                return this.@__Template_UserThis;
            }
            set {
                this.@__Template_UserThis = value;
            }
        }
        
        [TemplateContainer(typeof(System.Web.UI.MasterPage))]
        [TemplateInstanceAttribute(System.Web.UI.TemplateInstance.Single)]
        public virtual System.Web.UI.ITemplate Template_ContentBody {
            get {
                return this.@__Template_ContentBody;
            }
            set {
                this.@__Template_ContentBody = value;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.ContentPlaceHolder @__BuildControlTitleThis() {
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl;
            
            #line 3 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__ctrl = new global::System.Web.UI.WebControls.ContentPlaceHolder();
            
            #line default
            #line hidden
            this.TitleThis = @__ctrl;
            @__ctrl.TemplateControl = this;
            
            #line 3 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__ctrl.ID = "TitleThis";
            
            #line default
            #line hidden
            if ((this.ContentTemplates != null)) {
                this.@__Template_TitleThis = ((System.Web.UI.ITemplate)(this.ContentTemplates["TitleThis"]));
            }
            if ((this.@__Template_TitleThis != null)) {
                this.@__Template_TitleThis.InstantiateIn(@__ctrl);
            }
            else {
                System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
                
                #line 3 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
                @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Default Title"));
                
                #line default
                #line hidden
            }
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlTitle(System.Web.UI.Control @__ctrl) {
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl1;
            
            #line 3 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__ctrl1 = this.@__BuildControlTitleThis();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 3 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.ContentPlaceHolder @__BuildControlUserThis() {
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl;
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__ctrl = new global::System.Web.UI.WebControls.ContentPlaceHolder();
            
            #line default
            #line hidden
            this.UserThis = @__ctrl;
            @__ctrl.TemplateControl = this;
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__ctrl.ID = "UserThis";
            
            #line default
            #line hidden
            if ((this.ContentTemplates != null)) {
                this.@__Template_UserThis = ((System.Web.UI.ITemplate)(this.ContentTemplates["UserThis"]));
            }
            if ((this.@__Template_UserThis != null)) {
                this.@__Template_UserThis.InstantiateIn(@__ctrl);
            }
            else {
                System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
                
                #line 4 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
                @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Default User"));
                
                #line default
                #line hidden
            }
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlUser(System.Web.UI.Control @__ctrl) {
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl1;
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__ctrl1 = this.@__BuildControlUserThis();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.ContentPlaceHolder @__BuildControlContentBody() {
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl;
            
            #line 158 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__ctrl = new global::System.Web.UI.WebControls.ContentPlaceHolder();
            
            #line default
            #line hidden
            this.ContentBody = @__ctrl;
            @__ctrl.TemplateControl = this;
            
            #line 158 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__ctrl.ID = "ContentBody";
            
            #line default
            #line hidden
            if ((this.ContentTemplates != null)) {
                this.@__Template_ContentBody = ((System.Web.UI.ITemplate)(this.ContentTemplates["ContentBody"]));
            }
            if ((this.@__Template_ContentBody != null)) {
                this.@__Template_ContentBody.InstantiateIn(@__ctrl);
            }
            else {
                System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
                
                #line 158 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
                @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n        "));
                
                #line default
                #line hidden
            }
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlFullContent(System.Web.UI.Control @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__parser.AddParsedSubObject(this.CreateResourceBasedLiteralControl(0, 7973, true));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl1;
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__ctrl1 = this.@__BuildControlContentBody();
            
            #line default
            #line hidden
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__parser.AddParsedSubObject(this.CreateResourceBasedLiteralControl(7973, 566, true));
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlTree(fullmaster_master @__ctrl) {
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__ctrl.MasterPageFile = "MasterPage.master";
            
            #line default
            #line hidden
            
            #line 3 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            this.AddContentTemplate("PageTitle", new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControlTitle)));
            
            #line default
            #line hidden
            
            #line 4 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            this.AddContentTemplate("UserMaster", new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControlUser)));
            
            #line default
            #line hidden
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            this.AddContentTemplate("PageBody", new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControlFullContent)));
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n"));
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\r\n"));
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\FullMaster.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n"));
            
            #line default
            #line hidden
        }
        
        
        #line 912304 "D:\WinCVS\ThimeXMLORDER\FullMaster.master.cs"
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void FrameworkInitialize() {
            base.FrameworkInitialize();
            this.SetStringResourcePointer(global::ASP.fullmaster_master.@__stringResource, 0);
            this.@__BuildControlTree(this);
        }
        
        #line default
        #line hidden
    }
}
