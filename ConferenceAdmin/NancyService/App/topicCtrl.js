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

        _getTopics();

        // Functions
        function activate() {
        }

        function _clear() {
            vm.name = "";
        }

        function _selectedTopicUpdate(id, name) {
            vm.currentid = id;
            //Update input field
            vm.editname = name;
        }

        function _selectedTopicDelete(id) {
            vm.currentid = id;
        }

        function _getTopics() {
            restApi.getTopics()
            .success(function (data, status, headers, config) {
                vm.topicsList = data;
                load();
            })
           .error(function (data, status, headers, config) {
               vm.topicsList = data;
           });
        }

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
                        $("#addError").modal('show');
                    });
            }
        }

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
                    $("#editError").modal('show');
                });
            }
        }

        function _deleteTopic() {
            if (vm.currentid != undefined && vm.currentid != "") {
                restApi.deleteTopic(vm.currentid)
                .success(function (data, status, headers, config) {
                    if (data) {
                        vm.topicsList.forEach(function (topic, index) {
                            if (topic.topiccategoryID == vm.currentid) {
                                vm.topicsList.splice(index, 1);
                            }
                        });
                        $("#deleteConfirm").modal('show');
                    }
                    else {
                        $("#deleteError").modal('show');
                    }
                })
                .error(function (data, status, headers, config) {
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