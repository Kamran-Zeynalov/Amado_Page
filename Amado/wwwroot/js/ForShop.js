var titleLinks = document.querySelectorAll('.title-link');
var brandLinks = document.querySelectorAll('.brand-link');
var colorLinks = document.querySelectorAll('.color-link');

titleLinks.forEach(function (link) {
    link.addEventListener('click', function (event) {
        event.preventDefault();

        titleLinks.forEach(function (otherLink) {
            otherLink.classList.remove('active');
        });

        this.classList.add('active');

        var titleid = this.getAttribute('data-titleid');

        fetch('/shop/sorted?titleid=' + titleid)
            .then(response => response.text())
            .then(data => {
                var partialContainer = document.getElementById('partials');
                partialContainer.innerHTML = data;
            })
            .catch(error => {
                console.error(error);
            });
    });
});

brandLinks.forEach(function (link) {
    link.addEventListener('click', function (event) {
        event.preventDefault();

        brandLinks.forEach(function (otherLink) {
            otherLink.style.color = '#959595';
        });


        this.style.color = '#fbb710';

        var brandid = this.getAttribute('data-brandid');

        fetch('/shop/sorted?brandid=' + brandid)
            .then(response => response.text())
            .then(data => {
                var partialContainer = document.getElementById('partials');
                partialContainer.innerHTML = data;
            })
            .catch(error => {
                console.error(error);
            });
    });
});

colorLinks.forEach(function (link) {
    link.addEventListener('click', function (event) {
        event.preventDefault();

        var colorid = this.getAttribute('data-colorid');

        fetch('/shop/sorted?colorid=' + colorid)
            .then(response => response.text())
            .then(data => {
                var partialContainer = document.getElementById('partials');
                partialContainer.innerHTML = data;
            })
            .catch(error => {
                console.error(error);
            });
    });
});



const productsSection = document.querySelector("#partials");

function applyFilter(order) {
    setQueryParameter("order", order);
    renderProducts();
}
function applyFilterTake(viewTake) {
    setQueryParameter("viewTake", viewTake);
    renderProducts();
}
function initializePagination() {
    const pageBtns = document.querySelectorAll(".page-btn");
    pageBtns.forEach(function (btn) {
        btn.addEventListener('click', function () {
            pageBtns.forEach(function (otherBtn) {
                otherBtn.style.backgroundColor = "transparent";
            });
            btn.style.backgroundColor = "orange";
            const dataPage = btn.getAttribute('data-page');
            setQueryParameter("page", dataPage);
            renderProducts();
        });
    });
}

function setQueryParameter(key, value) {
    const url = new URL(window.location.href);
    url.searchParams.set(key, value);
    window.history.replaceState({}, '', url);
}

function getQueryParameter(key) {
    const url = new URL(window.location.href);
    return url.searchParams.get(key);
}


function renderProducts() {
    const page = getQueryParameter('page') || 1;
    const order = getQueryParameter('order') || 'desc';
    const viewTake = getQueryParameter('viewTake') || 2;

    fetch(`https://localhost:7075/shop/filter?page=${page}&order=${order}&viewTake=${viewTake}`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Network response was not ok: ${ response.statusText }");
            }
            return response.text();
        })
        .then(html => {
            productsSection.innerHTML = html;
            initializePagination();
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

var largee = document.querySelector('#largeScreen');
var small = document.querySelector('#smallScreen');

largee.addEventListener('click', function (event) {
    event.preventDefault();

    var partialElement = document.querySelector('#partials');

    if (partialElement) {
        var colSm6Elements = partialElement.querySelectorAll('.col-sm-6');
        colSm6Elements.forEach(function (element) {
            element.classList.remove('col-sm-6');
            element.classList.remove('col-sm-12');
            element.classList.add('col-sm-12');
        });
    }
});

small.addEventListener('click', function (event) {
    event.preventDefault();

    var partialElement = document.querySelector('#partials');

    if (partialElement) {
        partialElement.querySelectorAll('.col-sm-12').forEach(function (element) {
            element.classList.remove('col-sm-12');
            element.classList.remove('col-sm-6');
            element.classList.add('col-sm-6');
        });
    }
});