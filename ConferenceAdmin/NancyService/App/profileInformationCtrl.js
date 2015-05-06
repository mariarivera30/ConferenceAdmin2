(function () {
    'use strict';

    var controllerId = 'profileInformationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi','$window','$location', profileInformationCtrl]);

    function profileInformationCtrl($scope, $http, restApi, $window, $location) {
        var vm = this;
        vm.edit = false;
        vm.activate = activate;
        if ($window.sessionStorage.getItem('userID') != null)
            vm.userID = $window.sessionStorage.getItem('userID');
        else {
            $location.path("/Home");
        }

        // User Attributes
        vm.title;
        vm.firstName;
        vm.lastName;
        vm.affiliationName;
        vm.addressLine1;
        vm.addressLine2;
        vm.city;
        vm.state;
        vm.country;
        vm.zipcode;
        vm.email;
        vm.phone;
        vm.userFax;
        vm.date1;
        vm.date2;
        vm.date3;
        vm.notes;
        vm.companionKey;
        vm.key;
        vm.secureLink;
        vm.wrongKey;
        vm.hasKey = false;

        //Payemnt
        vm.obj = {};
        vm.amount;
        vm.amountStatus;

        vm.hasCompKey = false;
        // Application Attributes
        vm.acceptanceStatus;
        vm.registrationStatus;
        vm.hasApplied;
        vm.userTypeID;
        vm.userTypesList = {};
        vm.datesList = {};

        // Function Definitions
        vm.toggleEdit = toggleEdit;
        vm.updateProfileInfo = _updateProfileInfo;
        vm.getProfileInfo = _getProfileInfo;
        vm.apply = _apply;
        vm.getUserTypes = _getUserTypes;
        vm.userPayment = _userPayment;
        vm.complementaryPayment = _complementaryPayment;
        vm.selectCompanion = _selectCompanion;
        vm.getCompanionKey = _getCompanionKey;
        vm.checkComplementaryKey = _checkComplementaryKey;

        //vm.checkAll;
        vm.loading;
        _getDates();
        //payment Fucntions
        vm.getUserPriceInDeadline = _getUserPriceInDeadline;
        vm.goToBillAfter = _goToBillAfter;
        vm.goToBill = _goToBill;
        activate();

        function activate() {
            if (vm.userID != null) {
                _getProfileInfo(vm.userID);
                _getUserTypes();
                _getCompanionKey();

            }
        }

        /* [Maria] Display dialogs of error or payments */
        function _goToBillAfter() {
            window.open(vm.secureLink);
            $location.path('/profile/receiptinformation');
        }
        function _goToBill() {
           
            $location.path('/profile/receiptinformation');
        }
        vm.toggleModal = function (action) {


            if (action == "error") {
                vm.obj.title = "Server Error",
               vm.obj.message1 = "Please refresh the page and try again.",
               vm.obj.message2 = "",
               vm.obj.label = "",
               vm.obj.okbutton = true,
               vm.obj.okbuttonText = "OK",
               vm.obj.cancelbutton = false,
               vm.obj.cancelbuttoText = "Cancel",
               vm.showConfirmModal = !vm.showConfirmModal;
            }
            if (action == "paymenterror") {
                vm.obj.title = "Payment Error",
               vm.obj.message1 = "Please refresh the page and try to submit the payment again.",
               vm.obj.message2 = "",
               vm.obj.label = "",
               vm.obj.okbutton = true,
               vm.obj.okbuttonText = "OK",
               vm.obj.cancelbutton = false,
               vm.obj.cancelbuttoText = "Cancel",
               vm.showConfirmModal = !vm.showConfirmModal;

            }
            if (action == "Register") {
                vm.obj.title = "CCWIC Registration",
               vm.obj.message1 = "Your Registration was completed successfully. ",
               vm.obj.message2 = "",
               vm.obj.label = "",
               vm.obj.okbutton = true,
               vm.obj.okbuttonText = "OK",
               vm.obj.cancelbutton = false,
               vm.obj.cancelbuttoText = "Cancel",
               vm.showConfirmModal = !vm.showConfirmModal;
                vm.okFunc = vm.goToBill;
               
            }

            if (action == "paymentInProcess") {
                vm.obj.title = "Payment In Process",
                vm.obj.message1 = "Please complete the payment in the next page.",
                vm.obj.message2 = "",
                vm.obj.label = "",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "OK",
                vm.obj.cancelbutton = false,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
                vm.okFunc = vm.goToBillAfter;
            }
        };

        /* [Randy] show/hide edit form */
        function toggleEdit() {
            vm.edit ? vm.edit = false : vm.edit = true;
        }

        /* [Randy] get information for the user profile */
        function _getProfileInfo(userID) {
            restApi.getProfileInfo(userID).
                   success(function (data, status, headers, config) {
                       vm.title = data.title;
                       vm.firstName = data.firstName;
                       vm.lastName = data.lastName;
                       vm.affiliationName = data.affiliationName;
                       vm.addressLine1 = data.addressLine1;
                       vm.addressLine2 = data.addressLine2;
                       vm.city = data.city;
                       vm.state = data.state;
                       vm.country = data.country;
                       vm.zipcode = data.zipcode;
                       vm.email = data.email;
                       vm.phone = data.phone;
                       vm.userFax = data.userFax;
                       vm.date1 = data.date1;
                       vm.date2 = data.date2;
                       vm.date3 = data.date3;
                       vm.hasApplied = data.hasApplied;
                       vm.acceptanceStatus = data.acceptanceStatus;
                       vm.registrationStatus = data.registrationStatus;
                       vm.userTypeID = data.userTypeID;
                       vm.notes = data.notes;
                       vm.key = data.key;
                       _getUserPriceInDeadline();
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');
                   });
        }

        /* [Randy] edit information of the user profile */
        function _updateProfileInfo() {
            restApi.updateProfileInfo(vm).
                    success(function (data, status, headers, config) {
                    }).
                    error(function (data, status, headers, config) {
                        vm.toggleModal('error');
                    });
            vm.edit = false;
        }

        /* [Randy] verify complementary key */
        function _checkComplementaryKey(complementaryKey) {
            restApi.checkComplementaryKey(complementaryKey)
            .success(function (data, status, headers, config) {
                if (data) {
                    vm.wrongKey = false;
                    vm.correctComplementaryKey = true;
                    vm.hasKey = true;
                }
                else {
                    vm.wrongKey = true;
                    vm.correctComplementaryKey = false;
                    vm.hasKey = false;
                }
            })
            .error(function (error) {
                vm.wrongKey = true;
                vm.correctComplementaryKey = false;
                vm.hasKey = false;
            });
        }

        /* [Randy] verify companion key and assign minor to companion */
        function _selectCompanion(companionKey) {
            vm.companionKey = companionKey;
            restApi.selectCompanion(vm)
            .success(function (data, status, headers, config) {
                vm.wrongCompKey = false;
                vm.correctKey = true;
                vm.hasCompKey = true;
                if (data == "Accepted")
                    vm.companionRegistered = true;
                else
                    vm.companionRegistered = false;
            })
            .error(function (error) {
                vm.wrongCompKey = true;
                vm.correctKey = false;
                vm.hasCompKey = false;
            });
        }

        /* [Randy] get selected companion key */
        function _getCompanionKey() {
            restApi.getCompanionKey(vm.userID)
            .success(function (data, status, headers, config) {
                vm.companionKey = data.companionKey;
                if (vm.companionKey != null)
                    vm.hasCompKey = true;
                if (data == "Accepted")
                    vm.companionRegistered = true;
                else
                    vm.companionRegistered = false;
            })
            .error(function (error) {
                vm.hasCompKey = false;
            });
        }

        /* [Randy] make application to attend the conference */
        function _apply() {
            restApi.apply(vm).
                    success(function (data, status, headers, config) {
                        vm.hasApplied = true;
                    }).
                    error(function (data, status, headers, config) {
                        alert("An error occurred");
                    });
        }

        /* [Randy] get list of types of users */
        function _getUserTypes() {
            restApi.getUserTypes().
                   success(function (data, status, headers, config) {
                       vm.userTypesList = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.userTypesList = data;
                   });
        }

        /* [Maria] make the payment as a user */
        function _userPayment() {
            vm.loadingUploading = true;
            vm.amount = vm.amountStatus.amount;
            restApi.userPayment(vm).
                success(function (data, status, headers, config) {
                    vm.loadingUploading = false;
                    if (data != null) {
                        vm.secureLink = null;
                        if (data == "billCreated") {
                            vm.toggleModal('Register');
                           
                        }
                        else {
                            vm.secureLink = data;
                            vm.toggleModal('paymentInProcess');
                        }
                       
                        
                    }
                    else {
                        vm.toggleModal('error');
                    }
                }).
                error(function (data, status, headers, config) {
                    vm.toggleModal('error');
                });
        }

        /* [Randy] register as complementary with a payment of zero */
        function _complementaryPayment() {
       
            restApi.complementaryPayment(vm).
                success(function (data, status, headers, config) {
                    vm.toggleModal('Register');
                    vm.registrationStatus = "Accepted";

                }).
                error(function (data, status, headers, config) {
                    vm.toggleModal('error');
                });
        }

        /* [Randy] get dates of the conference */
        function _getDates() {
            restApi.getDates().
                   success(function (data, status, headers, config) {
                       vm.datesList = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.datesList = data;
                   });
        }


        /* [Maria] deadlines registation fee */
        function _getUserPriceInDeadline() {
            restApi.getUserPriceInDeadline(vm.userTypeID)
            .success(function (data, status, headers, config) {
                vm.amountStatus = data;

                if (vm.amountStatus == null) {
                    vm.toggleModal('error');
                }
                else {
                    if (vm.amountStatus.amount == 0)
                        vm.paymentbuttonText = "Register";
                    else {
                        vm.paymentbuttonText = "Make Payment";
                    }
                    }
                        

            })
            .error(function (error) {
   
                vm.toggleModal('error');
            });
        }

        /* [Maria] get receipt */
        function _getUserPayments() {
            vm.loadingComp = true;
            restApi.getSponsorPayments(vm.userID).
                   success(function (data, status, headers, config) {
                       vm.bills = data;
                       vm.loadingComp = false;
                       if (vm.bills.length == 0)
                           vm.messageVisible = true;
                       else {
                           vm.payment = vm.bills[0];
                       }
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');
                       vm.loadingComp = false;
                       vm.loadingComp = false;
                   });

        }
    }
})();
