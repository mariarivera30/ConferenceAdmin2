(function () {
    'use strict';

    var controllerId = 'layoutCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', layoutCtrl]);

    function layoutCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'layoutCtrl';

        function activate() {

        }


    }
})();
