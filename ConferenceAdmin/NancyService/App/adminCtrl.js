(function () {
    'use strict';

    var controllerId = 'administratorCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', administratorCtrl]);

    function administratorCtrl($scope, $http) {
        var vm = this;
        vm.list = [{
            name: "Randy Soto",
            email: "randy.soto@upr.edu",
            evaluations: ["1", "2", "3"],
            accepted: true,
            rejected: false
        }, {
            name: "Maria Rivera",
            email: "maria.rivera30@upr.edu",
            evaluations: ["1", "2", "3", "4"],
            accepted: false,
            rejected: false
        }, {
            name: "Jaimeiris Nieves",
            email: "jaimeiris.nieves@upr.edu",
            evaluations: ["1", "2"],
            accepted: false,
            rejected: false
        }, {
            name: "Heidi Negron",
            email: "heidi.negron1@upr.edu",
            evaluations: ["1", "2", "3"],
            accepted: false,
            rejected: true
        } ];


        vm.activate = activate;
        vm.title = 'administratorCtrl';
        // Functions



        function activate() {

        }


    }
})();
