; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('videoGalleryContainer', ['$filter', '$timeout', 'templateFactory',
		function ($filter, $timeout, templateFactory) {
		    return {
		        restrict: 'E',
		        templateUrl: function () {
		            return templateFactory.get('videoGalleryContainer');
		        },
		        transclude: true,
		        scope: {
		        },
		        link: function (scope, element, attrs, ctrl, transclude) {
		            scope.canChangeVideos;

		            scope.activeVideoId;

		            scope.videos = [];

		            scope.next = function () {
		                var activeVideoIndex = getActiveVideoIndex();

		                if (activeVideoIndex + 1 < scope.videos.length) {
		                    scope.videos[activeVideoIndex + 1].activate();
		                } else {
		                    scope.videos[0].activate();
		                }

		            };

		            scope.previous = function () {
		                var activeVideoIndex = getActiveVideoIndex();

		                if (activeVideoIndex - 1 >= 0) {
		                    scope.videos[activeVideoIndex - 1].activate();
		                } else {
		                    scope.videos[scope.videos.length - 1].activate();
		                }
		            };

		            function getActiveVideoIndex() {
		                return scope.videos.indexOf($filter('filter')(scope.videos, { id: scope.activeVideoId }, true)[0]);
		            }

		            function init() {
		                scope.$on('videoData', function (event, data) {
		                    event.preventDefault();
		                    scope.videos.push(data);
		                });

		                scope.$on('videoActivated', function (event, data) {
		                    scope.activeVideoId = data.id;
		                });

		                var transcludeEl = element.children('[transclude-video-containers]');

		                transclude(scope, function (clone, scope) {
		                    scope.canChangeVideos = clone.length > 2;
		                    transcludeEl.append(clone);
		                });

		                $timeout(function () {
		                    scope.videos[0].activate();
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular);