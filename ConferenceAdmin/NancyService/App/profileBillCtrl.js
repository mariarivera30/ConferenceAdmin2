(function () {
    'use strict';

    var controllerId = 'profileBillCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', profileBillCtrl]);

    function profileBillCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'profileBillCtrl';

        function activate() {

        }


    }
})();
