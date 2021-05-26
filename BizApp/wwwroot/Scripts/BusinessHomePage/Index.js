


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
			$(element).children().children().css('fill', 'red');


		}
		else if (response.type == "authorize") {
			window.location = "/Identity/Account/Login";
		}
		else {
			var number = parseInt($(element).children().eq(2).text()) - 1;
			$(element).children().eq(2).text(number);
			$(element).children().children().css('fill', '');


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

				$(element).children().children().css('fill', 'orange');


			}
			else if (response.type == "authorize") {
				window.location = "/Identity/Account/Login";
			}
			else {
				var number = parseInt($(element).children().eq(2).text()) - 1;
				$(element).children().eq(2).text(number);
				$(element).children().children().css('fill', '');


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
				$(element).children().children().css('fill', 'green');

			}
			else if (response.type == "authorize") {
				window.location = "/Identity/Account/Login";
			}
			else {
				var number = parseInt($(element).children().eq(2).text()) - 1;
				$(element).children().eq(2).text(number);
				$(element).children().children().css('fill', '');

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
				$(element).children().children().eq(0).attr('class', 'fas fa-bookmark');

			}
			else if (response.type == "authorize") {
				window.location = "/Identity/Account/Login";
			}
			else {
				$("#addToFavoritResult").text("با موفقیت از لیست علاقه مندی حذف شد");
				$("#favoritModal").modal('toggle');
				$(element).children().children().eq(0).attr('class', 'far fa-bookmark');

			}
		}
	});
}

function sendMessageToBusiness() {
	if ( $("#title").val() === "" ) {
		$("#errorsendmessage").text("اطلاعات فرم ناقص است");
		return; 
	}
	var fd = new FormData();
	fd.append('FullName', $("#fullName").val());
	fd.append('Mobile', $("#mobile").val());
	fd.append('Message', $("#message").val());
	fd.append('Title', $("#title").val());
	fd.append('BusinessId', $("#businessId").val());
	$.ajax({
		type: "Post",
		url: '/BusinessHome/SendMessageToBusiness',
		dataType: "json",
		data: fd,
		contentType: false,
		processData: false,
		success: function (response) {
			$("#fullName").val('');
			$("#mobile").val('');
			$("#message").val('');
			$("#title").val('');
			$("#errorsendmessage").text("");

			$("#sendmessagetobusinessmodal").modal('toggle');
			$("#addToFavoritResult").text("پیام شما با موفقیت برای صاحب کسب و کار ارسال گردید");
			$("#favoritModal").modal('show');
		}
	});
}



