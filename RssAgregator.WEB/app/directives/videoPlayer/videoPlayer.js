; (function (angular, videojs) {
    'use strict';

    angular.module('app')
		.directive('videoPlayer', ['$sce', 'uiSettings', '$log',
		function ($sce, uiSettings, $log) {
		    return {
		        restrict: 'E',
		        templateUrl: 'app/directives/videoPlayer/videoPlayer.html',
		        scope: {
		            link: '@',
		            poster: '@',

		            width: '@',
		            height: '@',

		            controls: '@',
		            autoplay: '@',
		            preload: '@',
		            loop: '@'
		        },
		        link: function (scope, element, attrs) {
		            scope.video;

		            scope.cantPlayVideo = $sce.trustAsHtml(uiSettings.VideoJSCantPlay);

		            scope.getContentType = function () {
		                var result = '';

		                if (scope.link) {
		                    var linkLength = scope.link.length;

		                    var lastIndexOfMp4 = scope.link.toLowerCase().lastIndexOf('.mp4');
		                    if (lastIndexOfMp4 === linkLength - 4) {
		                        result = 'video/mp4';
		                    } else {
		                        var lastIndexOfWebm = scope.link.toLowerCase().lastIndexOf('.webm');
		                        if (lastIndexOfWebm === linkLength - 5) {
		                            result = 'video/webm';
		                        } else {
		                            $log.error(uiSettings.VideoJSNotSupportedFormat);
		                        }
		                    }
		                }
		                
		                return result;
		            };

		            function init() {
		                var videoJsOptions = {};

		                if (scope.width) {
		                    videoJsOptions.width = scope.width;
		                }

		                if (scope.height) {
		                    videoJsOptions.height = scope.height;
		                }

		                if (scope.poster) {
		                    videoJsOptions.poster = scope.poster;
		                }

		                if (scope.controls) {
		                    videoJsOptions.controls = scope.controls;
		                }

		                if (scope.autoplay) {
		                    videoJsOptions.autoplay = scope.autoplay;
		                }

		                if (scope.preload) {
		                    videoJsOptions.preload = scope.preload;
		                }

		                if (scope.loop) {
		                    videoJsOptions.loop = scope.loop;
		                }

		                videojs(element.children('video')[0], videoJsOptions, function () {
		                    this.src([{ type: scope.getContentType(), src: $sce.trustAsResourceUrl(scope.link) }]);
		                    scope.video = this;
		                });


		                scope.$on('$destroy', function () {
		                    scope.video.dispose();
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular, videojs);