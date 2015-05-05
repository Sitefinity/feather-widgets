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

    var commentsRestApi = (function () {
        var rootUrl = '/RestApi/comments-api/comments';

        var getCommentsCount = function (threadKey, status) {
            var getCommentsCountUrl = rootUrl + '/count?ThreadKey=' + threadKey;
            if (status) {
                getCommentsCountUrl += '&Status=' + status;
            }

            return makeAjax(getCommentsCountUrl);
        };

        var getComments = function (threadKey, skip, take, sortDescending, language, olderThan, newerThan) {
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
            if (olderThan) {
                getCommentsUrl += '&OlderThan=', olderThan;
            }
            if (newerThan) {
                getCommentsUrl += '&OlderThan=', newerThan;
            }

            return makeAjax(getCommentsUrl);
        };

        var createComment = function (comment) {
            return makeAjax(rootUrl, 'POST', JSON.stringify(comment));
        };

        var getCaptcha = function () {
            var getCommentsUrl = '/RestApi/comments-api/captcha';

            return makeAjax(getCommentsUrl);
        };

        return {
            getCommentsCount: getCommentsCount,
            getComments: getComments,
            createComment: createComment,
            getCaptcha: getCaptcha
        };
    }());

    $(function () {
        var isUserAuthenticated = false;

        // Check if user is logged in
        makeAjax('/RestApi/session/is-authenticated?_=' + (Math.random().toString().substr(2) + (new Date()).getTime())).then(function (response) {
            if (response && response.IsAuthenticated) {
                isUserAuthenticated = true;
                $('[data-sf-role="comments-new-logged-out-view"]').hide();
            };
        });

        var validateComment = function (comment) {
            var deferred = $.Deferred();

            if (isUserAuthenticated) {
                deferred.resolve(comment.Message.length > 0);
            }
            else {
                commentsRestApi.getCaptia().then(function (captia) {
                    //TODO: Logic
                    deferred.resolve(true);
                })
            }

            return deferred.promise();
        };

        var createWidget = function () {
            var $this = $(this);

            var template = $($this.find('[data-sf-role="single-comment-template"]').html());
            var commentsContainer = $this.find('[data-sf-role="comments-holder"]');

            var commentsHeader = $this.find('[data-sf-role="comments-header"]');
            var commentsHeaderText = $this.find('[data-sf-role="comments-header-text"]').val();
            var commentsFormButton = $this.find('[data-sf-role="comments-form-button"]');
            var commentsLoadMoreButton = $this.find('[data-sf-role="comments-load-more-button"]');

            // Initially hide new comment form
            var newCommentForm = $this.find('[data-sf-role="comments-new-holder"]').hide();
            var newCommentMessage = $this.find('[data-sf-role="comments-new-message"]');
            var newCommentName = $this.find('[data-sf-role="comments-new-name"]');
            var newCommentEmail = $this.find('[data-sf-role="comments-new-email"]');
            var newCommentWebsite = $this.find('[data-sf-role="comments-new-website"]');

            var commentsThreadKey = $this.find('[data-sf-role="comments-thread-key"]').val();
            var commentsPerPage = $this.find('[data-sf-role="comments-page-size"]').val();
            var commentsTakenSoFar = 0;
            var commentsSortedDescending = false;
            var commentsRefreshRate = 3000;

            var commentsTextMaxLength = $this.find('[data-sf-role="comments-text-max-length"]').val();
            var commentsReadMoreText = $this.find('[data-sf-role="comments-read-full-comment"]').val();

            var captchaImage = $this.find('[data-sf-role="captcha-image"]');
            var captchaInput = $this.find('[data-sf-role="captcha-input"]');
            var captchaRefreshLink = $this.find('[data-sf-role="captcha-refresh-button"]');

            /*
                Load comments
            */

            // Initial loading of comments count for thread
            commentsRestApi.getCommentsCount(commentsThreadKey).then(function (response) {
                if (response) {
                    var currentThreadKeyCount = 0;
                    response.Items.forEach(function (item) {
                        if (item.Key === commentsThreadKey) {
                            currentThreadKeyCount = item.Count;
                        }
                    });

                    if (currentThreadKeyCount > 0) {
                        commentsHeader.text(currentThreadKeyCount + commentsHeaderText);
                    }
                    else {
                        commentsFormButton.hide();
                        commentsHeader.text(commentsFormButton.text());
                        newCommentForm.show();
                    }

                    if (currentThreadKeyCount <= commentsTakenSoFar) {
                        commentsLoadMoreButton.hide();
                    }
                }
            });

            var attachCommentText = function (element, text) {
                if (element && text) {
                    if (text.length < commentsTextMaxLength) {
                        element.text(text);
                    }
                    else {
                        element.text(text.substr(0, commentsTextMaxLength));
                        element.append($('<span />').hide().text(text.substr(commentsTextMaxLength)));
                        element.append($('<button data-sf-role="comments-read-full-comment-button" />').text(commentsReadMoreText));
                    }
                }
            };

            var createComment = function (comment, prepend) {
                var newComment = template.clone(true);

                newComment.find('[data-sf-role="comment-name"]').text(comment.Name);
                newComment.find('[data-sf-role="comment-date"]').text(comment.Date);

                attachCommentText(newComment.find('[data-sf-role="comment-message"]'), comment.Message);

                if (prepend) {
                    commentsContainer.prepend(newComment);
                }
                else {
                    commentsContainer.append(newComment);
                }
            };

            var loadComments = function (skip, take) {
                take = take || commentsPerPage;

                commentsRestApi.getComments(commentsThreadKey, skip, take, commentsSortedDescending).then(function (response) {
                    response = response || {};
                    response.Items = response.Items || [];

                    commentsTakenSoFar = commentsTakenSoFar + response.Items.length;
                    response.Items.forEach(createComment);
                });
            };

            // Initial loading of comments
            loadComments(0);

            commentsLoadMoreButton.click(function () {
                loadComments(commentsTakenSoFar);
            });

            var refreshComments = function () {
                loadComments(0, commentsTakenSoFar);
            };

            // Read full comment
            commentsContainer.on('click', '[data-sf-role="comments-read-full-comment-button"]', function (e) {
                if (e && e.target) {
                    $(e.target).hide().siblings().show();
                }
            });

            /*
                Sort comments
            */

            var sortComments = function (descending) {
                if (descending !== commentsSortedDescending) {
                    commentsSortedDescending = descending;
                    commentsContainer.html('');
                    loadComments(0, commentsTakenSoFar);
                }
            };

            $this.find('[data-sf-role="comments-sort-new-button"]').click(function () {
                sortComments(true);
            });

            $this.find('[data-sf-role="comments-sort-old-button"]').click(function () {
                sortComments(false);
            });

            /*
                Captcha
            */

            var captchaData = {
                iv: null,
                correctAnswer: null,
                key: null
            };

            var refresh = function () {
                var deferred = $.Deferred();

                captchaImage.attr("src", "");
                captchaInput.hide();

                commentsRestApi.getCaptcha().then(function (data) {
                    if (data) {
                        captchaImage.attr("src", "data:image/png;base64," + data.Image);
                        captchaData.iv = data.InitializationVector;
                        captchaData.correctAnswer = data.CorrectAnswer;
                        captchaData.key = data.Key;
                        captchaInput.val("");
                        captchaInput.show();
                    }

                    deferred.resolve(true);
                })
            };

            captchaRefreshLink.click(function () {
                refresh();
            });

            /*
                New comment
            */

            commentsFormButton.click(function () {
                newCommentForm.toggle();
            });

            var submitForm = function () {
                var comment = {
                    Message: newCommentMessage.val(),
                    ThreadKey: commentsThreadKey
                };

                if (!isUserAuthenticated) {
                    comment.Name = newCommentName.val();
                    comment.Email = newCommentEmail.val();
                    comment.Website = newCommentWebsite.val();
                }

                validateComment(comment).then(function (isValid) {
                    if (isValid) {
                        commentsRestApi.createComment(comment);

                        newCommentMessage.val('');
                        newCommentForm.hide();
                        commentsFormButton.show();

                        refreshComments();
                    }
                    else {
                        // React ?
                    }
                });
            };

            $this.find('[data-sf-role="comments-new-submit-button"]').click(submitForm);

            // Comments updating
            //setInterval(refreshComments, commentsRefreshRate);
        };

        // Widgets initialization
        $('[data-sf-role="comments-wrapper"]').each(createWidget);
    });
}(jQuery));