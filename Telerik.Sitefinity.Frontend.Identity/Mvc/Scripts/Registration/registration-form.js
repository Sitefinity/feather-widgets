(function ($) {
    var url = $('.sf-resend-confirmation-endpoint-url').val();
    $(".sendAgainLink").bind("click", function (e) {
        $.get(url, function (data) {
            if (JSON.parse(data)) {
                $(".confirmationResendInfo").show();
            }
        });
    });
}(jQuery));