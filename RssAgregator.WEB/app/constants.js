; (function (angular) {
    'use strict';

    angular.module('app')
        .constant('ResultCodeEnum', {
            Unknown: 0,
            Success: 1,
            Error: 2,
            Warning: 3
        }).constant('viewTemplates', {
            //controllers
            dashboardController: '/app/controllers/dashboardController/dashboard.html',
            addEditPostController: '/app/controllers/addEditPostController/addEditPostController.html',

            //general directives
            audioPlayer: '/app/directives/audioPlayer/audioPlayer.html',
            audioPlayerProgressBar: '/app/directives/audioPlayer/audioPlayerProgressBar.html',
            videoPlayer: '/app/directives/videoPlayer/videoPlayer.html',
            wordCloud: '/app/directives/wordCloud/wordCloud.html',
            navigation: '/app/directives/navigation/navigation.html',
            login: '/app/directives/login/login.html',

            //container directives
            audioContainer: '/app/directives/containers/audioContainer/audioContainer.html',
            imageGalleryContainer: '/app/directives/containers/imageGalleryContainer/imageGalleryContainer.html',
            imgContainer: '/app/directives/containers/imageGalleryContainer/imgContainer.html',
            postContainer: '/app/directives/containers/postContainer/postContainer.html',
            videoContainer: '/app/directives/containers/videoGalleryContainer/videoContainer.html',
            videoGalleryContainer: '/app/directives/containers/videoGalleryContainer/videoGalleryContainer.html',

            //modals
            genericModalFactory: '/app/modals/genericModal/genericModal.html',

            //other templates
            post: 'post', // not needed?
            header: 'header',
            postModal: 'postModal',
            aboutModal: 'aboutModal'
        }).constant('ActivityEnum', {
            Open: 0,
            Close: 1,
            LogIn: 2,
            LogOut: 3
        }).constant('viewFilters', {
            view: 'view',
            top: 'top',
            best: 'best',
            about: 'about'
        });
})(angular);