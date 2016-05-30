; (function (angular, $) {
    'use strict';

    angular.module('app')
		.directive('login', ['uiSettings', 'templateFactory', 'userService', 'notificationService',
		function (uiSettings, templateFactory, userService, notificationService) {
		    return {
		        restrict: 'E',
		        templateUrl: function () {
		            return templateFactory.get('login');
		        },
		        scope: {
		        },
		        link: function (scope, element, attrs) {
		            scope.userIsLoggedIn = false;
		            scope.userName;

		            scope.userNameAlreadyExistsError = uiSettings.LOGIN_UserNameAlreadyExists;
		            scope.tooShortUserName = uiSettings.LOGIN_TooShortUserName;

		            scope.emailAlreadyExistsError = uiSettings.LOGIN_EmailAlreadyExists;
		            scope.incorrectEmailAdress = uiSettings.LOGIN_IncorrectEmailAddress;

		            scope.incorrectPasswords = uiSettings.LOGIN_IncorrectUserPassword;
                    
		            scope.userData = {
		                userName: '',
		                userNameIsValid: true,
		                minNameLength: 0,

		                password: '',
		                secondTimePassword: '',
		                passwordIsValid: true,
		                minPasswordLength: 5,

		                email: '',
		                emailIsValid: true,
		                emailIsNotTaken: true,

		                stayInTheSystem: false,

		                userDataIsValid: function (isCreation) {
		                    if (isCreation) {
		                        return this.userNameIsValid && this.passwordIsValid && this.emailIsValid && this.emailIsNotTaken &&
                                       this.userName && this.email && this.password && this.password === this.secondTimePassword;
		                    } else {
		                        return this.userNameIsValid && this.passwordIsValid &&
                                       this.userName && this.password;
		                    }
		                }
                    };

		            scope.login = function () {
		                if (scope.userData.userName && scope.userData.password) {
		                    userService.post('Login', {
		                        Name: scope.userData.userName,
		                        Password: $.md5(scope.userData.password),
		                        CreateCookie: scope.userData.stayInTheSystem
		                    }).then(function (serverResult) {
		                        if (serverResult.sucessResult) {
		                            if (serverResult.DataResult) {
		                                scope.userIsLoggedIn = true;
		                                scope.userName = serverResult.DataResult;
		                            } else {
		                                notificationService.warning(uiSettings.IncorrectUserNameOrPassword);
		                            }
		                        } else {
		                            serverResult.showInfoMessage();
		                        }
		                    });
		                }
		            };

		            scope.logOut = function () {
		                userService.get('LogOut').then(function (serverResult) {
		                    if (serverResult.sucessResult) {
		                        if (serverResult.DataResult) {
		                            scope.userIsLoggedIn = false;
		                            scope.userName = null;
		                        }
		                    } else {
		                        serverResult.showInfoMessage();
		                    }
		                });
		            };

		            scope.create = function () {
		                if (scope.userData.userName && scope.userData.password && scope.userData.secondTimePassword && scope.userData.email) {
		                    userService.post('CreateUpdate', {
		                        Name: scope.userData.userName,
		                        Email: scope.userData.email,
		                        Password: $.md5(scope.userData.password),
		                        CreateCookie: scope.userData.stayInTheSystem
		                    }).then(function (serverResult) {
		                        if (serverResult.sucessResult) {
		                            scope.userIsLoggedIn = true;
		                            scope.userName = serverResult.DataResult;
		                        } else {
		                            serverResult.showInfoMessage();
		                        }
		                    });
		                }
		            };

		            scope.validateName = function (nameToValidate, expectedMinLength) {
		                if (nameToValidate && nameToValidate.length >= +expectedMinLength) {
		                    userService.post('UserNameExists', { InputParams: nameToValidate }).then(function (serverResult) {
		                        if (serverResult.sucessResult) {
		                            scope.userData.userNameIsValid = !serverResult.DataResult;
		                        } else {
		                            serverResult.showInfoMessage();
		                        }
		                    });
		                }
		            };

		            scope.validateEmail = function (emailToValidate, expectedMinLength) {
		                if (emailToValidate && emailToValidate.length >= +expectedMinLength) {
		                    var validationRegExp = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

		                    scope.userData.emailIsValid = validationRegExp.test(emailToValidate);
		                    if (scope.userData.emailIsValid) {
		                        userService.post('EmailExists', { InputParams: emailToValidate }).then(function (serverResult) {
		                            if (serverResult.sucessResult) {
		                                scope.userData.emailIsNotTaken = !serverResult.DataResult;
		                            } else {
		                                serverResult.showInfoMessage();
		                            }
		                        });
		                    }
		                }
		            };

		            function init() {
		                userService.getUserDataAsync().then(function (serverResult) {
		                    if (serverResult.sucessResult) {
		                        if (serverResult.DataResult != null) {
		                            scope.userIsLoggedIn = true;
		                            scope.userName = serverResult.DataResult.Name;
		                        }
		                    } else {
		                        serverResult.showInfoMessage();
		                    }
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular, $);