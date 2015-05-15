; (function ($) {
    'use strict';

    var Rating = function (element, options) {
        this.ratingContainer = element;
        this.settings = {
            maxValue: 5,   // max number of stars
            value: 0,    // number of selected stars
            step: 1,  //setting for selecting stars. Could be floating number.
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
            this.elementsRenderer(this).reset();
        },

        getBaseElementMarkup: function () {
            var templateElement = $(this.settings.template);
            if (!(templateElement && templateElement.length)) {
                templateElement = $('<div><span>&#9734</span></div>');
            }

            var markup = templateElement.html();

            return markup;
        },

        isValidCssDefinition: function (cssClass) {
            return cssClass.hasOwnProperty('from') && cssClass.hasOwnProperty('to') && cssClass.hasOwnProperty('css');
        },

        getCssClass: function (value, cssClasses) {
            if ($.isArray(cssClasses)) {
                for (var i in cssClasses) {
                    if (this.isValidCssDefinition(cssClasses[i]) && cssClasses[i].from < value && cssClasses[i].to >= value) {
                        return cssClasses[i].css;
                    }
                }
            }
            else {
                return cssClasses;
            }
        },

        getSelectedCssClass: function (value) {
            return this.getCssClass(value, this.settings.selectedClass);
        },

        getSelectedHoverClass: function (value) {
            return this.getCssClass(value, this.settings.hoverClass);
        },

        //re-renderer which shows which stars are active, hovered and so on.
        elementsRenderer: function (ratingControl) {
            var removeClasses = function (cssClasses) {
                var elements = ratingControl.ratingContainer.children();
                if ($.isArray(cssClasses)) {
                    for (var i in cssClasses) {
                        if (ratingControl.isValidCssDefinition(cssClasses[i])) {
                            elements.removeClass(cssClasses[i].css);
                        }
                    }
                }
                else {
                    return elements.removeClass(cssClasses);
                }
            };

            var addClasses = function (value, cssClasses) {
                var elements = ratingControl.ratingContainer.children();
                var valueIntPart = parseInt(value);
                var valueDecimalReminder = value - valueIntPart;
                var filledElementsCount = valueIntPart / ratingControl.settings.step;
                elements.slice(0, filledElementsCount).addClass(ratingControl.getCssClass(1, cssClasses));
                if (valueDecimalReminder !== 0) {
                    elements.slice(filledElementsCount, value / ratingControl.settings.step).addClass(ratingControl.getCssClass(valueDecimalReminder, cssClasses));
                }
            };

            var fill = function (e) { // fill to the current mouse position.
                var index = ratingControl.ratingContainer.children().index(e.srcElement) + 1;
                addClasses(index * ratingControl.settings.step, ratingControl.settings.selectedClass);
            };

            var hover = function (e) { // fill to the current mouse position with hover class.
                removeClasses(ratingControl.settings.hoverClass);

                var index = ratingControl.ratingContainer.children().index(e.srcElement) + 1;
                addClasses(index * ratingControl.settings.step, ratingControl.settings.hoverClass);
            };

            var drainHover = function () { // drain hovering of the elements.
                removeClasses(ratingControl.settings.hoverClass);
            };

            var reset = function () { // resets the element to the selected value.
                removeClasses(ratingControl.settings.selectedClass);
                removeClasses(ratingControl.settings.hoverClass);

                addClasses(ratingControl.settings.value, ratingControl.settings.selectedClass);
            };

            return {
                fill: fill,
                hover: hover,
                drainHover: drainHover,
                reset: reset
            };
        },

        selectElement: function (e) {
            var elements = this.ratingContainer.children();
            this.settings.value = (elements.index(e.srcElement) + 1) * this.settings.step;
            this.elementsRenderer(this).reset();
        },

        attachEvents: function () {
            var elements = this.ratingContainer.children();
            elements.on('click', $.proxy(this.selectElement, this));
            elements.on('mouseover', this.elementsRenderer(this).hover);
            elements.on('mouseout', this.elementsRenderer(this).drainHover);
            elements.on('focus', this.elementsRenderer(this).hover);
            elements.on('blur', this.elementsRenderer(this).reset);
        },

        render: function () {
            this.ratingContainer.empty();

            var baseElementTemplate = this.getBaseElementMarkup();

            for (var i = 0; i < this.settings.maxValue; i += this.settings.step) {
                this.ratingContainer.append(baseElementTemplate);
            }

            this.elementsRenderer(this).reset();

            if (!this.settings.readOnly) {
                this.attachEvents();
            }
        }

    };

    $.fn.extend({
        mvcRating: function (settings) {
            var fRating = new Rating($(this), settings);
            fRating.render();

            return fRating;
        },
    });

}(jQuery));
