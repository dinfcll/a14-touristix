var ListBatiment = [];

function ObtenirListeBatiment(Id, NomObjet) {
    var xmlhttp;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    var url = "/Destination/ObtenirListeBatiment";

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            Succes(xmlhttp.responseText);
        }
    }
    var params = "Id=" + Id
    xmlhttp.open("POST", url);
    xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlhttp.setRequestHeader("Content-length", params.length);
    xmlhttp.setRequestHeader("Connection", "close");
    xmlhttp.send(params);

    function Succes(response) {
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
        document.getElementById(NomObjet).innerHTML = out;
    }
}

function AjoutDropDownListBatiment(BatimentActif) {
    var DernierBatimentTag = document.getElementById("DernierBatiment");
    var DernierBatiment = parseInt(DernierBatimentTag.value, 10);

    if (BatimentActif.id == "Batiment" + DernierBatiment) {
        ListBatiment.push({
            "ID": DernierBatiment
        });
        DernierBatiment += 1;
        DernierBatimentTag.value = DernierBatiment;
        var DivBatiments = document.getElementById("Batiments");
        var addButton = document.createElement("select");
        addButton.type = "select";
        addButton.id = "Batiment" + DernierBatiment;
        addButton.name = "Batiment" + DernierBatiment;
        addButton.setAttribute("class", "dropdown1");
        addButton.setAttribute("onchange", "AjoutDropDownListBatiment(this)");
        addButton.style.width = "250px";
        var o2 = document.createElement("br");
        var o3 = document.createElement("br");
        DivBatiments.appendChild(addButton);
        DivBatiments.appendChild(o2);
        DivBatiments.appendChild(o3);
        ObtenirListeBatiment(-1, "Batiment" + DernierBatiment);
    }
}

var ListActivite = [];

function ObtenirListeActivite(Id, NomObjet) {
    var xmlhttp;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    var url = "/Destination/ObtenirListeActivite";

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            Succes(xmlhttp.responseText);
        }
    }
    var params = "Id=" + Id
    xmlhttp.open("POST", url);
    xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlhttp.setRequestHeader("Content-length", params.length);
    xmlhttp.setRequestHeader("Connection", "close");
    xmlhttp.send(params);

    function Succes(response) {
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
        document.getElementById(NomObjet).innerHTML = out;
    }
}

function AjoutDropDownListActivite(ActiviteActif) {
    var DerniereActiviteTag = document.getElementById("DerniereActivite");
    var DerniereActivite = parseInt(DerniereActiviteTag.value, 10);

    if (ActiviteActif.id == "Activite" + DerniereActivite) {
        ListActivite.push({
            "ID": DerniereActivite
        });
        DerniereActivite += 1;
        DerniereActiviteTag.value = DerniereActivite;
        var DivActivites = document.getElementById("Activites");
        var addButton = document.createElement("select");
        addButton.type = "select";
        addButton.id = "Activite" + DerniereActivite;
        addButton.name = "Activite" + DerniereActivite;
        addButton.setAttribute("class", "dropdown1");
        addButton.setAttribute("onchange", "AjoutDropDownListActivite(this)");
        addButton.style.width = "250px";
        var o2 = document.createElement("br");
        var o3 = document.createElement("br");
        DivActivites.appendChild(addButton);
        DivActivites.appendChild(o2);
        DivActivites.appendChild(o3);
        ObtenirListeActivite(-1, "Activite" + DerniereActivite);
    }
}

function AfficheMap(Adresse)
{
     var map;                             
        var mapOptions = {
            zoom: 8,
            center: new google.maps.LatLng(-34.397, 150.644)
        };
        map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

        new google.maps.Geocoder().geocode({ 'address': Adresse }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var marker = new google.maps.Marker({
                    map: map,
                    draggable: true
                });
                marker.setPosition(results[0].geometry.location);
                map.setCenter(results[0].geometry.location);
                map.setZoom(15);
            }
            else {
                alert("Impossible d'afficher la carte. Renvoi automatique vers Sydney.");
            }
        });
        google.maps.event.addDomListener(window, 'load', initialize);
    }


