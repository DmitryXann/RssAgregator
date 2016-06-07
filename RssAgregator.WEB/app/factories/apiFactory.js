; (function (angular) {
    'use strict';

    angular.module('app')
        .factory('apiFactory', ['$resource', 'ResultCodeEnum', 'notificationService', 'uiSettings',
            function ($resource, ResultCodeEnum, notificationService, uiSettings) {
                var useCache;

                function transformResponseProcessing(data, headers, status) {
                    if (status == 200) {
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
                    } else {
                        notificationService.error(uiSettings.ExceptionOccured);

                        return {
                            sucessResult: false,
                            showInfoMessage: function () {
                                notificationService.error(uiSettings.ExceptionOccured);
                            },
                            DataResult: null,
                            InfoResult: null
                        }
                    }
                }

                function create(controllerName, useGetCache) {
                    useCache = useGetCache;

                    return {
                        get: function (actionName, params) {
                            var useGetCache = false;

                            if (useCache === false) {
                                useCache = true;
                            } else if (useCache === true) {
                                useGetCache = true;
                            }

                            return $resource('/api/' + controllerName + '/' + actionName + '/:id',
                                { id: '@id' }, {
                                    get: {
                                        method: "GET",
                                        transformResponse: transformResponseProcessing,
                                        cache: useGetCache
                                    }
                            }).get(angular.isObject(params) ? params : { id: params }).$promise;
                        },
                        post: function (actionName, params) {
                            return $resource('/api/' + controllerName + '/' + actionName + '/:id',
                                { id: '@id' }, {
                                    save: {
                                        method: "POST",
                                        transformResponse: transformResponseProcessing
                                    }
                            }).save(params).$promise;
                        }
                    }
                }

                function resetCacheControl() {
                    useCache = false;
                }

                return {
                    create: create,
                    resetCacheControl: resetCacheControl
                };
            }]);
})(angular);