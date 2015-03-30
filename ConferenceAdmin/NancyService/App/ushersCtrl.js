(function () {
    'use strict';

    var controllerId = 'ushersCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', ushersCtrl]);

    function ushersCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        //add sponsor fields
        vm.title = 'ushersCtrl';
        vm.sponsor;
        vm.loading;
        vm.CCWICSponsorID = 1;
        vm.addComplementaryObj = { sponsorID: 0, quantity: 0, company: "" };

        // Functions



        vm.getSponsorComplementaryKeys = _getSponsorComplementaryKeys;
        vm.deleteComplemetaryKey = _deleteComplemetaryKey;
        vm.deleteSponsorComplemetaryKey = _deleteSponsorComplemetaryKey;
        vm.addComplementaryKey = _addComplementaryKey;
        vm.selectedKey = _selectedKey;

        activate();

        // Functions
        function activate() {

            vm.loading = true;
            _getSponsorbyID();

        }

        function _selectedKey(key) {
            vm.key = key;
        }

        function _getSponsorbyID() {
            restApi.getSponsorbyID(vm.CCWICSponsorID).
                   success(function (data, status, headers, config) {
                       vm.sponsor = data;
                       vm.loading = false;
                   }).
                   error(function (data, status, headers, config) {
                       alert("add un aler sexy");
                       vm.loading = false;
                   });
            _getSponsorComplementaryKeys();
        }

        //--------------------------Complementnary----------------------------------------
        function _getSponsorComplementaryKeys() {
            vm.loadingComp = true;
            restApi.getSponsorComplementaryKeys(1).
                   success(function (data, status, headers, config) {
                       vm.sponsorKeys = data;
                       vm.loadingComp = false;
                   }).
                   error(function (data, status, headers, config) {
                       vm.loadingComp = false;
                       alert("add un aler sexy");

                   });
        }

        function _deleteComplemetaryKey() {

            vm.loadingRemovingComp = true;
            restApi.deleteComplemetaryKey(vm.key.complementarykeyID)
            .success(function (data, status, headers, config) {
                vm.sponsorKeys.forEach(function (key, index) {
                    if (key.complementarykeyID == vm.key.complementarykeyID) {
                        vm.sponsorKeys.splice(index, 1);
                        vm.loadingRemovingComp = false;
                        $('#delete').modal('hide');
                    }

                });

            })

            .error(function (data, status, headers, config) {
                alert("add un aler sexy");
                vm.loadingRemovingComp = false;
                $('#delete').modal('hide');
                _clearSponsor();

            });
        }

        function _deleteSponsorComplemetaryKey() {

            vm.loadingRemovingComp = true;
            restApi.deleteSponsorComplemetaryKey(vm.sponsor.sponsorID)
            .success(function (data, status, headers, config) {
                vm.sponsorKeys = data;
                vm.loadingRemovingComp = false;
            })

            .error(function (data, status, headers, config) {
                alert("add un aler sexy");
                vm.loadingRemovingComp = false;


            });
        }
        function _addComplementaryKey() {
            vm.addComplementaryObj.sponsorID = vm.sponsor.sponsorID;
            vm.addComplementaryObj.quantity = vm.quantity;
            vm.addComplementaryObj.company = vm.sponsor.company;
            vm.uploadingComp = true;
            restApi.addComplementaryKey(vm.addComplementaryObj)
                     .success(function (data, status, headers, config) {
                         vm.sponsorKeys = data;
                         vm.uploadingComp = false;
                         vm.quantity = 0;

                     })

                     .error(function (error) {
                         vm.uploadingComp = false;
                         vm.quantity = 0;
                         alert("add un aler sexy");

                     });
        }








    }
})();





