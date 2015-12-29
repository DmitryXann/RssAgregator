; (function (angular) {
    'use strict';

    angular.module('app').service('templateService', ['apiFactory', function (apiFactory) {
        return apiFactory.create('Template');
    }]);

    angular.module('app').service('newsService', ['apiFactory', function (apiFactory) {
        return apiFactory.create('News');
    }]);

    angular.module('app').service('prostoPleerService', ['apiFactory', function (apiFactory) {
        return apiFactory.create('ProstoPleer');
    }]);

})(angular);