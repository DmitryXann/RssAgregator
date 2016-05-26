; (function (angular) {
    'use strict';

    angular.module('app')
        .decorator('userService', ['$delegate', '$q',
            function ($delegate, $q) {
                function ExtendedUserService() {
                    this.userIsLoggedInAsync = function () {
                        var deferred = $q.defer();
                        this.get('GetUserData').then(function (serverResult) {
                            if (serverResult.sucessResult) {
                                if (serverResult.DataResult != null) {
                                    deferred.resolve(true);
                                
                                } else {
                                    deferred.resolve(false);
                                }
                            } else {
                                serverResult.showInfoMessage();
                                deferred.resolve(false);
                            }
                        });

                        return deferred.promise;
                    };

                    this.getUserDataAsync = function () {
                        return this.get('GetUserData');
                    };
                }

                ExtendedUserService.prototype = $delegate;
                
                return new ExtendedUserService();
            }]);
})(angular);