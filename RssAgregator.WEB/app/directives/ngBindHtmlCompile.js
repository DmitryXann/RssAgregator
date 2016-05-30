; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('ngBindHtmlCompile', ['$compile',
		function ($compile) {
		    return {
		        restrict: 'E',
		        scope: false,
		        link: function (scope, element, attrs) {
		            scope.$watch(attrs.sourceHtml, function (html) {
		                if (html) {
		                    element.html(html);
		                    $compile(element.contents())(scope);
		                }
		            });
		        }
		    };
		}]);
})(angular);
