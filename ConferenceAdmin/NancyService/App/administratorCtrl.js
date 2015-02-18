(function () {
    'use strict';

    var controllerId = 'administratorCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', administratorCtrl]);

    function administratorCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'administratorCtrl';
        // Functions



        function activate() {

        }


    }
})();
