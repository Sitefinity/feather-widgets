(function ($) {
    $(function () {
        if ($('.sf-is-design-mode-value').val().toLowerCase() !== 'true') {
            $.get($('.sf-status-json-endpoint-url').val(), function (statusViewModel) {
                if (statusViewModel && statusViewModel.IsLoggedIn) {
                    var loggedInView = $('.sf-logged-in-view');
                    loggedInView.find('.sf-logged-in-avatar').attr('src', statusViewModel.AvatarImageUrl).attr('alt', statusViewModel.DisplayName);
                    loggedInView.find('.sf-logged-in-name').html(statusViewModel.DisplayName);
                    loggedInView.find('.sf-logged-in-email').html(statusViewModel.Email);
                    loggedInView.find('.sf-logged-in-log-out-btn').on('click', function () {
                        $.get('/Sitefinity/SignOut?sts_signout=true', function (data) {
                            window.location.href = $('.sf-logout-redirect-url').val() || window.location.href;
                        });
                    });

                    loggedInView.show();
                }
                else {
                    $('.sf-logged-out-view').show();
                }
            });
        }
        else {
            $('.sf-designer-mode-view').show();
        }
    });
}(jQuery));