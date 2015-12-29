; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('bindHtmlCompile', ['$compile',
		function ($compile) {
		    return {
		        restrict: 'E',
		        scope: {
		            sourceHtml: '='
		        },
		        link: function (scope, element, attrs) {
		            scope.$watch('sourceHtml', function (html) {
		                element.html(html);
		                $compile(element.contents())(scope.$parent);
		            });
		        }
		    };
		}]);
})(angular);