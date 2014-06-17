jQuery(document).ready(function () {
    jQuery('.nav-select').change(function () {
        var val = jQuery(this).find('option:selected').val();
        window.location.replace(val);
    });
});

