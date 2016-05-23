; (function (angular) {
    'use strict';

    angular.module('app')
        .service('userAuthService', ['userService', 'uiSettings', '$q',
            function (userService, uiSettings, $q) {
                this.userIsLoggedIn = function() {
                    return !angular.isUndefined(uiSettings.userName) && uiSettings.userName != null && uiSettings.userName !== '';
                };

                this.getUserDataAsync = function () {
                    return userService.get('GetUserData');
                };
            }]);
})(angular);