//import { EALREADY } from "constants";


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
			
				res += "<li  data-value=" + this.id + " onclick='FillCitySearchInput(\"" + this.listName + "\" ,\"" + this.id + "\")'><a href='#'  style='margin-right:15px'>" + this.listName + "</a></li>"; 
			});
			finalres = "<ul>" + res + "</ul>"; 
			$('#cmbSearchCity').append(finalres);
			$('#cmbSearchCity').click();

		},
	});


}

function FillCitySearchInput(listname, id) {
	$("#searchCityInput").val(listname);
	$('#cmbSearchCity').empty();
	$.ajax({
		type: "GET",
		url: 'Home/FillCitySession?Id=' + id+ '',
		dataType: "json",
		contentType: false,
		processData: false,
		success: function (response) {

		},
	});
}

function FillCategorySearchInput(listname) {
	$("#idTxtCategorySearch").val(listname);
	$('#cmbSearchCategoryAlternative').empty();
}

function GetMoreActivity() {
	var personActivityType = "";
	var businessPicture = ""; 
	var count = 0; 
	var img = [];
	if ($("#hasnext").val() == 'true') {
	$.ajax({
		type: "GET",
		url: 'Home/GetMoreAcivites?page='+ $("#currentpage").val()+'',
		dataType: "json",
		contentType: false,
		processData: false,
		success: function (response) {
			$("#currentpage").val('');
			$("#currentpage").val(response.currentpage);
			$("#hasnext").val(response.hasnext);
			if (response.items.length > 0) {
			
				$.each(response.items, function () {
					if (this.activityType == 1) {
						personActivityType = "یک نظر ثبت کرد";
						if (this.mainPage_RecentActivityContent.image == "") {
							businessPicture = "~/Upload/DefaultPicutres/Bussiness/Business.jpg";
						}
						else {
							businessPicture = this.mainPage_RecentActivityContent.image;
						}
						RecentActivityCreator(this.mainPage_RecentActivityCreator.image, this.mainPage_RecentActivityCreator.name, personActivityType, businessPicture, this.mainPage_RecentActivityContent.name, this.mainPage_RecentActivityContent.rate, this.mainPage_RecentActivityContent.text, this.mainPage_RecentActivityContent.useFulCount, this.mainPage_RecentActivityContent.funnyCount, this.mainPage_RecentActivityContent.coolCount, this.mainPage_RecentActivityContent.businessId, this.mainPage_RecentActivityContent.id);
					}
					if (this.activityType == 0) {
					
						if (this.mainPage_RecentActivityUserMediaBusinesses.length == 1) {
							RecentActivityBusinessMediaSignlePictureCreator(this.mainPage_RecentActivityCreator.name, this.mainPage_RecentActivityCreator.image, this.mainPage_RecentActivityContent.name, this.mainPage_RecentActivityUserMediaBusinesses[0].image, this.mainPage_RecentActivityContent.text, this.mainPage_RecentActivityContent.rate, "", "", this.mainPage_RecentActivityUserMediaBusinesses[0].likeCount, this.mainPage_RecentActivityUserMediaBusinesses[0].usersName, this.mainPage_RecentActivityContent.id, this.mainPage_RecentActivityUserMediaBusinesses[0].id);
						
						}
						else {
							
							for (var i = 1; i < this.mainPage_RecentActivityUserMediaBusinesses.length; i++) {
								count++;
								img.push({ url: this.mainPage_RecentActivityUserMediaBusinesses[i].image, id: this.mainPage_RecentActivityUserMediaBusinesses[i].id, like: this.mainPage_RecentActivityUserMediaBusinesses[i].likeCount });
							}
							RecentActivityBusinessMediaMultiPictureCreator(this.mainPage_RecentActivityCreator.image, this.mainPage_RecentActivityCreator.name, this.mainPage_RecentActivityContent.name, this.mainPage_RecentActivityUserMediaBusinesses[0].image, this.mainPage_RecentActivityContent.rate, this.mainPage_RecentActivityContent.text, count, img, this.mainPage_RecentActivityContent.text, this.mainPage_RecentActivityContent.id, this.mainPage_RecentActivityContent.businessId);
						}
					}
				});
				$(".recent-activities__list").css("height", $(".recent-activities__list").height() + 190);

			}
			else {
				$(".recent-activities__list").css("height", $(".recent-activities__list").height() - 190);
			}
	
		},
	});
	}
}
function RecentActivityCreator(personIcon, personName, personActivityType, businessPicture, businessName, rate, description, usefull, happy, cool, businessId, reviewId) {
	var $data = $('<div class="recent-activities__col"><div class="recent-activities__item1"><div class="recent-activities__item1__title"><div class="recent-activities__item1__title__image"><a href="#"><img src="' + personIcon + '"></a></div><div class="recent-activities__item1__title__text"><a href="#">' + personName + '</a><span>"' + personActivityType + '"</span></div></div><div class="recent-activities__item1__content"><div class="recent-activities__item1__content__image"><a style="cursor:pointer"><img src="' + businessPicture + '"></a></div><div class="recent-activities__item1__content__title1"><a href="/BusinessHome/Index?Id=' + businessId + '"> <span>' + businessName + '</span></a><div class="recent-activities__item1__content__title1__popover"><div class="recent-activities__item2__content__title__popover__image"><a href="#"><img src="' + businessPicture + '"></a></div><div class="recent-activities__item1__content__title1__popover__text"><div class="recent-activities__item1__content__title1__popover__text__title"><a href="/BusinessHome/Index?Id=' + businessId + '"> <span>' + businessName + '</span></a></div><div class="recent-activities__item1__content__title1__popover__text__desc"><span>"' + description + '"</span></div></div></div></div></div><div class="recent-activities__item1__comment"><div class="recent-activities__item1__comment__icon-star"><div class="recent-activities__item1__comment__icon-star__star" data-star="' + rate + '"></div></div><div class="recent-activities__item1__comment__paragraph"><p>"' + description + '"</p><a href="#">ادامه مطلب</a></div></div><div class="recent-activities__item1__icon"><div class="recent-activities__item1__icon__left"><span>' + usefull + '</span><a  data-toggle="tooltip" onclick="changeUsefull(this);" data-assigned-id=' + reviewId + ' title="!مفیدبود"><svg id="24x24_cool_outline" height="24" viewBox="0 0 24 24" width="24"><path d="M12 22C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-19c-4.963 0-9 4.037-9 9s4.037 9 9 9 9-4.037 9-9-4.037-9-9-9zm7.994 6.765C19.647 11.612 17.97 13 15.96 13h-.002c-1.617 0-3.028-.9-3.67-2.224a.32.32 0 0 0-.575 0C11.07 12.1 9.66 13 8.043 13H8.04c-2.01 0-3.74-1.15-4.035-3.018l-.124-.734c-.07-.305.257-.25.634-.248h14.972c.39 0 .673-.04.632.248l-.124.517zM17 15.143a5.405 5.405 0 0 1-5 3.357 5.405 5.405 0 0 1-5-3.357 8.6 8.6 0 0 0 5 1.6 8.6 8.6 0 0 0 5-1.6z"></path></svg></a><a  data-toggle="tooltip" title="جالب بود!"><span>' + happy + '</span><svg onclick="changeFunny(this);" data-assigned-id=' + reviewId + ' id="24x24_funny_outline" height="24" viewBox="0 0 24 24" width="24"><path d="M12 22C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-19c-4.963 0-9 4.037-9 9s4.037 9 9 9 9-4.037 9-9-4.037-9-9-9zm0 15a5.5 5.5 0 0 1-5.288-4h10.576A5.5 5.5 0 0 1 12 18zm3.5-7a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3zm-7 0a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3z"></path></svg></a><a  data-toggle="tooltip" title="جذاب بود!"><a  data-toggle="tooltip" title="جذاب بود!"><span>' + cool + '</span><svg id="24x24_useful_outline" height="24" viewBox="0 0 24 24" width="24" data-assigned-id=' + reviewId + ' onclick="changeCool(this)"><path d="M12 22C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-19c-4.963 0-9 4.037-9 9s4.037 9 9 9 9-4.037 9-9-4.037-9-9-9zm2 10.704V15h-4v-1.296c-1.35-.72-2.278-2.124-2.278-3.76a4.278 4.278 0 0 1 8.556 0c0 1.636-.93 3.04-2.278 3.76zM11 10a1 1 0 1 0 0-2 1 1 0 0 0 0 2zm3 6.5a2 2 0 0 1-4 0V16h4v.5z"></path></svg></div></div></div></div>');
	
	var masonryOptions = {
		itemSelector: '.recent-activities__col',
		originLeft: false
	};
	var $masonry = $('.recent-activities__list').masonry(masonryOptions);
	$masonry.append($data).masonry('appended', $data);
	$masonry.masonry('layout');

}


function RecentActivityBusinessMediaSignlePictureCreator(personName, personIcon, businessName, media, businessDesc, rate, count, description, likes, userNames, contentId , imageId) {

	var $data = $('<div class="recent-activities__col"><div class="recent-activities__item2"><div class="recent-activities__item2__title"><div class="recent-activities__item2__title__image"><a href="#"><img src="' + personIcon + '"></a></div><div class="recent-activities__item2__title__text"><a href="#" onclick="ShowModal(this)" data-assigned-id="' + contentId + '">' + personName + '</a><span> عکس اضافه کرد</span></div></div><div class="recent-activities__item2__content"><div class="recent-activities__item2__content__title"><a href="#"><span>' + businessName + ' </span></a><div class="recent-activities__item2__content__title__popover"><div class="recent-activities__item2__content__title__popover__image"><a href="#"><img src="' + media + '"></a></div><div class="recent-activities__item2__content__title__popover__text"><div class="recent-activities__item2__content__title__popover__text__title"><a href="#">' + businessName + '</a></div><div class="recent-activities__item2__content__title__popover__text__star"><div class="recent-activities__item2__content__title__popover__text__star__icon" data-star="' + rate + '"></div><div class="recent-activities__item2__content__title__popover__text__star__text"><p>امتیازات</p></div></div><div class="recent-activities__item2__content__title__popover__text__desc"><span style="max-width:200px;text-overflow:ellipsis;overflow:hidden;white-space:nowrap; ">' + businessDesc + '</span></div></div></div></div><div class="recent-activities__item2__content__image"><a href="#"><img style="width: 98%;margin-right: 2px" src="' + media + '"></a></div><div class="recent-activities__item2__content__like"><a data-toggle="tooltip-html" title="علی رحیمیان رضا حسن زاده حسن رفیعی" data-assigned-id=' + imageId + ' onclick="changeLike(this);" style="cursor:pointer"><svg id="24x24_like_outline" height="24" viewBox="0 0 24 24" width="24"><path d="M21.164 12.236c.05.164.086.334.086.514 0 .66-.37 1.23-.91 1.527.1.22.16.464.16.723 0 .66-.37 1.23-.91 1.527.1.22.16.464.16.723A1.75 1.75 0 0 1 18 19H7v-9h1c.37 0 1.257-2.37 2.104-3.345.89-1.017 1.234-1.782 1.457-2.513C11.785 3.412 12 2 12 2s2.388.11 2.388 2.9c0 1.39-.758 3.1-.388 4.1h6.25c.966 0 1.75.784 1.75 1.75 0 .63-.336 1.178-.836 1.486zM20.25 10h-6.946l-.242-.653c-.316-.855-.11-1.862.09-2.835.117-.56.236-1.14.236-1.61 0-.844-.283-1.314-.608-1.577-.076.387-.168.797-.262 1.107-.228.748-.604 1.673-1.66 2.88-.336.386-.744 1.166-1.072 1.794C9.146 10.326 8.796 11 8 11v7h10a.75.75 0 0 0 .75-.75.75.75 0 0 0-.07-.308l-.385-.843.812-.45A.74.74 0 0 0 19.5 15a.75.75 0 0 0-.07-.308l-.385-.843.812-.45a.74.74 0 0 0 .393-.65.793.793 0 0 0-.04-.22l-.23-.74.66-.406A.746.746 0 0 0 20.25 10zM2 10h4v10H2V10z"></path></svg><div>'+ likes +'</div></a></div></div></div></div>');
	var masonryOptions = {
		itemSelector: '.recent-activities__col',
		originLeft: false
	};
	var $masonry = $('.recent-activities__list').masonry(masonryOptions);
	$masonry.append($data).masonry('appended', $data);
	$masonry.masonry('layout');
}
function RecentActivityBusinessMediaMultiPictureCreator(personIcon, personName, businessName, businessFirstPicture, rate, description, count, arrImg, businessimg, contentId) {

	var otherimages = "";
	for (var i = 0; i < arrImg.length; i++) {
		otherimages += '<div class="recent-activities__item__image__item"><div class="recent-activities__item__image__item__img"><a href="#"><img src=' + arrImg[i].url + '></a></div><div class="recent-activities__item__image__item__like"><a data-toggle="tooltip"  onclick="changeLike(this);" data-assigned-id ='+arrImg[i].id+' title="متن"><svg id="24x24_like_outline" height="24" viewBox="0 0 24 24" width="24"><path d="M21.164 12.236c.05.164.086.334.086.514 0 .66-.37 1.23-.91 1.527.1.22.16.464.16.723 0 .66-.37 1.23-.91 1.527.1.22.16.464.16.723A1.75 1.75 0 0 1 18 19H7v-9h1c.37 0 1.257-2.37 2.104-3.345.89-1.017 1.234-1.782 1.457-2.513C11.785 3.412 12 2 12 2s2.388.11 2.388 2.9c0 1.39-.758 3.1-.388 4.1h6.25c.966 0 1.75.784 1.75 1.75 0 .63-.336 1.178-.836 1.486zM20.25 10h-6.946l-.242-.653c-.316-.855-.11-1.862.09-2.835.117-.56.236-1.14.236-1.61 0-.844-.283-1.314-.608-1.577-.076.387-.168.797-.262 1.107-.228.748-.604 1.673-1.66 2.88-.336.386-.744 1.166-1.072 1.794C9.146 10.326 8.796 11 8 11v7h10a.75.75 0 0 0 .75-.75.75.75 0 0 0-.07-.308l-.385-.843.812-.45A.74.74 0 0 0 19.5 15a.75.75 0 0 0-.07-.308l-.385-.843.812-.45a.74.74 0 0 0 .393-.65.793.793 0 0 0-.04-.22l-.23-.74.66-.406A.746.746 0 0 0 20.25 10zM2 10h4v10H2V10z"></path><div>' + arrImg[i].like +'</div></div></div>';
	}

	var $data = $('<div class="recent-activities__col" onclick="ShowModal(this)" data-assigned-id="'+contentId+'"><div class="recent-activities__item"><div class="recent-activities__item__title"><div class="recent-activities__item__title__image"><a href="#"> <img src="' + personIcon + '"></a></div><div class="recent-activities__item__title__text"><a href="#">"' + personName + '"</a><span>' + count + 'عکس اضافه کرد</span></div></div><div class="recent-activities__item1__content"><div class="recent-activities__item1__content__title1"><a href="#"><span>' + businessName + '</span></a><div class="recent-activities__item1__content__title1__popover"><div class="recent-activities__item2__content__title__popover__image"><a href="#"><img src="' + businessFirstPicture + '"></a></div><div class="recent-activities__item1__content__title1__popover__text"><div class="recent-activities__item1__content__title1__popover__text__title"><a href="#">' + businessName + '</a></div><div class="recent-activities__item1__content__title1__popover__text__star"><div class="recent-activities__item1__content__title1__popover__text__star__icon" data-star=' + rate + '></div><div class="recent-activities__item1__content__title1__popover__text__star__text"><p>امتیازات</p></div></div><div class="recent-activities__item1__content__title1__popover__text__desc"><span style="max-width: 200px;text-overflow: ellipsis;overflow: hidden;white-space: nowrap;">' + description + '</span></div></div></div></div></div><div class="recent-activities__item__image"><div class="recent-activities__item__image__item recent-activities__item__image__item--big"><div class="recent-activities__item__image__item__img"><a href="#"><img src="' + businessFirstPicture + '"></a></div><div class="recent-activities__item__image__item__like"><a href="#" data-toggle="tooltip" title="متن"><svg id="24x24_like_outline" height="24" viewBox="0 0 24 24" width="24"><path d="M21.164 12.236c.05.164.086.334.086.514 0 .66-.37 1.23-.91 1.527.1.22.16.464.16.723 0 .66-.37 1.23-.91 1.527.1.22.16.464.16.723A1.75 1.75 0 0 1 18 19H7v-9h1c.37 0 1.257-2.37 2.104-3.345.89-1.017 1.234-1.782 1.457-2.513C11.785 3.412 12 2 12 2s2.388.11 2.388 2.9c0 1.39-.758 3.1-.388 4.1h6.25c.966 0 1.75.784 1.75 1.75 0 .63-.336 1.178-.836 1.486zM20.25 10h-6.946l-.242-.653c-.316-.855-.11-1.862.09-2.835.117-.56.236-1.14.236-1.61 0-.844-.283-1.314-.608-1.577-.076.387-.168.797-.262 1.107-.228.748-.604 1.673-1.66 2.88-.336.386-.744 1.166-1.072 1.794C9.146 10.326 8.796 11 8 11v7h10a.75.75 0 0 0 .75-.75.75.75 0 0 0-.07-.308l-.385-.843.812-.45A.74.74 0 0 0 19.5 15a.75.75 0 0 0-.07-.308l-.385-.843.812-.45a.74.74 0 0 0 .393-.65.793.793 0 0 0-.04-.22l-.23-.74.66-.406A.746.746 0 0 0 20.25 10zM2 10h4v10H2V10z"></path></svg>لایک</a></div></div>' + otherimages + '</div><div class="recent-activities__item__show-all"><a style="cursor:pointer" hreaf="#">نمایش دیگر عکس ها</a></div></div></div>');
	var masonryOptions = {
		itemSelector: '.recent-activities__col',
		originLeft: false
	};
	var $masonry = $('.recent-activities__list').masonry(masonryOptions);
	$masonry.append($data).masonry('appended', $data);
	$masonry.masonry('layout');
	$('.recent-activities__item__show-all').on('click', function () {
		$(this).siblings('.recent-activities__item__image')
			.addClass('recent-activities__item__image--active')
		$(this).remove();

		$masonry.masonry(masonryOptions)
	});
}
window.onload = Load;
function Load() {
	$('a.someclass').click(function (e) {
		// Special stuff to do when this link is clicked...

		// Cancel the default action
		e.preventDefault();
	});
	

	$("#currentpage").val('1');
	$("#hasnext").val('true');
}


//modals
function ShowModal(elem) {
	$.ajax({
		type: "GET",
		url: 'Home/GetActivityById?id=' + $(elem).data('assigned-id') + '',
		dataType: "json",
		contentType: false,
		processData: false,
		success: function (response) {
			$("#modalbusinessmediapictures").html('');
			$("#modalUserProfilePicture").attr("src", response.item.userProfilePicture);
			$("#modalcreatorname").text(response.item.name);
			$("#modalbusinssdescription").text(response.item.description);
			$("#modalbusinessdate").text(response.item.persianDate);
			$("#modalbusinessreviews").text(response.item.totalReview);
			$.each(response.item.images, function () {		
				var otherimages = '<div class="swiper-slide"><div class="recent-activities__modal__image"><a href="#"><img id="" src="'+this+'" alt=""></a></div><div class="recent-activities__modal__footer-slide"><div class="recent-activities__modal__footer-slide__right"><a href="#"><svg id="18x18_grid" height="18" viewBox="0 0 18 18" width="18"><path d="M10 15v-5h5v5h-5zm0-12h5v5h-5V3zm-7 7h5v5H3v-5zm0-7h5v5H3V3z"></path></svg>نمایش همه</a></div><div class="recent-activities__modal__footer-slide__left"><a href="#"><svg id="18x18_share" height="18" viewBox="0 0 18 18" width="18"><path d="M17.714 6.43L13 10.356v-3.03c-1 0-5.097 1.47-6.286 3.62.274-3.08 4.286-5.5 6.286-5.5V2.5l4.714 3.93zM3 4v10h11v-2.5l1-1V15H2V3h8.5l-1 1H3z"></path></svg>اشتراک گذاری</a></div></div></div>';
				$("#modalbusinessmediapictures").append(otherimages);
			})
	         $("#click-image").modal('show');
		},
	});
}

//addUsefull
function changeUsefull(element) {

$.ajax({
	type: "Post",
	url: '/Review/ChangeUseFullCount?Id=' + $(element).data('assigned-id') + '',
	dataType: "json",
	contentType: false,
	processData: false,
	success: function (response) {

		if (response.type == "add") {
			var number = parseInt($(element).prev().text()) + 1;
			$(element).prev().text(number);
		}
		else if (response.type == "authorize") {
			window.location = "/Identity/Account/Login";
		}
		else {
			var number = parseInt($(element).prev().text()) - 1;
			$(element).prev().text(number);
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
				var number = parseInt($(element).prev().text()) + 1;
				$(element).prev().text(number);
			}
			else if (response.type == "authorize") {
				window.location = "/Identity/Account/Login";
			}
			else {
				var number = parseInt($(element).prev().text()) - 1;
				$(element).prev().text(number);
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
				var number = parseInt($(element).prev().text()) + 1;
				$(element).prev().text(number);
			}
			else if (response.type == "authorize") {
				window.location = "/Identity/Account/Login";
			}
			else {
				var number = parseInt($(element).prev().text()) - 1;
				$(element).prev().text(number);
			}
		}
	});
}

function changeLike(element) {
	//var t = $(element).attr('data-original-title').split("مالک سالن بیلیارد اصفهان").join("");

$.ajax({
	type: "Post",
	url: '/Review/ChangeLike?Id=' + $(element).data('assigned-id') + '',
	dataType: "json",
	contentType: false,
	processData: false,
	success: function (response) {
		if (response.type == "add") {
			var number = parseInt($(element).text()) + 1;
			$(element).children('div').text(number);
			var previousNames = $(element).attr('data-original-title');
			var currentName = '<br>' + response.username + '';
			var resault = previousNames + currentName; 
			$(element).attr('data-original-title', resault);
		}
		else if (response.type == "authorize") {
			window.location = "/Identity/Account/Login";
		}
		else {
			var number = parseInt($(element).text()) - 1;
			$(element).children('div').text(number);
			var previousNames = $(element).attr('data-original-title');
			var newNames = previousNames.split(response.username).join("");
			$(element).attr('data-original-title', newNames);
		}
	}
});
}

