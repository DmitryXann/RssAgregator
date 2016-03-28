; (function (angular) {
    'use strict';

    angular.module('app')
        .constant('templateType', {
            Post: 1,
            Header: 2
        }).constant('ResultCodeEnum', {
            Unknown: 0,
            Success: 1,
            Error: 2,
            Warning: 3
        });
})(angular);