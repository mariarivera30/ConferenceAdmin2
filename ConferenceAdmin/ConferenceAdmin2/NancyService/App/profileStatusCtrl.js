(function () {
    'use strict';

    var controllerId = 'profileStatusCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', profileStatusCtrl]);

    function profileStatusCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'profileStatusCtrl';

        function activate() {

        }


    }
})();
