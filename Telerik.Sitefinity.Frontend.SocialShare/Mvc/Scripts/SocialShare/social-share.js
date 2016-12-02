function sendSentenceToDec(shareOption) {
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

function sendSentenceToDecWithObj(socialShare) {
	var socialShareOption = socialShare.attr('data-sf-socialshareoption');
	sendSentenceToDec(socialShareOption);
}

function googleShareCallback(data) {
	var socialShare = $(this.iK.closest('[data-sf-role=socialShare]'));
	var socialShareOption = socialShare.attr('data-sf-socialshareoption');
	sendSentenceToDecWithObj(socialShare);
}

(function ($) {
    $(function () {
        $('[data-sf-role=socialShare]').on('click', function () {
            var socialShare = $(this);
            sendSentenceToDecWithObj(socialShare);
        });
    });
}(jQuery));