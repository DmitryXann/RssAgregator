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
		                return scope.postObject ? scope.postObject.AuthorLink : undefined;
		            };

		            scope.getPostLink = function () {
		                return scope.postObject ? scope.postObject.PostLink : undefined;
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
		                var result = false;

		                if (scope.postObject) {
		                    if (like) {
		                        result = scope.postLikes > scope.postObject.PostLikes;
		                    } else {
		                        result = scope.postLikes < scope.postObject.PostLikes;
		                    }
		                }

		                return result;
		            };

		            function init() {
		                scope.$watch('postObject', function(newVal) {
		                    if (newVal) {
		                        scope.postView = scope.postObject.PostContent;
		                        scope.authorName = scope.postObject.AuthorName;
		                        scope.authorLink = scope.postObject.AuthorLink;
		                        scope.postName = scope.postObject.PostName;
		                        scope.postLikes = scope.postObject.PostLikes;
		                    }
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular);