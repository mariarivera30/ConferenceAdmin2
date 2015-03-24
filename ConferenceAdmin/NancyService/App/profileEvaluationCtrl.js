(function () {
    'use strict';

    var controllerId = 'profileEvaluationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', profileEvaluationCtrl]);

    function profileEvaluationCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        vm.title = 'profileEvaluationCtrl';
        vm.evaluate = false;
        vm.submissionID;
        vm.evaluatorID;
        vm.submissionTitle;
        vm.userType;
        vm.topic;
        vm.submitterFirstName;
        vm.submitterLastName;
        vm.submissionAbstract;
        vm.submissionFileList = {};
        vm.submissionType;
        vm.evaluationTemplate;
        vm.panelistNames;
        vm.plan;
        vm.guideQuestions;
        vm.format;
        vm.equipment;
        vm.duration;
        vm.delivery;
        vm.evaluatiorSubmissionID;
        vm.evaluationFile;
        vm.evaluationScore;
        vm.privateFeedback;
        vm.publicFeedback;
        vm.isEvaluated;
        vm.subIsEvaluated;
        var currentUserID = 4;


        vm.modalsubmissionID;
        vm.modaluserType;
        vm.modalevaluatorID;
        vm.modalsubmissionTitle;
        vm.modaltopic;
        vm.modalsubmitterFirstName;
        vm.modalsubmitterLastName;
        vm.modalsubmissionAbstract;
        vm.modalsubmissionFileList;
        vm.modalsubmissionType;
        vm.modalevaluationTemplate;
        vm.modalpanelistNames;
        vm.modalplan;
        vm.modalguideQuestions;
        vm.modalformat;
        vm.modalequipment;
        vm.modalduration;
        vm.modaldelivery;
        vm.modalevaluatiorSubmissionID;
        vm.modalevaluationFile;
        vm.modalevaluationScore;
        vm.modalpublicFeedback;
        vm.modalprivateFeedback;
        
        vm.submissionlist = {};

        //Functions
        vm.getAssignedSubmissions = _getAssignedSubmissions;
        vm.showEvaluationScreen = _showEvaluationScreen;
        vm.getDocumentSubmitted = _getDocumentSubmitted;
        vm.hideEvaluationForm = _hideEvaluationForm;
        vm.addEvaluation = _addEvaluation;
        vm.getAbstract = _getAbstract;
        

        _getAssignedSubmissions(currentUserID);

        //Functions implemented:
        function activate() {

        }

        function _hideEvaluationForm() {
            vm.evaluate = false;
        }

        function _getAbstract() {
            vm.modalAbstract = vm.modalsubmissionAbstract;
        }

        function _getDocumentSubmitted(document, documentName) {
            vm.modalDocument = document;
            vm.modalDocumentName = documentName;
        }

        function _getAssignedSubmissions(currentUserID) {
            restApi.getAssignedSubmissions(currentUserID).
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

        function _showEvaluationScreen(submissionID) {
            vm.evaluate = true;
            restApi.getSubmission(submissionID).
                  success(function (data, status, headers, config) {
                      // this callback will be called asynchronously
                      // when the response is available                      
                      vm.modalsubmissionID = data.submissionID;
                      vm.modalevaluationsubmittedID = data.evaluationsubmittedID;
                              vm.modaluserType = data.userType;
                              vm.modalevaluatorID = data.evaluatorID;
                              vm.modalsubmissionTitle = data.submissionTitle;
                              vm.modaltopic = data.topic;
                              vm.modalsubmitterFirstName = data.submitterFirstName;
                              vm.modalsubmitterLastName = data.submitterLastName;
                              vm.modalsubmissionAbstract = data.submissionAbstract;
                              vm.modalsubmissionFileList = data.submissionFileList;
                              vm.modalsubmissionType = data.submissionType;
                              vm.modalevaluationTemplate = data.evaluationTemplate;
                              vm.modalpanelistNames = data.panelistNames;
                              vm.modalplan = data.plan;
                              vm.modalguideQuestions = data.guideQuestions;
                              vm.modalformat = data.format;
                              vm.modalequipment = data.equipment;
                              vm.modalduration = data.duration;
                              vm.modaldelivery = data.delivery;
                              vm.modalevaluatiorSubmissionID = data.evaluatiorSubmissionID;
                              vm.modalevaluationFile = data.evaluationFile;
                              vm.modalevaluationScore = data.evaluationScore;
                              vm.modalpublicFeedback = data.publicFeedback;
                              vm.modalprivateFeedback = data.privateFeedback;
                              vm.modalsubIsEvaluated = data.subIsEvaluated;                                                
                  }).
                  error(function (data, status, headers, config) {
                      // called asynchronously if an error occurs
                      // or server returns response with an error status.
                      vm.submissionlist = data;
                  });
        }


        function _addEvaluation() {
            var evaluation = { evaluationsubmittedID: vm.modalevaluationsubmittedID, evaluatiorSubmissionID: vm.modalevaluatiorSubmissionID, evaluationFile: "EvaluationFile", score: vm.modalevaluationScore, publicFeedback: vm.modalpublicFeedback, privateFeedback: vm.modalprivateFeedback }
            //if evaluating for the first time
            if (vm.modalsubIsEvaluated == false) {
                restApi.postEvaluation(evaluation)
                        .success(function (data, status, headers, config) {

                        })
                        .error(function (error) {

                        });
            }
            else { //if updating evaluation
                restApi.editEvaluation(evaluation)
                       .success(function (data, status, headers, config) {

                       })
                       .error(function (error) {
                       });
            }
        }
    }
})();




