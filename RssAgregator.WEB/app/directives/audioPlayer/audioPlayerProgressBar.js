; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('audioPlayerProgressBar', ['$timeout',
		function ($timeout) {
		    return {
		        restrict: 'E',
		        templateUrl: 'app/directives/audioPlayer/audioPlayerProgressBar.html',
		        scope: {
		            currentPlayingSong: '=',
                    seek: '='
		        },
		        link: function (scope, element, attrs) {
		            scope.maxSongDuration = 100;

		            scope.mouseButtonIsDown = false;
		            scope.currentMouseLocation = 0;

		            scope.currentSongName = '';

		            scope.currentSongUserFriendlyProgress = function () {
		                return scope.currentPlayingSong ? scope.currentPlayingSong.userFriendlyProgress : "00:00";
		            };

		            scope.currentSongProgress = function () {
		                return scope.currentPlayingSong && scope.currentPlayingSong.progress && scope.currentPlayingSong.estimatedDuration
                                            ? (scope.currentPlayingSong.progress / scope.currentPlayingSong.estimatedDuration) * 100
                                            : 0;
		            };

		            scope.mouseMove = function ($event) {
		                if (scope.mouseButtonIsDown && scope.currentMouseLocation !== $event.offsetX) {
		                    scope.currentMouseLocation = $event.offsetX;
		                }
		            };

		            scope.mouseDown = function (isMouseDown) {
		                scope.mouseButtonIsDown = isMouseDown;
		            };

		            function init() {
		                scope.$watch('currentPlayingSong', function (newValue) {
		                    if (newValue){
		                        scope.currentSongName = newValue.artist + ' - ' + newValue.name;
		                    }
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular);