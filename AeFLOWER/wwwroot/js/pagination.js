const pageNavs = document.querySelectorAll('.pagi-btn')
const pageItems = document.querySelectorAll('.page-num')
const pagePrev = document.querySelector('.pagi-btn-prev')
const pageNext = document.querySelector('.pagi-btn-next')

let pageNow = 1;

pagePrev.addEventListener('click', () => handlePagePrev())
pageNext.addEventListener('click', () => handlePageNext())

pageItems.forEach((pageItem, idx) => {
  pageItem.addEventListener('click', () => {
    pageNow = idx + 1;
    pageItems.forEach(pageItem => pageItem.classList.remove('active'))
    pageItem.classList.add('active')
  })
})

function handlePageNext() {
  pageNow++;
  if(pageNow > 6) pageNow = 6;

  pageItems.forEach(pageItem => {
    pageItem.classList.remove('active')
  })
  
  pageItems[pageNow - 1].classList.add('active')
}

function handlePagePrev() {
  pageNow--;
  if(pageNow < 1) pageNow = 1;

  pageItems.forEach(pageItem => {
    pageItem.classList.remove('active')
  })

  pageItems[pageNow - 1].classList.add('active')
}