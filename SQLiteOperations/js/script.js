var database = new WinRuntimes.Database();
function createDatabase() {
    database.createDatabase().then(function (response) {
        if (response == 'success') {
            var msg = new Windows.UI.Popups.MessageDialog("Successfully created!", "Success");
            msg.showAsync();
        }
        else {
            var msg = new Windows.UI.Popups.MessageDialog("Database creation failed", "Failed");
            msg.showAsync();
        }
    });
}

function insertRecords() {
    var countryName = document.getElementById("name").value;
    var cityName = document.getElementById("city").value;
    database.insertRecords(countryName, cityName).then(function(response) {
        if (response == 'success') {
            var msg = new Windows.UI.Popups.MessageDialog("Successfully inserted!", "Success");
            msg.showAsync();
        }
        else {
            var msg = new Windows.UI.Popups.MessageDialog("Insert failed", "Failed");
            msg.showAsync();
        }
    });
}

function readDatabase() {
    database.readDatabase().then(function (response) {
        document.getElementById("content").innerHTML = response;
    });
}

function updateRecord() {
    database.updateDatabase().then(function (response) {
        if (response == 'success') {
            var msg = new Windows.UI.Popups.MessageDialog("Update success!", "Success");
            msg.showAsync();
        }
        else {
            var msg = new Windows.UI.Popups.MessageDialog("Update failed", "Failed");
            msg.showAsync();
        }
    });
}

function deleteRecord() {
    database.deleteRecord().then(function (response) {
        if (response == 'success') {
            var msg = new Windows.UI.Popups.MessageDialog("Delete success!", "Success");
            msg.showAsync();
        }
        else {
            var msg = new Windows.UI.Popups.MessageDialog("Delete failed", "Failed");
            msg.showAsync();
        }
    })
}