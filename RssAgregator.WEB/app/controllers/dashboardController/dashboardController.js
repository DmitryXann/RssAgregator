; (function (angular) {
    'use strict';

    angular.module('app')
        .controller('dashboardController', ['$scope', 'templateFactory', 'onDemandViewTemplates', 'newsService', '$location', 'genericModalFactory', 'viewFilters', '$routeParams', 'userService', 'uiSettings',
            function ($scope, templateFactory, onDemandViewTemplates, newsService, $location, genericModalFactory, viewFilters, $routeParams, userService, uiSettings) {
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

                    if ($routeParams.filter) {
                        switch ($routeParams.filter.toLowerCase()) {
                            case viewFilters.view:
                                if ($routeParams.postId) {
                                    newsService.get('NewsItemSearch', $routeParams.postId).then(function(serverResult) {
                                        if (serverResult.sucessResult) {
                                            templateFactory.getAsync(onDemandViewTemplates.postModal).then(function (templateServerResult) {
                                                if (serverResult) {
                                                    genericModalFactory.show(serverResult.DataResult.PostName, 'Ok', null, templateServerResult, { post: serverResult.DataResult }, 'lg').then(function (modalOkResult) {
                                                        //No actions required
                                                    });
                                                }
                                            });
                                        } else {
                                            serverResult.showInfoMessage();
                                        }
                                    });
                                }
                            break;
                            case viewFilters.add:
                                templateFactory.getAsync(onDemandViewTemplates.addEditPostModal).then(function (templateServerResult) {
                                    if (templateServerResult) {
                                        genericModalFactory.show(uiSettings.AddModalHeader, null, null, templateServerResult, null, 'lg').then(function (modalOkResult) {
                                            //No actions required
                                        });
                                    }
                                });
                            break;
                            case viewFilters.edit:
                                if ($routeParams.postId) {
                                    templateFactory.getAsync(onDemandViewTemplates.addEditPostModal).then(function (templateServerResult) {
                                        if (serverResult) {
                                            genericModalFactory.show(uiSettings.EditModalHeader, null, null, templateServerResult, { post: serverResult.DataResult }, 'lg').then(function (modalOkResult) {
                                                //No actions required
                                            });
                                        }
                                    });
                                }
                            break;
                            case viewFilters.about:
                                templateFactory.getAsync(onDemandViewTemplates.aboutModal).then(function (templateServerResult) {
                                    if (templateServerResult) {
                                        genericModalFactory.show(uiSettings.AboutModalHeader, 'Ok', null, templateServerResult, null, 'lg').then(function (modalOkResult) {
                                            //No actions required
                                        });
                                    }
                                });
                            break;
                            case viewFilters.top:
                                //add some filters to the searchParams
                            break;
                            case viewFilters.best:
                                //add some filters to the searchParams
                            break;
                            case viewFilters.about:
                            break;
                        }
                    }

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