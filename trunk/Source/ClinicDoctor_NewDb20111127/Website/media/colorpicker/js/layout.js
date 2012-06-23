function BuildColorPicker() {
    if (($('#colorpickerHolder').html()) != null) {
        // Default color
        var defaultColor = '#ffffff';

        // Get color input object
        var inputObject = $($("#colorCode").val());
        if ($(inputObject).val() != '') {
            defaultColor = $(inputObject).val();
        }
        $('#colorpickerHolder').ColorPicker({
            flat: true,
            color: defaultColor,
            onSubmit: function(hsb, hex, rgb) {
                $('#colorSelector div').css('backgroundColor', '#' + hex);
                $(inputObject).val('#' + hex);
            }
        });
        $('#colorpickerHolder>div').css('position', 'absolute');
        var widt = false;
        $('#colorSelector').bind('click', function() {
            $('#colorpickerHolder').stop().animate({ height: widt ? 0 : 173 }, 500);
            widt = !widt;
        });
    }
}
