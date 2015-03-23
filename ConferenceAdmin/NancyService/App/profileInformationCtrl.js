(function () {
    'use strict';

    var controllerId = 'profileInformationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', profileInformationCtrl]);

    function profileInformationCtrl($scope, $http, restApi) {
        var vm = this;
        vm.edit = false;
        vm.activate = activate;

        vm.userID = 1;

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
        // Application Attributes
        vm.acceptanceStatus;
        vm.registrationStatus;
        vm.hasApplied;

        // Function Definitions
        vm.toggleEdit = toggleEdit;
        vm.updateProfileInfo = _updateProfileInfo;
        vm.getProfileInfo = _getProfileInfo;
        vm.apply = _apply;

        _getProfileInfo(vm.userID);

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

        function _apply() {
            restApi.apply(vm).
                    success(function (data, status, headers, config) {
                        vm.hasApplied = true;
                    }).
                    error(function (data, status, headers, config) {
                        alert("An error occurred");
                    });
        }

    }
})();
