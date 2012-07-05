var command = "";
var gridObject = null;

// Call refesh event for grid
function RefreshGrid() {
    if (command == 'CUSTOMBUTTON' && gridObject != null) {
        gridObject.Refresh();
        command = "";
        gridObject = null;
    }
}