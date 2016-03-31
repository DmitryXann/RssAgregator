; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('audioContainer', [
		function () {
		    return {
		        restrict: 'E',
		        template: '',
		        scope: {
		            link: '@',
		            preview: '@',
		            name: '@'
		        },
		        link: function (scope, element, attrs) {
		            scope.audio;

		            function init() {
		            }

		            init();
		        }
		    };
		}]);
})(angular);