(function () {
    'use strict';

    var controllerId = 'profileSubmissionCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', profileSubmissionCtrl]);

    function profileSubmissionCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'profileSubmissionCtrl';

        function activate() {

        }


    }
})();
