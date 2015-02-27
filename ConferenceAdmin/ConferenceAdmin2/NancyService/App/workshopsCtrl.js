(function () {
    'use strict';

    var controllerId = 'workshopsCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', workshopsCtrl]);

    function workshopsCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'workshopsCtrl';

        function activate() {

        }


    }
})();
