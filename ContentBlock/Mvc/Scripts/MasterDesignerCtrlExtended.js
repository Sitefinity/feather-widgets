(function () {
    if (typeof ($telerik) != "undefined") {

        var shareContentIdProperty = null;
        var shareContentIdPropertyValue = "";
        var newShareContentIdPropertyValue = "";
        var contentProperty = null;
        var contentPropertyValue = "";
        var newContentPropertyValue = "";
        var providerName = "";
        var contentBlockItem = null;

        var unlockContentItem = function () {
            var onUnlockContentBlockSuccess = function () { };
            var onUnlockContentBlockError = function () { };

            //unlock the item
            if (shareContentIdPropertyValue && shareContentIdPropertyValue != "00000000-0000-0000-0000-000000000000") {
                var shareContentService = angular.injector(['ng', 'shareContentServices']).get("ShareContentService");
                shareContentService.deleteTemp(shareContentIdPropertyValue, onUnlockContentBlockSuccess, onUnlockContentBlockError);
            }
        };

        var resetHelperProperties = function () {
            shareContentIdPropertyValue = "";
            contentPropertyValue = "";
            providerName = "";
        };

        $telerik.$(document).one("controlPropertiesUpdating", function (e, params) {
            var socialShareProperty;
            var providerProperty;

            var onSaveContentBlockSuccess = function () {
                if (validateProperties())
                    $telerik.$(document).trigger("controlPropertiesUpdate", [{ "Items": [contentProperty, socialShareProperty] }]);
                else
                    $telerik.$(document).trigger("controlPropertiesUpdate",
                        [{ "error": "You have entered invalid arguments!" }]);
            };
            var onSaveContentBlockError = function () { };

            var validateProperties = function () {
                validGuid = /^(\{|\()?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}(\}|\))?$/;

                if (socialShareProperty && socialShareProperty.PropertyValue.toLowerCase() != "false")
                    socialShareProperty.PropertyValue = "true";

                return validGuid.test(newShareContentIdPropertyValue);
            };

            for (var i = 0; i < params.Items.length; i++) {
                if (params.Items[i].PropertyName === "SharedContentID") {
                    shareContentIdProperty = params.Items[i];
                    newShareContentIdPropertyValue = shareContentIdProperty.PropertyValue;
                }
                if (params.Items[i].PropertyName === "Content") {
                    contentProperty = params.Items[i];
                    newContentPropertyValue = contentProperty.PropertyValue;
                }
                if (params.Items[i].PropertyName === "ProviderName")
                    providerProperty = params.Items[i];
                if (params.Items[i].PropertyName === "EnableSocialSharing")
                    socialShareProperty = params.Items[i];
            }

            if (validateProperties()) {//validate data
                //if content is shared properties changes should be handled separately
                if (contentBlockItem && shareContentIdPropertyValue != "00000000-0000-0000-0000-000000000000") {
                    params.args.Cancel = true;

                    //is only the content value has been modified
                    if (providerProperty.PropertyValue == providerName &&
                        shareContentIdPropertyValue == newShareContentIdPropertyValue
                        && newContentPropertyValue != contentPropertyValue) {
                        var shareContentService = angular.injector(['ng', 'shareContentServices']).get("ShareContentService");
                        shareContentService.updateContent(contentBlockItem, newContentPropertyValue,
                                providerName, onSaveContentBlockSuccess, onSaveContentBlockError);
                    }
                    else {//if shared content id is modified.
                        unlockContentItem();

                        $telerik.$(document).trigger("controlPropertiesUpdate",
                            [{ "Items": [shareContentIdProperty, socialShareProperty, providerProperty] }]);
                    }
                }
            }
            else {
                params.args.Cancel = true;
                $telerik.$(document).trigger("controlPropertiesUpdate",
                    [{ "error": "SharedContentID that you have entered is invalid!" }]);
            }

            //reset initial properties
            resetHelperProperties();
        });

        $telerik.$(document).one("controlPropertiesUpdateCanceling", function (e, params) {
            unlockContentItem();
            resetHelperProperties();
        });

        $telerik.$(document).one("controlPropertiesLoad", function (e, params) {

            for (var i = 0; i < params.Items.length; i++) {
                if (params.Items[i].PropertyName === "SharedContentID")
                    shareContentIdPropertyValue = params.Items[i].PropertyValue;
                if (params.Items[i].PropertyName === "Content") {
                    contentPropertyValue = params.Items[i].PropertyValue;
                }
                if (params.Items[i].PropertyName === "ProviderName")
                    providerName = params.Items[i].PropertyValue;
            }

            var onGetContentBlockSuccess = function (data) {
                contentBlockItem = data;
                if (data && data.Item) {
                    for (var i = 0; i < params.Items.length; i++) {
                        if (params.Items[i].PropertyName === "Content") {
                            contentPropertyValue = data.Item.Content.Value;
                            params.Items[i].PropertyValue = contentPropertyValue;
                        }
                    }

                    $telerik.$(document).trigger("controlPropertiesLoaded", [{ "Items": params.Items }]);
                }
            };
            var onGetContentBlockError = function () {
                for (var i = 0; i < params.Items.length; i++) {
                    if (params.Items[i].PropertyName === "Content")
                        params.Items[i].PropertyValue = "";
                    if(params.Items[i].PropertyName === "ProviderName")
                        params.Items[i].PropertyValue = "";
                    if (params.Items[i].PropertyName === "SharedContentID") {
                        shareContentIdPropertyValue = "00000000-0000-0000-0000-000000000000";
                        params.Items[i].PropertyValue = shareContentIdPropertyValue;
                    }
                }

                $telerik.$(document).trigger("controlPropertiesLoaded", [{ "Items": params.Items }]);
            };

            if (shareContentIdPropertyValue != "00000000-0000-0000-0000-000000000000") {
                var shareContentService = angular.injector(['ng', 'shareContentServices']).get("ShareContentService");
                shareContentService.getContent(shareContentIdPropertyValue, providerName, true, onGetContentBlockSuccess, onGetContentBlockError);
            }
        });
    }
})();