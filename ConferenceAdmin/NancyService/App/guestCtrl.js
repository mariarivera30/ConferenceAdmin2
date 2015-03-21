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
        vm.participantAuth;
        vm.companionAuth;
        vm.overnightAuth;
        vm.photoAuth;
        vm.authFirstName;
        vm.authLastName;
        vm.isRegistered;
        vm.registrationStatus;
        vm.currentid;

        vm.guestList = {};
        vm.acceptanceStatusList = ['Accepted', 'Rejected', ''];

        //Functions
        vm.getGuestList = _getGuestList;
        vm.updateAcceptanceStatus = _updateAcceptanceStatus;
        vm.displayGuest = _displayGuest;
        vm.displayAuthorizations = _displayAuthorizations;
        vm.rejectRegisteredGuest = _rejectRegisteredGuest;
        vm.rejectedSelectedGuest = _rejectedSelectedGuest;

        _getGuestList();

        //Functions:
        function activate() {

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
                       vm.companionAuth = data.companionAuth;
                       vm.overnightAuth = data.overnightAuth;
                       vm.photoAuth = data.photoAuth;
                       vm.participantAuth = data.participantAuth;
                       vm.authFirstName = firstName;
                       vm.authLastName = lastName
                   }).
                   error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       vm.companionAuth = data.companionAuth;
                       vm.overnightAuth = data.overnightAuth;
                       vm.photoAuth = data.photoAuth;
                       vm.participantAuth = data.participantAuth;
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
                            }
                        });


                    })


                .error(function (error) {

                });
            }
        }
    }
})();