(function ($) {
    document.setDecTestData = function setDecTestData(predicate) {
        $(document).ajaxSend(function(event, xhr, settings) {
            var data = $.parseJSON(settings.data);
            if($.isArray(data) ) {
                for(var i = 0; i < data.length; i++)
                {
                    processDecData(data[i]);
                }
            }
            else {
                processDecData(data);
            }

            function processDecData(data) {
                if(data.P = predicate) {
                    var object = data.O;
                    $('<input>').attr('type','hidden').attr('value', JSON.stringify(data)).attr('sf-decdata', object).appendTo('body');
                }
            }
        }); 
    }
}(jQuery));