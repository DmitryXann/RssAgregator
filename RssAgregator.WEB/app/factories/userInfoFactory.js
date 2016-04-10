; (function (angular) {
    'use strict';

    angular.module('app')
        .service('userInfoFactory', ['$http', '$q',
            function ($http, $q) {
                var locationData;

                function getUserInfo() {
                    var defferer = $q.defer();

                    if (!locationData) {
                        $http({
                            methid: 'GET',
                            url: 'http://ipinfo.io'
                        }).then(function (locationResult) {
                            locationData = locationResult.data
                            defferer.resolve(locationData);
                        }, function (locationResult) {
                            locationData = null;
                            defferer.reject(locationResult);
                        });
                    } else {
                        defferer.resolve(locationData);
                    }

                    return defferer.promise;
                }

                return {
                    getUserInfo: getUserInfo
                };
            }]);
})(angular);