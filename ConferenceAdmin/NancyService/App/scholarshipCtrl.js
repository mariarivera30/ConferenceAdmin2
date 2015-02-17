(function () {
    'use strict';

    var controllerId = 'scholarshipCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', scholarshipCtrl]);

    function scholarshipCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'scholarshipCtrl';

        function activate() {

        }


    }
})();
