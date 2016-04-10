; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('audioContainer', ['$rootScope',
		function ($rootScope) {
		    return {
		        restrict: 'E',
		        templateUrl: 'app/directives/containers/audioContainer/audioContainer.html',
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