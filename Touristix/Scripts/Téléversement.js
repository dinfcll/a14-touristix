﻿var EnvoyerImageDestination = function () {

    var ThumbnailsDestination = document.getElementById('ThumbnailsDestination');
    var _file = document.getElementById('FichierDestination');
    if (_file.files.length === 0) {
        return;
    }

    InitialisationEnvoi(_file);

    var data = new FormData();
    data.append('SelectedFile', _file.files[0]);

    var xmlhttp;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            BoutonVoirImage.href = "/Images/Destinations/" + _file.value;
            NomImage.innerHTML = _file.value;
            var Output = "";
            Output += "<div class='GrilleImages'>";
            Output += "<div class='thumbnail'>";
            Output += "<a href='/Images/Destinations/" + _file.value + "'>";
            Output += "<img src='/Images/Destinations/" + _file.value + "' class='Miniature' alt='' />";
            Output += "</a>";
            Output += "<br />";
            Output += "<div>";
            Output += "<button class='close pull-right' onclick='SupprimerImage(this, \"" + _file.value;
            Output += "\", \"/Admin/SupprimerImageDestination\")'>&times;</button>";
            Output += "</div>";
            Output += "<br />";
            Output += "</div>";
            Output += "</div>";
            ThumbnailsDestination.innerHTML += Output;
        }
    };

    ProgressionHttp(xmlhttp);

    var url = "/Admin/ReceptionImageDestination";
    xmlhttp.open('POST', url);
    xmlhttp.send(data);
}

var EnvoyerImageBatiment = function () {

    var ThumbnailsBatiment = document.getElementById('ThumbnailsBatiment');
    var _file = document.getElementById('FichierBatiment');
    if (_file.files.length === 0) {
        return;
    }

    InitialisationEnvoi(_file);

    var data = new FormData();
    data.append('SelectedFile', _file.files[0]);

    var xmlhttp;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            BoutonVoirImage.href = "/Images/Batiments/" + _file.value;
            NomImage.innerHTML = _file.value;
            var Output = "";
            Output += "<div class='GrilleImages'>";
            Output += "<div class='thumbnail'>";
            Output += "<a href='/Images/Batiments/" + _file.value + "'>";
            Output += "<img src='/Images/Batiments/" + _file.value + "' class='Miniature' alt='' />";
            Output += "</a>";
            Output += "<br />";
            Output += "<div>";
            Output += "<button class='close pull-right' onclick='SupprimerImage(this, \"" + _file.value;
            Output += "\", \"/Admin/SupprimerImageBatiment\")'>&times;</button>";
            Output += "</div>";
            Output += "<br />";
            Output += "</div>";
            Output += "</div>";
            ThumbnailsBatiment.innerHTML += Output;
        }
    };

    ProgressionHttp(xmlhttp);

    var url = "/Admin/ReceptionImageBatiment";
    xmlhttp.open('POST', url);
    xmlhttp.send(data);
}

var EnvoyerImageActivite = function () {

    var ThumbnailsActivite = document.getElementById('ThumbnailsActivité');
    var _file = document.getElementById('FichierActivité');
    if (_file.files.length === 0) {
        return;
    }

    InitialisationEnvoi(_file);

    var data = new FormData();
    data.append('SelectedFile', _file.files[0]);

    var xmlhttp;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            BoutonVoirImage.href = "/Images/Activités/" + _file.value;
            NomImage.innerHTML = _file.value;
            var Output = "";
            Output += "<div class='GrilleImages'>";
            Output += "<div class='thumbnail'>";
            Output += "<a href='/Images/Activités/" + _file.value + "'>";
            Output += "<img src='/Images/Activités/" + _file.value + "' class='Miniature' alt='' />";
            Output += "</a>";
            Output += "<br />";
            Output += "<div>";
            Output += "<button class='close pull-right' onclick='SupprimerImage(this, \"" + _file.value;
            Output += "\", \"/Admin/SupprimerImageActivite\")'>&times;</button>";
            Output += "</div>";
            Output += "<br />";
            Output += "</div>";
            Output += "</div>";
            ThumbnailsActivite.innerHTML += Output;
        }
    };

    ProgressionHttp(xmlhttp);

    var url = "/Admin/ReceptionImageActivite";
    xmlhttp.open('POST', url);
    xmlhttp.send(data);
}

var InitialisationEnvoi = function (_file) {
    BoiteDeContenu.style.visibility = "visible";
    $('#BoiteDeContenu').modal('show');

    Image.file = _file.files[0];
    var reader = new FileReader();
    reader.onload = (function (aImg) {
        return function (e) {
            aImg.src = e.target.result;
        };
    })(Image);
    reader.readAsDataURL(_file.files[0]);
}

var ProgressionHttp = function (xmlhttp) {
    xmlhttp.upload.addEventListener('progress', function (e) {
        var ProgressValue = Math.ceil(e.loaded / e.total) * 100;
        if (ProgressValue > 10) {
            if (ProgressValue < 100) {
                _progress.style.width = ProgressValue + '%';
                _progress.innerHTML = ProgressValue + '%';
            }
            else {
                _progress.style.width = '100%';
                _progress.innerHTML = "Téléversement terminé";
            }
        }
    }, false);
}

var FermerForm = function () {
    var FileFormDestination = document.getElementById('FileFormDestination'),
    FileFormBatiment = document.getElementById('FileFormBatiment'),
    FileFormActivite = document.getElementById('FileFormActivité');

    FileFormDestination.reset();
    FileFormBatiment.reset();
    FileFormActivite.reset();

    BoiteDeContenu.style.visibility = "hidden";
}
