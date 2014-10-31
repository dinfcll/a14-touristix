function MessageBox(id, txturl) {
    var ret = confirm("Souhaitez vous réellement effacer cet enregistrement?");
    if (ret == true) {
        var idMessage = { "id": id };
        $.ajax({
            type: "POST",
            url: txturl,
            data: idMessage,
            datatype: "html",
            success: function () {
                window.location.reload();
            },
            error: function(){
                alert("erreur");
            }
        });
    }
}
