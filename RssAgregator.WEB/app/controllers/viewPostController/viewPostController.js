; (function (angular) {
    'use strict';

    angular.module('app')
        .controller('viewPostController', ['$scope', 'newsService', '$routeParams',
            function ($scope, newsService, $routeParams) {
                $scope.postContent;

                function init() {
                    newsService.get('NewsItemSearch', $routeParams.postId).then(function (serverResult) {
                        if (serverResult.sucessResult) {
                            $scope.postContent = serverResult.DataResult;
                        } else {
                            serverResult.showInfoMessage();
                        }
                    });
                }

                init();
            }]);
})(angular);