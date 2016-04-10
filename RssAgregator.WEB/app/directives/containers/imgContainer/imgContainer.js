; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('imgContainer', ['$timeout',
		function ($timeout) {
		    return {
		        restrict: 'E',
		        scope: {
		            link: '@',
		        },
		        link: function (scope, element, attrs) {
		            function init() {
		                $timeout(function () {
		                    if (element.next('img-container').length || element.prev('img-container').length) {
		                        if (angular.isUndefined(scope.$parent.postImages) || scope.$parent.postImages === null) {
		                            scope.$parent.postImages = [];
		                        }

		                        scope.$parent.postImages.push(scope.link);
		                    }
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular);