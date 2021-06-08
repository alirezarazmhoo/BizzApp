const swiper = new Swiper(".swiper-container", {
  slidesPerView: 5,
  centeredSlides: false,
  breakpoints: {
    1200: {
      slidesPerView: 3.5,
    },
    768: {
      slidesPerView: 3,
    },
    320: {
      slidesPerView: 1.4,
    },
  },
  // Navigation arrows
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },

  // And if we need scrollbar
  scrollbar: {
    el: ".swiper-scrollbar",
  },
});
