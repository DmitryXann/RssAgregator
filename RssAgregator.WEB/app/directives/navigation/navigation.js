; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('navigation', ['templateFactory', 'navigationService', '$location',
		function (templateFactory, navigationService, $location) {
		    return {
		        restrict: 'E',
		        templateUrl: function () {
		            return templateFactory.get('navigation');
		        },
		        scope: {
		        },
		        link: function (scope, element, attrs) {
		            scope.navigationData;

		            scope.redirect = function (redirectTo) {
		                $location.path(redirectTo);
		            };

		            function init() {
		                navigationService.get('GetNavigationData').then(function (serverResult) {
		                    if (serverResult.sucessResult) {
		                        scope.navigationData = serverResult.DataResult;
		                    } else {
		                        serverResult.showInfoMessage();
		                    }
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular);