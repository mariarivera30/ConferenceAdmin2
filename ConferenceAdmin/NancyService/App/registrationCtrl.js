(function () {
    'use strict';

    var controllerId = 'registrationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', registrationCtrl]);

    function registrationCtrl($scope, $http) {
        var vm = this;

        vm.registration = false;
        vm.activate = activate;
        vm.title = 'registrationCtrl';

        function activate() {

        }


    }
})();
