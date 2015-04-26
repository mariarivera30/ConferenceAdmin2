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
        vm.idiamondAmount;
        vm.iplatinumAmount;
        vm.igoldAmount;
        vm.isilverAmount;
        vm.ibronzeAmount;
        vm.idiamondBenefits = [];
        vm.iplatinumBenefits = [];
        vm.igoldBenefits = [];
        vm.isilverBenefits = [];
        vm.ibronzeBenefits = [];
        vm.instructions;

        //For error modal:
        vm.obj = {
            title: "",
            message1: "",
            message2: "",
            label: "",
            okbutton: false,
            okbuttonText: "",
            cancelbutton: false,
            cancelbuttoText: "Cancel",
        };
        vm.okFunc;
        vm.cancelFunc;

        vm.toggleModal = function (action) {

            if (action == "error")
                vm.obj.title = "Server Error",
                vm.obj.message1 = "Please refresh the page and try again.",
                vm.obj.message2 = "",
                vm.obj.label = "",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "OK",
                vm.obj.cancelbutton = false,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
        };

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
                if (data != null && data != "") {
                    vm.idiamondAmount = data.diamondAmount;
                    vm.idiamondBenefits = data.diamondBenefits;
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
                load();
                vm.toggleModal('error');
            });
        }

        function _getInstructions() {
            restApi.getInstructions()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.instructions = data;
                }
            })
            .error(function (error) {
                vm.toggleModal('error');
            });
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("body").style.visibility = "visible";
        };
    }
})();