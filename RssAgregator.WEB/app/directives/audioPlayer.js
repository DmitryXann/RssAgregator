; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('audioPlayer', ['prostoPleerService',
		function (prostoPleerService) {
		    return {
		        restrict: 'E',
		        template: '<div ng-click="search(\'rammstein\')">Search!</div><div ng-click="play()">Play!</div>',
		        scope: {
		        },
		        link: function (scope, element, attrs) {
		            scope.player = soundManager;

		            scope.currentFile;
		            scope.currentindex = 0;

		            scope.playList = [];

		            scope.search = function (question, pageNumber) {
		                prostoPleerService.get('GetSongs', { Question: question, PageNumber: pageNumber ? pageNumber : 1, ResultLimit: 50 }).then(function (serverResult) {
		                    JSON.parse(serverResult.Data).tracks.map(function (value) {
		                        scope.playList.push({
		                            artist: value.artist,
		                            track: value.track,
		                            file: value.file,
		                            bitrate: value.bitrate,
		                            audioObject: soundManager.createSound({
		                                url: value.file,
		                                autoPlay: false,
		                            })
		                        });
		                    });

		                });
		            };

		            scope.play = function () {
		                scope.playList[0].audioObject.play();
		            };

		            function init() {
		                scope.player.setup({
		                    url: '/Scripts/soundmanager2/', 
		                    flashVersion: 9,
		                    preferFlash: false,
		                    onready: function () {
		                        //scope.search('Rammstein');
		                    }
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular);