; (function (angular) {
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

		            scope.cantPlayVideo = $sce.trustAsHtml(uiSettings.VideoJSCantPlay);
		            scope.videoLink = $sce.trustAsResourceUrl(scope.link);

		            scope.options = function () {
		                var result = '';

		                if (scope.controls) {
		                    if (result.length) {
		                        result += ', ';
		                    }

		                    result += '"controls": {0}'.format(scope.controls);
		                }

		                if (scope.autoplay) {
		                    if (result.length) {
		                        result += ', ';
		                    }

		                    result += '"autoplay": {0}'.format(scope.autoplay);
		                }

		                if (scope.preload) {
		                    if (result.length) {
		                        result += ', ';
		                    }

		                    result += '"preload": {0}'.format(scope.preload);
		                }

		                if (scope.loop) {
		                    if (result.length) {
		                        result += ', ';
		                    }

		                    result += '"loop": {0}'.format(scope.loop);
		                }

		                return '{{0}}'.format(result);
		            };

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
		                
		            }

		            init();
		        }
		    };
		}]);
})(angular);