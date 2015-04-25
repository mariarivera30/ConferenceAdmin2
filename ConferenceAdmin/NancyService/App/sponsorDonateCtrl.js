(function () {
    'use strict';

    var controllerId = 'sponsorDonateCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi','$window', sponsorDonateCtrl]);

    function sponsorDonateCtrl($scope, $http, restApi,$window) {
        var vm = this;
        vm.activate = activate;
        //add sponsor fields
        vm.title = 'sponsorDonateCtrl';
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
        vm.donation = 0;
        vm.detailType;
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
        if ($window.sessionStorage.getItem('userID') != null)
            vm.userID = $window.sessionStorage.getItem('userID');
        else {
            $location.path("/Home");
        }

        vm.getSponsorTypes = _getSponsorTypes;
        vm.getBenefits = _getBenefits;
        vm.sponsorPayment = _sponsorPayment;
        vm.rangeInvalid = _rangeInvalid;
        vm.checkEmailBeforePayment = _checkEmailBeforePayment;
        vm.showDetails = _showDetails;
        

        activate();


        // Functions
        function activate() {
            _getSponsorbyID();
    
            vm.loading = true;
            _getBenefits();
            vm.donation = 0;


        }



        function _getSponsorbyID() {
            restApi.getSponsorbyID(vm.userID).
                   success(function (data, status, headers, config) {
                       vm.sponsor = JSON.parse(JSON.stringify(data));
                       vm.currentSponsor = JSON.parse(JSON.stringify(data));
                       _getSponsorTypes();
                       vm.loading = false;
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');
                       vm.loading = false;
                   });

        }

        function _sponsorPayment() {
        
           

            restApi.sponsorPayment(vm.sponsor)
                .success(function (data, status, headers, config) {
                       vm.loadingUploading = false;
                       window.open(data);
                      

                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');

                   });
        }

        

        //This method validate the amount to be donated. Amount and sponsor type should match
        function _rangeInvalid() {
            if (vm.sponsor != undefined && vm.sponsorType != undefined) {
            
                if (vm.sponsorType == 1 && (vm.sponsor.amount + vm.donation) < (vm.sponsorsTypeList[vm.sponsorType].amount + 1)) {
                    return true;
                }
                if (vm.sponsorType == 5 && (vm.sponsor.amount + vm.donation) < 1 || (vm.sponsor.amount + vm.donation) > (vm.sponsorsTypeList[vm.sponsorType - 1].amount)) {

                    return true;
                }
                if ((vm.sponsorType != 1 && vm.sponsorType != 5) && (vm.sponsor.amount + vm.donation) < (vm.sponsorsTypeList[vm.sponsorType].amount + 1) || vm.donation > (vm.sponsorsTypeList[vm.sponsorType - 1].amount)) {
                    return true;
                }
                else if (vm.donation == 0 || vm.donation == undefined) {
                    return true;
                }
                else {
                    vm.sponsor.newAmount = vm.donation;
                    return false;
                }
            }
     
        else{return true;}
        }
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

        //---------------------------Sponsor-------------------------------------------------



        function _getSponsorTypes() {
            restApi.getSponsorTypesList().
                   success(function (data, status, headers, config) {
                       vm.sponsorsTypeList = data;
                       vm.dropDown = JSON.parse(JSON.stringify(data));
                       if (data != null) {
                           if (vm.sponsor.active) {
                               if (vm.sponsor.sponsorType==5) {
                                   vm.dropDown.splice(vm.sponsor.sponsorType, 5 - vm.sponsor.sponsorType);
                                   vm.sponsorType = vm.sponsor.sponsorType +1;
                               }
                              
                               else if (vm.sponsor.amount == vm.sponsorsTypeList[vm.sponsor.sponsorType -1].amount) {
                                   vm.dropDown.splice(vm.sponsor.sponsorType-1, 5 - vm.sponsor.sponsorType+1 );
                                   vm.sponsorType = vm.sponsor.sponsorType -1;
                               }
                               else  {
                                   vm.dropDown.splice(vm.sponsor.sponsorType, 5 - vm.sponsor.sponsorType);
                                   vm.sponsorType = vm.sponsor.sponsorType;
                               }
                           }
                           
                           else {
                               vm.sponsorType = 1;
                           }
                       
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
            vm.TYPE = vm.sponsorsTypeList[vm.sponsorType - 1];
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

        function _showDetails(type) {
            vm.detailType= type;
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("body").style.visibility = "visible";
        };



    }
})();





