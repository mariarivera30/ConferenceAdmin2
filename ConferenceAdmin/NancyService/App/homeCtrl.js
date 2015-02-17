(function () {
    'use strict';

    var controllerId = 'homeCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http','restApi', homeCtrl]);

    function homeCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'homeCtrl';


        function activate() {

        }


    }
})();
