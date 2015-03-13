(function () {
    'use strict';

    var controllerId = 'loginCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', loginCtrl]);

    function loginCtrl($scope, $http, restApi) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'loginCtrl';
        //register fields
        vm.firstname;
        vm.lastname;
        vm.email;
        vm.password;

        // login fields
        vm.username;
        vm.password;
        // Functions
        vm.register = _register;
        vm.login = _login;
        vm.validation = _validation;
        vm.token;

        function activate() {

        }  
        
        function _register() {
            $http.post('/auth/register', { email: vm.email, password: vm.password }).
                   success(function (data, status, headers, config) {
                       // this callback will be called asynchronously
                       // when the response is available
                   }).
                   error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                   });
        }

        function _login() {
            $http.post('/auth', { email: vm.username, password: vm.password }).
                   success(function (data, status, headers, config) {
                       // this callback will be called asynchronously
                       // when the response is available
                       $http.defaults.headers.common.Authorization = 'Token ' + data.token;

                   }).
                   error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       vm.message = data;
                   });
        }

        function _validation() {
            $http.get('/auth/validation').
                   success(function (data, status, headers, config) {
                       // this callback will be called asynchronously
                       // when the response is available
                       vm.message = data;
                   }).
                   error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       vm.message = data;
                   });
        }
    }
})();
