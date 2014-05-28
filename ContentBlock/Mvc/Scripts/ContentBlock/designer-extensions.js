
(function ($) {
    if (typeof ($telerik) == 'undefined')
        return;

    var sharedContentIdProperty = null,
        sharedContentIdPropertyValue = '',
        newSharedContentIdPropertyValue = '',
        contentProperty = null,
        contentPropertyValue = '',
        newContentPropertyValue = '',
        providerName = '',
        contentBlockItem = null,
        EMPTY_GUID = '00000000-0000-0000-0000-000000000000';

    var getSharedContentService = function () {
        return angular.injector(['ng', 'sharedContentServices']).get('sharedContentService');
    };

    var unlockContentItem = function () {
        //unlock the item
        if (sharedContentIdPropertyValue && sharedContentIdPropertyValue != EMPTY_GUID) {
            getSharedContentService().deleteTemp(sharedContentIdPropertyValue);
        }
    };

    var resetHelperProperties = function () {
        sharedContentIdPropertyValue = '';
        contentPropertyValue = '';
        providerName = '';
    };

    $telerik.$(document).one('controlPropertiesUpdating', function (e, params) {
        var socialShareProperty;
        var providerProperty;

        var onSaveContentBlockSuccess = function () {
            if (validateProperties())
                $telerik.$(document).trigger('controlPropertiesUpdate', [{ 'Items': [contentProperty, socialShareProperty] }]);
            else
                $telerik.$(document).trigger('controlPropertiesUpdate',
                    [{ 'error': 'You have entered invalid arguments!' }]);
        };
        var onSaveContentBlockError = function () { };

        var validateProperties = function () {
            validGuid = /^(\{|\()?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}(\}|\))?$/;

            if (socialShareProperty && socialShareProperty.PropertyValue.toLowerCase() != 'false')
                socialShareProperty.PropertyValue = 'true';

            return validGuid.test(newSharedContentIdPropertyValue);
        };

        for (var i = 0; i < params.Items.length; i++) {
            if (params.Items[i].PropertyName === 'SharedContentID') {
                sharedContentIdProperty = params.Items[i];
                newSharedContentIdPropertyValue = sharedContentIdProperty.PropertyValue;
            }
            if (params.Items[i].PropertyName === 'Content') {
                contentProperty = params.Items[i];
                newContentPropertyValue = contentProperty.PropertyValue;
            }
            if (params.Items[i].PropertyName === 'ProviderName')
                providerProperty = params.Items[i];
            if (params.Items[i].PropertyName === 'EnableSocialSharing')
                socialShareProperty = params.Items[i];
        }

        if (validateProperties()) {//validate data
            //if content is shared properties changes should be handled separately
            if (contentBlockItem && sharedContentIdPropertyValue != EMPTY_GUID) {
                params.args.Cancel = true;

                //is only the content value has been modified
                if (providerProperty.PropertyValue == providerName &&
                    sharedContentIdPropertyValue == newSharedContentIdPropertyValue
                    && newContentPropertyValue != contentPropertyValue) {
                    getSharedContentService().update(contentBlockItem, newContentPropertyValue, providerName)
                        .then(onSaveContentBlockSuccess, onSaveContentBlockError);
                }
                else {//if shared content id is modified.
                    unlockContentItem();

                    $telerik.$(document).trigger('controlPropertiesUpdate',
                        [{ 'Items': [sharedContentIdProperty, socialShareProperty, providerProperty] }]);
                }
            }
        }
        else {
            params.args.Cancel = true;
            $telerik.$(document).trigger('controlPropertiesUpdate',
                [{ 'error': 'SharedContentID that you have entered is invalid!' }]);
        }

        //reset initial properties
        resetHelperProperties();
    });

    $telerik.$(document).one('controlPropertiesUpdateCanceling', function (e, params) {
        unlockContentItem();
        resetHelperProperties();
    });

})(jQuery);