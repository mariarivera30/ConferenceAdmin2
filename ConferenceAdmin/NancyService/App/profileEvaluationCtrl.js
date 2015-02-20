(function () {
    'use strict';

    var controllerId = 'profileEvaluationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', profileEvaluationCtrl]);

    function profileEvaluationCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'profileEvaluationCtrl';

        function activate() {

        }


    }
})();
