; (function ($) {
    'use strict';

    /*
        Widget
    */
    var CommentsCountWidget = function (rootUrl, resources) {

        this.getCommentsCounts = function () {
            var threadKeys = this.collectThreadIds();
            var getCountUrl = String.format(rootUrl + '/comments/count?ThreadKey={0}', threadKeys);
            return $.ajax({
                type: 'GET',
                url: getCountUrl,
                contentType: 'application/json',
                cache: false,
                accepts: {
                    text: 'application/json'
                },
                processData: false
            });
        };

        this.collectThreadIds = function () {
            var commmentsCounterControls = $('[data-sf-role="comments-count-wrapper"]');
            var uniqueKeys = {};
            for (var i = 0; i < commmentsCounterControls.length; i++) {
                uniqueKeys[$(commmentsCounterControls[i]).attr('sf-thread-key')] = true;
            }
            var threadKeys = [];
            $.each(uniqueKeys, function (key, value) {
                threadKeys.push(key);
            });
            return threadKeys;
        };

        this.setCommentsCounts = function (data) {
            var threadCountList = data;

            var populateCommentsCountText = function (index, commentsCounterControl) {
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

                //set the comments count text in the counter control
                var anchor = $(commentsCounterControl).find('[data-sf-role="comments-count-anchor"]');
                if (anchor)
                    $(anchor).html(currentCountFormatted);
            };

            for (var i = 0; i < threadCountList.Items.length; i++) {
                var currentThreadKey = threadCountList.Items[i].Key;
                var commmentsCounterControls = $('div[sf-thread-key="' + currentThreadKey + '"]');
                var currentCount = threadCountList.Items[i].Count;

                //format count
                if (currentCount == -1)
                    continue;

                var self = this;
                $.each(commmentsCounterControls, populateCommentsCountText);
            }
        };

        this.initialize = function () {
            var self = this;

            self.getCommentsCounts().then(function (response) {
                if (response) {
                    self.setCommentsCounts(response);
                }
            });
        };
    };

    /*
        Counts creation
    */
    $(function () {
        var serviceUrl = $('[data-sf-role="comments-count-wrapper"]').find('[data-sf-role="service-url"]').val();
        var resources = JSON.parse($('[data-sf-role="comments-count-wrapper"]').find('[data-sf-role="comments-count-resources"]').val());
        (new CommentsCountWidget(serviceUrl, resources)).initialize();
    });
}(jQuery));
