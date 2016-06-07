; (function (angular) {
    'use strict';

    angular.module('app')
        .controller('dashboardController', ['$scope', 'templateFactory', 'onDemandViewTemplates', 'newsService', 'routeRedirectService', 'userService', 'uiSettings',
    function ($scope, templateFactory, onDemandViewTemplates, newsService, routeRedirectService, userService, uiSettings) {
                $scope.headerView;
                $scope.addNewPostButtonVisible = false;
                $scope.postList = [];

                $scope.pageSize = 10;
                $scope.currentPage = 1;
                $scope.hideAdultContent = true;

                $scope.addNewPost = function () {
                    routeRedirectService.redirect('add');
                };

                $scope.editPost = function (postId) {
                    routeRedirectService.redirect('edit', postid);
                };

                $scope.newPosts = function () {
                    
                };

                $scope.bestPosts = function () {

                };

                $scope.about = function () {
                    routeRedirectService.redirect('about');
                };

                $scope.search = function () {

                };

                function init() {
                    var searchParams = { 
                        PageSize: $scope.pageSize, 
                        PageNumber: $scope.currentPage, 
                        HideAdult: $scope.hideAdultContent 
                    };

                    templateFactory.getAsync(onDemandViewTemplates.header).then(function (serverResult) {
                        if (serverResult) {
                            $scope.headerView = serverResult;
                        }
                    });

                    newsService.get('NewsSearch', searchParams).then(function (serverResult) {
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