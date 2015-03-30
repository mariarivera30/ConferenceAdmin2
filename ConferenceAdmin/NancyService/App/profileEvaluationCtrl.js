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
        vm.content;
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
        vm.modalhasFile;
        
        vm.submissionlist = {};

        //Functions
        vm.getAssignedSubmissions = _getAssignedSubmissions;
        vm.showEvaluationScreen = _showEvaluationScreen;
        vm.getDocumentSubmitted = _getDocumentSubmitted;
        vm.hideEvaluationForm = _hideEvaluationForm;
        vm.addEvaluation = _addEvaluation;
        vm.getAbstract = _getAbstract;
        vm.downloadEvaluationTemplate = _downloadEvaluationTemplate;
        vm.downloadEvaluationFile = _downloadEvaluationFile;        

        _getAssignedSubmissions(currentUserID);

        //Functions implemented:
        function activate() {

        }

       function _downloadEvaluationFile() {
           window.open(vm.modalevaluationFile);
       }
       function _downloadEvaluationTemplate() {
           window.open(vm.modalevaluationTemplate);
       };

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

        function _showEvaluationScreen(submissionID, evaluatorID) {
            vm.evaluate = true;
            var myData = { submissionID: submissionID, evaluatorID: evaluatorID }
            restApi.getSubmissionDetails(myData).
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
                              vm.modalevaluationName = data.evaluationName;
                              vm.modalevaluationFile = data.evaluationFile;
                              vm.modalevaluationScore = data.evaluationScore;
                              vm.modalpublicFeedback = data.publicFeedback;
                              vm.modalprivateFeedback = data.privateFeedback;
                              vm.modalsubIsEvaluated = data.subIsEvaluated;
                              if (vm.modalevaluationFile == undefined || vm.modalevaluationFile == null) {
                                  vm.modalhasFile = false;
                              }
                              else {
                                  vm.modalhasFile = true;
                              }
                  }).
                  error(function (data, status, headers, config) {
                      // called asynchronously if an error occurs
                      // or server returns response with an error status.
                      vm.submissionlist = data;
                  });
        }

        //para preview la imagen
        $scope.showContent = function ($fileContent) {
            vm.content = $fileContent;
        };

        function _addEvaluation(myFile) {            
            var evaluation = { evaluationsubmittedID: vm.modalevaluationsubmittedID, evaluatiorSubmissionID: vm.modalevaluatiorSubmissionID, score: vm.modalevaluationScore, publicFeedback: vm.modalpublicFeedback, privateFeedback: vm.modalprivateFeedback }
            if (myFile != undefined) {
                evaluation.evaluationFile =  vm.content;
                evaluation.evaluationName = myFile.name;
            }
            else {
                alert("An file must be uploaded before submitting an evaluation.");
            }
            //if evaluating for the first time
            if (vm.modalsubIsEvaluated == false) {
                restApi.postEvaluation(evaluation)
                        .success(function (data, status, headers, config) {
                           vm.submissionlist.forEach(function (submission, index) {
                                if (submission.submissionID == vm.modalsubmissionID) {
                                    submission.isEvaluated = true;
                                    submission.publicFeedback = vm.modalpublicFeedback;
                                    submission.privateFeedback = vm.modalprivateFeedback;
                                    submission.score = vm.modalevaluationScore;
                                    submission.evaluationsubmittedID = vm.modalevaluationsubmittedID;
                                    submission.evaluationFile = vm.modalevaluationFile;
                                    submission.evaluationName = vm.modalevaluationName;
                                }
                            }
                            )
                        })
                        .error(function (error) {

                        });
            }
            else { //if updating evaluation
                restApi.editEvaluation(evaluation)
                       .success(function (data, status, headers, config) {
                           vm.submissionlist.forEach(function (submission, index) {
                               if (submission.submissionID == vm.modalsubmissionID) {
                                   submission.isEvaluated = true;
                                   submission.publicFeedback = vm.modalpublicFeedback;
                                   submission.privateFeedback = vm.modalprivateFeedback;
                                   submission.score = vm.modalevaluationScore;
                                   submission.evaluationsubmittedID = vm.modalevaluationsubmittedID;
                                   if (myFile != undefined) {
                                       submission.evaluationFile = vm.content;
                                       submission.evaluationName = myFile.name;
                                   }
                               }
                               if (myFile != undefined) {
                                   vm.modalevaluationFile = vm.content;
                                   vm.modalevaluationName = myFile.name;
                               }
                           }
                       )})
                       .error(function (error) {
                       });
            }
        }
    }
})();




