﻿// Load calendar for Schedule
function show_minical() {
    if (scheduler.isCalendarVisible())
        scheduler.destroyCalendar();
    else
        scheduler.renderCalendar({
            position: "dhx_minical_icon",
            date: scheduler._date,
            navigation: true,
            handler: function(date, calendar) {
                scheduler.setCurrentView(date);
                scheduler.destroyCalendar()
            }
        });
}


// Init Schedule
function initSchedule(weekday) {
    var _date = new Date();
    var _mode = "timeline";

    scheduler.locale.labels.timeline_tab = "Timeline";
    scheduler.config.xml_date = "%Y-%m-%d %H:%i";
    scheduler.config.details_on_dblclick = true;
    scheduler.config.details_on_create = true;
    scheduler.config.time_step = stepTime;

    $.ajax({
        type: "POST",
        url: "RosterIframe.aspx/GetDoctorTree",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = eval(response.d)[0];

            if (obj.result == "true") {
                var elements = obj.data;
                if (elements) {
                    scheduler.createTimelineView({
                        section_autoheight: false,
                        name: "timeline",
                        x_unit: "hour",
                        x_date: "%m/%d",
                        x_step: 12,
                        x_size: 7,
                        x_start: 0,
                        x_length: 48,
                        y_unit: elements,
                        y_property: "section_id",
                        render: "tree",
                        folder_dy: 20,
                        dy: 60
                    });

                    scheduler.init('scheduler_here', _date, _mode);

                    function block_readonly(id) {
                        if (!id) return true;
                        return !this.getEvent(id).readonly;
                    }
                    scheduler.attachEvent("onBeforeDrag", block_readonly)
                    scheduler.attachEvent("onClick", block_readonly)

                    // When event changed, check and update
                    scheduler.attachEvent("onBeforeEventChanged", function(event_object, native_event, is_new) {
//                        var evs = scheduler.getEvents(event_object.start_date, event_object.end_date);
//                        if (evs) {
//                            $.each(evs, function(i, item) {
//                                if (item.section_id == event_object.section_id && item.id != event_object.id) {
//                                    alert("You can not create roster on another existed roster.");
//                                    return false;
//                                }
//                            });
//                        }

                        // If update roster
                        if (is_new == false) {
                            if (event_object.start_date <= new Date()) {
                                alert("You can not change roster to passed or current date.");
                                return false;
                            }
                        }

                        return true;
                    });

                    // Save roster when resized or moved
                    scheduler.attachEvent("onEventChanged", function(event_id, event_object) {
                        //any custom logic here
                        var _id = event_object.id;
                        var _rosterType = event_object.RosterTypeId;
                        var _fromTime = event_object.start_date;
                        var _toTime = event_object.end_date;
                        var _note = event_object.note;
                        var _staffId = event_object.section_id;

                        // Update roster
                        $("#dialog-modal").show();
                        disableAllElements($("#tblContent"), false)

                        UpdateRoster(_id, _staffId, _rosterType, _fromTime, _toTime, _note);
                    });

                    // When view changed, reload roster
                    //    scheduler.attachEvent("onViewChange", function(mode, date) {
                    //any custom logic here
                    //        LoadRoster(mode, date);
                    //    });

                    // Load roster
                    LoadRoster(_mode, _date);
                }
            }
            else {
                alert(obj.message);
            }
        },
        fail: function() {
            alert("Unknow error!");
        },
        complete: function() {
        }
    });
}

scheduler.showLightbox = function(id) {
    initForm();

    var ev = scheduler.getEvent(id);

    if (ev.start_date <= new Date()) {
        if (ev.isnew == "false") {
            alert("You cannot change a passed roster.");
        }
        else {
            scheduler.deleteEvent(id);
            alert("You have to create new roster in the day after current date");
        }
        return false;
    }

    setTime(ev);

    // Set init value
    $("#hdId").val(id);
    $("#txtFromDate").datepicker("setDate", ev.start_date);
    $("#txtToDate").datepicker("setDate", ev.end_date);
    var _currentMonth = new Date();
    var _nextMonth = new Date(_currentMonth.getFullYear(), _currentMonth.getMonth() + 1, 1);
    $("#txtMonth").datepicker("setDate", _nextMonth);
    $("#txtNote").val(ev.note);
    $("#hdStaff").val("");
    
    // Staff id
    if (ev.section_id) {
        $("#hdStaff").val(ev.section_id);
        $("#cboStaff").val(ev.section_id);
    }

    if (ev.isnew == "false") {
        $("#trRepeat").hide();
        $("#tdTitle").text("Edit roster");

        if (ev.RosterTypeId) {
            $("#cboRosterType").val(ev.RosterTypeId);
        }
    }

    scheduler.startLightbox(id, html("RosterForm"));
}

function LoadRoster(mode, date) {
    var requestdata = JSON.stringify({ Mode: mode, CurrentDateView: date });
    $.ajax({
        type: "POST",
        url: "RosterIframe.aspx/LoadRoster",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = eval(response.d)[0];

            if (obj.result == "true") {
                var evs = obj.data;
                if (evs) {
                    addRoster(evs);
                }
            }
            else {
                alert(obj.message);
            }
        },
        fail: function() {
            alert("Unknow error!");
        },
        complete: function() {
        }
    });
}

function loadRosterType() {
    // Load data
    $.ajax({
        type: "POST",
        url: "RosterIframe.aspx/GetRosterType",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {
            var rosterType = eval(msg.d);
            $("#cboRosterType").find("option").remove();

            $.each(rosterType, function(i, item) {
                $("#cboRosterType").append('<option value="' + item.key + '">' + item.label + '</option>');
            });

            //            $("#cboRosterType").fadeIn();
//            $("#cboRosterType").focus();
        },
        fail: function() {
            alert("Cannot load Roster Type data. Please try again or contact Administrator.");
            scheduler.endLightbox(false, html("RosterForm"));
        },
        complete: function() {
            //            $("#loadingRosterType").fadeOut();
        }
    });
}

function setTime(ev) {
    var startTime = ev.start_date.format("HH:MM");
    var endTime = ev.end_date.format("HH:MM");
    $("#cboFromHour").val(startTime + ":00");
    $("#cboToHour").val(endTime + ":00");
}

function loadHour() {
    $.ajax({
        type: "POST",
        url: "RosterIframe.aspx/GetTime",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {
            var arr = eval(msg.d);
            $("#cboFromHour").find("option").remove();
            $("#cboToHour").find("option").remove();

            $.each(arr, function(i, item) {
                $("#cboFromHour").append('<option value="' + item.key + '">' + item.label + '</option>');
                $("#cboToHour").append('<option value="' + item.key + '">' + item.label + '</option>');
            });

            //            $("#cboFromHour").fadeIn();
            //            $("#loadingFromHour").fadeOut();
            //            $("#cboToHour").fadeIn();
            //            $("#loadingToHour").fadeOut();
        },
        fail: function() {
            alert("Cannot load Hour data. Please try again or contact Administrator.");
            scheduler.endLightbox(false, html("RosterForm"));
        },
        complete: function() {
            //            $("#loadingFromHour").fadeOut();
            //            $("#loadingToHour").fadeOut();
        }
    });
}

function loadGroup() {
    $.ajax({
        type: "POST",
        url: "RosterIframe.aspx/GetRoles",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            var obj = eval(response.d)[0];
            if (obj.result == "true") {
                var arr = eval(obj.data);
                $("#cboGroup").find("option:not(.default)").remove();

                $.each(arr, function(i, item) {
                    $("#cboGroup").append('<option value="' + item.key + '">' + item.label + '</option>');
                });

                loadStaff($("#cboGroup").val());
            }
            else {
                alert(obj.message);
            }
        },
        fail: function() {
            alert("Cannot load Group data. Please try again or contact Administrator.");
            scheduler.endLightbox(false, html("RosterForm"));
        },
        complete: function() {
        }
    });
}

