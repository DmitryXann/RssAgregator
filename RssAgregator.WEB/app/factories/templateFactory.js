; (function (angular) {
    'use strict';

    angular.module('app')
        .service('templateFactory', ['$templateCache', 'templateService', 'viewTemplates',
            function ($templateCache, templateService, viewTemplates) {

                function put(templateName) {
                    if (!$templateCache.get(templateName)) {
                        templateService.get('GetTemplate', templateName).then(function (serverResult) {
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

                    return viewTemplates[templateName]; //FOR DEBUG
                    //return template ? templateName : viewTemplates[templateName];
                }

                return {
                    put: put,
                    get: get
                };
            }]);
})(angular);