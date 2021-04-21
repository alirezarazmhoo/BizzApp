var coll = document.getElementsByClassName("collapsible");
var i;
for (i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("activeProv");
        var content = this.nextElementSibling;
        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            content.style.maxHeight = content.scrollHeight + "px";
        }
    });
}
//vaghti ke modal category baz mishavad
function OpenCategoryModal() {
    $('#click-cat').modal('show');
    var parseQueryString = function () {
        var str = window.location.search;
        var objURL = {};
        str.replace(
            new RegExp("([^?=&]+)(=([^&]*))?", "g"),
            function ($0, $1, $2, $3) {
                objURL[$1] = $3;
            }
        );
        return objURL;
    };
    var params = parseQueryString();
    if (params["catsFinder"] != undefined) {
        $('#click-cat .category-content .form-check-label').each(function () {
            if (decodeURIComponent(params["catsFinder"].toString()).includes($.trim($(this).text().replaceAll(" ", "-"))))
                $(this).prev().prop('checked', 'checked')
        });
    }
}
//vaghti ke modal feature baz mishavad
function OpenFeatureModal() {
    $('#click-featu').modal('show');
    var parseQueryString = function () {
        var str = window.location.search;
        var objURL = {};
        str.replace(
            new RegExp("([^?=&]+)(=([^&]*))?", "g"),
            function ($0, $1, $2, $3) {
                objURL[$1] = $3;
            }
        );
        return objURL;
    };
    var params = parseQueryString();
    if (params["featuFinder"] != undefined) {
        $('#click-featu .category-content .form-check-label').each(function () {
            if (decodeURIComponent(params["featuFinder"].toString()).includes($.trim($(this).text().replaceAll(" ", "-"))))
                $(this).prev().prop('checked', 'checked')
        });
    }
}
//vaghti ke modal province baz mishavad
function OpenProvModal() {
    $('#click-prov').modal('show');
    var parseQueryString = function () {
        var str = window.location.search;
        var objURL = {};
        str.replace(
            new RegExp("([^?=&]+)(=([^&]*))?", "g"),
            function ($0, $1, $2, $3) {
                objURL[$1] = $3;
            }
        );
        return objURL;
    };
    var params = parseQueryString();
    if (decodeURIComponent(params["districtFinder"]).toString() != undefined) {
        var match = decodeURIComponent(params["districtFinder"]).toString().split(',');
        $('.contentPro .form-check-input').prop('checked', false);
        for (var a in match) {
            $('#click-prov .Prov-content  .collapsible').each(function () {
                if (match[a] == $.trim($(this).text().replaceAll(" ", "-")))
                    $(this).nextAll().has(":checkbox").first().find(":checkbox").prop('checked', true);
            });
        }
    }
}
var pageN = 1;
const markerHtmlStyles = "background-color:#000; width: 3rem; height: 3rem; display: block;  left: -1.5rem;  top: -1.5rem;  position: relative;  border-radius: 3rem 3rem 0;  transform: rotate(45deg);  border: 1px solid #FFFFFF";
$(document).ready(function () {
    const url = new URL(window.location);
    var array = url.href.substring(url.href.indexOf("&") + 1)
    GetData("/Search/AllBussiness?CategoryId=" + CatIdFromUrl+"&" + array + "", "AllBussiness");
    findInUrlAndCheck();
    window.history.replaceState({}, '', url);
    $('.category-content__item-filter__property .form-check-featu').change(function () {
        var featursFinder = addParameterToUrl(null, $(this).next().text().replaceAll(" ", "-"));
        const url = new URL(window.location);
        if (featursFinder != undefined)
            url.searchParams.set('featuFinder', featursFinder);
        else
            url.searchParams.delete('featuFinder', featursFinder);
        GetData("/Search/AllBussiness" + url.search, "AllBussiness");
        window.history.replaceState({}, '', url);
    });
    $('.category-content__item-filter__areas .form-check-featu').change(function () {
        var pro = addParameterToUrl(null, null, $(this).next().text().replaceAll(" ", "-"));
        const url = new URL(window.location);
        if (pro != undefined)
            url.searchParams.set('districtFinder', pro);
        else
            url.searchParams.delete('districtFinder', pro);
        GetData("/Search/AllBussiness" + url.search, "AllBussiness");
        window.history.replaceState({}, '', url);

    });
});
//load partial asli va load map again
function GetData(ActionName, Target, Data) {
    $.ajax({
        type: "GET",
        data: Data,
        url: "" + ActionName + "",
        dataType: "html",
        success: function (data) {
            $('#' + Target + '').html('');
            $('#' + Target + '').html(data);
            $('#' + Target + '').show();

            $('.category-content__item__cat-product').each(function (i, value) {
                $(this).hover(function (e) {

                    var isSponsor = $(this).data('assigned-issponsor');

                    var myIconReplc = L.Icon.extend({
                        options: {
                            iconUrl: "/Upload/DefaultPicutres/Map/location12.png",
                            shadowUrl: null,
                            iconSize: new L.Point(25, 41),
                            iconAnchor: new L.Point(13, 41),
                            popupAnchor: new L.Point(0, -33),
                            className: 'leaflet-div-icon'
                        }
                        ,
                        createIcon: function () {
                            var div = document.createElement('div');

                            var img = this._createImg(this.options['iconUrl']);
                            var numdiv = document.createElement('div');
                            numdiv.setAttribute("class", "number");
                            numdiv.innerHTML = this.options['number'] || '';
                            div.appendChild(img);
                            div.appendChild(numdiv);
                            this._setIconStyles(div, 'icon');
                            return div;
                        },
                        createShadow: function () {
                            return null;
                        }
                    });
                    var myIconReplcIsSponsor = L.Icon.extend({
                        options: {
                            iconUrl: "/Upload/DefaultPicutres/Map/14.png",
                            shadowUrl: null,
                            iconSize: new L.Point(25, 41),
                            iconAnchor: new L.Point(13, 41),
                            popupAnchor: new L.Point(0, -33),
                            className: 'leaflet-div-icon'
                        }
                        ,
                        createIcon: function () {
                            var div = document.createElement('div');

                            var img = this._createImg(this.options['iconUrl']);
                            var numdiv = document.createElement('div');
                            numdiv.setAttribute("class", "number");
                            numdiv.innerHTML = this.options['number'] || '';
                            div.appendChild(img);
                            div.appendChild(numdiv);
                            this._setIconStyles(div, 'icon');
                            return div;
                        },
                        createShadow: function () {
                            return null;
                        }
                    });
                    if (isSponsor == 'False')
                        window.arr1[$(this).attr('id') - 1].setIcon(new myIconReplc({ number: "&nbsp" + $(this).attr('id') }));
                    else
                        window.arr1[$(this).attr('id') - 1].setIcon(new myIconReplcIsSponsor({ number: "&nbsp" + $(this).attr('id') }));


                }, function () {
                    var isSponsor = $(this).data('assigned-issponsor');

                    var myIconReplc = L.Icon.extend({
                        options: {
                            iconUrl: "/Upload/DefaultPicutres/Map/location11.png",
                          
                            shadowUrl: null,
                            iconSize: new L.Point(25, 41),
                            iconAnchor: new L.Point(13, 41),
                            popupAnchor: new L.Point(0, -33),
                            className: 'leaflet-div-icon'
                        },
                        createIcon: function () {
                            var div = document.createElement('div');
                            var img = this._createImg(this.options['iconUrl']);
                            var numdiv = document.createElement('div');
                            numdiv.setAttribute("class", "number");
                            numdiv.innerHTML = this.options['number'] || '';
                            div.appendChild(img);
                            div.appendChild(numdiv);
                            this._setIconStyles(div, 'icon');
                            return div;
                        },
                        createShadow: function () {
                            return null;
                        }

                    });
                    var myIconReplcIsSponsor = L.Icon.extend({
                        options: {
                            iconUrl: "/Upload/DefaultPicutres/Map/13.png",
                            shadowUrl: null,
                            iconSize: new L.Point(25, 41),
                            iconAnchor: new L.Point(13, 41),
                            popupAnchor: new L.Point(0, -33),
                            className: 'leaflet-div-icon'
                        }
                        ,
                        createIcon: function () {
                            var div = document.createElement('div');

                            var img = this._createImg(this.options['iconUrl']);
                            var numdiv = document.createElement('div');
                            numdiv.setAttribute("class", "number");
                            numdiv.innerHTML = this.options['number'] || '';
                            div.appendChild(img);
                            div.appendChild(numdiv);
                            this._setIconStyles(div, 'icon');
                            return div;
                        },
                        createShadow: function () {
                            return null;
                        }
                    });
                    if (isSponsor == 'False')

                        window.arr1[$(this).attr('id') - 1].setIcon(new myIconReplc({ number: "&nbsp" + $(this).attr('id') }));
                    else
                        window.arr1[$(this).attr('id') - 1].setIcon(new myIconReplcIsSponsor({ number: "&nbsp" + $(this).attr('id') }));

                });
            });
            var markerArray = [];
            var mymap = null;
            var marker = null;
            document.getElementById('weathermap').innerHTML = "<div id='mapid' style='width: 100%; height: 100%;'></div>";
            var planes = [];
            var lat = 0;
            var lon = 0;
            $('.category-content__item__cat-product').each(function (i, obj) {
                lat = $(this).data('assigned-lat');
                lon = $(this).data('assigned-lon');
                planes.push([$(this).data('assigned-datas'), $(this).data('assigned-lat'), $(this).data('assigned-lon'), $(this).data('assigned-count'), $(this).data('assigned-issponsor')]);
            });
            mymap = L.map('mapid').setView([lat, lon], 8);
            L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
                attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
                maxZoom: 18,
                id: 'mapbox/streets-v11',
                tileSize: 512,
                zoomOffset: -1,
                accessToken: 'your.mapbox.access.token'
            }).addTo(mymap);
            L.NumberedDivIcon = L.Icon.extend({
                options: {
                    iconUrl: '/Upload/DefaultPicutres/Map/location11.png',

                    number: '',
                    shadowUrl: null,
                    iconSize: new L.Point(25, 41),
                    iconAnchor: new L.Point(13, 41),
                    popupAnchor: new L.Point(0, -33),
                    className: 'leaflet-div-icon'
                },
                createIcon: function () {
                    var div = document.createElement('div');

                    var img = this._createImg(this.options['iconUrl']);

                    var numdiv = document.createElement('div');
                    numdiv.setAttribute("class", "number");
                    numdiv.innerHTML = this.options['number'] || '';
                    div.appendChild(img);
                    div.appendChild(numdiv);

                    this._setIconStyles(div, 'icon');

                    return div;
                },
                createShadow: function () {
                    return null;
                }
            });
            var myIconReplc1 = L.Icon.extend({
                options: {
                    iconUrl: "/Upload/DefaultPicutres/Map/13.png",

                    shadowUrl: null,
                    iconSize: new L.Point(25, 41),
                    iconAnchor: new L.Point(13, 41),
                    popupAnchor: new L.Point(0, -33),
                    className: 'leaflet-div-icon'
                },
                createIcon: function () {
                    var div = document.createElement('div');

                    var img = this._createImg(this.options['iconUrl']);

                    var numdiv = document.createElement('div');
                    numdiv.setAttribute("class", "number");
                    numdiv.innerHTML = this.options['number'] || '';
                    div.appendChild(img);
                    div.appendChild(numdiv);

                    this._setIconStyles(div, 'icon');

                    return div;
                },
                createShadow: function () {
                    return null;
                }
            });

            for (var i = 0; i < planes.length; i++) {


                if (planes[i][4] == 'False') {
                    marker = new L.marker([planes[i][1], planes[i][2]], {
                        icon: new L.NumberedDivIcon({
                            number: "&nbsp" + planes[i][3]
                        })
                    }).bindPopup(planes[i][0]).addTo(mymap);
                } else {

                    marker = new L.marker([planes[i][1], planes[i][2]], {
                        icon: new myIconReplc1({
                            number: "&nbsp" + planes[i][3]
                        })
                    }).bindPopup(planes[i][0]).addTo(mymap);
                }

                marker.on('mouseover', function (e) {
                    this.openPopup(); //this.setIcon(new myIconReplc2({ number: this.options.icon.options.number }))
                });
                marker.on('mouseout', function (e) {
                    this.closePopup(); //this.setIcon(new myIconReplc1({ number: this.options.icon.options.number }))
                });
                markerArray[pageN * 10 - 10 + i] = marker;
            }
            window.arr1 = markerArray;

            //}
        }
    });

}
//vaghti ke category ha change mishavand dar safe search
function SearchByCat(cats) {
    $(".category-content__item-filter__property").find(".lbl-cat").each(function () {
        if ($.trim($(this).text().replaceAll(" ", "-")) == $.trim(cats))
            $(this).toggleClass("active-lbl-cat");
    });
    var catsFinder = addParameterToUrl($.trim(cats));
    const url = new URL(window.location);
    if (catsFinder != undefined)
        url.searchParams.set('catsFinder', catsFinder);
    else
        url.searchParams.delete('catsFinder', catsFinder);
    GetData("/Search/AllBussiness" + url.search, "AllBussiness");
    window.history.replaceState({}, '', url);
}
//paramert ha be url ezafe mishavand
function addParameterToUrl(cats, featu, dist) {
    var catsFinder = "";
    var featuFinder = "";
    var distFinder = "";
    const urlParameters = window.location.href;
    var parseQueryString = function () {
        var str = window.location.search;
        // find href and then concat other category
        var objURL = {};
        str.replace(
            new RegExp("([^?=&]+)(=([^&]*))?", "g"),
            function ($0, $1, $2, $3) {
                objURL[$1] = $3;
            }
        );
        return objURL;
    };
    var params = parseQueryString();
    if (cats != null) {
        if (params["catsFinder"] == undefined)
            catsFinder = cats;
        else {
            var urlCats = decodeURIComponent(params["catsFinder"].toString());
            if (urlCats == cats + ",") {
                urlCats = urlCats.replace(urlCats, '');
            }
            else if (!urlCats.includes($.trim(cats)))
                catsFinder = urlCats + "," + cats;
            else {
                var strCats = urlCats;
                catsFinder = strCats.replace($.trim(cats), '');
                catsFinder = catsFinder.trim().replace(/,{1,}$/, '');
            }
        }
        if (catsFinder == "")
            return undefined;
        else
            return catsFinder;

    }
    if (featu != null) {
        if (params["featuFinder"] == undefined)
            featuFinder = featu;
        else {

            if (!decodeURIComponent(params["featuFinder"].toString()).includes($.trim(featu)))
                featuFinder = decodeURIComponent(params["featuFinder"].toString()) + "," + featu;
            else {

                var strfeatu = decodeURIComponent(params["featuFinder"].toString());
                featuFinder = strfeatu.replace($.trim(featu), '');
                featuFinder = featuFinder.trim().replace(/,{1,}$/, '');
            }

        }
        if (featuFinder == "")
            return undefined;
        else
            return featuFinder;

    }
    if (dist != null) {
        if (params["districtFinder"] == undefined)
            distFinder = dist;
        else {

            if (!decodeURIComponent(params["districtFinder"].toString()).includes($.trim(dist)))
                distFinder = decodeURIComponent(params["districtFinder"].toString()) + "," + dist;
            else {

                var strdist = decodeURIComponent(params["districtFinder"].toString());
                distFinder = strdist.replace($.trim(dist), '');
                distFinder = distFinder.trim().replace(/,{1,}$/, '');
            }

        }
        if (distFinder == "")
            return undefined;
        else
            return distFinder;

    }
}
//safe ke load mishe motavageh beshe ke kodoom parametr ha dar url hastan hamoon ha ro dar safe tik bezanad
function findInUrlAndCheck() {
    var parseQueryString = function () {
        var str = window.location.search;
        // find href and then concat other category
        var objURL = {};
        str.replace(
            new RegExp("([^?=&]+)(=([^&]*))?", "g"),
            function ($0, $1, $2, $3) {
                objURL[$1] = $3;
            }
        );
        return objURL;
    };
    var params = parseQueryString();
    if (params["catsFinder"] != undefined) {
        var match = decodeURIComponent(params["catsFinder"]).toString().split(',');
        $(".category-content__item-filter__property").find(".lbl-cat").each(function () {
            if (match.indexOf($.trim($(this).text().replaceAll(" ", "-"))) > -1)
                $(this).addClass("active-lbl-cat");
            else {
                $(this).removeClass("active-lbl-cat");
            }
        });
    }
    if (params["featuFinder"] != undefined) {
        var match = decodeURIComponent(params["featuFinder"]).toString().split(',');
        $(".category-content__item-filter__property").find(".form-check").each(function () {
            if (match.indexOf($.trim($(this).find('.form-check-label').text().replaceAll(" ", "-"))) > -1)
                $(this).find(":checkbox").prop('checked', true);
            else
                $(this).find(":checkbox").prop('checked', false);
        });
    }
    if (params["districtFinder"] != undefined) {
        var match = decodeURIComponent(params["districtFinder"]).toString().split(',');
        for (var a in match) {
            $(".category-content__item-filter__areas").find(".form-check").each(function () {
                if (match.indexOf($.trim($(this).find('.form-check-label').text().replaceAll(" ", "-"))) > -1)
                    $(this).find(":checkbox").prop('checked', true);
                else
                    $(this).find(":checkbox").prop('checked', false);
            });
        }
    }
}
//pagination dakhele partial change shavad call mishavad
function ChangePage(page) {
    pageN = page;
    const url = new URL(window.location);
    url.searchParams.set('page', page);
    GetData("/Search/AllBussiness" + url.search, "AllBussiness");
    window.history.replaceState({}, '', url);
}
//dokme search dakhele modale category ke click shod call mishavad
function SearchByCategoty() {
    var catsFinder = "";
    $('#click-cat .category-content input[type=checkbox]:checked').each(function () {
        catsFinder += $(this).next().text().replaceAll(" ", "-") + ",";
    });
    catsFinder = catsFinder.slice(0, -1);
    const url = new URL(window.location);
    if (catsFinder != undefined)
        url.searchParams.set('catsFinder', catsFinder);
    else
        url.searchParams.delete('catsFinder', catsFinder);
    GetData("/Search/AllBussiness" + url.search, "AllBussiness");
    window.history.replaceState({}, '', url);
    $("#click-cat").modal('hide');
    $('.modal-backdrop').remove();
    findInUrlAndCheck();
}
//dokme search dakhele modale Feature ke click shod call mishavad
function SearchByFeature() {
    var featuFinder = "";
    $('#click-featu .category-content input[type=checkbox]:checked').each(function () {
        featuFinder += $(this).next().text().replaceAll(" ", "-") + ",";
    });
    featuFinder = featuFinder.slice(0, -1);
    const url = new URL(window.location);
    if (featuFinder != undefined)
        url.searchParams.set('featuFinder', featuFinder);
    else
        url.searchParams.delete('featuFinder', featuFinder);
    GetData("/Search/AllBussiness" + url.search, "AllBussiness");
    window.history.replaceState({}, '', url);
    $("#click-featu").modal('hide');
    $('.modal-backdrop').remove();
    findInUrlAndCheck();
}
//dokme search dakhele modale province ke click shod call mishavad
function SearchByPro() {
    var districtFinder = "";
    $('#click-prov .Prov-content input[type=checkbox]:checked').each(function () {
        districtFinder += $(this).next().text().replaceAll(" ", "-") + ",";
    });
    districtFinder = districtFinder.slice(0, -1);
    const url = new URL(window.location);
    if (districtFinder != undefined)
        url.searchParams.set('districtFinder', districtFinder);
    else
        url.searchParams.delete('districtFinder', districtFinder);
    GetData("/Search/AllBussiness" + url.search, "AllBussiness");
    window.history.replaceState({}, '', url);
    $("#click-prov").modal('hide');
    $('.modal-backdrop').remove();
    findInUrlAndCheck();
}
//back moroor gar ke zade shod call mishavad
$(window).on('popstate', function (event) {
    const url = new URL(window.location);
    var array = url.href.substring(url.href.indexOf("&") + 1)
    GetData("/Search/AllBussiness?CategoryId=" + CatIdFromUrl+"&" + array + "", "AllBussiness");
    window.history.replaceState({}, '', url);
    findInUrlAndCheck();
});
//forward moroor gar ke zade shod call mishavad
$(window).on('pushstate', function (event) {
    const url = new URL(window.location);
    var array = url.href.substring(url.href.indexOf("&") + 1)
    GetData("/Search/AllBussiness?CategoryId=" + CatIdFromUrl+"&" + array + "", "AllBussiness");
    window.history.replaceState({}, '', url);
    findInUrlAndCheck();
});