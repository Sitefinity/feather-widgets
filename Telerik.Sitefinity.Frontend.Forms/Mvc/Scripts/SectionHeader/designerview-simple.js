(function () {
    angular.module('simpleViewModule', ['designer']);
    angular.module('designer').requires.push('expander', 'sfFields', 'sfSelectors', 'simpleViewModule');
})();