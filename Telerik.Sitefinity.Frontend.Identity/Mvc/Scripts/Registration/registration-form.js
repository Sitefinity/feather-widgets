(function ($) {

    var url = $("[data-sf-role='sf-resend-confirmation-endpoint-url']").val();
    $("[data-sf-role='sendAgainLink']").bind("click", function (e) {
        $.get(url, function (data) {
            if (JSON.parse(data)) {
                $("[data-sf-role='confirmationResendInfo']").show();
            }
        });
    });
}(jQuery));