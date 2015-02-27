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
            visitor: false,
            average: 57
        }, {
            name: "Maria Rivera",
            email: "maria.rivera30@upr.edu",
            evaluations: ["1", "2", "3", "4"],
            type: "Administrator",
            accepted: false,
            rejected: false,
            pending: true,
            visitor: false,
            average: 61
        }, {
            name: "Jaimeiris Nieves",
            email: "jaimeiris.nieves@upr.edu",
            evaluations: ["1", "2"],
            type: "Finance Admin",
            accepted: false,
            rejected: false,
            pending: false,
            visitor: true,
            average: 63
        }, {
            name: "Heidi Negron",
            email: "heidi.negron1@upr.edu",
            evaluations: ["1", "2", "3"],
            type: "Finance Admin",
            accepted: false,
            rejected: true,
            pending: false,
            visitor: false,
            average: 78
        } ];


        vm.activate = activate;
        vm.title = 'administratorCtrl';
        // Functions



        function activate() {

        }


    }
})();
