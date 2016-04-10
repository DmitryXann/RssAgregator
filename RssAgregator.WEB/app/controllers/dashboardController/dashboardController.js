; (function (angular) {
    'use strict';

    angular.module('app')
        .controller('dashboardController', ['$scope', 'templateService', 'templateType', 'newsService',
            function ($scope, templateService, templateType, newsService) {
                $scope.headerView;
                $scope.addNewPostButtonVisible = false;
                $scope.postList = [];

                $scope.pageSize = 10;
                $scope.currentPage = 1;
                $scope.hideAdultContent = true;

                $scope.addNewPost = function () {

                };

                $scope.newPosts = function () {

                };

                $scope.bestPosts = function () {

                };

                $scope.about = function () {

                };

                $scope.search = function () {

                };

                function init() {
                    templateService.get('GetTemplate', { id: templateType.Header }).then(function (serverResult) {
                        if (serverResult.sucessResult) {
                            $scope.headerView = serverResult.DataResult.View;
                        } else {
                            serverResult.showInfoMessage();
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