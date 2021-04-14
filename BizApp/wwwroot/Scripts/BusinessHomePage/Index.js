


function changeUsefull(element) {

$.ajax({
	type: "Post",
	url: '/Review/ChangeUseFullCount?Id=' + $(element).data('assigned-id') + '',
	dataType: "json",
	contentType: false,
	processData: false,
	success: function (response) {
		if (response.type == "add") {
			var number = parseInt($(element).children().eq(2).text()) + 1;
			$(element).children().eq(2).text(number);
		}
		else {
			var number = parseInt($(element).children().eq(2).text()) - 1;
			$(element).children().eq(2).text(number);
		}
	}
});
}
function changeFunny(element) {

	$.ajax({
		type: "Post",
		url: '/Review/ChangeFunnyCount?Id=' + $(element).data('assigned-id') + '',
		dataType: "json",
		contentType: false,
		processData: false,
		success: function (response) {
			if (response.type == "add") {
				var number = parseInt($(element).children().eq(2).text()) + 1;
				$(element).children().eq(2).text(number);
			}
			else {
				var number = parseInt($(element).children().eq(2).text()) - 1;
				$(element).children().eq(2).text(number);
			}
		}
	});
}
function changeCool(element) {
	$.ajax({
		type: "Post",
		url: '/Review/ChangeCoolCount?Id=' + $(element).data('assigned-id') + '',
		dataType: "json",
		contentType: false,
		processData: false,
		success: function (response) {
			if (response.type == "add") {
				var number = parseInt($(element).children().eq(2).text()) + 1;
				$(element).children().eq(2).text(number);
			}
			else {
				var number = parseInt($(element).children().eq(2).text()) - 1;
				$(element).children().eq(2).text(number);
			}
		}
	});
}

function BusinessHomeLoadMap(lon, lat) {

	var theMarker = {};
	if (lon == 0 && lat == 0) {
		lon = 51.668037;
		lat = 32.650823;
	}
	var mymap = L.map('businesshomemapid').setView([lat, lon], 13);
	if (lon !== 0 && lat !== 0) {
		theMarker = L.marker([lat, lon]).addTo(mymap);
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
	
}

function AddOrRemvoeFavorit(element) {
	$.ajax({
		type: "Post",
		url: '/UserFavorits/AddOrRemove?Id='+ $(element).data('assigned-id') + '',
		dataType: "json",
		contentType: false,
		processData: false,
		success: function (response) {
			if (response.type == "add") {
				$("#addToFavoritResult").text("با موفقیت به لیست علاقه مندی افزوده شد");
				$("#favoritModal").modal('show');
			}
			else {
				$("#addToFavoritResult").text("با موفقیت از لیست علاقه مندی حذف شد");
				$("#favoritModal").modal('toggle');

			}
		}
	});
}