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
            type: "Evaluator",
            accepted: true,
            rejected: false,
            registered: true,
            average: 57
        }, {
            name: "Maria Rivera",
            email: "maria.rivera30@upr.edu",
            evaluations: ["1", "2", "3", "4"],
            type: "Administrator",
            accepted: false,
            rejected: false,
            registered: false,
            average: 61
        }, {
            name: "Jaimeiris Nieves",
            email: "jaimeiris.nieves@upr.edu",
            evaluations: ["1", "2"],
            type: "Professional",
            accepted: false,
            rejected: false,
            registered: true,
            average: 63
        }, {
            name: "Heidi Negron",
            email: "heidi.negron1@upr.edu",
            evaluations: ["1", "2", "3"],
            type: "Evaluator",
            accepted: false,
            rejected: true,
            registered: false,
            average: 78
        } ];


        vm.activate = activate;
        vm.title = 'administratorCtrl';
        // Functions



        function activate() {

        }


    }
})();
