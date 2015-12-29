; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('imgContainer', [
		function () {
		    return {
		        restrict: 'E',
		        template: '<img src="{{::link}}">',
		        scope: {
		            link: '@',
		        },
		        link: function (scope, element, attrs) {
		            function init() {
		            }

		            init();
		        }
		    };
		}]);
})(angular);