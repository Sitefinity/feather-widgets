module.exports = function (grunt) {
    'use strict';
	
	//Project Configuration
	grunt.initConfig({
		
		pkg: grunt.file.readJSON('package.json'),
		
		jshint: {
			//define the files to lint
			files: ['gruntfile.js',
					'**/*.js',
					'!node_modules/**/*.js',
					'!Tests/**/*.js'
			]
		},
		
		jasmine: {
			unit:{
				src: [
					'**/*.js',
					'!node_modules/**/*.js',
					'!Tests/**/*.js'
				],
				options: {
					vendor:[
					'Tests/FeatherWidgets.ClientTest/helpers/jquery-1.8.3.min.js',
					'Tests/FeatherWidgets.ClientTest/helpers/angular.js',
					'Tests/FeatherWidgets.ClientTest/helpers/angular-resource.js',
					'Tests/FeatherWidgets.ClientTesthelpers/angular-route.js',
					'Tests/FeatherWidgets.ClientTest/helpers/angular-mocks.js',
					'Tests/FeatherWidgets.ClientTest/helpers/kendo.web.min.js',
					'Tests/FeatherWidgets.ClientTest/helpers/angular-kendo.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/MVC/Scripts/Bootstrap/js/*.js',
					'Tests/FeatherWidgets.ClientTest/templates.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/Designers/Scripts/*.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/MVC/Scripts/Designer/*.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/MVC/Scripts/*.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-services.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-selectors.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-list-selector.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-items-tree.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/news/sf-news-item-service.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/news/sf-news-selector.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/dynamic-modules/sf-data-service.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/dynamic-modules/sf-dynamic-items-selector.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/taxonomies/sf-flat-taxon-service.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/taxonomies/sf-taxon-selector.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/date-time/sf-timespan-selector.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/pages/sf-page-service.js',
					'Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/pages/sf-page-selector.js',
					'Tests/FeatherWidgets.ClientTest/helpers/mocks/*.js'
					],
					specs: ['Tests/FeatherWidgets.ClientTest/unit/**/*.js'],
					junit: {
						path: 'Tests/FeatherWidgets.ClientTest/TestResults'
						},
					template: require('grunt-template-jasmine-istanbul'),
					templateOptions: {
						coverage: 'Tests/FeatherWidgets.ClientTest/coverage/coverage.json',
						report: [
							{type: 'html', options: {dir: 'Tests/FeatherWidgets.ClientTest/coverage'}},
							{type: 'cobertura', options: {dir: 'Tests/FeatherWidgets.ClientTest/coverage/cobertura'}},
							{type: 'text-summary'}
						]
					}
				}
			}
		},
		
		//Converts directive's external html templates into javascript strings and stores them in the Angular's $templateCache service.
		html2js: {
			options: {
			  singleModule: true,
			  module: 'templates',
			  base: 'Telerik.Sitefinity.Frontend'
			},
			main: {
			  src: ['Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/**/*.html'],
			  dest: 'Tests/FeatherWidgets.ClientTest/templates.js'
			},
		},
	});
	
	//Load the needed plugins
	grunt.loadNpmTasks('grunt-contrib-jshint');
	grunt.loadNpmTasks('grunt-contrib-jasmine');
	grunt.loadNpmTasks('grunt-html2js');
	
	//Default task(s)
	grunt.registerTask('default', ['jshint','html2js', 'jasmine']);
	
};