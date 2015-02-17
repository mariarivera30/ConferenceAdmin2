(function () {
    'use strict';

    var controllerId = 'scholarshipCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', scholarshipCtrl]);

    function controller($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'scholarshipCtrl';

        function activate() {

        }


    }
})();
