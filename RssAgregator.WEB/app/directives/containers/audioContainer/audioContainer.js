; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('audioContainer', ['$rootScope', 'templateFactory',
		function ($rootScope, templateFactory) {
		    return {
		        restrict: 'E',
		        templateUrl: function () {
		            return templateFactory.get('audioContainer');
		        },
		        scope: {
		            link: '@',
		            artist: '@',
		            name: '@'
		        },
		        link: function (scope, element, attrs) {
		            scope.play = function () {
		                $rootScope.$broadcast('playSong', { name: scope.name, artist: scope.artist, link: scope.link });
		            };
		        }
		    };
		}]);
})(angular);