(function () {
    'use strict';

    var controllerId = 'deadlinesCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', deadlinesCtrl]);

    function controller($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'deadlinesCtrl';

        function activate() {

        }


    }
})();
