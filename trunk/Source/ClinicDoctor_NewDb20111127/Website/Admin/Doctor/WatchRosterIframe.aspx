<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WatchRosterIframe.aspx.cs"
    Inherits="Admin_Doctor_WatchRosterIframe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Admin/resources/css/ui-lightness/jquery-ui-1.7.3.custom.css") %>" />
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/jquery-1.6.2.min.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/ui/jquery.ui.core.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/ui/jquery.ui.widget.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/ui/jquery.ui.dialog.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/ui/jquery.ui.datepicker.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/date.format.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/json2.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/codebase/dhtmlxscheduler.js") %>"
        type="text/javascript" charset="utf-8"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/codebase/ext/dhtmlxscheduler_readonly.js") %>"
        type="text/javascript" charset="utf-8"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/codebase/ext/dhtmlxscheduler_minical.js") %>"
        type="text/javascript" charset="utf-8"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/codebase/ext/dhtmlxscheduler_tooltip.js") %>"
        type="text/javascript" charset="utf-8"></script>
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/codebase/dhtmlxscheduler.css") %>"
        type="text/css" media="screen" title="no title" charset="utf-8" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Admin/myscript/css/scheduler.css") %>"
        type="text/css" media="screen" title="no title" charset="utf-8" />
    <style type="text/css" media="screen">
        html, body
        {
            margin: 0;
            padding: 0;
            height: 100%;
            overflow: hidden;
            font-size: 13px;
        }
        .one_line
        {
            white-space: nowrap;
            overflow: hidden;
            padding-top: 5px;
            padding-left: 5px;
            text-align: left !important;
        }
    </style>
    <script type="text/javascript" charset="utf-8">
        // Load weekday
        var weekday = <%=Constants.Weekdays %>;
        var stepTime = <%=ClinicDoctor.Settings.BusinessLayer.ServiceFacade.SettingsHelper.MinuteStep %>;
        var html = function (id) { return document.getElementById(id); }; //just a helper

    </script>
    <script src="DrRS.js" type="text/javascript"></script>
</head>
<body>
    <div id="scheduler_here" class="dhx_cal_container" style='width: 100%; height: 100%;'>
        <div class="dhx_cal_navline">
            <div class="dhx_cal_prev_button">
                &nbsp;</div>
            <div class="dhx_cal_next_button">
                &nbsp;</div>
            <div class="dhx_cal_today_button">
            </div>
            <div class="dhx_cal_date">
            </div>
            <div class="dhx_minical_icon" id="dhx_minical_icon" onclick=" ShowMinical()">
                &nbsp;</div>
            <div class="dhx_cal_tab" name="day_tab" style="right: 204px;">
            </div>
            <div class="dhx_cal_tab" name="week_tab" style="right: 140px;">
            </div>
            <div class="dhx_cal_tab" name="month_tab" style="right: 76px;">
            </div>
        </div>
        <div class="dhx_cal_header">
        </div>
        <div class="dhx_cal_data">
        </div>
    </div>
</body>
</html>
