(function () {
    'use strict';

    var controllerId = 'committeeCtrl2';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', committeeCtrl2]);

    function committeeCtrl2($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        vm.title = 'committeeCtrl2';

        vm.planningCommitteeList = {};
        vm.committeeID;
        vm.position;
        vm.name;
        vm.lname;
        vm.affiliation;
        vm.loading = false;

        //For error modal:
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

        //Functions
        vm.clear = _clear;
        vm.getCommittee = _getCommittee;
        vm.addMember = _addMember;
        vm.editMember = _editMember;
        vm.deleteMember = _deleteMember;
        vm.selectedMemberEdit = _selectedMemberEdit;
        vm.selectedMemberDelete = _selectedMemberDelete;

        _getCommittee();

        function activate() {

        }

        function _clear() {
            vm.name = "";
            vm.lname = "";
            vm.position = "";
            vm.affiliation = "";
            var x = document.getElementsByName("position");
            var i;
            for (i = 0; i < x.length; i++) {
                if (x[i].type == "radio") {
                    x[i].checked = false;
                }
            }
        }
        function _selectedMemberEdit(id, position) {
            vm.committeeID = id;
            var x = document.getElementById(position);
            if (x.type == "radio") {
                x.checked = true;
            }

            //.blur();
        }

        function _selectedMemberDelete(id, position) {
            vm.committeeID = id;
            vm.position = position;
        }

        function _getCommittee() {
            restApi.getPlanningCommittee()
           .success(function (data, status, headers, config) {
               if (data != null && data != "") {
                   vm.planningCommitteeList = data;
               }
               load();
           })
          .error(function (data, status, headers, config) {
              load();
              vm.toggleModal('error');
          });
        }

        function _addMember() {
            vm.loading = true;
            if (vm.name != undefined && vm.name != "" && vm.lname != undefined && vm.name != "" && vm.position != undefined) {
                var committee = {
                    firstName: vm.name,
                    lastName: vm.lname,
                    affiliation: vm.affiliation,
                    description: vm.position,
                }

                restApi.postNewCommittee(committee)
                    .success(function (data, status, headers, config) {
                        if (data.firstName != null && data.firstName != "") {
                            vm.planningCommitteeList.push(data);
                            _clear();
                            $("#addConfirm").modal('show');
                        }
                        else {
                            $("#addError").modal('show');
                            _clear();
                        }
                        vm.loading = false;
                        $("#addMember").modal('hide');
                    })

                    .error(function (error) {
                        vm.loading = false;
                        $("#addMember").modal('hide');
                        vm.toggleModal('error');
                    });
            }
        }

        function _editMember() {
            vm.loading = true;
            if (vm.committeeID != undefined && vm.committeeID != "" && vm.position != undefined && vm.position != "") {
                var info = {
                    committeeID: vm.committeeID,
                    description: vm.position
                }
                restApi.editCommittee(info)
                .success(function (data, status, headers, config) {
                    if (data != null && data != "") {
                        vm.planningCommitteeList.forEach(function (s, index) {
                            if (s.committeeID == vm.committeeID) {
                                s.description = vm.position;
                            }
                        });
                    }
                    vm.loading = false;
                    $("#editPosition").modal('hide');
                    $("#editConfirm").modal('show');
                })
                .error(function (data, status, headers, config) {
                    vm.loading = false;
                    $("#editPosition").modal('hide');
                    vm.toggleModal('error');
                });
            }
        }

        function _deleteMember() {
            if (vm.committeeID != undefined && vm.committeeID != "" && vm.position != undefined && vm.position != "") {
                var info = {
                    committeeID: vm.committeeID,
                    description: vm.position
                }
                restApi.deleteCommittee(info)
                .success(function (data, status, headers, config) {
                    if (data) {
                        vm.planningCommitteeList.forEach(function (s, index) {
                            if (s.committeeID == vm.committeeID) {
                                vm.planningCommitteeList.splice(index, 1);
                            }
                        });
                        $("#deleteConfirm").modal('show');
                    }
                })
                .error(function (data, status, headers, config) {
                    vm.toggleModal('error');
                });
            }
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("loading-icon").style.visibility = "hidden";
            document.getElementById("body").style.visibility = "visible";
        };
    }
})();