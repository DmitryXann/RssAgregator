; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('ngDisabledChildren', [
		function () {
		    return {
		        restrict: 'A',
		        priority: 100,
		        scope: false,
		        link: function (scope, element, attrs) {
					scope.$watch(attrs.ngDisabledChildren, function (newVal) {
						attr.$set('disabled', !!newVal);
					})
		        }
		    };
		}]);
})(angular);