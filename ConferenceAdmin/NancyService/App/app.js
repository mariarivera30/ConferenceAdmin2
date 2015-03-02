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
        .state('sponsorregistration', {
            url: "/SponsorRegistration",
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
         )
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
        .state('administrator.registrationlist', {
            url: "/RegistrationList",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_registrationlist.html"
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
        .state('administrator.managesponsors', {
            url: "/ManageSponsors",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_managesponsors.html"
                }
            }
        })
        .state('administrator.managecommittee', {
            url: "/ManageCommittee",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_managecommittee.html"
                }
            }
        })
        .state('administrator.planningcommittee', {
            url: "/PlanningCommittee",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_planningcommittee.html"
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
        .state('administrator.guests', {
            url: "/ManageGuests",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_manageguests.html"
                }
            }
        })
        .state('administrator.managekeycodes', {
            url: "/ManageKeyCodes",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_managekeycodes.html"
                }
            }
        })
        .state('administrator.submissions', {
            url: "/ManageSubmissions",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_managesubmissions.html"
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
        .state('administrator.evaluationdetails', {
            url: "/EvaluationDetails",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_evaluationDetails.html"
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
        .state('profile.receiptinformation', {
            url: "/ReceiptInformation",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_receiptinfo.html"
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
            url: "/Evaluations",
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
            url: "/Submissions",
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
        .state('profile.apply', {
            url: "/Application",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_application.html"
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