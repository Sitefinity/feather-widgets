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

    var checkRequired = function (container) {
        var violationMessage = $('[data-sf-role="required-violation-message"]');

        var inputs = container.find('input[type="file"]');
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].value) {
                violationMessage.hide();
                return true;
            }
        }

        violationMessage.show();
        return false;
    };

    var checkFileTypes = function (container, fileTypes) {
        var hasViolations = false;
        var inputs = container.find('input[type="file"]');
        for (var i = 0; i < inputs.length; i++) {
            var violationMessage = $(inputs[i]).closest('[data-sf-role="single-file-input"]').find('[data-sf-role="filetype-violation-message"]');
            if (inputs[i].value) {
                var stopIndex = inputs[i].value.lastIndexOf('.');
                if (stopIndex >= 0) {
                    var extension = inputs[i].value.substring(stopIndex).toLowerCase();
                    if (fileTypes.indexOf(extension) < 0) {
                        violationMessage.show();
                        hasViolations = true;
                        continue;
                    }
                }
            }

            violationMessage.hide();
        }

        return !hasViolations;
    };

    var checkFileSizes = function (container, min, max) {
        if (typeof window.File == 'undefined' || typeof window.FileList == 'undefined')
            return true;

        var hasViolations = false;
        var inputs = container.find('input[type="file"]');
        for (var i = 0; i < inputs.length; i++) {
            var violationMessage = $(inputs[i]).closest('[data-sf-role="single-file-input"]').find('[data-sf-role="filesize-violation-message"]');
            if (inputs[i].files.length > 0) {
                var file = inputs[i].files[0];
                if ((min > 0 && file.size < min) || (max > 0 && file.size > max)) {
                    violationMessage.show();
                    hasViolations = true;
                    continue;
                }
            }

            violationMessage.hide();
        }

        return !hasViolations;
    };

    var formValidation = function (ev) {
        var requiredValidationResult = !ev.data.config.IsRequired || checkRequired(ev.data.container);
        var fileTypesValidationResult = ev.data.config.AcceptedFileTypes.length === 0 || checkFileTypes(ev.data.container, ev.data.config.AcceptedFileTypes);
        var fileSizesValidationResult = !(ev.data.config.MinFileSizeInMb || ev.data.config.MaxFileSizeInMb) || checkFileSizes(ev.data.container, ev.data.config.MinFileSizeInMb * 1024 * 1024, ev.data.config.MaxFileSizeInMb * 1024 * 1024);

        return requiredValidationResult && fileTypesValidationResult && fileSizesValidationResult;
    };
    
    var init = function (element) {
        var jElement = $(element);
        var config = JSON.parse(htmlDecode(jElement.attr('data-sf-config')));

        var inputContainer = jElement.find('[data-sf-role="file-field-inputs"]');
        var inputTemplate = jElement.find('[data-sf-role="file-input-template"]').html();
        var form = jElement.closest('form');
        initInput(inputTemplate, inputContainer, config);

        if (config.AllowMultipleFiles) {
            $(jElement).find('[data-sf-role="add-input"]').click(function () {
                initInput(inputTemplate, inputContainer, config);
            });
        }

        jElement.find('input[type="file"]').data('sfvalidator', function () {
            return formValidation({ data: { config: config, container: inputContainer } });
        });

        form.submit({ config: config, container: inputContainer }, formValidation);
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