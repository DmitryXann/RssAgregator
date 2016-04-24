; (function (angular, videojs) {
    'use strict';

    angular.module('app')
		.directive('tagCloud', ['$sce', 'uiSettings', 'templateFactory',
		function ($sce, uiSettings, templateFactory) {
		    return {
		        restrict: 'E',
		        templateUrl: function () {
		            return templateFactory.get('tagCloud');
		        },
		        scope: {
		        },
		        link: function (scope, element, attrs) {

		            function init() {
		            }

		            init();
		        }
		    };
		}]);
})(angular, videojs);