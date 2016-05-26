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
            }).when('/addPost', {
                templateUrl: '/app/controllers/addEditPostController/addEditPostController.html',
                //templateUrl: function () {
                //    return templateFactory.get('addEditPostController');
                //},
                controller: 'addEditPostController'
            }).when('/editPost/:postId', {
                templateUrl: '/app/controllers/addEditPostController/addEditPostController.html',
                //templateUrl: function () {
                //    return templateFactory.get('addEditPostController');
                //},
                controller: 'addEditPostController'
            }).otherwise({
                redirectTo: '/'
            });
    }]);
})(angular);