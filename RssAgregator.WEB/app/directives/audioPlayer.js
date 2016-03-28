; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('audioPlayer', ['onlineRadioService', '$window', '$filter',
		function (onlineRadioService, $window, $filter) {
		    return {
		        restrict: 'E',
		        templateUrl: 'app/directives/audioPlayer.html',
		        scope: {
		        },
		        link: function (scope, element, attrs) {
		            scope.playListTabOpened = true;
		            scope.searchListTabOpened = false;

		            scope.searchQuestion = null;
		            scope.pageNumber = 1;

		            scope.player = soundManager;

		            scope.currentPlayingSong;
		            scope.currentindex = 0;

		            scope.playList = [];
		            scope.searchResult = [];

		            scope.repeatAllPlayList = false;
		            scope.shufflePlayList = false;

		            scope.addToPlayList = function (listEl) {
		                if (!listEl.alreadyInPlayList) {
		                    var elToAdd = angular.copy(listEl);

		                    elToAdd.audioObject = soundManager.createSound({
		                        id: elToAdd.id.toString() + Math.floor(Math.random() * 100000).toString(),
		                        url: elToAdd.link,
		                        autoPlay: false,
		                        onfinish: scope.playNext
		                    });

		                    scope.playList.push(elToAdd);

		                    listEl.alreadyInPlayList = true;
		                }
		            };

		            scope.download = function (listEl) {
		                var expectedElement = angular.element('#DownloadFrame');
		                if (expectedElement.length > 0) {
		                    expectedElement.remove();
		                }

		                var downloadFrame = angular.element('<iframe/>', {
		                    src: listEl.link,
		                    style: 'display:none;',
		                    id: 'DownloadFrame'
		                });

		                angular.element('body').append(downloadFrame);
		            };

		            scope.play = function (listEl) {
		                scope.player.stopAll();

		                scope.currentPlayingSong = listEl;
		                listEl.audioObject.play();
		            };

		            scope.playNext = function () {
		                scope.player.stopAll();
		            };

		            scope.playPrevious = function () {
		                scope.player.stopAll();

		            };

		            scope.payPause = function () {
		                if (scope.playList && scope.playList.length) {
		                    if (!scope.currentPlayingSong) {
		                        scope.currentPlayingSong = scope.playList[0];
		                    }
		                    if (scope.currentPlayingSong.audioObject.playState === 0) {
		                        scope.currentPlayingSong.audioObject.play();
		                    } else {
		                        if (scope.currentPlayingSong.audioObject.paused) {
		                            scope.currentPlayingSong.audioObject.resume();
		                        } else {
		                            scope.currentPlayingSong.audioObject.pause();
		                        }
		                    }
		                }
		            };

		            scope.shuffle = function () {
		                scope.shufflePlayList != scope.shufflePlayList;
		            };

		            scope.repeatAll = function () {
		                scope.repeatAllPlayList != scope.repeatAllPlayList;
		            };

		            scope.getCurrentSongName = function () {
		                return scope.currentPlayingSong ? scope.currentPlayingSong.artist + ' - ' + scope.currentPlayingSong.name : '';
		            };

		            scope.search = function () {
		                onlineRadioService.get('GetSongs', { Question: scope.searchQuestion, PageNumber: scope.pageNumber }).then(function (serverResult) {
		                    if (serverResult.sucessResult) {
		                        scope.searchResult = [];

		                        angular.forEach(serverResult.DataResult, function (value) {
		                            scope.searchResult.push({
		                                name: value.Name,
		                                artist: value.Artist,
		                                link: value.Link,
		                                alreadyInPlayList: false
		                            });
		                        });

		                        scope.playListTabOpened = false;
		                        scope.searchListTabOpened = true;
		                    } else {
		                        serverResult.showInfoMessage();
		                    }
		                });
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