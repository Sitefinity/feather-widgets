jQuery(document).ready(function () {
    var populateDefaultItem = function () {
        var defaultElementIndex = 0;
        var firstImageElement = jQuery('.js-Gallery-thumbs').find('a')[defaultElementIndex];
        if (firstImageElement) {
            populateSelecteditem(firstImageElement);
        }
    };

    var populateSelecteditem = function (element) {
        jQuery(element).addClass('is-selected');
        var item = jQuery.parseJSON(jQuery(element).attr('data-item'));
        var selectedElementIndex = jQuery(element).index();

        jQuery('.js-Gallery-image').find('img').attr('src', item.MediaUrl);
        jQuery('.js-Gallery-image').find('img').attr('title', item.Title);
        jQuery('.js-Gallery-image').find('img').attr('alt', item.AlternativeText);
        jQuery('.js-Gallery-title').html(item.Title);
        jQuery('.js-Gallery-description').html(item.Description);
        jQuery('.js-Gallery-index').html(selectedElementIndex + 1);
    };

    var removeCurrentlySelected = function () {
        var currentlySelected = jQuery('.js-Gallery-thumbs').find('a.is-selected');
        currentlySelected.removeClass('is-selected');
    };

    jQuery('.js-Gallery-thumbs').find('a').bind('click', function (e) {
        removeCurrentlySelected();
        populateSelecteditem(this);
    });

    jQuery('.js-Gallery-prev').bind('click', function (e) {
        var currentlySelected = jQuery('.js-Gallery-thumbs').find('a.is-selected');
        if (currentlySelected && currentlySelected.length > 0) {
            var prevElement = currentlySelected.prev('a');
            if (prevElement && prevElement.length > 0) {
                removeCurrentlySelected();
                populateSelecteditem(prevElement);
            }
        }
    });

    jQuery('.js-Gallery-next').bind('click', function (e) {
        var currentlySelected = jQuery('.js-Gallery-thumbs').find('a.is-selected');
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
