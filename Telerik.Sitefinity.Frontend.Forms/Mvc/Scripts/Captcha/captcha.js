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
                options.data = data;
            }

            return $.ajax(options);
        },

        getCaptcha: function () {
            var getCaptchaUrl = this.rootUrl + 'captcha';
            return this.makeAjax(getCaptchaUrl);
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
        captchaInput: function () { return this.getOrInitializeProperty('_captchaInput', 'captcha-input'); },
        captchaRefreshLink: function () { return this.getOrInitializeProperty('_captchaRefreshLink', 'captcha-refresh-button'); },
        captchaDataIv: function () { return this.getOrInitializeProperty('_captchaDataIv', 'captcha-iv'); },
        captchaDataCorrectAnswer: function () { return this.getOrInitializeProperty('_captchaDataCorrectAnswer', 'captcha-ca'); },
        captchaDataKey: function () { return this.getOrInitializeProperty('_captchaDataKey', 'captcha-k'); },
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
                    self.captchaDataIv().val(data.InitializationVector);
                    self.captchaDataCorrectAnswer().val(data.CorrectAnswer);
                    self.captchaDataKey().val(data.Key);
                    self.captchaInput().val("");
                    self.captchaInput().show();
                    self.wrapper.show();
                }

                deferred.resolve(true);
            });

            return deferred;
        },

        /*
            Initialization
        */
        initializeProperties: function () {
            this.restApi = new CaptchaRestApi(this.settings.rootUrl);

            this.captchaData = {
                iv: null,
                correctAnswer: null,
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

        initialize: function () {
            this.initializeProperties();
            this.initializeCaptcha();
            this.initializeHandlers();
        }
    };

    /*
        Widget creation
    */
    $(function () {
        $('[data-sf-role="field-captcha-container"]').each(function () {
            var container = $(this);
            var rootUrl = container.find('[data-sf-role="captcha-settings"]').val();
            (new CaptchaWidget(container, { rootUrl: rootUrl })).initialize();
        });
    });
}(jQuery));