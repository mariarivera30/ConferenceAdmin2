(function () {
    'use strict';

    var controllerId = 'profileEvaluationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', '$window', profileEvaluationCtrl]);

    function profileEvaluationCtrl($scope, $http, restApi, $window) {
        var vm = this;

        //attributes
        vm.activate = activate;
        vm.title = 'profileEvaluationCtrl';
        vm.evaluate = false;
        vm.submissionID;
        vm.evaluatorID;
        vm.submissionTitle;
        vm.userType;
        vm.topic;
        vm.allowFinalVersion;
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
        vm.isFinalSubmission;
        vm.canAllowFinalVersion;
        vm.subIsEvaluated;
        vm.content;
        vm.currentUserID = $window.sessionStorage.getItem('userID');

        //modal attributes
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

        //table attributes
        vm.submissionlist = [];
        vm.sindex = 0;
        vm.smaxIndex = 0;
        vm.sfirstPage = true;

        //functions
        vm.getAssignedSubmissions = _getAssignedSubmissions;
        vm.searchAssignedSubmission = _searchAssignedSubmission;
        vm.nextSub = _nextSub;
        vm.previousSub = _previousSub;
        vm.getFirstSubPage = _getFirstSubPage;
        vm.getLastSubPage = _getLastSubPage;
        vm.showEvaluationScreen = _showEvaluationScreen;
        vm.getDocumentSubmitted = _getDocumentSubmitted;
        vm.hideEvaluationForm = _hideEvaluationForm;
        vm.addEvaluation = _addEvaluation;
        vm.getAbstract = _getAbstract;
        vm.downloadEvaluationTemplate = _downloadEvaluationTemplate;
        vm.downloadEvaluationFile = _downloadEvaluationFile;
        vm.openDocumentSubmitted = _openDocumentSubmitted;
        vm.resetDownloadLink = _resetDownloadLink;

        //function calls
        _getAssignedSubmissions(vm.sindex);

        //Functions implemented:
        function activate() {

        }

        /* [Randy] download the submitted file for this evaluation */
        function _downloadEvaluationFile() {
            var data = { submissionID: vm.modalsubmissionID, evaluatorID: vm.modalevaluatorID };
            //window.open(vm.modalevaluationFile);
            restApi.getEvaluationFile(data).
                success(function (data, status, headers, config) {
                    //window.open(data.evaluationFile);

                    $("#file-").attr("href", data.evaluationFile).attr("download", data.evaluationFileName);

                    //var file = new Blob([data.evaluationFile]);
                    //saveAs(file, data.evaluationFileName);
                }).
                error(function (data, status, headers, config) {
                    alert("An error ocurred while downloading the file.");
                });
        }

        /* [Randy] download the template for this evaluation */
        function _downloadEvaluationTemplate(id) {
            //window.open(vm.modalevaluationTemplate);
            restApi.getEvaluationTemplate(id).
                success(function (data, status, headers, config) {
                    //window.open(data.evaluationFile);

                    $("#file-" + id).attr("href", data.evaluationFile).attr("download", data.evaluationFileName);

                    //var file = new Blob([data.evaluationFile]);
                    //saveAs(file, data.evaluationFileName);
                }).
                error(function (data, status, headers, config) {
                    alert("An error ocurred while downloading the file.");
                });
        };

        function _hideEvaluationForm() {
            vm.evaluate = false;
            vm.myFile = null;
            //var input = $("#browseButton");
            //input.replaceWith(input = input.clone(true));
        }

        function _getAbstract() {
            vm.modalAbstract = vm.modalsubmissionAbstract;
        }
        /* [Jaimeiris] gets documents like abstract to show en modal only */
        function _getDocumentSubmitted(document, documentName) {
            vm.modalDocument = document;
            vm.modalDocumentName = documentName;            
        }
        /* [Jaimeiris] opens the documents in another screen */
        function _openDocumentSubmitted(id) {
            restApi.getSubmissionFile(id).
                success(function (data, status, headers, config) {
                    //window.open(data.document);

                    $("#file-" + id).attr("href", data.document).attr("download", data.documentName);

                    //var file = new Blob([data.document]);
                    //saveAs(file, data.documentName);
                }).
                error(function (data, status, headers, config) {
                    alert("An error ocurred while downloading the file.");
                });
        }

        /* [Randy] reset the link to default */
        function _resetDownloadLink(id) {
            $("#file-" + id).attr("href", "").removeAttr("download");
        }

        /* [Jaimeiris] get list of all submissions assigned to a specific evaluator */
        function _getAssignedSubmissions(index) {
            vm.uploadingComp = true;
            var data = { evaluatorUserID: vm.currentUserID, index: index }
            restApi.getAssignedSubmissions(data).
                   success(function (data, status, headers, config) {
                       if (data.results == null)
                           vm.empty = true;
                       vm.uploadingComp = false;
                       vm.smaxIndex = data.maxIndex;
                       if (vm.smaxIndex == 0) {
                           vm.sindex = 0;
                           vm.submissionlist = [];
                       }
                       else if (vm.sindex >= vm.smaxIndex) {
                           vm.sindex = vm.smaxIndex - 1;
                           _getAssignedSubmissions(vm.sindex);
                       }
                       else {
                           vm.submissionlist = data.results;
                       }
                   }).
                   error(function (data, status, headers, config) {
                       vm.uploadingComp = false;
                   });
        }

        /* [Jaimeiris] Pagination: get next page */
        function _nextSub() {
            if (vm.sindex < vm.smaxIndex - 1) {
                vm.sindex += 1;
                _getAssignedSubmissions(vm.sindex);
            }
        }

        /* [Jaimeiris] Pagination: get previious page */
        function _previousSub() {
            if (vm.sindex > 0) {
                vm.sindex -= 1;
                _getAssignedSubmissions(vm.sindex);
            }
        }

        /* [Jaimeiris] Pagination: get first page */
        function _getFirstSubPage() {
            vm.sindex = 0;
            _getAssignedSubmissions(vm.sindex);
        }

        /* [Jaimeiris] Pagination: get last page */
        function _getLastSubPage() {
            vm.sindex = vm.smaxIndex - 1;
            _getAssignedSubmissions(vm.sindex);
        }
        //----END PAGINATON CODE---

        /* [Jaimeiris] Prepare screen with the information of the evaluation */
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
                    vm.modalAllowFinalVersion = data.allowFinalVersion;
                    vm.modalIsFinalVersion = data.isFinalVersion;
                    vm.modalcanAllowFinalVersion = data.canAllowFinalVersion;
                    vm.modalTemplateID = data.evaluationTemplateID;
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

        /* [Randy] check file extension */
        $scope.showContent = function ($fileContent) {
            vm.content = $fileContent;
            vm.fileext = vm.myFile.name.split(".", 2)[1];
            if (vm.fileext == "pdf" || vm.fileext == "doc" || vm.fileext == "docx" || vm.fileext == "ppt")
                vm.ext = false;
            else {
                document.getElementById("browseButton").value = "";
                vm.ext = true;
                $scope.content = "";
                $fileContent = "";
                vm.myFile = undefined;
            }
        };

        /* [Jaimeiris] add new evaluation to a submission */
        function _addEvaluation() {            
            var evaluation = {
                evaluationsubmittedID: vm.modalevaluationsubmittedID, evaluatiorSubmissionID: vm.modalevaluatiorSubmissionID,
                score: vm.modalevaluationScore, publicFeedback: vm.modalpublicFeedback, privateFeedback: vm.modalprivateFeedback,
                allowFinalVersion: vm.modalAllowFinalVersion, initialSubmissionID: vm.modalsubmissionID
            }
            if (vm.myFile != undefined) {
                evaluation.evaluationFile =  vm.content;
                evaluation.evaluationName = vm.myFile.name;
                vm.myFile = "";
                vm.content = "";
            }
            //if evaluating for the first time
            if (vm.modalsubIsEvaluated == false) {
                if (vm.myFile == undefined) {
                    alert("A file must be uploaded before submitting an evaluation.");
                }
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
                                    submission.allowFinalVersion = vm.modalAllowFinalVersion;
                                }
                            })
                            if (vm.myFile != undefined) {
                                vm.modalevaluationFile = vm.content;
                                vm.modalevaluationName = vm.myFile.name;
                            }
                            $("#addConfirm").modal('show');
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
                                   if (vm.myFile != undefined) {
                                       submission.evaluationFile = vm.content;
                                       submission.evaluationName = vm.myFile.name;
                                   }
                               }
                               if (vm.myFile != undefined) {
                                   vm.modalevaluationFile = vm.content;
                                   vm.modalevaluationName = vm.myFile.name;
                               }
                           }                           
                       )
                           $("#addConfirm").modal('show');
                       })
                       .error(function (error) {
                       });
            }
        }

        /* [Randy] search for a submission using a certain criteria */
        function _searchAssignedSubmission(index) {
            var data = { evaluatorUserID: vm.currentUserID, index: index, criteria: vm.criteria }
            restApi.searchAssignedSubmission(data).
                   success(function (data, status, headers, config) {
                       vm.smaxIndex = data.maxIndex;
                       if (vm.smaxIndex == 0) {
                           vm.sindex = 0;
                           vm.submissionlist = [];
                       }
                       else if (vm.sindex >= vm.smaxIndex) {
                           vm.sindex = vm.smaxIndex - 1;
                           _searchAssignedSubmission(vm.sindex);
                       }
                       else {
                           vm.submissionlist = data.results;
                       }
                   }).
                   error(function (data, status, headers, config) {
                   });
        }

    }
})();




