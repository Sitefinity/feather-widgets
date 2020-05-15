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
                    self.captchaAudioBtn().click(function () { self.captchaAudio()[0].play();});
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