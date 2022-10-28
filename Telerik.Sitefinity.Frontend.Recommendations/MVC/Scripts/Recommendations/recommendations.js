(function () {
    function init() {
        if (window.DataIntelligenceSubmitScript) {
            recommendationFlow();
        }
        else {
            if (window.addEventListener) {
                window.addEventListener('decclientready', recommendationFlow, false);
            }
            else if (window.attachEvent) {
                window.attachEvent('decclientready', recommendationFlow);
            }
        }
    }

    function recommendationFlow() {
        var recommendationsMetadatas = document.getElementsByClassName("recommendations-metadata");
        for (var i = 0; i < recommendationsMetadatas.length; i++) {
            var currentRecommendationMetadata = recommendationsMetadatas[i];
            var uniqueId = currentRecommendationMetadata.querySelector(".unique-id").value;
            var baseUrl = currentRecommendationMetadata.querySelector(".base-url").value;
            var conversionId = currentRecommendationMetadata.querySelector(".conversion-id").value;
            var maxNumberOfItems = currentRecommendationMetadata.querySelector(".max-number-of-items").value;
            var siteId = currentRecommendationMetadata.querySelector(".site-id").value;

            executeRecommendationFlow(baseUrl, parseInt(conversionId), uniqueId, parseInt(maxNumberOfItems), siteId);
        }
    }

    function executeRecommendationFlow(baseUrl, conversionId, widgetUniqueId, maxNumberOfItems, siteId) {
        if (window.DataIntelligenceSubmitScript) {
            var clientJourney = window.DataIntelligenceSubmitScript._client.recommenderClient.getClientJourney();
            if (clientJourney && clientJourney.length > 0) {
                var data = {
                    conversionId: conversionId,
                    journeyJson: JSON.stringify(clientJourney)
                };

                var url = baseUrl + "/sf/system/Default.GetRecommendations()?sf_site=" + siteId;
                getRecommendations(url, data).then(handleResponseData);
            }
        }

        function handleResponseData(data) {
            var recommendations = data.value;
            var recommendationsWrapper = document.getElementById("recommendations-wrapper-" + widgetUniqueId);
            if (recommendations && recommendations.length > 0) {
                recommendationsWrapper.removeAttribute("hidden");
            }

            for (var i = 0; i < recommendations.length; i++) {
                if (i === maxNumberOfItems) {
                    break;
                }

                var recommendation = recommendations[i];
                var recommendationUrl = window.DataIntelligenceSubmitScript._client.recommenderClient.prepareRecommendationUrl(recommendation.Url, conversionId);
                var recommendationDivElement = document.createElement("div");
                var recommendationTitleElement = document.createElement("a");
                recommendationTitleElement.setAttribute("href", recommendationUrl);
                recommendationTitleElement.innerText = recommendation.Title;
                recommendationTitleElement.classList.add("fs-5");
                recommendationDivElement.appendChild(recommendationTitleElement);

                var recommendationsDiv = recommendationsWrapper.getElementsByClassName("content-recommendations")[0];
                recommendationsDiv.appendChild(recommendationTitleElement);

                window.DataIntelligenceSubmitScript._client.recommenderClient.trackRecommendationShown(recommendation, conversionId);
            }
        }

        function getRecommendations(url, data) {
            var requestInfoObject = {
                method: 'POST',
                mode: 'cors',
                cache: 'no-cache',
                credentials: 'same-origin',
                headers: {
                    'Content-Type': 'application/json'
                },
                redirect: 'follow',
                referrerPolicy: 'no-referrer',
                body: JSON.stringify(data)
            };
            var responseJson = fetch(url, requestInfoObject).then(function handleResponse(response) {
                return response.json();
            });

            return responseJson;
        }
    }

    document.addEventListener("DOMContentLoaded", init);
})();
