; (function (angular) {
    'use strict';

    angular.module('app')
        .factory('genericModalFactory', ['$uibModal', 'templateFactory',
		    function ($uibModal, templateFactory) {
		        return {
		            show: function (modalTitle, okButtonName, cancelButtonName, modalHtmlContent, modalViewData, size, windowClass) {
		                var modalInstance = $uibModal.open({
		                    controller: 'genericModalController',
		                    templateUrl: function () {
		                        return templateFactory.get('genericModalFactory');
		                    },
		                    backdrop: 'static',
		                    keyboard: false,
		                    size: size,
		                    windowClass: windowClass,
		                    resolve: {
		                        modalOptions: function () {
		                            return {
		                                modalTitle: modalTitle,
		                                okButtonName: okButtonName,
		                                cancelButtonName: cancelButtonName,
		                                modalHtmlContent: modalHtmlContent,
		                                modalViewData: modalViewData
		                            };
		                        }
		                    }
		                });

		                return modalInstance.result;
		            }
		        };
		    }]).controller('genericModalController', ['$scope', '$uibModalInstance', 'modalOptions', 'uiSettings', '$sce',
            function ($scope, $uibModalInstance, modalOptions, uiSettings, $sce) {
                $scope.uiSettings = uiSettings;
                $scope.$sce = $sce;

                $scope.modalTitle = modalOptions.modalTitle;
                $scope.modalHtmlContent = modalOptions.modalHtmlContent;

                $scope.okButtonName = modalOptions.okButtonName;
                $scope.cancelButtonName = modalOptions.cancelButtonName;

                $scope.okButtonDisabled = false;
                $scope.cancelButtonDisabled = false;


                $scope.modalViewData = modalOptions.modalViewData;
                $scope.modalData = {};

                $scope.changeOkButtonState = function (buttonDisabled) {
                    $scope.okButtonDisabled = buttonDisabled;
                };

                $scope.changeCancelButtonState = function (buttonDisabled) {
                    $scope.cancelButtonDisabled = buttonDisabled;
                };

                $scope.ok = function () {
                    $uibModalInstance.close($scope.modalData);
                };

                $scope.cancel = function () {
                    if ($scope.cancelButtonName) {
                        $uibModalInstance.dismiss($scope.modalData);
                    } else {
                        $uibModalInstance.close($scope.modalData);
                    }
                };
            }]);
})(angular);