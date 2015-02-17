(function () {
    'use strict';

    var controllerId = 'venueCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', venueCtrl]);

    function venueCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'venueCtrl';

        function activate() {

        }


    }
})();
