$(document).ready(() => {
  
  $('.home-slider').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    infinite: true,
    arrows: true,
    prevArrow: `<button class="slide-left"><i class="fas fa-chevron-left"></i></button>`,
    nextArrow: `<button class="slide-right"><i class="fas fa-chevron-right"></i></button>`,
    autoplay: true,
    autoplaySpeed: 2000,
    responsive: [
      {
        breakpoint: 768,
        settings: {
          arrows: false,
        }
      }
    ]
  })

  $('.newest-slider').slick({
    slidesToShow: 5,
    slidesToScroll: 5,
    infinite: true,
    arrows: true,
    prevArrow: `<button class="slide-left"><i class="fas fa-chevron-left"></i></button>`,
    nextArrow: `<button class="slide-right"><i class="fas fa-chevron-right"></i></button>`,
    autoplay: true,
    autoplaySpeed: 3000,
    responsive: [
      {
        breakpoint: 576,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 2,
          arrows: false,
        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 3,
          arrows: false,
        }
      },
    ]
  })

  $('.blog-slider').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    infinite: true,
    autoplay: true,
    autoplaySpeed: 2000,
    arrows: true,
    prevArrow: `<button class="slide-left"><i class="fas fa-chevron-left"></i></button>`,
    nextArrow: `<button class="slide-right"><i class="fas fa-chevron-right"></i></button>`,
    responsive: [
      {
        breakpoint: 576,
        settings: {
          arrows: false,
        }
      },
      {
        breakpoint: 768,
        settings: {
          arrows: false,
        }
      },
    ]
  })

  $('.single-product-slider').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    infinite: false,
    autoplay: false,
    arrows: false,
    draggable: false,
    fade: true,
    asNavFor: '.navs-product-slider',
  })

  $('.navs-product-slider').slick({
    slidesToShow: 4,
    slidesToScroll: 1,
    infinite: true,
    autoplay: true,
    arrows: false,
    focusOnSelect: true,
    asNavFor: '.single-product-slider',
    responsive: [
      {
        breakpoint: 576,
        settings: {
          slidesToShow: 4,
          slidesToScroll: 1,
        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 4,
          slidesToScroll: 1,
        }
      },
    ]
  })

})