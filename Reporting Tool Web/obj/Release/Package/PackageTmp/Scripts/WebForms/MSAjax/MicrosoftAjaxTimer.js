//CdnPath=http://ajax.aspnetcdn.com/ajax/4.5.1/1/MicrosoftAjaxTimer.js
//----------------------------------------------------------
// Copyright (C) Microsoft Corporation. All rights reserved.
//----------------------------------------------------------
// MicrosoftAjaxTimer.js
Type._registerScript("Timer.js",["MicrosoftAjaxComponentModel.js"]);Sys.UI._Timer=function(a){Sys.UI._Timer.initializeBase(this,[a]);this._interval=60000;this._enabled=true;this._postbackPending=false;this._raiseTickDelegate=null;this._endRequestHandlerDelegate=null;this._timer=null;this._pageRequestManager=null;this._uniqueID=null};Sys.UI._Timer.prototype={get_enabled:function(){return this._enabled},set_enabled:function(a){this._enabled=a},get_interval:function(){return this._interval},set_interval:function(a){this._interval=a},get_uniqueID:function(){return this._uniqueID},set_uniqueID:function(a){this._uniqueID=a},dispose:function(){this._stopTimer();if(this._pageRequestManager!==null)this._pageRequestManager.remove_endRequest(this._endRequestHandlerDelegate);Sys.UI._Timer.callBaseMethod(this,"dispose")},_doPostback:function(){__doPostBack(this.get_uniqueID(),"")},_handleEndRequest:function(c,b){var a=b.get_dataItems()[this.get_id()];if(a)this._update(a[0],a[1]);if(this._postbackPending===true&&this._pageRequestManager!==null&&this._pageRequestManager.get_isInAsyncPostBack()===false){this._postbackPending=false;this._doPostback()}},initialize:function(){Sys.UI._Timer.callBaseMethod(this,"initialize");this._raiseTickDelegate=Function.createDelegate(this,this._raiseTick);this._endRequestHandlerDelegate=Function.createDelegate(this,this._handleEndRequest);if(Sys.WebForms&&Sys.WebForms.PageRequestManager)this._pageRequestManager=Sys.WebForms.PageRequestManager.getInstance();if(this._pageRequestManager!==null)this._pageRequestManager.add_endRequest(this._endRequestHandlerDelegate);if(this.get_enabled())this._startTimer()},_raiseTick:function(){this._startTimer();if(this._pageRequestManager===null||!this._pageRequestManager.get_isInAsyncPostBack()){this._doPostback();this._postbackPending=false}else this._postbackPending=true},_startTimer:function(){this._timer=window.setTimeout(Function.createDelegate(this,this._raiseTick),this.get_interval())},_stopTimer:function(){if(this._timer!==null){window.clearTimeout(this._timer);this._timer=null}},_update:function(c,b){var a=!this.get_enabled(),d=this.get_interval()!==b;if(!a&&(!c||d)){this._stopTimer();a=true}this.set_enabled(c);this.set_interval(b);if(this.get_enabled()&&a)this._startTimer()}};Sys.UI._Timer.registerClass("Sys.UI._Timer",Sys.UI.Control);