(function () {
    'use strict';
    //pa q no hayan global scope variable
    var id = 'app';

    // TODO: Inject modules as needed.
    var app = angular.module('app', [
        // Angular modules 
        'ngAnimate',        // animations
        'ngRoute',           // routing
        'ui.router'
        // Custom modules 

        // 3rd Party Modules
        
    ]);

    // Execute bootstrapping code and any dependencies.
    // TODO: inject services as needed.
    app.run(['$q', '$rootScope',
        function ($q, $rootScope) {

        }]);
    app.config(function ($stateProvider, $urlRouterProvider) {
        //
        // For any unmatched url, redirect to /state1
        $urlRouterProvider.otherwise("/Home");
        //
        // Now set up the states
        $stateProvider
          .state('home', {
              url: "/Home",
              templateUrl: "views/home.html"
          })
        .state('committee', {
            url: "/Committee",
            templateUrl: "views/committee.html"
        })
        .state('venue', {
            url: "/Venue",
            templateUrl: "views/venue.html"
        })
        .state('deadline', {
            url: "/Deadlines",
            templateUrl: "views/deadlines.html"
        })
        .state('registration', {
            url: "/Registration",
            templateUrl: "views/registration.html"
        })
        .state('sponsors', {
            url: "/Sponsors",
            templateUrl: "views/sponsors.html"
        })
        .state('contact', {
            url: "/Contact",
            templateUrl: "views/contact.html"
        })
        .state('schedule', {
            url: "/Schedule",
            templateUrl: "views/schedule.html"
        })
        .state('abstracts', {
            url: "/Abstracts",
            templateUrl: "views/abstracts.html"
        })
         .state('workshops', {
             url: "/Workshops",
             templateUrl: "views/workshops.html"
         }
         ).state('papers', {
            url: "/Papers",
            templateUrl: "views/papers.html"
        })
         .state('register', {
             url: "/Register",
             templateUrl: "views/register.html"
         }
         ).state('login', {
             url: "/Login",
             templateUrl: "views/login.html"
         })
    });
})();