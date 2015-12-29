; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('videoContainer', [
		function () {
		    return {
		        restrict: 'E',
		        scope: {
		            link: '@',
		            author: '@',
		            name: '@'
		        },
		        link: function (scope, element, attrs) {
		            function init() {
		            }

		            init();
		        }
		    };
		}]);
})(angular);