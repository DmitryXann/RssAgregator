; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('imageGalleryContainer', ['$filter', '$timeout', 'templateFactory',
		function ($filter, $timeout, templateFactory) {
		    return {
		        restrict: 'E',
		        templateUrl: function () {
		            return templateFactory.get('imageGalleryContainer');
		        },
                transclude: true,
		        scope: {
		        },
		        link: function (scope, element, attrs, ctrl, transclude) {
		            scope.activeImage = null;
		            scope.images = [];
		            scope.canChangeImage;

		            scope.next = function () {
		                var activeImageIndex = getActiveImageIndex();

		                if (activeImageIndex + 1 < scope.images.length) {
		                    scope.images[activeImageIndex + 1].activate();
		                } else {
		                    scope.images[0].activate();
		                }
		                
		            };

		            scope.previous = function () {
		                var activeImageIndex = getActiveImageIndex();

		                if (activeImageIndex - 1 >= 0) {
		                    scope.images[activeImageIndex - 1].activate();
		                } else {
		                    scope.images[scope.images.length - 1].activate();
		                }
		            };

		            function getActiveImageIndex() {
		                return scope.images.indexOf($filter('filter')(scope.images, { id: scope.activeImage.id }, true)[0]);
		            }

		            function init() {
		                scope.$on('imageData', function (event, data) {
		                    event.preventDefault();
		                    scope.images.push(data);
		                });

		                scope.$on('badImage', function (event, data) {
		                    event.preventDefault();

		                    var indexOfBadEl = scope.images.indexOf($filter('filter')(scope.images, { id: data.id }, true)[0]);
		                    scope.images.splice(indexOfBadEl, 1);
		                });

		                scope.$on('imgActivated', function (event, data) {
		                    scope.activeImage = $filter('filter')(scope.images, { id: data.id }, true)[0];
		                });

		                var transcludeEl = element.children('[transclude-img]');

		                transclude(scope, function (clone, scope) {
		                    scope.canChangeImage = clone.length > 2;

		                    transcludeEl.append(clone);
		                });

		                $timeout(function () {
		                    scope.images[0].activate();
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular);