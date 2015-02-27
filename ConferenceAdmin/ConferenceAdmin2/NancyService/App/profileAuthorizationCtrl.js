(function () {
    'use strict';

    var controllerId = 'profileAuthorizationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', profileAuthorizationCtrl]);

    function profileAuthorizationCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'profileAuthorizationCtrl';

        function activate() {

        }


    }
})();
