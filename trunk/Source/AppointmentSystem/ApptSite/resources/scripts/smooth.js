/* path to the stylesheets for the color picker */
var style_path = "resources/css/colors";

$(document).ready(function() {
    /* messages fade away when dismiss is clicked */
    $(".message > .dismiss > a").live("click", function(event) {
        var value = $(this).attr("href");
        var id = value.substring(value.indexOf('#') + 1);

        $("#" + id).fadeOut('slow', function() { });

        return false;
    });

    /* color picker */
    $("#colors-switcher > a").click(function() {
        var style = $("#color");

        style.attr("href", "" + style_path + "/" + $(this).attr("title").toLowerCase() + ".css");

        return false;
    });

    $("#menu h6 a").click(function() {
        var link = $(this);
        var value = link.attr("href");
        var id = value.substring(value.indexOf('#') + 1);

        var heading = $("#h-menu-" + id);
        var list = $("#menu-" + id);

        if (list.attr("class") == "closed") {
            heading.attr("class", "selected");
            list.attr("class", "opened");
        } else {
            heading.attr("class", "");
            list.attr("class", "closed");
        }
    });

    $("#menu li[class~=collapsible]").click(function() {
        var element = $(this);

        element.children("a:first-child").each(function() {
            var child = $(this);

            if (child.attr("class") == "plus") {
                child.attr("class", "minus");
            } else {
                child.attr("class", "plus");
            }
        });

        element.children("ul").each(function() {
            var child = $(this);

            if (child.attr("class") == "collapsed") {
                child.attr("class", "expanded");
            } else {
                child.attr("class", "collapsed");
            }
        });
    });
});

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
            Ok: function() {
                $(this).dialog("close");
                if (objId) {
                    $('#' + objId).focus();
                    $('#' + objId).select();
                }
            }
        },
        close: function() {
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

function ShowProgress() {
    LoadingPanel.Show();
}

function CloseProgress() {
    LoadingPanel.Hide();
}

/******************************* Scheduler *********************************/
// Ham block time cho scheduler
function BlockTimespan(scheduler, date, mode) {
    // Block thoi gian
    var now, startDate, endDate, currentDay;
    scheduler.deleteMarkedTimespan();
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

            scheduler.addMarkedTimespan({
                start_date: startDate,
                end_date: now,
                css: 'small_lines_section',
                type: "dhx_time_block"
            });

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

            scheduler.addMarkedTimespan({
                start_date: startDate,
                end_date: now,
                css: 'small_lines_section',
                type: "dhx_time_block"
            });

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

            scheduler.addMarkedTimespan({
                start_date: startDate,
                end_date: now,
                css: 'small_lines_section',
                type: "dhx_time_block"
            });

            break;
        default:
            break;
    }
    scheduler.updateView();
}
