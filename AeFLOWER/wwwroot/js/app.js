const navMenu = document.querySelector('.nav-menu')
const navToggle = document.querySelector('.nav-toggle')
const headerTop = document.querySelector('.header-top')
const scrollTop = document.querySelector('.scroll-top')

const tabs = document.querySelectorAll('.tab-item')
const panes = document.querySelectorAll('.pane-item')

const minus = document.querySelector('.minus')
const plus = document.querySelector('.plus')
const quantityControl = document.querySelector('.quantity-item')

showMenu(navToggle, navMenu)
showHeaderTop(headerTop)
showScrollTop(scrollTop)
handleTab(tabs, panes)

$('input.quantity-item').each(function () {
  var $this = $(this),
    qty = $this.parent().find('.is-form'),
    min = Number($this.attr('min')),
    max = Number($this.attr('max'))
  if (min == 0) {
    var d = 0
  } else d = min
  $(qty).on('click', function () {
    if ($(this).hasClass('minus')) {
      if (d > min) d += -1
    } else if ($(this).hasClass('plus')) {
      var x = Number($this.val()) + 1
      if (x <= max) d += 1
    }
    $this.attr('value', d).val(d)
  })
})

function showMenu(toggle, menu) {
  let isToggle = false;
  if (toggle && menu) toggle.addEventListener('click', () => {
    isToggle = isToggle ? false : true
    menu.classList.toggle('show-menu')
    toggle.innerHTML = isToggle ? '<i class="fas fa-times"></i>' : '<i class="fas fa-bars"></i>'
  })
}

function showScrollTop(scrollTop) {
  if (scrollTop) {
    window.addEventListener('scroll', () => {
      if (this.scrollY > 500) scrollTop.classList.add('show-scroll')
      else scrollTop.classList.remove('show-scroll')
    })
  }
}

function handleTab(tabs, panes) {
  if (tabs.length && panes.length) {
    tabs.forEach(tab => {
      tab.addEventListener('click', () => {
        tabs.forEach(tab => tab.classList.remove('tab-active'))
        tab.classList.add('tab-active')

        const tabId = tab.attributes.id.value;
        let paneActive = null;

        panes.forEach(pane => {
          pane.style.display = 'none'
          if (pane.attributes.id.value == tabId) paneActive = pane
        })

        paneActive.style.display = "block"
      })
    })
  }
}

function showHeaderTop(headerTop) {
  if (headerTop) {
    window.addEventListener('scroll', () => {
      if (this.scrollY > 0) headerTop.classList.add('hide')
      else headerTop.classList.remove('hide')
    })
  }
}
