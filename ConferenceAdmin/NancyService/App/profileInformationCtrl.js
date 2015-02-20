(function () {
    'use strict';

    var controllerId = 'profileInformationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', profileInformationCtrl]);

    function profileInformationCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'profileInformationCtrl';

        function activate() {

        }


    }
})();
