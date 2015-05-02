(function () {
    'use strict';
    //pa q no hayan global scope variable
    var id = 'app';

    // TODO: Inject modules as needed.
    var app = angular.module('app', [
        // Angular modules 
        'ngAnimate',        // animations
        'ngRoute',           // routing
        'ui.router',
        'ngSanitize',
        'ngCkeditor'
        // Custom modules 

        // 3rd Party Modules
        
    ]);

    // Execute bootstrapping code and any dependencies.
    // TODO: inject services as needed.
    app.run(['$q', '$rootScope',
        function ($q, $rootScope) {

        }]);

//    $rootscope.$on( "$statechangestart", function(event, next, current) {
//        if ($rootscope.loggedinuser == null) {
//            // no logged user, redirect to /login
//            if (next.templateurl === "adminusers.html"  || next.templateurl === "adminsettings.html"|| next.templateurl === "adminitems.html" || next.templateurl === "admincomment.html" || 
//                current.templateurl === "adminusers.html"  || current.templateurl === "adminsettings.html"|| current.templateurl === "adminitems.html" || current.templateurl === "admincomment.html") {
        		
//                event.preventdefault();
//                $state.transitionto('form.admin.login');
        		
//            } 

//        }
//        else if(next.templateurl === "adminlogin.html"){
//            event.preventdefault();
//            $state.transitionto('form.admin.users');
//        }

     

//    });
//});

   app.factory('AuthInterceptor', function ($window, $q) {
        return {
            request: function (config) {
                config.headers = config.headers || {};
                if ($window.sessionStorage.getItem('token')) {
                    config.headers.Authorization = 'Token ' + $window.sessionStorage.getItem('token');
                }
                return config || $q.when(config);
            },
            response: function (response) {
                if (response.status === 401) {
                    // TODO: Redirect user to login page.
                }
                return response || $q.when(response);
            }
        };
   });
 
   app.config(function ($stateProvider, $urlRouterProvider,$httpProvider) {

       $urlRouterProvider.rule(function ($injector, $location) {
           //what this function returns will be set as the $location.url
           var path = $location.path(), normalized = path.toLowerCase();
           if (path != normalized) {
               //instead of returning a new url string, I'll just change the $location.path directly so I don't have to worry about constructing a new url string and so a new state change is not triggered
               $location.replace().path(normalized);
           }
           // because we've returned nothing, no state change occurs
       });
       ///); 
        // For any unmatched url, redirect to /state1
       $httpProvider.interceptors.push('AuthInterceptor');
       $urlRouterProvider.otherwise("/Home");
        //
        // Now set up the states
        $stateProvider
          .state('home', {
              url: "/home",
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
            url: "/committee",
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
            url: "/venue",
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
            url: "/deadlines",
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
            url: "/registration",
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
            url: "/sponsorregistration",
            views: {
                'banner': {
                    templateUrl: "views/banner.html"
                },
                'dynamic': {
                    templateUrl: "views/sponsorRegistration.html"
                }

            }
        })
            .state('payment', {
                url: "/payment",
                views: {
                    'banner': {
                        templateUrl: ""
                    },
                    'dynamic': {
                        templateUrl: "views/paymentView.html"
                    }

                }
            })

             .state('paymentbill', {
                 url: "/paymentbill/:paymentId",
                 views: {
                     
                     'dynamic': {
                         templateUrl: "views/paymentReceipt.html"
                     }

                 }
             })
             .state('paymenterror', {
                 url: "/paymenterror",
                 views: {
                     'dynamic': {
                         templateUrl: "views/paymentError.html"
                     }

                 }
             })
        .state('sponsors', {
            url: "/sponsors",
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
            url: "/contact",
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
            url: "/schedule",
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
            url: "/abstracts",
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
             url: "/workshops",
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
             url: "/register",
             views: {
                 'banner': {
                     templateUrl: "views/banner.html"
                 },
                 'dynamic': {
                     templateUrl: "views/register.html"
                 }

             }
         }
         )
              .state('changePassword', {
                  url: "/changepassword",
                  views: {
                      'dynamic': {
                          templateUrl: "views/changePassword.html"
                      },


                  }
              }).state('login', {
             url: "/login",
             views: {
                 'dynamic': {
                     templateUrl: "views/login.html"
                 },


             }

         }).state('login.log', {
             url: "/log",
             views: {
                 'login': {
                     templateUrl: "views/login2.html"
                 },


             }
         })
         .state('login.signup', {
               url: "/signup",
               views: {
                   'login': {
                       templateUrl: "views/signup.html"
                   },


               }
           })
            .state('validate', {
                url: "/validate",
                views: {
                    'dynamic': {
                        templateUrl: "views/validateAccount.html"
                    },


                }
            })
             .state('login.request', {
                 url: "/requestpass",
                 views: {
                     'login': {
                         templateUrl: "views/requestPass.html"
                     },


                 }
             })

        .state('administrator', {
             url: "/administrator",
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
            url: "/generalinformation",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_generalInformation.html"
                }
            }
        })

        .state('administrator.home', {
            url: "/home",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_home.html"
                }
            }
        })

        .state('administrator.managetopics', {
            url: "/managetopics",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_managetopics.html"
                }
            }
        })
         .state('administrator.venue', { 
             url: "/venue",
             views: {
                 'adminPage': {
                     templateUrl: "views/admin_venue.html"
                 }
             }
         })
        .state('administrator.deadlines', {
            url: "/deadlines",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_deadlines.html"
                }
            }
        })
        .state('administrator.contact', {
            url: "/contact",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_contact.html"
                }
            }
        })
        .state('administrator.participation', {
            url: "/participation",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_participation.html"
                }
            }
        })

        .state('administrator.program', {
            url: "/program",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_program.html"
                }
            }
        })

        .state('administrator.registration', {
            url: "/registrationform",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_registrationform.html"
                }
            }
        })
        .state('administrator.registrationlist', {
            url: "/registrationlist",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_registrationlist.html"
                }
            }
        })
        .state('administrator.agenda', {
            url: "/agenda",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_agenda.html"
                }
            }
        })
        .state('administrator.sponsors', {
            url: "/sponsors",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_sponsors.html"
                }
            }
        })
        .state('administrator.managesponsors', {
            url: "/managesponsors",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_managesponsors.html",
                    controller: "sponsorCtrl"
                }
            }
        })
        .state('administrator.manageadministrators', {
            url: "/manageadministrators",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_manageadministrators.html"
                }
            }
        })

        .state('administrator.manageevaluators', {
            url: "/manageevaluators",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_manageevaluators.html"
                }
            }
        })

        .state('administrator.planningcommittee', {
            url: "/planningcommittee",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_planningcommittee.html"
                }
            }
        })
        .state('administrator.managetemplates', {
            url: "/managetemplates",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_managetemplates.html"
                }
            }
        })
            .state('administrator.manageAuthtemplates', {
                url: "/manageauthtemplates",
                views: {
                    'adminPage': {
                        templateUrl: "views/admin_manageAuthorizationTemplate.html"
                    }
                }
            })
        .state('administrator.guests', {
            url: "/manageguests",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_manageguests.html"
                }
            }
        })
        .state('administrator.managekeycodes', {
            url: "/managekeycodes",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_managekeycodes.html"
                }
            }
        })
        .state('administrator.submissions', {
            url: "/managesubmissions",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_managesubmissions.html"
                }
            }
        })
        .state('administrator.reports', {
            url: "/reports",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_reports.html"
                }
            }
        })
        .state('administrator.evaluationdetails', {
            url: "/evaluationdetails",
            views: {
                'adminPage': {
                    templateUrl: "views/admin_evaluationDetails.html"
                }
            }
        })

        .state('profile', {
            url: "/profile",
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
            url: "/generalinformation",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_information.html"
                }
            }
        })
            .state('profile.sponsorcomplementary', { 
                url: "/sponsorcomplementary",
                views: {
                    'profilePage': {
                        templateUrl: "views/profileSponsorComplementary.html"
                    }
                }
            })
             .state('profile.sponsorpaymentBill', { 
                 url: "/sponsorpaymentbill",
                 views: {
                     'profilePage': {
                         templateUrl: "views/profileSponsorPaymentBills.html"
                     }
                 }
             })
             .state('profile.sponsorinformation', { 
                 url: "/sponsorgeneralinformation",
                 views: {
                     'profilePage': {
                         templateUrl: "views/profileSponsor.html"
                     }
                 }
             })
                 .state('profile.sponsordonate', {
                     url: "/sponsordonate",
                     views: {
                         'profilePage': {
                             templateUrl: "views/profileSponsorDonate.html"
                         }
                     }
                 })

        .state('profile.receiptinformation', {
            url: "/receiptinformation",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_receiptinfo.html"
                }
            }
        })
        .state('profile.benefits', {
            url: "/benefits",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_benefits.html"
                }
            }
        })
        .state('profile.status', {
            url: "/status",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_status.html"
                }
            }
        })
        .state('profile.evaluation', {
            url: "/evaluations",
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
            url: "/submissions",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_submission.html"
                }
            }
        })
            .state('profile.evaluateSubmission', {
                url: "/evaluatesubmission",
                views: {
                    'viewEvaluation': {
                        templateUrl: "views/evaluation.html"
                    }
                }
            })
        .state('profile.authorization', {
            url: "/authorization",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_authorization.html"
                }
            }
        })
        .state('profile.apply', {
            url: "/application",
            views: {
                'profilePage': {
                    templateUrl: "views/profile_application.html"
                }
            }
        })
        .state('participation', {
            url: "/participation",
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