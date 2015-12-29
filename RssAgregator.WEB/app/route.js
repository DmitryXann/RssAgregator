/*global angular*/
; (function (angular) {
    'use strict';
    angular.module('app').config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/', {
            templateUrl: 'app/controllers/dashboardController/dashboard.html',
            controller: 'dashboardController'
        }).otherwise({
            redirectTo: '/'
        });
    }]);
})(angular);