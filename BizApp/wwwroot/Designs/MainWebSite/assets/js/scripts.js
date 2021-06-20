$(window).on('load', () => {
    Dropzone.autoDiscover = false;

    $('a[href="#"]').on('click', function (e) {
        e.preventDefault();
    })

    var services_slider = new Swiper('.services .swiper-container', {
        direction: 'horizontal',
        slidesPerView: 2.2,
        spaceBetween: 15,
        autoplay: {
            delay: 3000
        },
        loop: 1,
        navigation: {
            // nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },

    });


    var user_profile_slider = new Swiper('.profile-header .swiper-container', {
        slidesPerView: 1,
        autoplay: {
            delay: 3000
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },

    });
    var photos_image_slider = new Swiper('.all-photos-content__modal .swiper-container', {
        slidesPerView: 1,
        autoplay: {
            delay: 3000
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        pagination: {
            el: '.swiper-pagination',
            type: 'fraction',
        },

    });
    var recent_image_slider = new Swiper('.recent-activities__modal .swiper-container', {
        slidesPerView: 1,
        autoplay: {
            delay: 3000
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        pagination: {
            el: '.swiper-pagination',
            type: 'fraction',
        },

    });
    var profile_photos_slider = new Swiper('.profile-content-photos .swiper-container', {
        slidesPerView: 1,
        autoplay: {
            delay: 3000
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        pagination: {
            el: '.swiper-pagination',
            type: 'fraction',
        },

    });
    $('#click-image').on('shown.bs.modal', function (e) {
        recent_image_slider.update();
    })

    var masonryOptions = {
        itemSelector: '.recent-activities__col',
        originLeft: false
    };

    var profile_all_photos_slider = new Swiper('.profile-content-all-photos__modal .swiper-container', {
        slidesPerView: 1,
        autoplay: {
            delay: 3000
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        pagination: {
            el: '.swiper-pagination',
            type: 'fraction',
        },

    });
    var services_slider = new Swiper('.stay-gold-content .swiper-container', {
        slidesPerView: 1.6,
        spaceBetween: 15,
        // autoplay: {
        //     delay: 3000
        // },
        breakpoints: {
            768: {

                slidesPerView: 3.5,
            },

            480: {
                slidesPerView: 2.5,
            },
        },
        loop: 1,
        navigation: {
            nextEl: '.swiper-btn-next',
        },

    });
    var collections_slider = new Swiper('.collections-including .swiper-container', {
        slidesPerView: 1.5,
        spaceBetween: 15,
        // autoplay: {
        //     delay: 3000
        // },
        loop: 1,
        breakpoints: {
            992: {

                slidesPerView: 5,
            },
            768: {

                slidesPerView: 4,
            },

            480: {
                slidesPerView: 2,
            },
        },
        navigation: {
            nextEl: '.swiper-btn-next',
        },

    });
    var people_slider = new Swiper('.stay-gold-people .swiper-container', {
        slidesPerView: 1.5,
        spaceBetween: 15,
        // autoplay: {
        //     delay: 3000
        // },
        loop: 1,
        breakpoints: {
            992: {

                slidesPerView: 4.5,
            },
            768: {

                slidesPerView: 3.5,
            },

            480: {
                slidesPerView: 2.5,
            },
        },
        navigation: {
            nextEl: '.swiper-btn-next',
        },

    });


    var collection_content_slider = new Swiper('.collection-content .swiper-container', {
        slidesPerView: 3.5,
        spaceBetween: 7,


    });


    $('.profile-content-all-photos__modal').on('shown.bs.modal', function (e) {

        if (profile_all_photos_slider.length > 1) {
            $.each(profile_all_photos_slider, function (i, e) {
                profile_all_photos_slider[i].update()
            })
        }
        else {
            profile_all_photos_slider.update()
        }
    })



    var $masonry = $('.recent-activities__list').masonry(masonryOptions);

    $('.recent-activities__item__show-all').on('click', function () {
        $(this).siblings('.recent-activities__item__image')
            .addClass('recent-activities__item__image--active')
        $(this).remove();

        $masonry.masonry(masonryOptions)
    });


    //$('.recent-activities__text-link').on('click', function () {

    //    var $data = $('<div class="recent-activities__col"> <div class="recent-activities__item2"> <div class="recent-activities__item2__title"> <div class="recent-activities__item2__title__image"> <a href="#"> <img src="./assets/img/person.jpg"></a> </div> <div class="recent-activities__item2__title__text"> <a href="#">پارکر</a> <span>3 عکس اضافه کرد</span> </div> </div> <div class="recent-activities__item2__content"> <div class="recent-activities__item2__content__title"> <a href="#"> <span>شیر روی سنگ ها</span></a> <div class="recent-activities__item2__content__title__popover"> <div class="recent-activities__item2__content__title__popover__image"> <a href="#"><img src="./assets/img/food.jpg"></a> </div> <div class="recent-activities__item2__content__title__popover__text"> <div class="recent-activities__item2__content__title__popover__text__title"> <a href="#">شیر روی سنگ ها</a> </div> <div class="recent-activities__item2__content__title__popover__text__star"> <div class="recent-activities__item2__content__title__popover__text__star__icon" data-star="5"></div> <div class="recent-activities__item2__content__title__popover__text__star__text"> <p> بررسی ها</p> </div> </div> <div class="recent-activities__item2__content__title__popover__text__desc"> <span>آرایش ناخن ، ماساژ ، کشیدن مژه و رنگ آمیزی مژه</span> </div> </div> </div> </div> <div class="recent-activities__item2__content__image"> <a href="#"><img src="./assets/img/food.jpg"></a> </div> <div class="recent-activities__item2__content__like"> <a href="#"> <svg id="24x24_like_outline" height="24" viewBox="0 0 24 24" width="24"> <path d="M21.164 12.236c.05.164.086.334.086.514 0 .66-.37 1.23-.91 1.527.1.22.16.464.16.723 0 .66-.37 1.23-.91 1.527.1.22.16.464.16.723A1.75 1.75 0 0 1 18 19H7v-9h1c.37 0 1.257-2.37 2.104-3.345.89-1.017 1.234-1.782 1.457-2.513C11.785 3.412 12 2 12 2s2.388.11 2.388 2.9c0 1.39-.758 3.1-.388 4.1h6.25c.966 0 1.75.784 1.75 1.75 0 .63-.336 1.178-.836 1.486zM20.25 10h-6.946l-.242-.653c-.316-.855-.11-1.862.09-2.835.117-.56.236-1.14.236-1.61 0-.844-.283-1.314-.608-1.577-.076.387-.168.797-.262 1.107-.228.748-.604 1.673-1.66 2.88-.336.386-.744 1.166-1.072 1.794C9.146 10.326 8.796 11 8 11v7h10a.75.75 0 0 0 .75-.75.75.75 0 0 0-.07-.308l-.385-.843.812-.45A.74.74 0 0 0 19.5 15a.75.75 0 0 0-.07-.308l-.385-.843.812-.45a.74.74 0 0 0 .393-.65.793.793 0 0 0-.04-.22l-.23-.74.66-.406A.746.746 0 0 0 20.25 10zM2 10h4v10H2V10z"></path> </svg> لایک </a> </div> </div> </div> </div>');

    //    $masonry.append($data).masonry('appended', $data);

    //    $masonry.masonry('layout');


    //})



    $('.main-header__search__right input').on('focus', function () {
        $(this).parent().addClass('main-header__search__right--open')
    })

    $('.main-header__search__right__menu-bottom li').on('click', function () {
        var val = $(this).data('value');
        $('.main-header__search__right input').val(val);
        $('.main-header__search__right').removeClass('main-header__search__right--open')
    })

    $(document).on('click', function (event) {
        if (!$(event.target).closest('.main-header__search').length) {
            $('.main-header__search__right').removeClass('main-header__search__right--open')
        }
    });


    $('.main-header__search__left input').on('focus', function () {
        $(this).parent().addClass('main-header__search__left--open')
    })

    $('.main-header__search__left__menu-bottom li').on('click', function () {
        var val = $(this).data('value');
        $('.main-header__search__left input').val(val);
        $('.main-header__search__left').removeClass('main-header__search__left--open')
    })

    $(document).on('click', function (event) {
        if (!$(event.target).closest('.main-header__search').length) {
            $('.main-header__search__left').removeClass('main-header__search__left--open')
        }
    });





    $('.first-review__item__search__near input').keydown('focus', function () {
        $(this).parent().addClass('first-review__item__search__near--open')
    })
    $('.first-review__item__search__near__menu-bottom li').on('click', function () {
        var val = $(this).data('value');
        $('.first-review__item__search__near input').val(val);
        $('.first-review__item__search__near').removeClass('first-review__item__search__near--open')
    })
    $(document).on('click', function (event) {
        if (!$(event.target).closest('.first-review__item__search').length) {
            $('.first-review__item__search__near').removeClass('first-review__item__search__near--open')
        }
    });







    $('.header-mobile3__menu-mobail__top__icon svg').on('click', function () {
        $(this).parent().toggleClass('header-mobile3__menu-mobail__top__icon--open');
        $('.header-mobile3__menu').toggle()
    })




    $('.main-header__nav .user a').on('click', function () {
        if ($(this).parent().hasClass('child-open')) {
            $(this).parent().removeClass('child-open')
            $('.main-header__nav__left__tolbar').removeClass('main-header__nav__left__tolbar--open');
        }
        else {
            $(this).parent().addClass('child-open')
            $('.main-header__nav__left__tolbar').addClass('main-header__nav__left__tolbar--open');
        }
    });

    $(document).on('click', function (event) {
        if (!$(event.target).closest('.main-header__nav__left--loggined > ul > li.user').length) {
            $('.main-header__nav__left__tolbar').removeClass('main-header__nav__left__tolbar--open');
            $('.main-header__nav .user').removeClass('child-open')
        }
    });




    $('.write-areviews__item__text__close').on('click', function () {
        $(this).closest('[class^="col"]').remove()
    })

    $('.start-business__form__input__input-form-phone__lable a').on('click', function () {
        $(this).closest('span').remove();
        $('.start-business__form__input__country').show()
    })

    if ($('body').hasClass('header-fix')) {
        if ($(window).scrollTop() >= 200) {
            $('body').addClass('header-menu-hide')
            $('.header-type1__menu-bottom').slideUp(300)
        }
        else {
            $('body').removeClass('header-menu-hide')
            $('.header-type1__menu-bottom').slideDown(300)
        }
        $(window).scroll(function () {
            if ($(window).scrollTop() >= 200) {
                $('body').addClass('header-menu-hide')
                $('.header-type1__menu-bottom').slideUp(300)
            }
            else {
                $('body').removeClass('header-menu-hide')
                $('.header-type1__menu-bottom').slideDown(300)
            }
        })
    }

    $('.yelp-paris__content__item--more').on('click', function () {
        $(".yelp-paris__more").slideToggle()
    })



    if ($('#mapid').length > 0) {
        var mymap = L.map('mapid').setView([32.6538472, 51.6724925], 18);
        L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
            attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
            maxZoom: 18,
            id: 'mapbox/streets-v11',
            tileSize: 512,
            zoomOffset: -1,
            accessToken: 'your.mapbox.access.token'
        }).addTo(mymap);

        var map_icon = L.icon({
            iconUrl: './assets/img/location.png',
            iconSize: [30, 42],
            iconAnchor: [30, 42],
            popupAnchor: [0, -40]
        });
        var marker = L.marker([32.6538472, 51.6724925], { icon: map_icon, }).addTo(mymap);
    }

    if ($('#map-collection').length > 0) {
        var mymap = L.map('map-collection').setView([32.6538472, 51.6724925], 18);
        L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
            attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
            maxZoom: 18,
            id: 'mapbox/streets-v11',
            tileSize: 512,
            zoomOffset: -1,
            accessToken: 'your.mapbox.access.token'
        }).addTo(mymap);

        var map_icon = L.icon({
            iconUrl: './assets/img/location.png',
            iconSize: [30, 42],
            iconAnchor: [30, 42],
            popupAnchor: [0, -40]
        });
        var marker = L.marker([32.6538472, 51.6724925], { icon: map_icon, }).addTo(mymap);
    }

    $('.category-content__filter-mobail__button-list__list a').on('click', function () {
        $(this).addClass('active')
        $('.category-content__filter-mobail__button-list__map a').removeClass('active');
        $('.category-content__map').removeClass('active');
    })

    $('.category-content__filter-mobail__button-list__map a').on('click', function () {
        $(this).addClass('active')
        $('.category-content__filter-mobail__button-list__list a').removeClass('active');
        $('.category-content__map').addClass('active')
    })

    $('.category-content__filter-mobail__button-filter a').on('click', function () {
        $(this).toggleClass('active')
        $('.category-content__item-filter').toggleClass('category-content__item-filter--active')
    })


    $('.profile-content__list-profile__title').on('click', function () {
        $('.profile-content__list-profile ul').slideToggle(300)
    })

    $('.talk__content__title').on('click', function () {
        $('.talk__content__list-link').slideToggle(300)
    })





    $('.header-type1__menu__search__right input').on('focus', function () {
        $(this).parent().addClass('header-type1__menu__search__right--open');
    })

    $('.header-type1__menu__search__right__menu-bottom li').on('click', function () {
        var val = $(this).data('value');
        $('.header-type1__menu__search__right input').val(val);
        $('.header-type1__menu__search__right').removeClass('header-type1__menu__search__right--open')
    })

    $(document).on('click', function (event) {
        if (!$(event.target).closest('.header-type1__menu__search').length) {
            $('.header-type1__menu__search__right').removeClass('header-type1__menu__search__right--open')
        }
    });





    $('.header-type1__menu__search__left input').on('focus', function () {
        $(this).parent().addClass('header-type1__menu__search__left--open');
    })

    $('.header-type1__menu__search__left__menu-bottom li').on('click', function () {
        var val = $(this).data('value');
        $('.header-type1__menu__search__left input').val(val);
        $('.header-type1__menu__search__left').removeClass('header-type1__menu__search__left--open')
    })

    $(document).on('click', function (event) {
        if (!$(event.target).closest('.header-type1__menu__search').length) {
            $('.header-type1__menu__search__left').removeClass('header-type1__menu__search__left--open')
        }
    });





    $('.header-yelp__menu__search__right').on('click', function () {
        $(this).toggleClass('header-yelp__menu__search__right--open')

    })
    $('.header-yelp__menu__search__left').on('click', function () {
        $(this).toggleClass('header-yelp__menu__search__left--open')
    })







    $('.stay-gold-content__wrapper__recommended__alert__close > svg').on('click', function () {
        $(this).closest('.stay-gold-content__wrapper__recommended__alert').remove()
    })
    $('.item-location__item__alert__icon > i').on('click', function () {
        $(this).closest('.item-location__item__alert').remove()
    })
    $('.item-location__content__desc__alert__close  > svg').on('click', function () {
        $(this).closest('.item-location__content__desc__alert').remove()
    })

    $('.header-type1__menu__user-nav > ul > li.has-sub').on('click', function () {
        $(this).find('>ul').toggle()
    })

    $('.header-mobile1__menu-mobail__top__user-nav > ul > li.has-sub').on('click', function () {
        $(this).find('>ul').toggle()
    })

    $("#dropzone1").dropzone({
        url: "/BusinessGallery/AddPhotoForBusinessByCustomer",
        autoProcessQueue: true,
        uploadMultiple: true, // uplaod files in a single request
        parallelUploads: 100,
        init: function () {
    
            this.on("addedfile", function (file) {
                $('.upload-pic__header h3').show();
                $('.upload-pic__footer').show();
                $('.upload-pic .dropzone .dz-message').remove();
                $('.upload-pic .dropzone').addClass('loaded');
                caption = file.caption == undefined ? "" : file.caption;
                file._captionLabel = Dropzone.createElement("<label>توضیحات: <small>دلخواه</small></label>")
                file._captionBox = Dropzone.createElement("<textarea id='image_" + file.lastModified + "' name='caption[]' placeholder='توضیحات را در این قسمت بنویسید'>" + caption + "</textarea>");
                file.previewElement.appendChild(file._captionLabel);
                file.previewElement.appendChild(file._captionBox);
            }),
                this.on("sending", function (file, xhr, formData) {                   
                    formData.append('file',file);
                }),
                this.on('success', function (file, message) {
                    file.imageId = Dropzone.createElement("<input value=" + message.id + "></input>");
                    file.previewElement.appendChild(file.imageId);
                })


       
        }
    });

    $("#dropzone2").dropzone({
        url: "/file/post",
        autoProcessQueue: true,
        init: function () {
            this.on("addedfile", function (file) {
                $('.review-content__modal__content__close h3').show();
                $('.review-content__modal__content__footer').show();
                $('.review-content__modal__content__image').remove();
                $('.review-content__modal .dz-message').remove();
                $('.review-content__modal .dropzone').addClass('loaded');

                caption = file.caption == undefined ? "" : file.caption;
                file._captionLabel = Dropzone.createElement("<label>توضیحات: <small>دلخواه</small></label>")
                file._captionBox = Dropzone.createElement("<textarea id='image_" + file.lastModified + "' name='caption[]' placeholder='توضیحات را در این قسمت بنویسید'>" + caption + "</textarea>");
                file.previewElement.appendChild(file._captionLabel);
                file.previewElement.appendChild(file._captionBox);
            }),
                this.on("sending", function (file, xhr, formData) {
                    formData.append('yourPostName', file._captionBox.value);
                })
        }
    });

    var page_slider = new Swiper('.page-slider .swiper-container', {
        slidesPerView: 1,
        spaceBetween: 15,
        breakpoints:
        {
            576:
            {
                slidesPerView: 3,
            }
        }
    });

    $('[data-toggle="tooltip"]').tooltip()



    $(document).on('click', '[data-tab]', function () {
        let $this = $(this),
            tab = $this.data('tab'),
            target = $this.data('tab-target');

        $('[data-tab][data-tab-target="' + target + '"]').removeClass('active');
        $this.addClass('active');

        $('[data-tab-content][data-tab-target="' + target + '"]').removeClass(
            'active'
        );
        $(
            '[data-tab-content="' + tab + '"][data-tab-target="' + target + '"]'
        ).addClass('active');

    });


    $(".review-content__arrow").on("click", function () {
        $(".review-content").toggleClass("review-content--close-sidebar")
    })


    $(".review-content__wrapper__box-comment__score__star > label").on("mouseover", function () {
        var data_title = $(this).data("title")
        $(".review-content__wrapper__box-comment__score__text > span").html(data_title)
    })

    $(".review-content__wrapper__box-comment__score__star > label").on("mouseleave", function () {
        var text = $(".review-content__wrapper__box-comment__score__text").attr('data-default')
        $(".review-content__wrapper__box-comment__score__text > span").html(text)
    })

    $(".review-content__wrapper__box-comment__score__star > label").on("click", function () {
        var data_title = $(this).data("title")
        $(".review-content__wrapper__box-comment__score__text").attr('data-default', data_title)
    })


});

//# sourceMappingURL=scripts.js.map
