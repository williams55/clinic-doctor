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

function AlertMessage() {
    // Alert if there is have message
    if (grid.cpApptMessage && grid.cpApptMessage != "") {
        alert(grid.cpApptMessage);
        grid.cpApptMessage = "";
    }
}
