; (function (angular) {
    'use strict';

    angular.module('app')
        .controller('dashboardController', ['$scope', 'templateFactory', 'viewTemplates', 'newsService', '$location', 'genericModalFactory',
            function ($scope, templateFactory, viewTemplates, newsService, $location, genericModalFactory) {
                $scope.headerView;
                $scope.addNewPostButtonVisible = false;
                $scope.postList = [];

                $scope.pageSize = 10;
                $scope.currentPage = 1;
                $scope.hideAdultContent = true;

                $scope.addNewPost = function () {
                    $location.path('/add');
                };

                $scope.editPost = function (postId) {
                    $location.path('/edit/{0}'.format(postid));
                };

                $scope.newPosts = function () {
                    
                };

                $scope.bestPosts = function () {

                };

                $scope.about = function () {
                    $location.path('/about');
                };

                $scope.search = function () {

                };

                function init() {
                    templateFactory.getAsync(viewTemplates.header).then(function (serverResult) {
                        if (serverResult) {
                            $scope.headerView = serverResult;
                        }
                    });

                    newsService.get('NewsSearch', { PageSize: $scope.pageSize, PageNumber: $scope.currentPage, HideAdult: $scope.hideAdultContent }).then(function (serverResult) {
                        if (serverResult.sucessResult) {
                            angular.forEach(serverResult.DataResult, function (value) {
                                $scope.postList.push(value);
                            });
                        } else {
                            serverResult.showInfoMessage();
                        }
                    });
                }

                init();
            }]);
})(angular);