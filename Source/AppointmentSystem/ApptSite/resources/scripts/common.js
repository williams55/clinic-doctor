if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function(searchElement /*, fromIndex */) {
        "use strict";
        if (this == null) {
            throw new TypeError();
        }
        var t = Object(this);
        var len = t.length >>> 0;
        if (len === 0) {
            return -1;
        }
        var n = 0;
        if (arguments.length > 0) {
            n = Number(arguments[1]);
            if (n != n) { // shortcut for verifying if it's NaN
                n = 0;
            } else if (n != 0 && n != Infinity && n != -Infinity) {
                n = (n > 0 || -1) * Math.floor(Math.abs(n));
            }
        }
        if (n >= len) {
            return -1;
        }
        var k = n >= 0 ? n : Math.max(len - Math.abs(n), 0);
        for (; k < len; k++) {
            if (k in t && t[k] === searchElement) {
                return k;
            }
        }
        return -1;
    };
}

var command = "";
var gridObject = null;

// Call refesh event for grid
function RefreshGrid() {
    if ((command == 'CUSTOMBUTTON' || command == 'CUSTOMCALLBACK') && gridObject != null) {
        var pageIndex = gridObject.GetPageIndex();
        gridObject.GotoPage(pageIndex);
        command = "";
        gridObject = null;
    }
}

function AlertMessage() {
    var currentGrid = gridObject;
    if (!currentGrid) {
        currentGrid = grid;
    }
    // Alert if there is have message
    if (currentGrid.cpApptMessage && currentGrid.cpApptMessage != "") {
        ShowDialog("", "", currentGrid.cpApptMessage, "");
        currentGrid.cpApptMessage = "";
    }
}
function confirmDelete() { // confirm again user when delete patient

    return confirm("Are you sure want to delete?");
}

function isEmail(string) {
    if (string.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1) {
        return true;
    }
    return false;
}
function isPhone(string) {
    if (string.search(/\d{3}\-\d{3}\-\d{4}/)) {
        return true;
    }
    return false;
}

/******************************* Dialog *********************************/
function ShowDialog(objId, title, message, url) {
    $("#spanMessage-content").html(message);
    $("#dialog-message-title").attr("title", "Message");
    if (title) {
        $("#dialog-message-title").attr("title", title);
    }
    $("#dialog-message-title").dialog({
        resizable: false,
        height: 150,
        modal: true,
        zIndex: $.maxZIndex() + 1,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
                if (objId) {
                    $('#' + objId).focus();
                    $('#' + objId).select();
                }
            }
        },
        close: function () {
            if (url) {
                location.href = url;
            }
        }
    });
}

// Hien thi message
function ShowMessage(message) {
    ShowDialog('', '', message, '');
}

// Hien thi message
function CallError() {
    ShowDialog('', 'Error', 'System error. Please contact Administrator', '');
}

function ShowProgress() {
    LoadingPanel.Show();
}

function CloseProgress() {
    LoadingPanel.Hide();
}

/******************************* Scheduler *********************************/
// Ham block time cho scheduler
function BlockTimespan(scheduler, date, mode, list) {
    // Block thoi gian
    var now, startDate, endDate, currentDay;
    switch (mode) {
        // Truong hop view la timeline thi kiem tra ngay co phai la ngay hien tai khong

        case 'timeline':
        case 'unit':
        case 'day':
            // Lay thoi gian hien tai
            now = new Date();

            // Lay thoi diem dau tuan
            startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());

            // Lay thoi diem cuoi ngay
            endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate(), 23, 59, 59);

            // Neu thoi gian cua view hien tai < thoi gian hien tai => gan thoi gian hien tai = gia tri do
            // Cai nay dung de block
            if (endDate < now) now = endDate;

            list.push(scheduler.addMarkedTimespan({
                start_date: startDate,
                end_date: now,
                css: 'small_lines_section',
                type: "dhx_time_block"
            }));

            break;
        case 'week':
            // Lay thu tu cua ngay xem hien tai
            currentDay = date.getDay();

            // Neu la CN thi gan no = 7
            if (currentDay == 0) currentDay = 7;

            // Lay thoi diem cuoi tuan
            endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate(), 23, 59, 59);
            endDate.setDate(date.getDate() + 7 - currentDay);

            // Lay thoi diem dau tuan
            startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            startDate.setDate(date.getDate() + 1 - currentDay);

            // Lay thoi gian hien tai
            now = new Date();

            // Neu thoi gian cua view hien tai < thoi gian hien tai => gan thoi gian hien tai = gia tri do
            // Cai nay dung de block
            if (endDate < now) now = endDate;

            list.push(scheduler.addMarkedTimespan({
                start_date: startDate,
                end_date: now,
                css: 'small_lines_section',
                type: "dhx_time_block"
            }));

            break;
        case 'month':
            // Lay thoi diem dau thang
            // Sau do lay thoi diem dau tuan cua ngay dau thang
            startDate = new Date(date.getFullYear(), date.getMonth(), 1);
            currentDay = startDate.getDay();
            if (currentDay == 0) currentDay = 7;
            startDate.setDate(startDate.getDate() + 1 - currentDay);

            // Lay thoi diem cuoi thang
            // Sau do lay thoi diem cuoi tuan cua ngay cuoi thang
            endDate = new Date(date.getFullYear(), date.getMonth(), 1, 23, 59, 59);
            endDate.setMonth(endDate.getMonth() + 1);
            currentDay = endDate.getDay();
            if (currentDay == 0) currentDay = 7;
            endDate.setDate(endDate.getDate() + 7 - currentDay);

            // Lay thoi gian hien tai
            now = new Date();
            now = new Date(now.getFullYear(), now.getMonth(), now.getDate() + 1);

            // Neu thoi gian cua view hien tai < thoi gian hien tai => gan thoi gian hien tai = gia tri do
            // Cai nay dung de block
            if (endDate < now) now = endDate;

            list.push(scheduler.addMarkedTimespan({
                start_date: startDate,
                end_date: now,
                css: 'small_lines_section',
                type: "dhx_time_block"
            }));

            break;
        default:
            break;
    }
}

function DeleteTimespan(scheduler, list) {
    $.each(list, function (i, item) {
        scheduler.deleteMarkedTimespan(item);
    });
}