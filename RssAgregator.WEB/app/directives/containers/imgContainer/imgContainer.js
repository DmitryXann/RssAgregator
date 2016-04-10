; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('imgContainer', [
		function () {
		    return {
		        restrict: 'E',
		        templateUrl: 'app/directives/containers/imgContainer/imgContainer.html',
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