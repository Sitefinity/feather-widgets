(function () {
    document.addEventListener('DOMContentLoaded', function () {
        var loginoutBtn = document.querySelector('[data-sf-role="login-status-button"]');
        if (loginoutBtn) {
            loginoutBtn.addEventListener("click", function () {
                if (document.querySelector('[data-sf-role="sf-allow-windows-sts-login"]').value.toLowerCase() === 'true') {
                    location.href = '?stsLogin=true';
                } else {
                    location.href = document.querySelector('[data-sf-role="sf-login-redirect-url"]').value || '#';
                }
                return false;
            });
        }

        if (document.querySelector('[data-sf-role="sf-is-design-mode-value"]').value.toLowerCase() !== 'true') {
            var xhr = new XMLHttpRequest();
            xhr.open('GET', document.querySelector('[data-sf-role="sf-status-json-endpoint-url"]').value);
            xhr.onload = function () {
                if (xhr.status === 200) {
                    var statusViewModel = JSON.parse(xhr.responseText);
                    if (statusViewModel && statusViewModel.IsLoggedIn) {
                        var loggedInView = document.querySelector('[data-sf-role="sf-logged-in-view"]');
                        var avatar = loggedInView.querySelector('[data-sf-role="sf-logged-in-avatar"]');
                        if (avatar) {
                            avatar.setAttribute('src', statusViewModel.AvatarImageUrl);
                            avatar.setAttribute('alt', statusViewModel.DisplayName);
                        }

                        loggedInView.querySelector('[data-sf-role="sf-logged-in-name"]').innerHTML = statusViewModel.DisplayName;
                        var emailContainer = loggedInView.querySelector('[data-sf-role="sf-logged-in-email"]');
                        if (emailContainer) {
                            emailContainer.innerHTML = statusViewModel.Email;
                        }

                        loggedInView.style.display = "block";
                    }
                    else {
                        document.querySelector('[data-sf-role="sf-logged-out-view"]').style.display = "block";
                    }
                }
            };
            xhr.setRequestHeader('Cache-Control', 'no-cache, no-store, must-revalidate');
            xhr.setRequestHeader('Pragma', 'no-cache');
            xhr.setRequestHeader('Expires', '0');
            xhr.send();
        }
        else {
            document.querySelector('[data-sf-role="sf-logged-out-view"]').style.display = "block";
        }
    });
}());