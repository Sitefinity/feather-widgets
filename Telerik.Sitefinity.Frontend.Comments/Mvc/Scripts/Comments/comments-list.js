; (function ($) {
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

            return makeAjax(getCommentsCountUrl);
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
            return makeAjax(this.rootUrl + 'comments', 'POST', JSON.stringify(comment));
        },

        getCaptcha: function () {

        }
    };

    /*
        Widget
    */
    var CommentsListWidget = function (wrapper, settings) {
        this.settings = settings || {};
        this.wrapper = wrapper;

        this.commentsSortedDescending = true;
        this.commentsRefreshRate = 3000;
        this.commentsTakenSoFar = 0;
        this.firstCommentDate = 0;
        this.lastCommentDate = 0;
        this.maxCommentsToShow = 0;
    };

    CommentsListWidget.prototype = {
        /*
            Properties
        */
        isUserAuthenticated: false,

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
        newCommentForm: function () { return this.getOrInitializeProperty('_newCommentForm', 'comments-new-form'); },
        newCommentFormButton: function () { return this.getOrInitializeProperty('_newCommentFormButton', 'comments-new-form-button'); },
        newCommentSubmitButton: function () { return this.getOrInitializeProperty('_newCommentSubmitButton', 'comments-new-submit-button'); },
        newCommentSubscribeView: function () { return this.getOrInitializeProperty('_newCommentSubscribeView', 'comments-new-subscribe-view'); },
        newCommentSubscribeCheckbox: function () { return this.getOrInitializeProperty('_newCommentSubscribeCheckbox', 'comments-new-subscribe-checkbox'); },
        newCommentMessage: function () { return this.getOrInitializeProperty('_newCommentMessage', 'comments-new-message'); },
        newCommentName: function () { return this.getOrInitializeProperty('_newCommentName', 'comments-new-name'); },
        newCommentEmail: function () { return this.getOrInitializeProperty('_newCommentEmail', 'comments-new-email'); },
        newCommentWebsite: function () { return this.getOrInitializeProperty('_newCommentWebsite', 'comments-new-website'); },
        commentsNewLoggedOutView: function () { return this.getOrInitializeProperty('_commentsNewLoggedOutView', 'comments-new-logged-out-view'); },
        commentsSortNewButton: function () { return this.getOrInitializeProperty('_commentsSortNewButton', 'comments-sort-new-button'); },
        commentsSortOldButton: function () { return this.getOrInitializeProperty('_commentsSortOldButton', 'comments-sort-old-button'); },

        /*
            Helpers
        */
        getDateString: function (sfDateString, secondsOffset) {
            var date = new Date(parseInt(sfDateString.replace(/\D/g, ''), 10));
            date.setMinutes(date.getMinutes() + date.getTimezoneOffset());
            date.setSeconds(date.getSeconds() + secondsOffset);

            return date.toUTCString();
        },

        /*
            Comments operations
        */
        validateComment: function (comment) {
            var deferred = $.Deferred();

            if (this.isUserAuthenticated) {
                deferred.resolve(comment.Message.length > 0);
            }
            else {
                this.commentsRestApi.getCaptia().then(function (captia) {
                    //TODO: captcha logic

                    deferred.resolve(true);
                });
            }

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
                    element.append($('<button data-sf-role="comments-read-full-comment-button" />').text(this.settings.commentsReadFullCommentText));
                }
            }
        },

        renderComments: function (comments, doPrepend) {
            if (comments && comments.length) {
                var self = this;

                comments.forEach(function (comment) {
                    var newComment = self.getSingleCommentTemplate().clone(true);

                    newComment.find('[data-sf-role="comment-avatar"]').attr('src', comment.ProfilePictureThumbnailUrl).attr('alt', comment.Name);

                    newComment.find('[data-sf-role="comment-name"]').text(comment.Name);
                    newComment.find('[data-sf-role="comment-date"]').text(comment.Date);

                    self.attachCommentMessage(newComment.find('[data-sf-role="comment-message"]'), comment.Message);

                    if (doPrepend) {
                        self.commentsContainer().prepend(newComment);
                    }
                    else {
                        self.commentsContainer().append(newComment);
                    }
                });
            }
        },

        loadComments: function (skip, take, newerThan) {
            var self = this;

            self.commentsRestApi.getComments(self.settings.commentsThreadKey, skip, take, self.commentsSortedDescending, newerThan).then(function (response) {
                if (response && response.Items && response.Items.length) {
                    self.commentsTakenSoFar += response.Items.length;

                    self.firstCommentDate = self.getDateString(response.Items[0].DateCreated, 1);
                    self.lastCommentDate = self.getDateString(response.Items[response.Items.length - 1].DateCreated, 1);

                    // Prepend the recieved comments only if current sorting is descending and the comments are being refreshed
                    self.renderComments(response.Items, newerThan && self.commentsSortedDescending);

                    // Refresh total count if items are recieved
                    if (newerThan) {
                        self.commentsTotalCount().text(parseInt(self.commentsTotalCount().text()) + response.Items.length);
                    }
                }
            });
        },

        refreshComments: function (self) {
            if (self.commentsSortedDescending) {
                self.loadComments(0, self.settings.commentsPerPage, self.firstCommentDate);
            }
            else if (self.maxCommentsToShow - self.commentsTakenSoFar > 0) {
                self.loadComments(0, self.maxCommentsToShow - self.commentsTakenSoFar, self.lastCommentDate);
            }
        },

        sortComments: function (useDescending) {
            if (this.commentsSortedDescending !== useDescending) {
                this.commentsSortedDescending = useDescending;
                this.commentsContainer().html('');
                this.loadComments(0, this.settings.commentsPerPage);
                this.commentsTakenSoFar = 0;
            }
        },

        submitNewComment: function () {
            var self = this;

            var comment = {
                Message: self.newCommentMessage().val(),
                ThreadKey: self.settings.commentsThreadKey
            };

            if (!self.isUserAuthenticated) {
                comment.Name = self.newCommentName().val();
                comment.Email = self.newCommentEmail().val();
                comment.Website = self.newCommentWebsite().val();
            }

            self.validateComment(comment).then(function (isValid) {
                if (isValid) {
                    self.commentsRestApi.createComment(comment).then(function (response) {
                        self.newCommentMessage().val('');
                        self.newCommentForm().hide();
                        self.newCommentFormButton().show();

                        // Comments refresh will handle the showing of the new comment.

                        // Success message ?
                    });
                }
                else {
                    // Error message ?
                }
            });
        },

        /*
            Widget initialization
        */
        initializeProperties: function () {
            this.commentsRestApi = new CommentsRestApi(this.settings.rootUrl);

            this.maxCommentsToShow = this.settings.commentsPerPage;

            // Initially hide new comment form
            this.newCommentForm().hide();

            // Hide the subscribe option if not enabled
            if (!this.settings.commentsAllowSubscription) {
                this.newCommentSubscribeView().hide();
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
                if (response && response.IsAuthenticated) {
                    self.isUserAuthenticated = true;
                    self.commentsNewLoggedOutView().hide();
                }
            });
        },

        initializeComments: function () {
            var self = this;

            // Initial loading of comments count for thread
            self.commentsRestApi.getCommentsCount(self.settings.commentsThreadKey).then(function (response) {
                if (response && response.Items) {
                    var currentThreadKeyCount = 0;

                    for (var i = 0; i < response.Items.length; i++) {
                        if (response.Items[i].Key === self.settings.commentsThreadKey) {
                            currentThreadKeyCount = response.Items[i].Count;
                            break;
                        }
                    }

                    if (currentThreadKeyCount > 0) {
                        self.commentsTotalCount().text(currentThreadKeyCount);
                        self.commentsHeader().text(self.settings.commentsHeaderText);
                    }
                    else {
                        self.commentsTotalCount().hide();
                        self.newCommentFormButton().hide();
                        self.commentsHeader().text(self.newCommentFormButton().text());
                        self.newCommentForm().show();
                    }

                    if (currentThreadKeyCount <= Math.max(self.commentsTakenSoFar, self.settings.commentsPerPage)) {
                        self.commentsLoadMoreButton().hide();
                    }
                }
            });

            // Initial loading of comments
            self.loadComments(0, self.settings.commentsPerPage);

            // Comments Refresh
            setInterval(function () {
                self.refreshComments(self);
            }, self.commentsRefreshRate);
        },

        initializeSubscribtion: function () {
            var self = this;

            // debugger;
            var copy = {
                init: function () {
                    url = this.get_checkSubscriptionStatus();
                    url = url + '?threadKey=' + escape(this.get_subscriptionItemKey());

                    jQuery.ajax({
                        type: "GET",
                        url: url,
                        cache: false,
                        success: jQuery.proxy(this.onCheckSubscriptionStatusSuccess, this),
                        dataType: "json"
                    });
                },

                onCheckSubscriptionStatusSuccess: function (data) {
                    if (data.IsSubscribed === true)
                        jQuery(this.get_unsubscribeWrp()).show();
                    else
                        jQuery(this.get_subscribeWrp()).show();
                },

                _subscribeLinkHandler: function (sender) {
                    url = this.get_subscribeUrl();
                    url = url + '?threadKey=' + escape(this.get_subscriptionItemKey());

                    jQuery.ajax({
                        type: "POST",
                        url: url,
                        cache: false,
                        success: jQuery.proxy(this.onSubscribeSuccess, this),
                        dataType: "json"
                    });
                },

                onSubscribeSuccess: function () {
                    jQuery(this.get_subscribeWrp()).hide();
                    jQuery(this.get_successfullyUnsubscribedWrp()).hide();
                    jQuery(this.get_successfullySubscribedWrp()).show();
                },

                _unsubscribeLinkHandler: function (sender) {
                    url = this.get_unsubscribeUrl();
                    url = url + '?threadKey=' + escape(this.get_subscriptionItemKey());

                    jQuery.ajax({
                        type: "POST",
                        url: url,
                        cache: false,
                        success: jQuery.proxy(this.onUnsubscribeSuccess, this),
                        dataType: "json"
                    });
                },

                onUnsubscribeSuccess: function () {
                    jQuery(this.get_unsubscribeWrp()).hide();
                    jQuery(this.get_successfullySubscribedWrp()).hide();
                    jQuery(this.get_successfullyUnsubscribedWrp()).show();
                }
            };
        },

        initializeHandlers: function () {
            var self = this;

            self.commentsLoadMoreButton().click(function () {
                self.maxCommentsToShow += self.settings.commentsPerPage;
                self.loadComments(self.commentsTakenSoFar, self.settings.commentsPerPage);
                return false;
            });

            self.commentsContainer().on('click', '[data-sf-role="comments-read-full-comment-button"]', function (e) {
                if (e && e.target) {
                    $(e.target).hide().siblings().show();
                    return false;
                }
            });

            self.commentsSortNewButton().click(function () {
                self.sortComments(true);
                return false;
            });

            self.commentsSortOldButton().click(function () {
                self.sortComments(false);
                return false;
            });

            self.newCommentFormButton().click(function () {
                self.newCommentForm().toggle();
                return false;
            });

            self.newCommentSubmitButton().click(function () {
                self.submitNewComment();
                return false;
            });
        },

        initialize: function () {
            this.initializeProperties();
            this.initializeUserStatus();
            this.initializeComments();
            this.initializeSubscribtion();
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
            (new CommentsListWidget(element, settings)).initialize();
        });
    });
}(jQuery));
