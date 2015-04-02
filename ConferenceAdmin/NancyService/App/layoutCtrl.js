(function () {
    'use strict';

    var controllerId = 'layoutCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$rootScope', '$http', '$window', '$location', layoutCtrl]);

    function layoutCtrl($scope, $rootScope, $http, $window, $location) {

        var vm = this;

        vm.activate = activate;
        vm.title = 'layoutCtrl';
        vm.tabViewControl = _tabViewControl;
        vm.logout = _logout;

        activate();

        $rootScope.$on('Login', function (data) {


            vm.messageLogOut = $window.sessionStorage.getItem('email').substring(1, $window.sessionStorage.getItem('email').length - 1);
            vm.showProfile = true;

        });

        $rootScope.$on('Logout', function (data) {

            vm.showProfile = false;
            vm.messageLogOut = "";

        });

        function activate() {

            _tabViewControl();
            if ($window.sessionStorage.length == 0) {
                vm.showProfile = false;
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

                    if (claim.localeCompare('admin') == 0 || claim.localeCompare('master') == 0 ||
                        claim.localeCompare('adminFinance') == 0 || claim.localeCompare('adminCommittee') == 0) {

                    }

                });
            }
            else { vm.loged = false; }


        };

        function _logout() {
            $rootScope.$emit('Logout', { hideAlias: true });

            $window.sessionStorage.clear();
            vm.loged = false;
            $location.path('/Home');

        }



    }
})();
