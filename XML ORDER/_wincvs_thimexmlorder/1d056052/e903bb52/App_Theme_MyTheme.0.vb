﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.ComponentModel.DataAnnotations
Imports System.Configuration
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.Caching
Imports System.Web.DynamicData
Imports System.Web.Profile
Imports System.Web.Security
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.Expressions
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq

Namespace ASP
    
    <System.Runtime.CompilerServices.CompilerGlobalScopeAttribute()>  _
    Public Class MyTheme
        Inherits Global.System.Web.UI.PageTheme
        
        Private __controlSkins As System.Collections.Specialized.HybridDictionary = New System.Collections.Specialized.HybridDictionary(3)
        
        Private __linkedStyleSheets() As String = Nothing
        
        Private Shared __initialized As Boolean
        
        Private Shared __BuildControl__control2_skinKey As Object = System.Web.UI.PageTheme.CreateSkinKey(GetType(System.Web.UI.WebControls.Label), "SameSizeLabel")
        
        Private Shared __BuildControl__control3_skinKey As Object = System.Web.UI.PageTheme.CreateSkinKey(GetType(System.Web.UI.WebControls.DropDownList), "SameSizeDD")
        
        Private Shared __BuildControl__control4_skinKey As Object = System.Web.UI.PageTheme.CreateSkinKey(GetType(System.Web.UI.WebControls.TextBox), "SameSizeText")
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub New()
            MyBase.New
            Me.__controlSkins(__BuildControl__control2_skinKey) = New System.Web.UI.ControlSkin(GetType(System.Web.UI.WebControls.Label), AddressOf Me.__BuildControl__control2)
            Me.__controlSkins(__BuildControl__control3_skinKey) = New System.Web.UI.ControlSkin(GetType(System.Web.UI.WebControls.DropDownList), AddressOf Me.__BuildControl__control3)
            Me.__controlSkins(__BuildControl__control4_skinKey) = New System.Web.UI.ControlSkin(GetType(System.Web.UI.WebControls.TextBox), AddressOf Me.__BuildControl__control4)
            If (Global.ASP.MyTheme.__initialized = false) Then
                Global.ASP.MyTheme.__initialized = true
            End If
        End Sub
        
        Protected Overrides ReadOnly Property AppRelativeTemplateSourceDirectory() As String
            Get
                Return "/D:/WinCVS/ThimeXMLORDER/App_Themes/MyTheme/"
            End Get
        End Property
        
        Protected Overrides ReadOnly Property ControlSkins() As System.Collections.IDictionary
            Get
                Return Me.__controlSkins
            End Get
        End Property
        
        Protected Overrides ReadOnly Property LinkedStyleSheets() As String()
            Get
                Return Me.__linkedStyleSheets
            End Get
        End Property
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Private Function __BuildControl__control2(ByVal ctrl As System.Web.UI.Control) As System.Web.UI.Control
            Dim __ctrl As System.Web.UI.WebControls.Label = CType(ctrl,System.Web.UI.WebControls.Label)
            
            #ExternalSource("D:\WinCVS\ThimeXMLORDER\App_Themes\MyTheme\Size.skin",17)
            __ctrl.Font.Bold = true
            
            #End ExternalSource
            
            #ExternalSource("D:\WinCVS\ThimeXMLORDER\App_Themes\MyTheme\Size.skin",17)
            __ctrl.Height = New System.Web.UI.WebControls.Unit(20R, Global.System.Web.UI.WebControls.UnitType.Pixel)
            
            #End ExternalSource
            
            #ExternalSource("D:\WinCVS\ThimeXMLORDER\App_Themes\MyTheme\Size.skin",17)
            __ctrl.Width = New System.Web.UI.WebControls.Unit(75R, Global.System.Web.UI.WebControls.UnitType.Pixel)
            
            #End ExternalSource
            Return __ctrl
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Private Function __BuildControl__control3(ByVal ctrl As System.Web.UI.Control) As System.Web.UI.Control
            Dim __ctrl As System.Web.UI.WebControls.DropDownList = CType(ctrl,System.Web.UI.WebControls.DropDownList)
            
            #ExternalSource("D:\WinCVS\ThimeXMLORDER\App_Themes\MyTheme\Size.skin",18)
            CType(__ctrl,System.Web.UI.IAttributeAccessor).SetAttribute("style", "height: 20px; width: 100px")
            
            #End ExternalSource
            Return __ctrl
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Private Function __BuildControl__control4(ByVal ctrl As System.Web.UI.Control) As System.Web.UI.Control
            Dim __ctrl As System.Web.UI.WebControls.TextBox = CType(ctrl,System.Web.UI.WebControls.TextBox)
            
            #ExternalSource("D:\WinCVS\ThimeXMLORDER\App_Themes\MyTheme\Size.skin",19)
            __ctrl.BorderStyle = Global.System.Web.UI.WebControls.BorderStyle.Groove
            
            #End ExternalSource
            
            #ExternalSource("D:\WinCVS\ThimeXMLORDER\App_Themes\MyTheme\Size.skin",19)
            __ctrl.Height = New System.Web.UI.WebControls.Unit(15R, Global.System.Web.UI.WebControls.UnitType.Pixel)
            
            #End ExternalSource
            
            #ExternalSource("D:\WinCVS\ThimeXMLORDER\App_Themes\MyTheme\Size.skin",19)
            __ctrl.Width = New System.Web.UI.WebControls.Unit(100R, Global.System.Web.UI.WebControls.UnitType.Pixel)
            
            #End ExternalSource
            
            #ExternalSource("D:\WinCVS\ThimeXMLORDER\App_Themes\MyTheme\Size.skin",19)
            __ctrl.Font.Size = New System.Web.UI.WebControls.FontUnit(New System.Web.UI.WebControls.Unit(8R, Global.System.Web.UI.WebControls.UnitType.Point))
            
            #End ExternalSource
            
            #ExternalSource("D:\WinCVS\ThimeXMLORDER\App_Themes\MyTheme\Size.skin",19)
            __ctrl.Font.Names = New String() {"Verdana"}
            
            #End ExternalSource
            Return __ctrl
        End Function
    End Class
End Namespace
