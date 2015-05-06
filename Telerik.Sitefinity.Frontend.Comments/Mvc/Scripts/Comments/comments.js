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

    $(function () {
        var isUserAuthenticated = false;

        // Check if user is logged in
        makeAjax('/RestApi/session/is-authenticated?_=' + (Math.random().toString().substr(2) + (new Date()).getTime())).then(function (response) {
            if (response && response.IsAuthenticated) {
                isUserAuthenticated = true;
                $('[data-sf-role="comments-new-logged-out-view"]').hide();
            };
        });

        var getDateString = function (sfDateString, secondsOffset) {
            var date = new Date(parseInt(sfDateString.replace(/\D/g, ''), 10));

            // On request dates are converted to local time so offset is added here.
            date.setMinutes(date.getMinutes() + date.getTimezoneOffset());
            date.setSeconds(date.getSeconds() + secondsOffset);

            return date.toUTCString();
        };

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
            var commentsSortedDescending = true;
            var commentsRefreshRate = 3000;
            var commentsTakenSoFar = 0;
            var firstCommentDate = 0;
            var lastCommentDate = 0;
            var maxCommentsToShow = commentsPerPage;

            var commentsTextMaxLength = $this.find('[data-sf-role="comments-text-max-length"]').val();
            var commentsReadMoreText = $this.find('[data-sf-role="comments-read-full-comment"]').val();

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

                    if (currentThreadKeyCount <= Math.max(commentsTakenSoFar, commentsPerPage)) {
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

            var createComments = function (comments, prepend) {
                comments.forEach(function (comment) {
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
                });
            };

            var loadComments = function (skip, take, newerThan) {
                commentsRestApi.getComments(commentsThreadKey, skip, take, commentsSortedDescending, newerThan).then(function (response) {
                    if (response && response.Items && response.Items.length) {
                        commentsTakenSoFar += response.Items.length;

                        firstCommentDate = getDateString(response.Items[0].DateCreated, 1);
                        lastCommentDate = getDateString(response.Items[response.Items.length - 1].DateCreated, 1);

                        // Prepend the recieved comments only if current sorting is descending and the comments are being refreshed
                        createComments(response.Items, newerThan && commentsSortedDescending);
                    }
                });
            };

            var refreshComments = function () {
                if (commentsSortedDescending) {
                    loadComments(0, commentsPerPage, firstCommentDate);
                }
                else if (maxCommentsToShow - commentsTakenSoFar > 0) {
                    loadComments(0, maxCommentsToShow - commentsTakenSoFar, lastCommentDate);
                }
            };

            // Initial loading of comments
            loadComments(0, commentsPerPage);

            commentsLoadMoreButton.click(function () {
                maxCommentsToShow += commentsPerPage;
                loadComments(commentsTakenSoFar, commentsPerPage);
                return false;
            });

            // Read full comment
            commentsContainer.on('click', '[data-sf-role="comments-read-full-comment-button"]', function (e) {
                if (e && e.target) {
                    $(e.target).hide().siblings().show();
                    return false;
                }
            });

            /*
                Sort comments
            */

            var sortComments = function (descending) {
                if (descending !== commentsSortedDescending) {
                    commentsSortedDescending = descending;
                    commentsContainer.html('');
                    // Use commentsTakenSoFar to show again same amout of comments?
                    loadComments(0, commentsPerPage);
                    commentsTakenSoFar = 0;
                }
            };

            $this.find('[data-sf-role="comments-sort-new-button"]').click(function () {
                sortComments(true);
                return false;
            });

            $this.find('[data-sf-role="comments-sort-old-button"]').click(function () {
                sortComments(false);
                return false;
            });

            /*
                New comment
            */

            commentsFormButton.click(function () {
                newCommentForm.toggle();
                return false;
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
                        commentsRestApi.createComment(comment).then(function (response) {
                            console.log(response);

                            newCommentMessage.val('');
                            newCommentForm.hide();
                            commentsFormButton.show();
                        });

                        // Comments refresh will handle the new comment.
                        // refreshComments();
                    }
                    else {
                        // React ?
                    }
                });
            };

            $this.find('[data-sf-role="comments-new-submit-button"]').click(function () {
                submitForm();
                return false;
            });

            // Comments updating
            setInterval(refreshComments, commentsRefreshRate);
        };

        // Widgets initialization
        $('[data-sf-role="comments-wrapper"]').each(createWidget);
    });
}(jQuery));