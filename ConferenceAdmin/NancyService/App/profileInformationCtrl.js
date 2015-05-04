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
        vm.wrongKey;
        vm.hasKey = false;
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

        if (vm.userID != null) {
            _getProfileInfo(vm.userID);
            _getUserTypes();
            _getCompanionKey();
        }

        function activate() {

        }


        function toggleEdit() {
            vm.edit ? vm.edit = false : vm.edit = true;
        }

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
                   }).
                   error(function (data, status, headers, config) {
                       alert("An error occurred trying to access your Profile Information.");
                   });
        }


        function _updateProfileInfo() {
            restApi.updateProfileInfo(vm).
                    success(function (data, status, headers, config) {
                    }).
                    error(function (data, status, headers, config) {
                        alert("An error occurred trying to access your Profile Information.");
                    });
            vm.edit = false;
        }

        //if not Minor
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

        //if Minor
        function _selectCompanion(companionKey) {
            vm.companionKey = companionKey;
            restApi.selectCompanion(vm)
            .success(function (data, status, headers, config) {
                vm.wrongKey = false;
                vm.correctKey = true;
                vm.hasKey = true;
                if (data == "Accepted")
                    vm.companionRegistered = true;
                else
                    vm.companionRegistered = false;
            })
            .error(function (error) {
                vm.wrongKey = true;
                vm.correctKey = false;
                vm.hasKey = false;
            });
        }

        //if Minor
        function _getCompanionKey() {
            restApi.getCompanionKey(vm.userID)
            .success(function (data, status, headers, config) {
                vm.companionKey = data.companionKey;
                if (vm.companionKey != null)
                    vm.hasKey = true;
                if (data == "Accepted")
                    vm.companionRegistered = true;
                else
                    vm.companionRegistered = false;
            })
            .error(function (error) {
                vm.hasKey = false;
            });
        }


        function _apply() {
            restApi.apply(vm).
                    success(function (data, status, headers, config) {
                        vm.hasApplied = true;
                    }).
                    error(function (data, status, headers, config) {
                        alert("An error occurred");
                    });
        }

        function _getUserTypes() {
            restApi.getUserTypes().
                   success(function (data, status, headers, config) {
                       vm.userTypesList = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.userTypesList = data;
                   });
        }

        function _userPayment() {
            vm.loadingUploading = true;
            restApi.userPayment(vm).
                success(function (data, status, headers, config) {
                    vm.loadingUploading = false;
                    if (data != null) {
                        window.open(data);
                        $location.path('/profile/receiptinformation');
                        
                    }
                    else {
                    alert("An error occurred");
                    }
                }).
                error(function (data, status, headers, config) {
                    alert("An error occurred");
                });
        }

        function _complementaryPayment() {
       
            restApi.complementaryPayment(vm).
                success(function (data, status, headers, config) {
                    vm.registrationStatus = "Accepted";
                }).
                error(function (data, status, headers, config) {
                    alert("An error occurred");
                });
        }

        function _getDates() {
            restApi.getDates().
                   success(function (data, status, headers, config) {
                       vm.datesList = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.datesList = data;
                   });
        }


        ///Deadlines registation fee
        function _getUserAmount() {
            restApi.getUserAmountInDeadline()
            .success(function (data, status, headers, config) {
                vm.amount = data;

            })
            .error(function (error) {
                load();
                vm.toggleModal('error');
            });
        }

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
