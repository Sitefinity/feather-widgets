; (function ($) {
    if (typeof window.FormData === 'undefined')
        return;

    $(function () {
        var formContainers = $('[data-sf-role="form-container"]');
        formContainers.each(function (i, element) {
            var formContainer = $(element);
            var loadingImg = formContainer.find('[data-sf-role="loading-img"]');
            var fieldsContainer = formContainer.find('[data-sf-role="fields-container"]');
            var successMessage = formContainer.find('[data-sf-role="success-message"]');
            var errorMessage = formContainer.find('[data-sf-role="error-message"]');
            var redirectUrl = formContainer.find('input[data-sf-role="redirect-url"]').val();
            var submitUrl = formContainer.find('input[data-sf-role="ajax-submit-url"]').val();
            var handler = function (submitEvent) {
                if (submitEvent.isPropagationStopped() || submitEvent.isDefaultPrevented())
                    return false;

                var nonFilefields = formContainer.find('*[name][value][type!="file"]');
                var flyingForm = $('<form />');
                nonFilefields.clone().appendTo(flyingForm);
                var formData = new FormData(flyingForm[0]);

                var fileFields = formContainer.find('input[name][type="file"]');
                for (var i = 0; i < fileFields.length; i++) {
                    if (fileFields[i].files && fileFields[i].files.length > 0)
                        formData.append(fileFields[i].name, fileFields[i].files[0]);
                }

                var selectFields = formContainer.find('select[name]');
                for (var j = 0; j < selectFields.length; j++) {
                    formData.append(selectFields[j].name, $(selectFields[j]).val());
                }

                var request = new XMLHttpRequest();
                request.open('POST', submitUrl);
                request.onload = function (ev) {
                    if (request.status === 200) {
                        if (redirectUrl) {
                            document.location.replace(redirectUrl);
                        }
                        else {
                            successMessage.show();
                            loadingImg.hide();
                        }
                    } else if (request.status === 409) {
                        //someone has thrown CancelationException, we shouldn't display anything.
                    } else {
                        var responseJson = JSON.parse(request.response);
                        errorMessage.text(responseJson.error);
                        errorMessage.show();
                        loadingImg.hide();
                    }
                };

                loadingImg.show();
                fieldsContainer.hide();
                errorMessage.hide();

                request.send(formData);

                return false;
            };

            var form = formContainer.find('form');
            if (form.length > 0)
                form.submit(handler);
            else
                formContainer.find('button[type="submit"],input[type="submit"]').click(handler);
        });
    });
})(jQuery);