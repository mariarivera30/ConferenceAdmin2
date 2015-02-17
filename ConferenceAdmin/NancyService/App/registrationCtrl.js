(function () {
    'use strict';

    var controllerId = 'registrationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', registrationCtrl]);

    function controller($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'registrationCtrl';

        function activate() {

        }


    }
})();
