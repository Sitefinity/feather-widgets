; (function ($) {
    'use strict';

    var Rating = function (element, options) {
        this.ratingContainer = element;
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
            return this.settings.value;
        },

        setValue: function (newValue) {
            this.settings.value = newValue;
            $.proxy(this.elementsRenderer.reset, this)();
        },

        getBaseElementMarkup: function () {
            var templateElement = $(this.settings.template);
            if (!(templateElement && templateElement.length)) {
                templateElement = $('<div><span>&#9734</span></div>');
            }

            var markup = templateElement.html();

            return markup;
        },

        getSelectedElementMarkup: function () {
            var markup = this.getBaseElementMarkup();
            $(markup).attr('class', this.settings.selectedClass);

            return markup;
        },

        //re-renderer which shows which stars are active, hovered and so on.
        elementsRenderer: {
            fill: function (e) { // fill to the current mouse position.
                var elements = this.ratingContainer.children();
                var index = elements.index(e.srcElement) + 1;
                elements.slice(0, index).addClass(this.settings.selectedClass);
            },
            hover: function (e) { // fill to the current mouse position with hover class.
                var elements = this.ratingContainer.children();
                elements.removeClass(this.settings.hoverClass);

                var index = elements.index(e.srcElement) + 1;
                elements.slice(0, index).addClass(this.settings.hoverClass);
            },
            drainHover: function () { // drain hovering of the elements.
                this.ratingContainer.children().removeClass(this.settings.hoverClass);
            },
            reset: function () { // resets the element to the selected value.
                var elements = this.ratingContainer.children();
                elements.removeClass(this.settings.selectedClass).removeClass(this.settings.hoverClass);
                elements.slice(0, this.settings.value / this.settings.step).addClass(this.settings.selectedClass);
            }
        },

        selectElement: function (e) {
            var elements = this.ratingContainer.children();
            this.settings.value = (elements.index(e.srcElement) + 1) * this.settings.step;
            $.proxy(this.elementsRenderer.reset, this)();
        },

        attachEvents: function () {
            var elements = this.ratingContainer.children();
            elements.bind('click', $.proxy(this.selectElement, this));
            elements.bind('mouseover', $.proxy(this.elementsRenderer.hover, this));
            elements.bind('mouseout', $.proxy(this.elementsRenderer.drainHover, this));
            elements.bind('focus', $.proxy(this.elementsRenderer.hover, this));
            elements.bind('blur', $.proxy(this.elementsRenderer.reset, this));
        },

        render: function () {
            this.ratingContainer.empty();

            var baseElementTemplate = this.getBaseElementMarkup();
            var selectedElementTemplate = this.getSelectedElementMarkup();

            for (var i = 0; i < this.settings.value; i += this.settings.step) {
                this.ratingContainer.append(selectedElementTemplate);
            }
            for (var j = this.settings.value; j < this.settings.maxValue; j += this.settings.step) {
                this.ratingContainer.append(baseElementTemplate);
            }

            if (!this.settings.readOnly) {
                this.attachEvents();
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