function loadStaff(_role) {
    var requestdata = JSON.stringify({ Role: _role});
    $("#cboStaff").hide();
    $("#loadingStaff").show();
    $.ajax({
        type: "POST",
        url: "RosterIframe.aspx/GetStaffs",
        data: requestdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            var obj = eval(response.d)[0];
            if (obj.result == "true") {
                var arr = eval(obj.data);
                $("#cboStaff").find("option").remove();

                $.each(arr, function(i, item) {
                    $("#cboStaff").append('<option value="' + item.key + '">' + item.label + '</option>');
                });
                
                $("#cboStaff").val($("#hdStaff").val());

                $("#cboStaff").show();
            }
            else {
                alert(obj.message);
            }
        },
        fail: function() {
            alert("Cannot load Group data. Please try again or contact Administrator.");
            scheduler.endLightbox(false, html("RosterForm"));
        },
        complete: function() {
            $("#loadingStaff").hide();
        }
    });
}

function initForm() {
    $("#dialog-modal").hide();

    $("#cboRosterType").show();
    $("#loadingRosterType").hide();

    $("#divWeekday").hide();
    $("#spanMonth").hide();

    $("#cboFromHour").show();
    $("#loadingFromHour").hide();
    $('#spanFromDate').show();

    $("#cboToHour").show();
    $("#loadingToHour").hide();
    $('#spanToDate').show();

    $("#trRepeat").show();

    // Set empty
    $("#chkRepeat").attr("checked", false);
    $("#hdId").val("");
    $("#tdTitle").text("New roster");
    $("#txtNote").val("");

    loadStaff('-1');
    $("#cboGroup").focus();
    $($('#cboGroup option').get(0)).attr('selected', 'selected');

    disableAllElements($("#tblContent"), true)
}

function SaveRoster() {
    // Validate date time
    if ($("#chkRepeat").attr("checked")) {
        if ($("#cboFromHour").val() >= $("#cboToHour").val()) {
            alert("From time must be less than to time");
            return;
        }
    }
    else {
        var _validateFromTime = $("#txtFromDate").datepicker("getDate").format("yyyy/mm/dd ") + $("#cboFromHour").val();
        var _validateToTime = $("#txtToDate").datepicker("getDate").format("yyyy/mm/dd ") + $("#cboToHour").val();
        if (_validateFromTime >= _validateToTime) {
            alert("From date, time must be less than to date, time");
            return;
        }
    }

    var _id = $("#hdId").val();
    var ev = scheduler.getEvent(_id);
    var _rosterType = $("#cboRosterType").val();
    var _fromTime = $("#txtFromDate").val() + " " + $("#cboFromHour").val();
    var _toTime = $("#txtToDate").val() + " " + $("#cboToHour").val();
    var _note = $("#txtNote").val();
    var _staffId = $("#cboStaff").val();

    // Check if staff is null
    if (_staffId == null) {
        alert("You must choose staff.");
        return;
    }
    
    if (ev.isnew == "false") {
        // Update roster
        $("#dialog-modal").show();
        disableAllElements($("#tblContent"), false)

        UpdateRoster(_id, _staffId, _rosterType, _fromTime, _toTime, _note);
    }
    else {
        // Add new roster
        var _repeat = 'false';
        var _weekday = '';
        var _month = '';

        if ($("#chkRepeat").attr("checked")) {
            var allVals = [];
            $("input[type=checkbox]:checked", "#spanWeekday").each(function() {
                allVals.push($(this).val());
            });

            if (allVals.length == 0) {
                alert("You have to choose a weekday at least.");
                return;
            }

            _repeat = 'true';
            _weekday = allVals.join(";");
            _month = $("#txtMonth").val();
        }

        $("#dialog-modal").show();
        disableAllElements($("#tblContent"), false)

        var requestdata = JSON.stringify({ StaffId: _staffId, RosterType: _rosterType, StartTime: _fromTime, EndTime: _toTime, Note: _note, RepeatRoster: _repeat, Weekday: _weekday, Month: _month });
        $.ajax({
            type: "POST",
            url: "RosterIframe.aspx/SaveEvent",
            data: requestdata,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(response) {
                var obj = eval(response.d)[0];
                if (obj.result == "true") {
                    var evs = obj.data;
                    addRoster(evs);
                    scheduler.endLightbox(false, html("RosterForm"));
                }
                else {
                    alert(obj.message);
                }
            },
            fail: function() {
                alert("Unknow error!");
            },
            complete: function() {
                disableAllElements($("#tblContent"), true)
                $("#dialog-modal").hide();
            }
        });
    }
}

