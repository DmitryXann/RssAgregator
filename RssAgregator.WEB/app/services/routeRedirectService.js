; (function (angular) {
    'use strict';

    angular.module('app')
        .service('routeRedirectService', ['$location',
            function ($location) {

                this.redirect = function (viewFilter, id) {
                    if (!viewFilter) {
                        $location.url('/');
                    } else {
                        var searchObj = {
                            filter: viewFilter
                        };

                        if (id) {
                            searchObj.id = id;
                        }

                        $location.search(searchObj);
                    }
                };
            }]);
})(angular);