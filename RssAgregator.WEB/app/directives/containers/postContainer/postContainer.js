; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('postContainer', ['newsService', 'templateFactory',
		function (newsService, templateFactory) {
		    return {
		        restrict: 'E',
		        templateUrl: function () {
		            return templateFactory.get('postContainer');
		        },
		        transclude: true,
		        scope: {
		            postId: '@',
		            authorName: '@',
		            authorLink: '@',
		            postName: '@',

		            postObject: '='
		        },
		        link: function (scope, element, attrs) {
		            scope.postView;
		            scope.authorName;
		            scope.authorLink;
		            scope.postName;
		            scope.postLikes;

		            scope.getAuthorLink = function () {
		                return scope.postObject.AuthorLink;
		            };

		            scope.getPostLink = function () {
		                return scope.postObject.PostLink;
		            };
                
		            scope.likeToggle = function (like) {
		                if (like) {
		                    if (scope.postLikes <= scope.postObject.PostLikes) {
		                        scope.postLikes += 1;
		                    }
		                    
		                } else if (scope.postLikes >= scope.postObject.PostLikes) {
		                    scope.postLikes -= 1;
		                }
		            };

		            scope.likeToggleActive = function (like) {
		                if (like) {
		                    return scope.postLikes > scope.postObject.PostLikes;

		                } else {
		                    return scope.postLikes < scope.postObject.PostLikes;
		                }
		            };

		            function init() {
		                scope.postView = scope.postObject.PostContent;
		                scope.authorName = scope.postObject.AuthorName;
		                scope.authorLink = scope.postObject.AuthorLink;
		                scope.postName = scope.postObject.PostName;
		                scope.postLikes = scope.postObject.PostLikes;
		            }

		            init();
		        }
		    };
		}]);
})(angular);