(function ($) {
    var findSubmitButtonDock = function (wrapperDockingZones) {
        if (wrapperDockingZones && wrapperDockingZones.length) {
            var allDocks = [];

            wrapperDockingZones.forEach(function (dockingZoneId) {
                allDocks.push.apply(allDocks, $find(dockingZoneId).get_docks());
            });

            var submitButtonDocks = allDocks.filter(function (dock) {
                var dockProperties = JSON.parse($(dock.get_element()).attr('parameters') || null);
                if (dockProperties && dockProperties.length) {
                    return dockProperties.some(function (prop) {
                        return prop.Key === 'ControllerName' && prop.Value === 'Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.SubmitButtonController'
                    });
                }
            });

            return submitButtonDocks[0];
        }
    };

    $(function () {
        $(document).on('sf-zone-editor-item-dropped', function (e) {
            if (e.args.ControlType === 'Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy') {
                var droppedMvcFields = $('#RadDockZoneBody').children('[behaviourobjecttype]');
                if (droppedMvcFields.length) {
                    var hasSubmitButton = Array.prototype.some.call(droppedMvcFields, function (el) {
                        return el.attributes.behaviourobjecttype.value === 'Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.SubmitButtonController';
                    });

                    if (!hasSubmitButton) {
                        var submitButtonDock = findSubmitButtonDock(e.sender.get_toolboxDockingZones());
                        e.sender._dropSubmitButton(submitButtonDock, e.args.DockId);
                    }
                }
            }
        });
    });
}(jQuery));