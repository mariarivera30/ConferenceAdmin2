(function () {
    'use strict';

    var controllerId = 'profileSubmissionCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', profileSubmissionCtrl]);

    function profileSubmissionCtrl($scope, $http, restApi) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'profileSubmissionCtrl';
        var currentUserID = 1;
        vm.submissionID;
        vm.submissionType;
        vm.submissionTypeID;
        vm.submissionTypeName;
        vm.submissionTitle;
        vm.status;
        vm.isEvaluated;
        vm.view = "Add";

        vm.submissionlist = {};
        vm.submissionTypeList = {}; //= ['Extended Paper', 'Poster', 'Pannel', 'BoF', 'Workshop'];

        //Functions
        vm.getUserSubmissions = _getUserSubmissions;
        vm.viewEditForm = _viewEditForm;
        vm.viewTheView = _viewTheView;
        vm.backToList = _backToList;
        vm.deleteSelectedSubmission = _deleteSelectedSubmission;
        vm.deleteSubmission = _deleteSubmission;
        vm.downloadPDFFile = _downloadPDFFile;

        _getUserSubmissions(currentUserID);
        _getSubmissionTypes();

        //Functions:

        function activate() {

        }

        function _viewEditForm(submissionTypeName) {
            vm.view = "Edit";
            vm.itemSubmissionType = submissionTypeName;
        }

        function _viewTheView(submissionTypeName, submissionID) {
            vm.view = "View";
            vm.itemSubmissionType = submissionTypeName;
            restApi.getUserSubmission(submissionID).
                  success(function (data, status, headers, config) {
                      vm.modalsubmissionID = data.submissionID;
                      vm.modaluserType = data.userType;
                      vm.modalsubmissionTitle = data.submissionTitle;
                      vm.modaltopic = data.topic;
                      vm.modalsubmissionAbstract = data.submissionAbstract;
                      vm.modalsubmissionFileList = data.submissionFileList;
                      vm.modalsubmissionTypeName = data.submissionTypeName;
                      vm.modalsubmissionTypeID = data.submissionTypeID;
                      vm.modalpanelistNames = data.panelistNames;
                      vm.modalplan = data.plan;
                      vm.modalguideQuestions = data.guideQuestions;
                      vm.modalformat = data.format;
                      vm.modalequipment = data.equipment;
                      vm.modalduration = data.duration;
                      vm.modaldelivery = data.delivery;
                      vm.modalsubIsEvaluated = data.subIsEvaluated;
                      vm.modalpublicFeedback = data.publicFeedback;
                  }).
                  error(function (data, status, headers, config) {
                      // called asynchronously if an error occurs
                      // or server returns response with an error status.
                      vm.submissionlist = data;
                  });
        }
        function _backToList() {
            vm.view = "Add";
        }

        function getSubmission() {

        }

        function _deleteSelectedSubmission(submissionID) {
            vm.currentSubmissionID = submissionID;
        }
        function _deleteSubmission() {
            if (vm.currentSubmissionID != undefined) {
                restApi.deleteSubmission(vm.currentSubmissionID)
                .success(function (data, status, headers, config) {
                    vm.submissionlist.forEach(function (submission, index) {
                        if (submission.submissionID == vm.currentSubmissionID) {
                            vm.submissionlist.splice(index, 1);
                        }
                    });
                })
                .error(function (data, status, headers, config) {
                });
            }
        }

        function _getUserSubmissions(currentUserID) {
            restApi.getUserSubmissionList(currentUserID).
                   success(function (data, status, headers, config) {
                       // this callback will be called asynchronously
                       // when the response is available

                       vm.submissionlist = data;
                   }).
                   error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       vm.submissionlist = data;
                   });
        }

        function _getSubmissionTypes() {
            restApi.getSubmissionTypes().
                   success(function (data, status, headers, config) {
                       vm.submissionTypeList = data;
                       //vm.submissionTypeID = 1
                       //vm.submissionTypeName = 'Extended Paper';
                       vm.submissionType = vm.submissionTypeList[0];
                   }).
                   error(function (data, status, headers, config) {
                       vm.submissionTypeList = data;
                   });
        }

        function _downloadPDFFile(document) {
            window.open(document);
        }

    }
})();
