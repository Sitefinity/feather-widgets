(function ($) {
    $(function () {
        $('.image-link').magnificPopup({
            type: 'image',
            gallery: {
                enabled: true
            },
            type: 'image',
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
                    if (detailUrl) {
                        history.pushState('data', img.attr('title'), detailUrl);
                    }
                }
            }
        });
    });
})(jQuery);