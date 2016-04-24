; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('videoContainer', ['$timeout', 'templateFactory',
		function ($timeout, templateFactory) {
		    return {
		        restrict: 'E',
		        templateUrl: function () {
		            return templateFactory.get('videoContainer');
		        },
                transclude: true,
		        scope: {
		        },
		        link: function (scope, element, attrs, ctrl, transclude) {
		            var transcludedEl = element.children('[transclude-video]');

		            scope.videoNotActive = true;

		            scope.videoClick = function ($event) {
		                $event.preventDefault();
		                activateVideo();
		            };

		            function activateVideo() {
		                scope.videoNotActive = false;

		                transcludedEl.css('margin-top', '0');

		                $timeout(function () {
		                    scope.$parent.$broadcast('videoActivated', { id: scope.$id, height: transcludedEl.height() });
		                }, 100);
		            }

		            function init() {
		                transclude(scope, function (clone, scope) {
		                    transcludedEl.append(clone);
		                });

		                scope.$on('videoActivated', function (event, data) {
		                    if (data.id != scope.$id) {
		                        scope.videoNotActive = true;
		                        transcludedEl.css('margin-top', '{0}px'.format(data.height + 10));
		                    }
		                });

		                scope.$emit('videoData', { id: scope.$id, activate: activateVideo });
		            }

		            init();
		        }
		    };
		}]);
})(angular);