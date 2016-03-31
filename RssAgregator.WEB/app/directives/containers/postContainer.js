; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('postContainer', ['newsService', '$sce',
		function (newsService, $sce) {
		    return {
		        restrict: 'E',
		        template: '<bind-html-compile source-html="::postView"></bind-html-compile>',
		        //template: '<div ng-bind-html="::postView"></div>',
		        scope: {
                    postId: '@',
		            postObject: '='
		        },
		        link: function (scope, element, attrs) {
		            scope.postView;

		            function init() {
		                scope.postView = scope.postObject.PostContent;
		            }

		            init();
		        }
		    };
		}]);
})(angular);