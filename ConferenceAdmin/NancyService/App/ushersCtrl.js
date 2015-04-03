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
            if (action === "remove") {

                vm.obj.title = "Remove Sponsor",
                vm.obj.message1 = "This action will remove the sponsor. Are you sure you want to continue?",
                vm.obj.message2 = vm.sponsor.firstName + " " + vm.sponsor.lastName,
                vm.obj.label = "Sponsor Name:",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "Remove",
                vm.obj.cancelbutton = true,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
                vm.okFunc = vm.deleteSponsor();
                vm.cancelFunc;

            }
            if (action === "removeKeys") {

                vm.obj.title = "Remove Complementary Key",
                vm.obj.message1 = "This action will remove all complementary keys of this sponsor. Are you sure you want to continue?",
                vm.obj.message2 = "",
                vm.obj.label = "",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "Remove",
                vm.obj.cancelbutton = true,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
                vm.okFunc = vm.deleteSponsorComplemetaryKey;
                vm.cancelFunc;

            }
            if (action === "removeKey") {

                vm.obj.title = "Remove Complementary Key",
                vm.obj.message1 = "This action will remove a complementary key. Are you sure you want to continue?",

                vm.obj.message2 = vm.keyPop,
                vm.obj.label = "Complementary Key:",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "Remove",
                vm.obj.cancelbutton = true,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
                vm.okFunc = vm.deleteComplemetaryKey;
                vm.cancelFunc;

            }
            else if (action == "error")
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
            vm.keyPop = key.key;
        }

        function _getSponsorbyID() {
            restApi.getSponsorbyID(vm.CCWICSponsorID).
                   success(function (data, status, headers, config) {
                       vm.sponsor = data;
                       vm.loading = false;
                       _getSponsorComplementaryKeys();
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');
                       vm.loading = false;
                   });
            
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
                       vm.toggleModal('error');

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
                vm.toggleModal('error');
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
                vm.toggleModal('error');
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
                         vm.toggleModal('error');

                     });
        }








    }
})();





