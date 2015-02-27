(function () {
    'use strict';

    var controllerId = 'loginCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', loginCtrl]);

    function loginCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'loginCtrl';

        function activate() {

        }


    }
})();
