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
        vm.profileButton = false;
       
      

        // Functions
        vm.login = _login;


        function activate() {

        }

        function _login() {
            restApi.login(vm)
                   .success(function (data, status, headers, config) {
                       $http.defaults.headers.common.Authorization = 'Token ' + data.token;
                       $window.sessionStorage.setItem('token', data.token);
                       $window.sessionStorage.setItem('claim-1', data.userClaims[0]);
                       $window.sessionStorage.setItem('claim-2', "");

                       if (data.userClaims.length > 1)
                           $window.sessionStorage.setItem('claim-2', data.userClaims[1]);

                       var list = []; 
                       list.push($window.sessionStorage.getItem('claim-1'));
                       list.push($window.sessionStorage.getItem('claim-2'));
                       list.forEach(function (claim) {

                           if (claim.localeCompare('minor') == 0 || claim.localeCompare('companion') == 0 || claim.localeCompare('participant') == 0 || claim.localeCompare('evaluator') == 0) {
                               if (claim.localeCompare('evaluator') == 0)
                                   $location.path('/Profile/Evaluations');
                               else $location.path('/Profile/GeneralInformation');
                               return;
                           }
                           else if (claim.localeCompare('admin') == 0 || claim.localeCompare('adminFinance') == 0 || claim.localeCompare('adminCommittee') == 0 ) {
                               if (claim.localeCompare('adminCommittee') == 0)
                                   $location.path('/Administrator/ManageSubmissions');
                               else {
                                   $location.path('/Administrator/GeneralInformation');
                               }
                           }

                       });
                          
                       
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
