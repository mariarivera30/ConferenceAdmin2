(function () {
    'use strict';

    var controllerId = 'profileBenefitsCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', profileBenefitsCtrl]);

    function profileBenefitsCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'profileBenefitsCtrl';

        function activate() {

        }


    }
})();
