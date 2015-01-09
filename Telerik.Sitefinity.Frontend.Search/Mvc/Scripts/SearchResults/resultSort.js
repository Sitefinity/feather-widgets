(function ($) {
    $(document).ready(function () {
        //Dropdownlist Selectedchange event
        $("#UserSort").change(function (value) {
            var selectedValue = $(value.currentTarget).val();
            window.location.href = window.location.href + "&orderBy=" + selectedValue;
        })
    });
})(jQuery);