// Ten bien chua id cac form
var formId = "form-dhtmlx";

var blStaff = false;
var CurrentRoster;

// Danh sach cac timespan cu
var listTs = [];

// True if lightbox is using, do not refesh page
var isLightbox = false;

// Dung de danh dau la dang chay de khong chay lai su kien view change
var isUsing = false;

/****************************DHTMLX - Start******************************/
// Load calendar for Schedule
function ShowMinical() {
    if (scheduler.isCalendarVisible())
        scheduler.destroyCalendar();
    else
        scheduler.renderCalendar({
            position: "dhx_minical_icon",
            date: scheduler._date,
            navigation: true,
            handler: function(date, calendar) {
                scheduler.setCurrentView(date);
                scheduler.destroyCalendar();
            }
        });
}

// Init Schedule
function initSchedule(weekday) {
    var currentDate = new Date();
    var currentMode = "timeline";

    scheduler.locale.labels.timeline_tab = "Timeline";
    scheduler.config.xml_date = "%Y-%m-%d %H:%i";
    scheduler.config.details_on_dblclick = true;
    scheduler.config.details_on_create = true;
    scheduler.config.time_step = stepTime;
    scheduler.config.mark_now = false;
    scheduler.xy.min_event_height = 0;
    scheduler.config.multi_day = true; // rest of multiday events would be displayed at the top
    
    // Them nut chuc nang dua vao quyen
    scheduler.config.icons_select = [];
    if ($('[id$=divUpdate]').length) {
        scheduler.config.icons_select.push('icon_edit');
    }
    if ($('[id$=divDelete]').length) {
        scheduler.config.icons_select.push('icon_delete');
    }

    // Thay the title cua event
    scheduler.renderEvent = function(container, event) {
        var bg_color = (event.color ? ("background:" + event.color + ";") : "background: #1796B0");
        var color = (event.textColor ? ("color:" + event.textColor + ";") : "");
        var w = parseInt(container.style.width);
        var h = parseInt(container.style.height);
        var bottom = container.className.indexOf('dhx_cal_select_menu') > 0;

        var inner_html = '<div class="dhx_event_move" style="cursor: pointer; width:' + (w - (this._quirks ? 4 : 14)) 
            // Chieu cao giam di de cho border
            + 'px; height:' + (h - 3) + 'px;' + bg_color + '' + color + '">' +
            '<div style="padding: 5px; text-align: center; font-weight: bold;">' +
            (event.DoctorShortName ? event.DoctorShortName + '' : '') +
            (event.RosterTypeTitle ? ' - '  + event.RosterTypeTitle + '<br />' : '') + 
            //scheduler.templates.event_date(event.start_date) + " - " + scheduler.templates.event_date(event.end_date) +
            '</div></div>'; // +2 css specific, moved from render_event
        var footer_class = "dhx_event_resize";
        if (bottom)
            footer_class = "dhx_resize_denied";

        inner_html += '<div class="' + footer_class + '" style=" width:' + (w - 14) + 'px;' + (bottom ? ' margin-top:-1px;' : '') + '' + bg_color + '' + color + '" ></div>';

        container.innerHTML = inner_html;
        return true; // required, true - we've created custom form; false - display default one instead
    };
    
    // Thay tooltip
    scheduler.templates.tooltip_date_format = scheduler.date.date_to_str("%H:%i");
    scheduler.templates.tooltip_text = function(start, end, event) {
        return (event.DoctorShortName ? '<b>Doctor:</b> '  + event.DoctorShortName + '<br/>' : '') + 
            (event.RosterTypeTitle ? '<b>Type:</b> '  + event.RosterTypeTitle + '<br/>' : '') + 
            "<b>Time:</b> " + scheduler.templates.tooltip_date_format(start) + " - " + scheduler.templates.tooltip_date_format(end);
    };
    
    // Thay doi su kien nut click
    scheduler._click.buttons = {
        "delete": function(id) {
            DeleteRoster(id);
        },
        edit: function(id) { scheduler.showLightbox(id); }
    };

    // Set lai chieu cao, dinh dang cot time
    var step = stepTime;
    var format = scheduler.date.date_to_str("%H:%i");
    scheduler.config.hour_size_px = (60 / step) * 22;
    scheduler.templates.hour_scale = function(date) {
        var htmltime = "";
        for (var i = 0; i < 60 / step; i++) {
            htmltime += "<div style='height:21px;line-height:21px;'>" + format(date) + "</div>";
            date = scheduler.date.add(date, step, "minute");
        }
        return htmltime;
    };

    scheduler.createTimelineView({
        section_autoheight: false,
        name: "timeline",
        x_unit: "minute",
        x_date: "%H:%i",
        x_step: 60,
        x_size: 24,
        x_start: 0,
        x_length: 24,
        y_unit: scheduler.serverList("timeline", []),
        y_property: "section_id",
        render: "tree",
        dx: 150,
        folder_dy: 20,
        dy: 40
    });


    scheduler.attachEvent("onBeforeDrag", BeforeDrag);
    scheduler.attachEvent("onClick", BlockReadonly);
    scheduler.attachEvent("onEventChanged", EventChanged);

    scheduler.attachEvent("onViewChange", CallChangeView);

    scheduler.init('scheduler_here', currentDate, currentMode);

    CallDoctorTree();

    // Refesh current view for mark_now can auto update
    setInterval(function() {
        if (!isLightbox) {
            scheduler.setCurrentView();
        }
    }, 60000);
}
/****************************DHTMLX - End******************************/

