; (function (angular) {
    'use strict';

    angular.module('app', [
        'ngAnimate',
        'ngRoute',
        'ngResource',
        'ngSanitize',
        'ngTouch',
        'ui.bootstrap'
    ]).run(['$rootScope', '$log', '$window',
        function ($rootScope, $log, $window) {

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

        function init() {
            //global round10 Math prototype
            if (!Math.round10) {
                Math.round10 = function (value, exp) {
                    return decimalAdjust('round', value, exp);
                };
            }

            //global format String prototype
            if (!String.prototype.format) {
                String.prototype.format = function () {
                    var args = arguments;

                    return this.replace(/{(\d+)}/g, function (match, number) {
                        return !angular.isUndefined(args[number]) ? args[number] : match;
                    });
                };
            }

            //global error handling
            $window.onerror = function (errorMsg, url, lineNumber) {
                $log.error('An error ha occurred: {0}, url: {1}, line: {3}'.format(errorMsg, url, lineNumber));
                return false; //TODO: change to true
            };

            //videojs init
            videojs.options.flash.swf = '/Scripts/video-js-5.9.2/video-js.swf';

            //soundmanagerv2 init
            soundManager.setup({
                url: '/Scripts/soundmanagerv2/',
                flashVersion: 9,
                preferFlash: false
            });
        }

        init();
    }]);
})(angular);