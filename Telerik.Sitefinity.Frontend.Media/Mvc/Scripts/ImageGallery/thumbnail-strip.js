; (function ($) {
    $(document).ready(function () {
        var populateDefaultItem = function () {
            var defaultElementIndex = 0;
            $('.js-Gallery-thumbs').each(function () {
                var firstImageElement = $(this).find('a')[defaultElementIndex];
                if (firstImageElement) {
                    populateSelecteditem(firstImageElement);
                }
            });
        };

        var populateSelecteditem = function (element, updateUrl) {
            $(element).addClass('is-selected');
            var item = $.parseJSON($(element).attr('data-item') || null);
            var selectedElementIndex = $(element).index();
            var gallery = $(element).parents('.sf-Gallery');
            var galerryImage = gallery.find('.js-Gallery-image');

            galerryImage.find('img').attr('src', item.MediaUrl);
            galerryImage.find('img').attr('title', item.Title);
            galerryImage.find('img').attr('alt', item.AlternativeText);

            if (item.Width) {
                galerryImage.find('img').attr("width", item.Width);
            }
            else {
                galerryImage.find('img').removeAttr("width");
            }

            if (item.Height) {
                galerryImage.find('img').attr("height", item.Height);
            }
            else {
                galerryImage.find('img').removeAttr("height");
            }
            
            gallery.find('.js-Gallery-title').html(item.Title);
            gallery.find('.js-Gallery-description').html(item.Description);
            gallery.find('.js-Gallery-index').html(selectedElementIndex + 1);

            if (updateUrl) {
                var detailUrl = $(element).attr('data-detail-url');
                if (detailUrl) {
                    history.pushState(detailUrl, item.Title, detailUrl);
                }
            }
        };

        var selectPrev = function (element) {
            var currentlySelected = $(element).parents('.js-Gallery-image').siblings('.sf-Gallery-thumbs-container').find('.js-Gallery-thumbs').find('a.is-selected');
            if (currentlySelected && currentlySelected.length > 0) {
                var prevElement = currentlySelected.prev('a');
                if (prevElement && prevElement.length > 0) {
                    removeCurrentlySelected(currentlySelected);
                    populateSelecteditem(prevElement, true);
                }
            }
        };

        var selectNext = function (element) {
            var currentlySelected = $(element).parents('.js-Gallery-image').siblings('.sf-Gallery-thumbs-container').find('.js-Gallery-thumbs').find('a.is-selected');
            if (currentlySelected && currentlySelected.length > 0) {
                var nextElement = currentlySelected.next('a');
                if (nextElement && nextElement.length > 0) {
                    removeCurrentlySelected(currentlySelected);
                    populateSelecteditem(nextElement, true);
                }
            }
        };

        var removeCurrentlySelected = function (element) {
            var currentlySelected = $(element).parents('.js-Gallery-thumbs').find('a.is-selected');
            currentlySelected.removeClass('is-selected');
        };

        $('.js-Gallery-thumbs').find('a').bind('click', function (e) {
            removeCurrentlySelected(this);
            populateSelecteditem(this, true);
        });

        $('.js-Gallery-thumbs').find('a').bind('keypress', function (e) {
            var key = e.which;
            if (key == 13)  // the enter key code
            {
                removeCurrentlySelected(this);
                populateSelecteditem(this, true);
            }
        });

        $('.js-Gallery-prev').bind('click', function (e) {
            selectPrev(this);
        });

        $('.js-Gallery-next').bind('click', function (e) {
            selectNext(this);
        });

        $('.js-Gallery-prev').bind('keypress', function (e) {
            var key = e.which;
            if (key == 13)  // the enter key code
            {
                selectPrev(this);
            }
        });

        $('.js-Gallery-next').bind('keypress', function (e) {
            var key = e.which;
            if (key == 13)  // the enter key code
            {
                selectNext(this);
            }
        });

        populateDefaultItem();

        window.addEventListener('popstate', function (e) {
            if (e.state) {
                var img = $('[data-detail-url="' + e.state + '"]');
                if (img.length > 0) {
                    populateSelecteditem(img[0]);
                }
            }
            else {
                populateDefaultItem();
            }
        });
    });
})(jQuery);