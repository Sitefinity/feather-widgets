(function () {

    /* Polyfills */

    if (window.NodeList && !NodeList.prototype.forEach) {
        NodeList.prototype.forEach = Array.prototype.forEach;
    }

    if (!Element.prototype.matches) {
        Element.prototype.matches = Element.prototype.msMatchesSelector ||
            Element.prototype.webkitMatchesSelector;
    }

    if (!Element.prototype.closest) {
        Element.prototype.closest = function (s) {
            var el = this;

            do {
                if (el.matches(s)) return el;
                el = el.parentElement || el.parentNode;
            } while (el !== null && el.nodeType === 1);
            return null;
        };
    }

    /* Polyfills end */

    document.addEventListener('DOMContentLoaded', function () {
        document.querySelectorAll('[data-sf-role="toggleLink"]').forEach(function (x) {
            x.addEventListener('click', function () {
                var link = this;

                expandElement(link);

                var wrapper = link.closest('[data-sf-role="lists"]');

                var linkCount = wrapper.querySelectorAll('[data-sf-role="toggleLink"]').length;
                var expandedLinkCount = wrapper.querySelectorAll('[data-sf-role="toggleLink"].expanded').length;

                if (linkCount === expandedLinkCount) {
                    hideExpandAllLink(wrapper);
                }
                else {
                    hideCollapseAllLink(wrapper);
                }
            });
        });

        document.querySelectorAll('[data-sf-role="expandAll"]').forEach(function (x) {
            x.addEventListener('click', function () {
                var wrapper = this.closest('[data-sf-role="lists"]');
                wrapper.querySelector('[data-sf-role="expandAll"]').style.display = 'none';
                wrapper.querySelector('[data-sf-role="collapseAll"]').style.display = 'block';
                var links = wrapper.querySelectorAll('[data-sf-role="toggleLink"]');
                links.forEach(function (link) {
                    link.classList.add('expanded');
                    toggleLinkArrow(link);

                    var nextSiblingDiv = link.nextElementSibling;
                    if (nextSiblingDiv.tagName.toLowerCase() === 'div') {
                        nextSiblingDiv.style.display = 'block';
                    }
                });
            });
        });

        document.querySelectorAll('[data-sf-role="collapseAll"]').forEach(function (x) {
            x.addEventListener('click', function () {
                var wrapper = this.closest('[data-sf-role="lists"]');
                wrapper.querySelector('[data-sf-role="expandAll"]').style.display = 'block';
                wrapper.querySelector('[data-sf-role="collapseAll"]').style.display = 'none';
                var links = wrapper.querySelectorAll('[data-sf-role="toggleLink"]');
                links.forEach(function (link) {
                    link.classList.remove('expanded');
                    toggleLinkArrow(link);
                    var nextSiblingDiv = link.nextElementSibling;
                    if (nextSiblingDiv.tagName.toLowerCase() === 'div') {
                        nextSiblingDiv.style.display = 'none';
                    }
                });
            });
        });
    });

    function expandElement(link) {
        if (link.classList.contains('expanded')) {
            link.classList.remove('expanded');
        } else {
            link.classList.add('expanded');
            var itemTitle = link.innerText.trim();
            sendSentence(itemTitle);
        }

        var content = link.nextElementSibling;
        if (content.style.display === 'none') {
            content.style.display = 'block';
        } else {
            content.style.display = 'none';
        }
        toggleLinkArrow(link);
    }

    function toggleLinkArrow(link) {
        var isExpanded = link.classList.contains("expanded");
        if (link.querySelectorAll("[data-sf-toggle='collapsed']")) {
            link.querySelectorAll("[data-sf-toggle='collapsed']").forEach(function (e) { e.style.display = isExpanded ? "none" : ""; });
        }
        if (link.querySelectorAll("[data-sf-toggle='expanded']")) {
            link.querySelectorAll("[data-sf-toggle='expanded']").forEach(function (e) { e.style.display = isExpanded ? "" : "none"; });
        }
    }

    function hideExpandAllLink(wrapper) {
        wrapper.querySelectorAll('[data-sf-role="expandAll"]').forEach(function (node) {
            node.style.display = 'none';
        });
        wrapper.querySelectorAll('[data-sf-role="collapseAll"]').forEach(function (node) {
            node.style.display = 'block';
        });
    }

    function hideCollapseAllLink(wrapper) {
        wrapper.querySelectorAll('[data-sf-role="expandAll"]').forEach(function (node) {
            node.style.display = 'block';
        });
        wrapper.querySelectorAll('[data-sf-role="collapseAll"]').forEach(function (node) {
            node.style.display = 'none';
        });
    }

    function sendSentence(itemTitle) {
        if (window.DataIntelligenceSubmitScript) {
            DataIntelligenceSubmitScript._client.sentenceClient.writeSentence({
                predicate: "Expand list",
                object: itemTitle,
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
}());