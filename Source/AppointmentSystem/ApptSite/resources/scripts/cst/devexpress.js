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
    if (gridObject.cpApptMessage && gridObject.cpApptMessage != "") {
        ShowDialog("", "", gridObject.cpApptMessage, "");
        gridObject.cpApptMessage = "";
    }
}
function confirmDelete() { // confirm again user when delete patient
   
     return confirm("Are you sure want to delete?");
    }