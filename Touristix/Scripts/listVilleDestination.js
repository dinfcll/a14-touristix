//var ListVille = [];

function ObtenirListeVilles(NomObjet) {
    var xmlhttp;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    var url = "/Destination/ObtenirListeVille";

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            Succes(xmlhttp.responseText);
        }
    }
    var params = "Id=" + Id + "strPays=" + NomObjet.Value;
    xmlhttp.open("GET", url);
    xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlhttp.setRequestHeader("Content-length", params.length);
    xmlhttp.setRequestHeader("Connection", "close");
    xmlhttp.send(params);

    function Success(response) {
        var arr = JSON.parse(response);
        var i;
        var out = "";
        for (i = 0; i < arr.length; i++) {
            var Selected = arr[i].Value == Id;
            if (Selected) {
                out += '<option value="' + arr[i].Value + '" selected>' +
                            arr[i].Text + '</option>';
            }
            else {
                out += '<option value="' + arr[i].Value + '">' +
                            arr[i].Text + '</option>';
            }
        }
        document.getElementById("ddlVille").innerHTML = out;
        document.getElementById("Intro").innerHTML = "allo";
    }
}
