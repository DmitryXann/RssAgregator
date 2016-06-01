; (function (angular) {
    'use strict';

    angular.module('app').config(['$routeProvider', /*'templateFactory',*/
        function ($routeProvider/*, templateFactory*/) {
            $routeProvider.when('/', {
                templateUrl:'/app/controllers/dashboardController/dashboard.html',
                //templateUrl: function () {
                //    return templateFactory.get('dashboardController');
                //},
                controller: 'dashboardController'
            }).when('/:filter', {
                templateUrl: '/app/controllers/dashboardController/dashboard.html',
                //templateUrl: function () {
                //    return templateFactory.get('dashboardController');
                //},
                controller: 'dashboardController'
            }).when('/:filter/:postId', {
                templateUrl: '/app/controllers/dashboardController/dashboard.html',
                //templateUrl: function () {
                //    return templateFactory.get('dashboardController');
                //},
                controller: 'dashboardController'
            }).otherwise({
                redirectTo: '/'
            });
    }]);
})(angular);