/****************************Scheduler - Start******************************/
function CallDoctorTree() {
    if (scheduler._mode == 'timeline') {
        var trees = $.grep(listTree, function (e) { return e.key == cboSelector.GetValue(); });
        if (trees.length > 0) {
            scheduler.updateCollection("timeline", trees[0].list);
        }
    }
    setTimeout(function() {
        CallChangeView();
    }, 200);
}

function CallChangeView(mode, date) {
    if (!mode) mode = scheduler._mode;
    if (!date) date = scheduler._min_date;
    if (!isUsing && !isLightbox) {
        // Lam cai load roster cua doctor,
        // neu doctor co roster thi hien thi
        // khi hien thi, dua vao thoi gian cua roster ma to mau
        isUsing = true;

        // Clone list cu ra
        var tmpListTs = listTs.slice();
        listTs = [];

        // Danh sach chua cac list ajax
        var arrAjax = [];
        LoadRoster(arrAjax, mode, date);
        BlockTimespan(scheduler, date, mode, listTs);
        DeleteTimespan(scheduler, tmpListTs);

        $.when.apply($, arrAjax).done(function() {
            scheduler.updateView();
            isUsing = false;
        });
    }
}

// Set readonly property for event
function BlockReadonly(id) {
    if (!id) return true;
    return !scheduler.getEvent(id).readonly;
}

// Function will be raise when event changed
function BeforeDrag(id) {
    // Get event and clone it before drag, it's useful in case failure drag
    CurrentRoster = $.extend(true, {}, scheduler.getEvent(id));
    return BlockReadonly(id);
}

// Function will be raise when event changed
function EventChanged(id, event) {
    // Case move event
    //if (scheduler._drag_mode && scheduler._drag_mode == 'move') {
    MoveRoster(id, event);
    //}
}

scheduler.showLightbox = function(id) {
    isLightbox = true;
    InitForm();
    var ev = scheduler.getEvent(id);

    // Set init value
    $("#hdId").val(id);
    startDate.SetDate(ev.start_date);
    endDate.SetDate(ev.end_date);
    startTime.SetDate(ev.start_date);
    endTime.SetDate(ev.end_date);
    $("#txtNote").val(ev.note);

    // Set doctor
    if (ev.section_id) {
        cboDoctor.SetText(ev.section_id);
    }

    // If it's edit mode
    if (ev.isnew == false) {
        $('#drag-title .dhx_time').text('Edit roster ' + id);
        $("#repeater-section, [id$=divSave]").hide();
        $("[id$=divUpdate], [id$=divDelete]").show();

//        if (ev.RosterTypeId) {
//            cboRosterType.SetSelectedItem(cboRosterType.FindItemByValue(ev.RosterTypeId));
//        }
    }
    else {
        $('#drag-title .dhx_time').text('New roster');
        $("#repeater-section, [id$=divSave]").show();
        $("[id$=divUpdate], [id$=divDelete]").hide();
    }

    var d = html("drag-title");
    d.onmousedown = scheduler._ready_to_dnd;
    d.onselectstart = function() { return false; };
    d.style.cursor = "pointer";
    scheduler._init_dnd_events();
    scheduler.startLightbox(id, html(formId));

    // Goi ham validate form khi form hien thi
    ValidateForm();

    // Neu event la readonly thi disable cac control
    if (ev.ReadOnly) {
        $("[id$=divSave], [id$=divUpdate]").hide();
        $("[id$=divDelete]").hide();
    }

    return true;
};
/****************************Scheduler - End******************************/

/****************************Roster - Start******************************/
// Load roster to scheduler
function LoadRoster(arrAjax, mode, date) {
    var requestdata = JSON.stringify({ group: cboSelector.GetValue() , mode: mode, date: date });
    arrAjax.push($.ajax({
        type: "POST",
        url: "Default.aspx/LoadRoster",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                // Xoa cac roster hien tai
                scheduler.clearAll();
                var evs = obj.data;
                if (evs) {
                    if (mode == "month") {
                        AddTimespanMonth(evs);
                    } else {
                        AddRoster(evs);
                    }
                }
            }
            else {
                ShowMessage(obj.message);
            }
        },
        fail: function() {
            ShowMessage("Unknow error!");
        }
    }));
}

// Save a new roster
function NewRoster() {
    if (!ValidateForm()) {
        ShowMessage('Please input required fields first.');
        return;
    }

//    var rosterType = cboRosterType.GetValue();
    var fromTime = startTime.GetDate();
    var toTime = endTime.GetDate();
    var fromDate = startDate.GetDate();
    var toDate = endDate.GetDate();
    var note = $("#txtNote").val();
    var doctor = cboDoctor.GetValue();

    // Add new roster
    var repeat = false;
    var weekday = '';

    if ($("#chkRepeat").attr("checked")) {
        var allVals = [];
        $("[id^=chkWeekday]:checked").each(function() {
            allVals.push($(this).val());
        });

        if (allVals.length == 0) {
            ShowMessage('You must choose a weekday at least.');
            return;
        }

        repeat = true;
        weekday = allVals.join('');
    }

    var requestdata = JSON.stringify({
        doctorId: doctor,
//        rosterTypeId: rosterType,
        rosterTypeId: 0,
        startTime: fromTime,
        endTime: toTime,
        startDate: fromDate,
        endDate: toDate,
        note: note,
        repeatRoster: repeat,
        weekday: weekday
    });
    ShowProgress();
    $.ajax({
        type: "POST",
        url: "Default.aspx/NewRoster",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            CloseProgress();
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                var evs = obj.data;
                AddRoster(evs);
                CancelRoster();
            }
            else {
                ShowMessage(obj.message);
            }
        },
        fail: function() {
            CloseProgress();
            ShowMessage("Unknow error!");
        }
    });
}

// Save moved roster
function MoveRoster(eventId, objEvent) {
    var requestdata = JSON.stringify({
        id: objEvent.id,
        doctorId: objEvent.section_id,
//        rosterTypeId: objEvent.RosterTypeId,
        rosterTypeId: 0,
        startTime: objEvent.start_date,
        endTime: objEvent.end_date,
        note: objEvent.note,
    });
    $.ajax({
        type: "POST",
        url: "Default.aspx/MoveRoster",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = JSON.parse(response.d);

            if (obj.result != "true") {
                ShowMessage(obj.message);
                scheduler.deleteEvent(eventId);
                scheduler.addEvent(CurrentRoster);
            }
        },
        fail: function() {
            ShowMessage("Unknow error!");
            scheduler.deleteEvent(eventId);
            scheduler.addEvent(CurrentRoster);
        },
        complete: function() {
            CurrentRoster = null; // set null after use
        }
    });
}

