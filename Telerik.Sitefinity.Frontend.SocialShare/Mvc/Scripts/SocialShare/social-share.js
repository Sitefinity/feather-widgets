(function ($) {
    $(function () {
        $('[data-sf-role=socialShare]').on('click', function () {
            var socialShare = $(this);
            var socialShareOption = socialShare.attr('data-sf-socialshareoption');
            sendSentence(socialShareOption);
        });

        function sendSentence(shareOption) {
            if (window.DataIntelligenceSubmitScript) {
                DataIntelligenceSubmitScript._client.sentenceClient.writeSentence({
                    predicate: "Share on social media",
                    object: shareOption,
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