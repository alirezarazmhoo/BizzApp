"use strict";

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

function _classPrivateFieldGet(receiver, privateMap) { var descriptor = _classExtractFieldDescriptor(receiver, privateMap, "get"); return _classApplyDescriptorGet(receiver, descriptor); }

function _classApplyDescriptorGet(receiver, descriptor) { if (descriptor.get) { return descriptor.get.call(receiver); } return descriptor.value; }

function _classPrivateFieldSet(receiver, privateMap, value) { var descriptor = _classExtractFieldDescriptor(receiver, privateMap, "set"); _classApplyDescriptorSet(receiver, descriptor, value); return value; }

function _classExtractFieldDescriptor(receiver, privateMap, action) { if (!privateMap.has(receiver)) { throw new TypeError("attempted to " + action + " private field on non-instance"); } return privateMap.get(receiver); }

function _classApplyDescriptorSet(receiver, descriptor, value) { if (descriptor.set) { descriptor.set.call(receiver, value); } else { if (!descriptor.writable) { throw new TypeError("attempted to set read only private field"); } descriptor.value = value; } }

var _totalAjax = new WeakMap();

var _tagId = new WeakMap();

var _fullUrl = new WeakMap();

class MainAjax {
    //-- total ajax history used to use in window.onpopstate event//
    //-- save tag id to use in window.onpopstate event//
    constructor() {
        _totalAjax.set(this, {
            writable: true,
            value: {}
        });

        _tagId.set(this, {
            writable: true,
            value: ''
        });

        _fullUrl.set(this, {
            writable: true,
            value: void 0
        });

        _defineProperty(this, "urlToObject", url => {
            let paramsObject = {}; //-- split url by question sign(?)//

            const urlCheckForPath = url.split('?'); //-- if params exist in url//

            if (urlCheckForPath[1]) url = urlCheckForPath[1]; //-- if params doesn't exist in url return an empty object//

            if (urlCheckForPath[0] && !urlCheckForPath[1]) return paramsObject; //-- split params by and sign(&) and save into paramsObject//

            url.split('&').reduce((a, b) => {
                let [key, val] = b.split('=');
                a[key] = val;
                return a;
            }, paramsObject);
            return paramsObject;
        });

        _defineProperty(this, "objectToUrl", (obj = {}) => {
            let url = '';

            for (let key in obj) {
                //-- check if key is first parameter to add question sign in begin of url//
                Object.keys(obj)[0] === key ? url += `?${key}=${obj[key]}` : url += `&${key}=${obj[key]}`;
            }

            return url;
        });

        _classPrivateFieldSet(this, _fullUrl, new URL(document.location.href));
    } //-- add new history to totalAjax//


    set totalAjax(ajaxRequest) {
        _classPrivateFieldGet(this, _totalAjax)[document.location.href] = ajaxRequest;
    }

    get totalAjax() {
        return _classPrivateFieldGet(this, _totalAjax);
    }

    set tagId(value) {
        this._tagId = value;
    }

    get tagId() {
        return _classPrivateFieldGet(this, _tagId);
    }

    set fullUrl(value) {
        _classPrivateFieldSet(this, _fullUrl, value);
    }

    get fullUrl() {
        return _classPrivateFieldGet(this, _fullUrl);
    } //-- create object to insert into totalAjax object//


    totalAjaxObjectCreator(method, url, isJsonResponse, body) {
        this.totalAjax = {
            method: method,
            url: url,
            isJsonResponse: isJsonResponse,
            body: body
        };
    }
    /*
    convert url to object
    ex :
    http://example.com/filter?type=post&id=5191 = to => {type: post, id: 5191}
    ?type=post&id=5191 = to => {type: post, id: 5191}
     */


}

var _currentParameters = new WeakMap();

var _qs = new WeakMap();

var _params = new WeakMap();

var _path = new WeakMap();

var _urlParameters = new WeakMap();

