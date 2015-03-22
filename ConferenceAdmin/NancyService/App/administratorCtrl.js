(function () {
    'use strict';

    var controllerId = 'administratorCtrl';
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', administratorCtrl]);

    function administratorCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        vm.title = 'administratorCtrl';
        vm.firstName;
        vm.lastName;
        vm.adminList = {};
        vm.privilegeList = {};
        vm.privilegeID;
        vm.privilegeDelID;
        vm.userID;
        vm.email;
        vm.privilegeName;

        // Functions
        vm.clear = _clear;
        vm.addAdmin = _addAdmin;
        vm.getAdmin = _getAdmin;
        vm.editAdmin = _editAdmin;
        vm.deleteAdmin = _deleteAdmin;
        vm.selectedAdminEdit = _selectedAdminEdit;
        vm.selectedAdminDelete = _selectedAdminDelete;
        _getAdmin();
        _getPrivilegeList();

        function activate() {

        }

        function _clear() {
            vm.email = "";
            vm.firstName = "";
            vm.lastName = "";
            $('#privileges input').removeAttr('checked');
            $("#privileges").buttonset('refresh');
        }

        function _selectedAdminEdit(id, privilegeName) {
            vm.userID = id;
            document.getElementById(privilegeName).checked = true;
        }

        function _selectedAdminDelete(id, privilegeID) {
            vm.userID = id;
            vm.privilegeDelID = privilegeID;
        }

        function _getPrivilegeList() {
            restApi.getPrivilegesList()
            .success(function (data, status, headers, config) {
                vm.privilegeList = data;
            })
           .error(function (data, status, headers, config) {
          });
        }

        function _addAdmin() {
            if (vm.email != undefined && vm.email != "" && vm.privilegeID != undefined) {
                restApi.postNewAdmin(vm)
                    .success(function (data, status, headers, config) {
                        if (data.email != null) {
                            vm.adminList.push(data);
                            _clear();
                        }
                    })

                    .error(function (error) {

                    });
            }
        }

        function _getAdmin() {
            restApi.getAdministrators()
            .success(function (data, status, headers, config) {
                vm.adminList = data;
                load();
            })
           .error(function (data, status, headers, config) {
               vm.adminList = data;
           });
        }

        function _editAdmin() {
            if (vm.userID != undefined && vm.privilegeID != undefined) {
                restApi.editAdmin(vm.userID, vm.privilegeID)
                .success(function (data, status, headers, config) {
                    vm.adminList.forEach(function (admin, index) {
                        if (admin.userID == vm.userID) {
                            admin.privilege = data;
                        }
                    });
                })
                .error(function (data, status, headers, config) {
                });
            }
        }

        function _deleteAdmin() {
            if (vm.userID != undefined && vm.privilegeDelID != undefined) {
                restApi.deleteAdmin(vm.userID, vm.privilegeDelID)
                .success(function (data, status, headers, config) {
                    vm.adminList.forEach(function (admin, index) {
                        if (admin.userID == vm.userID) {
                            vm.adminList.splice(index, 1);
                        }
                    });
                })
                .error(function (data, status, headers, config) {
                });
            }
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("loading-icon").style.visibility = "hidden";
            var body = document.getElementById("body");
            body.style.visibility = "visible";
        };
    }
})();