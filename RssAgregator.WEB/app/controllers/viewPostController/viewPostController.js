; (function (angular) {
    'use strict';

    angular.module('app')
        .controller('viewPostController', ['$scope', 'newsService', '$routeParams',
            function ($scope, newsService, $routeParams) {
                $scope.postContent;
                $scope.postName;
                $scope.postTags;
                $scope.isAdult;
                $scope.author;

                function init() {
                    newsService.get('NewsItemSearch', $routeParams.postId).then(function (serverResult) {
                        if (serverResult.sucessResult) {
                            $scope.postContent = serverResult.DataResult.PostContent;

                            $scope.postName = serverResult.DataResult.PostName;
                            $scope.postTags = serverResult.DataResult.PostTags.join(',');
                            $scope.isAdult = serverResult.DataResult.AdultContent;
                        } else {
                            serverResult.showInfoMessage();
                        }
                    });
                }

                init();
            }]);
})(angular);