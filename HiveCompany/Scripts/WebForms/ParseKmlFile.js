

function parseDocumentKml(file, callback) {
    var fileReader = new FileReader();

    fileReader.onload = function (e) {
        result = extractGoogleCoords(e.target.result)

        for (let i = 0; i < result.polygons.length; i++) {
            const polygon = new google.maps.Polygon({
                paths: result.polygons[i],
                strokeColor: "#FF0000",
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: "#FF0000",
                fillOpacity: 0.35,
                clickable: false
            });

            polygon.setMap(map);
            polygons.push(polygon);
        }

        callback(); // chama o callback para informar que a leitura do arquivo foi concluída
    };

    fileReader.readAsText(file);
}


function extractGoogleCoords(plainText) {
    var parser = new DOMParser();
    var xmlDoc = parser.parseFromString(plainText, "text/xml");
    var googlePolygons = [];

    if (xmlDoc.documentElement.nodeName == "kml") {

        var cidade = '';
        var uf = '';

        for (var _i = 0, _a = xmlDoc.getElementsByTagName('Placemark'); _i < _a.length; _i++) {
            var item = _a[_i];


            var tagname = item.getElementsByTagName('name');
            if (tagname.length == 0)
                continue;
            var nome          = item.getElementsByTagName('name')[0].childNodes[0].nodeValue.trim();

            var polygons = item.getElementsByTagName('Polygon');

            if (polygons.length == 0)
                var polygons = item.getElementsByTagName('LineString');
            
            /** POLYGONS PARSE **/
            for (var _b = 0, polygons_1 = polygons; _b < polygons_1.length; _b++) {
                var polygon = polygons_1[_b];


                var coordinates_1 = polygon.getElementsByTagName('coordinates');

                for (var _c = 0, coordinate_1 = coordinates_1; _c < coordinates_1.length; _c++) {

                    var coords_1 = coordinate_1[_c].childNodes[0].nodeValue.trim();

                    var points = coords_1.split(" ");

                    if (points.length < 3)
                        continue;

                    var googlePolygonsPaths = [];

                    for (var _d = 0, points_1 = points; _d < points_1.length; _d++) {
                        var point = points_1[_d];

                        if (point != '') {
                            var coord = point.split(",");
                            googlePolygonsPaths.push({ lat: +coord[1], lng: +coord[0] });
                        }
                    }

                    const local1 = new Area(nome, cidade, uf, coords_1);
                    Poligonos.push(local1); /* meu array de areas que sera mandado para o servico */

                    googlePolygons.push(googlePolygonsPaths);
                }
            }
        }
    }
    else {
        throw "Erro ao converter arquivo";
    }    

    return { polygons: googlePolygons };
}

