; (function ($) {
    $(document).ready(function () {
        var populateDefaultItem = function () {
            var defaultElementIndex = 0;
            var firstImageElement = $('.js-Gallery-thumbs').find('a')[defaultElementIndex];
            if (firstImageElement) {
                populateSelecteditem(firstImageElement);
            }
        };

        var populateSelecteditem = function (element) {
            $(element).addClass('is-selected');
            var item = $.parseJSON($(element).attr('data-item'));
            var detailUrl = $(element).attr('data-detail-url');
            var selectedElementIndex = $(element).index();

            $('.js-Gallery-image').find('img').attr('src', item.MediaUrl);
            $('.js-Gallery-image').find('img').attr('title', item.Title);
            $('.js-Gallery-image').find('img').attr('alt', item.AlternativeText);
            $('.js-Gallery-title').html(item.Title);
            $('.js-Gallery-description').html(item.Description);
            $('.js-Gallery-index').html(selectedElementIndex + 1);

            history.pushState('data', item.Title, detailUrl);
        };

        var removeCurrentlySelected = function () {
            var currentlySelected = $('.js-Gallery-thumbs').find('a.is-selected');
            currentlySelected.removeClass('is-selected');
        };

        $('.js-Gallery-thumbs').find('a').bind('click', function (e) {
            removeCurrentlySelected();
            populateSelecteditem(this);
        });

        $('.js-Gallery-prev').bind('click', function (e) {
            var currentlySelected = $('.js-Gallery-thumbs').find('a.is-selected');
            if (currentlySelected && currentlySelected.length > 0) {
                var prevElement = currentlySelected.prev('a');
                if (prevElement && prevElement.length > 0) {
                    removeCurrentlySelected();
                    populateSelecteditem(prevElement);
                }
            }
        });

        $('.js-Gallery-next').bind('click', function (e) {
            var currentlySelected = $('.js-Gallery-thumbs').find('a.is-selected');
            if (currentlySelected && currentlySelected.length > 0) {
                var nextElement = currentlySelected.next('a');
                if (nextElement && nextElement.length > 0) {
                    removeCurrentlySelected();
                    populateSelecteditem(nextElement);
                }
            }
        });

        populateDefaultItem();
    });
})(jQuery);