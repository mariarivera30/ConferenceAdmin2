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
                    templateUrl: "views/sponsorRegistration.html"
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
        .state('administrator.managetopics', {
            url: "/ManageTopics",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_managetopics.html"
                }
            }
        })
         .state('administrator.venue', { 
             url: "/Venue",
             views: {
                 'adminPage': {
                     templateUrl: "views/admin_venue.html"
                 }
             }
         })
        .state('administrator.deadlines', {
            url: "/Deadlines",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_deadlines.html"
                }
            }
        })
        .state('administrator.contact', {
            url: "/Contact",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_contact.html"
                }
            }
        })
        .state('administrator.participation', {
            url: "/Participation",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_participation.html"
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
        .state('administrator.managetemplates', {
            url: "/ManageTemplates",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_managetemplates.html"
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

        .state('profile', {
            url: "/Profile",
            views: {
                'dynamic': {
                    templateUrl: "views/profile.html"
                },
                'banner': {
                    templateUrl: "views/banner.html"
                }

            }
        })
        .state('profile.information', { //Start Administrator Menu
            url: "/GeneralInformation",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_information.html"
                }
            }
        })
        .state('profile.bill', {
            url: "/Bill",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_bill.html"
                }
            }
        })
        .state('profile.benefits', {
            url: "/Benefits",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_benefits.html"
                }
            }
        })
        .state('profile.status', {
            url: "/Status",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_status.html"
                }
            }
        })
        .state('profile.evaluation', {
            url: "/Evaluator",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_evaluation.html"
                },
                'viewEvaluation': {
                    templateUrl: "views/evaluation.html"
                }
            }
        })
      
        .state('profile.submission', {
            url: "/Submission",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_submission.html"
                }
            }
        })
            .state('profile.evaluateSubmission', {
                url: "/EvaluateSubmission",
                views: {
                    'viewEvaluation': {
                        templateUrl: "views/evaluation.html"
                    }
                }
            })
        .state('profile.authorization', {
            url: "/Authorization",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_authorization.html"
                }
            }
        })
              .state('participation', {
                  url: "/Participation",
                  views: {
                      'dynamic': {
                          templateUrl: "views/participation_Information.html"
                      },
                      'banner': {
                          templateUrl: "views/banner.html"
                      }
                  }
              })
    });
})();