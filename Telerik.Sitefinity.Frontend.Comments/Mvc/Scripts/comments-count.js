; (function ($) {
    'use strict';

    /*
        Widget
    */
    var CommentsCountWidget = function (rootUrl, resources) {
        this.rootUrl = rootUrl;
        this.resources = resources;
    };

    CommentsCountWidget.prototype = {
        getCommentsCounts: function () {
            var threadKeys = this.collectThreadIds();
            var getCommentsCountsUrl = String.format(this.rootUrl + '/comments/count?ThreadKey={0}', threadKeys);

            return $.ajax({
                type: 'GET',
                url: getCommentsCountsUrl,
                contentType: 'application/json',
                cache: false,
                accepts: {
                    text: 'application/json'
                },
                processData: false
            });
        },

        collectThreadIds: function () {
            var commmentsCounterControls = $('[data-sf-role="comments-count-wrapper"]');
            var uniqueKeys = {};
            for (var i = 0; i < commmentsCounterControls.length; i++) {
                uniqueKeys[$(commmentsCounterControls[i]).attr('data-sf-thread-key')] = true;
            }

            var threadKeys = [];
            $.each(uniqueKeys, function (key) {
                threadKeys.push(key);
            });

            return threadKeys;
        },

        setCommentsCounts: function (threadCountList) {
            var self = this;
            for (var i = 0; i < threadCountList.Items.length; i++) {
                if (threadCountList.Items[i].Count == -1) {
                    continue;
                }
                
                $('div[data-sf-thread-key="' + threadCountList.Items[i].Key + '"]').each(self.populateCommentsCountTextCallBack(threadCountList.Items[i].Count));
            }
        },
        
        populateCommentsCountTextCallBack: function (currentCount) {
            var self = this;
            return function (index, element) {
                self.populateCommentsCountText($(element), currentCount);
            };
        },

        populateCommentsCountText: function (element, currentCount) {
            var currentCountFormatted = '';
            if (!currentCount) {
                currentCountFormatted = this.resources.leaveComment;
            }
            else {
                currentCountFormatted = currentCount;

                if (currentCount == 1)
                    currentCountFormatted += ' ' + this.resources.comment.toLowerCase();
                else
                    currentCountFormatted += ' ' + this.resources.commentsPlural.toLowerCase();
            }

            //set the comments count text in the counter control
            element.find('[data-sf-role="comments-count-anchor-text"]').text(currentCountFormatted);
        },

        initialize: function () {
            var self = this;

            self.getCommentsCounts().then(function (response) {
                if (response) {
                    self.setCommentsCounts(response);
                }
            });

            $(document).on('sf-comments-count-received', function (event, args) {
                $('div[data-sf-thread-key="' + args.key + '"]').each(self.populateCommentsCountTextCallBack(args.count));
            });
        }
    };

    /*
        Widgets creation
    */
    $(function () {
        var serviceUrl = $('[data-sf-role="comments-count-wrapper"]').find('[data-sf-role="service-url"]').val();
        var resources = JSON.parse($('[data-sf-role="comments-count-wrapper"]').find('[data-sf-role="comments-count-resources"]').val());
        (new CommentsCountWidget(serviceUrl, resources)).initialize();
    });
}(jQuery));
