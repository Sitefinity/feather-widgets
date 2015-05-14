; (function ($) {
    'use strict';

    var Rating = function (element, options) {
        this.ratingContainer = element;
        self = this;
        this.settings = {
            maxValue: 5,   // max number of stars
            value: 0,    // number of selected stars
            step: 0.5,  //setting for selecting stars. Could be floating number.
            readOnly: false,
            template: '', //defines template for the separate elements. Inner Html will be rendered as many times as {{maxValue}}
            selectedClass: 'on', //Class applied to the element when it is selected
            hoverClass: 'hover' //Class applied to the element when it is hovered
        };

        if (options) {
            jQuery.extend(this.settings, options);
        }

        return this;
    };

    Rating.prototype = {
        getValue: function () {
            return self.settings.value;
        },

        setValue: function (newValue) {
            self.settings.value = newValue;
            self.elementsRenderer.reset();
        },

        getBaseElementMarkup: function () {
            var templateElement = $(self.settings.template);
            if (!(templateElement && templateElement.length)) {
                templateElement = $('<div><span>&#9734</span></div>');
            }

            var markup = templateElement.html();

            return markup;
        },

        getSelectedElementMarkup: function () {
            var markup = self.getBaseElementMarkup();
            $(markup).attr('class', self.settings.selectedClass);

            return markup;
        },

        //re-renderer which shows which stars are active, hovered and so on.
        elementsRenderer: {
            fill: function (e) { // fill to the current mouse position.
                var elements = self.ratingContainer.children();
                var index = elements.index(e.srcElement) + 1;
                elements.slice(0, index).addClass(self.settings.selectedClass);
            },
            hover: function (e) { // fill to the current mouse position with hover class.
                var elements = self.ratingContainer.children();
                elements.removeClass(self.settings.hoverClass);

                var index = elements.index(e.srcElement) + 1;
                elements.slice(0, index).addClass(self.settings.hoverClass);
            },
            drainHover: function () { // drain hovering of the elements.
                self.ratingContainer.children().removeClass(self.settings.hoverClass);
            },
            reset: function () { // resets the element to the selected value.
                var elements = self.ratingContainer.children();
                elements.removeClass(self.settings.selectedClass).removeClass(self.settings.hoverClass);
                elements.slice(0, self.settings.value / self.settings.step).addClass(self.settings.selectedClass);
            }
        },

        selectElement: function (e) {
            var elements = self.ratingContainer.children();
            self.settings.value = (elements.index(e.srcElement) + 1) * self.settings.step;
            self.elementsRenderer.reset();
        },

        attachEvents: function () {
            var elements = self.ratingContainer.children();
            elements.bind('click', self.selectElement);
            elements.bind('mouseover', self.elementsRenderer.hover);
            elements.bind('mouseout', self.elementsRenderer.drainHover);
            elements.bind('focus', self.elementsRenderer.hover);
            elements.bind('blur', self.elementsRenderer.reset);
        },

        render: function () {
            self.ratingContainer.empty();

            var baseElementTemplate = self.getBaseElementMarkup();
            var selectedElementTemplate = self.getSelectedElementMarkup();

            for (var i = 0; i < self.settings.value; i += self.settings.step) {
                self.ratingContainer.append(selectedElementTemplate);
            }
            for (var j = self.settings.value; j < self.settings.maxValue; j += self.settings.step) {
                self.ratingContainer.append(baseElementTemplate);
            }

            if (!self.settings.readOnly) {
                self.attachEvents();
            }
        }

    };

    $.fn.extend({
        featherRating: function (settings) {
            var fRating = new Rating($(this), settings);
            fRating.render();

            return fRating;
        },
    });

}(jQuery));
