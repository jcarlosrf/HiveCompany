var map;

var polygons = [];

var Poligonos = [];

class Area {
    constructor(nome, cidade, uf, coordenadas) {
        this.nome = nome;
        this.cidade = cidade;
        this.uf = uf;
        this.coordenadas = coordenadas;
    }
}

window.initMap = initMap;

function initMap() {

    var latlng = new google.maps.LatLng(-15.7217003, -48.1021774);

    var options = {
        zoom: 5,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("mapa"), options);

    let fileUpload = $('.kmlFile');


    fileUpload[0].onchange = function (e) {       
        var count = 0; // variável contador inicializada em 0

        for (let i = 0; i < this.files.length; i++) {
            let input = this.files[i];
            parseDocumentKml(input, function () {
                count++; // incrementa o contador quando a leitura do arquivo é concluída

                if (count == fileUpload[0].files.length) { // verifica se todos os arquivos foram lidos
                    saveSessionPoligonos(); // chama a função apenas quando todos os arquivos foram lidos
                }
            });
        }
    };

    $('.btn-salvar-areas').click(function () {
        salvarAreas();
    });    
}

function saveSessionPoligonos() {
    var geocoder = new google.maps.Geocoder();

    if (Poligonos.length == 0)
        return;

    var points = Poligonos[0].coordenadas.split(" ");
    var coord = points[0].split(",");
    var latLng = new google.maps.LatLng(+coord[1], +coord[0]);

    var newOptions = {
        zoom: 10,
        center: latLng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map.setOptions(newOptions);

    // Utiliza a função reverseGeocode() para buscar cidade e UF da coordenada
    geocoder.geocode({ 'location': latLng }, function (results, status) {
        if (status === 'OK') {
            if (results[0]) {

                // Busca na lista de componentes do resultado o nome da cidade e UF
                for (var _d = 0, _e = results[0].address_components; _d < _e.length; _d++) {
                    var component = _e[_d];
                    if (component.types.indexOf("locality") !== -1 || component.types.indexOf("administrative_area_level_2") !== -1) {
                        cidade = component.long_name;
                    }
                    if (component.types.indexOf("administrative_area_level_1") !== -1) {
                        uf = component.short_name;
                    }
                }

                Poligonos[0].cidade = cidade;
                Poligonos[0].uf = uf;

                var retorno;
                var parametros = JSON.stringify({ "areas": Poligonos });

                $.ajax({
                    type: 'POST'

                    , url: "./services/wsareas.asmx/SaveSessionPolygon"
                    , contentType: 'application/json; charset=utf-8'
                    , dataType: 'json'
                    , data: parametros
                    , beforeSend: function () {
                        $(".upgProgress").show("fast");
                    }
                    , complete: function () {
                        $(".upgProgress").hide("slow");
                    }
                    , success: function (data, status) {
                        //Tratando o retorno com parseJSON                                 
                        retorno = $.parseJSON(data.d);

                        if (retorno == "") {
                            var btnSalvar = document.querySelector('.btn-salvar-areas');
                            // Habilita o botão
                            btnSalvar.disabled = false;
                            Poligonos = [];
                        }
                        else {
                            alert(retorno);
                        }
                    }
                });
            }
        }
    }); // fim do geocoder

}


function salvarAreas() {
    var retorno;
    
    $.ajax({
        type: 'POST'

        , url: "./services/wsareas.asmx/SaveBdPolygon"
        , contentType: 'application/json; charset=utf-8'
        , dataType: 'json'
        , beforeSend: function () {
            $(".upgProgress").show("fast");
        }
        , complete: function () {
            $(".upgProgress").hide("slow");
        }
        , success: function (data, status) {
            //Tratando o retorno com parseJSON                                 
            retorno = $.parseJSON(data.d);

            if (retorno == "") {
                alert("Gravado com sucesso!");

                var btnSalvar = document.querySelector('.btn-salvar-areas');
                // Habilita o botão
                btnSalvar.disabled = true;
                const polygons = [];
                const Poligonos = [];

                var options = {
                    zoom: 5,
                    center: latlng,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };

                // Crie um novo objeto Map e vincule-o ao mesmo elemento HTML
                map = new google.maps.Map(document.getElementById("mapa"), options);
            }
            else {
                alert(retorno);
            }
        }
    });
}