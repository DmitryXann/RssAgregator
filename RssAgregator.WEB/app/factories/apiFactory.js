; (function (angular) {
    'use strict';

    angular.module('app')
        .factory('apiFactory', ['$resource',
            function ($resource) {
                function create(controllerName) {

                    return {
                        get: function (actionName, params) {
                            return $resource('api/' + controllerName + '/' + actionName + '/:id', { id: '@id' }).get(params).$promise;
                        }
                    }
                }

                return {
                    create: create
                };
            }]);
})(angular);