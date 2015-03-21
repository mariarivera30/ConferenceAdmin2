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
        vm.topicsList = {};

        // Functions
        vm.activate = activate;
        vm.addTopic = _addTopic;
        vm.getTopics = _getTopics;
        vm.updateTopic = _updateTopic;
        vm.deleteTopic = _deleteTopic;
        vm.selectedTopicUpdate = _selectedTopicUpdate;
        vm.selectedTopicDelete = _selectedTopicDelete;

        _getTopics();

        // Functions
        function activate() {
        }

        function clear() {
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

        function _addTopic() {
            var topicname = vm.name;
            if (topicname != null && topicname != "") {
                restApi.postNewTopic(vm.name)
                    .success(function (data, status, headers, config) {
                        vm.topicsList.push(data);
                        clear();
                    })
                    .error(function (error) {

                    });
            }
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

        function _updateTopic() {
            if (vm.currentid != undefined && vm.editname != null && vm.editname != "") {
                var topic={topiccategoryID: vm.currentid, name: vm.editname}
                restApi.updateTopic(topic)
                .success(function (data, status, headers, config) {
                    vm.topicsList.forEach(function(topic, index){
                    if(topic.topiccategoryID == vm.currentid){
                        topic.name= vm.editname;
                    }   
                 });
                })
                .error(function (data, status, headers, config) {
                });
            }
            else {
                alert("You must provide a valid name.");
            }
            clear();
        }

        function _deleteTopic() {
            if (vm.currentid != undefined) {
                restApi.deleteTopic(vm.currentid)
                .success(function (data, status, headers, config) {
                    vm.topicsList.forEach(function (topic, index) {
                        if (topic.topiccategoryID == vm.currentid) {
                            vm.topicsList.splice(index, 1);
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