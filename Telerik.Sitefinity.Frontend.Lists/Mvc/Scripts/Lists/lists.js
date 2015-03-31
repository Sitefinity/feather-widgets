(function ($) {
    $(function () {
        $('[data-sf-role=toggleLink]').on('click', function () {
            var link = $(this);

            expandElement(link);

            var wrapper = link.closest('[data-sf-role=lists]');

            var linkCount = wrapper.find('[data-sf-role=toggleLink]').length;
            var expandedLinkCount = wrapper.find('[data-sf-role=toggleLink].expanded').length;

            if (linkCount === expandedLinkCount) {
                hideExpandAllLink(wrapper);
            }
            else {
                hideCollapseAllLink(wrapper);
            }
        });

        $('[data-sf-role=expandAll]').on('click', function () {
            var wrapper = $(this).closest('[data-sf-role=lists]');
            wrapper.find('[data-sf-role=expandAll]').css('display', 'none');
            wrapper.find('[data-sf-role=collapseAll]').css('display', 'block');
            var links = wrapper.find('[data-sf-role=toggleLink]');
            links.addClass('expanded');
            links.next('div').css('display', 'block');
        });

        $('[data-sf-role=collapseAll]').on('click', function () {
            var wrapper = $(this).closest('[data-sf-role=lists]');
            wrapper.find('[data-sf-role=expandAll]').css('display', 'block');
            wrapper.find('[data-sf-role=collapseAll]').css('display', 'none');
            var links = wrapper.find('[data-sf-role=toggleLink]');
            links.removeClass('expanded');
            links.next('div').css('display', 'none');
        });

        function expandElement(link) {
            if (link.hasClass('expanded'))
                link.removeClass('expanded');
            else
                link.addClass('expanded');

            var content = link.next();
            if (content.css('display') === 'none')
                content.css('display', 'block');
            else
                content.css('display', 'none');
        };

        function hideExpandAllLink(wrapper) {
            wrapper.find('[data-sf-role=expandAll]').css('display', 'none');
            wrapper.find('[data-sf-role=collapseAll]').css('display', 'block');
        };

        function hideCollapseAllLink(wrapper) {
            wrapper.find('[data-sf-role=expandAll]').css('display', 'block');
            wrapper.find('[data-sf-role=collapseAll]').css('display', 'none');
        };
    });
}(jQuery));