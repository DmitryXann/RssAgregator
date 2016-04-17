; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('imgContainer', [
		function () {
		    return {
		        restrict: 'E',
		        template: '<img ng-src="{{::link}}" ng-click="activate()" is-active="{{isActive()}}" >',
		        scope: {
		            link: '@',
		            activeImage: '=',
                    images: '='
		        },
		        link: function (scope, element, attrs) {
		            scope.activeImageObj;           

		            scope.activate = function () {
		                scope.activeImage = angular.copy(scope.activeImageObj);
		            };

		            scope.isActive = function () {
		                return scope.activeImage ? scope.activeImage.id === scope.$id : false
		            };

		            function init() {
		                scope.activeImageObj = { id: scope.$id, link: scope.link, activate: scope.activate };

		                var activeImageWatcher = scope.$watch('images', function (vewVal) {
		                    if (vewVal) {
		                        scope.images.push(angular.copy(scope.activeImageObj));

		                        if (element.next('img-container').length === 0 && element.prev('img-container').length === 0) {
		                            element.remove();
		                        }

		                        activeImageWatcher();
		                    }
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular);