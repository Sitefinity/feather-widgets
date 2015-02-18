(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['designer', 'kendo.directives', 'sfFields', 'sfSelectors']);
    angular.module('designer').requires.push('simpleViewModule');

})(jQuery);
