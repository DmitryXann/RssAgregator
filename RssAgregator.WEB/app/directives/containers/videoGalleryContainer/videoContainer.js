; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('videoContainer', ['$filter', '$log',
		function ($filter, $log) {
		    return {
		        restrict: 'E',
		        template: '<img ng-src="{{::link}}" ng-click="activate()" is-active="{{isActive()}}" >',
		        scope: {
		            link: '@',
		            activeImage: '=',
		            images: '='
		        },
		        link: function (scope, element, attrs) {

		        }
		    };
		}]);
})(angular);