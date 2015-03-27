(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'sfSelectors']);
    angular.module('designer').requires.push('simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {

    }]);
})(jQuery);
