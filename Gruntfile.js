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
					'!Tests/**/*.js',
					'!Telerik.Sitefinity.Frontend.SocialShare/gruntfile.js'
			]
		},
		
		jasmine: {	
			newsTests:{
				src: [
					'Telerik.Sitefinity.Frontend.News/MVC/Scripts/**/*.js'
				],
				options: {
					vendor:[
					'../feather/Tests/Telerik.Sitefinity.Frontend.ClientTest/helpers/jquery-1.8.3.min.js',
					'../feather/Tests/Telerik.Sitefinity.Frontend.ClientTest/helpers/angular.js',
					'../feather/Tests/Telerik.Sitefinity.Frontend.ClientTest/helpers/angular-resource.js',
					'../feather/Tests/Telerik.Sitefinity.Frontend.ClientTest/helpers/angular-route.js',
					'../feather/Tests/Telerik.Sitefinity.Frontend.ClientTest/helpers/angular-mocks.js',
					'../feather/Tests/Telerik.Sitefinity.Frontend.ClientTest/helpers/kendo.web.min.js',
					'../feather/Tests/Telerik.Sitefinity.Frontend.ClientTest/helpers/angular-kendo.js',
					'../feather/Telerik.Sitefinity.Frontend/MVC/Scripts/Bootstrap/js/*.js',
					'!../feather/Telerik.Sitefinity.Frontend/Mvc/Scripts/Angular/**',
					'../feather/Tests/Telerik.Sitefinity.Frontend.ClientTest/templates.js',
					'../feather/Telerik.Sitefinity.Frontend/Designers/Scripts/*.js',
					'../feather/Telerik.Sitefinity.Frontend/MVC/Scripts/Designer/*.js',
					'../feather/Telerik.Sitefinity.Frontend/MVC/Scripts/*.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-services.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-selectors.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-list-selector.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-items-tree.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/news/sf-news-item-service.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/news/sf-news-selector.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/dynamic-modules/sf-data-service.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/dynamic-modules/sf-dynamic-items-selector.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/taxonomies/sf-flat-taxon-service.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/taxonomies/sf-taxon-selector.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/date-time/sf-timespan-selector.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/pages/sf-page-service.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/pages/sf-page-selector.js',
					'!../feather/Telerik.Sitefinity.Frontend/Designers/Scripts/page-editor.js',
					'Tests/FeatherWidgets.ClientTest/helpers/mocks/*.js'
					],
					errorReporting: true,
					specs: ['Tests/FeatherWidgets.ClientTest/unit/News/*.js'],
					junit: {
						path: 'Tests/FeatherWidgets.ClientTest/TestResults/News'
						},
					template: require('grunt-template-jasmine-istanbul'),
					templateOptions: {
						coverage: 'Tests/FeatherWidgets.ClientTest/coverage/News/coverage.json',
						report: [
							{type: 'html', options: {dir: 'Tests/FeatherWidgets.ClientTest/coverage/News'}},
							{type: 'cobertura', options: {dir: 'Tests/FeatherWidgets.ClientTest/coverage/cobertura/News'}},
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
			  src: ['../feather/Telerik.Sitefinity.Frontend/client-components/selectors/**/*.html'],
			  dest: '../feather/Tests/Telerik.Sitefinity.Frontend.ClientTest/templates.js'
			},
		},
	});
	
	//Load the needed plugins
	grunt.loadNpmTasks('grunt-contrib-jshint');
	grunt.loadNpmTasks('grunt-contrib-jasmine');
	grunt.loadNpmTasks('grunt-html2js');
	
	//Default task(s)
	grunt.registerTask('default', ['jshint','html2js','jasmine']);
	
};