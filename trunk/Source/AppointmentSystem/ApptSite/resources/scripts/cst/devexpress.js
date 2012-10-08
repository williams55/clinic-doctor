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