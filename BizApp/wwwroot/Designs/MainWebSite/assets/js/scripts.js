$(window).on('load', () => {


    $('a[href="#"]').on('click',function(e){
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

    var masonryOptions = {
        itemSelector: '.recent-activities__col',
        originLeft: false
    };

    var $masonry = $('.recent-activities__list').masonry(masonryOptions);

    $('.recent-activities__item__show-all').on('click',function(){
        $(this).siblings('.recent-activities__item__image')
        .addClass('recent-activities__item__image--active')
        $(this).remove();

        $masonry.masonry(masonryOptions)
    })

    

    $('.main-header__search__right').on('click',function() {
        $(this).toggleClass('main-header__search__right--open')
    })

    $('.main-header__search__left').on('click',function() {
        $(this).toggleClass('main-header__search__left--open')
    })

    $('.header-mobile3__menu-mobail__top__icon svg').on('click',function() {
        $(this).parent().toggleClass('header-mobile3__menu-mobail__top__icon--open');
        $('.header-mobile3__menu').toggle()
    })


    $('.write-areviews__item__text__close').on('click',function(){
        $(this).closest('[class^="col"]').remove()
    })

    $('.start-business__form__input__input-form-phone__lable a').on('click',function(){
        $(this).closest('span').remove();
        $('.start-business__form__input__country').show()
    })

    if($('body').hasClass('header-fix'))
    {   
        if($(window).scrollTop() >= 200)
        {
            $('body').addClass('header-menu-hide')
            $('.header-type1__menu-bottom').slideUp(300)
        }
        else
        {
            $('body').removeClass('header-menu-hide')
            $('.header-type1__menu-bottom').slideDown(300)
        }
        $(window).scroll(function(){
            if($(window).scrollTop() >= 200)
            {
                $('body').addClass('header-menu-hide')
                $('.header-type1__menu-bottom').slideUp(300)
            }
            else
            {
                $('body').removeClass('header-menu-hide')
                $('.header-type1__menu-bottom').slideDown(300)
            }
        })
    }

    $('.yelp-paris__content__item--more').on('click',function() {
        $(".yelp-paris__more").slideToggle()
    })
    


    if($('#mapid').length > 0)
    {
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

    $('.category-content__filter-mobail__button-list__list a').on('click',function(){
        $(this).addClass('active')
        $('.category-content__filter-mobail__button-list__map a').removeClass('active');
        $('.category-content__map').removeClass('active');
    })

    $('.category-content__filter-mobail__button-list__map a').on('click',function(){
        $(this).addClass('active')
        $('.category-content__filter-mobail__button-list__list a').removeClass('active');
        $('.category-content__map').addClass('active')
    })

    $('.category-content__filter-mobail__button-filter a').on('click',function(){
        $(this).toggleClass('active')
        $('.category-content__item-filter').toggleClass('category-content__item-filter--active')
    })
    

    $('.profile-content__list-profile__title').on('click',function(){
        $('.profile-content__list-profile ul').slideToggle(300)
    })

    $('.talk__content__title').on('click',function(){
        $('.talk__content__list-link').slideToggle(300)
    })
    

    $('.header-type1__menu__search__right').on('click',function(){
        $(this).toggleClass('header-type1__menu__search__right--open')
    
    })
    
    $('.header-type1__menu__search__left').on('click',function(){
        $(this).toggleClass('header-type1__menu__search__left--open')
    
    })

    $('.header-yelp__menu__search__right').on('click',function(){
        $(this).toggleClass('header-yelp__menu__search__right--open')

    })
    $('.header-yelp__menu__search__left').on('click',function(){
        $(this).toggleClass('header-yelp__menu__search__left--open')

    })
    $('.item-location__item__alert__icon > i').on('click',function(){
        $(this).closest('.item-location__item__alert').remove()
    })
    $('.item-location__content__desc__alert__close  > svg').on('click',function(){
        $(this).closest('.item-location__content__desc__alert').remove()
    })

    $('.header-type1__menu__user-nav > ul > li.has-sub').on('click',function(){
        $(this).find('>ul').toggle()
    })

    $('.header-mobile1__menu-mobail__top__user-nav > ul > li.has-sub').on('click',function(){
        $(this).find('>ul').toggle()
    })
    
    $("#dropzone").dropzone({ url: "/file/post" });
    
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
});


//# sourceMappingURL=scripts.js.map
