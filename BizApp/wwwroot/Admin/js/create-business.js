$(document).ready(function () {

	$("#file").change(function () {
		deleteFeatureImage(this.value);
		createFeatureImage(this.value);
	});

	//$('#featureImageRemoveButton').on('click', function () { deleteFeatureImage(null); });

	$("#saveButton").click(function (e) {
		var categoryId = $("#CategoryId").val();
		if (categoryId == 0) {
			e.preventDefault();

			$(window).scrollTop($('#autocomplete-ajax').position().top);
			$('#autocomplete-ajax').focus();
			return false;
		}

		var districtId = $("#DistrictId").val();
		if (districtId === 0) {
			e.preventDefault();

			$(window).scrollTop($('#autocomplete-district').position().top);
			$('#autocomplete-district').focus();
			return false;
		}
	});
});

function deleteFeatureImage(value = null) {
	$("#featureImageDiv").empty();
	$("#featureImageDiv").remove();

	// if is edited
	// delete file from server

}

function createFeatureImage(value) {
	var element = 
		`<div class="img-wrap" id="featureImageDiv">
			<span class="close" onclick="deleteFeatureImage()">&times;</span>
			<img style="width: 200px; height: auto;" src="` + value + `" width="500">
		</div>`;

	$('#featureImageMainElement').append(element);
}

// Initialize ajax autocomplete for categories:
// HELP: https://www.devbridge.com/sourcery/components/jquery-autocomplete/
$('#autocomplete-ajax').autocomplete({
			serviceUrl: '/admin/categories/getHierarchyNames',
	minChars: 3,
	paramName: 'searchString',
	transformResult: function (response) {
		var result = JSON.parse(response);
		return {
			suggestions: $.map(result, function (dataItem) {
				return {data: dataItem.value, value: dataItem.text };
			})
		};
	},
	type: "get",
	onSelect: function (suggestion) {
			$('#categorySelection').html('<b>دسته انتخاب شده: </b><i>' + suggestion.value + '</i>');
		$('#CategoryId').attr('value', suggestion.data);
	},
	onInvalidateSelection: function () {
			$('#categorySelection').html('دسته انتخاب شده: هیچ');
		$('#CategoryId').attr('value', 0);
	},
	showNoSuggestionNotice: true,
	noSuggestionNotice: "متاسفانه موردی پیدا نشد"

});

// Initailize ajax autocomplete for districts
$('#autocomplete-district').autocomplete({
			serviceUrl: '/admin/district/getAllWithParentNames',
	minChars: 3,
	paramName: 'searchString',
	transformResult: function (response) {
		var result = JSON.parse(response);
		return {
			suggestions: $.map(result, function (dataItem) {
				return {data: dataItem.value, value: dataItem.text };
			})
		};
	},
	type: "get",
	onSelect: function (suggestion) {
			$('#districtSelection').html('<b>ناحیه انتخاب شده: </b><i>' + suggestion.value + '</i>');
		$('#DistrictId').attr('value', suggestion.data);
	},
	onInvalidateSelection: function () {
			$('#districtSelection').html('دسته انتخاب شده: هیچ');
		$('#DistrictId').attr('value', 0);
	},
	showNoSuggestionNotice: true,
	noSuggestionNotice: "متاسفانه موردی پیدا نشد"

});


function LoadMap(lon, lat) {

	var theMarker = { };
	if (lon == 0 && lat == 0) {

			lon = 32.650823;
		lat = 51.668037;
	}
	var mymap = L.map('mapid').setView([lon, lat], 13);
	if (lon !== 0 && lat !== 0) {
			theMarker = L.marker([lon, lat]).addTo(mymap);

	}
	//var mymap = L.map('mapid').setView([32.650823, 51.668037], 13);
	//theMarker = L.marker([lon,lat], {icon: icon }).addTo(mymap);
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
		theMarker = L.marker([lat, lon], {icon: icon }).addTo(mymap);

		$("#latitude").val(e.latlng.lat);
		$("#longitude").val(e.latlng.lng);
	});
}
