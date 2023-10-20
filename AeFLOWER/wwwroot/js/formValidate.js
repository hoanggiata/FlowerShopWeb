$(document).ready(() => {

  if (document.querySelector('#contactForm')) {
    $('#contactForm').validate({
      rules: {
        'fullname': {
          minlength: 3,
          maxlength: 50,
        },
        'phone': {
          minlength: 10,
          maxlength: 11,
        },
        'email': {
          minlength: 3,
          maxlength: 50,
          email: true,
        },
        'message': {
          minlength: 100,
          maxlength: 300,
        },
      },
      messages: {
        'fullname': {
          required: 'Họ và tên không được để trống',
          minlength: 'Họ và tên dài 3 đến 50 ký tự',
          maxlength: 'Họ và tên dài 3 đến 50 ký tự',
        },
        'phone': {
          required: 'Số điện thoại không được để trống',
          minlength: 'Số điện thoại từ 10 đến 11 số',
          maxlength: 'Số điện thoại từ 10 đến 11 số',
        },
        'email': {
          required: 'Email không được để trống',
          minlength: 'Email dài 10 đến 50 ký tự',
          maxlength: 'Email dài 10 đến 50 ký tự',
          email: 'Email không phù hợp',
        },
        'message': {
          required: 'Nội dung không được để trống',
          minlength: 'Nội dung dài 100 đến 300 ký tự',
          maxlength: 'Nội dung dài 100 đến 300 ký tự',
        }
      },
    })
  }
  
})