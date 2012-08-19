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

function ShowProgress() {
    $("#spanMessage-content").html('<div id="progressbar"></div>');
    $("#progressbar").progressbar({
        value: 100
    });
    $("#dialog-message-title").dialog({
        resizable: false,
        height: 120,
        modal: false,
        buttons: {}
    });
}

function CloseProgress() {
    $("#dialog-message-title").dialog("close");
}
