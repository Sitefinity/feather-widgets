; (function ($) {
    $(function () {
        var makeAjax = function (url, type) {
            type = type || 'GET';

            return $.ajax({
                type: type,
                url: url,
                contentType: "application/json",
                accepts: {
                    text: "application/json"
                },
                cache: false
            });
        };

        var getRandom = function () {
            return Math.random().toString().substr(2) + (new Date()).getTime();
        };

        var getIsUserLoggedIn = function () {
            return makeAjax('/RestApi/session/is-authenticated?_=' + getRandom());
        };

        var getComments = function (threadKey, skip, take, sortDescending) {
            var urlParams = 'ThreadKey=' + threadKey + '&Take=' + take;

            if (skip && skip > 0) {
                urlParams = urlParams + '&Skip=' + skip;
            }

            if (sortDescending === true) {
                urlParams = urlParams + '&SortDescending=True';
            }

            return makeAjax('/RestApi/comments-api/comments/?' + urlParams);
        };

        var postComment = function () {
            // Service call
        };

        // Remove unneeded fields from all forms if user is logged in
        getIsUserLoggedIn().then(function (response) {
            if (!(response && response.IsAuthenticated)) {
                $('[data-sf-role="comments-new-logged-out-view"]').hide();
            };
        });

        var createWidget = function () {
            var $this = $(this);

            var template = $($this.find('[data-sf-role="single-comment-template"]').html());
            var commentsContainer = $this.find('[data-sf-role="comments-holder"]');

            var commentsHeader = $this.find('[data-sf-role="comments-header"]');
            var commentsHeaderText = $this.find('[data-sf-role="comments-header-text"]').val();
            var commentsFormButton = $this.find('[data-sf-role="comments-form-button"]');

            // Initially hide new comment form
            var newCommentForm = $this.find('[data-sf-role="comments-new-holder"]').hide();

            var commentsThreadKey = $this.find('[data-sf-role="comments-thread-key"]').val();
            var commentsPerPage = $this.find('[data-sf-role="comments-page-size"]').val();
            var commentsTakenSoFar = 0;
            var commentsSortedDescending = false;

            var commentsTextMaxLength = $this.find('[data-sf-role="comments-text-max-length"]').val();
            var commentsReadMoreText = $this.find('[data-sf-role="comments-read-full-comment"]').val();

            /*
                Load comments
            */

            var attachText = function (element, text) {
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

            var createComment = function (comment) {
                var newComment = template.clone(true);

                newComment.find('[data-sf-role="comment-name"]').text(comment.Name);
                newComment.find('[data-sf-role="comment-date"]').text(comment.Date);

                attachText(newComment.find('[data-sf-role="comment-text"]'), comment.Text);

                commentsContainer.append(newComment);
            };

            var showComments = function (comments) {
                if (comments && comments.length) {
                    commentsHeader.text(comments.length + commentsHeaderText);
                    comments.forEach(createComment);
                }
                else {
                    commentsFormButton.hide();
                    commentsHeader.text(commentsFormButton.text());
                    newCommentForm.show();
                }
            };

            var loadComments = function (skip, take, sortDescending) {
                take = take || commentsPerPage;
                skip = skip || commentsTakenSoFar;

                getComments(commentsThreadKey, commentsTakenSoFar, take).then(function (response) {
                    response = response || {};
                    response.Items = response.Items || [];

                    commentsTakenSoFar = commentsTakenSoFar + response.Items.length;
                    showComments(response.Items);
                });
            };

            // Initial loading of comments
            loadComments();

            $this.find('[data-sf-role="comments-load-more-button"]').click(loadComments);

            // Read more comments
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
                    commentsSortedDescending = !commentsSortedDescending;
                    commentsContainer.html('');
                    loadComments(0, commentsTakenSoFar, descending);
                }
            };

            $this.find('[data-sf-role="comments-sort-new-button"]').click(function () {
                sortComments(true);
            });

            $this.find('[data-sf-role="comments-sort-old-button"]').click(function () {
                sortComments(false);
            });

            /*
                New comment
            */

            commentsFormButton.click(function () {
                newCommentForm.toggle();
            });

            $this.find('[data-sf-role="comments-new-submit-button"]').click(function () {
                // service call
            });
        };

        // Widgets initialization
        $('[data-sf-role="comments-wrapper"]').each(createWidget);
    });
}(jQuery));