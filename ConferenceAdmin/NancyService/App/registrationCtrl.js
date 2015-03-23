(function () {
    'use strict';

    var controllerId = 'registrationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', registrationCtrl]);

    function registrationCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        //add registration fields        
        vm.title = 'registrationCtrl';
        vm.userID;
        vm.paymentID;
        vm.date1;
        vm.date2;
        vm.date3;
        vm.checkAll;

        vm.registrationsList = {};
        vm.userTypesList = {};
        vm.firstname;
        vm.lastname;
        vm.usertypeid;
        vm.affiliationName;
        vm.registrationstatus;
        vm.hasapplied;
        vm.acceptancestatus;
        vm.byAdmin;

        vm.currentid;
        vm.editfirstname;
        vm.editlastname;
        vm.editusertypeid;
        vm.editaffiliationName;

        // Functions
        vm.activate = activate;
        vm.addRegistration = _addRegistration;
        vm.getRegistrations = _getRegistrations;
        vm.updateRegistration = _updateRegistration;
        vm.deleteRegistration = _deleteRegistration;
        vm.selectedRegistrationUpdate = _selectedRegistrationUpdate;
        vm.selectedRegistrationDelete = _selectedRegistrationDelete;
        vm.getUserTypes = _getUserTypes;
        vm.clear = clear;


        _getRegistrations();
        _getUserTypes();



        // Functions
        function activate() {
            firstname = vm.firstname;
            lastname = vm.lastname;
            usertypeid = vm.usertypeid;
            affiliationName = vm.affiliationName;
            registrationstatus = vm.registrationstatus;
            hasapplied = vm.hasapplied;
            acceptancestatus = vm.acceptancestatus;
            date1 = vm.date1;
            date2 = vm.date2;
            date3 = vm.date3;
        }

        function clear() {
            vm.firstname = "";
            vm.lastname = "";
            vm.usertypeid = 0;
            vm.affiliationName = "";
            vm.title = "";
            vm.registrationstatus = "";
            vm.hasapplied = false;
            vm.acceptancestatus = "";
            vm.date1 = false;
            vm.date2 = false;
            vm.date3 = false;
            vm.checkAll = false;
            vm.currentid = 0;
        }

        function _addRegistration() {
            var userTypeName = vm.usertypeid.userTypeName;
            vm.usertypeid = vm.usertypeid.userTypeID;
            if (vm.firstname != null && vm.lastname != null && vm.usertypeid != 0 && vm.affiliationName != null && !(vm.date1 == false && vm.date2 == false)) {
                restApi.postNewRegistration(vm)
                    .success(function (data, status, headers, config) {
                        vm.usertypeid = userTypeName;
                        vm.registrationsList.push(vm);
                    })

                    .error(function (error) {
                        alert("Failed to add Registration.");
                    });
            }
            else {
                alert("There is information missing.");
            }
            activate();
        }


        function _getRegistrations() {
            restApi.getRegistrations().
                   success(function (data, status, headers, config) {
                       vm.registrationsList = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.registrationsList = data;
                   });
        }



        function _selectedRegistrationUpdate(id, firstname, lastname, usertypeid, affiliationName, date1, date2, date3) {
            vm.currentid = id;
            vm.editfirstname = firstname;
            vm.editlastname = lastname;

            for (var i = 1; i < vm.userTypesList.length; i++) {
                if (vm.userTypesList[i].userTypeName == usertypeid) {
                    vm.editusertypeid = i;
                }
            }

            vm.editaffiliationName = affiliationName;
            vm.editdate1 = date1;
            vm.editdate2 = date2;
            vm.editdate3 = date3;
        }

        function _updateRegistration() {
            if (vm.currentid != undefined && vm.currentid != 0) {
                var registration = { registrationID: vm.currentid, firstname: vm.editfirstname, lastname: vm.editlastname, usertypeid: vm.editusertypeid.userTypeID, affiliationName: vm.editaffiliationName, date1: vm.editdate1, date2: vm.editdate2, date3: vm.editdate3 }
                restApi.updateRegistration(registration)
                .success(function (data, status, headers, config) {
                    vm.registrationsList.forEach(function (registration, index) {
                        if (registration.registrationID == vm.currentid) {
                            vm.firstname = vm.editfirstname;
                            vm.lastname = vm.editlastname;
                            vm.usertypeid = vm.editusertypeid.userTypeName;
                            vm.affiliationName = vm.editaffiliationName;
                            vm.date1 = vm.editdate1;
                            vm.date2 = vm.editdate2;
                            vm.date3 = vm.editdate3;
                        }
                        else
                            location.reload();
                    });
                })
                .error(function (data, status, headers, config) {
                    alert("Failed to edit Registration.");
                    location.reload();
                });
            }
            else {
                alert("There is information missing.");
            }
            clear();
        }



        function _selectedRegistrationDelete(id) {
            vm.currentid = id;
        }

        function _deleteRegistration() {
            if (vm.currentid != undefined) {
                restApi.deleteRegistration(vm.currentid)
                .success(function (data, status, headers, config) {
                    vm.registrationsList.forEach(function (registration, index) {
                        if (registration.registrationID == vm.currentid) {
                            vm.registrationsList.splice(index, 1);
                        }
                    });
                })
                .error(function (data, status, headers, config) {
                });
            }
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

    }
})();
