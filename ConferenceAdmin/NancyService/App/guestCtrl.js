(function () {
    'use strict';

    var controllerId = 'guestCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', guestCtrl]);

    function guestCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        //object fields
        vm.title = 'guestCtrl';
        vm.userID;
        vm.firstName;
        vm.lastName;
        vm.userType;
        vm.acceptanceStatus;
        vm.authorizationStatus;
        vm.photoAuthorization;
        vm.overnightAuthorization;
        vm.companionAuthorization;
        vm.participationAuthorization;
        vm.title;
        vm.affiliationName;
        vm.line1;
        vm.line2;
        vm.city;
        vm.state;
        vm.country;
        vm.zipcode;
        vm.email;
        vm.phoneNumber;
        vm.fax;
        vm.day1;
        vm.day2;
        vm.day3;
        vm.companionFirstName;
        vm.companionLastName;
        vm.authorizations = {};
        vm.documentName;
        vm.documentFile;
        vm.authFirstName;
        vm.authLastName;
        vm.isRegistered;
        vm.registrationStatus;
        vm.currentid;

        vm.guestList = {};
        vm.acceptanceStatusList = ['Accepted', 'Rejected'];

        //Functions
        vm.getGuestList = _getGuestList;
        vm.updateAcceptanceStatus = _updateAcceptanceStatus;
        vm.displayGuest = _displayGuest;
        vm.displayAuthorizations = _displayAuthorizations;
        vm.rejectRegisteredGuest = _rejectRegisteredGuest;
        vm.rejectedSelectedGuest = _rejectedSelectedGuest;
        vm.downloadPDFFile = _downloadPDFFile;
        vm.getDates = _getDates;

        _getGuestList();
        _getDates();

        //Functions:
        function activate() {

        }

        function _getDates() {
            restApi.getDates().
                   success(function (data, status, headers, config) {
                       vm.datesList = data;
                       vm.date1 = vm.datesList[0];
                       vm.date2 = vm.datesList[1];
                       vm.date3 = vm.datesList[2];
                   }).
                   error(function (data, status, headers, config) {
                       vm.datesList = data;
                       vm.date1 = vm.datesList[0];
                       vm.date2 = vm.datesList[1];
                       vm.date3 = vm.datesList[2];
                   });
        }

        function _displayGuest(userID, firstName, lastName, acceptanceStatus, isRegistered, authorizationStatus, title, affiliationName,
            line1, line2, city, state, country, zipcode, email, phoneNumber, fax, day1, day2, day3, companionFirstName, companionLastName) {

            vm.modalFirstName = firstName;
            vm.modalLastName = lastName;
            vm.modalAcceptanceStatus = acceptanceStatus;
            vm.modalIsRegistered = isRegistered;
            vm.modalAuthorizationStatus = authorizationStatus;
            vm.modalTitle = title;
            vm.modalAffiliationName = affiliationName;
            vm.modalLine1 = line1;
            vm.modalLine2 = line2;
            vm.modalCity = city;
            vm.modalState = state;
            vm.modalCountry = country;
            vm.modalZipcode = zipcode;
            vm.modalEmail = email;
            vm.modalPhoneNumber = phoneNumber;
            vm.modalFax = fax;
            vm.modalDay1 = day1;
            vm.modalDay2 = day2;
            vm.modalDay3 = day3;
            vm.modalCompanionFirstName = companionFirstName;
            vm.modalCompanionLastName = companionLastName;
        }

        function _displayAuthorizations(userID, firstName, lastName) {
            restApi.displayAuthorizations(userID).
                   success(function (data, status, headers, config) {
                       // this callback will be called asynchronously
                       // when the response is available
                       vm.authorizations = data;
                       vm.authFirstName = firstName;
                       vm.authLastName = lastName;
                   }).
                   error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       vm.authorizations = data;
                   });
        }

        function _getGuestList() {
            restApi.getGuestList().
                   success(function (data, status, headers, config) {
                       // this callback will be called asynchronously
                       // when the response is available
                       vm.guestList = data;
                   }).
                   error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       vm.guestList = data;
                   });
        }

        function _updateAcceptanceStatus(userID, acceptanceStatus) {
            var localGuest = { ID: userID, status: acceptanceStatus };
            restApi.updateAcceptanceStatus(localGuest)
                .success(function (data, status, headers, config) {
                    vm.guestList.forEach(function (guest, index) {
                        if (guest.userID == userID) {
                            guest.acceptanceStatus = acceptanceStatus;
                        }
                    })
                })

                .error(function (error) {

                });
        }

        function _rejectedSelectedGuest(userID) {
            vm.currentid = userID;
        }

        function _rejectRegisteredGuest() {
            if (vm.currentid != undefined) {
                restApi.rejectRegisteredGuest(vm.currentid)
                    .success(function (data, status, headers, config) {
                        vm.guestList.forEach(function (guest, index) {
                            if (guest.userID == vm.currentid) {
                                guest.isRegistered = false;
                                guest.registrationStatus = "Rejected";
                                guest.acceptanceStatus = "Rejected";
                                guest.day1 = false;
                                guest.day2 = false;
                                guest.day3 = false;
                            }
                        });


                    })


                .error(function (error) {

                });
            }
        }

        function _downloadPDFFile(document) {
            window.open(document);
        }
    }
})();