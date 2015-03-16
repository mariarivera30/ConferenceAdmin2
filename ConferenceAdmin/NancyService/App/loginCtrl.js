(function () {
    'use strict';

    var controllerId = 'loginCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', '$window', '$location', loginCtrl]);

    function loginCtrl($scope, $http, restApi, $window, $location) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'loginCtrl';
        //register fields
        vm.firstname;
        vm.lastname;
        vm.email;
        vm.password;
        vm.token;


        // Functions
        vm.login = _login;


        function activate() {

        }

        function _login() {
            restApi.login(vm)
                   .success(function (data, status, headers, config) {
                       $http.defaults.headers.common.Authorization = 'Token ' + data.token;
                       $window.sessionStorage.setItem('token', data.token);
                       $window.data = data;

                       if ($window.data.userPriviledge != 0)
                           $location.path('/Administrator');
                       else {
                           $location.path('/Profile');
                       }
                   })

                   .error(function (error) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       $window.sessionStorage.removeItem('token');
                       vm.message = "Wrong email or password please try again";
                       vm.email = "";
                       vm.password = "";
                   });

        }

    }
})();
