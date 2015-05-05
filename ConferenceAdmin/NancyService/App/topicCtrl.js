(function () {
    'use strict';

    var controllerId = 'topicCtrl';
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', topicCtrl]);

    function topicCtrl($scope, $http, restApi) {
        var vm = this;

        //add topic fields
        vm.title = 'topicCtrl';
        vm.name;
        vm.editname;
        vm.currentid;
        vm.topicsList = [];
        vm.search;
        vm.loading = false;

        // Functions
        vm.activate = activate;
        vm.clear = _clear;
        vm.getTopics = _getTopics;
        vm.addTopic = _addTopic;
        vm.updateTopic = _updateTopic;
        vm.deleteTopic = _deleteTopic;
        vm.selectedTopicUpdate = _selectedTopicUpdate;
        vm.selectedTopicDelete = _selectedTopicDelete;

        //For error modal
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

        _getTopics();

        // Functions
        function activate() {
        }

        //Clear form
        function _clear() {
            vm.name = "";
            $scope.addTopicForm.$setPristine();
        }

        //Topic selected for update
        function _selectedTopicUpdate(id, name) {
            vm.currentid = id;
            //Update input field
            vm.editname = name;
        }

        //Topic selected for deletion
        function _selectedTopicDelete(id) {
            vm.currentid = id;
        }

        //get list of topics
        function _getTopics() {
            restApi.getTopics()
            .success(function (data, status, headers, config) {
                vm.topicsList = data;
                load();
            })
           .error(function (data, status, headers, config) {
               load();
               vm.toggleModal('error');
           });
        }

        //add a new topic
        function _addTopic() {
            vm.loading = true;
            var topicname = vm.name;
            if (topicname != null && topicname != "") {
                restApi.postNewTopic(vm.name)
                    .success(function (data, status, headers, config) {
                        if (data != null && data != "") {
                            vm.topicsList.push(data);
                            _clear();
                            vm.loading = false;
                            $("#addTopic").modal('hide');
                            $("#addConfirm").modal('show');
                        }
                    })
                    .error(function (error) {
                        vm.loading = false;
                        $("#addTopic").modal('hide');
                        vm.toggleModal('error');
                    });
            }
        }

        //change topic name
        function _updateTopic() {
            vm.loading = true;
            if (vm.currentid != undefined && vm.currentid != "" && vm.editname != null && vm.editname != "") {
                var topic = { topiccategoryID: vm.currentid, name: vm.editname }
                restApi.updateTopic(topic)
                .success(function (data, status, headers, config) {
                    if (data) {
                        vm.topicsList.forEach(function (topic, index) {
                            if (topic.topiccategoryID == vm.currentid) {
                                topic.name = vm.editname;
                            }
                        });
                        _clear();
                        vm.loading = false;
                        $("#editTopic").modal('hide');
                        $("#editConfirm").modal('show');
                    }
                })
                .error(function (data, status, headers, config) {
                    vm.loading = false;
                    $("#editTopic").modal('hide');
                    vm.toggleModal('error');
                });
            }
        }

        //delete topic
        function _deleteTopic() {
            vm.loading = true;
            if (vm.currentid != undefined && vm.currentid != "") {
                restApi.deleteTopic(vm.currentid)
                .success(function (data, status, headers, config) {
                    if (data) {
                        vm.topicsList.forEach(function (topic, index) {
                            if (topic.topiccategoryID == vm.currentid) {
                                vm.topicsList.splice(index, 1);
                            }
                        });
                        vm.loading = false;
                        $("#deleteTopic").modal('hide');
                        $("#deleteConfirm").modal('show');
                    }
                    else {
                        vm.loading = false;
                        $("#deleteTopic").modal('hide');
                        $("#deleteError").modal('show');
                    }
                })
                .error(function (data, status, headers, config) {
                    vm.loading = false;
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