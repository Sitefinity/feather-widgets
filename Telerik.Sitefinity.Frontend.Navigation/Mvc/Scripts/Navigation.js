(function () {
    /* Polyfills */

    if (window.NodeList && !NodeList.prototype.forEach) {
        NodeList.prototype.forEach = Array.prototype.forEach;
    }

    var initialized = false;
    var initialize = function () {
        if (!initialized) {
            initialized = true;
            document.querySelectorAll('.nav-select').forEach(function (x) {
                x.addEventListener('change', function (e) {
                    var checkedOption = e.currentTarget.querySelector('option:checked');
                    if (checkedOption) {
                        var val = checkedOption.value;
                        window.location.replace(val);
                    }
                });
            });
        }
    };

    document.addEventListener('DOMContentLoaded', function () {
        initialize();
    });

    if (window.personalizationManager) {
        window.personalizationManager.addPersonalizedContentLoaded(function () {
            initialize();
        });
    }
}());