(function () {
    var init = function () {
        var containers = document.querySelectorAll('[data-sf-role="ai-results"]');

        for (var i = 0; i < containers.length; i++) {
            initSearchResults(containers[i]);
        }
    };

    function initSearchResults(container) {
        var configEl = container.querySelector('[data-sf-role="ai-results-data"]');
        if (!configEl) return;

        var config;
        try {
            config = JSON.parse(configEl.textContent || configEl.innerHTML);
        } catch (e) {
            return;
        }

        var pageSize = config.pageSize || 20;
        var resultItems = container.querySelectorAll('[data-sf-role="result-item"]');
        var paginationEl = container.querySelector('[data-sf-role="pagination"]');
        var pagerContainer = container.querySelector('[data-sf-role="pager-container"]');
        var pagerSummary = container.querySelector('[data-sf-role="pager-summary"]');
        var totalCount = resultItems.length;
        var totalPages = Math.ceil(totalCount / pageSize);

        var currentPage = getCurrentPageFromUrl();

        renderPage(currentPage);

        function getCurrentPageFromUrl() {
            var params = new URLSearchParams(window.location.search);
            var page = parseInt(params.get('page'), 10);
            return (page && page > 0) ? page : 1;
        }

        function renderPage(page) {
            if (page < 1) page = 1;
            if (page > totalPages && totalPages > 0) page = totalPages;

            var start = (page - 1) * pageSize;
            var end = start + pageSize;

            for (var i = 0; i < resultItems.length; i++) {
                resultItems[i].style.display = (i >= start && i < end) ? '' : 'none';
            }

            renderPager(page);
            renderSummary(page, start, end);
        }

        function renderPager(currentPage) {
            if (!paginationEl || totalPages <= 1) return;

            paginationEl.innerHTML = '';

            if (pagerContainer) pagerContainer.style.display = '';

            var displayPagesCount = 10;
            var startPage = 1;
            if (currentPage > displayPagesCount) {
                startPage = (Math.floor((currentPage - 1) / displayPagesCount) * displayPagesCount) + 1;
            }
            var endPage = Math.min(totalPages, startPage + displayPagesCount - 1);
            var isPreviousVisible = startPage > displayPagesCount;
            var isNextVisible = endPage < totalPages;

            if (isPreviousVisible) {
                var prevLi = document.createElement('li');
                prevLi.className = 'page-item';
                var prevA = document.createElement('a');
                prevA.className = 'page-link';
                prevA.setAttribute('aria-label', 'Previous');
                prevA.href = buildPageUrl(startPage - 1);
                prevA.innerHTML = '&laquo;';
                prevA.addEventListener('click', makePageClickHandler(startPage - 1));
                prevLi.appendChild(prevA);
                paginationEl.appendChild(prevLi);
            }

            for (var p = startPage; p <= endPage; p++) {
                var li = document.createElement('li');
                var href = buildPageUrl(p);
                var a = document.createElement('a');
                a.className = 'page-link';
                a.href = href;
                a.textContent = p;
                a.addEventListener('click', makePageClickHandler(p));

                if (p === currentPage) {
                    li.className = 'active page-item';
                    a.setAttribute('aria-current', 'true');
                    a.setAttribute('aria-label', 'Page ' + p);
                } else {
                    li.className = 'page-item';
                    a.setAttribute('aria-label', 'Go to page ' + p);
                }

                li.appendChild(a);
                paginationEl.appendChild(li);
            }

            if (isNextVisible) {
                var nextLi = document.createElement('li');
                nextLi.className = 'page-item';
                var nextA = document.createElement('a');
                nextA.className = 'page-link';
                nextA.setAttribute('aria-label', 'Next');
                nextA.href = buildPageUrl(endPage + 1);
                nextA.innerHTML = '&raquo;';
                nextA.addEventListener('click', makePageClickHandler(endPage + 1));
                nextLi.appendChild(nextA);
                paginationEl.appendChild(nextLi);
            }
        }

        function renderSummary(page, start, end) {
            if (!pagerSummary) return;

            if (totalCount === 0) {
                pagerSummary.textContent = '';
                return;
            }

            var displayEnd = Math.min(end, totalCount);
            pagerSummary.textContent = (start + 1) + ' - ' + displayEnd;
        }

        function makePageClickHandler(page) {
            return function (e) {
                e.preventDefault();
                navigateToPage(page);
            };
        }

        function navigateToPage(page) {
            var params = new URLSearchParams(window.location.search);
            params.set('page', page);
            var newUrl = window.location.pathname + '?' + params.toString();
            history.pushState({ page: page }, '', newUrl);
            renderPage(page);
            scrollToContainer();
        }

        function buildPageUrl(page) {
            var params = new URLSearchParams(window.location.search);
            params.set('page', page);
            return window.location.pathname + '?' + params.toString();
        }

        function scrollToContainer() {
            if (container && container.scrollIntoView) {
                container.scrollIntoView({ behavior: 'smooth', block: 'start' });
            }
        }

        window.addEventListener('popstate', function (e) {
            var page = (e.state && e.state.page) ? e.state.page : getCurrentPageFromUrl();
            renderPage(page);
        });
    }

    document.addEventListener('DOMContentLoaded', function () {
        init();
    });

    document.addEventListener('widgetLoaded', function () {
        init();
    });
}());
