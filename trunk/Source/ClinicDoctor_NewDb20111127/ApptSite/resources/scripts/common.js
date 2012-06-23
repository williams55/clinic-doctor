function CommaFormatted(num, isUsd) {
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
        num.substring(num.length - (4 * i + 3));
    if (isUsd == 1)
        return (((sign) ? '' : '-') + num + '.' + cents);
    return (((sign) ? '' : '-') + num);
}

function HighlightMenu() {
    var current = $('a[href="' + window.location.pathname + '"]');
    if ($(current).length == 0)
        current = $('a[href="' + window.location.pathname.replace("Edit.aspx", ".aspx") + '"]');
    $(current).addClass('current');
    $(current).closest('ul').css("display", "block");
    $(current).closest('ul').prev().addClass('current');
}
function MasterPageLoad() {
    $(".datepicker").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy'
    });
    $('.checkall').click(function() {
        jQuery(this).parents('table:eq(0)').find(':checkbox').attr('checked', this.checked);
    });
    HighlightMenu();
    if ($.browser.safari) { // toolbars appeared on separate lines. 
        $('[id*=ReportViewer] table').each(function(i, item) {
            if ($(item).attr('id') && $(item).attr('id').match( /fixedTable$/ ) != null)
                $(item).css('display', 'table');
            else
                $(item).css('display', 'inline-block');
        });
        $('[id*=ReportViewer] table td').css('padding', '0px');
    }
    
    // needed when AsyncEnabled=true.
}