class SetUrlParams extends MainAjax {
    //-- new parameters//
    //-- url parameters//
    //-- new url parameters to add instead of old url parameters = like => ?type=post&id=5199 instead of old parameters ?type=product&id=1244//
    constructor() {
        super(); //-- get parameters from full url//

        _currentParameters.set(this, {
            writable: true,
            value: {}
        });

        _qs.set(this, {
            writable: true,
            value: void 0
        });

        _params.set(this, {
            writable: true,
            value: {}
        });

        _path.set(this, {
            writable: true,
            value: void 0
        });

        _urlParameters.set(this, {
            writable: true,
            value: ''
        });

        _defineProperty(this, "parseQueryString", () => {
            if (_classPrivateFieldGet(this, _qs)) _classPrivateFieldSet(this, _currentParameters, this.urlToObject(_classPrivateFieldGet(this, _qs)));
        });

        _defineProperty(this, "getParamByKey", key => {
            return _classPrivateFieldGet(this, _currentParameters)[key];
        });

        _defineProperty(this, "addUrlParameter", () => {
            const params = _classPrivateFieldGet(this, _params);

            for (let key in params) {
                if (!this.getParamByKey(key)) {
                    _classPrivateFieldGet(this, _currentParameters)[key] = params[key];
                } else if (this.getParamByKey(key) !== params[key]) {
                    this.replaceParam(key, params[key]);
                }
            }

            this.setFinalUrlParameters();
            window.history.pushState({}, '', _classPrivateFieldGet(this, _path) + _classPrivateFieldGet(this, _urlParameters));
        });

        _defineProperty(this, "replaceParam", (param, value) => {
            _classPrivateFieldGet(this, _currentParameters)[param] = value;
        });

        _defineProperty(this, "setFinalUrlParameters", () => {
            if (Object.keys(_classPrivateFieldGet(this, _currentParameters)).length > 0) {
                _classPrivateFieldSet(this, _urlParameters, this.objectToUrl(_classPrivateFieldGet(this, _currentParameters)));
            }
        });

        _classPrivateFieldSet(this, _qs, this.fullUrl.search.substr(1));

        this.parseQueryString();
    }

    getPathFromUrl(url) {
        return url.split('?')[0];
    }

    setPathAndParams(url) {
        _classPrivateFieldSet(this, _path, this.getPathFromUrl(url));

        _classPrivateFieldSet(this, _params, this.urlToObject(url));
    }

}

class AjaxRequest extends SetUrlParams {
    constructor(isChangeUrl = true) {
        super();

        _defineProperty(this, "_ajaxUrl", '/');

        _defineProperty(this, "_ajaxIsJsonResponse", false);

        _defineProperty(this, "_ajaxBody", {});

        _defineProperty(this, "_ajaxMethod", 'get');

        _defineProperty(this, "_isChangeUrl", void 0);

        _defineProperty(this, "post", (url, isJsonResponse = false, body = {}) => {
            this.setAjaxParams('post', url, isJsonResponse, body);
            return this.ajax('post', url, isJsonResponse, body);
        });

        _defineProperty(this, "delete", (url, isJsonResponse = false, body = {}) => {
            this.setAjaxParams('delete', url, isJsonResponse, body);
            return this.ajax('delete', url, isJsonResponse, body);
        });

        _defineProperty(this, "put", (url, isJsonResponse = false, body = {}) => {
            this.setAjaxParams('put', url, isJsonResponse, body);
            return this.ajax('put', url, isJsonResponse, body);
        });

        _defineProperty(this, "get", (url, isJsonResponse = false) => {
            this.setAjaxParams('get', url, isJsonResponse);
            return this.ajax('get', url, isJsonResponse);
        });

        _defineProperty(this, "xhr", () => {
            return new Promise(resolve => {
                const xhr = new XMLHttpRequest(),
                    //csrf = document.querySelector("#csrf").content,
                    data = new FormData(); //data.append("_token", csrf);
                //xhr.open(`${this._ajaxMethod}`, `${this._ajaxUrl}`, false);

                xhr.open(this._ajaxMethod, this._ajaxUrl, false);
                xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest'); //xhr.setRequestHeader('X-CSRF-TOKEN', csrf);

                if (this._ajaxIsJsonResponse) xhr.setRequestHeader('Accept', 'application/json');

                if (this._ajaxMethod !== 'get') {
                    if (this._ajaxBody !== {}) {
                        for (let key in this._ajaxBody) {
                            data.append(key, this._ajaxBody[key]);
                        }
                    }
                }

                if (this._ajaxMethod !== 'get') {
                    xhr.send(data);
                } else {
                    xhr.send();
                }

                return resolve({
                    readyState: xhr.readyState,
                    status: xhr.status,
                    response: xhr.response,
                    responseText: xhr.responseText
                });
            }).then(result => {
                return result;
            }).catch(err => console.error(err));
        });

        _defineProperty(this, "ajax", async () => {
            const result = await this.xhr();
            console.log(result);
            if (result.status !== 200) Promise.prototype.catch(() => `status code error : statusCode = ${result.status}
                        ${result.responseText}`);

            if (this._isChangeUrl) {
                this.setPathAndParams(this._ajaxUrl);
                this.totalAjaxObjectCreator(this._ajaxMethod, this.urlToObject(this._ajaxUrl), this._ajaxIsJsonResponse, this._ajaxBody);
                this.addUrlParameter();
            }

            return result.response;
        });

        this._isChangeUrl = isChangeUrl;
    }

