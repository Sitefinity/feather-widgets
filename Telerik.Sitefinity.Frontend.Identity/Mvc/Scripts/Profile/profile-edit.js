(function ($) {
    $(function () {
        var hasPasswordErrors = $('[data-sf-role=has-password-errors]').val() === 'true';

        var changePasswordHolder = $('[data-sf-role=edit-profile-change-password-holder]');

        if (hasPasswordErrors || changePasswordHolder.find('input').val()) {
            changePasswordHolder.show();
            $('[data-sf-role=edit-profile-change-password-button]').hide();
        }

        var editProfileUserImage = $('[data-sf-role=edit-profile-user-image]');
        var fileInput = $('[data-sf-role=edit-profile-upload-picture-input]');

        $('[data-sf-role=edit-profile-change-password-button]').on('click', function (e) {
            e.preventDefault();

            changePasswordHolder.show();
            $(this).hide();
        });

        fileInput.on('change', function (e) {
            if (e.target.files && e.target.files[0]) {
                var reader = new FileReader();
                reader.onload = function (readerLoadedEvent) {
                    editProfileUserImage.attr('src', readerLoadedEvent.target.result);
                };
                reader.readAsDataURL(e.target.files[0]);
            }

            var form = document.forms.aspnetForm;
            if (form) {
                form.enctype = 'multipart/form-data';
            }
        });

        $('[data-sf-role=edit-profile-upload-picture-button]').click(function () {
            fileInput.click();
        });
    });
}(jQuery));