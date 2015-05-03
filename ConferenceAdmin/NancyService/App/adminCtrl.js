/// <reference path="../Views/administrator.html" />
(function () {
    'use strict';

    var controllerId = 'adminCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi','$window','$location', adminCtrl]);

    function adminCtrl($scope, $http, restApi, $window, $location) {


        var vm = this;
        vm.activate = activate;
        vm.title = 'adminCtrl';
        //Website content tabs

        vm.general = false;
        vm.planning = false;
        vm.venue = false;
        vm.deadline = false;
        vm.registrationInt = false;
        vm.participation = false;
        vm.sponsorsInt = false;
        vm.contact = false;
        vm.administrator = false;

        //Conference Manage tabs
        vm.committeeManage = false; //committe,admin,
        vm.topic = false; //committe,admin,
        vm.registrationList = false;
        vm.guest = false;
        vm.submissions = false;//committe,admin,
        vm.sponsors = false;
        vm.templates = false;//committe,admin,
        vm.authTemplates = false;//committe,admin,
        vm.keyCodes = false;
        vm.reports = false;
        vm.administrator = false;
        vm.program = false;
        vm.isAdmin = false;


        // Functions
        vm.changeMenu = _changeMenu;
        vm.tabViewControl = _tabViewControl;
        activate();

        function activate() {

            _tabViewControl();
        }

        function _tabViewControl() {
         
            var list = JSON.parse(sessionStorage.getItem('claims'));
            if (list != null) {
                if (list.length > 0) {
                    list.forEach(function (claim) {

                        if (claim.localeCompare('Master') == 0) {
                            vm.participation = true;
                            vm.administrator = true;
                            vm.general = true;
                            vm.planning = true;
                            vm.venue = true;
                            vm.deadline = true;
                            vm.registrationInt = true;
                            vm.sponsorsInt = true;
                            vm.contact = true;
                            vm.home = true;
                            vm.program = true;


                            //Conference Manage tabs

                            vm.committeeManage = true; //committe,admin,
                            vm.topic = true; //committe,admin,
                            vm.registrationList = true;
                            vm.guest = true;
                            vm.submissions = true;//committe,admin,
                            vm.sponsors = true;
                            vm.templates = true;//committe,admin,
                            vm.authTemplates = true;
                            vm.keyCodes = true;
                            vm.reports = true;
                            vm.isAdmin = true;
                            vm.evaluators = true;
                        }

                        if (claim.localeCompare('Admin') == 0) {
                            //Website content tabs
                            vm.participation = true;
                            vm.administrator = true;
                            vm.general = true;
                            vm.planning = true;
                            vm.venue = true;
                            vm.deadline = true;
                            vm.registrationInt = true;
                            vm.sponsorsInt = true;
                            vm.contact = true;
                            vm.home = true;
                            vm.program = true;
                            

                            //Conference Manage tabs

                            vm.committeeManage = false; //committe,admin,
                            vm.topic = true; //committe,admin,
                            vm.registrationList = true;
                            vm.guest = true;
                            vm.submissions = true;//committe,admin,
                            vm.sponsors = true;
                            vm.templates = true;//committe,admin,
                            vm.authTemplates = true;
                            vm.keyCodes = true;
                            vm.reports = true;
                            vm.isAdmin = true;
                            vm.evaluators = true;

                        }
                        if (claim.localeCompare('Finance') == 0) {
                            vm.participation = true;
                            vm.general = true;
                            vm.planning = true;
                            vm.venue = true;
                            vm.deadline = true;
                            vm.registrationInt = true;
                            vm.sponsorsInt = true;
                            vm.contact = true;
                            vm.program = true;
                            vm.home = true;

                            //Conference Manage tabs
                            vm.committeeManage = false; //committe,admin,
                            vm.topic = false; //committe,admin,
                            vm.registrationList = true;
                            vm.guest = true;
                            vm.submissions = false;//committe,admin,
                            vm.sponsors = true;
                            vm.templates = false;//committe,admin,
                            vm.authTemplates = true;
                            vm.keyCodes = true;
                            vm.reports = true;
                            vm.isAdmin = true;
                        }

                        if (claim.localeCompare('CommitteEvaluator') == 0) {
                           
                            vm.home = false;
                            vm.general = false;
                            vm.planning = false;
                            vm.venue = false;
                            vm.deadline = false;
                            vm.registrationInt = false;
                            vm.participation = false;
                            vm.sponsorsInt = false;
                            vm.contact = false;
                            vm.administrator = false;
                            vm.program = false;

                            vm.submissions = true;//committe,admin,
                            vm.templates = true;//committe,admin,
                            vm.authTemplates = true;
                            vm.topic = true; //committe,admin,
                          
                            vm.isAdmin = true;
                            vm.evaluators = true;
                        }

                    

                    });

                    if (!vm.isAdmin) {
                        $location.path('/Home');
                    }
                }
            }
            else {
                $location.path('/Home');
            }


        };

        function _changeMenu() {
            vm.hideMenu ? vm.hideMenu = false : vm.hideMenu = true;
        }


    }
}
)();
