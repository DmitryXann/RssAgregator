; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('postContainer', ['newsService', '$sce',
		function (newsService, $sce) {
		    return {
		        restrict: 'E',
		        templateUrl: 'app/directives/containers/postContainer/postContainer.html',
		        scope: {
                    postId: '@',
		            postObject: '='
		        },
		        link: function (scope, element, attrs) {
		            scope.postView;
		            scope.postImages;

		            function init() {
		                scope.postView = scope.postObject.PostContent;
		            }

		            init();
		        }
		    };
		}]);
})(angular);