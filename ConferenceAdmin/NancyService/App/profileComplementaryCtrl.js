(function () {
    'use strict';

    var controllerId = 'profileComplementaryCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi','$window', profileComplementaryCtrl]);

    function profileComplementaryCtrl($scope, $http, restApi, $window) {
        var vm = this;
        vm.activate = activate;
        //add sponsor fields
        vm.title = 'profileComplementaryCtrl';
        vm.sponsor;
        vm.loadingComp;
        if ($window.sessionStorage.getItem('userID') != null)
            vm.userID = $window.sessionStorage.getItem('userID');
        else {
            $location.path("/Home");
        }
    
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

        //Complementary Keys- Variables (Paging)
        vm.sponsorKeys = []; //Results to Display
        vm.sindex = 0;  //Page index [Goes from 0 to smaxIndex-1]
        vm.smaxIndex = 0;   //Max page number

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

      
      
        vm.selectedKey = _selectedKey;

        //Functions- Sponsors (Paging)
        vm.getSponsorComplimentaryKeysFromIndex = _getSponsorComplimentaryKeysFromIndex;
        vm.previousComplimentary = _previousComplimentary;
        vm.nextComplimentary = _nextComplimentary;
        vm.getFirstComplimentaryPage = _getFirstComplimentaryPage;
        vm.getLastComplimentaryPage = _getLastComplimentaryPage;

        activate();

        // Functions
        function activate() {

            vm.loadingComp = true;
            _getSponsorbyID();

        }

        function _selectedKey(key) {
            vm.key = key;
            vm.keyPop = key.key;
        }
  
        function _getSponsorbyID() {
            restApi.getSponsorbyID(vm.userID).
                   success(function (data, status, headers, config) {
                       vm.sponsor = data;
                       vm.CCWICSponsorID = vm.sponsor.sponsorID;
                       _getSponsorComplimentaryKeysFromIndex(vm.sindex);
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');
                       vm.loadingComp = false;
                   });

        }

        //--------------------------Complementnary----------------------------------------
        function _getSponsorComplimentaryKeysFromIndex(index) {
            var info = {
                index: index,
                sponsorID : vm.CCWICSponsorID,
             
            }
            restApi.getSponsorComplementaryKeysFromIndex(info).
                   success(function (data, status, headers, config) {
                       vm.smaxIndex = data.maxIndex;
                       if (vm.smaxIndex == 0) {
                           vm.sindex = 0;
                           vm.sponsorKeys = [];
                       }
                       else if (vm.sindex >= vm.smaxIndex) {
                           vm.sindex = vm.smaxIndex - 1;
                           _getSponsorListFromIndex(vm.sindex);
                       }
                       else {
                           vm.sponsorKeys = data.results;
                       }
                       vm.loadingComp = false;
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');
                       vm.loadingComp = false;
                   });
        }

        function _nextComplimentary() {
            if (vm.sindex < vm.smaxIndex - 1) {
                vm.sindex += 1;
                _getSponsorComplimentaryKeysFromIndex(vm.sindex);
            }
        }

        function _previousComplimentary() {
            if (vm.sindex > 0) {
                vm.sindex -= 1;
                _getSponsorComplimentaryKeysFromIndex(vm.sindex);
            }
        }

        function _getFirstComplimentaryPage() {
            vm.sindex = 0;
            _getSponsorComplimentaryKeysFromIndex(vm.sindex);
        }

        function _getLastComplimentaryPage() {
            vm.sindex = vm.smaxIndex - 1;
            _getSponsorComplimentaryKeysFromIndex(vm.sindex);
        }

      
    }
})();





