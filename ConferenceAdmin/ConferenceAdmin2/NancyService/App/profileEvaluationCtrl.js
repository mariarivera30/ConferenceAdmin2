(function () {
    'use strict';

    var controllerId = 'profileEvaluationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', profileEvaluationCtrl]);

    function profileEvaluationCtrl($scope, $http) {
        var vm = this;

        vm.list = [{
            name: "Randy Soto",
            email: "randy.soto@upr.edu",
            type: "Extended Paper",
            accepted: true,
            rejected: false
        }, {
            name: "Maria Rivera",
            email: "maria.rivera30@upr.edu",
            type: "Pannel",
            accepted: false,
            rejected: false
        }, {
            name: "Jaimeiris Nieves",
            email: "jaimeiris.nieves@upr.edu",
            type: "Poster",
            accepted: false,
            rejected: false
        }, {
            name: "Heidi Negron",
            email: "heidi.negron1@upr.edu",
            type: "Workshop",
            accepted: false,
            rejected: true
        }];


        vm.activate = activate;
        vm.title = 'profileEvaluationCtrl';

        function activate() {

        }


    }
})();




