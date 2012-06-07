var blHour = false;
var blStaff = false;
var blPatient = false;
var _current_id = "-1";
var miniCalendar;

// Load calendar for Schedule
function show_minical() {
    miniCalendar = scheduler.renderCalendar({
        container: "datepicker",
        date: scheduler._date,
        navigation: true,
        handler: function(date, calendar) {
            scheduler.setCurrentView(date);
        }
    });
}


// Init Schedule
function initSchedule(weekday) {
    var _date = new Date();
    var _mode = "unit";

    scheduler.locale.labels.timeline_tab = "Timeline";
    scheduler.config.xml_date = "%Y-%m-%d %H:%i";
    scheduler.config.details_on_dblclick = true;
    scheduler.config.details_on_create = true;
    //scheduler.config.time_step = stepTime;
    scheduler.config.mark_now = true;

    scheduler.locale.labels.unit_tab = "Unit";

    $.ajax({
        type: "POST",
        url: "AppointmentIframe.aspx/GetDoctorTree",
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
                        x_date: "%m/%d %H:%i",
                        x_step: 6,
                        x_size: 10,
                        x_start: 0,
                        x_length: 30,
                        y_unit: elements,
                        y_property: "section_id",
                        render: "tree",
                        folder_dy: 20,
                        dy: 60
                    });

                    scheduler.createUnitsView({
                        name: _mode,
                        property: "section_id",
                        list: elements
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
                        // If update roster
                        if (is_new == false) {
                            if (event_object.start_date <= new Date()) {
                                alert("You can not change appointment to passed or current date.");
                                return false;
                            }
                        }

                        return true;
                    });

                    // Save roster when resized or moved
                    scheduler.attachEvent("onEventChanged", function(event_id, event_object) {
                        //any custom logic here
                        var _id = event_object.id;
                        var _doctorUserName = event_object.section_id;
                        var _rosterType = event_object.RosterTypeId;
                        var _rosterTitle = event_object.RosterTypeTitle;
                        var _fromTime = event_object.start_date;
                        var _toTime = event_object.end_date;
                        var _note = event_object.note;

                        var requestdata = JSON.stringify({ Id: _id, DoctorUsername: _doctorUserName, StartTime: _fromTime, EndTime: _toTime });
                        $.ajax({
                            type: "POST",
                            url: "AppointmentIframe.aspx/UpdateEventMove",
                            async: true,
                            data: requestdata,
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function(response) {
                                var obj = eval(response.d)[0];

                                var evs = obj.data;

                                if (obj.result == "false") {
                                    alert(obj.message);
                                }

                                scheduler.deleteEvent(_id);
                                addRoster(evs);
                            },
                            fail: function() {
                                alert("Unknow error!");
                                return false;
                            },
                            complete: function() {
                            }
                        });
                    });

                    dhtmlxEvent(document, (_isOpera ? "keypress" : "keydown"), function(e) {

                        if (e.keyCode == 37) {
                            //left
                            scheduler.matrix.timeline.x_start -= 1;
                            if (scheduler.matrix.timeline.x_start < 0)
                                scheduler.matrix.timeline.x_start = 0;
                            scheduler.callEvent("onOptionsLoad", []);
                        } else if (e.keyCode == 39) {
                            //right
                            scheduler.matrix.timeline.x_start += 1;
                            if (scheduler.matrix.timeline.x_start > 24)
                                scheduler.matrix.timeline.x_start = 24;
                            scheduler.callEvent("onOptionsLoad", []);
                        }
                    });

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
            show_minical();
        }
    });
}

scheduler.showLightbox = function(id) {
    if (!blHour || !blStaff || !blPatient) {
        scheduler.deleteEvent(id);
        alert("Loading data is not complete. Please wait for a few moment.");
        return false;
    }

    initForm();

    var ev = scheduler.getEvent(id);
    _current_id = id;

    if (ev.start_date < new Date()) {
        if (ev.isnew == "false") {
            alert("You cannot change a passed appointment.");
            return false;
        }
        else {
            scheduler.deleteEvent(id);
            alert("You cannot create new appointment a passed day.");
            return false;
        }
    }

    setTime(ev);

    // Set init value
    $("#hdId").val(id);
    $("#txtFromDate").datepicker("setDate", ev.start_date);
    $("#txtToDate").datepicker("setDate", ev.end_date);
    $("#txtNote").val(ev.note);
    $("#hdStaff").val("");
    $("#hdPatient").val("");
    $("#hdContent").val("");
    $("#hdRoom").val("");

    // Staff id
    if (ev.section_id) {
        $("#hdStaff").val(ev.section_id);

        if (ev.isnew == "false") {
            $("#hdPatient").val(ev.CustomerId);
            $("#cboPatient").val(ev.CustomerId);
            $("#hdDoctor").val(ev.DoctorUserName);
            $("#hdRoom").val(ev.RoomId);

            $("#hdContent").val(ev.ContentId);
            $("#cboContent").val(ev.ContentId);
            $("#cboContent").show();
            $("#spanContent").hide();
            $("#cboContent").change();
        }
        else {
            $("#spanContent").show();
            $("#cboContent").hide();

            // Load current Functionality
            var requestdata = JSON.stringify({ DoctorUserName: $("#hdStaff").val() });
            $.ajax({
                type: "POST",
                url: "AppointmentIframe.aspx/GetCurrentContents",
                data: requestdata,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function(response) {
                    var obj = eval(response.d)[0];

                    if (obj.result == "true") {
                        var evs = obj.data;
                        if (evs) {
                            $("#cboContent").val(evs);
                            $("#cboContent").change();
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
                    $("#cboContent").show();
                    $("#spanContent").hide();
                }
            });
        }
    }

    if (ev.isnew == "false") {
        $("#trRepeat").hide();
        $("#tdTitle").text("Edit Appointment");
    }

    scheduler.startLightbox(id, html("RosterForm"));

    $("#trNewPatient").show();
    $('#trNewPatient1').hide();
    $('#trNewPatient2').hide();
    $('#idIsFemaleTitle').hide();
    $('#idIsFemale').hide();
}

function LoadRoster(mode, date) {
    var requestdata = JSON.stringify({ Mode: mode, CurrentDateView: date });
    $.ajax({
        type: "POST",
        url: "AppointmentIframe.aspx/LoadRoster",
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

function loadPatient() {
    $("#cboPatient").hide();
    $("#loadingPatient").show();
    // Load data
    $.ajax({
        type: "POST",
        url: "AppointmentIframe.aspx/GetPatient",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {
            var obj = eval(msg.d)[0];
            if (obj.result == "true") {
                var arr = eval(obj.data);
                $("#cboPatient").find("option").remove();

                $.each(arr, function(i, item) {
                    $("#cboPatient").append('<option value="' + item.key + '">' + item.label + '</option>');
                });
            }
            blPatient = true;
        },
        fail: function() {
            alert("Cannot load Patients. Please try again or contact Administrator.");
            scheduler.endLightbox(false, html("RosterForm"));
        },
        complete: function() {
            $("#cboPatient").show();
            $("#loadingPatient").hide();
        }
    });
}

function loadHour() {
    $.ajax({
        type: "POST",
        url: "AppointmentIframe.aspx/GetTime",
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

            blHour = true;
        },
        fail: function() {
            alert("Cannot load Hour data. Please try again or contact Administrator.");
            scheduler.endLightbox(false, html("RosterForm"));
        },
        complete: function() {
        }
    });
}

function loadContent() {
    $.ajax({
        type: "POST",
        url: "AppointmentIframe.aspx/GetContents",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            var obj = eval(response.d)[0];
            if (obj.result == "true") {
                var arr = eval(obj.data);
                $("#cboContent").find("option").remove();

                $.each(arr, function(i, item) {
                    $("#cboContent").append('<option value="' + item.key + '">' + item.label + '</option>');
                });

                $("#cboContent").show();
            }
            else {
                alert(obj.message);
            }
            blStaff = true;
        },
        fail: function() {
            alert("Cannot load content data. Please try again or contact Administrator.");
            scheduler.endLightbox(false, html("RosterForm"));
        },
        complete: function() {
        }
    });
}

function loadStaff() {
    if (!$("#txtFromDate").datepicker("getDate") || !$("#txtToDate").datepicker("getDate"))
        return;

    var requestdata = JSON.stringify({ AppointmentId: _current_id, FuncId: $("#cboContent").val(), StartTime: $("#cboFromHour").val(), EndTime: $("#cboToHour").val(),
        StartDate: $("#txtFromDate").datepicker("getDate"), EndDate: $("#txtToDate").datepicker("getDate")
    });
    $("#cboStaff").hide();
    $("#spanStaff").show();

    $.ajax({
        type: "POST",
        url: "AppointmentIframe.aspx/GetStaffs",
        data: requestdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            var obj = eval(response.d)[0];
            if (obj.result == "true") {
                var arr = eval(obj.data);
                $("#cboStaff").find("option").remove();

                $.each(arr, function(i, item) {
                    $("#cboStaff").append('<option ' + item.property + ' value="' + item.key + '">' + item.label + '</option>');
                });

                $("#cboStaff").val($("#hdStaff").val());
                loadRooms();
            }
            else {
                alert(obj.message);
            }
            blStaff = true;
        },
        fail: function() {
            alert("Cannot load Doctor data. Please try again or contact Administrator.");
            scheduler.endLightbox(false, html("RosterForm"));
        },
        complete: function() {
            $("#cboStaff").show();
            $("#spanStaff").hide();
        }
    });
}

function loadRooms() {
    if (!$("#txtFromDate").datepicker("getDate") || !$("#txtToDate").datepicker("getDate"))
        return;

    var requestdata = JSON.stringify({ AppointmentId: _current_id, DoctorUserName: $("#cboStaff").val(), FuncId: $("#cboContent").val(),
        StartTime: $("#cboFromHour").val(), EndTime: $("#cboToHour").val(),
        StartDate: $("#txtFromDate").datepicker("getDate"), EndDate: $("#txtToDate").datepicker("getDate")
    });

    $("#cboRoom").hide();
    $("#loadingRoom").show();

    // Load data
    $.ajax({
        type: "POST",
        url: "AppointmentIframe.aspx/GetRooms",
        data: requestdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {
            var obj = eval(msg.d)[0];
            if (obj.result == "true") {
                var arr = eval(obj.data);
                $("#cboRoom").find("option").remove();

                $.each(arr, function(i, item) {
                    $("#cboRoom").append('<option ' + item.property + ' value="' + item.key + '">' + item.label + '</option>');
                });

                $("#cboRoom").val($("#hdRoom").val());
            }
        },
        fail: function() {
            alert("Cannot load Room. Please try again or contact Administrator.");
            scheduler.endLightbox(false, html("RosterForm"));
        },
        complete: function() {
            $("#cboRoom").show();
            $("#loadingRoom").hide();
        }
    });
}

// Create new patient
function CreateSimplePatient() {
    $("#txtFirstName").val($.trim($("#txtFirstName").val()))
    $("#txtLastName").val($.trim($("#txtLastName").val()))
    $("#txtCellPhone").val($.trim($("#txtCellPhone").val()))
    $("#txtAddress").val($.trim($("#txtAddress").val()))

    var _firstName = $("#txtFirstName").val();
    var _lastName = $("#txtLastName").val();
    var _cellPhone = $("#txtCellPhone").val();
    var _address = $("#txtAddress").val();
    var _isFemale = $("#chkIsFemale").val();
    var _id = $("#hdId").val();
    var ev = scheduler.getEvent(_id);
    var _fromTime = $("#cboFromHour").val();
    var _toTime = $("#cboToHour").val();
    var _fromDate = $("#txtFromDate").datepicker("getDate");
    var _toDate = $("#txtToDate").datepicker("getDate");
    var _note = $("#txtNote").val();
    var _doctorUserName = $("#cboStaff").val();
    var _room = $("#cboRoom").val();
    var _content = $("#cboContent").val();

    // Validate
    if (_firstName == "") {
        alert("Please input First name");
        $("#txtFirstName").focus();
        return;
    }
    if (_lastName == "") {
        alert("Please input Last name");
        $("#txtLastName").focus();
        return;
    }

    // Check if staff is not selected
    if (_content == null || _content == "-1") {
        alert("You must choose content.");
        return;
    }

    // Check datetime
    var _validateFromTime = _fromDate.format("yyyy/mm/dd ") + _fromTime;
    var _validateToTime = _toDate.format("yyyy/mm/dd ") + _toTime;
    if (_validateFromTime >= _validateToTime) {
        alert("From date, time must be less than to date, time");
        $("#cboFromHour").focus();
        return;
    }

    // Check if staff is not selected
    if (_doctorUserName == null || _doctorUserName == "-1") {
        alert("You must choose doctor.");
        return;
    }

    // Check if room is not selected
    if (_room == null || _room == "-1") {
        alert("You must choose room.");
        return;
    }

    $("#dialog-modal").show();
    disableAllElements($("#tblContent"), false)

    var requestdata = JSON.stringify({ FirstName: _firstName, LastName: _lastName, CellPhone: _cellPhone, Address: _address, IsFemale: _isFemale });
    $.ajax({
        type: "POST",
        url: "AppointmentIframe.aspx/CreateSimplePatient",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = eval(response.d)[0];
            if (obj.result == "true") {
                var evs = obj.data;

                $("#hdPatient").val(evs);
                loadPatient();
                $("#cboPatient").val(evs);
                $("#trNewPatient").show();
                $('#trNewPatient1').hide();
                $('#trNewPatient2').hide();
                $('#idIsFemaleTitle').hide();
                $('#idIsFemale').hide();

                AppointmentBusiness(evs);
            }
            else {
                alert(obj.message);
                disableAllElements($("#tblContent"), true)
                $("#dialog-modal").hide();
                return;
            }
        },
        fail: function() {
            alert("Unknow error!");
        },
        complete: function() {
        }
    });
}

function AppointmentBusiness(_patient) {
    // Check if content is not selected
    if ($("#cboContent").val() == null || $("#cboContent").val() == "-1") {
        alert("You must choose content.");
        $("#cboContent").focus();
        return;
    }

    var _id = $("#hdId").val();
    var ev = scheduler.getEvent(_id);
    var _fromTime = $("#cboFromHour").val();
    var _toTime = $("#cboToHour").val();
    var _fromDate = $("#txtFromDate").datepicker("getDate");
    var _toDate = $("#txtToDate").datepicker("getDate");
    var _note = $("#txtNote").val();
    var _doctorUserName = $("#cboStaff").val();
    var _room = $("#cboRoom").val();
    var _content = $("#cboContent").val();

    // Check if staff is not selected
    if (_content == null || _content == "-1") {
        alert("You must choose content.");
        return;
    }

    // Check datetime
    var _validateFromTime = _fromDate.format("yyyy/mm/dd ") + _fromTime;
    var _validateToTime = _toDate.format("yyyy/mm/dd ") + _toTime;
    if (_validateFromTime >= _validateToTime) {
        alert("From date, time must be less than to date, time");
        $("#cboFromHour").focus();
        return;
    }

    // Check if staff is not selected
    if (_doctorUserName == null || _doctorUserName == "-1") {
        alert("You must choose doctor.");
        return;
    }

    // Check if room is not selected
    if (_room == null || _room == "-1") {
        alert("You must choose room.");
        return;
    }

    var requestdata;
    if (ev.isnew == "false") {
        // Update
        $("#dialog-modal").show();
        disableAllElements($("#tblContent"), false)

        requestdata = JSON.stringify({ Id: _id, PatientId: _patient, ContentId: _content, Note: _note, StartTime: _fromTime, EndTime: _toTime,
            StartDate: _fromDate, EndDate: _toDate, DoctorUsername: _doctorUserName, RoomId: _room
        });
        UpdateRoster(requestdata, 'UpdateEventSave', _id);
    }
    else {
        // Add new 
        $("#dialog-modal").show();
        disableAllElements($("#tblContent"), false)

        requestdata = JSON.stringify({ PatientId: _patient, ContentId: _content, Note: _note, StartTime: _fromTime, EndTime: _toTime,
            StartDate: _fromDate, EndDate: _toDate, DoctorUsername: _doctorUserName, RoomId: _room
        });
        $.ajax({
            type: "POST",
            url: "AppointmentIframe.aspx/SaveEvent",
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

function SaveRoster() {
    if ($("#chkNewPatient").attr("checked")) {
        CreateSimplePatient();
    }
    else {
        // Check if patient is not selected
        if ($("#cboPatient").val() == null || $("#cboPatient").val() == "-1") {
            alert("You must choose patient.");
            $("#cboPatient").focus();
            return;
        }
        AppointmentBusiness($("#cboPatient").val());
    }
}

function UpdateRoster(requestdata, method, _id) {
    $.ajax({
        type: "POST",
        url: "AppointmentIframe.aspx/" + method,
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = eval(response.d)[0];
            if (obj.result == "true") {
                var evs = obj.data;
                scheduler.deleteEvent(_id);
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
    if (!confirm("Do you want to delete this appointment?"))
        return false;

    disableAllElements($("#tblContent"), false)
    $("#dialog-modal").show();
    var ev = scheduler.getEvent($("#hdId").val());
    if (ev.isnew == "false") {
        var requestdata = JSON.stringify({ Id: ev.id });
        $.ajax({
            type: "POST",
            url: "AppointmentIframe.aspx/DeleteEvent",
            data: requestdata,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(response) {
                var obj = eval(response.d)[0];
                if (obj.result == "true") {
                    alert("Appointment has been deleted.");
                    scheduler.deleteEvent(ev.id)
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

$(document).ready(function() {
    loadPatient();
    loadHour();
    loadStaff();
    loadContent();

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

    initEvents();
});

// Get patient info
function GetPatientInfo(patientId, cboPatientName, cboPatientId) {
    if (patientId) {
        // Load patient
        cboPatientName.setComboValue(patientId);
        cboPatientId.setComboValue(patientId);
    }
    else {
        // Tao moi patient
    }
}

function setTime(ev) {
    var startTime = ev.start_date.format("HH:MM");
    var endTime = ev.end_date.format("HH:MM");
    $("#cboFromHour").val(startTime + ":00");
    $("#cboToHour").val(endTime + ":00");
}

function initForm() {
    $("#dialog-modal").hide();

    $("#trNewPatient").show();
    $('#trNewPatient1').show();
    $('#trNewPatient2').show();
    $('#idIsFemaleTitle').show();
    $('#idIsFemale').show();

    $("#cboFromHour").show();
    $("#loadingFromHour").hide();
    $('#spanFromDate').show();

    $("#cboToHour").show();
    $("#loadingToHour").hide();
    $('#spanToDate').show();

    $("#cboContent").hide();
    $("#spanContent").show();

    $("#cboStaff").hide();
    $("#spanStaff").show();

    $("#cboRoom").hide();
    $("#loadingRoom").show();

    // Set empty
    $("#chkNewPatient").attr("checked", false);
    $("#hdId").val("");
    $("#tdTitle").text("New appointment");
    $("#txtNote").val("");

    $("#chkNewPatient").focus();

    disableAllElements($("#tblContent"), true)
}

function disableAllElements(obj, disabled) {
    if (disabled) {
        $("input, textarea, select", obj).removeAttr('disabled');
    }
    else {
        $("input, textarea, select", obj).attr('disabled', true);
    }
}

function checkDateTime() {
    var _fromTime = $("#cboFromHour").val();
    var _toTime = $("#cboToHour").val();
    var _fromDate = $("#txtFromDate").datepicker("getDate");
    var _toDate = $("#txtToDate").datepicker("getDate");

    // Check datetime
    var _validateFromTime = _fromDate.format("yyyy/mm/dd ") + _fromTime;
    var _validateToTime = _toDate.format("yyyy/mm/dd ") + _toTime;
    if (_validateFromTime >= _validateToTime) {
        return false;
    }
    return true;
}

function initEvents() {
    $("#chkNewPatient").click(function() {
        $('#trNewPatient1').toggle($(this).attr('checked'));
        $('#trNewPatient2').toggle($(this).attr('checked'));
        $('#idIsFemaleTitle').toggle($(this).attr('checked'));
        $('#idIsFemale').toggle($(this).attr('checked'));

        $('#trNewPatient').toggle(!$(this).attr('checked'));
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
            $("#chkNewPatient").focus();
            return false;
        }
    });

    $("#cboPatient").change(function() {
        //        loadRooms();
    });

    // Call Load doctor function
    $("#cboContent").change(function() {
        $("#cboStaff").change();
        loadStaff();
    });

    $("#cboFromHour, #txtFromDate").change(function() {
        if (!checkDateTime()) {
            $("#cboToHour").val($("#cboFromHour").val());
            $("#txtToDate").val($("#txtFromDate").val());
        }

        $("#cboStaff").change();
        loadStaff();
    });

    $("#cboToHour, #txtToDate").change(function() {
        if (!checkDateTime()) {
            $("#cboFromHour").val($("#cboToHour").val());
            $("#txtFromDate").val($("#txtToDate").val());
        }

        $("#cboStaff").change();
        loadStaff();
    });

    // Call Load room function
    $("#cboStaff").change(function() {
        loadRooms();
    });

}