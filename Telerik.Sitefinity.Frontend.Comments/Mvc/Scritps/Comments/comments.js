; (function ($) {
    $(function () {
        var template = $($('[data-sf-role="single-comment-template"]').html());
        var commentsContainer = $('[data-sf-role="comments-holder"]');

        // Initially hide new comment form
        var newCommentForm = $('[data-sf-role="comments-new-holder"]').hide();

        var commentsThreadKey = $('[data-sf-role="comments-thread-key"]').val();
        var commentsPerPage = $('[data-sf-role="comments-page-size"]').val();
        var commentsTakenSoFar = 0;
        var commentsSortedDescending = false;

        /*
            Service calls
        */

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

        var getComments = function (skip, take, sortDescending) {
            var urlParams = 'ThreadKey=' + commentsThreadKey + '&Take=' + take;

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

        /*
            Load comments
        */

        var clearComments = function () {
            commentsContainer.html('');
        };

        var createComment = function (comment) {
            var newComment = template.clone(true);

            newComment.find('[data-sf-role="comment-name"]').text(comment.Name);
            newComment.find('[data-sf-role="comment-date"]').text(comment.Date);
            newComment.find('[data-sf-role="comment-text"]').text(comment.Text);

            commentsContainer.append(newComment);
        };

        var showComments = function (comments) {
            if (comments && comments.length) {
                $('[data-sf-role="comments-header"]').text(comments.length + $('[data-sf-role="comments-header-text"]').val());
                comments.forEach(createComment);
            }
            else {
                $('[data-sf-role="comments-header"]').text($('[data-sf-role="comments-form"]').hide().text());
                newCommentForm.show();
            }
        };

        var loadComments = function (skip, take, sortDescending) {
            take = take || commentsPerPage;

            getComments(commentsTakenSoFar, take).then(function (response) {
                response = response || {};
                response.Items = response.Items || [];

                commentsTakenSoFar = commentsTakenSoFar + response.Items.length;
                showComments(response.Items);
            });
        };

        $('[data-sf-role="comments-load-more"]').click(function () {
            loadComments(commentsTakenSoFar);
        });
        
        // Initial loading of comments
        loadComments();

        /*
            Sort comments
        */

        $('[data-sf-role="comments-sort-new"]').click(function () {
            if (commentsSortedDescending === false) {
                commentsSortedDescending = true;
                clearComments();
                loadComments(0, commentsTakenSoFar, true);
            }
        });

        $('[data-sf-role="comments-sort-old"]').click(function () {
            if (commentsSortedDescending === true) {
                commentsSortedDescending = false;
                clearComments();
                loadComments(0, commentsTakenSoFar, false);
            }
        });

        /*
            New comment
        */

        // Remove unneeded fields in the form if user is logged in
        getIsUserLoggedIn().then(function (response) {
            if (!(response && response.IsAuthenticated)) {
                $('[data-sf-role="comments-new-logged-out-view"]').hide();
            }
        });

        $('[data-sf-role="comments-form"]').click(function () {
            newCommentForm.toggle('slow');
        });

        $('[data-sf-role="comments-new-submit"]').click(function () {
            // service call
        });
    });
}(jQuery));