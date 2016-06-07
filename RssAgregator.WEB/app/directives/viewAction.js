; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('viewAction', ['$routeParams', 'viewFilters', 'newsService', 'templateFactory', 'onDemandViewTemplates', 'genericModalFactory', 'uiSettings', 'routeRedirectService',
		function ($routeParams, viewFilters, newsService, templateFactory, onDemandViewTemplates, genericModalFactory, uiSettings, routeRedirectService) {
		    return {
		        restrict: 'A',
		        scope: false,
		        link: function (scope, element, attrs) {
		            scope.$watch(function () {
		                return $routeParams.filter;
		            }, function (newVal) {
		                if (newVal) {
		                    switch (newVal.toLowerCase()) {
		                        case viewFilters.view:
		                            if ($routeParams.id) {
		                                newsService.get('NewsItemSearch', $routeParams.id).then(function (serverResult) {
		                                    if (serverResult.sucessResult) {
		                                        templateFactory.getAsync(onDemandViewTemplates.postModal).then(function (templateServerResult) {
		                                            if (serverResult) {
		                                                genericModalFactory.show(serverResult.DataResult.PostName, 'Ok', null, templateServerResult, { post: serverResult.DataResult }, 'lg').then(function (modalOkResult) {
		                                                    routeRedirectService.redirect();
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
		                            if ($routeParams.id) {
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
		                                        routeRedirectService.redirect();
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
		            });
		        }
		    };
		}]);
})(angular);
