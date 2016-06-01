; (function (angular, ContentTools) {
    'use strict';

    angular.module('app')
		.directive('addEditPost', ['newsService', '$routeParams', '$location', '$timeout', '$sce', 'uiSettings', 'templateFactory',
		function (newsService, $routeParams, $location, $timeout, $sce, uiSettings, templateFactory) {
		    return {
		        restrict: 'E',
		        templateUrl: function () {
		            return templateFactory.get('addEditPost');
		        },
		        scope: {
		        },
		        link: function (scope, element, attrs) {
		            scope.editor;

		            scope.postContent;
		            scope.originalData;

		            scope.postName = '';
		            scope.invalidPostName = true;
		            scope.invalidPostNameMessage = uiSettings.POST_InvalidPostNameData;

		            scope.postTags = '';
		            scope.invalidPostTag = true;
		            scope.invalidPostTagMessage = uiSettings.POST_InvalidPostTagsData;

		            scope.isAdult = false;

		            scope.postDataOnChange = function (minLength, sourceData, validation) {
		                if (!angular.isUndefined(scope[validation]) && !angular.isUndefined(scope[sourceData])) {
		                    scope[validation] = scope[sourceData].length < minLength;
		                    scope.$emit('invalidDataOnPage', scope.invalidPostName || scope.invalidPostTag);
		                }
		            };

		            function init() {
		                var closeModal = function () {
		                    $timeout(function () {
		                        scope.$emit('closeModalWindow', true);
		                    }, 500);
		                };

		                var savedEditorEventHandler = function (event) {
		                    var that = this;
		                    var postContent = '';
		                    var regions = event.detail().regions;

		                    that.busy(true);

		                    if (Object.keys(regions).length !== 0) {
		                        for (var name in regions) {
		                            if (regions.hasOwnProperty(name)) {
		                                postContent += regions[name];
		                            }
		                        }
		                    }

		                    if ($routeParams.postId &&
                                (scope.postName !== scope.originalData.PostName || scope.postTags !== scope.originalData.PostTags || scope.isAdult !== scope.originalData.AdultContent) ||
                                postContent) {
		                        newsService.post('AddEditNewsItem', {
		                            PostName: scope.postName,
		                            PostTags: scope.postTags,
		                            AdultContent: scope.isAdult,
		                            PostContent: postContent || scope.originalData.PostContent,
		                            IsNewOne: angular.isUndefined($routeParams.postId)
		                        }).then(function (serverResult) {
		                            that.busy(false);

		                            if (serverResult.sucessResult) {
		                                new ContentTools.FlashUI('ok');
		                                closeModal();
		                            } else {
		                                new ContentTools.FlashUI('no');
		                                serverResult.showInfoMessage();
		                            }
		                        });
		                    } else {
		                        that.busy(false);

		                        new ContentTools.FlashUI('ok');
		                        closeModal();
		                    }
		                };

		                var stopEdtiorEventHandler = function (event) {
		                    if (!event.detail().save) {
		                        closeModal();
		                    }
		                };

		                var initializeEditor = function () {
		                    scope.editor = ContentTools.EditorApp.get();
		                    scope.editor.init('*[data-editable]', 'data-name');

		                    scope.editor.addEventListener('saved', savedEditorEventHandler);
		                    scope.editor.addEventListener('stop', stopEdtiorEventHandler);

		                    scope.editor.ignition().busy(true);
		                };


		                if ($routeParams.postId) {
		                    newsService.get('NewsItemSearch', $routeParams.postId).then(function (serverResult) {
		                        if (serverResult.sucessResult) {
		                            scope.originalData = serverResult.DataResult;

		                            scope.postContent = $sce.trustAsHtml(serverResult.DataResult.PostContent);
		                            scope.postName = serverResult.DataResult.PostName;
		                            scope.postTags = serverResult.DataResult.PostTags.join(',');
		                            scope.isAdult = serverResult.DataResult.AdultContent;

		                            initializeEditor();
		                            $timeout(function () {
		                                scope.editor.ignition().edit();
		                            }, 500);
		                        } else {
		                            serverResult.showInfoMessage();
		                        }
		                    });
		                } else {
		                    scope.postContent = null;
		                    initializeEditor();

		                    $timeout(function () {
		                        scope.editor.ignition().edit();
		                        scope.postDataOnChange(2, 'postName', 'invalidPostName');
		                    }, 100);
		                }

		                scope.$on('$destroy', function () {
		                    if (scope.editor) {
		                        scope.editor.removeEventListener('saved', savedEditorEventHandler);
		                        scope.editor.removeEventListener('stop', stopEdtiorEventHandler);

		                        scope.editor.destroy();
		                    }
		                });

		                scope.$on('$routeChangeStart', function ($event) {
		                    if (scope.editor.ignition().state() === 'editing') {
		                        $event.preventDefault();
		                    }
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular, ContentTools);