; (function (angular) {
    'use strict';

    angular.module('app')
        .constant('templateType', {
            Post: 1,
            Header: 2
        }).constant('ResultCodeEnum', {
            Unknown: 0,
            Success: 1,
            Error: 2,
            Warning: 3
        }).constant('viewTemplates', {
            //controllers
            dashboardController: 'app/controllers/dashboardController/dashboard.html',
            aboutController: 'app/controllers/aboutController/aboutController.html',

            //general directives
            audioPlayer: 'app/directives/audioPlayer/audioPlayer.html',
            audioPlayerProgressBar: 'app/directives/audioPlayer/audioPlayerProgressBar.html',
            videoPlayer: 'app/directives/videoPlayer/videoPlayer.html',

            //container directives
            audioContainer: 'app/directives/containers/audioContainer/audioContainer.html',
            imageGalleryContainer: 'app/directives/containers/imageGalleryContainer/imageGalleryContainer.html',
            imgContainer: 'app/directives/containers/imageGalleryContainer/imgContainer.html',
            postContainer: 'app/directives/containers/postContainer/postContainer.html',
            videoContainer: 'app/directives/containers/videoGalleryContainer/videoContainer.html',
            videoGalleryContainer: 'app/directives/containers/videoGalleryContainer/videoGalleryContainer.html'
        });
})(angular);