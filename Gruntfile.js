module.exports = function (grunt) {
    'use strict';
    var fs = require('fs');

    grunt.registerMultiTask("assertMinified", "Asserts if minified files are updated correctly", function() {
        var paths = grunt.file.expand( this.data.paths );
        var output = this.data.output;
        var removeSourceMapLine = function (x){
            var sourceMapIndex = x.indexOf('//# sourceMappingURL');
            if (sourceMapIndex !== -1) {
                return x.substring(0, sourceMapIndex);
            } else {
                return x;
            }
        };

        var suite = 'minified';
        var out = [];

        var failurePaths = [];
	    var files = {};

        paths.forEach(function( path ) {
            var testFilePath = path.replace(".min.js", ".test.min.js");
            if (grunt.file.exists(testFilePath)){
                var expectedFile = grunt.file.read(testFilePath);        
                var sourceFile = grunt.file.read(path);
                if ( removeSourceMapLine(sourceFile) != removeSourceMapLine(expectedFile) ) {
                    failurePaths.push(path);
                    //grunt.fail.fatal("The minified file for " + path + " is not the updated with latest changes. Run 'grunt uglify:minify' command to update the minified files.");
                } 
            }
        });

	    out.push("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
	    out.push("<testsuites> <testsuite name=\"" + suite + "\" tests=\"" + (paths.length || 0) + "\" failures=\"" + failurePaths.length + "\" errors=\"0\">");

        failurePaths.forEach(function( path ) {
            out.push("\t<testcase name=\"" + path + "\">");
			out.push("\t\t<failure message=\"" + "Not updated minified files." + "\">");
			out.push("The minified file for " + path + " is not the updated with latest changes. Run grunt uglify:minify command to update the minified files.");
			out.push("\t\t</failure>");
			out.push("\t</testcase>");
        });

        // we need at least 1 empty test
        if (failurePaths.length === 0) {
		    out.push("\t<testcase name=\"" + suite + "\" />");
	    } 
	    out.push("</testsuite></testsuites>");

	    fs.writeFileSync(output, out.join('\n'));
    });
	
	//Project Configuration
	grunt.initConfig({
		
		pkg: grunt.file.readJSON('package.json'),
		
		jshint: {
			options: {
				reporter: "../Feather/Tests/Telerik.Sitefinity.Frontend.ClientTest/jshint-reporter.js",
				reporterOutput: "Tests/FeatherWidgets.ClientTest/TestResults/hint.xml"
			},
			//define the files to lint
			files: ['gruntfile.js',
					'**/*.js',
					'!node_modules/**/*.js',
					'!Tests/**/*.js',
					'!Telerik.Sitefinity.Frontend.*/gruntfile.js',
					'!Telerik.Sitefinity.Frontend.Media/assets/magnific/*',
                    '!**/*min.js'
			]
		},
		
		watch: {
		  options: {
			spawn: false
		  },
		  js: {
			files: ['Telerik.Sitefinity.Frontend.*/**/*.js',
					    '!Telerik.Sitefinity.Frontend.*/**/designerview-*.js',
					    '!Telerik.Sitefinity.Frontend.*/**/*.min.js']
		  }
		},
		
		uglify: {
            options : {
                sourceMap : true,
                sourceMapIncludeSources : true
              },
		      minify: {
			    files: grunt.file.expandMapping(['Telerik.Sitefinity.Frontend.*/**/*.js',
					    '!Telerik.Sitefinity.Frontend.*/**/designerview-*.js',
					    '!Telerik.Sitefinity.Frontend.*/**/*.min.js'], './', {
				    rename: function(destBase, destPath) {
					    return destBase+destPath.replace('.js', '.min.js');
				    }
			    })
		      },
              test: {
			    files: grunt.file.expandMapping(['Telerik.Sitefinity.Frontend.*/**/*.js',
					    '!Telerik.Sitefinity.Frontend.*/**/designerview-*.js',
					    '!Telerik.Sitefinity.Frontend.*/**/*.min.js'], './', {
				    rename: function(destBase, destPath) {
					    return destBase+destPath.replace('.js', '.test.min.js');
				    }
			    })
		      }
		},

        assertMinified: {
            js: {
                paths: ['Telerik.Sitefinity.Frontend.*/**/*.min.js',
					'!Telerik.Sitefinity.Frontend.*/**/designerview-*.js',
					'!Telerik.Sitefinity.Frontend.*/**/*.test.min.js',
                    '!Telerik.Sitefinity.Frontend.*/**/*.min.min.js'],
                output: "Tests/FeatherWidgets.ClientTest/TestResults/assertMinified.xml"
            }
        },		

		jasmine: {	
			newsTests:{
				src: [
					'Telerik.Sitefinity.Frontend.News/MVC/Scripts/**/*.js'
				],
				options: {
					vendor:[
					'../feather/Tests/Telerik.Sitefinity.Frontend.ClientTest/helpers/jquery.min.js',
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
					'../feather/Telerik.Sitefinity.Frontend/client-components/components/icon/sf-icon.js',
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
			},
			searchTests:{
			    src: [
					'Telerik.Sitefinity.Frontend.Search/MVC/Scripts/**/*.js'
			    ],
			    options: {
			        vendor:[
					'../feather/Tests/Telerik.Sitefinity.Frontend.ClientTest/helpers/jquery.min.js',
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
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/pages/sf-page-service.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/pages/sf-page-selector.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/search/sf-search-service.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/components/icon/sf-icon.js',
					'!../feather/Telerik.Sitefinity.Frontend/Designers/Scripts/page-editor.js',
					'Tests/FeatherWidgets.ClientTest/helpers/mocks/*.js'
			        ],
			        errorReporting: true,
			        specs: ['Tests/FeatherWidgets.ClientTest/unit/Search/*.js'],
			        junit: {
			            path: 'Tests/FeatherWidgets.ClientTest/TestResults/Search'
			        },
			        template: require('grunt-template-jasmine-istanbul'),
			        templateOptions: {
			            coverage: 'Tests/FeatherWidgets.ClientTest/coverage/Search/coverage.json',
			            report: [
							{type: 'html', options: {dir: 'Tests/FeatherWidgets.ClientTest/coverage/Search'}},
							{type: 'cobertura', options: {dir: 'Tests/FeatherWidgets.ClientTest/coverage/cobertura/Search'}},
							{type: 'text-summary'}
			            ]
			        }
			    }
			},
			dynamicContentTests:{
				src: [
					'Telerik.Sitefinity.Frontend.DynamicContent/MVC/Scripts/**/*.js'
				],
				options: {
					vendor:[
					'../feather/Tests/Telerik.Sitefinity.Frontend.ClientTest/helpers/jquery.min.js',
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
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/dynamic-modules/sf-data-service.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/dynamic-modules/sf-dynamic-items-selector.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/taxonomies/sf-flat-taxon-service.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/taxonomies/sf-taxon-selector.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/date-time/sf-timespan-selector.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/pages/sf-page-service.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/selectors/pages/sf-page-selector.js',
					'../feather/Telerik.Sitefinity.Frontend/client-components/components/icon/sf-icon.js',
					'!../feather/Telerik.Sitefinity.Frontend/Designers/Scripts/page-editor.js',
					'Tests/FeatherWidgets.ClientTest/helpers/mocks/*.js'
					],
					errorReporting: true,
					specs: ['Tests/FeatherWidgets.ClientTest/unit/DynamicContent/*.js'],
					junit: {
						path: 'Tests/FeatherWidgets.ClientTest/TestResults/DynamicContent'
						},
					template: require('grunt-template-jasmine-istanbul'),
					templateOptions: {
						coverage: 'Tests/FeatherWidgets.ClientTest/coverage/DynamicContent/coverage.json',
						report: [
							{type: 'html', options: {dir: 'Tests/FeatherWidgets.ClientTest/coverage/DynamicContent'}},
							{type: 'cobertura', options: {dir: 'Tests/FeatherWidgets.ClientTest/coverage/cobertura/DynamicContent'}},
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
		}
	
	});
	
	// on watch events configure jshint and uglify to only run on changed file
	grunt.event.on('watch', function(action, filepath) {
	  grunt.config(['jshint.src', 'uglify.minify.files'], filepath);
	  grunt.config('jshint.src', filepath);
	  grunt.config('uglify.minify.files', 
				grunt.file.expandMapping(new Array(filepath), './', {
				    rename: function(destBase, destPath) {
					    return destBase+destPath.replace('.js', '.min.js');
				    }
			    }));
	  grunt.task.run('jshint');
	  grunt.task.run('uglify:minify');
	});
	
	//Load the needed plugins
	grunt.loadNpmTasks('grunt-contrib-watch');
	grunt.loadNpmTasks('grunt-contrib-jshint');
	grunt.loadNpmTasks('grunt-contrib-jasmine');
	grunt.loadNpmTasks("grunt-contrib-connect");
	grunt.loadNpmTasks('grunt-html2js');
	grunt.loadNpmTasks("grunt-contrib-uglify");
	
	//Default task(s)
	grunt.registerTask('default', ['jshint','html2js','jasmine', 'uglify:test', 'assertMinified']);
    //Watches for changes in the js files and minifies only the changed file
	grunt.registerTask('dev', ['watch', 'html2js', 'jasmine', 'assertMinified']);
	
};