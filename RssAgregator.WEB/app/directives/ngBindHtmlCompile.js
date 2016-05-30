; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('ngBindHtmlCompile', ['$compile',
		function ($compile) {
		    return {
		        restrict: 'E',
		        scope: {
		            sourceHtml: '='
		        },
		        link: function (scope, element, attrs) {
		            scope.$watch('sourceHtml', function (html) {
		                if (html) {
		                    element.html(html);
		                    $compile(element.contents())(scope.$parent);
		                }
		            });
		        }
		    };
		}]);
})(angular);