// Update roster
function UpdateRoster() {
    if (!ValidateForm()) {
        ShowMessage('Please input required fields first.');
        return;
    }

    var id = $("#hdId").val();
    var requestdata = JSON.stringify({
        id: id,
        doctorId: cboDoctor.GetValue(),
//        rosterTypeId: cboRosterType.GetValue(),
        rosterTypeId: 0,
        startTime: startTime.GetDate(),
        endTime: endTime.GetDate(),
        startDate: startDate.GetDate(),
        endDate: endDate.GetDate(),
        note: $("#txtNote").val()
    });
    ShowProgress();
    $.ajax({
        type: "POST",
        url: "Default.aspx/UpdateRoster",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            CloseProgress();
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                var evs = obj.data;
                scheduler.deleteEvent(id);
                AddRoster(evs);
                CancelRoster();
            } else {
                ShowMessage(obj.message);
            }
        },
        fail: function() {
            CloseProgress();
            ShowMessage("Unknow error!");
        }
    });
}

// Add roster into scheduler
function AddRoster(evs) {
    $.each(evs, function(i, item) {
        item.start_date = new Date(item.start_date);
        item.end_date = new Date(item.end_date);
        scheduler.addEvent(item);
    });
}

// Add roster into scheduler
function AddTimespanMonth(evs) {
    $.each(evs, function(i, item) {
		var date = new Date(item.Date);
		var options = {
			start_date: date,
			end_date: scheduler.date.add(date, 1, "day"),
			type: "dhx_time_block", /* creating events on those dates will be disabled - dates are blocked */
			css: "have_roster",
			html: "Have Roster"
		};
        listTs.push(scheduler.addMarkedTimespan(options));
        
        if (date < new Date())
            listTs.push(scheduler.addMarkedTimespan({
                start_date: date,
                end_date: scheduler.date.add(date, 1, "day"),
                css: 'small_lines_section',
                type: "dhx_time_block"
            }));
   });
}

// Cancel roster
function CancelRoster() {
    scheduler.endLightbox(false, html(formId));
    scheduler._lightbox = null;
    isLightbox = false;
    CurrentRoster = null;
}

// Delete roster by Id
function DeleteRoster(ros) {
    if (!confirm("Do you want to delete this roster?"))
        return;

    var id = $("#hdId").val();
    if (ros) id = ros; // Neu co doi tuong roster truyen vao thi lay id cua no
    var requestdata = JSON.stringify({ id: id });
    ShowProgress();
    $.ajax({
        type: "POST",
        url: "Default.aspx/DeleteRoster",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = JSON.parse(response.d);
            CloseProgress();
            if (obj.result == "true") {
                scheduler.deleteEvent(id);
                CancelRoster();
            } else {
                ShowMessage(obj.message);
            }
        },
        fail: function() {
            CloseProgress();
            ShowMessage("Unknow error!");
        }
    });
}
/****************************Roster - End******************************/

/**************************** Form ******************************/
function InitForm() {
    // Set empty
    $("#hdId").val("");
    $("#txtNote").val("");
}

// Ham kiem tra form co valid khong
function ValidateForm() {
    cboDoctor.Validate();
//    cboRosterType.Validate();
    startTime.Validate();
    startDate.Validate();
    endTime.Validate();
    endDate.Validate();
    return cboDoctor.isValid /*&& cboRosterType.isValid*/ && startTime.isValid && startDate.isValid && endTime.isValid && endDate.isValid;
}

$(document).ready(function() {
    initSchedule(weekday);

    $(".datePicker").datepicker({
        showOn: "button",
        buttonImage: "../../resources/css/ui-lightness/images/calendar.gif",
        buttonImageOnly: true, changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        selectOtherMonths: true
    });

    // Khoi tao su kien click tren repeater
    $("#btnRepeat").click(function() {
        var $container = $(this).parents('.dhx_wrap_section:first');
        $('.dhx_cal_ltext', $container).toggle();
        if ($(':last-child', this).html().indexOf('Disabled') != -1) {
            $(':last-child', this).html('Enabled');
            $('.dhx_custom_button_recurring', this).addClass('dhx_custom_button_recurring_enabled');
            $("#chkRepeat").attr('checked', 'checked');
        } else {
            $(':last-child', this).html('Disabled');
            $('.dhx_custom_button_recurring', this).removeClass('dhx_custom_button_recurring_enabled');
            $("#chkRepeat").removeAttr('checked');
        }
    });
});
