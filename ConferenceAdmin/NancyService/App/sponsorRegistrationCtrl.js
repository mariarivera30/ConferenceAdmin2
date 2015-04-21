(function () {
    'use strict';

    var controllerId = 'sponsorRegCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', sponsorRegCtrl]);

    function sponsorRegCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        //add sponsor fields
        vm.title = 'sponsorRegCtrl';
        vm.sponsor = {};
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
        vm.idiamondAmount;
        vm.iplatinumAmount;
        vm.igoldAmount;
        vm.isilverAmount;
        vm.ibronzeAmount;
        vm.iplatinumBenefits;
        vm.igoldBenefits;
        vm.isilverBenefits;
        vm.ibronzeBenefits;
        vm.sponsorsTypeList = [];
   

        // Functions
        
        vm.getSponsorTypes = _getSponsorTypes;
        vm.addValues = _addValues;
        vm.downloadLogo = _downloadLogo;
        vm.clearPic = _clearPic;
        vm.getBenefits = _getBenefits;
        vm.sponsorPayment = _sponsorPayment;
        vm.viewInput = _viewInput;
        vm.checkEmailBeforePayment = _checkEmailBeforePayment;

        vm.amountMax = vm.sponsorsTypeList[vm.sponsor.sponsorType];
        vm.amountMin;


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
            _getSponsorTypes();
            vm.loading = true;
            _getBenefits();
      

        }

        function _viewInput() {
            return vm.sponsor.sponsorType == 1 || vm.sponsor.sponsorType == 5;
        }

        vm.toggleModal = function (action) {
             
            if (action == "emailMessage") {

                vm.obj.title = "Email is used",
                vm.obj.message1 = "Please if you are already are a Sponsor please click the link below to upgrade your Sponsorship account!";

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

        function _selectedSponsor(sponsor, action) {

            vm.sponsor = JSON.parse(JSON.stringify(sponsor));
            //  vm.typeID = sponsor.sponsorType ;

        }

        function _clearPic(File) {
            $scope.content = "";
            $scope.$fileContent = "";
            File = undefined;
            vm.sponsor.logo = "";
            vm.ext = false;
            document.getElementById("inputFile").value = "";

        }

        function _addValues() {
            vm.add = true;
            vm.edit = false;
            vm.view = false;
            vm.headerModal = "Add Sponsor";

            _clearSponsor();
            vm.sponsor = {};
            vm.sponsor.sponsorType = 1;
        }
   
      
      
      
        function _downloadLogo() {
            if (vm.sponsor.logo != undefined && vm.sponsor.logo != "")
                window.open(vm.sponsor.logo);

        }


       





        //---------------------------Sponsor-------------------------------------------------
        
    

        function _getSponsorTypes() {
            restApi.getSponsorTypesList().
                   success(function (data, status, headers, config) {
                       vm.sponsorsTypeList = data;
                       
                       if (data != null) {
                           vm.sponsor.sponsorType = 1;
                           vm.amountMax = vm.sponsorsTypeList[vm.sponsor.sponsorType].amount;
                       }
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');

                   });
        }

        function _checkEmailBeforePayment() {
            vm.loadingUploading = true;
            restApi.checkEmailSponsor(vm.sponsor.email)
                .success(function (data, status, headers, config) {
                    if (data != "") {
                        vm.toggleModal('emailMessage');
                        vm.loadingUploading = false;
                       }
                       else {
                        _sponsorPayment();
                       }

                   }).
                   error(function (data, status, headers, config) {

                       vm.loadingUploading = false;
                       $rootScope.$emit('popUp', 'error');
                   });
        }

        function _sponsorPayment() {
            vm.TYPE = vm.sponsorsTypeList[vm.sponsor.sponsorType - 1];
            vm.sponsor.typeName = vm.TYPE.name;
            if (File != undefined) {

                vm.sponsor.logo = $scope.content;
                vm.sponsor.logoName = File.name;
                File = null;
            }
            restApi.sponsorPayment(vm.sponsor).
                   success(function (data, status, headers, config) {
                       vm.loadingUploading = false;
                       window.open(data);
                       _clearSponsor();
    
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');

                   });
        }

     

        function _getBenefits() {
            restApi.getAllSponsorBenefits()
            .success(function (data, status, headers, config) {
                if (data != null) {
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

            });
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("body").style.visibility = "visible";
        };



    }
})();





