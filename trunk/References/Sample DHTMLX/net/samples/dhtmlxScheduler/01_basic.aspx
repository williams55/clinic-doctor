<%@ Page Language="C#" MasterPageFile="~/dhtmlxScheduler/scheduler.Master" %>

<asp:Content ContentPlaceHolderID="SchedulerConector" ID="Content" runat="server">
<script>
    function init() {
        scheduler.config.xml_date = "%Y-%m-%d %H:%i";
        scheduler.config.lightbox.sections = [
		    { name: "description", height: 130, map_to: "text", type: "textarea", focus: true },
		    { name: "location", height: 43, type: "textarea", map_to: "details" },
		    { name: "Tags", height: 30, type: "textarea", map_to: "Tags" },
		    { name: "time", height: 72, type: "time", map_to: "auto" }
	    ]
        scheduler.config.first_hour = 4;
        scheduler.locale.labels.section_location = "Location";
        scheduler.locale.labels.section_Tags = "Tags";
        scheduler.config.details_on_create = true;
        scheduler.config.details_on_dblclick = true;


        scheduler.init('scheduler_here', null, "month");
        scheduler.setLoadMode("month");

        scheduler.load("schedulerConnector.ashx" + "?uid=" + scheduler.uid());
        var dp = new dataProcessor("schedulerConnector.ashx" + "?uid=" + scheduler.uid());

        dp.init(scheduler);
    }
</script>
</asp:Content>