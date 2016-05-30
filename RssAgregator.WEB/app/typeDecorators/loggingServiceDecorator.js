; (function (angular) {
    'use strict';

    angular.module('app')
        .decorator('loggingService', ['$delegate', '$q', 'userInfoFactory',
            function ($delegate, $q, userInfoFactory) {
                function ExtendedLoggingService() {
                    var that = this;

                    that.logUserActivity = function (actvityType) {
                        var deferred = $q.defer();

                        userInfoFactory.getUserInfo().then(function (locationResult) {
                            that.post('LogUserActivity', {
                                Country: locationResult.country,
                                City: locationResult.city,
                                Region: locationResult.region,
                                Organization: locationResult.org,
                                Activity: actvityType
                            }).then(function (serverResult) {
                                deferred.resolve(serverResult);
                            });
                        });

                        return deferred.promise;
                    };
                }

                ExtendedLoggingService.prototype = $delegate;

                return new ExtendedLoggingService();
            }]);
})(angular);