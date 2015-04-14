(function ($) {
    $(function () {
        var hasPasswordErrors = $('[data-sf-role=has-password-errors]').val() === 'true';

        var changePasswordHolder = $('[data-sf-role=edit-profile-change-password-holder]');

        if (!hasPasswordErrors && !changePasswordHolder.find('input').val()) {
            changePasswordHolder.hide();
        }

        var editProfileUserImage = $('[data-sf-role=edit-profile-user-image]');
        var fileInput = $('[data-sf-role=edit-profile-upload-picture-input]');

        $('[data-sf-role=edit-profile-change-password-button]').on('click', function (e) {
            e.preventDefault();

            changePasswordHolder.toggle();
            $(this).hide();
        });

        //$('[data-sf-role=edit-profile-delete-picture-button]').on('click', function (e) {
        //    e.preventDefault();

        //    editProfileUserImage.attr('src', $('[data-sf-role=edit-profile-default-avatar-url]').val()).hide();
        //    $('[data-sf-role=edit-profile-delete-picture]').val(true);
        //});

        fileInput.on('change', function (e) {
            if (e.target.files && e.target.files[0]) {
                var reader = new FileReader();
                reader.onload = function (readerLoadedEvent) {
                    editProfileUserImage.attr('src', readerLoadedEvent.target.result);
                };
                reader.readAsDataURL(e.target.files[0]);
            }
        });

        $('[data-sf-role=profile-submit]').click(function () {
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