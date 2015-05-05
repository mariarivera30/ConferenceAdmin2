(function () {
    'use strict';

    var controllerId = 'profileSponsorCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi','$window', profileSponsorCtrl]);

    function profileSponsorCtrl($scope, $http, restApi,$window) {
        var vm = this;
        vm.activate = activate;
        //add sponsor fields
        vm.title = 'profileSponsorCtrl';
        vm.sponsor;
        vm.currentSponsor = {};
        vm.loading;
        vm.addComplementaryObj = { sponsorID: 0, quantity: 0, company: "" };
        vm.TYPE = {};
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

        // Functions
        
        vm.updateSponsor = _updateSponsor;
        vm.downloadLogo = _downloadLogo;
        vm.clearSponsor = _clearSponsor;
        
        vm.clearPic = _clearPic;

        if ($window.sessionStorage.getItem('userID') != null)
            vm.userID = $window.sessionStorage.getItem('userID');
        else {
            $location.path("/Home");
        }

       

        vm.pdf = "pdf";

        activate();

        $scope.showContent = function ($fileContent, file) {
            if ($fileContent != undefined) {
                $scope.content = $fileContent;
                vm.fileext = file.name.split(".", 2)[1];
                if (vm.fileext == "jpg" || vm.fileext == "png" || vm.fileext == "jpeg" || vm.fileext == "pic" || vm.fileext == "pict" || vm.fileext == "gif")
                    vm.ext = false;
                else {
                    document.getElementById("inputFile").value = "";
                    $scope.content = "";
                    $fileContent = "";
                    vm.ext = true;
                }


            }
        };

        // Functions
        function activate() {
            _getSponsorbyID();
            _getSponsorTypes();
            vm.loading = true;
            vm.complementaryView = false;

        }


        vm.toggleModal = function (action) {
            
            
            if (action == "errorFile") {

                vm.obj.title = "File type error",
                vm.obj.message1 = "That file extention is not support by this system. Try one of these(png, jpg, ext, gif, jpeg, pic,pict)";

                vm.obj.message2 = "",
                vm.obj.label = "",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "",
                vm.obj.cancelbutton = true,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
                vm.okFunc = "";
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

        function _selectedKey(key) {
            vm.key = key;
            vm.keyPop = key.key;
        }
        function _clearPic(File) {
            $scope.content = "";
            $scope.$fileContent = "";
            File = undefined;
            vm.ext = false;
            document.getElementById("inputFile").value = "";
        }

        function _clearSponsor() {
            vm.sponsor = vm.sponsor = JSON.parse(JSON.stringify(vm.currentSponsor));;
            _clearPic();

        }
      
      
      
     
        function _downloadLogo() {
            if (vm.sponsor.logo != undefined && vm.sponsor.logo != "") {
                vm.sub = vm.sponsor.logo.substring(11, 16).split(";")[0];
                var element = document.createElement('a');
                element.setAttribute("href", vm.sponsor.logo);
                element.setAttribute("download", vm.sponsor.company + "Logo" + "." + vm.sub);
                element.click();
            }
            

        }

        //---------------------------Sponsor-------------------------------------------------
        function _getSponsorbyID() {
            restApi.getSponsorbyID(vm.userID).
                   success(function (data, status, headers, config) {
                       vm.sponsor = JSON.parse(JSON.stringify(data));
                       vm.currentSponsor = JSON.parse(JSON.stringify(data));
               
                       vm.loading = false;
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');
                       vm.loading = false;
                   });

        }
        function _updateSponsor(myFile) {


            vm.TYPE = vm.sponsorsTypeList[vm.sponsor.sponsorType - 1];
            vm.sponsor.typeName = vm.TYPE.name;

            if (myFile != undefined && $scope.content != "" && $scope.content != undefined) {
                vm.sponsor.logoName = myFile.name;
                vm.sponsor.logo = $scope.content;
                myFile = undefined;

            }

            vm.loadingUploading = true;
            restApi.updateSponsor(vm.sponsor)
            .success(function (data, status, headers, config) {
                    vm.currentSponsor = JSON.parse(JSON.stringify(data));
                    vm.sponsor = JSON.parse(JSON.stringify(data));
                    vm.loadingUploading = false;
                    _clearPic();

            }

            )
            .error(function (data, status, headers, config) {
                vm.edit = false;
                _clearSponsor();
                vm.loadingUploading = false;
                vm.toggleModal('error');
                _clearPic();
            });


        }


        function _getSponsorTypes() {
            restApi.getSponsorTypesList().
                   success(function (data, status, headers, config) {
                       vm.sponsorsTypeList = data;
                       if (data != null)
                           vm.typeID = 0;

                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');

                   });
        }

       
        

    }
})();





