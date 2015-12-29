; (function (angular) {
    'use strict';

    angular.module('app')
        .controller('dashboardController', ['$scope', 'templateService', 'templateType', 'newsService',
            function ($scope, templateService, templateType, newsService) {
                $scope.headerView;
                $scope.addNewPostButtonVisible = false;
                $scope.postList = [];

                $scope.pageSize = 5;
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
                        $scope.headerView = serverResult.Data.View;
                    });

                    newsService.get('NewsSearch', { PageSize: $scope.pageSize, PageNumber: $scope.currentPage, HideAdult: $scope.hideAdultContent }).then(function (serverResult) {
                        angular.forEach(serverResult.Data, function (value) {
                            $scope.postList.push(value);
                        });
                    });
                }

                init();
            }]);
})(angular);