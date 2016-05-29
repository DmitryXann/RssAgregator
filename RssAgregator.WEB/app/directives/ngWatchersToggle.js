; (function (angular, $) {
    'use strict';

    angular.module('app')
		.directive('ngWatchersToggle', ['$window', '$timeout',
		function ($window, $timeout) {
		    return {
		        restrict: 'A',
		        multiElement: true,
		        terminal: true,
		        scope: false,
		        link: function (scope, element, attrs) {
		            const MIN_SCROLL_VALUE = 10;

		            var expectedScope;

		            var previousYValue = 0;
		            var previousXValue = 0;
		            
		            var elYStart;
		            var elYEnd;
		            var elXStart;
		            var elXEnd;

		            var isShown = true;

		            function handleVisibilityChange() {
		                toogleWatchers(isShown && scrollShownStatus());
		            }
                   
		            function scrollShownStatus() {
		                if (($window.scrollY > previousYValue && $window.scrollY > previousYValue + MIN_SCROLL_VALUE) ||
                            ($window.scrollY < previousYValue && $window.scrollY < previousYValue - MIN_SCROLL_VALUE)) {
		                    previousYValue = $window.scrollY;
		                }

		                if (($window.scrollX > previousXValue && $window.scrollX > previousXValue + MIN_SCROLL_VALUE) ||
                            ($window.scrollX < previousXValue && $window.scrollX < previousXValue - MIN_SCROLL_VALUE)) {
		                    previousXValue = $window.scrollX;
		                }

		                return (elYStart >= previousYValue || elYEnd >= previousYValue) &&
                               (elXStart >= previousXValue || elXEnd >= previousXValue);
		            }

		            function toogleWatchers(useWatchers) {
		                if (useWatchers && expectedScope.$$deactivatedWatchers) {
		                    for (var index = 0; index < expectedScope.$$deactivatedWatchers.length; index++) {
		                        expectedScope.$$watchers.push(expectedScope.$$deactivatedWatchers[index]);
		                    }

		                    expectedScope.$$deactivatedWatchers = [];
		                    expectedScope.$$waitingForWatchersActivation = false;
		                } else if (!useWatchers && !expectedScope.$$waitingForWatchersActivation) {
		                    expectedScope.$$deactivatedWatchers = [];

		                    for (var index = 0; index < expectedScope.$$watchers.length; index++) {
		                        expectedScope.$$deactivatedWatchers.push(expectedScope.$$watchers[index]);
		                    }

		                    expectedScope.$$watchers = [];
		                    expectedScope.$$waitingForWatchersActivation = true;
		                }
		            }

		            function getAbsoluteElHight(el) {
		                var elHeight = el[0].scrollHeight;

		                if (!elHeight) {
		                    angular.forEach(el.children(), function (child) {
		                        elHeight += child.scrollHeight;
		                    });
		                }

		                return elHeight;
		            }

		            function getAbsoluteElWidth(el) {
		                var elWidth = el[0].scrollWidth;

		                if (!elWidth) {
		                    angular.forEach(el.children(), function (child) {
		                        elWidth += child.scrollWidth;
		                    });
		                }

		                return elWidth;
		            }

		            function init() {
		                $timeout(function () {
		                    expectedScope = element.scope();
		                    if (expectedScope.$id == scope.$id) {
		                        expectedScope = element.children().scope()
		                    }

		                    var elOffset = element.offset();

		                    elYStart = elOffset.top;
		                    elYEnd = elOffset.top + getAbsoluteElHight(element);

		                    elXStart = elOffset.left;
		                    elXEnd = elOffset.left + getAbsoluteElWidth(element);

		                    $($window).on("scroll", handleVisibilityChange);

		                    scope.$on('$destroy', function () {
		                        $($window).off("scroll", handleVisibilityChange);
		                    });

		                    if (attrs.ngHide) {
		                        scope.$watch(attrs.ngHide, function (newVal) {
		                            isShown = !newVal;
		                            handleVisibilityChange();
		                        });
		                    }

		                    if (attrs.ngShow) {
		                        scope.$watch(attrs.ngShow, function (newVal) {
		                            isShown = newVal;
		                            handleVisibilityChange();
		                        });
		                    }
		                }, 0, false);
		            }

		            init();
		        }
		    };
		}]);
})(angular, $);