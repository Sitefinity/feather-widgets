(function () {
    document.addEventListener('DOMContentLoaded', function () {
        var sendAgainLinks = document.querySelectorAll("[data-sf-role='sendAgainLink']");
            sendAgainLinks.forEach(function (sendAgainLink) {
                sendAgainLink.addEventListener('click', function () {
                    var request = new XMLHttpRequest();
                    request.onreadystatechange = function () {
                        if (request.readyState === XMLHttpRequest.DONE && request.status === 200) {
                            if (JSON.parse(request.response)) {
                                document.querySelector("[data-sf-role='confirmationResendInfo']").style.display = 'block';
                            }
                        }
                    };

                    var url = document.querySelector("[data-sf-role='sf-resend-confirmation-endpoint-url']").value;
                    request.open('GET', url);
                    request.send();
                });
            });
    });
}()); 