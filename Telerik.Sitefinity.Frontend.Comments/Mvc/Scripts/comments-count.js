; (function ($) {
    'use strict';

    /*
        Widget
    */
    var CommentsCountWidget = function (rootUrl) {
        if (rootUrl === null || rootUrl.length === 0)
            rootUrl = '/';
        else if (rootUrl.charAt(rootUrl.length - 1) !== '/')
            rootUrl = rootUrl + '/';

        this.rootUrl = rootUrl;
    };

    CommentsCountWidget.prototype = {
        getIsReviewThreadKey: function (threadKey) {
            var useReviewSuffix = '_review';
            return threadKey.indexOf(useReviewSuffix, threadKey.length - useReviewSuffix.length) >= 0;
        },

        makeAjax: function (url, type) {
            return $.ajax({
                type: type || 'GET',
                url: url,
                contentType: 'application/json',
                cache: false,
                accepts: {
                    text: 'application/json'
                },
                processData: false
            });
        },

        collectThreadIds: function () {
            var self = this;

            // retrieve all comments count wrappers on page
            var commmentsCounterControls = $('[data-sf-role="comments-count-wrapper"]');
            var uniqueKeys = {};
            for (var i = 0; i < commmentsCounterControls.length; i++) {
                uniqueKeys[$(commmentsCounterControls[i]).attr('data-sf-thread-key')] = true;
            }

            var threadKeys = [];
            var reviewSuffix = '_review';
            $.each(uniqueKeys, function (key) {
                threadKeys.push(key);
            });

            return threadKeys;
        },

        getCommentsCounts: function (commentsThreadKeys) {
            var getCommentsCountsUrl = this.rootUrl + 'comments/count?ThreadKey=' + encodeURIComponent(commentsThreadKeys);

            return this.makeAjax(getCommentsCountsUrl).then(function (response) {
                if (response && response.Items) {
                    return response.Items;
                }
            });
        },

        getReviewsCounts: function (reviewsThreadKeys) {
            var getReviewsCountsUrl = this.rootUrl + 'comments/reviews_statistics?ThreadKey=' + encodeURIComponent(reviewsThreadKeys);

            return this.makeAjax(getReviewsCountsUrl);
        },

        getCommentsAndReviewsCounts: function (commentsThreadKeys, reviewsThreadKeys) {
            var self = this;

            var allCounts = [];

            return self.getCommentsCounts(commentsThreadKeys).then(function (commentsCountsResponse) {
                if (commentsCountsResponse && commentsCountsResponse.length) {
                    allCounts = allCounts.concat(commentsCountsResponse);
                }

                return self.getReviewsCounts(reviewsThreadKeys).then(function (reviewsCountsResponse) {
                    if (reviewsCountsResponse && reviewsCountsResponse.length) {
                        allCounts = allCounts.concat(reviewsCountsResponse);
                    }

                    return allCounts;
                });
            });
        },

        getAllCounts: function () {
            var self = this;

            var threadKeys = this.collectThreadIds();

            var commentsThreadKeys = threadKeys.filter(function (key) {
                return !self.getIsReviewThreadKey(key);
            });
            var reviewsThreadKeys = threadKeys.filter(function (key) {
                return self.getIsReviewThreadKey(key);
            });

            if (commentsThreadKeys.length > 0 && reviewsThreadKeys.length > 0) {
                return this.getCommentsAndReviewsCounts(commentsThreadKeys, reviewsThreadKeys);
            }
            else if (commentsThreadKeys.length > 0) {
                return this.getCommentsCounts(commentsThreadKeys);
            }
            else if (reviewsThreadKeys.length > 0) {
                return this.getReviewsCounts(reviewsThreadKeys);
            }
            else {
                return {
                    // no comments or reviews - resolve with empty array
                    then: function (cb) {
                        cb([]);
                    }
                };
            }
        },

        setCommentsCounts: function () {
            var self = this;

            self.getAllCounts().then(function (threadCountList) {
                for (var i = 0; i < threadCountList.length; i++) {
                    if (threadCountList[i].Count >= 0) {
                        $('div[data-sf-thread-key="' + threadCountList[i].Key + '"]').each(self.populateCommentsCountTextCallBack(threadCountList[i].Count, threadCountList[i].AverageRating));
                    }
                }
            });
        },

        populateCommentsCountTextCallBack: function (currentCount, currentRating) {
            var self = this;
            return function (index, element) {
                self.populateCommentsCountText($(element), currentCount, currentRating);
            };
        },

        populateCommentsCountText: function (element, currentCount, currentRating) {
            var resources = JSON.parse(element.find('[data-sf-role="comments-count-resources"]').val());

            var currentCountFormatted = '';
            if (!currentCount) {
                currentCountFormatted = resources.leaveComment;
            }
            else {
                currentCountFormatted = currentCount;

                if (currentCount == 1)
                    currentCountFormatted += ' ' + resources.comment.toLowerCase();
                else
                    currentCountFormatted += ' ' + resources.commentsPlural.toLowerCase();
            }

            // set the comments count text in the counter control
            element.find('[data-sf-role="comments-count-anchor-text"]').text(currentCountFormatted);

            // render average rating
            if (currentCount && currentRating) {
                this.renderAverageRating(element, currentRating, currentCount);
            }
        },

        renderAverageRating: function (element, currentRating, currentCount, averageRatingResource) {
            var wrapper = element.find('[data-sf-role="list-rating-wrapper"]').show();
            var oldRatingEl = wrapper.find('[data-sf-role="list-rating-value"]');
            var oldRating = oldRatingEl.text().trim();

            // There is already rating - update it
            if (oldRating) {
                var oldRatingValue = parseFloat(oldRating);
                var newRating = (((currentCount - 1) * oldRatingValue) + currentRating) / currentCount;
                // round to the second decimal
                currentRating = Math.round(newRating * 100) / 100;
            }

            wrapper.find('[data-sf-role="list-rating-container"]').mvcRating({ readOnly: true, value: currentRating, template: $('[data-sf-role="rating-template"]') });
            oldRatingEl.text(currentRating);
        },

        initialize: function () {
            var self = this;

            self.setCommentsCounts();

            $(document).on('sf-comments-count-received', function (event, args) {
                $('div[data-sf-thread-key="' + args.key + '"]').each(self.populateCommentsCountTextCallBack(args.count, args.rating));
            });
        },
    };

    /*
        Widgets creation
    */
    $(function () {
        var rootUrl = $('[data-sf-role="comments-count-wrapper"]').find('[data-sf-role="service-url"]').val();
        (new CommentsCountWidget(rootUrl)).initialize();
    });
}(jQuery));
