; (function (angular, WordCloud) {
    'use strict';

    angular.module('app')
		.directive('wordCloud', ['$sce', 'uiSettings', '$log', 'templateFactory', 'newsService', '$window',
		function ($sce, uiSettings, $log, templateFactory, newsService, $window) {
		    return {
		        restrict: 'E',
		        templateUrl: function () {
		            return templateFactory.get('wordCloud');
		        },
		        scope: {
		            width: '@',
		            height: '@'
		        },
		        link: function (scope, element, attrs) {
		            

		            function init() {
		                newsService.get('GetAllNewsTags').then(function (serverResult) {
		                    if (serverResult.sucessResult) {
		                        var serverResult = serverResult.DataResult;
		                        

		                        var tagsArray = [["Les Misérables", 30],
		                        ["Victor Hugo", 20],
		                        ["Jean Valjean", 15], 
		                        ["Javert", 15], 
		                        ["Fantine", 15],
		                        ["Cosette", 15],
		                        ["Éponine", 12],
		                        ["Marius", 12],
		                        ["Enjolras", 12],
		                        ["Thénardiers", 10],
		                        ["Gavroche", 10],
		                        ["Bishop Myriel", 10],
		                        ["Patron-Minette", 10],
		                        ["God", 10],
		                        ["ABC Café", 9],
		                        ["Paris", 9],
		                        ["Digne", 9],
		                        ["Elephant of the Bastille", 9],
		                        ["silverwarem", 5], 
		                        ["Bagne of Toulon", 5 ],
		                        ["loaf of bread", 5 ],
		                        ["Rue Plumet", 5 ],
		                        ["revolution", 5 ],
		                        ["barricade", 5 ],
		                        ["sewers", 4],
		                        ["Fex urbis lex orbis", 4]];

		                        for (var el in serverResult) {
		                            tagsArray.push([String(el), serverResult[el]])
		                        }

		                        var canvasEl = element.children('.word-cloud').children('canvas');
		                        var boxEl = element.children('.word-cloud').children('#box');

		                        canvasEl.append(boxEl);

		                        WordCloud(canvasEl[0], {
		                            list: tagsArray,
		                            hover: function drawBox(item, dimension) {
		                                if (!dimension) {
		                                    boxEl.prop('hidden', true);
		                                    return;
		                                }

		                                boxEl.prop('hidden', false);
		                                boxEl.css({
		                                    left: dimension.x / $window.devicePixelRatio + 'px',
		                                    top: dimension.y / $window.devicePixelRatio + 'px',
		                                    width: dimension.w / $window.devicePixelRatio + 'px',
		                                    height: dimension.h / $window.devicePixelRatio + 'px'
		                                });
		                            },
		                            gridSize: Math.round((scope.width / (tagsArray.length / 1.5)) * (scope.width / 1024)),
		                            ontFamily: 'Average, Times, serif',
		                        });
		                    } else {
		                        serverResult.showInfoMessage();
		                    }
		                });

		                scope.$on('$destroy', function () {

		                });
		            }

		            init();
		        }
		    };
		}]);
})(angular, WordCloud);