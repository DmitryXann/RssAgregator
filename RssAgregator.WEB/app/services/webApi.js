; (function (angular) {
    'use strict';

    angular.module('app').service('templateService', ['apiFactory', function (apiFactory) {
        return apiFactory.create('Template');
    }]);

    angular.module('app').service('newsService', ['apiFactory', function (apiFactory) {
        return apiFactory.create('News');
    }]);

    angular.module('app').service('onlineRadioService', ['apiFactory', function (apiFactory) {
        return apiFactory.create('OnlineRadio');
    }]);

    angular.module('app').service('navigationService', ['apiFactory', function (apiFactory) {
        return apiFactory.create('Navigation');
    }]);

    angular.module('app').service('loggingService', ['apiFactory', function (apiFactory) {
        return apiFactory.create('Logging');
    }]);

    angular.module('app').service('userService', ['apiFactory', function (apiFactory) {
        return apiFactory.create('User');
    }]);

})(angular);