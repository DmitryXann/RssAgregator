; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('videoGalleryContainer', ['$filter',
		function ($filter) {
		    return {
		        restrict: 'E',
		        templateUrl: 'app/directives/containers/videoGalleryContainer/videoGalleryContainer.html',
		        transclude: true,
		        scope: {
		        },
		        link: function (scope, element, attrs, ctrl, transclude) {
		           
		        }
		    };
		}]);
})(angular);