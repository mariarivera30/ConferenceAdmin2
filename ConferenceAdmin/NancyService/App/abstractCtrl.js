(function () {
    'use strict';

    var controllerId = 'abstractCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', abstractCtrl]);

    function abstractCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'controller';
        // Functions

       

        function activate() {

        }

       
    }
})();
