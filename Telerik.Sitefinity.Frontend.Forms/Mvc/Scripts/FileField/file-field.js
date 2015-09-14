(function ($) {
    var htmlDecode = function(html) {
        var a = document.createElement('a'); a.innerHTML = html;
        return a.textContent;
    };

    var adjustVisibility = function (container) {
        var allRemoveLinks = container.find('[data-sf-role="remove-input"]');
        allRemoveLinks.toggle(allRemoveLinks.length > 1);
    };

    var initInput = function (template, container, config) {
        var input = $(template);
        input.appendTo(container);

        if (config.AllowMultipleFiles) {
            adjustVisibility(container);

            input.find('[data-sf-role="remove-input"]').click(function () {
                input.remove();
                adjustVisibility(container);
            });
        }
    };

    var init = function (element) {
        var jElement = $(element);
        var config = JSON.parse(htmlDecode(jElement.attr('data-sf-config')));

        var inputContainer = jElement.find('[data-sf-role="file-field-inputs"]');
        var inputTemplate = jElement.find('[data-sf-role="file-input-template"]').html();
        initInput(inputTemplate, inputContainer, config);

        if (config.AllowMultipleFiles) {
            $(jElement).find('[data-sf-role="add-input"]').click(function () {
                initInput(inputTemplate, inputContainer, config);
            });
        }
    };

    $(function () {
        var containers = $('[data-sf-role="file-field-container"]');
        if (!containers || containers.length < 1)
            return;

        for (var i = 0; i < containers.length; i++) {
            init(containers[i]);
        }
    });
}(jQuery));