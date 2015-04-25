(function () {
    'use strict';

    var controllerId = 'layoutCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$rootScope', '$http', '$window', '$location', 'restApi', layoutCtrl]);

    function layoutCtrl($scope, $rootScope, $http, $window, $location, restApi) {

        var vm = this;

        vm.activate = activate;
        vm.title = 'layoutCtrl';
        vm.conferenceName;
        vm.conferenceLogo;
        vm.obj = {
            title: "",
            message1: "",
            message2: "",
            label: "",
            okbutton: false,
            okbuttonText: "",
            cancelbutton: false,
            cancelbuttoText: "Cancel",
        };

        //Functions
        vm.getGeneralInfo = _getGeneralInfo;
        vm.tabViewControl = _tabViewControl;
        vm.logout = _logout;
        vm.goTo = _goTo;

        _getGeneralInfo();
        activate();

        //////Alerts/////
        function _goTo(path)
        { $location.path(path); }

        vm.toggleModal = function (action) {
            if (action == "changed") {

                vm.obj.title = "Reset Password";
                vm.obj.message1 = "Your temporary Password was send to your email!";
                vm.obj.message2 = "";
                vm.obj.label = "";
                vm.obj.okbutton = true;
                vm.obj.okbuttonText = "OK";
                vm.obj.cancelbutton = false;
                vm.obj.cancelbuttoText = "Cancel";
                vm.goPath = '/Login/Log';
                vm.showConfirmModal = !vm.showConfirmModal;

            }

            if (action == "confirm") {

                vm.obj.title = "Account Confirmation";
                vm.obj.message1 = "Congratulation your account was confirmed!";
                vm.obj.message2 = "";
                vm.obj.label = "";
                vm.obj.okbutton = true;
                vm.obj.okbuttonText = "OK";
                vm.obj.cancelbutton = false;
                vm.obj.cancelbuttoText = "Cancel";
                vm.goPath = '/Login/Log';
                vm.showConfirmModal = !vm.showConfirmModal;

            }
            if (action == "makeConfirmation") {

                vm.obj.title = "Next Step Account Confirmation",
                vm.obj.message1 = "Please check you email to confirm your account!",
                vm.obj.message2 ="",
                vm.obj.label = "",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "OK",
                vm.obj.cancelbutton = false,
                vm.obj.cancelbuttoText = "Cancel",
                vm.goPath = '/Validate';
                vm.showConfirmModal = !vm.showConfirmModal;

            }
            else if (action == "error")
                vm.obj.title = "Server Error",
                vm.obj.message1 = "Please refresh the page and try again.",
                vm.obj.message2 = "",
                vm.obj.label = "",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "OK",
                vm.obj.cancelbutton = false,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
        };

        ////

        $rootScope.$on('popUp', function (event, data) {
            if (data == "requestedPass") {
                vm.toggleModal('changed');
                vm.showConfirmModal = data;
                

            }
            else if (data == "error") {
                vm.toggleModal('error');
                vm.showConfirmModal = data;

            }
            else if (data == "confirm") {
                vm.toggleModal('confirm');
                vm.showConfirmModal = data;

            } 
            else if (data == 'makeConfirmation') {
                vm.toggleModal('makeConfirmation');
                vm.showConfirmModal = data;
            }

        });


        $rootScope.$on('Login', function (event, data) {
            vm.messageLogOut = $window.sessionStorage.getItem('email').substring(1, $window.sessionStorage.getItem('email').length - 1);
            vm.showProfile = true;
            vm.showAdminsitrator = data;
        });

        $rootScope.$on('Loginpart', function (data) {
            vm.messageLogOut = $window.sessionStorage.getItem('email').substring(1, $window.sessionStorage.getItem('email').length - 1);
            vm.showProfile = true;

        });
        $rootScope.$on('Logout', function (data) {

            vm.showProfile = false;
            vm.messageLogOut = "";
            vm.showAdminsitrator = false;
        });

        $rootScope.$on('headerPage', function (event, hideAlias) {

            vm.conferenceName = hideAlias;


        });


        function activate() {

            _tabViewControl();
            if ($window.sessionStorage.length == 0) {
                vm.showProfile = false;
                vm.showAdminsitrator = false;
            }
            else {
                vm.showProfile = true;
                vm.messageLogOut = $window.sessionStorage.getItem('email').substring(1, $window.sessionStorage.getItem('email').length - 1);

            }

        }


        function _tabViewControl() {


            if ($window.sessionStorage.length != 0) {

                var list = JSON.parse(sessionStorage.getItem('claims'));
                list.forEach(function (claim) {

                    if (claim.localeCompare('Admin') == 0 || claim.localeCompare('Master') == 0 ||
                        claim.localeCompare('Finance') == 0 || claim.localeCompare('CommitteEvaluator') == 0) {
                        vm.showAdminsitrator = true;
                    }

                });
            }
            else { vm.loged = false; vm.showAdminsitrator = false; }


        };

        function _logout() {

            $window.sessionStorage.clear();
            vm.loged = false;
            vm.showAdminsitrator = false;
            vm.showProfile = false;
            $location.path('/Home');

        }

        $rootScope.$on('ConferenceAcronym', function (event, data) {
            vm.conferenceAcronym = data;
        });

        $rootScope.$on('ConferenceName', function (event, data) {
            vm.conferenceName = data;
        });

        $rootScope.$on('ConferenceLogo', function (event, data) {
            vm.conferenceLogo = data;
        });

        function _getGeneralInfo() {
            restApi.getGeneralInfo()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.conferenceAcronym = data.conferenceAcronym;
                    vm.conferenceName = data.conferenceName;
                    
                    _getImage();
                }
            })
            .error(function (error) {

            });
        }

        function _getImage() {
            restApi.getWebsiteLogo()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.conferenceLogo = data.logo;
                }
            })

            .error(function (error) {

            });
        }
    }
})();
