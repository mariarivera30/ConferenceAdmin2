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
              views: {
                  'banner': {
                      templateUrl: "views/banner.html"
                  },
                  'dynamic': {
                      templateUrl: "views/home.html"
                  }
                 
              }
          })
        .state('committee', {
            url: "/Committee",
            views: {
                'banner': {
                    templateUrl: "views/banner.html"
                },
                'dynamic': {
                    templateUrl: "views/committee.html"
                }
                
            }
        })
        .state('venue', {
            url: "/Venue",
            views: {
                'banner': {
                    templateUrl: "views/banner.html"
                },
                'dynamic': {
                    templateUrl: "views/venue.html"
                }

            }
        })
        .state('deadline', {
            url: "/Deadlines",
            views: {
                'banner': {
                    templateUrl: "views/banner.html"
                },
                'dynamic': {
                    templateUrl: "views/deadlines.html"
                }
                
            }
        })
        .state('registration', {
            url: "/Registration",
            views: {
                'banner': {
                    templateUrl: "views/banner.html"
                },
                'dynamic': {
                    templateUrl: "views/registration.html"
                }

            }
        })
        .state('sponsors', {
            url: "/Sponsors",
            views: {
                'banner': {
                    templateUrl: "views/banner.html"
                },
                'dynamic': {
                    templateUrl: "views/sponsors.html"
                }
                
            }
        })
        .state('contact', {
            url: "/Contact",
            views: {
                'banner': {
                    templateUrl: "views/banner.html"
                },
                'dynamic': {
                    templateUrl: "views/contact.html"
                }

            }
        })
        .state('schedule', {
            url: "/Schedule",
            views: {
                'banner': {
                    templateUrl: "views/banner.html"
                },
                'dynamic': {
                    templateUrl: "views/schedule.html"
                }
                
            }
        })
        .state('abstracts', {
            url: "/Abstracts",
            views: {
                'banner': {
                    templateUrl: "views/banner.html"
                },
                'dynamic': {
                    templateUrl: "views/abstracts.html"
                }
                
            }
        })
         .state('workshops', {
             url: "/Workshops",
             views: {
                 'banner': {
                     templateUrl: "views/banner.html"
                 },
                 'dynamic': {
                     templateUrl: "views/workshops.html"
                 }
                 
             }
         }
         ).state('papers', {
            url: "/Papers",
            views: {
                'banner': {
                    templateUrl: "views/banner.html"
                },
                'dynamic': {
                    templateUrl: "views/papers.html"
                }
                
            }
        })
         .state('register', {
             url: "/Register",
             views: {
                 'banner': {
                     templateUrl: "views/banner.html"
                 },
                 'dynamic': {
                     templateUrl: "views/register.html"
                 }

             }
         }
         ).state('login', {
             url: "/Login",
             views: {
                 'dynamic': {
                     templateUrl: "views/login.html"
                 },
                 'banner': {
                     templateUrl: "views/abstracts.html"
                 }

             }
         })
        .state('administrator', {
             url: "/Administrator",
             views: {
                 'dynamic': {
                     templateUrl: "views/administrator.html"
                 },
                 'banner': {
                     templateUrl: "views/banner.html"
                 }

             }
         })
        .state('administrator.information', { //Start Administrator Menu
            url: "/GeneralInformation",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_generalInformation.html"
                }
            }
        })
        .state('administrator.registration', {
            url: "/RegistrationForm",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_registrationform.html"
                }
            }
        })
        .state('administrator.agenda', {
            url: "/Agenda",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_agenda.html"
                }
            }
        })
        .state('administrator.sponsors', {
            url: "/Sponsors",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_sponsors.html"
                }
            }
        })
        .state('administrator.manageadmins', {
            url: "/ManageCommittee",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_manageadmins.html"
                }
            }
        })
        .state('administrator.attendants', {
            url: "/ManageApplicants",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_manageattendants.html"
                }
            }
        })
        .state('administrator.evaluators', {
            url: "/ManageEvaluations",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_manageevaluators.html"
                }
            }
        })
        .state('administrator.reports', {
            url: "/Reports",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_reports.html"
                }
            }
        })
        .state('administrator.manageminors', {
            url: "/ManageMinors",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_manageminors.html"
                }
            }
        })
    });
})();