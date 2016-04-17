; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('imageGalleryContainer', ['$filter',
		function ($filter) {
		    return {
		        restrict: 'E',
		        templateUrl: 'app/directives/containers/imageGalleryContainer/imageGalleryContainer.html',
                transclude: true,
		        scope: {
		        },
		        link: function (scope, element, attrs, ctrl, transclude) {
		            scope.activeImage = null;
		            scope.images = [];
		            scope.canChangeImage;

		            scope.next = function () {
		                var activeImageIndex = getActiveImageId();

		                if (activeImageIndex + 1 < scope.images.length) {
		                    scope.images[activeImageIndex + 1].activate();
		                } else {
		                    scope.images[0].activate();
		                }
		                
		            };

		            scope.previous = function () {
		                var activeImageIndex = getActiveImageId();

		                if (activeImageIndex - 1 >= 0) {
		                    scope.images[activeImageIndex - 1].activate();
		                } else {
		                    scope.images[scope.images.length - 1].activate();
		                }
		            };

		            function getActiveImageId() {
		                return scope.images.indexOf($filter('filter')(scope.images, { id: scope.activeImage.id }, true)[0]);
		            }

		            function init() {
		                var transcludeEl = element.children('[transclude-img]');

		                transclude(scope, function (clone, scope) {
		                    transcludeEl.append(clone);
		                });

		                var imagesWatcher = scope.$watch(function () {
		                    return scope.images.length > 0;
		                }, function (newVal) {
		                    if (newVal) {
		                        scope.canChangeImage = scope.images.length > 1;
		                        scope.images[0].activate();
		                        imagesWatcher();
		                    }
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular);