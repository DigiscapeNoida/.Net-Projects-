﻿#pragma checksum "D:\WinCVS\ThimeXMLORDER\MasterPage.master" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "02B137CB83E453F273D56085F0C4AB0224586E22"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



public partial class MasterPage {
    
    
    #line 5 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
    protected global::System.Web.UI.HtmlControls.HtmlHead ctl00_ctl00_Head1;
    
    #line default
    #line hidden
    
    
    #line 165 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
    protected global::System.Web.UI.WebControls.HyperLink OrderCreatorAnchor;
    
    #line default
    #line hidden
    
    
    #line 183 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
    protected global::System.Web.UI.WebControls.ContentPlaceHolder UserMaster;
    
    #line default
    #line hidden
    
    
    #line 190 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
    protected global::System.Web.UI.WebControls.ContentPlaceHolder PageTitle;
    
    #line default
    #line hidden
    
    
    #line 195 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
    protected global::System.Web.UI.WebControls.ContentPlaceHolder PageBody;
    
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
    
    #line 165 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
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
    
    #line 165 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
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
    
    #line 165 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
    using System.Web.UI.WebControls.WebParts;
    
    #line default
    #line hidden
    
    #line 283 "C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\web.config"
    using System.Web.SessionState;
    
    #line default
    #line hidden
    
    
    [System.Runtime.CompilerServices.CompilerGlobalScopeAttribute()]
    public class masterpage_master : global::MasterPage {
        
        private System.Web.UI.ITemplate @__Template_UserMaster;
        
        private System.Web.UI.ITemplate @__Template_PageTitle;
        
        private System.Web.UI.ITemplate @__Template_PageBody;
        
        private static bool @__initialized;
        
        private static object @__stringResource;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public masterpage_master() {
            
            #line 912304 "D:\WinCVS\ThimeXMLORDER\MasterPage.master.cs"
            ((global::System.Web.UI.MasterPage)(this)).AppRelativeVirtualPath = "~/MasterPage.master";
            
            #line default
            #line hidden
            if ((global::ASP.masterpage_master.@__initialized == false)) {
                global::ASP.masterpage_master.@__stringResource = this.ReadStringResource();
                global::ASP.masterpage_master.@__initialized = true;
            }
            this.ContentPlaceHolders.Add("usermaster");
            this.ContentPlaceHolders.Add("pagetitle");
            this.ContentPlaceHolders.Add("pagebody");
        }
        
        [TemplateContainer(typeof(System.Web.UI.MasterPage))]
        [TemplateInstanceAttribute(System.Web.UI.TemplateInstance.Single)]
        public virtual System.Web.UI.ITemplate Template_UserMaster {
            get {
                return this.@__Template_UserMaster;
            }
            set {
                this.@__Template_UserMaster = value;
            }
        }
        
        [TemplateContainer(typeof(System.Web.UI.MasterPage))]
        [TemplateInstanceAttribute(System.Web.UI.TemplateInstance.Single)]
        public virtual System.Web.UI.ITemplate Template_PageTitle {
            get {
                return this.@__Template_PageTitle;
            }
            set {
                this.@__Template_PageTitle = value;
            }
        }
        
