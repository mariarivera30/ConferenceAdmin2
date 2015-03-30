(function () {
    'use strict';

    var controllerId = 'sponsorInterfaceCtrl2';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', sponsorInterfaceCtrl2]);
    function sponsorInterfaceCtrl2($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        vm.title = 'sponsorInterfaceCtrl2';

        //Interface
        vm.iplatinumAmount;
        vm.igoldAmount;
        vm.isilverAmount;
        vm.ibronzeAmount;
        vm.iplatinumBenefits;
        vm.igoldBenefits;
        vm.isilverBenefits;
        vm.ibronzeBenefits;
        vm.instructions;

        //Functions
        vm.getBenefits = _getBenefits;
        vm.getInstructions = _getInstructions;

        _getInstructions();
        _getBenefits();
        function activate() {

        }

        function _getBenefits() {
            restApi.getAllSponsorBenefits()
            .success(function (data, status, headers, config) {
                if (data != null) {
                    vm.iplatinumAmount = data.platinumAmount;
                    vm.iplatinumBenefits = data.platinumBenefits;
                    vm.igoldAmount = data.goldAmount;
                    vm.igoldBenefits = data.goldBenefits;
                    vm.isilverAmount = data.silverAmount;
                    vm.isilverBenefits = data.silverBenefits;
                    vm.ibronzeAmount = data.bronzeAmount;
                    vm.ibronzeBenefits = data.bronzeBenefits;
                    load();
                }
            })

            .error(function (error) {

            });
        }

        function _getInstructions() {
            restApi.getInstructions()
            .success(function (data, status, headers, config) {
                if (data != null) {
                    vm.instructions = data;
                }
            })
            .error(function (error) {
            });
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("body").style.visibility = "visible";
        };
    }
})();