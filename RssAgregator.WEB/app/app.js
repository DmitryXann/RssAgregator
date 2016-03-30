; (function (angular) {
    'use strict';

    angular.module('app', [
        'ngAnimate',
        'ngRoute',
        'ngResource',
        'ngSanitize',
        'ngTouch',
        'ui.bootstrap'
    ]).run(['$rootScope', function ($rootScope) {
        // Global error handler
        //window.onerror = function (msg, url, line) {
        //    //toastr.error("Error: " + msg + "\nurl: " + url + "\nline #: " + line);
        //    return true;
        //};
    }]);
})(angular);