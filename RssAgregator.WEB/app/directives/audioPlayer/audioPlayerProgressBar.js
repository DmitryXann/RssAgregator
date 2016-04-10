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
		            scope.mouseEtered = false;

		            scope.currentMouseLocation = 0;
		            scope.currentScrollPercentage = 0;

		            scope.currentSongName = '';

		            scope.currentSongUserFriendlyProgress = function () {
		                return scope.currentPlayingSong ? scope.currentPlayingSong.userFriendlyProgress : '00:00';
		            };

		            scope.currentSongProgress = function () {
		                return scope.mouseButtonIsDown || scope.mouseEtered
		                            ? scope.currentScrollPercentage
                                    :(scope.currentPlayingSong && scope.currentPlayingSong.progress && scope.currentPlayingSong.estimatedDuration
                                            ? (scope.currentPlayingSong.progress / scope.currentPlayingSong.estimatedDuration) * 100
                                            : 0);
		            };

		            scope.mouseMove = function ($event) {
		                if (scope.currentPlayingSong) {
		                    recalculateProgress($event);
		                }
		            };

		            scope.mouseEnterToggle = function (isMouseEnter) {
		                if (scope.currentPlayingSong && isMouseEnter) {
		                    scope.mouseEtered = true;
		                } else {
		                    scope.mouseButtonIsDown = false;
		                    scope.mouseEtered = false;

		                    scope.currentMouseLocation = 0;
		                    scope.currentScrollPercentage = 0;
		                }
		            };

		            scope.mouseClickToggle = function ($event, isMouseDown) {
		                scope.mouseButtonIsDown = isMouseDown;
                        
		                if (scope.currentPlayingSong && !isMouseDown) {
		                    recalculateProgress($event);
		                    scope.seek(scope.currentScrollPercentage);
		                }
		            };

		            scope.approximateSongProgress = function () {
		                var result = '00:00';

		                if (scope.currentPlayingSong) {
		                    var expecetMillisecond = (scope.currentPlayingSong.estimatedDuration * scope.currentScrollPercentage) / 100;

		                    var minutes = Math.floor(expecetMillisecond / 60000);
		                    var seconds = ((expecetMillisecond % 60000) / 1000).toFixed(0);
		                    result = (minutes < 10 ? '0' : '') + minutes + ":" + (seconds < 10 ? '0' : '') + seconds;
		                }

		                return result;
		            };


		            function recalculateProgress($event) {
		                if (scope.currentPlayingSong && scope.currentMouseLocation !== $event.offsetX) {
		                    scope.currentMouseLocation = $event.offsetX;

		                    var currentScrollPercentage = Math.round10(($event.offsetX * 100) / angular.element($event.currentTarget).width(), -1);
		                    if (scope.currentScrollPercentage != currentScrollPercentage) {
		                        scope.currentScrollPercentage = currentScrollPercentage;
		                    }
		                }
		            }

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