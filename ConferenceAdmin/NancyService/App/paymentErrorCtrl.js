(function () {
    'use strict';

    var controllerId = 'paymentErrorCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', '$rootScope', paymentErrorCtrl]);

    function paymentErrorCtrl($scope, $http, $rootScope) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'paymentErrorCtrl';

        function activate() {

        }



    }
})();
