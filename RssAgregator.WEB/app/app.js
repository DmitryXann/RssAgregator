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
        function decimalAdjust(type, value, exp) {
            if (angular.isUndefined(exp) || +exp === 0) {
                return Math[type](value);
            }

            value = +value;
            exp = +exp;

            if (isNaN(value) || !(angular.isNumber(exp) && exp % 1 === 0)) {
                return NaN;
            }

            value = value.toString().split('e');
            value = Math[type](+(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp)));

            value = value.toString().split('e');

            return +(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp));
        }

        if (!Math.round10) {
            Math.round10 = function (value, exp) {
                return decimalAdjust('round', value, exp);
            };
        }
    }]);
})(angular);