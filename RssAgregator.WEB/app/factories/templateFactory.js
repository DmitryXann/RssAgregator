; (function (angular) {
    'use strict';

    angular.module('app')
        .service('templateFactory', ['$templateCache', 'templateService', 'viewTemplates', '$q',
            function ($templateCache, templateService, viewTemplates, $q) {

                function getTemplateFroService(templateName) {
                    return templateService.get('GetTemplate', templateName);
                }


                function put(templateName) {
                    if (!$templateCache.get(templateName)) {
                        getTemplateFroService(templateName).then(function (serverResult) {
                            if (serverResult.sucessResult) {
                                $templateCache.put(templateName, serverResult.DataResult.View);
                            } else {
                                serverResult.showInfoMessage();
                            }
                        });
                    }
                }

                function get(templateName) {
                    var template = $templateCache.get(templateName);
                    return template ? templateName : viewTemplates[templateName];
                }

                function getAsync(templateName) {
                    var deferred = $q.defer();
                    var template = $templateCache.get(templateName);

                    if (template) {
                        deferred.resolve(template);
                    } else {
                        getTemplateFroService(templateName).then(function (serverResult) {
                            if (serverResult.sucessResult) {
                                $templateCache.put(templateName, serverResult.DataResult.View);
                                deferred.resolve(serverResult.DataResult.View);
                            } else {
                                serverResult.showInfoMessage();
                                deferred.resolve(null);
                            }
                        });
                    }

                    return deferred.promise;
                }

                return {
                    put: put,
                    get: get,
                    getAsync: getAsync
                };
            }]);
})(angular);