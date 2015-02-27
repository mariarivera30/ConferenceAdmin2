(function () {
    'use strict';

    var controllerId = 'contactCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', contactCtrl]);

    function contactCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'contactCtrl';

        function activate() {

        }


    }
})();
