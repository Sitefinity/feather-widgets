; (function ($) {
    var makeAjax = function (url, type, data) {
        var options = {
            type: type || 'GET',
            url: url,
            contentType: "application/json",
            accepts: {
                text: "application/json"
            },
            cache: false
        };

        if (data) {
            options.data = data;
        }

        return $.ajax(options);
    };

    var basicCommentsRestApi = (function () {
        var rootUrl = '/RestApi/comments-api/comments';

        var getCommentsCount = function (threadKey, status) {
            var getCommentsCountUrl = rootUrl + '/count?ThreadKey=' + threadKey;
            if (status) {
                getCommentsCountUrl += '&Status=' + status;
            }

            return makeAjax(getCommentsCountUrl);
        };

        var getComments = function (threadKey, skip, take, sortDescending, newerThan, language) {
            var getCommentsUrl = rootUrl + '/?ThreadKey=' + threadKey + '&Take=' + take;

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
        };

        var createComment = function (comment) {
            return makeAjax(rootUrl, 'POST', JSON.stringify(comment));
        };

        var getCaptia = function () {

        };

        return {
            getCommentsCount: getCommentsCount,
            getComments: getComments,
            createComment: createComment,
            getCaptia: getCaptia
        };
    }());

    var CommentsMvcWidget = function (wrapper, settings) {
        this.settings = settings || {};
        this.wrapper = wrapper;
        this.commentsRestApi = basicCommentsRestApi;

        this.commentsSortedDescending = true;
        this.commentsRefreshRate = 3000;
        this.commentsTakenSoFar = 0;
        this.firstCommentDate = 0;
        this.lastCommentDate = 0;
        this.maxCommentsToShow = 0;
    };

    CommentsMvcWidget.prototype = {
        isUserAuthenticated: false,

        getElementByDataSfRole: function (sfRole) {
            return this.wrapper.find('[data-sf-role="' + sfRole + '"]');
        },

        getOrInitializeProperty: function (property, sfRole) {
            if (!this[property]) {
                this[property] = this.getElementByDataSfRole(sfRole);
            }

            return this[property];
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
        newCommentMessage: function () { return this.getOrInitializeProperty('_newCommentMessage', 'comments-new-message'); },
        newCommentName: function () { return this.getOrInitializeProperty('_newCommentName', 'comments-new-name'); },
        newCommentEmail: function () { return this.getOrInitializeProperty('_newCommentEmail', 'comments-new-email'); },
        newCommentWebsite: function () { return this.getOrInitializeProperty('_newCommentWebsite', 'comments-new-website'); },
        commentsNewLoggedOutView: function () { return this.getOrInitializeProperty('_commentsNewLoggedOutView', 'comments-new-logged-out-view'); },
        commentsSortNewButton: function () { return this.getOrInitializeProperty('_commentsSortNewButton', 'comments-sort-new-button'); },
        commentsSortOldButton: function () { return this.getOrInitializeProperty('_commentsSortOldButton', 'comments-sort-old-button'); },

        // Remove if we use only settings
        commentsThreadKey: function () { return this.getOrInitializeProperty('_commentsThreadKey', 'comments-thread-key', 'val'); },
        commentsPerPage: function () { return parseInt(this.getOrInitializeProperty('_commentsPerPage', 'comments-per-page', 'val')); },
        commentsTextMaxLength: function () { return parseInt(this.getOrInitializeProperty('_commentsTextMaxLength', 'comments-text-max-length', 'val')); },
        commentsReadFullCommentText: function () { return this.getOrInitializeProperty('_commentsReadFullCommentText', 'comments-read-full-comment', 'val'); },
        commentsHeaderText: function () { return this.getOrInitializeProperty('_commentsHeaderText', 'comments-header-text', 'val'); },

        getDateString: function (sfDateString, secondsOffset) {
            var date = new Date(parseInt(sfDateString.replace(/\D/g, ''), 10));
            date.setMinutes(date.getMinutes() + date.getTimezoneOffset());
            date.setSeconds(date.getSeconds() + secondsOffset);

            return date.toUTCString();
        },

        validateComment: function (comment) {
            var deferred = $.Deferred();

            if (this.isUserAuthenticated) {
                deferred.resolve(comment.Message.length > 0);
            }
            else {
                this.commentsRestApi.getCaptia().then(function (captia) {
                    //TODO: captia logic

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

        submitForm: function () {
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

        initialize: function () {
            var self = this;

            self.maxCommentsToShow = self.settings.commentsPerPage;

            // Initially hide new comment form
            self.newCommentForm().hide();

            // Check if user is logged in
            makeAjax('/RestApi/session/is-authenticated?_=' + (Math.random().toString().substr(2) + (new Date()).getTime())).then(function (response) {
                if (response && response.IsAuthenticated) {
                    self.isUserAuthenticated = true;
                    self.commentsNewLoggedOutView().hide();
                }
            });

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

            /* 
                Event handlers 
            */

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
                self.submitForm();
                return false;
            });
            
            // Comments updating
            setInterval(function () {
                self.refreshComments(self);
            }, self.commentsRefreshRate);
        }
    };
    
    $(function () {
        $('[data-sf-role="comments-wrapper"]').each(function () {
            var element = $(this);
            var settings = JSON.parse(element.find('[data-sf-role="comments-settings"]').val());
            (new CommentsMvcWidget(element, settings)).initialize();
        });
    });
}(jQuery));
