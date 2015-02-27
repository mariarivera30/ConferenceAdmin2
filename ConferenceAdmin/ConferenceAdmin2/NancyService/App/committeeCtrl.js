(function () {
    'use strict';

    var controllerId = 'committeeCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', committeeCtrl]);

    function committeeCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'committeeCtrl';
       
        function activate() {

        }


    }
})();
