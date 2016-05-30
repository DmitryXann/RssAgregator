; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('ngDisabledChildren', ['$timeout',
		function ($timeout) {
		    return {
		        restrict: 'A',
		        priority: 100,
		        scope: false,
		        link: function (scope, element, attrs) {
		            scope.$watch(attrs.ngDisabledChildren, function (newVal) {
		                $timeout(function () {
		                    if (!!newVal) {
		                        element.find('input').attr('disabled', 'disabled');
		                    } else {
		                        element.find('input').removeAttr('disabled');
		                    }
		                }, 0, false);
		            });
		        }
		    };
		}]);
})(angular);
