(function () {
    'use strict';

    var controllerId = 'papersCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', papersCtrl]);

    function controller($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'papersCtrl';

        function activate() {

        }


    }
})();
