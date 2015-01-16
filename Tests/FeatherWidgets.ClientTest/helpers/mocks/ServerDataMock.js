(function ($) {
    var serverDataModule = angular.module('serverDataModule');

    serverDataModule.provider('serverDataMock', function () {
        var serverData = {
            defaultView: "Simple",
            widgetName: "DynamicContent",
            controlId: "312d6fcf-94fd-622b-8416-ff0000d46a77",
            defaultProviderName: "dynamicProvider2",
            itemType: "Telerik.Sitefinity.DynamicTypes.Model.TestModule.TestContentType123",
            parentTypes: "[]"
        }

        var serverDataService = {
            get: function (key) {
                return serverData[key];
            },

            has: function (key) {
                return serverData.hasOwnProperty(key);
            },

            refresh: function () {
                return this;
            }
        };

        return {
            $get: function () {
                return serverDataService;
            }
        };
    });
})(jQuery);