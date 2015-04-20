; (function ($) {
    $(function () {
        var pageHref = location.href;
        var pageTitle = document.title;
        $('.video-link').magnificPopup({
            type: 'iframe',
            gallery: {
                enabled: true
            },
            preloader: false,
            zoom: {
                enabled: true,

                duration: 300,
                easing: 'ease-in-out',
                opener: function (openerElement) {
                    return openerElement.is('img') ? openerElement : openerElement.find('img');
                }
            },
            callbacks: {
                change: function () {
                    var img = this.currItem.el.is('img') ? this.currItem.el : this.currItem.el.find('img');
                    var detailUrl = img.attr('data-detail-url');
                    if (detailUrl && history.state !== this.currItem.index) {
                        history.pushState(this.currItem.index, img.attr('alt'), detailUrl);
                    }
                },
                close: function () {
                    if (pageHref !== location.href) {
                        history.pushState(null, pageTitle, pageHref);
                    }
                }
            }
        });
    });

    window.addEventListener('popstate', function (e) {
        if (e.state !== undefined && e.state !== null && typeof e.state === 'number') {
            if (e.state >= 0 && e.state < $.magnificPopup.instance.items.length) {
                $('.image-link').magnificPopup('open', e.state);
            }
        }
        else {
            $('.image-link').magnificPopup('close');
        }
    });
})(jQuery);