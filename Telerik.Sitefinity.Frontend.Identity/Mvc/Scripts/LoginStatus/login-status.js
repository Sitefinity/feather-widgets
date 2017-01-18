(function ($) {
    $(function () {
        $('[data-sf-role="login-status-button"]').on('click', function () {
            location.href = $('[data-sf-role="sf-login-redirect-url"]').val() || '#';
            return false;
        });

        if ($('[data-sf-role="sf-is-design-mode-value"]').val().toLowerCase() !== 'true') {
            $.ajax({
                url: $('[data-sf-role="sf-status-json-endpoint-url"]').val(),
                cache: false,
                success: function (statusViewModel) {
                    if (statusViewModel && statusViewModel.IsLoggedIn) {
                        var loggedInView = $('[data-sf-role="sf-logged-in-view"]');
                        loggedInView.find('[data-sf-role="sf-logged-in-avatar"]').attr('src', statusViewModel.AvatarImageUrl).attr('alt', statusViewModel.DisplayName);
                        loggedInView.find('[data-sf-role="sf-logged-in-name"]').html(statusViewModel.DisplayName);
                        loggedInView.find('[data-sf-role="sf-logged-in-email"]').html(statusViewModel.Email);
                        loggedInView.find('[data-sf-role="sf-logged-in-log-out-btn"]').on('click', function () {
                            var redirectUrl = $('[data-sf-role="sf-logout-redirect-url"]').val();
                            window.location.href = redirectUrl;
                            return false;
                        });

                        loggedInView.show();
                    }
                    else {
                        $('[data-sf-role="sf-logged-out-view"]').show();
                    }
                }
            });
        }
        else {
            $('[data-sf-role="sf-logged-out-view"]').show();
        }
    });
}(jQuery));