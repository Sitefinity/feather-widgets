; (function ($) {
    $(function () {
        var pageHref = location.href;
        var pageTitle = document.title;
        
        // Initialize magnificPopup for each gallery separately
        $('.sfOverlayGallery').each(function () {
            var $gallery = $(this);
            var $imageLinks = $gallery.find('.image-link');
            
            $imageLinks.magnificPopup({
                type: 'image',
                gallery: {
                    enabled: true
                },
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

                        if (img && img.length > 0) {
                            var width = $(img[0]).data("width");
                            if (width) {
                                this.currItem.img.css("max-width", width);
                            }
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
    });

    window.addEventListener('popstate', function (e) {
        if (e.state !== undefined && e.state !== null && typeof e.state === 'number') {
            var magnificInstance = $.magnificPopup.instance;
            if (magnificInstance && e.state >= 0 && e.state < magnificInstance.items.length) {
                magnificInstance.goTo(e.state);
            }
        }
        else {
            if ($.magnificPopup.instance) {
                $.magnificPopup.instance.close();
            }
        }
    });
})(jQuery);