(function () {
    document.addEventListener('DOMContentLoaded', function () {
        document.querySelector('[data-sf-role="playVideo"]').addEventListener('play', function (e) {
            var video = e.currentTarget;
            var videoSrc = video.getAttribute('src');
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
}());