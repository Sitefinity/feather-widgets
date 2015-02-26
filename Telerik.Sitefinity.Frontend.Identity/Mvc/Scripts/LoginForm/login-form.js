(function ($) {
    $("#RememberMe").bind("click", function () {
        $("#sf_persistent").val($(this).val());
    });
}(jQuery));