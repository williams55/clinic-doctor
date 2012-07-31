function GenCss(value, width, height) {
    var matrix = CSS3Helpers.getTransformationMatrix(value, true);
    
    var filterCode = config.getScriptedValue('transformToMatrix.templates.ieFilters',
				{
				    M11: matrix.e(1, 1),
				    M12: matrix.e(1, 2),
				    M21: matrix.e(2, 1),
				    M22: matrix.e(2, 2),
				    width: width + "px",
				    height: height + "px",
				    transform: value.trim().replace(/\s+/g, ' ').replace(/\)(\s)/g, ')<br>                         ')
				});
    console.log(filterCode);
}

$(document).ready(function() {
    setInterval(function() {
        var seconds = new Date().getSeconds();
        var sdegree = seconds * 6;
        var srotate = "rotate(" + sdegree + "deg)";

        var matrix = CSS3Helpers.getTransformationMatrix(srotate, true);

        $("#sec").css({
            "-moz-transform": srotate,
            "-webkit-transform": srotate,
            "-ms-transform": srotate,
            "transform": srotate,
            "-o-transform": srotate,
            "filter": "progid:DXImageTransform.Microsoft.Matrix(M11=" + matrix.e(1, 1)
            + ", M12=" + matrix.e(1, 2)
            + ", M21=" + matrix.e(2, 1)
            + ", M22=" + matrix.e(2, 2)
            + ")"
        });

    }, 1000);


    setInterval(function() {
        var hours = new Date().getHours();
        var mins = new Date().getMinutes();
        var hdegree = hours * 30 + (mins / 2);
        var hrotate = "rotate(" + hdegree + "deg)";

        var matrix = CSS3Helpers.getTransformationMatrix(hrotate, true);

        $("#hour").css({
            "-moz-transform": hrotate,
            "-webkit-transform": hrotate,
            "-ms-transform": hrotate,
            "transform": hrotate,
            "-o-transform": hrotate,
            "filter": "progid:DXImageTransform.Microsoft.Matrix(M11=" + matrix.e(1, 1)
            + ", M12=" + matrix.e(1, 2)
            + ", M21=" + matrix.e(2, 1)
            + ", M22=" + matrix.e(2, 2)
            + ")"
        });

    }, 1000);


    setInterval(function() {
        var mins = new Date().getMinutes();
        var mdegree = mins * 6;
        var mrotate = "rotate(" + mdegree + "deg)";

        var matrix = CSS3Helpers.getTransformationMatrix(mrotate, true);

        $("#min").css({
            "-moz-transform": mrotate,
            "-webkit-transform": mrotate,
            "-ms-transform": mrotate,
            "transform": mrotate,
            "-o-transform": mrotate,
            "filter": "progid:DXImageTransform.Microsoft.Matrix(M11=" + matrix.e(1, 1)
            + ", M12=" + matrix.e(1, 2)
            + ", M21=" + matrix.e(2, 1)
            + ", M22=" + matrix.e(2, 2)
            + ")"
        });

    }, 1000);

}); 
