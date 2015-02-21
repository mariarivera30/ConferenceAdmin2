(function () {
    'use strict';

    var controllerId = 'profileCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', profileCtrl]);

    function profileCtrl($scope, $http) {
        var vm = this;
        vm.list = [{
            name: "Randy Soto",
            email: "randy.soto@upr.edu",
            evaluations: ["1", "2", "3"],
            type: "Evaluator Admin",
            accepted: true,
            rejected: false
        }, {
            name: "Maria Rivera",
            email: "maria.rivera30@upr.edu",
            evaluations: ["1", "2", "3", "4"],
            type: "Administrator",
            accepted: false,
            rejected: false
        }, {
            name: "Jaimeiris Nieves",
            email: "jaimeiris.nieves@upr.edu",
            evaluations: ["1", "2"],
            type: "Finance Admin",
            accepted: false,
            rejected: false
        }, {
            name: "Heidi Negron",
            email: "heidi.negron1@upr.edu",
            evaluations: ["1", "2", "3"],
            type: "Usher",
            accepted: false,
            rejected: true
        }];


        vm.activate = activate;
        vm.title = 'profileCtrl';
        // Functions



        function activate() {

        }


    }
})();
