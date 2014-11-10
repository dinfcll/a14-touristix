var EnvoyerImageDestination = function () {

    var _file = document.getElementById('FichierDestination');

    if (_file.files.length === 0) {
        return;
    }

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
            BoutonVoirImage.href = "Images/Destinations/" + _file.value;
            NomImage.innerHTML = _file.value;
        }
    };

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

    var url = "/Admin/ReceptionImageDestination";
    xmlhttp.open('POST', url);
    xmlhttp.send(data);
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
