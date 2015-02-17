(function () {
    'use strict';

    var controllerId = 'scheduleCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', scheduleCtrl]);

    function controller($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'scheduleCtrl';

        function activate() {

        }


    }
})();
