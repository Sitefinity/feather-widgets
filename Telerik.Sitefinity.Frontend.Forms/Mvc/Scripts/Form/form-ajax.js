; (function ($) {
    if (typeof window.FormData === 'undefined')
        return;

    $(function () {
        var formContainers = $('[data-sf-role="form-container"]:has([data-sf-role="ajax-submit-url"])');
        formContainers.each(function (i, element) {
            var formContainer = $(element);
            var loadingImg = formContainer.find('[data-sf-role="loading-img"]');
            var fieldsContainer = formContainer.find('[data-sf-role="fields-container"]');
            var successMessage = formContainer.find('[data-sf-role="success-message"]');
            var errorMessage = formContainer.find('[data-sf-role="error-message"]');
            var redirectUrl = formContainer.find('input[data-sf-role="redirect-url"]').val();
            var submitUrl = formContainer.find('input[data-sf-role="ajax-submit-url"]').val();

            var submitClickHandler = function () {
                var parentForm = formContainer.closest('form');
                var parentFormChildren = parentForm.children();

                if (parentForm.length > 0)
                    parentFormChildren.unwrap();

                var newForm = formContainer.find('form');
                var wrapped = false;
                if (newForm.length === 0) {
                    wrapped = true;
                    formContainer.wrap('<form />');
                    newForm = formContainer.parent();
                }

                newForm.one('submit', function () {
                    var inputs = formContainer.find('input');
                    var isValid = true;
                    for (var i = 0; i < inputs.length; i++) {
                        var jInput = $(inputs[i]);
                        if (typeof (jInput.data('sfvalidator')) === 'function')
                            isValid = jInput.data('sfvalidator')() && isValid;
                    }

                    if (!isValid)
                        return false;

                    var formData = new FormData(newForm[0]);
                    var request = new XMLHttpRequest();
                    request.open('POST', submitUrl);
                    request.onload = function () {
                        if (request.status === 200) {
                            var responseJson = JSON.parse(request.response);
                            if (responseJson.success) {
                                if (redirectUrl) {
                                    document.location.replace(redirectUrl);
                                }
                                else {
                                    successMessage.show();
                                    loadingImg.hide();
                                }
                            }
                            else {
                                errorMessage.text(responseJson.error);
                                errorMessage.show();
                                fieldsContainer.show();
                                loadingImg.hide();
                            }
                        }
                    };

                    loadingImg.show();
                    fieldsContainer.hide();
                    errorMessage.hide();

                    request.send(formData);

                    if (wrapped)
                        formContainer.unwrap();

                    if (parentForm.length > 0)
                        parentFormChildren.wrapAll(parentForm);

                    return false;
                });
            };

            formContainer.find('button[type="submit"],input[type="submit"]').click(submitClickHandler);
        });
    });
})(jQuery);