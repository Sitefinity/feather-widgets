(function () {
    var shareContentServices;
    try { shareContentServices = angular.module("shareContentServices") } catch (err) { shareContentServices = angular.module('shareContentServices', ['controlPropertyServices']); }

    //this is the service responsible for managing the properties data for all interested parties
    shareContentServices.factory('ShareContentService', function ($http, HelperDataService, PropertyDataService, PageControlDataService) {

        //sends request for creating new content block item
        publishShareContent = function (title, content, providerName, onsuccess, onerror) {
            if (HelperDataService.blankDataItem) {
                var blankItem = jQuery.parseJSON(HelperDataService.blankDataItem);
                putUrl = HelperDataService.contentItemsServiceUrl;
                var key = blankItem.Id;
                putUrl += key + "/";

                if (providerName)
                    putUrl += "?provider=" + providerName;

                if (blankItem.PublicationDate)
                    blankItemPublicationDate = new Date();
                blankItem.Title = new Object();
                blankItem.Title.Value = title;
                blankItem.Title.PersistedValue = title;
                blankItem.Content = new Object();
                blankItem.Content.Value = content;
                blankItem.Content.PersistedValue = content;
                blankItem.DateCreated = "\/Date(" + new Date(blankItem.DateCreated).getTime() + ")\/";
                blankItem.PublicationDate = "\/Date(" + new Date(blankItem.PublicationDate).getTime() + ")\/";
                var itemContext = new Object();
                itemContext["Item"] = blankItem;
                itemContext["ItemType"] = "Telerik.Sitefinity.GenericContent.Model.ContentItem";

                var culture = PageControlDataService.data.PropertyValueCulture;
                var cultureHeaders = { 'SF_UI_CULTURE': culture };
                $http.put(putUrl, itemContext, { headers: cultureHeaders })
                    .success(onsuccess)
                    .error(onerror);
            }
            else
                onerror({Detail: "HelperDataService should provide blankDataItem."});
        };

        //creates new content block item with the provided title
        shareContent = function (title, onsuccess, onerror) {
            var shareContentIdProperty;
            var onPublishContentSuccess = function (xhrData, status, headers, config) {
                //change the ShareContentId property of the widget
                shareContentIdProperty.PropertyValue = xhrData.Item.Id;

                var modifiedProperties = [];
                modifiedProperties.push(shareContentIdProperty);
                var currentSaveMode = 0;
                if (PageControlDataService.data.PropertyValueCulture) {
                    currentSaveMode = 1;
                }
                PropertyDataService.saveProperties(onsuccess, onerror, currentSaveMode, modifiedProperties);
            };
            var onGetPropertiesSuccess = function (xhrData, status, headers, config) {
                var content;
                var provider;
                if (xhrData) {
                    for (var i = 0; i < xhrData.Items.length; i++) {
                        if (xhrData.Items[i].PropertyName === "SharedContentID")
                            shareContentIdProperty = xhrData.Items[i];
                        if (xhrData.Items[i].PropertyName === "Content")
                            content = xhrData.Items[i].PropertyValue;
                        if (xhrData.Items[i].PropertyName === "ProviderName")
                            provider = xhrData.Items[i].PropertyValue;
                    }
                }
                //check whether content is already shared
                if (shareContentIdProperty.PropertyValue !== "00000000-0000-0000-0000-000000000000")
                    content = "";

                publishShareContent(title, content, provider, onPublishContentSuccess, onerror);
            };

            PropertyDataService.getProperties(onGetPropertiesSuccess, onerror);
        };

        //updates content of the content block item
        updateContent = function (itemData, content, providerName, onsuccess, onerror) {
            var currentItem = itemData.Item;
            putUrl = HelperDataService.contentItemsServiceUrl;
            var key = currentItem.Id;
            putUrl += key + "/?draftPageId=" + PageControlDataService.data.PageId;
            if (providerName)
                putUrl += "&provider=" + providerName;

            currentItem.Content.Value = content;
            currentItem.Content.PersistedValue = content;
            itemData["Item"] = currentItem;

            var culture = PageControlDataService.data.PropertyValueCulture;
            var cultureHeaders = { 'SF_UI_CULTURE': culture };
            $http.put(putUrl, itemData, { headers: cultureHeaders })
                .success(
                    function (xhrData, status, headers, config) {
                        onsuccess(xhrData, status, headers, config);
                    })
                .error(onerror);
        };

        //gets the content block depending on the provided shareContentId
        getContent = function (shareContentId, providerName, ifCheckOut, onsuccess, onerror) {
            getUrl = HelperDataService.contentItemsServiceUrl;
            getUrl += shareContentId + "/?published=true&checkOut=" + ifCheckOut;

            if (providerName)
                getUrl += "&provider=" + providerName;

            var culture = PageControlDataService.data.PropertyValueCulture;
            var cultureHeaders = { 'SF_UI_CULTURE': culture };
            $http.get(getUrl, { cache: false, headers: cultureHeaders })
                .success(
                    function (xhrData, status, headers, config) {
                        onsuccess(xhrData, status, headers, config);
                    })
                .error(onerror);
        };

        //get all content items for particular provider
        getContentItems = function (providerName, onsuccess, onerror) {
            var culture = PageControlDataService.data.PropertyValueCulture;

            getUrl = HelperDataService.contentItemsServiceUrl;
            getUrl += "	?itemType=Telerik.Sitefinity.GenericContent.Model.Content"
                + "&itemSurrogateType=Telerik.Sitefinity.GenericContent.Model.Content"                
                + "&skip=0"
                + "&take=50"
                + "&filter=Visible==true AND Status==Live";

            if (culture)
                getUrl+= "AND Culture == " + culture;

            if (providerName)
                getUrl += "&allProviders=false&provider=" + providerName;

            var cultureHeaders;
            if(culture)
                cultureHeaders = { 'SF_UI_CULTURE': culture };
            $http.get(getUrl, { cache: false, headers: cultureHeaders })
                .success(onsuccess)
                .error(onerror);
        };

        //deletes the temp item for a content with the provided id
        deleteTemp = function (id, onsuccess, onerror) {
            deleteUrl = HelperDataService.contentItemsServiceUrl;
            deleteUrl += "temp/" + id + "/";

            var culture = PageControlDataService.data.PropertyValueCulture;
            var cultureHeaders = { 'SF_UI_CULTURE': culture };
            $http.delete(deleteUrl, { cache: false, headers: cultureHeaders })
                .success(onsuccess)
                .error(onerror);
        }

        //the public interface of the service
        return {
            getContentItems: getContentItems,
            getContent: getContent,
            updateContent: updateContent,
            shareContent: shareContent,
            deleteTemp: deleteTemp
        };
    });
})();