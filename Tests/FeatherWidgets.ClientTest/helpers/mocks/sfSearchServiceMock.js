angular.module('sfServices')
.factory('sfSearchServiceMock', function ($rootScope, $q) {
    /* Private methods and variables */
    var dataItemPromise;

    var getSearchIndexes = function (skip, take, search) {
        if (dataItemPromise)
            return dataItemPromise;

        var deferred = $q.defer();

        var searchIndexes = {
            Items: [{
                ID: '4c003fb0-2a77-61ec-be54-ff00007864f4',
                Title: "search index 1"
            }],
            TotalCount: 1
        };

        initialData = $.extend(true, {}, searchIndexes);
        dataItemPromise = null;
        deferred.resolve(searchIndexes);

        dataItemPromise = deferred.promise;
        return dataItemPromise;
    };

    return {
        /* Returns the data items. */
        getSearchIndexes: getSearchIndexes
    };
});