function UpdateRoster(_id, _staffId, _rosterType, _fromTime, _toTime, _note) {
    var requestdata = JSON.stringify({ Id: _id, StaffId: _staffId, RosterType: _rosterType, StartTime: _fromTime, EndTime: _toTime, Note: _note });
    $.ajax({
        type: "POST",
        url: "RosterIframe.aspx/UpdateEvent",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = eval(response.d)[0];
            if (obj.result == "true") {
                var evs = obj.data;
                scheduler.deleteEvent(_id)
                addRoster(evs);
                scheduler.endLightbox(false, html("RosterForm"));
            }
            else {
                alert(obj.message);
            }
        },
        fail: function() {
            alert("Unknow error!");
        },
        complete: function() {
            disableAllElements($("#tblContent"), true)
            $("#dialog-modal").hide();
        }
    });
}

function addRoster(evs) {
    $.each(evs, function(i, item) {
        scheduler.addEvent(item);
    });
}

function CancelRoster() {
    scheduler.endLightbox(false, html("RosterForm"));
}

function DeleteRoster() {
    if (!confirm("Do you want to delete this roster?"))
        return false;
        
    var ev = scheduler.getEvent($("#hdId").val());
    if (ev.isnew == "false") {
        var requestdata = JSON.stringify({ Id: ev.id });
        $.ajax({
            type: "POST",
            url: "RosterIframe.aspx/DeleteEvent",
            data: requestdata,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(response) {
                var obj = eval(response.d)[0];
                if (obj.result == "true") {
                    scheduler.deleteEvent(ev.id)
                    alert("Roster has been deleted.");
                }
                else {
                    alert(obj.message);
                }
            },
            fail: function() {
                alert("Unknow error!");
            },
            complete: function() {
            }
        });
    }
    scheduler.endLightbox(false, html("RosterForm"));
}

$(document).ready(function() {
    loadRosterType();
    loadHour();
    loadGroup();
    
    $("input, textarea").addClass("idle");
    $("input, textarea").focus(function() {
        $(this).addClass("activeField").removeClass("idle");
    }).blur(function() {
        $(this).removeClass("activeField").addClass("idle");
    });
    initSchedule(weekday);

    $(".datePicker").datepicker({
        showOn: "button",
        buttonImage: "../resources/scripts/codebase/imgs/calendar.gif",
        buttonImageOnly: true, changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        minDate: "+1D"
    });

    var _currentMonth = new Date();
    var _nextMonth = new Date(_currentMonth.getFullYear(), _currentMonth.getMonth() + 1, 1);
    $("#txtMonth").datepicker({
        showOn: "button",
        buttonImage: "../resources/scripts/codebase/imgs/calendar.gif",
        buttonImageOnly: true, changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        dateFormat: "mm/yy",
        minDate: _nextMonth
    });

    $("#chkRepeat").click(function() {
        $('#divWeekday').toggle($(this).attr('checked'));
        $('#spanMonth').toggle($(this).attr('checked'));

        $('#spanFromDate').toggle(!$(this).attr('checked'));
        $('#spanToDate').toggle(!$(this).attr('checked'));

        $("#spanWeekday").find("span").remove();
        $.each(weekday, function(i, item) {
            $("#spanWeekday").append('<span style="float:left; padding-right:5px; width:95px;"><input type="checkbox" value=' + item.label
            + ' id="chk' + item.key + '" /> ' + item.label + '</span>');
        });
    });

    $("#cboGroup").change(function() {
        loadStaff($(this).val());
    });

    $("#btnSave").click(function() {
        SaveRoster();
    });

    $("#btnCancel").click(function() {
        CancelRoster();
    });

    $("#btnDelete").click(function() {
        DeleteRoster();
    });

    $("#btnDelete").keydown(function(e) {
        var key;

        if (window.event)
            key = window.event.keyCode;     //IE
        else
            key = e.which;     //firefox

        if (key == 9) {
            $("#cboGroup").focus();
            return false;
        }
    });

});

function disableAllElements(obj, disabled) {
    if (disabled) {
        $("input, textarea, select", obj).removeAttr('disabled');
    }
    else {
        $("input, textarea, select", obj).attr('disabled', true);
    }
}