(function () {
    /* Polyfills */

    if (window.NodeList && !NodeList.prototype.forEach) {
        NodeList.prototype.forEach = Array.prototype.forEach;
    }

    document.addEventListener('DOMContentLoaded', function () {
        document.querySelectorAll('.nav-select').forEach(function (x) {
            x.addEventListener('change', function (e) {
                var checkedOption = e.currentTarget.querySelector('option:checked');
                if (checkedOption) {
                    var val = checkedOption.value;
                    window.location.replace(val);
                }
            });
        });
    });
}());