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
            }).when('/about', {
                templateUrl: '/app/controllers/aboutController/aboutController.html',
                //templateUrl: function () {
                //    return templateFactory.get('aboutController');
                //},
                controller: 'aboutController'
            }).when('/add', {
                templateUrl: '/app/controllers/addEditPostController/addEditPostController.html',
                //templateUrl: function () {
                //    return templateFactory.get('addEditPostController');
                //},
                controller: 'addEditPostController'
            }).when('/edit/:postId', {
                templateUrl: '/app/controllers/addEditPostController/addEditPostController.html',
                //templateUrl: function () {
                //    return templateFactory.get('addEditPostController');
                //},
                controller: 'addEditPostController'
            }).when('/view/:postId', {
                templateUrl: '/app/controllers/viewPostController/viewPostController.html',
                //templateUrl: function () {
                //    return templateFactory.get('viewPostController');
                //},
                controller: 'viewPostController'
            }).otherwise({
                redirectTo: '/'
            });
    }]);
})(angular);