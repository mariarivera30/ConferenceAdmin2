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

        vm.general = true;
        vm.planning = true;
        vm.venue = true;
        vm.deadline = true;
        vm.registrationInt = true;
        vm.participation = true;
        vm.sponsorsInt = true;
        vm.contact = true;

        //Conference Manage tabs
        vm.committeeManage = true; //committe,admin,
        vm.topic = true; //committe,admin,
        vm.registrationList = true;
        vm.guest = true;
        vm.submissions = true;//committe,admin,
        vm.sponsors = true;
        vm.templates = true;//committe,admin,
        vm.keyCodes = true;
        vm.reports = true;


        // Functions
        vm.tabViewControl = _tabViewControl;
        activate();

        function activate() {
            //_tabViewControl();
        }

        function _tabViewControl() {
            var list = [];
            list.push($window.sessionStorage.getItem('claim-1'));
            list.push($window.sessionStorage.getItem('claim-2'));
            list.forEach(function (claim) {



                if (claim.localeCompare('admin') == 0) {
                    //Website content tabs
                    vm.participation = true;
                    vm.general = true;
                    vm.planning = true;
                    vm.venue = true;
                    vm.deadline = true;
                    vm.registrationInt = true;
                    vm.sponsorsInt = true;
                    vm.contact = true;

                    //Conference Manage tabs

                    vm.committeeManage = true; //committe,admin,
                    vm.topic = true; //committe,admin,
                    vm.registrationList = true;
                    vm.guest = true;
                    vm.submissions = true;//committe,admin,
                    vm.sponsors = true;
                    vm.templates = true;//committe,admin,
                    vm.keyCodes = true;
                    vm.reports = true;

                }
                if (claim.localeCompare('adminFinance') == 0) {
                    vm.general = true;
                    vm.planning = true;
                    vm.venue = true;
                    vm.deadline = true;
                    vm.registrationInt = true;
                    vm.sponsorsInt = true;
                    vm.contact = true;
                    vm.participation = true;

                    //Conference Manage tabs
                    vm.committeeManage = false; //committe,admin,
                    vm.topic = false; //committe,admin,
                    vm.registrationList = true;
                    vm.guest = true;
                    vm.submissions = false;//committe,admin,
                    vm.sponsors = true;
                    vm.templates = false;//committe,admin,
                    vm.keyCodes = true;
                    vm.reports = true;
                }

                if (claim.localeCompare('adminCommittee') == 0) {
                    vm.submissions = true;//committe,admin,
                    vm.templates = true;//committe,admin,
                    vm.topic = true; //committe,admin,
                    vm.participation = false;
                }




            });



        };

    


    }
}
)();
