; (function (angular) {
    'use strict';

    angular.module('app')
        .service('notificationService', ['ResultCodeEnum',
            function (ResultCodeEnum) {
                this.notify = function (serverResult) {
                    if (angular.isUndefined(serverResult) || serverResult == null) {
                        this.error("serverResult is null or indefined");
                    } else if (angular.isUndefined(serverResult.InfoResult) || serverResult.InfoResult == null) {
                        this.error("serverResult.InfoResult is null or indefined");
                    } else {
                        switch (serverResult.InfoResult.ResultCode) {
                            case ResultCodeEnum.Unknown:
                                this.error(serverResult.InfoResult.ResultMessage);
                            break;
                            case ResultCodeEnum.Success:
                                this.success(serverResult.InfoResult.ResultMessage);
                            break;
                            case ResultCodeEnum.Error:
                                this.error(serverResult.InfoResult.ResultMessage);
                            break;
                            case ResultCodeEnum.Warning:
                                this.warning(serverResult.InfoResult.ResultMessage);
                            break;
                        }
                    }
                };

                this.error = function (message) {
                    toastr.error(message);
                };

                this.warning = function (message) {
                    toastr.warning(message);
                };

                this.info = function (message) {
                    toastr.info(message);
                };

                this.success = function (message) {
                    toastr.success(message);
                };
            }]);
})(angular);