    setAjaxParams(method, url, isJsonResponse = false, body = {}) {
        this._ajaxUrl = url;
        this._ajaxIsJsonResponse = isJsonResponse;
        this._ajaxBody = body;
        this._ajaxMethod = method;
    }

}

var _tagId2 = new WeakMap();

class Request extends AjaxRequest {
    constructor(tagId) {
        super();

        _tagId2.set(this, {
            writable: true,
            value: void 0
        });

        _defineProperty(this, "_urlParams", void 0);

        _defineProperty(this, "_params", void 0);

        _defineProperty(this, "disposeFilter", async (path, params = {}) => {
            
           
            this._params = params;
            this.setUrlParams();
            const url = path + this.objectToUrl(params);
            const data = await this.get(url, false);

            if (data) {
                this.renderData(data);
            }
        });

        _defineProperty(this, "setUrlParams", () => {
            this._urlParams = this.objectToUrl(this._params);
        });
        
        _defineProperty(this, "renderData", data => {
            document.querySelector(`#${_classPrivateFieldGet(this, _tagId2)}`).innerHTML = data; 
            $('.category-content__item__cat-product').each(function (i, value) {
                $(this).hover(function (e) {
                    
                    var isSponsor = $(this).data('assigned-issponsor');

                    var myIconReplc = L.Icon.extend({
                        options: {
                            iconUrl: "/Designs/MainWebSite/assets/img/location12.png",
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
                            iconUrl: "/Designs/MainWebSite/assets/img/14.png",
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
                            iconUrl: "/Designs/MainWebSite/assets/img/location11.png",
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
                            iconUrl: "/Designs/MainWebSite/assets/img/13.png",
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
                    // EDIT THIS TO POINT TO THE FILE AT http://www.charliecroom.com/marker_hole.png (or your own marker)
                    iconUrl: '/Designs/MainWebSite/assets/img/location11.png',
                    number: '',
                    shadowUrl: null,
                    iconSize: new L.Point(25, 41),
                    iconAnchor: new L.Point(13, 41),
                    popupAnchor: new L.Point(0, -33),

                    /*
                    iconAnchor: (Point)
                    popupAnchor: (Point)
                    */
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
                    iconUrl: "/Designs/MainWebSite/assets/img/13.png",
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

        });

        _classPrivateFieldSet(this, _tagId2, tagId);
      

    }

}

window.onpopstate = async () => {
   

    const urlParameters = window.location.href;
   

    if (request.totalAjax[urlParameters] && request.totalAjax[urlParameters].method === 'get') {
        
        const ajaxRequest = new AjaxRequest(false);
        const data = await ajaxRequest.get(urlParameters);
        request.renderData(data);
    }

};

