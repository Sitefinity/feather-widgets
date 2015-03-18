module.exports = function (config) {
    config.set({
        basePath: '../',

        files: [
	              'helpers/jquery.min.js',
	              'helpers/angular.js',
                  'helpers/angular-route.js',
                  'helpers/angular-resource.js',
                  'helpers/angular-mocks.js',
                  'helpers/kendo.all.min.js',
                  'helpers/angular-kendo.js',
                  'helpers/Telerik.Sitefinity.Frontend/Mvc/Scripts/Bootstrap/js/ui-bootstrap-tpls.min.js',

                  'helpers/Telerik.Sitefinity.Frontend/Designers/Scripts/*.js',
	              'helpers/Telerik.Sitefinity.Frontend/MVC/Scripts/Designer/*.js',
	              'helpers/Telerik.Sitefinity.Frontend/MVC/Scripts/*.js',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-services.js',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-selectors.js',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-bubbles-selection.html',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-list-group-selection.html',
	              'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-list-selector.js',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-list-selector.html',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-items-tree.js',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-items-tree.html',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/dynamic-modules/sf-data-service.js',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/dynamic-modules/sf-dynamic-items-selector.js',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/dynamic-modules/sf-dynamic-items-selector.html',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/news/sf-news-item-service.js',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/news/sf-news-selector.js',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/news/sf-news-selector.html',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/date-time/sf-timespan-selector.js',
	              'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/date-time/sf-timespan-selector.html',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/pages/sf-page-service.js',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/pages/sf-page-selector.js',
                  'helpers/Telerik.Sitefinity.Frontend/client-components/selectors/pages/sf-page-selector.html',
                  'helpers/mocks/*.js',
                  'unit/**'
        ],

        //exclude: [
        //      '../../Telerik.Sitefinity.Frontend/Mvc/Scripts/Angular/*.min.js',
	    //      '../../Telerik.Sitefinity.Frontend/Mvc/Scripts/Angular/angular-loader.js',
	    //      '../../Telerik.Sitefinity.Frontend/Mvc/Scripts/Angular/angular-scenario.js',
        //      '../../Telerik.Sitefinity.Frontend/Designers/Scripts/page-editor.js'
        //],

        //preprocessors: {
        //    '../../Telerik.Sitefinity.Frontend/Designers/Scripts/*.js': 'coverage',
        //    '../../Telerik.Sitefinity.Frontend/Mvc/Scripts/*.js': 'coverage',
        //    '../../Telerik.Sitefinity.Frontend/Mvc/Scripts/Designer/*.js': 'coverage',
        //    '../../Telerik.Sitefinity.Frontend/client-components/selectors/**/*.html': ['ng-html2js']
        //},

        //Converts directive's external html templates into javascript strings and stores them in the Angular's $templateCache service.
        ngHtml2JsPreprocessor: {
            // setting this option will create only a single module that contains templates
            // from all the files, so you can load them all with module('template')
            moduleName: 'templates',

            // Returns the id of the template in $templateCache. To get a template in a test use id like 'client-components/selectors/common/sf-list-selector.html'
            cacheIdFromPath: function (filepath) {
                // filepath is the path to the template on the disc 
                return filepath.split('Telerik.Sitefinity.Frontend/')[1];
            }
        },

        autoWatch: true,

        singleRun: true,

        frameworks: ['jasmine'],

        browsers: ['PhantomJS'],

        plugins: [
            'karma-junit-reporter',
            'karma-chrome-launcher',
            'karma-firefox-launcher',
            'karma-script-launcher',
            'karma-phantomjs-launcher',
            'karma-jasmine',
            'karma-coverage',
            'karma-ng-html2js-preprocessor'
        ],

        reporters: ['progress', 'junit', 'coverage'],

        junitReporter: {
            outputFile: 'test-results.xml'
        },

        coverageReporter: {
            type: 'html',
            dir: 'coverage/',
            file: 'coverage.xml'
        }
    });
};