        [TemplateContainer(typeof(System.Web.UI.MasterPage))]
        [TemplateInstanceAttribute(System.Web.UI.TemplateInstance.Single)]
        public virtual System.Web.UI.ITemplate Template_PageBody {
            get {
                return this.@__Template_PageBody;
            }
            set {
                this.@__Template_PageBody = value;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlMeta @__BuildControl__control2() {
            global::System.Web.UI.HtmlControls.HtmlMeta @__ctrl;
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlMeta();
            
            #line default
            #line hidden
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl.Content = "text/html; charset=utf-8";
            
            #line default
            #line hidden
            
            #line 6 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("http-equiv", "Content-Type");
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlTitle @__BuildControl__control3() {
            global::System.Web.UI.HtmlControls.HtmlTitle @__ctrl;
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTitle();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 7 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("XML Order Creation and Integration"));
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlLink @__BuildControl__control4() {
            global::System.Web.UI.HtmlControls.HtmlLink @__ctrl;
            
            #line 8 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlLink();
            
            #line default
            #line hidden
            
            #line 8 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl.Href = "ImagesAndStyleSheet/qsstyle.css";
            
            #line default
            #line hidden
            
            #line 8 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("rel", "stylesheet");
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlHead @__BuildControlctl00_ctl00_Head1() {
            global::System.Web.UI.HtmlControls.HtmlHead @__ctrl;
            
            #line 5 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlHead("HEAD");
            
            #line default
            #line hidden
            this.ctl00_ctl00_Head1 = @__ctrl;
            
            #line 5 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl.ID = "ctl00_ctl00_Head1";
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlMeta @__ctrl1;
            
            #line 5 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl1 = this.@__BuildControl__control2();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 5 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlTitle @__ctrl2;
            
            #line 5 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl2 = this.@__BuildControl__control3();
            
            #line default
            #line hidden
            
            #line 5 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl2);
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlLink @__ctrl3;
            
            #line 5 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl3 = this.@__BuildControl__control4();
            
            #line default
            #line hidden
            
            #line 5 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl3);
            
            #line default
            #line hidden
            
            #line 5 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(this.CreateResourceBasedLiteralControl(0, 4185, true));
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.HyperLink @__BuildControlOrderCreatorAnchor() {
            global::System.Web.UI.WebControls.HyperLink @__ctrl;
            
            #line 165 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl = new global::System.Web.UI.WebControls.HyperLink();
            
            #line default
            #line hidden
            this.OrderCreatorAnchor = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            
            #line 165 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "toolbar");
            
            #line default
            #line hidden
            
            #line 165 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl.ID = "OrderCreatorAnchor";
            
            #line default
            #line hidden
            
            #line 165 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl.NavigateUrl = "OrderCreator.aspx";
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 165 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Create Order"));
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.ContentPlaceHolder @__BuildControlUserMaster() {
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl;
            
            #line 183 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl = new global::System.Web.UI.WebControls.ContentPlaceHolder();
            
            #line default
            #line hidden
            this.UserMaster = @__ctrl;
            
            #line 183 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl.ID = "UserMaster";
            
            #line default
            #line hidden
            if ((this.ContentTemplates != null)) {
                this.@__Template_UserMaster = ((System.Web.UI.ITemplate)(this.ContentTemplates["UserMaster"]));
            }
            if ((this.@__Template_UserMaster != null)) {
                this.@__Template_UserMaster.InstantiateIn(@__ctrl);
            }
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.ContentPlaceHolder @__BuildControlPageTitle() {
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl;
            
            #line 190 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl = new global::System.Web.UI.WebControls.ContentPlaceHolder();
            
            #line default
            #line hidden
            this.PageTitle = @__ctrl;
            
            #line 190 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl.ID = "PageTitle";
            
            #line default
            #line hidden
            if ((this.ContentTemplates != null)) {
                this.@__Template_PageTitle = ((System.Web.UI.ITemplate)(this.ContentTemplates["PageTitle"]));
            }
            if ((this.@__Template_PageTitle != null)) {
                this.@__Template_PageTitle.InstantiateIn(@__ctrl);
            }
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.ContentPlaceHolder @__BuildControlPageBody() {
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl;
            
            #line 195 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl = new global::System.Web.UI.WebControls.ContentPlaceHolder();
            
            #line default
            #line hidden
            this.PageBody = @__ctrl;
            
            #line 195 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl.ID = "PageBody";
            
            #line default
            #line hidden
            if ((this.ContentTemplates != null)) {
                this.@__Template_PageBody = ((System.Web.UI.ITemplate)(this.ContentTemplates["PageBody"]));
            }
            if ((this.@__Template_PageBody != null)) {
                this.@__Template_PageBody.InstantiateIn(@__ctrl);
            }
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlTree(masterpage_master @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3c." +
                        "org/TR/1999/REC-html401-19991224/loose.dtd\">\r\n\r\n<HTML xmlns=\"http://www.w3.org/1" +
                        "999/xhtml\"> \r\n"));
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlHead @__ctrl1;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl1 = this.@__BuildControlctl00_ctl00_Head1();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(this.CreateResourceBasedLiteralControl(4185, 2023, true));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.HyperLink @__ctrl2;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl2 = this.@__BuildControlOrderCreatorAnchor();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl2);
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(this.CreateResourceBasedLiteralControl(6208, 962, true));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl3;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl3 = this.@__BuildControlUserMaster();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl3);
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(this.CreateResourceBasedLiteralControl(7170, 426, true));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl4;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl4 = this.@__BuildControlPageTitle();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl4);
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(this.CreateResourceBasedLiteralControl(7596, 332, true));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl5;
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__ctrl5 = this.@__BuildControlPageBody();
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl5);
            
            #line default
            #line hidden
            
            #line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master"
            @__parser.AddParsedSubObject(this.CreateResourceBasedLiteralControl(7928, 1053, true));
            
            #line default
            #line hidden
        }
        
        
        #line 912304 "D:\WinCVS\ThimeXMLORDER\MasterPage.master.cs"
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void FrameworkInitialize() {
            base.FrameworkInitialize();
            this.SetStringResourcePointer(global::ASP.masterpage_master.@__stringResource, 0);
            this.@__BuildControlTree(this);
        }
        
        #line default
        #line hidden
    }
}