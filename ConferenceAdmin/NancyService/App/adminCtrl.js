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
            type: "Evaluator Admin",
            accepted: true,
            rejected: false,
            pending: false,
            visitor: false
        }, {
            name: "Maria Rivera",
            email: "maria.rivera30@upr.edu",
            evaluations: ["1", "2", "3", "4"],
            type: "Administrator",
            accepted: false,
            rejected: false,
            pending: true,
            visitor: false
        }, {
            name: "Jaimeiris Nieves",
            email: "jaimeiris.nieves@upr.edu",
            evaluations: ["1", "2"],
            type: "Finance Admin",
            accepted: false,
            rejected: false,
            pending: false,
            visitor: true
        }, {
            name: "Heidi Negron",
            email: "heidi.negron1@upr.edu",
            evaluations: ["1", "2", "3"],
            type: "Finance Admin",
            accepted: false,
            rejected: true,
            pending: false,
            visitor: false
        } ];


        vm.activate = activate;
        vm.title = 'administratorCtrl';
        // Functions



        function activate() {

        }


    }
})();
