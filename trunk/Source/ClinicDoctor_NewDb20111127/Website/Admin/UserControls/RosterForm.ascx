<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RosterForm.ascx.cs" Inherits="Admin_UserControls_RosterForm" %>
<style type="text/css">
    .schedulerForm
    {
        position: absolute;
        top: 100px;
        left: 200px;
        z-index: 10001;
        display: none;
        font-family: Arial, Sans-Serif;
        font-size: 13px;
        background-color: #d6e5f4;
        padding: 10px;
    }
    .schedulerForm input[type="text"], #RosterForm textarea
    {
        font-family: Arial, Sans-Serif;
        font-size: 13px;
        margin-bottom: 5px;
        display: block;
        padding: 4px;
        border: solid 1px #85b1de;
        width: 300px;
        background-color: #EDF2F7;
    }
    .schedulerForm input[type="text"].activeField, #RosterForm textarea.activeField
    {
        background-image: none;
        background-color: #ffffff;
        border: solid 1px #33677F;
    }
    .schedulerForm input[type="text"].idle, #RosterForm textarea.idle
    {
        border: solid 1px #85b1de;
        background-color: #eeeeee;
        background-repeat: repeat-x;
        background-position: top;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $("input, textarea").addClass("idle");
        $("input, textarea").focus(function () {
            $(this).addClass("activeField").removeClass("idle");
        }).blur(function () {
            $(this).removeClass("activeField").addClass("idle");
        });
    });
</script>
<div style="visibility: visible; height: 214px; top: 42px; left: 309px;" id="RosterForm">
    <label for="txtName">
        Name</label>
    <input id="Text16" type="text" />
    <label for="txtEmail">
        Email</label>
    <input id="Text17" type="text" />
    <label for="txtWebsite">
        Website</label>
    <input id="Text18" type="text" />
    <label for="txtComment">
        Comment</label>
    <textarea id="Textarea6" rows="4" cols="30"></textarea></div>
