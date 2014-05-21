(function ($) {
	var sharedContentServices = angular.module('sharedContentServices', ['pageEditorServices']);

	//this is the service responsible for managing the properties data for all interested parties
	sharedContentServices.factory('sharedContentService', function ($http, $q, descriptorService, propertyService, widgetContext) {

		var CULTURE_HEADER = 'SF_UI_CULTURE',
			EMPTY_GUID = '00000000-0000-0000-0000-000000000000';

		/**
		 * Generates the headers dictionary for the HTTP request
		 * to be performed. The headers will contain by default the
		 * Sitefinity UI culture header.
		 */
		var requestOptions = function () {
			var header = {};
			header[CULTURE_HEADER] = widgetContext.culture;
			return {
				cache: false,
				headers: header
			};
		};

		//sends request for creating new content block item
		var publish = function (title, content, providerName) {
			var deferred = $q.defer();

			var blankItem = $.parseJSON(descriptorService.blankDataItem);
			putUrl = descriptorService.contentItemsServiceUrl;
			var key = blankItem.Id;
			putUrl += key + '/';

			if (providerName)
				putUrl += '?provider=' + providerName;

			if (blankItem.PublicationDate)
				blankItemPublicationDate = new Date();

			blankItem.Title = new Object();
			blankItem.Title.Value = title;
			blankItem.Title.PersistedValue = title;
			blankItem.Content = new Object();
			blankItem.Content.Value = content;
			blankItem.Content.PersistedValue = content;
			blankItem.DateCreated = '\/Date(' + new Date(blankItem.DateCreated).getTime() + ')\/';
			blankItem.PublicationDate = '\/Date(' + new Date(blankItem.PublicationDate).getTime() + ')\/';
			var itemContext = new Object();
			itemContext['Item'] = blankItem;
			itemContext['ItemType'] = 'Telerik.Sitefinity.GenericContent.Model.ContentItem';

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
			
		    var sharedContentIdProperty,
			    deferred = $q.defer();

			var onPublishContentSuccess = function (data) {
				//change the SharedContentId property of the widget
				sharedContentIdProperty.PropertyValue = data.Item.Id;

				var modifiedProperties = [sharedContentIdProperty];

				//The type of save that should be performed. 0 - default, 1 - all translations, 2 - currently translation only
				var currentSaveMode = widgetContext.culture ? 1 : 0;

				propertyService.save(currentSaveMode, modifiedProperties).then(function (data) {
				    deferred.resolve(data);
				}, 
				function (data) {
				    deferred.reject(data);
				});
			};

			var onGetPropertiesSuccess = function (data) {
				var content;
				var provider;
				if (data) {
				    for (var i = 0; i < data.Items.length; i++) {
				        if (data.Items[i].PropertyName === 'SharedContentID')
							sharedContentIdProperty = data.Items[i];
				        if (data.Items[i].PropertyName === 'Content')
				            content = data.Items[i].PropertyValue;
				        if (data.Items[i].PropertyName === 'ProviderName')
				            provider = data.Items[i].PropertyValue;
					}
				}
				//check whether content is already shared
				if (sharedContentIdProperty.PropertyValue !== EMPTY_GUID)
					content = '';

				publish(title, content, provider).then(onPublishContentSuccess, function (data) {
				    deferred.reject(data);
				});
			};

			propertyService.get().then(onGetPropertiesSuccess, function (data) {
				deferred.reject(data);
			});

			return deferred.promise;
		};

		//updates content of the content block item
		var update = function (itemData, content, providerName) {
			var currentItem = itemData.Item;
			var putUrl = descriptorService.contentItemsServiceUrl;
			var key = currentItem.Id;
			putUrl += key + '/?draftPageId=' + widgetContext.PageId;

			if (providerName)
				putUrl += '&provider=' + providerName;

			currentItem.Content.Value = content;
			currentItem.Content.PersistedValue = content;
			itemData['Item'] = currentItem;

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
		var get = function (sharedContentId, providerName, ifCheckOut) {
			var getUrl = descriptorService.contentItemsServiceUrl + sharedContentId + '/?published=true&checkOut=' + ifCheckOut;

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

		//get all content items for particular provider
		var getAll = function (providerName) {

			var getUrl = descriptorService.contentItemsServiceUrl
				+ '?itemType=Telerik.Sitefinity.GenericContent.Model.Content'
				+ '&itemSurrogateType=Telerik.Sitefinity.GenericContent.Model.Content'
				+ '&skip=0'
				+ '&take=50'
				+ '&filter=Visible==true AND Status==Live';

			var culture = widgetContext.culture;
			if (culture)
				getUrl += 'AND Culture == ' + culture;

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
			var deleteUrl = descriptorService.contentItemsServiceUrl
				+ 'temp/' + id + '/';

			var deferred = $q.defer();
			$http.delete(deleteUrl, requestOptions())
				.success(function (data) {
					deferred.resolve(data);
				})
				.error(function (data) {
					deferred.reject(data);
				});

			return deferred.promise;
		}

		//the public interface of the service
		return {
			getAll: getAll,
			get: get,
			update: update,
			share: share,
			deleteTemp: deleteTemp
		};
	});

})(jQuery);