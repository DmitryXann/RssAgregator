; (function (angular, ContentTools) {
    'use strict';

    angular.module('app')
        .controller('addEditPostController', ['$scope', 'newsService',
            function ($scope, newsService) {
                $scope.editor;

                $scope.postName = '';
                $scope.postTags = '';
                $scope.isAdult = false;

                function init() {
                    $scope.editor = ContentTools.EditorApp.get();
                    $scope.editor.init('*[data-editable]', 'data-name');

                    $scope.editor.addEventListener('saved', function (ev) {
                        var that = this;
                        // Check that something changed
                        var regions = ev.detail().regions;
                        if (Object.keys(regions).length == 0) {
                            return;
                        }

                        // Set the editor as busy while we save our changes
                        that.busy(true);

                        // Collect the contents of each region into a FormData instance
                        var postContent = '';
                        for (var name in regions) {
                            if (regions.hasOwnProperty(name)) {
                                postContent += regions[name];
                            }
                        }

                        newsService.post('AddNewsItem', {
                            PostName: $scope.postName,
                            PostTags: $scope.postTags,
                            AdultContent: $scope.isAdult,
                            PostContent: postContent
                        }).then(function (serverResult) {
                            that.busy(false);

                            if (serverResult.sucessResult) {
                                new ContentTools.FlashUI('ok');
                            } else {
                                new ContentTools.FlashUI('no');
                                serverResult.showInfoMessage();
                            }
                        });
                    });

                    $scope.$on('$destroy', function () {
                        $scope.editor.destroy();
                    });

                    $scope.$on('$routeChangeStart', function (event) {
                        $scope.editor.revert();
                        event.preventDefault();
                    });
                }

                init();
            }]);
})(angular, ContentTools);