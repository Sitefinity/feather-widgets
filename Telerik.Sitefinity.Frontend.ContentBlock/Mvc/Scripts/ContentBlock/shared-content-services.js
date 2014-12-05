(function ($) {
	var sharedContentServices = angular.module('sharedContentServices', ['pageEditorServices', 'serverDataModule']);

	//this is the service responsible for managing the properties data for all interested parties
	sharedContentServices.factory('sharedContentService', ['$http', '$q', 'propertyService', 'widgetContext', 'serverData',
		function ($http, $q, propertyService, widgetContext, serverData) {
		    var CULTURE_HEADER = 'SF_UI_CULTURE',
			    EMPTY_GUID = '00000000-0000-0000-0000-000000000000';

		    /**
		     * Generates the headers dictionary for the HTTP request
		     * to be performed. The headers will contain by default the
		     * Sitefinity UI culture header.
		     */
		    var requestOptions = function () {
			    var header = {};

			    if (widgetContext.culture)
				    header[CULTURE_HEADER] = widgetContext.culture;

			    return {
				    cache: false,
				    headers: header
			    };
		    };

		    var getServiceUrl = function () {
		        return sitefinity.services.getContentItemServiceUrl();
		    };

		    //sends request for creating new content block item
		    var publish = function (title, content, providerName) {
			    var deferred = $q.defer(),
			        blankItem = $.parseJSON(serverData.get('blankContentItem')),
			        putUrl = getServiceUrl() + blankItem.Id + '/';

			    if (providerName)
				    putUrl += '?provider=' + providerName;

			    if (blankItem.PublicationDate)
				    blankItemPublicationDate = new Date();

			    blankItem.Title = {
				    Value: title,
				    PersistedValue: title
			    };
			    blankItem.Content = {
				    Value: content,
				    PersistedValue: content
			    };
			    blankItem.DateCreated = '\/Date(' + new Date(blankItem.DateCreated).getTime() + ')\/';
			    blankItem.PublicationDate = '\/Date(' + new Date(blankItem.PublicationDate).getTime() + ')\/';
			    var itemContext = {
				    Item: blankItem,
				    ItemType: 'Telerik.Sitefinity.GenericContent.Model.ContentItem'
			    };

			    $http.put(putUrl, itemContext, requestOptions())
				    .success(function (data) {
					    deferred.resolve(data);
				    })
				    .error(function (data) {
					    deferred.reject(data);
				    });

			    return deferred.promise;
		    };

		    //creates new content block item with the provided title
		    var share = function (title) {
		        // Shared content service is initialized once for several views. 
		        // We have to make sure that we have server data that is up to date.
		        serverData.refresh();

			    var properties;

			    var saveProperties = function (data) {
				    //change the SharedContentID property of the widget
				    properties.SharedContentID.PropertyValue = data.Item.Id;

				    var modifiedProperties = [properties.SharedContentID];
				    //The type of save that should be performed. 0 - default, 1 - all translations, 2 - currently translation only
				    var currentSaveMode = widgetContext.culture ? 1 : 0;

				    return propertyService.save(currentSaveMode, modifiedProperties);
			    };

			    return propertyService.get()
				    .then(function (data) {
					    properties = propertyService.toAssociativeArray(data.Items);
					    var content = properties.Content.PropertyValue;
					    var provider = properties.ProviderName.PropertyValue;

					    return publish(title, content, provider);
				    })
				    .then(saveProperties);
		    };

		    //updates content of the content block item
		    var update = function (itemData, content, providerName) {
		        // Shared content service is initialized once for several views. 
		        // We have to make sure that we have server data that is up to date.
		        serverData.refresh();

			    var currentItem = itemData.Item;
			    var putUrl = getServiceUrl() + currentItem.Id + '/?draftPageId=' + widgetContext.pageId;

			    if (providerName)
				    putUrl += '&provider=' + providerName;

			    currentItem.Content.Value = content;
			    currentItem.Content.PersistedValue = content;
			    itemData.Item = currentItem;

			    var deferred = $q.defer();

			    $http.put(putUrl, itemData, requestOptions())
				    .success(function (data) {
					    deferred.resolve(data);
				    })
				    .error(function (data) {
					    deferred.reject(data);
				    });

			    return deferred.promise;
		    };

		    //gets the content block depending on the provided shareContentId
		    var get = function (sharedContentId, providerName, checkOut) {
		        // Shared content service is initialized once for several views. 
		        // We have to make sure that we have server data that is up to date.
		        serverData.refresh();

		        var getUrl = getServiceUrl() + sharedContentId + '/?published=true&checkOut=' + checkOut;

			    if (providerName)
				    getUrl += '&provider=' + providerName;

			    var deferred = $q.defer();
			    $http.get(getUrl, requestOptions())
				    .success(function (data) {
					    deferred.resolve(data);
				    })
				    .error(function (data) {
					    deferred.reject(data);
				    });

			    return deferred.promise;
		    };

		    //get content items for particular provider
		    var getItems = function (providerName, skip, take, filter) {
		        // Shared content service is initialized once for several views. 
		        // We have to make sure that we have server data that is up to date.
		        serverData.refresh();

		        var getUrl = getServiceUrl() +
				    '?itemType=Telerik.Sitefinity.GenericContent.Model.Content' +
				    '&itemSurrogateType=Telerik.Sitefinity.GenericContent.Model.Content' +
				    '&filter=Visible==true AND Status==Live';

			    var culture = widgetContext.culture;
			    if (culture)
				    getUrl += ' AND Culture == ' + culture;

			    if (filter)
				    getUrl += ' AND (Title.ToUpper().Contains("' + filter + '".ToUpper()))';

			    if (skip)
				    getUrl += '&skip=' + skip;

			    if (take)
				    getUrl += '&take=' + take;

			    if (providerName)
				    getUrl += '&allProviders=false&provider=' + providerName;

			    var deferred = $q.defer();
			    $http.get(getUrl, requestOptions())
				    .success(function (data) {
					    deferred.resolve(data);
				    })
				    .error(function (data) {
					    deferred.reject(data);
				    });

			    return deferred.promise;
		    };

		    //deletes the temp item for a content with the provided id
		    var deleteTemp = function (id) {
		        // Shared content service is initialized once for several views. 
		        // We have to make sure that we have server data that is up to date.
		        serverData.refresh();

		        var deleteUrl = getServiceUrl() + 'temp/' + id + '/';

			    var deferred = $q.defer();
			    $http.delete(deleteUrl, requestOptions())
				    .success(function (data) {
					    deferred.resolve(data);
				    })
				    .error(function (data) {
					    deferred.reject(data);
				    });

			    return deferred.promise;
		    };

		    //the public interface of the service
		    return {
			    getItems: getItems,
			    get: get,
			    update: update,
			    share: share,
			    deleteTemp: deleteTemp
		    };
		}
	]);

})(jQuery);