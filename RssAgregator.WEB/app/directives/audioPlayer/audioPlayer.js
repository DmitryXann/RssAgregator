; (function (angular) {
    'use strict';

    angular.module('app')
		.directive('audioPlayer', ['onlineRadioService', '$window', '$filter', '$timeout', 'notificationService', 'uiSettings', 'userInfoFactory', '$rootScope', '$log',
		function (onlineRadioService, $window, $filter, $timeout, notificationService, uiSettings, userInfoFactory, $rootScope, $log) {
		    return {
		        restrict: 'E',
		        templateUrl: 'app/directives/audioPlayer/audioPlayer.html',
		        scope: {
		        },
		        link: function (scope, element, attrs) {
		            scope.player = soundManager;

		            scope.loadingResults = false;
		            scope.typeAheadNoResults = false;

		            scope.activceTabIndex = 0;
		            
		            scope.searchQuestion = null;
		            scope.pageNumber = 1;

		            scope.currentPlayingSong;
		            scope.currentIndex = 0;

		            scope.playList = [];
		            scope.searchResult = [];

		            scope.repeatAllPlayList = false;
		            scope.shufflePlayList = false;

		            //scope.download = function (listEl) {
		            //    var expectedElement = angular.element('#DownloadFrame');
		            //    if (expectedElement.length > 0) {
		            //        expectedElement.remove();
		            //    }

		            //    var downloadFrame = angular.element('<iframe/>', {
		            //        src: listEl.link,
		            //        style: 'display:none;',
		            //        id: 'DownloadFrame'
		            //    });

		            //    angular.element('body').append(downloadFrame);
		            //};

		            scope.play = function (listEl) {
		                scope.player.stopAll();

		                scope.currentIndex = scope.playList.indexOf(listEl);
		                scope.currentPlayingSong = listEl;
		                listEl.audioObject.play();
		            };

		            scope.payPause = function () {
		                if (!scope.currentPlayingSong && scope.playList && scope.playList.length) {
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
		            };

		            scope.playNext = function () {
		                if (scope.playList && scope.playList.length) {
		                    var ignoreSongChange = false;
		                    var expectedIndex = 0;

		                    if (scope.shufflePlayList) {
		                        expectedIndex = getRandomSong(0, scope.playList.length);
		                        if (expectedIndex == scope.currentIndex) {
		                            expectedIndex = getRandomSong(0, scope.playList.length);
		                        }
		                    } else {
		                        if (scope.currentIndex + 1 < scope.playList.length) {
		                            expectedIndex = scope.currentIndex + 1;
		                        } else if (scope.repeatAllPlayList) {
		                            expectedIndex = 0;
		                        } else {
		                            ignoreSongChange = true;
		                        }
		                    }

		                    if (!ignoreSongChange) {
		                        scope.player.stopAll();
		                        scope.currentIndex = expectedIndex;
		                        scope.currentPlayingSong = scope.playList[scope.currentIndex];
		                        scope.currentPlayingSong.audioObject.play();
		                    }
		                }
		            };

		            scope.playPrevious = function () {
		                if (scope.playList && scope.playList.length) {
		                    var ignoreSongChange = false;
		                    var expectedIndex = 0;

		                    if (scope.shufflePlayList) {
		                        expectedIndex = getRandomSong(0, scope.playList.length);
		                        if (expectedIndex == scope.currentIndex) {
		                            expectedIndex = getRandomSong(0, scope.playList.length);
		                        }
		                    } else {
		                        if (scope.currentIndex - 1 >= 0) {
		                            expectedIndex = scope.currentIndex - 1;
		                        } else if (scope.repeatAllPlayList) {
		                            expectedIndex = scope.playList.length - 1;
		                        } else {
		                            ignoreSongChange = true;
		                        }
		                    }

		                    if (!ignoreSongChange) {
		                        scope.player.stopAll();

		                        scope.currentIndex = expectedIndex;
		                        scope.currentPlayingSong = scope.playList[scope.currentIndex];
		                        scope.currentPlayingSong.audioObject.play();
		                    }
		                }
		            };

		            scope.seek = function (expectedPersentSeek) {
		                if (scope.currentPlayingSong) {
		                    var expecetMillisecond = (scope.currentPlayingSong.estimatedDuration * expectedPersentSeek) / 100;
		                    scope.currentPlayingSong.audioObject.setPosition(expecetMillisecond);
		                }
		            };

		            scope.shuffle = function () {
		                scope.shufflePlayList = !scope.shufflePlayList;
		            };

		            scope.repeatAll = function () {
		                scope.repeatAllPlayList = !scope.repeatAllPlayList;
		            };

		            
		            scope.search = function () {
		                 userInfoFactory.getUserInfo().then(function (locationResult) {
                             onlineRadioService.get('GetSongs', {
                                 Question: scope.searchQuestion,
                                 PageNumber: scope.pageNumber,
                                 City: locationResult.city,
                                 Country: locationResult.country
                             }).then(function (serverResult) {
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
                                     scope.activceTabIndex = 1;
                                 } else {
                                     serverResult.showInfoMessage();
                                 }
                             });
		                });
		            };

		            scope.addToPlayList = function (listEl) {
		                var elToAdd = angular.copy(listEl);

		                elToAdd.audioObject = soundManager.createSound({
		                    id: Math.floor(Math.random() * 100000).toString(),
		                    url: elToAdd.link,
		                    autoPlay: false,
		                    onfinish: scope.playNext,
		                    whileplaying: function () {
		                        var that = this;

		                        var minutes = Math.floor(that.position / 60000);
		                        var seconds = ((that.position % 60000) / 1000).toFixed(0);
		                        var expectedTime = (minutes < 10 ? '0' : '') + minutes + ":" + (seconds < 10 ? '0' : '') + seconds;

		                        if (elToAdd.progress !== expectedTime) {
		                            $timeout(function () {
		                                elToAdd.progress = that.position;
		                                elToAdd.userFriendlyProgress = expectedTime;
		                            });
		                        }
		                    },
		                    onload: function (success) {
		                        if (success) {
		                            var that = this;

		                            $timeout(function () {
		                                var minutes = Math.floor(that.duration / 60000);
		                                var seconds = ((that.duration % 60000) / 1000).toFixed(0);

		                                elToAdd.estimatedDuration = that.duration;
		                                elToAdd.userFriendlyEstimateDuration = (minutes < 10 ? '0' : '') + minutes + ":" + (seconds < 10 ? '0' : '') + seconds;
		                            });
		                        } else {
		                            notificationService.warning(uiSettings.CantPlaySong);
		                            $log.warn('Can`t play song: ' + elToAdd.link + ' song was added to the black list');

		                            userInfoFactory.getUserInfo().then(function (locationResult) {
		                                onlineRadioService.post('AddNotPlayebleSong', {
		                                    SongURL: elToAdd.link,
		                                    City: locationResult.city,
		                                    Country: locationResult.country
		                                }).then(function (serverResult) {
		                                    if (serverResult.sucessResult) {
		                                        if (serverResult.sucessResult) {
		                                            $timeout(function () {
		                                                notificationService.warning(uiSettings.SongInBlackList);
		                                            }, 500);
		                                        }
		                                    } else {
		                                        serverResult.showInfoMessage();
		                                    }
		                                });
		                            });
		                        }
		                    }
		                });

		                scope.playList.push(elToAdd);
		            };

		            scope.deleteFromPlayList = function (listEl) {
		                var index = scope.playList.indexOf(listEl);
		                scope.playList.splice(index, 1);
		            };
                    
		            scope.typeAheadSearchResult = function (searchQuestion) {
		                return onlineRadioService.get('GetTypeAhedResult', { Question: searchQuestion }).then(function (serverResult) {
		                    if (serverResult.sucessResult) {
		                        scope.loadingResults = true;
		                        scope.typeAheadNoResults = serverResult.DataResult.length === 0;
		                        return serverResult.DataResult;
		                    } else {
		                        serverResult.showInfoMessage();
		                        scope.typeAheadNoResults = true;
		                    }
		                });
		            };

		            function getRandomSong(min, max) {
		                return Math.floor(Math.random() * (max - min) + min);
		            }

		            function init() {
		                $rootScope.$on('playSong', function (event, args) {
		                    event.preventDefault();

		                    if (args && args.link) {
		                        scope.addToPlayList({
		                            name: args.name,
		                            artist: args.artist,
		                            link: args.link
		                        });

		                        scope.play(scope.playList[scope.playList.length - 1]);
		                    }
		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular);