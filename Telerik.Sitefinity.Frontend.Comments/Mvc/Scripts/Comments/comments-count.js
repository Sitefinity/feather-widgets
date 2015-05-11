; (function ($) {
    'use strict';

    /*
        Widget
    */
    var CommentsCountWidget = function (rootUrl, resources) {

        this.getCommentsCounts = function () {
            var threadKeys = this.collectThreadIds();
            var getCountUrl = String.format(rootUrl + '/comments/count?ThreadKey={0}', threadKeys);
            return jQuery.ajax({
                type: 'GET',
                url: getCountUrl,
                contentType: 'application/json',
                cache: false,
                accepts: {
                    text: 'application/json'
                },
                processData: false
            });
        },

        this.collectThreadIds = function () {
            var commmentsCounterControls = jQuery('[data-sf-role="comments-count-wrapper"]');
            var uniqueKeys = {};
            for (var i = 0; i < commmentsCounterControls.length; i++) {
                uniqueKeys[jQuery(commmentsCounterControls[i]).attr('sf-thread-key')] = true;
            }
            var threadKeys = new Array();
            jQuery.each(uniqueKeys, function (key, value) {
                threadKeys.push(key);
            });
            return threadKeys;
        },

        this.setCommentsCounts = function (data) {
            var threadCountList = data;
            for (var i = 0; i < threadCountList.Items.length; i++) {
                var currentThreadKey = threadCountList.Items[i].Key;
                var commmentsCounterControls = jQuery('div[sf-thread-key="' + currentThreadKey + '"]');
                var currentCount = threadCountList.Items[i].Count;

                //format count
                if (currentCount == -1)
                    continue;

                var self = this;
                jQuery.each(commmentsCounterControls, function (index, commentsCounterControl) {
                    var currentCountFormatted = '';
                    if (!currentCount) {
                        currentCountFormatted = self.leaveComment;
                    }
                    else {
                        currentCountFormatted = currentCount;
                    }

                    if (currentCount == 1)
                        currentCountFormatted += ' ' + resources.comment.toLowerCase();
                    else
                        currentCountFormatted += ' ' + self.commentsPlural.toLowerCase();

                    //set the comments count text in the counter control
                    jQuery(commentsCounterControl).html(currentCountFormatted);
                });
            }
        },

        this.initialize = function () {
            var self = this;

            self.getCommentsCounts().then(function (response) {
                if (response) {
                    self.setCommentsCounts(response);
                }
            });
        }
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
