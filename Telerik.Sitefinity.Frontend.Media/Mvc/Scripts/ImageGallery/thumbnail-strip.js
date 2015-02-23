jQuery(document).ready(function () {
    var populateDefaultItem = function () {
        var defaultElementIndex = 0;
        var firstImageElement = jQuery('.sf-Gallery a')[defaultElementIndex];
        if (firstImageElement) {
            populateSelecteditem(firstImageElement);
        }
    };

    var populateSelecteditem = function (element) {
        jQuery(element).attr('selected', true);
        var item = jQuery.parseJSON(jQuery(element).attr('data-item'));
        var selectedElementIndex = jQuery(element).index();

        jQuery('.sf-Gallery-image .img-responsive').attr('src', item['MediaUrl']);
        jQuery('.sf-Gallery-image .img-responsive').attr('title', item['Title']);
        jQuery('.sf-Gallery-image .img-responsive').attr('alt', item['AlternativeText']);
        jQuery('.image-title').html(item['Title']);
        jQuery('.description').html(item['Description']);
        jQuery('.item-index').html(selectedElementIndex + 1);
    };

    var removeCurrentlySelected = function () {
        var currentlySelected = jQuery('.sf-Gallery a[selected]');
        currentlySelected.removeAttr('selected');
    };

    jQuery('.sf-Gallery a').bind('click', function (e) {
        removeCurrentlySelected();
        populateSelecteditem(this);
    });

    jQuery('.sf-prev-link').bind('click', function (e) {
        var currentlySelected = jQuery('.sf-Gallery a[selected]');
        if (currentlySelected && currentlySelected.length > 0) {
            var prevElement = currentlySelected.prev('a');
            if (prevElement && prevElement.length > 0) {
                removeCurrentlySelected();
                populateSelecteditem(prevElement);
            }
        }
    });

    jQuery('.sf-next-link').bind('click', function (e) {
        var currentlySelected = jQuery('.sf-Gallery a[selected]');
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