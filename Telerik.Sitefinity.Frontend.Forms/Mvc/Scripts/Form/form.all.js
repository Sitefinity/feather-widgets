//form-ajax.js
(function ($) {
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
            var generalErrorMessage = formContainer.find('[data-sf-role="general-error-message"]');
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

                    if (typeof MarketoSubmitScript !== 'undefined') {
                        MarketoSubmitScript._populateFormId($(newForm).find('input[data-sf-role="form-id"]').val());
                        var newSubmitButtons = $(newForm).find(MarketoSubmitScript._settings.externalFormSubmitButtonsQuery);
                        if (newSubmitButtons.length > 0)
                            MarketoSubmitScript._formFields = MarketoSubmitScript._getExternalFormFields(newSubmitButtons[0]);
                        if (MarketoSubmitScript._formFields && MarketoSubmitScript._formFields.length === 0)
                            MarketoSubmitScript._populateFieldsFromLabels(newForm);
                        MarketoSubmitScript._formSubmitHandler(newForm);
                    }

                    var formData = new FormData(newForm[0]);
                    var request = new XMLHttpRequest();
                    request.open('POST', submitUrl);
                    request.onload = function () {
                        if (request.status === 200) {
                            var responseJson = JSON.parse(request.response);
                            if (responseJson.success) {
                                if (responseJson.redirectUrl && responseJson.redirectUrl !== '') {
                                    document.location.replace(responseJson.redirectUrl);
                                } else if (responseJson.message && responseJson.message !== '') {
                                    successMessage.text(responseJson.message);
                                    successMessage.show();
                                    loadingImg.hide();
                                } else {
                                    if (redirectUrl) {
                                        document.location.replace(redirectUrl);
                                    }
                                    else {
                                        successMessage.show();
                                        loadingImg.hide();
                                    }
                                }
                            }
                            else {
                                generalErrorMessage.text(responseJson.error);
                                generalErrorMessage.show();
                                fieldsContainer.show();
                                fieldsContainer.find('[data-sf-role="captcha-refresh-button"]').click();
                                loadingImg.hide();
                            }
                        }
                    };

                    loadingImg.show();
                    fieldsContainer.hide();
                    errorMessage.hide();
                    generalErrorMessage.hide();

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

//captcha.js
(function ($) {
    'use strict';

    /*
        Rest Api
    */
    var CaptchaRestApi = function (rootUrl) {
        this.rootUrl = (rootUrl && rootUrl[rootUrl.length - 1] !== '/') ? (rootUrl + '/') : rootUrl;
    };

    CaptchaRestApi.prototype = {
        makeAjax: function (url, type, data) {
            var options = {
                type: type || 'GET',
                url: url,
                contentType: 'application/json',
                accepts: {
                    text: 'application/json'
                },
                cache: false
            };

            if (data) {
                options.data = JSON.stringify(data);
            }

            return $.ajax(options);
        },

        getCaptcha: function () {
            var getCaptchaUrl = this.rootUrl;
            return this.makeAjax(getCaptchaUrl);
        },

        validateCaptcha: function (data) {
            var validateCaptchaUrl = this.rootUrl;
            return this.makeAjax(validateCaptchaUrl, 'POST', data);
        }
    };

    /*
        Widget
    */
    var CaptchaWidget = function (wrapper, settings, resources) {
        this.wrapper = wrapper;
        this.settings = settings;
        this.resources = resources;
    };

    CaptchaWidget.prototype = {
        /*
            Elements
        */
        getOrInitializeProperty: function (property, sfRole) {
            if (!this[property]) {
                this[property] = this.getElementByDataSfRole(sfRole);
            }

            return this[property];
        },
        getElementByDataSfRole: function (sfRole) {
            return this.wrapper.find('[data-sf-role="' + sfRole + '"]');
        },
        captchaImage: function () { return this.getOrInitializeProperty('_captchaImage', 'captcha-image'); },
        captchaAudio: function () { return this.getOrInitializeProperty('_captchaAudio', 'captcha-audio'); },
        captchaAudioBtn: function () { return this.getOrInitializeProperty('_captchaAudioBtn', 'captcha-audio-btn'); },
        captchaInput: function () { return this.getOrInitializeProperty('_captchaInput', 'captcha-input'); },
        captchaRefreshLink: function () { return this.getOrInitializeProperty('_captchaRefreshLink', 'captcha-refresh-button'); },
        captchaDataKey: function () { return this.getOrInitializeProperty('_captchaDataKey', 'captcha-k'); },
        captchaDataInvalidAnswerMessage: function () { return this.getOrInitializeProperty('_captchaDataInvalidAnswerMessage', 'captcha-iam'); },
        errorMessage: function () { return this.getOrInitializeProperty('_errorMessage', 'error-message'); },

        /*
            Logic
        */
        captchaRefresh: function () {
            var self = this;
            var deferred = $.Deferred();

            self.captchaImage().attr("src", "");
            self.captchaInput().hide();

            self.restApi.getCaptcha().then(function (data) {
                if (data) {
                    self.captchaImage().attr("src", "data:image/png;base64," + data.Image);
                    self.captchaAudio().attr("src", "data:audio/wav;base64," + data.Audio);
                    self.captchaAudioBtn().click(function () { self.captchaAudio()[0].play(); });
                    self.captchaDataKey().val(data.Key);
                    self.captchaInput().val("");
                    self.captchaInput().show();
                    self.wrapper.show();
                }

                deferred.resolve(true);
            });

            return deferred;
        },

        validateInput: function () {
            var self = this;
            var deferred = $.Deferred(),
                data = {
                    Answer: self.captchaInput().val(),
                    Key: self.captchaDataKey().val()
                };

            self.restApi.validateCaptcha(data).then(function (validationResult) {
                if (validationResult.IsValid) {
                    self.hideInvalidMessage();
                } else {
                    self.showInvalidMessage();
                    if (validationResult.RefreshCaptcha) {
                        self.captchaRefresh();
                    } else {
                        self.wrapper.find('input').focus();
                        self.wrapper.find('input').select();
                    }
                }

                deferred.resolve(validationResult.IsValid);
            });

            return deferred;
        },

        /*
            Initialization
        */
        initializeProperties: function () {
            this.restApi = new CaptchaRestApi(this.settings.rootUrl);

            this.captchaData = {
                key: null
            };
        },

        initializeCaptcha: function () {
            this.captchaRefresh();
        },

        initializeHandlers: function () {
            var self = this;

            self.captchaRefreshLink().click(function () {
                self.captchaRefresh();
                return false;
            });
        },

        showInvalidMessage: function () {
            // If there is no error message container the old logic is required
            if (this.errorMessage().length > 0) {
                var messasge = this.captchaDataInvalidAnswerMessage().val();
                this.errorMessage().text(messasge);
                this.errorMessage().show();
            } else {
                this.getElementByDataSfRole('invalid-captcha-input').css('visibility', 'visible');
            }
        },

        hideInvalidMessage: function () {
            // If there is no error message container the old logic is required
            if (this.errorMessage().length > 0) {
                this.errorMessage().hide();
            } else {
                this.getElementByDataSfRole('invalid-captcha-input').css('visibility', 'hidden');
            }
        },

        initialize: function () {
            this.initializeProperties();
            this.initializeCaptcha();
            this.initializeHandlers();
            this.hideInvalidMessage();
        }
    };

    function getValidationMessages(input) {
        var container = $(input).parents('[data-sf-role="field-captcha-container"]');
        var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
        var validationMessages = JSON.parse(validationMessagesInput.val());

        return validationMessages;
    }

    function setErrorMessage(input, message) {
        var errorMessagesContainer = _getErrorMessageContainer(input);

        if (errorMessagesContainer) {
            _toggleCustomErrorMessage(errorMessagesContainer, message);
        } else {
            input.setCustomValidity(message);
        }
    }

    function _toggleCustomErrorMessage(container, message) {
        container.innerText = message;

        if (message === '') {
            container.style.display = 'none';
        } else {
            container.style.display = 'block';
        }
    }

    function _getErrorMessageContainer(input) {
        var container = $(input).closest('[data-sf-role="field-captcha-container"]')[0];
        if (container) {
            var errorMessagesContainer = container.querySelector('[data-sf-role="error-message"]');
            return errorMessagesContainer;
        }

        return null;
    }

    function changeOrInput(e) {
        if (typeof e.target.validity == 'undefined')
            return;

        if (e.target.required && e.target.validity.valueMissing) {
            var validationMessages = getValidationMessages(e.target);
            setErrorMessage(e.target, validationMessages.required);
        } else {
            setErrorMessage(e.target, '');
        }

        // If there is no error message container the old logic is required
        if (!_getErrorMessageContainer(e.target)) {
            e.data.hideInvalidMessage();
        }
    }

    function invalid(e) {
        if (typeof e.target.validity == 'undefined')
            return;

        if (_getErrorMessageContainer(e.target)) {
            e.preventDefault();
        }

        var validationMessages = getValidationMessages(e.target);

        if (e.target.validity.valueMissing) {
            setErrorMessage(e.target, validationMessages.required);
        }
    }

    /*
        Widget creation
    */
    $(function () {
        $('[data-sf-role="field-captcha-container"]').each(function () {
            var container = $(this);
            var rootUrl = container.find('[data-sf-role="captcha-settings"]').val();
            var captcha = new CaptchaWidget(container, { rootUrl: rootUrl });
            captcha.initialize();

            var input = container.find('[data-sf-role="captcha-input"]');

            if (input) {
                input.on('change', captcha, changeOrInput);
                input.on('input', captcha, changeOrInput);
                input.on('invalid', invalid);
                input.data('widget-validator', function () {
                    return captcha.validateInput();
                });
            }
        });
    });
}(jQuery));
//checkboxes-field.js
(function ($) {
    var changeOrInput = function (e) {
        var container = $(e.target).parents('[data-sf-role="checkboxes-field-container"]');
        var inputs = $(container).find('[data-sf-role="checkboxes-field-input"]');
        var hasChecked = $(container).find('input[data-sf-role="checkboxes-field-input"]:checked').length > 0;

        if (hasChecked || container.find('[data-sf-role="required-validator"]').val() === 'False') {
            $(inputs[0]).removeAttr('required');
            setErrorMessage(inputs[0], '');
        }
        else {
            $(inputs[0]).attr('required', 'required');
        }
    };

    var invalid = function (e) {
        var validationMessages = getValidationMessages(e.target);

        if (_getErrorMessageContainer(e.target)) {
            e.preventDefault();
        }

        if (e.target.validity.valueMissing) {
            setErrorMessage(e.target, validationMessages.required);
        }
    };

    var getValidationMessages = function (input) {
        var container = $(input).parents('[data-sf-role="checkboxes-field-container"]');
        var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
        var validationMessages = JSON.parse(validationMessagesInput.val());

        return validationMessages;
    };

    var setErrorMessage = function (input, message) {
        var errorMessagesContainer = _getErrorMessageContainer(input);

        if (errorMessagesContainer) {
            _toggleCustomErrorMessage(errorMessagesContainer, message);
        } else {
            input.setCustomValidity(message);
        }
    };

    var _toggleCustomErrorMessage = function (container, message) {
        container.innerText = message;

        if (message === '') {
            container.style.display = 'none';
        } else {
            container.style.display = 'block';
        }
    };

    var _getErrorMessageContainer = function (input) {
        var container = $(input).closest('[data-sf-role="checkboxes-field-container"]')[0];
        if (container) {
            var errorMessagesContainer = container.querySelector('[data-sf-role="error-message"]');
            return errorMessagesContainer;
        }

        return null;
    };

    $(function () {
        var containers = $('[data-sf-role="checkboxes-field-container"]');

        if (!containers || containers.length < 1)
            return;

        var attachHandlers = function (input) {
            input.on('change', function (e) {
                changeOrInput(e);
                if (typeof $.fn.processFormRules == 'function') {
                    $(e.target).processFormRules();
                }
            });
            input.on('input', changeOrInput);
            input.on('invalid', invalid);
        };

        var checkboxClickHandler = function (e) {
            var container = $(e.target).parents('[data-sf-role="checkboxes-field-container"]');
            var checkboxes = container.find('input[data-sf-role="checkboxes-field-input"]');
            var otherInput = $(container.find('[data-sf-checkboxes-role="other-choice-text"]').first());
            var otherCheckbox = $(container.find('[data-sf-checkboxes-role="other-choice-checkbox"]').first());
            var otherCheckboxIndex = checkboxes.index(otherCheckbox);
            var currentIndex = checkboxes.index($(e.target));
            var isRequired = container.find('[data-sf-role="required-validator"]').val() === 'True';

            if (otherCheckbox.is(':checked')) {
                otherInput.attr('type', 'text');

                if (isRequired)
                    otherInput.attr('required', 'required');
                else
                    otherInput.removeAttr('required');
            }
            else {
                otherInput.attr('type', 'hidden');
                otherInput.removeAttr('required');
            }
        };

        var inputChangeHandler = function (e) {
            var container = $(e.target).parents('[data-sf-role="checkboxes-field-container"]');
            var otherCheckbox = $(container.find('[data-sf-checkboxes-role="other-choice-checkbox"]').first());
            otherCheckbox.val($(e.target).val());

            if (typeof $.fn.processFormRules == 'function') {
                $(e.target).processFormRules();
            }
        };

        for (var i = 0; i < containers.length; i++) {
            var container = $(containers[i]);
            var inputs = container.find('[data-sf-role="checkboxes-field-input"]');

            if (container.find('[data-sf-role="required-validator"]').val() === 'True' && !inputs.is(':checked'))
                $(inputs[0]).attr('required', 'required');

            attachHandlers(inputs);

            var checkboxes = container.find('input[data-sf-role="checkboxes-field-input"]');

            var otherInput = $(container.find('[data-sf-checkboxes-role="other-choice-text"]').first());

            checkboxes.click(checkboxClickHandler);
            otherInput.change(inputChangeHandler);
        }
    });
}(jQuery));
//dropdown-list-field.js
(function ($) {
    $(function () {
        function changeOrInput(e) {
            if (e.target.value === '') {
                var validationMessages = getValidationMessages(e.target);
                setErrorMessage(e.target, validationMessages.required);
            }
            else {
                setErrorMessage(e.target, '');
            }

            if (typeof $.fn.processFormRules == 'function') {
                $(e.target).processFormRules();
            }
        }

        function invalid(e) {
            var validationMessages = getValidationMessages(e.target);

            if (_getErrorMessageContainer(e.target)) {
                e.preventDefault();
            }

            if (e.target.validity.valueMissing) {
                setErrorMessage(e.target, validationMessages.required);
            }
        }

        function getValidationMessages(input) {
            var container = $(input).parents('[data-sf-role="dropdown-list-field-container"]');
            var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
            var validationMessages = JSON.parse(validationMessagesInput.val());

            return validationMessages;
        }

        function setErrorMessage(input, message) {
            var errorMessagesContainer = _getErrorMessageContainer(input);

            if (errorMessagesContainer) {
                _toggleCustomErrorMessage(errorMessagesContainer, message);
            } else {
                input.setCustomValidity(message);
            }
        }

        function _toggleCustomErrorMessage(container, message) {
            container.innerText = message;

            if (message === '') {
                container.style.display = 'none';
            } else {
                container.style.display = 'block';
            }
        }

        function _getErrorMessageContainer(input) {
            var container = $(input).closest('[data-sf-role="dropdown-list-field-container"]')[0];
            if (container) {
                var errorMessagesContainer = container.querySelector('[data-sf-role="error-message"]');
                return errorMessagesContainer;
            }

            return null;
        }

        function init() {
            var containers = $('[data-sf-role="dropdown-list-field-container"]');

            if (!containers || containers.length < 1)
                return;

            for (var i = 0; i < containers.length; i++) {
                var select = $(containers[i]).find('[data-sf-role="dropdown-list-field-select"]');

                if (select) {
                    select.on('change', changeOrInput);
                    select.on('invalid', invalid);
                }
            }
        }

        init();
    });
}(jQuery));
//file-field.js
(function ($) {
    var htmlDecode = function (html) {
        var a = document.createElement('a'); a.innerHTML = html;
        return a.textContent;
    };

    var adjustVisibility = function (container) {
        var allRemoveLinks = container.find('[data-sf-role="remove-input"]');
        allRemoveLinks.toggle(allRemoveLinks.length > 1);
    };

    var initInput = function (template, container, config) {
        var input = $(template);
        input.appendTo(container);

        input.on('change', function (e) {
            if (typeof $.fn.processFormRules === 'function') {
                $(e.target).processFormRules();
            }
        });

        if (config.AllowMultipleFiles) {
            adjustVisibility(container);

            input.find('[data-sf-role="remove-input"]').click(function () {
                input.remove();
                adjustVisibility(container);
                if (typeof $.fn.processFormRules === 'function') {
                    $(container).processFormRules();
                }
            });
        }
    };

    var checkRequired = function (container) {
        // This is called only for input[type="file"]
        // If the fields is hidden, don't check for required
        if (container.is(":hidden"))
            return true;

        var violationMessage = $('[data-sf-role="required-violation-message"]');

        var inputs = container.find('input[type="file"]');
        var firstInvalidInput = null;
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].value) {
                violationMessage.hide();
                return true;
            } else if (!firstInvalidInput) {
                firstInvalidInput = inputs[i];
            }
        }

        violationMessage.show();
        firstInvalidInput.focus();
        return false;
    };

    var checkFileTypes = function (container, fileTypes) {
        var hasViolations = false;
        var inputs = container.find('input[type="file"]');
        for (var i = 0; i < inputs.length; i++) {
            var violationMessage = $(inputs[i]).closest('[data-sf-role="single-file-input"]').find('[data-sf-role="filetype-violation-message"]');
            if (inputs[i].value) {
                var stopIndex = inputs[i].value.lastIndexOf('.');
                if (stopIndex >= 0) {
                    var extension = inputs[i].value.substring(stopIndex).toLowerCase();
                    if (fileTypes.indexOf(extension) < 0) {
                        violationMessage.show();
                        hasViolations = true;
                        inputs[i].focus();
                        continue;
                    }
                }
            }

            violationMessage.hide();
        }

        return !hasViolations;
    };

    var checkFileSizes = function (container, min, max) {
        if (typeof window.File == 'undefined' || typeof window.FileList == 'undefined')
            return true;

        var hasViolations = false;
        var inputs = container.find('input[type="file"]');
        for (var i = 0; i < inputs.length; i++) {
            var violationMessage = $(inputs[i]).closest('[data-sf-role="single-file-input"]').find('[data-sf-role="filesize-violation-message"]');
            if (inputs[i].files.length > 0) {
                var file = inputs[i].files[0];
                if ((min > 0 && file.size < min) || (max > 0 && file.size > max)) {
                    violationMessage.show();
                    hasViolations = true;
                    inputs[i].focus();
                    continue;
                }
            }

            violationMessage.hide();
        }

        return !hasViolations;
    };

    var formValidation = function (ev) {
        var requiredValidationResult = !ev.data.config.IsRequired || checkRequired(ev.data.container);
        var fileTypesValidationResult = ev.data.config.AcceptedFileTypes.length === 0 || checkFileTypes(ev.data.container, ev.data.config.AcceptedFileTypes);
        var fileSizesValidationResult = !(ev.data.config.MinFileSizeInMb || ev.data.config.MaxFileSizeInMb) || checkFileSizes(ev.data.container, ev.data.config.MinFileSizeInMb * 1024 * 1024, ev.data.config.MaxFileSizeInMb * 1024 * 1024);

        return requiredValidationResult && fileTypesValidationResult && fileSizesValidationResult;
    };

    var init = function (element) {
        var jElement = $(element);
        var config = JSON.parse(htmlDecode(jElement.attr('data-sf-config')));

        var inputContainer = jElement.find('[data-sf-role="file-field-inputs"]');
        var inputTemplate = jElement.find('[data-sf-role="file-input-template"]').html();
        var form = jElement.closest('form');
        initInput(inputTemplate, inputContainer, config);

        if (config.AllowMultipleFiles) {
            $(jElement).find('[data-sf-role="add-input"]').click(function () {
                initInput(inputTemplate, inputContainer, config);
            });
        }

        jElement.find('input[type="file"]').data('sfvalidator', function () {
            return formValidation({ data: { config: config, container: inputContainer } });
        });

        form.submit({ config: config, container: inputContainer }, formValidation);
    };

    $(function () {
        var containers = $('[data-sf-role="file-field-container"]');
        if (!containers || containers.length < 1)
            return;

        for (var i = 0; i < containers.length; i++) {
            init(containers[i]);
        }
    });
}(jQuery));
//multiple-choice-field.js
(function ($) {
    var changeOrInput = function (e) {
        var container = $(e.target).parents('[data-sf-role="multiple-choice-field-container"]');
        var inputs = $(container).find('[data-sf-role="multiple-choice-field-input"]');
        inputs.each(function (index, input) {
            input.validity.valueMissing = false;
            setErrorMessage(input, '');
        });
    };

    var invalid = function (e) {
        var validationMessages = getValidationMessages(e.target);

        if (_getErrorMessageContainer(e.target)) {
            e.preventDefault();
        }

        if (e.target.validity.valueMissing) {
            setErrorMessage(e.target, validationMessages.required);
        }

        var isValidLength = e.target.value.length <= 255;
        if (e.target.validity.patternMismatch && !isValidLength) {
            setErrorMessage(e.target, validationMessages.maxLength);
        }
    };

    var getValidationMessages = function (input) {
        var container = $(input).parents('[data-sf-role="multiple-choice-field-container"]');
        var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
        var validationMessages = JSON.parse(validationMessagesInput.val());

        return validationMessages;
    };

    var setErrorMessage = function (input, message) {
        var errorMessagesContainer = _getErrorMessageContainer(input);

        if (errorMessagesContainer) {
            _toggleCustomErrorMessage(errorMessagesContainer, message);
        } else {
            input.setCustomValidity(message);
        }
    };

    var _toggleCustomErrorMessage = function (container, message) {
        container.innerText = message;

        if (message === '') {
            container.style.display = 'none';
        } else {
            container.style.display = 'block';
        }
    };

    var _getErrorMessageContainer = function (input) {
        var container = $(input).closest('[data-sf-role="multiple-choice-field-container"]')[0];
        if (container) {
            var errorMessagesContainer = container.querySelector('[data-sf-role="error-message"]');
            return errorMessagesContainer;
        }

        return null;
    };

    $(function () {
        var containers = $('[data-sf-role="multiple-choice-field-container"]');

        if (!containers || containers.length < 1)
            return;

        var attachHandlers = function (input) {
            input.on('change', function (e) {
                changeOrInput(e);
                if (typeof $.fn.processFormRules == 'function')
                    $(e.target).processFormRules();
            });
            input.on('input', changeOrInput);
            input.on('invalid', invalid);
        };

        var radioClickHandler = function (e) {
            var container = $(e.target).parents('[data-sf-role="multiple-choice-field-container"]');
            var radios = container.find('input[data-sf-role="multiple-choice-field-input"]');
            var otherInput = $(container.find('[data-sf-multiple-choice-role="other-choice-text"]').first());
            var otherRadio = $(container.find('[data-sf-multiple-choice-role="other-choice-radio"]').first());
            var otherRadioIndex = radios.index(otherRadio);
            var currentIndex = radios.index($(e.target));
            var isRequired = $(radios).first().attr('required');

            if (currentIndex == otherRadioIndex) {
                otherInput.attr('type', 'text');

                if (isRequired)
                    otherInput.attr('required', 'required');

                otherInput.attr('pattern', '.{0,255}');
                otherInput.on('invalid', invalid);
            }
            else {
                otherInput.attr('type', 'hidden');
                otherInput.removeAttr('required');
            }
        };

        var inputChangeHandler = function (e) {
            var container = $(e.target).parents('[data-sf-role="multiple-choice-field-container"]');
            var otherRadio = $(container.find('[data-sf-multiple-choice-role="other-choice-radio"]').first());
            otherRadio.val($(e.target).val());
        };

        for (var i = 0; i < containers.length; i++) {
            var container = $(containers[i]);
            var inputs = container.find('[data-sf-role="multiple-choice-field-input"]');

            attachHandlers(inputs);

            var radios = container.find('input[data-sf-role="multiple-choice-field-input"]');

            var otherInput = $(container.find('[data-sf-multiple-choice-role="other-choice-text"]').first());

            radios.click(radioClickHandler);
            otherInput.change(inputChangeHandler);
        }
    });
}(jQuery));
//navigation-field.js
; (function ($) {
    if (typeof window.FormData === 'undefined')
        return;

    $(function () {
        var formContainers = $('[data-sf-role="form-container"]');

        var initializeFormContainer = function (element) {
            var formElement = $(element);
            var formStepsContainers = formElement.find('[data-sf-role="separator"]');
            var navigationFieldContainers = formElement.find('[data-sf-role="navigation-field-container"]');
            var srProgressbar = formElement.find('[data-sf-role="sr-progressbar"]');

            // Selects element with class sf-sr-only and adds text: Step # of #
            var modifySrOnlyData = function (currentPage, totalPages, pageTitle) {
                // TODO: Accessibility is not implemented no logic is required
                if (srProgressbar.length <= 0) {
                    return;
                }

                var stepOfResource = $(element).find('[data-sf-role="step-of-resources"]').val();
                var valueText = stepOfResource.replace("{0}", currentPage).replace("{1}", totalPages) + ": " + pageTitle;
                srProgressbar.attr("aria-valuenow", currentPage);
                srProgressbar.attr("aria-valueText", valueText);
            };

            var updateNavigationFields = function (navigationElements, index) {

                navigationElements.each(function (navIndex, navigationElement) {
                    var navElement = $(navigationElement);
                    var pages = $(navigationElement).find('[data-sf-navigation-index]');
                    var numberOfAllSteps = formStepsContainers.length;
                    var progressInPercent = Math.round((index / numberOfAllSteps) * 100);

                    var progressBar = formElement.find('[data-sf-role="progress-bar"]');
                    var progressPercent = formElement.find('[data-sf-role="progress-percent"]');

                    if (progressBar && progressBar.length > 0) {
                        progressBar.width(progressInPercent + '%');
                    }

                    if (progressPercent && progressPercent.length > 0) {
                        progressPercent.text(progressInPercent + '%');
                    }

                    if (pages && pages.length > 0) {
                        pages.each(function (i, page) {
                            var pageIndex = parseInt($(page).data("sfNavigationIndex"));
                            var pageTitleWrp = $(page).find('[data-sf-page-title]');
                            var pageTitle = "";

                            var activeCssClass = $(navElement).attr("data-sf-active-css-class") || "active";
                            var pastCssClass = $(navElement).attr("data-sf-past-css-class") || "past";
                            var futureCssClass = $(navElement).attr("data-sf-future-css-class") || "future";

                            var pastIndicatorPast = $(page).find("[data-sf-progress-indicator='past']");
                            var pastIndicatorIncomplete = $(page).find("[data-sf-progress-indicator='incomplete']");

                            // TODO: Accessibility is implemented
                            if (pageTitleWrp.length > 0) {
                                pageTitle = $(pageTitleWrp).data("sfPageTitle");
                            }
                            $(page).removeClass(activeCssClass).removeClass(futureCssClass).removeClass(pastCssClass);

                            if (pageIndex !== index) {
                                if (pageIndex < index) {
                                    $(page).addClass(pastCssClass);
                                    $(pastIndicatorPast).show();
                                    $(pastIndicatorIncomplete).hide();
                                } else if (pageIndex > index) {
                                    $(page).addClass(futureCssClass);
                                    $(pastIndicatorPast).hide();
                                    $(pastIndicatorIncomplete).show();
                                }                                
                            } else {
                                $(page).addClass(activeCssClass);
                                $(pastIndicatorPast).hide();
                                $(pastIndicatorIncomplete).show();

                                // Because pageIndex starts from 0 we increase it by 1 so it is simple to read
                                var currentPage = ++pageIndex;
                                modifySrOnlyData(currentPage, pages.length, pageTitle);
                            }                            
                        });
                    }

                });
            };

            // Initialize navigation fields
            updateNavigationFields(navigationFieldContainers, 0);

            formElement.on("form-page-changed", function (e, index, previousIndex) {
                updateNavigationFields(navigationFieldContainers, index);
            });
        };


        formContainers.each(function (i, element) {
            initializeFormContainer(element);
        });

        // This implementation is only for the Form preview mode 
        var isPreviewMode = window.location.href.indexOf("/Preview") !== -1;
        if (formContainers.length === 0 && isPreviewMode) {
            var separator = $('[data-sf-role="separator"]');
            if (separator.length > 0) {
                initializeFormContainer(separator.parent());
            }
        }
    });
})(jQuery);
//page-break.js
; (function ($) {
    if (typeof window.FormData === 'undefined')
        return;

    $(function () {
        var selectors = {
            separator: '[data-sf-role="separator"]',
            formContainer: '[data-sf-role="form-container"]',
            previousButton: '[data-sf-btn-role="prev"]',
            nextButton: '[data-sf-btn-role="next"]'
        };
        var formContainers = $(selectors.formContainer);

        var initializeFormContainer = function (element) {
            var formElement = $(element);
            var formStepsContainers = formElement.find(selectors.separator);
            var formStepIndex = 0;
            var skipToPageCollection = [];
            var submitButton = null;
            var isSubmitButtonAdded = false;
            var stepNewForm = null;

            formElement.on("form-page-changed", function (e, index, previousIndex) {
                formStepIndex = index;
            });
            
            formElement.on("form-page-skip", function (e, skipToPageList) {
                skipToPageCollection = skipToPageList;
            });

            formStepsContainers.each(function (i, element) {
                $(element).hide();
            });

            formStepsContainers.first().show();
            formStepsContainers.first().find(selectors.previousButton).hide();
            formStepsContainers.last().find(selectors.nextButton).hide();

            var tryGoToNextStep = function (currentStepContainer, continueFunction) {
                var formContainer = $(currentStepContainer);
                stepNewForm = $('form#stepNewForm');
                if (stepNewForm.length === 0) {
                    formContainer.wrap('<form id="stepNewForm"></form>');
                    stepNewForm = formContainer.parent();
                }

                stepNewForm.one('submit', function (e) {
                    e.preventDefault();
                    if (e.target.innerHTML.length > 0) {
                        var inputs = stepNewForm.find('input');
                        var isValid = true;
                        for (var i = 0; i < inputs.length; i++) {
                            var jInput = $(inputs[i]);
                            if (typeof (jInput.data('sfvalidator')) === 'function')
                                isValid = jInput.data('sfvalidator')() && isValid;
                        }

                        if (!isValid) {
                            return false;
                        }

                        formContainer.unwrap();

                        if (isSubmitButtonAdded) {
                            submitButton.remove();
                        }

                        continueFunction();
                    }
                });

                submitButton = formContainer.find('input#stepNewFormSubmit');

                if (submitButton.length === 0) {
                    // If we do not have submit button in this step we need to add a hidden submit button in order to click it and trigger the native HTML 5
                    // browser validation
                    submitButton = $('<input id="stepNewFormSubmit" style="display:none;" type="submit"/>');
                    formContainer.append(submitButton);
                    isSubmitButtonAdded = true;
                }

                submitButton.click();
            };

            var getSkipToPageItem = function (pageIndex) {
                var item = null;
                for (var i = 0; i < skipToPageCollection.length; i++) {
                    if (skipToPageCollection[i].SkipFromPage === pageIndex) return skipToPageCollection[i];
                }

                return item;
            };

            var getSkipFromPageItem = function (pageIndex) {
                var item = null;
                for (var i = 0; i < skipToPageCollection.length; i++) {
                    if (skipToPageCollection[i].SkipToPage === pageIndex) return skipToPageCollection[i];
                }

                return item;
            };

            var focusForm = function () {
                var form = $(formElement).find("form");

                // TODO: Accessibility is not implemented no logic is required
                if (form.length <= 0) {
                    return;
                }

                form.attr("tabindex", 0);
                form.focus();
                form.removeAttr("tabindex");
            };

            var separatorsNext = formStepsContainers.find(selectors.nextButton);

            separatorsNext.click(function (e) {
                e.preventDefault();

                var currentStepContainer = $(e.target).closest(selectors.separator);
                tryGoToNextStep(currentStepContainer, function () {
                    var previousIndex = formStepIndex;
                    currentStepContainer.hide();

                    if (skipToPageCollection && skipToPageCollection.length > 0) {
                        var skipItem = getSkipToPageItem(formStepIndex);
                        if (skipItem) {
                            formStepIndex = skipItem.SkipToPage;
                        }
                        else {
                            formStepIndex++;
                        }
                    }
                    else {
                        formStepIndex++;
                    }

                    $(formStepsContainers[formStepIndex]).show();
                    formElement.trigger("form-page-changed", [formStepIndex, previousIndex]);
                    focusForm();
                });
            });

            var separatorsPrev = formStepsContainers.find(selectors.previousButton);
            separatorsPrev.click(function (e) {
                e.preventDefault();

                var previousIndex = formStepIndex;
                var stepNewForm = $('form#stepNewForm');
                var currentContainer = $(e.target).closest(selectors.separator);
                if (stepNewForm.children(selectors.separator).length > 0) {
                    currentContainer.unwrap();
                }

                currentContainer.hide();

                if (skipToPageCollection && skipToPageCollection.length > 0) {
                    var skipItem = getSkipFromPageItem(formStepIndex);
                    if (skipItem) {
                        formStepIndex = skipItem.SkipFromPage;
                    }
                    else {
                        formStepIndex--;
                    }
                }
                else {
                    formStepIndex--;
                }

                $(formStepsContainers[formStepIndex]).show();
                formElement.trigger("form-page-changed", [formStepIndex, previousIndex]);
                focusForm();
            });
        };

        formContainers.each(function (i, element) {
            initializeFormContainer(element);
        });

        // This implementation is only for the Form preview mode 
        var isPreviewMode = window.location.href.indexOf("/Preview") !== -1;
        if (formContainers.length === 0 && isPreviewMode) {
            var separator = $(selectors.separator);
            if (separator.length > 0) {
                initializeFormContainer(separator.parent());
            }
        }
    });
})(jQuery);
//paragraph-text-field.js
(function ($) {
    $(function () {

        function processRules(e) {
            if (typeof $.fn.processFormRules === 'function') {
                $(e.target).processFormRules();
            }
        }

        var delayTimer;
        function processRulesWithDelay(e) {
            clearTimeout(delayTimer);
            delayTimer = setTimeout(function () {
                processRules(e);
            }, 300);
        }

        function handleValidation(e) {
            if (typeof e.target.validity === 'undefined')
                return;

            var validationMessages = getValidationMessages(e.target);

            if (e.target.required && e.target.validity.valueMissing) {
                setErrorMessage(e.target, validationMessages.required);
            }
            else if (e.target.validity.tooShort || e.target.validity.tooLong) {
                setErrorMessage(e.target, validationMessages.maxLength);
            }
            else {
                setErrorMessage(e.target, '');
            }
        }

        function onChange(e) {
            handleValidation(e);
            processRules(e);
        }

        function onInput(e) {
            handleValidation(e);
            processRulesWithDelay(e);
        }

        function invalid(e) {
            if (typeof e.target.validity === 'undefined')
                return;

            if (_getErrorMessageContainer(e.target)) {
                e.preventDefault();
            }

            var validationMessages = getValidationMessages(e.target);

            if (e.target.validity.valueMissing) {
                setErrorMessage(e.target, validationMessages.required);
            }
            else if (e.target.validity.tooShort || e.target.validity.tooLong) {
                setErrorMessage(e.target, validationMessages.maxLength);
            }
            else if (e.target.validity.patternMismatch) {
                setErrorMessage(e.target, validationMessages.maxLength);
            }
        }

        function getValidationMessages(input) {
            var container = $(input).parents('[data-sf-role="paragraph-text-field-container"]');
            var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
            var validationMessages = JSON.parse(validationMessagesInput.val());

            return validationMessages;
        }

        function setErrorMessage(input, message) {
            var errorMessagesContainer = _getErrorMessageContainer(input);

            if (errorMessagesContainer) {
                _toggleCustomErrorMessage(errorMessagesContainer, message);
            } else {
                input.setCustomValidity(message);
            }
        }

        function _toggleCustomErrorMessage(container, message) {
            container.innerText = message;

            if (message === '') {
                container.style.display = 'none';
            } else {
                container.style.display = 'block';
            }
        }

        function _getErrorMessageContainer(input) {
            var container = $(input).closest('[data-sf-role="paragraph-text-field-container"]')[0];
            if (container) {
                var errorMessagesContainer = container.querySelector('[data-sf-role="error-message"]');
                return errorMessagesContainer;
            }

            return null;
        }

        function init() {
            var containers = $('[data-sf-role="paragraph-text-field-container"]');

            if (!containers || containers.length < 1)
                return;

            for (var i = 0; i < containers.length; i++) {
                var textarea = $(containers[i]).find('[data-sf-role="paragraph-text-field-textarea"]');

                if (textarea) {
                    textarea.on('change', onChange);
                    textarea.on('input', onInput);
                    textarea.on('invalid', invalid);
                }
            }
        }

        init();
    });
}(jQuery));
//submit-button.js
(function ($) {
    $(function () {
        $('button[type="submit"]').each(function () {
            $(this).closest('form').submit(function (ev) {
                if (!ev.isDefaultPrevented()) {
                    var button = $(this).find('button[type="submit"]');

                    // If the submit button is not visible and there is a page break button next that is visible 
                    // we asume that we are in case with multi step form, where the enter button is hit and the form is submited, 
                    // so that we need to click next instead of submitting the form.
                    var isSubmitButtonVisible = $(button).is(":visible");
                    var firstPageBreak = $(ev.target).find('[data-sf-btn-role="next"]:visible')[0];
                    if (!isSubmitButtonVisible && firstPageBreak) {
                        firstPageBreak.click();
                        ev.preventDefault();

                        return;
                    }

                    button.prop('disabled', true);

                    //we need first to validate all inputs which require server side validation (cannot be validated client-side)
                    //before the form is submitted and submit the form only if all are valid - e.g. captcha is such control
                    var allInputs = ev.target.querySelectorAll('input, textarea', 'select');
                    var widgetValidators = [];

                    for (var i = 0; i < allInputs.length; i++) {
                        if ($(allInputs[i]).data('widget-validator')) {
                            widgetValidators.push($(allInputs[i]).data('widget-validator'));
                        }
                    }

                    if (widgetValidators.length) {
                        ev.preventDefault();
                        var deferreds = [];
                        for (i = 0; i < widgetValidators.length; i++) {
                            deferreds.push(widgetValidators[i]());
                        }

                        $.when.apply($, deferreds).done(function () {
                            for (var i = 0; i < widgetValidators.length; i++) {
                                if (!arguments[i]) {
                                    return false;
                                }
                            }

                            ev.target.submit();
                        }).always(function () {
                            button.prop('disabled', false);
                        });
                    }
                }
            });
        });
    });
}(jQuery));
//text-field.js
(function ($) {
    $(function () {

        function processRules(e) {
            if (typeof $.fn.processFormRules === 'function') {
                $(e.target).processFormRules();
            }
        }

        var delayTimer;
        function processRulesWithDelay(e) {
            clearTimeout(delayTimer);
            delayTimer = setTimeout(function () {
                processRules(e);
            }, 300);
        }

        function handleValidation(e) {
            if (typeof e.target.validity === 'undefined')
                return;

            if (e.target.required && e.target.validity.valueMissing) {
                var validationMessages = getValidationMessages(e.target);
                setErrorMessage(e.target, validationMessages.required);
            } else {
                setErrorMessage(e.target, '');
            }
        }

        function onChange(e) {
            handleValidation(e);
            processRules(e);
        }

        function onInput(e) {
            handleValidation(e);
            processRulesWithDelay(e);
        }

        function invalid(e) {
            if (typeof e.target.validity === 'undefined')
                return;

            if (_getErrorMessageContainer(e.target)) {
                e.preventDefault();
            }

            var validationMessages = getValidationMessages(e.target);
            var validationRestrictions = getValidationRestrictions(e.target);
            var isValidLength = e.target.value.length >= validationRestrictions.minLength;

            if (validationRestrictions.maxLength > 0)
                isValidLength &= e.target.value.length <= validationRestrictions.maxLength;

            if (e.target.validity.valueMissing) {
                setErrorMessage(e.target, validationMessages.required);
            }
            else if (e.target.validity.patternMismatch && !isValidLength) {
                setErrorMessage(e.target, validationMessages.maxLength);
            }
            else if (e.target.validity.patternMismatch && isValidLength) {
                setErrorMessage(e.target, validationMessages.regularExpression);
            }
            else if (!e.target.validity.valid) {
                setErrorMessage(e.target, validationMessages.invalid);
            }
        }

        function getValidationMessages(input) {
            var container = $(input).parents('[data-sf-role="text-field-container"]');
            var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
            var validationMessages = JSON.parse(validationMessagesInput.val());

            return validationMessages;
        }

        function getValidationRestrictions(input) {
            var container = $(input).parents('[data-sf-role="text-field-container"]');
            var validationRestrictionsInput = $(container).find('[data-sf-role="violation-restrictions"]');
            var validationRestrictions = JSON.parse(validationRestrictionsInput.val());

            return validationRestrictions;
        }

        function setErrorMessage(input, message) {
            var errorMessagesContainer = _getErrorMessageContainer(input);

            if (errorMessagesContainer) {
                _toggleCustomErrorMessage(errorMessagesContainer, message);
            } else {
                input.setCustomValidity(message);
            }
        }

        function _toggleCustomErrorMessage(container, message) {
            container.innerText = message;

            if (message === '') {
                container.style.display = 'none';
            } else {
                container.style.display = 'block';
            }
        }

        function _getErrorMessageContainer(input) {
            var container = $(input).closest('[data-sf-role="text-field-container"]')[0];
            if (container) {
                var errorMessagesContainer = container.querySelector('[data-sf-role="error-message"]');
                return errorMessagesContainer;
            }

            return null;
        }

        function init() {
            var containers = $('[data-sf-role="text-field-container"]');

            if (!containers || containers.length < 1)
                return;

            for (var i = 0; i < containers.length; i++) {
                var input = $(containers[i]).find('[data-sf-role="text-field-input"]');

                if (input) {
                    input.on('change', onChange);
                    input.on('input', onInput);
                    input.on('invalid', invalid);
                }
            }
        }

        init();
    });
}(jQuery));
