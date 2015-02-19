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
            evaluations: ["1", "2", "3"]
        }, {
            name: "Maria Rivera",
            email: "maria.rivera30@upr.edu",
            evaluations: ["1", "2", "3", "4"]
        }, {
            name: "Jaimeiris Nieves",
            email: "jaimeiris.nieves@upr.edu",
            evaluations: ["1", "2"]
        }, {
            name: "Heidi Negron",
            email: "heidi.negron1@upr.edu",
            evaluations:["1", "2", "3"]
        } ];


        vm.activate = activate;
        vm.title = 'administratorCtrl';
        // Functions



        function activate() {

        }


    }
})();
