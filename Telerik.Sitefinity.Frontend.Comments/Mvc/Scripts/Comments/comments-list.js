﻿; (function ($) {
    'use strict';

    var makeAjax = function (url, type, data) {
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
    };

    /*
        Rest Api
    */
    var CommentsRestApi = function (rootUrl) {
        if (rootUrl && rootUrl[rootUrl.length - 1] !== '/') {
            rootUrl += '/';
        }

        this.rootUrl = rootUrl;
    };

    CommentsRestApi.prototype = {
        getCommentsCount: function myfunction(threadKey, status) {
            var getCommentsCountUrl = this.rootUrl + 'comments/count?ThreadKey=' + threadKey;
            if (status) {
                getCommentsCountUrl += '&Status=' + status;
            }

            return makeAjax(getCommentsCountUrl).then(function (response) {
                var count = 0;

                if (response && response.Items) {
                    for (var i = 0; i < response.Items.length; i++) {
                        if (response.Items[i].Key === threadKey) {
                            count = response.Items[i].Count;
                            break;
                        }
                    }
                }

                return count;
            });
        },

        getComments: function (threadKey, skip, take, sortDescending, newerThan, language) {
            var getCommentsUrl = this.rootUrl + 'comments/?ThreadKey=' + threadKey;

            if (take && take > 0) {
                getCommentsUrl += '&Take=' + take;
            }
            if (skip && skip > 0) {
                getCommentsUrl += '&Skip=' + skip;
            }
            if (sortDescending === true) {
                getCommentsUrl += '&SortDescending=True';
            }
            if (language) {
                getCommentsUrl += '&Language=' + language;
            }
            if (newerThan) {
                getCommentsUrl += '&NewerThan=' + newerThan;
            }

            return makeAjax(getCommentsUrl);
        },

        createComment: function (comment) {
            var createCommentUrl = this.rootUrl + 'comments';
            return makeAjax(createCommentUrl, 'POST', JSON.stringify(comment));
        },

        getSubscriptionStatus: function (threadKey) {
            var subscriptionStatusUrl = this.rootUrl + 'notifications/?threadKey=' + threadKey;
            return makeAjax(subscriptionStatusUrl);
        },

        toggleSubscription: function (threadKey, unsubscribe) {
            var toggleSubscriptionUrl = this.rootUrl + 'notifications/';
            if (unsubscribe) {
                toggleSubscriptionUrl += 'unsubscribe/';
            }
            else {
                toggleSubscriptionUrl += 'subscribe/';
            }

            toggleSubscriptionUrl += '?threadKey=' + threadKey;

            return makeAjax(toggleSubscriptionUrl, 'POST');
        },

        getCaptcha: function () {
            var getCaptchaUrl = this.rootUrl + 'captcha';
            return makeAjax(getCaptchaUrl);
        }
    };

    /*
        Widget
    */
    var CommentsListWidget = function (wrapper, settings, resources) {
        this.settings = settings || {};
        this.resources = resources || {};
        this.wrapper = wrapper;

        this.commentsTakenSoFar = 0;
        this.maxCommentsToShow = 0;
        this.allCommentsCount = 0;

        this.lastCommentDate = 0;

        this.commentsSortedDescending = settings.commentsInitiallySortedDescending;
    };

    CommentsListWidget.prototype = {
        /*
            Properties
        */
        isUserAuthenticated: false,
        isSelectedSortButtonCssClass: 'is-selected',

        getOrInitializeProperty: function (property, sfRole) {
            if (!this[property]) {
                this[property] = this.getElementByDataSfRole(sfRole);
            }

            return this[property];
        },

        /*
            Elements
        */
        getElementByDataSfRole: function (sfRole) {
            return this.wrapper.find('[data-sf-role="' + sfRole + '"]');
        },

        getSingleCommentTemplate: function () {
            if (!this.singleCommentTemplate) {
                if (this.settings.singleCommentTemplate) {
                    this.singleCommentTemplate = this.settings.singleCommentTemplate;
                }
                else {
                    this.singleCommentTemplate = $(this.getElementByDataSfRole('single-comment-template').html()).first();
                }
            }

            return this.singleCommentTemplate;
        },

        commentsContainer: function () { return this.getOrInitializeProperty('_commentsContainer', 'comments-container'); },
        commentsTotalCount: function () { return this.getOrInitializeProperty('_commentsTotalCount', 'comments-total-count'); },
        commentsHeader: function () { return this.getOrInitializeProperty('_commentsHeader', 'comments-header'); },
        commentsLoadMoreButton: function () { return this.getOrInitializeProperty('_commentsLoadMoreButton', 'comments-load-more-button'); },

        commentsNewLoggedOutView: function () { return this.getOrInitializeProperty('_commentsNewLoggedOutView', 'comments-new-logged-out-view'); },
        newCommentForm: function () { return this.getOrInitializeProperty('_newCommentForm', 'comments-new-form'); },
        newCommentPendingApprovalMessage: function () { return this.getOrInitializeProperty('_newCommentPendingApprovalMessage', 'comments-new-pending-approval-message'); },
        newCommentFormButton: function () { return this.getOrInitializeProperty('_newCommentFormButton', 'comments-new-form-button'); },
        newCommentSubmitButton: function () { return this.getOrInitializeProperty('_newCommentSubmitButton', 'comments-new-submit-button'); },
        newCommentMessage: function () { return this.getOrInitializeProperty('_newCommentMessage', 'comments-new-message'); },
        newCommentMessageError: function () { return this.getOrInitializeProperty('_newCommentMessageError', 'comments-new-message-error'); },
        newCommentName: function () { return this.getOrInitializeProperty('_newCommentName', 'comments-new-name'); },
        newCommentNameError: function () { return this.getOrInitializeProperty('_newCommentNameError', 'comments-new-name-error'); },
        newCommentEmail: function () { return this.getOrInitializeProperty('_newCommentEmail', 'comments-new-email'); },
        newCommentRequiresAuthentication: function () { return this.getOrInitializeProperty('_newCommentRequiresAuthentication', 'comments-new-requires-authentication'); },

        commentsSortNewButton: function () { return this.getOrInitializeProperty('_commentsSortNewButton', 'comments-sort-new-button'); },
        commentsSortOldButton: function () { return this.getOrInitializeProperty('_commentsSortOldButton', 'comments-sort-old-button'); },

        captchaContainer: function () { return this.getOrInitializeProperty('_captchaContainer', 'captcha-container'); },
        captchaImage: function () { return this.getOrInitializeProperty('_captchaImage', 'captcha-image'); },
        captchaInput: function () { return this.getOrInitializeProperty('_captchaInput', 'captcha-input'); },
        captchaRefreshLink: function () { return this.getOrInitializeProperty('_captchaRefreshLink', 'captcha-refresh-button'); },
        errorMessage: function () { return this.getOrInitializeProperty('_errorMessage', 'error-message'); },

        listLoadingIndicator: function () { return this.getOrInitializeProperty('_listLoadingIndicator', 'list-loading-indicator'); },
        submitLoadingIndicator: function () { return this.getOrInitializeProperty('_submitLoadingIndicator', 'submit-loading-indicator'); },

        commentsSubscribeButton: function () { return this.getOrInitializeProperty('_commentsSubscribeButton', 'comments-subscribe-button'); },
        commentsSubscribeText: function () { return this.getOrInitializeProperty('_commentsSubscribeText', 'comments-subscribe-text'); },

        /*
            Widget methods
        */
        getSfStringFromDate: function (date) {
            return '/Date(' + date.getTime() + ')/';
        },

        getDateFromSfString: function (sfDateString) {
            return new Date(parseInt(sfDateString.replace(/\D/g, ''), 10));
        },

        getDateString: function (sfDateString, secondsOffset) {
            var date = this.getDateFromSfString(sfDateString);
            date.setMinutes(date.getMinutes() + date.getTimezoneOffset());
            date.setSeconds(date.getSeconds() + secondsOffset);

            return date.toUTCString();
        },

        hideErrors: function () {
            this.newCommentMessageError().hide();
            this.newCommentNameError().hide();
            this.errorMessage().hide();
        },

        validateComment: function (comment) {
            var deferred = $.Deferred();
            var isValid = true;

            if (comment.Message.length < 1) {
                isValid = false;
                this.newCommentMessageError().show();
            }

            if (!this.isUserAuthenticated && comment.Name.length < 1) {
                isValid = false;
                this.newCommentNameError().show();
            }

            deferred.resolve(isValid);

            return deferred.promise();
        },

        attachCommentMessage: function (element, message) {
            if (element && message) {
                if (message.length < this.settings.commentsTextMaxLength) {
                    element.text(message);
                }
                else {
                    element.text(message.substr(0, this.settings.commentsTextMaxLength));
                    element.append($('<span />').hide().text(message.substr(this.settings.commentsTextMaxLength)));
                    element.append($('<button data-sf-role="comments-read-full-comment-button" />').text(this.resources.readFullComment));
                }
            }
        },

        createCommentMarkup: function (comment) {
            var newComment = this.getSingleCommentTemplate().clone(true);

            newComment.find('[data-sf-role="comment-avatar"]').attr('src', comment.ProfilePictureThumbnailUrl).attr('alt', comment.Name);

            newComment.find('[data-sf-role="comment-name"]').text(comment.Name);
            newComment.find('[data-sf-role="comment-date"]').text(this.getDateFromSfString(comment.DateCreated).format(this.settings.commentDateTimeFormatString));

            this.attachCommentMessage(newComment.find('[data-sf-role="comment-message"]'), comment.Message);

            return newComment;
        },

        renderComments: function (comments, container, doPrepend) {
            if (comments && comments.length) {
                var self = this;

                comments.forEach(function (comment) {
                    var newComment = self.createCommentMarkup(comment);

                    if (doPrepend) {
                        container.prepend(newComment);

                        if (self.commentsTakenSoFar > self.maxCommentsToShow) {
                            container.children().slice((self.commentsTakenSoFar - self.maxCommentsToShow) * (-1)).remove();
                            self.commentsTakenSoFar = self.maxCommentsToShow;
                        }
                    }
                    else {
                        container.append(newComment);
                    }
                });
            }
        },

        renderCommentsCount: function () {
            // Comments count header
            this.commentsHeader().text(this.allCommentsCount > 0 ? (this.allCommentsCount > 1 ? this.resources.commentsPlural : this.resources.commentSingular) : this.newCommentFormButton().text());
            this.commentsTotalCount().toggle(this.allCommentsCount > 0).text(this.allCommentsCount);
            this.newCommentFormButton().toggle(this.allCommentsCount > 0);

            // Comments sort buttons
            this.commentsSortNewButton().toggle(this.allCommentsCount > 1);
            this.commentsSortOldButton().toggle(this.allCommentsCount > 1);

            // Comments load more button
            if (this.settings.enablePaging) {
                this.commentsLoadMoreButton().toggle(this.allCommentsCount > Math.max(this.commentsTakenSoFar, this.settings.commentsPerPage));
            }
        },

        loadComments: function (skip, take, newerThan) {
            var self = this;
            if (self.isLoadinglist)
                return;

            self.isLoadingList = true;
            self.listLoadingIndicator().show();

            return self.commentsRestApi.getComments(self.settings.commentsThreadKey, skip, take, self.commentsSortedDescending, newerThan).then(function (response) {
                if (response && response.Items && response.Items.length) {
                    self.commentsTakenSoFar += response.Items.length;
                    self.renderCommentsCount();

                    // Prepend the recieved comments only if current sorting is descending and the comments are being refreshed
                    self.renderComments(response.Items, self.commentsContainer(), newerThan && self.commentsSortedDescending);
                }

                return response;
            }).always(function () {
                self.listLoadingIndicator().hide();
                self.isLoadingList = false;
            });
        },

        refreshLastCommentDate: function (response) {
            if (response && response.Items && response.Items.length) {
                var itemToTake = this.commentsSortedDescending ? response.Items[0] : response.Items[response.Items.length - 1];
                this.lastCommentDate = this.getDateString(itemToTake.DateCreated, 1);
            }
        },

        refreshAllCommentsCount: function (response) {
            if (response && response.Items && response.Items.length) {
                this.allCommentsCount += response.Items.length;
                this.renderCommentsCount();
            }
        },

        setAllCommentsCount: function (count) {
            this.allCommentsCount = count;
            this.renderCommentsCount();
        },

        refreshComments: function (self, isNewCommentPosted) {
            var commentsToTake = self.commentsSortedDescending ? self.settings.commentsPerPage : self.maxCommentsToShow - self.commentsTakenSoFar;

            // New comment is created, but won't be retrievet via refresh - update comment count.
            if (!self.commentsSortedDescending && commentsToTake <= 0 && isNewCommentPosted) {
                self.allCommentsCount++;
                self.renderCommentsCount();
            }
            else {
                self.loadComments(0, commentsToTake, self.lastCommentDate).then(function (response) {
                    self.refreshLastCommentDate(response);
                    self.refreshAllCommentsCount(response);
                });
            }
        },

        sortComments: function (useDescending) {
            var self = this;

            if (self.commentsSortedDescending !== useDescending) {
                self.commentsSortedDescending = useDescending;
                self.commentsContainer().html('');
                self.commentsTakenSoFar = 0;
                self.loadComments(0, self.settings.commentsPerPage).then(function () {
                    self.renderCommentsCount();
                });
            }
        },

        buildNewCommentFromForm: function () {
            var self = this;

            var comment = {
                Message: self.newCommentMessage().val(),
                ThreadKey: self.settings.commentsThreadKey
            };

            if (!self.isUserAuthenticated) {
                comment.Name = self.newCommentName().val();
                comment.Email = self.newCommentEmail().val();

                if (self.settings.requiresCaptcha) {
                    comment.Captcha = {
                        Answer: self.captchaInput().val(),
                        CorrectAnswer: self.captchaData.correctAnswer,
                        InitializationVector: self.captchaData.iv,
                        Key: self.captchaData.key
                    };
                }
            }

            comment.Thread = self.settings.commentsThread || {};
            comment.Thread.Group = comment.Thread.Group || {};

            comment.Thread.Group.Key = comment.Thread.Group.Key || comment.Thread.groupKey;

            return comment;
        },

        submitNewComment: function () {
            var self = this;

            self.submitLoadingIndicator().show();
            self.newCommentSubmitButton().hide();

            var comment = self.buildNewCommentFromForm();

            self.validateComment(comment).then(function (isValid) {
                var endSubmiting = function () {
                    self.captchaRefresh();
                    self.submitLoadingIndicator().hide();
                    self.newCommentSubmitButton().show();
                };

                if (isValid) {
                    self.commentsRestApi.createComment(comment).then(function (response) {
                        // Clean form
                        self.newCommentMessage().val('');
                        self.newCommentName().val('');
                        self.newCommentEmail().val('');

                        if (self.settings.requiresApproval) {
                            self.newCommentPendingApprovalMessage().show();
                        }
                        else if (!self.settings.commentsAutoRefresh) {
                            self.refreshComments(self, true);
                        }
                    }, function (jqXHR, textStatus, errorThrown) {
                        if (jqXHR.responseText) {
                            var errorTxt = JSON.parse(jqXHR.responseText).ResponseStatus.Message;
                            self.errorMessage().html(errorTxt);
                            self.errorMessage().show();
                        }
                    }).always(endSubmiting);
                }
                else {
                    endSubmiting();
                }
            });
        },

        captchaRefresh: function () {
            var self = this;
            var deferred = $.Deferred();

            self.captchaImage().attr("src", "");
            self.captchaInput().hide();

            self.commentsRestApi.getCaptcha().then(function (data) {
                if (data) {
                    self.captchaImage().attr("src", "data:image/png;base64," + data.Image);
                    self.captchaData.iv = data.InitializationVector;
                    self.captchaData.correctAnswer = data.CorrectAnswer;
                    self.captchaData.key = data.Key;
                    self.captchaInput().val("");
                    self.captchaInput().show();
                }

                deferred.resolve(true);
            });
        },

        setupCaptcha: function () {
            if (!this.isUserAuthenticated && this.settings.requiresCaptcha) {
                this.captchaData = {
                    iv: null,
                    correctAnswer: null,
                    key: null
                };

                this.captchaRefresh();
                this.captchaContainer().show();
            }
        },

        toggleSubscription: function () {
            var self = this;

            self.commentsRestApi.toggleSubscription(self.settings.commentsThreadKey, self.isSubscribedToNewComments).then(function (response) {
                self.isSubscribedToNewComments = !self.isSubscribedToNewComments;

                self.commentsSubscribeButton().text(self.isSubscribedToNewComments ? self.resources.unsubscribeLink : self.resources.subscribeLink);
                self.commentsSubscribeText().text(self.isSubscribedToNewComments ? self.resources.successfullySubscribedToNewComments : self.resources.successfullyUnsubscribedFromNewComments);
            });
        },

        /*
            Widget initialization
        */
        initializeProperties: function () {
            this.commentsRestApi = new CommentsRestApi(this.settings.rootUrl);

            this.isLoadingList = false;
            this.isSubscribedToNewComments = false;

            if (!this.settings.enablePaging) {
                this.settings.commentsPerPage = 500;
            }

            this.maxCommentsToShow = this.settings.commentsPerPage;

            // Initially hide the "RequiresAuthentication" message.
            this.newCommentRequiresAuthentication().hide();

            if (this.commentsSortedDescending) {
                this.commentsSortNewButton().addClass(this.isSelectedSortButtonCssClass);
            }
            else {
                this.commentsSortOldButton().addClass(this.isSelectedSortButtonCssClass);
            }
        },

        initializeUserStatus: function () {
            var self = this;

            var isUserAuthenticatedUrl = self.settings.isUserAuthenticatedUrl;
            if (isUserAuthenticatedUrl[isUserAuthenticatedUrl.length - 1] !== '/') {
                isUserAuthenticatedUrl += '/';
            }
            isUserAuthenticatedUrl += '?_=' + (Math.random().toString().substr(2) + (new Date()).getTime());
            makeAjax(isUserAuthenticatedUrl).then(function (response) {
                if (response) {
                    if (response.IsAuthenticated) {
                        self.isUserAuthenticated = true;
                        self.commentsNewLoggedOutView().hide();

                        // Get Subscribtion status only if user is logged in.
                        self.initializeSubscription();
                    }
                    else if (self.settings.requiresAuthentication) {
                        self.newCommentForm().hide();
                        self.newCommentRequiresAuthentication().show();
                    }
                }

                $.proxy(self.setupCaptcha(), self);
            });
        },

        initializeComments: function () {
            var self = this;

            if (!self.settings.enablePaging) {
                self.commentsLoadMoreButton().hide();
            }

            // Initial loading of comments count for thread
            self.commentsRestApi.getCommentsCount(self.settings.commentsThreadKey).then(function (response) {
                self.setAllCommentsCount(response);
            });

            // Initial loading of comments
            self.loadComments(0, self.settings.commentsPerPage).then(function (response) {
                self.refreshLastCommentDate(response);
            });

            // Comments Refresh
            if (self.settings.commentsAutoRefresh) {
                setInterval(function () {
                    self.refreshComments(self);
                }, self.settings.commentsRefreshInterval);
            }
        },

        initializeSubscription: function () {
            var self = this;

            self.commentsRestApi.getSubscriptionStatus(self.settings.commentsThreadKey).then(function (response) {
                if (response) {
                    self.isSubscribedToNewComments = response.IsSubscribed;

                    self.commentsSubscribeButton().text(self.isSubscribedToNewComments ? self.resources.unsubscribeLink : self.resources.subscribeToNewComments);
                    self.commentsSubscribeText().text(self.isSubscribedToNewComments ? self.resources.youAreSubscribedToNewComments : '');

                    self.commentsSubscribeButton().click(function () {
                        self.toggleSubscription();
                        return false;
                    });
                }
            });
        },

        initializeHandlers: function () {
            var self = this;

            if (self.settings.enablePaging) {
                self.commentsLoadMoreButton().click(function () {
                    self.maxCommentsToShow += self.settings.commentsPerPage;
                    self.loadComments(self.commentsTakenSoFar, self.settings.commentsPerPage);
                    return false;
                });
            }

            self.commentsContainer().on('click', '[data-sf-role="comments-read-full-comment-button"]', function (e) {
                if (e && e.target) {
                    $(e.target).hide().siblings().show();
                    return false;
                }
            });

            self.commentsSortNewButton().click(function () {
                self.sortComments(true);
                self.commentsSortNewButton().addClass(self.isSelectedSortButtonCssClass);
                self.commentsSortOldButton().removeClass(self.isSelectedSortButtonCssClass);
                return false;
            });

            self.commentsSortOldButton().click(function () {
                self.sortComments(false);
                self.commentsSortOldButton().addClass(self.isSelectedSortButtonCssClass);
                self.commentsSortNewButton().removeClass(self.isSelectedSortButtonCssClass);
                return false;
            });

            self.newCommentFormButton().click(function () {
                $('html, body').animate({ scrollTop: self.newCommentForm().offset().top }, 1000);

                return false;
            });

            self.newCommentSubmitButton().click(function () {
                if (!self.settings.isDesignMode) {
                    self.hideErrors();
                    self.submitNewComment();
                }

                return false;
            });

            self.captchaRefreshLink().click(function () {
                self.captchaRefresh();
                return false;
            });
        },

        initialize: function () {
            this.initializeProperties();
            this.initializeUserStatus();
            this.initializeComments();
            this.initializeHandlers();
        }
    };

    /*
        Widgets creation
    */
    $(function () {
        $('[data-sf-role="comments-wrapper"]').each(function () {
            var element = $(this);
            var settings = JSON.parse(element.find('[data-sf-role="comments-settings"]').val());
            var resources = JSON.parse(element.find('[data-sf-role="comments-resources"]').val());
            (new CommentsListWidget(element, settings, resources)).initialize();
        });
    });
}(jQuery));
