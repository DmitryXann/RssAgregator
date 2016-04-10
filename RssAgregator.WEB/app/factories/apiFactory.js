; (function (angular) {
    'use strict';

    angular.module('app')
        .factory('apiFactory', ['$resource', 'ResultCodeEnum', 'notificationService',
            function ($resource, ResultCodeEnum, notificationService) {
                function transformResponseProcessing(data, headers) {
                    var serverResult = angular.fromJson(data);
                    var serverResultData = serverResult.Data;

                    return {
                        sucessResult: !angular.isUndefined(serverResultData) && serverResultData != null &&
                                      !angular.isUndefined(serverResultData.InfoResult) && serverResultData.InfoResult != null &&
                                      serverResultData.InfoResult.ResultCode === ResultCodeEnum.Success,
                        showInfoMessage: function () {
                            notificationService.notify(serverResultData);
                        },
                        DataResult: serverResultData.DataResult,
                        InfoResult: serverResultData.InfoResult
                    }
                }

                function create(controllerName) {
                    return {
                        get: function (actionName, params) {
                            return $resource('api/' + controllerName + '/' + actionName + '/:id',
                                { id: '@id' }, {
                                    get: {
                                        method: "GET",
                                        transformResponse: transformResponseProcessing,
                                }
                            }).get(params).$promise;
                        },
                        post: function (actionName, params) {
                            return $resource('api/' + controllerName + '/' + actionName + '/:id',
                                { id: '@id' }, {
                                    save: {
                                        method: "POST",
                                        transformResponse: transformResponseProcessing
                                    }
                            }).save(params).$promise;
                        }
                    }
                }

                return {
                    create: create
                };
            }]);
})(angular);