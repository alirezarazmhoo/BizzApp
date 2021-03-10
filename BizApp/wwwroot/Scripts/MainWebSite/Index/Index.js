//import { read } from "fs/promises";

function AutoComplete(parameter) {
	var input = document.getElementById('idTxtCategorySearch');
	if (input.value.length == 0) {
		$('#cmbSearchCategoryAlternative').empty();
		$('#cmbSearchCategoryAlternative').css("display", "none");
		$('#cmbSearchCategoryPrimary').css("display", "block");
	}
	else {
		$('#cmbSearchCategoryPrimary').css("display", "none");
		$('#cmbSearchCategoryAlternative').empty();
		$('#cmbSearchCategoryAlternative').css("display", "block");
		$.ajax({
			type: "GET",
			url: '/Home/SearchCategory?txtSearch=' + parameter + '',
			dataType: "json",
			contentType: false,
			processData: false,
			success: function (response) {
				$.each(response.categories, function () {
					$('#cmbSearchCategoryAlternative').append($("<li onclick='FillCategorySearchInput(\"" + this.name + "\")'><a href=#this.id style='margin-right:15px'>" + this.name + "<a><li>"));
				});

			},
		});
	}
}

function CityAutoComplete(parameter) {
	var res = ""; 
	var finalres = "";
	$('#cmbSearchCity').empty();
	$.ajax({
		type: "GET",
		url: 'Home/GetAllWithParentNames?txtSearch=' + parameter + '',
		dataType: "json",
		contentType: false,
		processData: false,
		success: function (response) {
			$.each(response.districts, function () {
			
				res += "<li  data-value=" + this.id + " onclick='FillCitySearchInput(\"" + this.listName + "\")'><a href='#'  style='margin-right:15px'>" + this.listName + "</a></li>"; 
			});
			finalres = "<ul>" + res + "</ul>"; 
			$('#cmbSearchCity').append(finalres);
			$('#cmbSearchCity').click();

		},
	});


}

function FillCitySearchInput(listname) {
	$("#searchCityInput").val(listname);
	$('#cmbSearchCity').empty();
}

function FillCategorySearchInput(listname) {
	$("#idTxtCategorySearch").val(listname);
	$('#cmbSearchCategoryAlternative').empty();
}


function GetMoreActivity() {
	
	var personActivityType = "";
	var businessPicture = ""; 


	var activityType ="";
	var data = "";

	$.ajax({
		type: "GET",
		url: 'Home/GetMoreAcivites',
		dataType: "json",
		contentType: false,
		processData: false,
		success: function (response) {
			//alert(JSON.stringify(response.items));
			//alert(response.items[0].mainPage_RecentActivityContent.name)

			$.each(response.items, function () {
				if (this.activityType == 1) {
					personActivityType = "یک نظر ثبت کرد";
					if (this.mainPage_RecentActivityContent.image == "") {
						businessPicture = "~/Upload/DefaultPicutres/Bussiness/Business.jpg";
					}
					else {
						businessPicture = this.mainPage_RecentActivityContent.image;
					}
					RecentActivityCreator(this.mainPage_RecentActivityCreator.image, this.mainPage_RecentActivityCreator.name, personActivityType, businessPicture, this.mainPage_RecentActivityContent.name, this.mainPage_RecentActivityContent.rate, this.mainPage_RecentActivityContent.text);
				}
			
			});
	
		},
	});
}
function RecentActivityCreator(personIcon, personName, personActivityType, businessPicture, businessName, rate, description) {
	var $data = $('<div class="recent-activities__col"><div class="recent-activities__item1"><div class="recent-activities__item1__title"><div class="recent-activities__item1__title__image"><a href="#"><img src="' + personIcon + '"></a></div><div class="recent-activities__item1__title__text"><a href="#">"' + personName + '"</a><span>"' + personActivityType+'"</span></div></div><div class="recent-activities__item1__content"><div class="recent-activities__item1__content__image"><a href="#"><img src="' + businessPicture + '"></a></div><div class="recent-activities__item1__content__title1"><a href="#"> <span>"' + businessName + '"</span></a><div class="recent-activities__item1__content__title1__popover"><div class="recent-activities__item2__content__title__popover__image"><a href="#"><img src="' + businessPicture + '"></a></div><div class="recent-activities__item1__content__title1__popover__text"><div class="recent-activities__item1__content__title1__popover__text__title"><a href="#"> <span>"' + businessName + '"</span></a></div><div class="recent-activities__item1__content__title1__popover__text__desc"><span>"' + description + '"</span></div></div></div></div></div><div class="recent-activities__item1__comment"><div class="recent-activities__item1__comment__icon-star"><div class="recent-activities__item1__comment__icon-star__star" data-star="' + rate + '"></div></div><div class="recent-activities__item1__comment__paragraph"><p>"' + description + '"</p><a href="#">ادامه مطلب</a></div></div><div class="recent-activities__item1__icon"><div class="recent-activities__item1__icon__left"><a href="#" data-toggle="tooltip" title="متن"><svg id="24x24_cool_outline" height="24" viewBox="0 0 24 24" width="24"><path d="M12 22C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-19c-4.963 0-9 4.037-9 9s4.037 9 9 9 9-4.037 9-9-4.037-9-9-9zm7.994 6.765C19.647 11.612 17.97 13 15.96 13h-.002c-1.617 0-3.028-.9-3.67-2.224a.32.32 0 0 0-.575 0C11.07 12.1 9.66 13 8.043 13H8.04c-2.01 0-3.74-1.15-4.035-3.018l-.124-.734c-.07-.305.257-.25.634-.248h14.972c.39 0 .673-.04.632.248l-.124.517zM17 15.143a5.405 5.405 0 0 1-5 3.357 5.405 5.405 0 0 1-5-3.357 8.6 8.6 0 0 0 5 1.6 8.6 8.6 0 0 0 5-1.6z"></path></svg></a><a href="#" data-toggle="tooltip" title="متن"><svg id="24x24_funny_outline" height="24" viewBox="0 0 24 24" width="24"><path d="M12 22C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-19c-4.963 0-9 4.037-9 9s4.037 9 9 9 9-4.037 9-9-4.037-9-9-9zm0 15a5.5 5.5 0 0 1-5.288-4h10.576A5.5 5.5 0 0 1 12 18zm3.5-7a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3zm-7 0a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3z"></path></svg></a></div></div></div></div>');
	
	var masonryOptions = {
		itemSelector: '.recent-activities__col',
		originLeft: false
	};
	var $masonry = $('.recent-activities__list').masonry(masonryOptions);
	$masonry.append($data).masonry('appended', $data);
}
