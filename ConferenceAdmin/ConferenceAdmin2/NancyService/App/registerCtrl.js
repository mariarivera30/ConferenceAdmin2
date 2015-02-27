(function () {
    'use strict';

    var controllerId = 'registerCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', registerCtrl]);

    function registerCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'registerCtrl';

        function activate() {

        }


    }
})();
