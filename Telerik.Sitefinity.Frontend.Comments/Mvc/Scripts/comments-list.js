; (function ($) {
    'use strict';

    /*
        Rest Api
    */
    var RestApi = function (rootUrl, createCommentUrl, isUserAuthenticatedUrl, hasUserAlreadyReviewedUrl) {
        this.rootUrl = (rootUrl && rootUrl[rootUrl.length - 1] !== '/') ? (rootUrl + '/') : rootUrl;
        this.createCommentUrl = (createCommentUrl && createCommentUrl[createCommentUrl.length - 1] !== '/') ? (createCommentUrl + '/') : createCommentUrl;
        this.isUserAuthenticatedUrl = (isUserAuthenticatedUrl && isUserAuthenticatedUrl[isUserAuthenticatedUrl.length - 1] !== '/') ? (isUserAuthenticatedUrl + '/') : isUserAuthenticatedUrl;
        this.hasUserAlreadyReviewedUrl = (hasUserAlreadyReviewedUrl && hasUserAlreadyReviewedUrl[hasUserAlreadyReviewedUrl.length - 1] !== '/') ? (hasUserAlreadyReviewedUrl + '/') : hasUserAlreadyReviewedUrl;
    };

    RestApi.prototype = {
        getRandomNumber: function () {
            return Math.random().toString().substr(2) + (new Date()).getTime();
        },

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

        getCommentsCount: function myfunction(threadKey, status) {
            var getCommentsCountUrl = this.rootUrl + 'comments/count?ThreadKey=' + threadKey;
            if (status) {
                getCommentsCountUrl += '&Status=' + status;
            }

            return this.makeAjax(getCommentsCountUrl).then(function (response) {
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
                getCommentsUrl += '&NewerThan=' + encodeURIComponent(newerThan);
            }

            return this.makeAjax(getCommentsUrl);
        },

        createComment: function (comment) {
            var createCommentUrl = this.createCommentUrl || this.rootUrl + 'comments';
            return this.makeAjax(createCommentUrl, 'POST', JSON.stringify(comment));
        },

        getSubscriptionStatus: function (threadKey) {
            var subscriptionStatusUrl = this.rootUrl + 'notifications/?threadKey=' + threadKey;
            return this.makeAjax(subscriptionStatusUrl);
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

            return this.makeAjax(toggleSubscriptionUrl, 'POST');
        },

        getCaptcha: function () {
            var getCaptchaUrl = this.rootUrl + 'captcha';
            return this.makeAjax(getCaptchaUrl);
        },

        getIsUserAuthenticated: function () {
            var userAuthenticatedUrl = this.isUserAuthenticatedUrl;
            userAuthenticatedUrl += '?_=' + this.getRandomNumber();
            return this.makeAjax(userAuthenticatedUrl);
        },

        getHasUserAlreadyReviewed: function (threadKey) {
            var userAlreadyReviewedUrl = this.hasUserAlreadyReviewedUrl;
            userAlreadyReviewedUrl += '?ThreadKey=' + threadKey;
            userAlreadyReviewedUrl += '&_=' + this.getRandomNumber();
            return this.makeAjax(userAlreadyReviewedUrl);
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
        isSelectedSortButtonCssClass: 'selected',

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
        newCommentRating: function () { return this.getOrInitializeProperty('_newCommentRating', 'submit-rating-container'); },
        newCommentName: function () { return this.getOrInitializeProperty('_newCommentName', 'comments-new-name'); },
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

        commentsSubscribeText: function () { return this.getOrInitializeProperty('_commentsSubscribeText', 'comments-subscribe-text'); },
        commentsSubscribeButton: function () { return this.getOrInitializeProperty('_commentsSubscribeButton', 'comments-subscribe-button'); },
        commentsSubscribeButtonText: function () { return this.getOrInitializeProperty('_commentsSubscribeButtonText', 'comments-subscribe-button-text'); },

        newReviewFormReplacement: function () { return this.getOrInitializeProperty('_newReviewFormReplacement', 'review-new-form-replacement'); },

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

            return date.toISOString();
        },

        isValidEmail: function (email) {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            return regex.test(email);
        },

        validateComment: function (comment) {
            var deferred = $.Deferred();
            var isValid = true;

            if (comment.Message.length < 1) {
                isValid = false;
                this.newCommentMessage().after(this.errorMessage().clone(true).text(this.resources.messageIsRequired).show());
            }

            if (!this.isUserAuthenticated && comment.Name.length < 1) {
                isValid = false;
                this.newCommentName().after(this.errorMessage().clone(true).text(this.resources.nameIsRequired).show());
            }

            if (!this.isUserAuthenticated && comment.Email && !this.isValidEmail(comment.Email)) {
                isValid = false;
                this.newCommentEmail().after(this.errorMessage().clone(true).text(this.resources.invalidEmailFormat).show());
            }

            if (this.settings.useReviews && !comment.Rating) {
                isValid = false;
                this.newCommentMessage().after(this.errorMessage().clone(true).text(this.resources.ratingIsRequired).show());
            }

            deferred.resolve(isValid);

            return deferred.promise();
        },

        /*
            Comments listing
        */
        attachCommentMessage: function (element, message) {
            if (element && message) {
                if (message.length < this.settings.commentsTextMaxLength) {
                    element.text(message);
                }
                else {
                    element.text(message.substr(0, this.settings.commentsTextMaxLength));
                    element.append($('<span />').hide().text(message.substr(this.settings.commentsTextMaxLength)));
                    element.append($('<a href="#" data-sf-role="comments-read-full-comment-button" />').text(this.settings.useReviews ? this.resources.readFullReview : this.resources.readFullComment));
                }
            }
        },

        createCommentMarkup: function (comment) {
            var newComment = this.getSingleCommentTemplate().clone(true);

            newComment.find('[data-sf-role="comment-avatar"]').attr('src', comment.ProfilePictureThumbnailUrl).attr('alt', comment.Name);

            newComment.find('[data-sf-role="comment-name"]').text(comment.Name);
            newComment.find('[data-sf-role="comment-date"]').text(this.getDateFromSfString(comment.DateCreated).toDateString());

            this.attachCommentMessage(newComment.find('[data-sf-role="comment-message"]'), comment.Message);

            if (this.settings.useReviews) {
                newComment.find('[data-sf-role="list-rating-container"]').mvcRating({ readOnly: true, value: comment.Rating });
            }

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
            var singularText = this.settings.useReviews ? this.resources.reviewSingular : this.resources.commentSingular;
            var pluralText = this.settings.useReviews ? this.resources.reviewPlural : this.resources.commentsPlural;
            var multipleText = this.allCommentsCount > 1 ? pluralText : singularText;
            this.commentsHeader().text(this.allCommentsCount > 0 ? multipleText : this.newCommentFormButton().text());

            // Comments count value
            this.commentsTotalCount().toggle(this.allCommentsCount > 0).text(this.allCommentsCount);

            // Comments write comment button
            this.newCommentFormButton().css('display', this.allCommentsCount > 0 ? 'inline-block' : 'none');

            // Comments sort buttons
            this.commentsSortNewButton().css('display', this.allCommentsCount > 1 ? 'inline-block' : 'none');
            this.commentsSortOldButton().css('display', this.allCommentsCount > 1 ? 'inline-block' : 'none');

            // Comments load more button
            this.commentsLoadMoreButton().css('display', this.allCommentsCount > Math.max(this.commentsTakenSoFar, this.settings.commentsPerPage) ? 'inline-block' : 'none');
        },

        loadComments: function (skip, take, newerThan) {
            var self = this;
            if (self.isLoadinglist)
                return;

            self.isLoadingList = true;
            self.listLoadingIndicator().show();

            return self.restApi.getComments(self.settings.commentsThreadKey, skip, take, self.commentsSortedDescending, newerThan).then(function (response) {
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

        setAllCommentsCount: function (count, rating, supressEvent) {
            this.allCommentsCount = count;
            this.renderCommentsCount();

            if (!supressEvent) {
                $(document).trigger('sf-comments-count-received', { key: this.settings.commentsThreadKey, count: this.allCommentsCount, rating: rating });
            }
        },

        refreshComments: function (self, isNewCommentPosted, rating) {
            var commentsToTake = self.commentsSortedDescending ? self.settings.commentsPerPage : self.maxCommentsToShow - self.commentsTakenSoFar;

            // New comment is created, but won't be retrievet via refresh - update comment count.
            if (!self.commentsSortedDescending && commentsToTake <= 0 && isNewCommentPosted) {
                self.setAllCommentsCount(self.allCommentsCount + 1);
            }
            else {
                self.loadComments(0, commentsToTake, self.lastCommentDate).then(function (response) {
                    self.refreshLastCommentDate(response);

                    if (response && response.TotalCount) {
                        self.setAllCommentsCount(self.allCommentsCount + response.TotalCount, rating);
                    }
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

        toggleSubscription: function () {
            var self = this;

            self.restApi.toggleSubscription(self.settings.commentsThreadKey, self.isSubscribedToNewComments).then(function (response) {
                self.isSubscribedToNewComments = !self.isSubscribedToNewComments;

                self.commentsSubscribeButtonText().text(self.isSubscribedToNewComments ? self.resources.unsubscribeLink : self.resources.subscribeLink);

                var successfullySubscribedtext = self.settings.useReviews ? self.resources.successfullySubscribedToNewReviews : self.resources.successfullySubscribedToNewComments;
                self.commentsSubscribeText().text(self.isSubscribedToNewComments ? successfullySubscribedtext : self.resources.successfullyUnsubscribedFromNewComments);
            });
        },

        /*
            Comments creation
        */
        buildNewCommentFromForm: function () {
            var self = this;

            var comment = {
                Message: self.newCommentMessage().val(),
                ThreadKey: self.settings.commentsThreadKey,
            };

            if (self.settings.useReviews) {
                comment.Rating = this.newCommentRatingContainer.getValue();
            }

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

        cleanNewCommentForm: function () {
            this.newCommentMessage().val('');
            this.newCommentName().val('');
            this.newCommentEmail().val('');

            if (this.newCommentRatingContainer) {
                this.newCommentRatingContainer.setValue(0);
            }
        },

        endSubmitNewComment: function () {
            if (!this.isUserAuthenticated && this.settings.requiresCaptcha) {
                this.captchaRefresh();
            }

            this.submitLoadingIndicator().hide();
            this.newCommentSubmitButton().show();
        },

        createCommentSuccess: function (response) {
            this.cleanNewCommentForm();

            if (this.settings.requiresApproval) {
                this.newCommentPendingApprovalMessage().show();
            }
            else if (!this.settings.commentsAutoRefresh) {
                var rating = this.settings.useReviews && response ? response.Rating : null;
                this.refreshComments(this, true, rating);
            }

            if (this.settings.useReviews) {
                this.newCommentForm().hide();
                this.newCommentFormButton().hide();

                var textToShow = this.settings.requiresApproval ? this.newCommentPendingApprovalMessage().text() : this.resources.thankYouReviewSubmited;

                this.newReviewFormReplacement().text(textToShow).show();
            }
        },

        createCommentFail: function (jqXHR) {
            if (jqXHR && jqXHR.responseText) {
                var errorTxt = JSON.parse(jqXHR.responseText).ResponseStatus.Message;
                this.errorMessage().text(errorTxt).show();
            }
        },

        submitNewComment: function () {
            var self = this;

            self.submitLoadingIndicator().show();
            self.newCommentSubmitButton().hide();

            var comment = self.buildNewCommentFromForm();

            self.validateComment(comment).then(function (isValid) {
                if (isValid) {
                    self.restApi.createComment(comment)
                        .then(function (response) {
                            self.createCommentSuccess(response);
                        }, function (jqXHR) {
                            self.createCommentFail(jqXHR);
                        })
                        .always(function () {
                            self.endSubmitNewComment();
                        });
                }
                else {
                    self.endSubmitNewComment();
                }
            });
        },

        captchaRefresh: function () {
            var self = this;
            var deferred = $.Deferred();

            self.captchaImage().attr("src", "");
            self.captchaInput().hide();

            self.restApi.getCaptcha().then(function (data) {
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

        /*
            Widget initialization
        */
        initializeProperties: function () {
            this.restApi = new RestApi(this.settings.rootUrl, this.settings.createCommentUrl, this.settings.isUserAuthenticatedUrl, this.settings.hasUserAlreadyReviewedUrl);

            this.isLoadingList = false;
            this.isSubscribedToNewComments = false;
            this.maxCommentsToShow = this.settings.commentsPerPage;

            var rContainer = this.newCommentRating();
            if (rContainer && rContainer.length) {
                this.newCommentRatingContainer = rContainer.mvcRating();
            }

            // Initially hide the "RequiresAuthentication" message.
            this.newCommentRequiresAuthentication().hide();

            // Initially hide the "Thank you for your comment" message.
            this.newCommentPendingApprovalMessage().hide();

            // Initially hide the "You've already submitted a review for this item" message.
            if (this.settings.useReviews) {
                this.newReviewFormReplacement().hide();
            }

            if (this.commentsSortedDescending) {
                this.commentsSortNewButton().addClass(this.isSelectedSortButtonCssClass);
            }
            else {
                this.commentsSortOldButton().addClass(this.isSelectedSortButtonCssClass);
            }
        },

        initializeUserStatus: function () {
            var self = this;

            self.restApi.getIsUserAuthenticated().then(function (response) {
                if (response) {
                    if (response.IsAuthenticated) {
                        self.isUserAuthenticated = true;

                        // Get Subscribtion status only if user is logged in.
                        self.initializeSubscription();
                    }
                    else if (self.settings.requiresAuthentication) {
                        self.newCommentForm().hide();
                        self.newCommentRequiresAuthentication().show();
                    }

                    if (self.settings.useReviews) {
                        self.initializeHasUserAlreadyReviewed();
                    }
                }

                $.proxy(self.setupCaptcha(), self);
            });
        },

        initializeSubscription: function () {
            var self = this;

            self.restApi.getSubscriptionStatus(self.settings.commentsThreadKey).then(function (response) {
                if (response) {
                    self.isSubscribedToNewComments = response.IsSubscribed;

                    var subscribeText = self.settings.useReviews ? self.resources.subscribeToNewReviews : self.resources.subscribeToNewComments;
                    var youAreSubscribedText = self.settings.useReviews ? self.resources.youAreSubscribedToNewReviews : self.resources.youAreSubscribedToNewComments;

                    self.commentsSubscribeButtonText().text(self.isSubscribedToNewComments ? self.resources.unsubscribeLink : subscribeText);
                    self.commentsSubscribeText().text(self.isSubscribedToNewComments ? youAreSubscribedText : '');

                    self.commentsSubscribeButton().click(function () {
                        self.toggleSubscription();
                        return false;
                    });
                }
            });
        },

        initializeComments: function () {
            var self = this;

            // Initial loading of comments count for thread
            self.restApi.getCommentsCount(self.settings.commentsThreadKey).then(function (response) {
                // Initial count initialization should not raise count change event
                self.setAllCommentsCount(response, null, true);
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

        initializeHasUserAlreadyReviewed: function () {
            var self = this;

            self.restApi.getHasUserAlreadyReviewed(self.settings.commentsThreadKey).then(function (response) {
                if (response && response.AuthorAlreadyReviewed) {
                    self.newCommentForm().hide();
                    self.newCommentFormButton().hide();
                    self.newReviewFormReplacement().show();
                }
            });
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
                    // Hide all generated errors
                    self.errorMessage().hide();
                    self.getElementByDataSfRole('error-message').hide();

                    self.submitNewComment();
                }

                return false;
            });

            self.captchaRefreshLink().click(function () {
                self.captchaRefresh();
                return false;
            });

            self.newCommentMessage().focus(function () {
                if (!self.isUserAuthenticated) {
                    self.commentsNewLoggedOutView().show();
                }
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
