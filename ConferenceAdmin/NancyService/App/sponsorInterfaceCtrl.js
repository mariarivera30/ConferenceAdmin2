(function () {
    'use strict';

    var controllerId = 'sponsorInterfaceCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', sponsorInterfaceCtrl]);
    function sponsorInterfaceCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        vm.title = 'sponsorInterfaceCtrl';

        //For Admin Modal
        vm.temp;
        vm.selectedSponsorType;
        vm.amount;
        vm.benefits = {};
        vm.instructions;

        //Functions
        vm.getBenefits = _getBenefits;
        vm.saveBenefits = _saveBenefits;
        vm.getInstructions = _getInstructions;
        vm.saveInstructions = _saveInstructions;
        vm.selectedSponsor = _selectedSponsor;
        vm.clear = _clear;
        vm.reset = _reset;

        _getInstructions();

        function activate() {

        }

        function _reset() {
            vm.instructions = vm.temp;
        }

        function _clear() {
            vm.selectedSponsorType = "";
            vm.amount = "";
            vm.benefits = {};
        }

        function _selectedSponsor(sponsorType) {
            vm.sponsorType = sponsorType;
            if (sponsorType == "Platinum") {
                _getBenefits("Platinum");
            }

            else if (sponsorType == "Gold") {
                _getBenefits("Gold");
            }

            else if (sponsorType == "Silver") {
                _getBenefits("Silver");
            }

            else if (sponsorType == "Bronze") {
                _getBenefits("Bronze");
            }
        }

        function _getBenefits(sname) {
            restApi.getAdminSponsorBenefits(sname)
            .success(function (data, status, headers, config) {
                if (data != null) {

                    if (sname == "Platinum") {
                        vm.amount = data.platinumAmount;
                        vm.benefits = data.platinumBenefits;
                        $("#editSponsorBenefits").modal('show');
                    }
                    else if (sname == "Gold") {
                        vm.amount = data.goldAmount;
                        vm.benefits = data.goldBenefits;
                        $("#editSponsorBenefits").modal('show');
                    }
                    else if (sname == "Silver") {
                        vm.amount = data.silverAmount;
                        vm.benefits = data.silverBenefits;
                        $("#editSponsorBenefits").modal('show');
                    }
                    else if (sname == "Bronze") {
                        vm.amount = data.bronzeAmount;
                        vm.benefits = data.bronzeBenefits;
                        $("#editSponsorBenefits").modal('show');
                    }
                }
            })

            .error(function (error) {

            });
        }

        function _saveBenefits() {
            var saveSponsor = {
                name: vm.sponsorType,
                amount: vm.amount,
                benefits: vm.benefits
            }
            restApi.saveAdminSponsorBenefits(saveSponsor)
            .success(function (data, status, headers, config) {
                if (data != null) {
                    $("#updateConfirm").modal('show');
                }
            })
            .error(function (error) {
                $("#updateError").modal('show');
            });
        }

        function _getInstructions() {
            restApi.getInstructions()
            .success(function (data, status, headers, config) {
                if (data != null) {
                    vm.temp = data;
                    vm.instructions = data;
                }
            })
            .error(function (error) {
            });
        }

        function _saveInstructions() {
            restApi.saveInstructions(vm.instructions)
            .success(function (data, status, headers, config) {
                if (data) {
                    vm.temp = vm.instructions;
                    $("#updateConfirm").modal('show');
                }
            })
            .error(function (error) {
                $("#updateError").modal('show');
            });
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("body").style.visibility = "visible";
        };
    }
})();