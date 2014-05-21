
/**
 * Override this object to create a custom configuration for the designer AngularJs module or to register additional dependencies.
 */
var designerExtensions = {
    /**
     * Configuration function of the designer module.
     */
    config: function ($routeProvider) {
        $routeProvider
            //the route points to a MVC controller action that returns a proper view
            .when('/property-grid', {
                templateUrl: 'property-grid-view', controller: 'propertyGridCtrl'
            })
            .otherwise({
                templateUrl: 'simple-view', controller: 'simpleCtrl'
            });
    },

    /**
     * Dependencies of the designer module.
     */
    dependencies: ['pageEditorServices', 'breadcrumb', 'ngRoute', 'modalDialog', 'kendo.directives']
};

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
                    getSharedContentService().update(contentBlockItem, newContentPropertyValue,
                            providerName).then(onSaveContentBlockSuccess, onSaveContentBlockError);
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

    $telerik.$(document).one('controlPropertiesLoad', function (e, params) {

        for (var i = 0; i < params.Items.length; i++) {
            if (params.Items[i].PropertyName === 'SharedContentID')
                sharedContentIdPropertyValue = params.Items[i].PropertyValue;
            if (params.Items[i].PropertyName === 'Content') {
                contentPropertyValue = params.Items[i].PropertyValue;
            }
            if (params.Items[i].PropertyName === 'ProviderName')
                providerName = params.Items[i].PropertyValue;
        }

        var onGetContentBlockSuccess = function (data) {
            contentBlockItem = data;
            if (data && data.Item) {
                for (var i = 0; i < params.Items.length; i++) {
                    if (params.Items[i].PropertyName === 'Content') {
                        contentPropertyValue = data.Item.Content.Value;
                        params.Items[i].PropertyValue = contentPropertyValue;
                    }
                }

                $telerik.$(document).trigger('controlPropertiesLoaded', [{ 'Items': params.Items }]);
            }
        };

        var onGetContentBlockError = function () {
            for (var i = 0; i < params.Items.length; i++) {
                if (params.Items[i].PropertyName === 'Content')
                    params.Items[i].PropertyValue = '';
                if (params.Items[i].PropertyName === 'ProviderName')
                    params.Items[i].PropertyValue = '';
                if (params.Items[i].PropertyName === 'SharedContentID') {
                    sharedContentIdPropertyValue = EMPTY_GUID;
                    params.Items[i].PropertyValue = sharedContentIdPropertyValue;
                }
            }

            $telerik.$(document).trigger('controlPropertiesLoaded', [{ 'Items': params.Items }]);
        };

        if (sharedContentIdPropertyValue != EMPTY_GUID) {
            getSharedContentService().get(sharedContentIdPropertyValue, providerName, true).then(onGetContentBlockSuccess, onGetContentBlockError);
        }
    });

})(jQuery);