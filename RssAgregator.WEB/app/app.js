; (function (angular, videojs, soundManager) {
    'use strict';

    angular.module('app', [
        'ngAnimate',
        'ngRoute',
        'ngResource',
        'ngSanitize',
        'ngTouch',
        'ui.bootstrap'
    ]).run(['$rootScope', '$exceptionHandler', '$window', 'templateFactory', 'viewTemplates', 'loggingService', 'ActivityEnum', '$http',
        function ($rootScope, $exceptionHandler, $window, templateFactory, viewTemplates, loggingService, ActivityEnum, $http) {
            $rootScope.invalidDataOnPage = false;

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
                    $exceptionHandler('An error ha occurred: {0}, url: {1}, line: {2}'.format(errorMsg, url, lineNumber));
                    return true;
                };

                //videojs init
                videojs.options.flash.swf = '/Scripts/video-js-5.9.2/video-js.swf';

                //soundmanagerv2 init
                soundManager.setup({
                    url: '/Scripts/soundmanagerv2/',
                    flashVersion: 9,
                    preferFlash: false
                });

                //get all templates from DB JUST FOR DEBUG
                for (var el in viewTemplates) {
                    //templateFactory.put(el);
                }

                $rootScope.$on('invalidDataOnPage', function ($event, data) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $rootScope.invalidDataOnPage = data;
                });


                //Logging of app open
                loggingService.logUserActivity(ActivityEnum.Open).then(function (serverResult) {
                    if (!serverResult.sucessResult || !serverResult.DataResult) {
                        $exceptionHandler("Can`t log user data");
                    }
                });
            }

            init();
    }]);
})(angular, videojs, soundManager);