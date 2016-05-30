; (function (angular, $) {
    'use strict';

    angular.module('app')
        .service('$exceptionHandler', ['$log',
            function ($log) {
                return function (exception, cause) {
                    try {
                        $log.error.apply($log, arguments);
                        $.post("/api/Logging/LogFEError", { ErrorMsg: '{0} {1}'.format(exception, cause || '') });
                    } catch (ex) {
                        //something gone really terrible, there are no possibility to log data.
                        if (window.console && window.console.error) {
                            window.console.error(ex + ', inner exception: ' + exception);
                        }
                    }
                };
            }]);
})(angular, $);