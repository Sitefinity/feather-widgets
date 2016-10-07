(function ($) {
    $(function () {
        $('[data-sf-role=playVideo]').on('play', function () {
            var video = $(this);
            var videoSrc = video.attr('src');
            sendSentence(videoSrc);
        });

        function sendSentence(itemSrc) {
            if (window.DataIntelligenceSubmitScript) {
                DataIntelligenceSubmitScript._client.sentenceClient.writeSentence({
                    predicate: "Play video",
                    object: itemSrc,
                    objectMetadata: [{
                        'K': 'PageTitle',
                        'V': document.title
                    },
                    {
                        'K': 'PageUrl',
                        'V': location.href
                    }
                    ]
                });
            }
        }
    });
}(jQuery));