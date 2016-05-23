; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('imgContainer', ['$filter', '$exceptionHandler', 'templateFactory',
		function ($filter, $exceptionHandler, templateFactory) {
		    return {
		        restrict: 'E',
		        templateUrl: function () {
		            return templateFactory.get('imgContainer');
		        },
		        scope: {
		            link: '@'
		        },
		        link: function (scope, element, attrs) {
		            scope.isActive = false;

		            scope.activate = function () {
		                scope.isActive = true;

		                scope.$parent.$broadcast('imgActivated', { id: scope.$id });
		            };

		            function init() {
		                scope.$emit('imageData', { id: scope.$id, link: scope.link, activate: scope.activate });

		                if (element.next('img-container').length === 0 && element.prev('img-container').length === 0) {
		                    element.remove();
		                }

		                element.children('img').error(function () {
		                    scope.$emit('badImage', { id: scope.$id });
		                    element.remove();

		                    $exceptionHandler('Bad image was removed: ' + scope.link);
		                });

		                scope.$on('imgActivated', function (event, data) {
		                    if (scope.$id !== data.id) {
		                        scope.isActive = false;
		                    }
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular);