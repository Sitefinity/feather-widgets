jQuery(document).ready(function () {
    jQuery('.nav-select').change(function () {
        var val = jQuery(this).find('option:selected').val();
        window.location.replace(val);
    });

    jQuery('.nav-toggle').bind('click', function () {
        jQuery(this).siblings('.nav').toggle();
        jQuery(this).toggleClass('active');
    });
});

