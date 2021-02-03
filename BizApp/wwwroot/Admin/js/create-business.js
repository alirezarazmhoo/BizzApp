$(document).ready(function () {
	LoadMap(32.650823, 51.668037);
	$("#provinceId").change(function () {
		// clear city dropdownl
		$('#cityId').empty();

		// set default value
		var nonVauleItem = '<option value="0">لطفا شهر را انتخاب کنید</option>';
		$('#cityId').html(nonVauleItem);

		// check if province not selected
		if (this.value == '0') return;

		var url = '/admin/city/getcities';

		$.getJSON(url, { provinceId: this.value }, function (data) {
			var items = nonVauleItem;

			$.each(data, function (i, model) {
				items += '<option value="' + model.value + '">' + model.text + '</option>';
			});

			$('#cityId').html(items);
		});
	});

	$("#cityId").change(function () {
		// clear district dropdown
		$('#districtId').empty();

		// set default value for dropdwon
		var nonVauleItem = '<option value="0">لطفا ناحیه را انتخاب کنید</option>';
		$('#districtId').html(nonVauleItem);

		// check if province not selected
		if (this.value == '0') return;

		var url = '/admin/district/getdistricts';

		$.getJSON(url, { cityId: this.value }, function (data) {
			var items = nonVauleItem;

			$.each(data, function (i, model) {
				items += '<option value="' + model.value + '">' + model.text + '</option>';
			});

			$('#districtId').html(items);
		});
	});

	$("#districtId").change(function () {
		$("span[data-valmsg-for='districtId']").html('');
	});

	$("#saveButton").click(function (e) {
		var categoryId = $("#categoryId").val();
		if (categoryId == 0) {
			e.preventDefault();

			$(window).scrollTop($('#autocomplete-ajax').position().top);
			$('#autocomplete-ajax').focus();
			return false;
		}
	});
});

// Initialize ajax autocomplete:
// HELP: https://www.devbridge.com/sourcery/components/jquery-autocomplete/
$('#autocomplete-ajax').autocomplete({
	serviceUrl: '/admin/categories/getHierarchyNames',
	minChars: 3,
	paramName: 'searchString',
	transformResult: function (response) {
		var result = JSON.parse(response);
		return {
			suggestions: $.map(result, function (dataItem) {
				return { data: dataItem.value, value: dataItem.text };
			})
		};
	},
	type: "get",
	onSelect: function (suggestion) {
		$('#categorySelection').html('<b>دسته انتخاب شده: </b><i>' + suggestion.value + '</i>');
		$('#categoryId').attr('value', suggestion.data);
	},
	onInvalidateSelection: function () {
		$('#categorySelection').html('دسته انتخاب شده: هیچ');
		$('#categoryId').attr('value', 0);
	},
	showNoSuggestionNotice: true,
	noSuggestionNotice: "متاسفانه موردی پیدا نشد"

});

function LoadMap(lon, lat) {

	var theMarker = {};
	if (lon == 0 && lat == 0) {

		lon = 32.650823;
		lat = 51.668037;
	}
	var mymap = L.map('mapid').setView([lon, lat], 13);
	if (lon !== 0 && lat !== 0) {
		theMarker = L.marker([lon, lat]).addTo(mymap);

	}
	//var mymap = L.map('mapid').setView([32.650823, 51.668037], 13);
	//theMarker = L.marker([lon,lat], { icon: icon }).addTo(mymap);
	L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
		attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
		maxZoom: 18,
		id: 'mapbox/streets-v11',
		tileSize: 512,
		zoomOffset: -1,
		accessToken: 'pk.eyJ1IjoiYWxpcmV6YXJhem1qb28iLCJhIjoiY2s1Yzg2aTM4MWo1bjNvcDN2dWQwbGs5byJ9.0MqeBvs7xijOfpnGE73R_A'
	}).addTo(mymap);
	mymap.on('click', function (e) {
		var icon = new L.Icon.Default();
		icon.options.shadowSize = [0, 0];
		lat = e.latlng.lat;
		lon = e.latlng.lng;
		if (theMarker !== undefined) {

			mymap.removeLayer(theMarker);
		}
		theMarker = L.marker([lat, lon], { icon: icon }).addTo(mymap);

		$("#latitude").val(e.latlng.lat);
		$("#longitude").val(e.latlng.lng);
	});
}
