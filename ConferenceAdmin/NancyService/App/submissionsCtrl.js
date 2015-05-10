(function () {
    'use strict';

    var controllerId = 'submissionsCtrl';

    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', '$window', submissionsCtrl]);

    function submissionsCtrl($scope, $http, restApi, $window) {
        var vm = this;
        var userID = $window.sessionStorage.getItem('userID');
        vm.submissionsList = [];
        vm.acceptanceStatusList = ['Pending', 'Accepted', 'Rejected'];
        vm.evaluationsList = [];
        vm.prevEvaluationsList = [];
        vm.evaluatorsList = [];
        vm.submissionTypeList = [];
        vm.topicsList = [];
        vm.documentsList = [];
        vm.templatesList = [];
        vm.usersList = [];
        vm.deletedSubmissionsList = [];
        // custom Submission class fields
        vm.submissionID;
        vm.evaluatorID;
        vm.topic;
        vm.userType;
        vm.submissionTypeID;
        vm.submissionTypeName;
        vm.submissionTitle;
        vm.topiccategoryID;
        vm.status;
        vm.isEvaluated;
        vm.isFinalSubmission;
        vm.finalSubmissionAllowed;
        vm.acceptanceStatus;
        vm.avgScore;
        // submission fields
        vm.submissionID;
        vm.topicID;
        vm.topiccategoryID;
        vm.submissionTypeID;
        vm.submissionAbstract;
        vm.title;
        vm.status;
        vm.byAdmin;
        // evaluation-submission fields
        vm.evaluatiorSubmissionID;
        vm.evaluatorID;
        vm.statusEvaluation;
        // evaluation fields
        vm.evaluationsubmittedID;
        vm.evaluationName;
        vm.evaluationFile;
        vm.score;
        vm.avgScore;
        vm.publicFeedback;
        vm.privateFeedback;
        //pagination
        vm.sindex = 0;
        vm.smaxIndex = 0;
        vm.sfirstPage = true;
        //pagination for deleted
        vm.dindex = 0;
        vm.dmaxIndex = 0;
        vm.dfirstPage = true;

        //functions
        vm.clear = _clear;
        vm.getAllSubmissions = _getAllSubmissions;
        vm.nextSubmission = _nextSubmission;
        vm.previousSubmission = _previousSubmission;
        vm.getFirstSubmissionPage = _getFirstSubmissionPage;
        vm.getLastSubmissionPage = _getLastSubmissionPage;
        vm.downloadPDFFile = _downloadPDFFile;
        vm.downloadEvaluationFile = _downloadEvaluationFile;
        vm.getSubmissionView = _getSubmissionView;
        vm.getEvaluationsForSubmission = _getEvaluationsForSubmission;
        vm.getEvaluationDetails = _getEvaluationDetails;
        vm.getAllEvaluators = _getAllEvaluators;
        vm.assignEvaluator = _assignEvaluator;
        vm.removeEvaluator = _removeEvaluator;
        vm.getSubmissionTypes = _getSubmissionTypes;
        vm.getTopics = _getTopics;
        vm.addAdminSubmission = _addAdminSubmission;
        vm.addDocument = _addDocument;
        vm.deleteDocument = _deleteDocument;
        vm.getTemplates = _getTemplates;
        vm.assignTemplate = _assignTemplate;
        vm.changeSubmissionStatus = _changeSubmissionStatus;
        vm.selectUser = _selectUser;
        vm.getListOfUsers = _getListOfUsers;
        vm.getDeletedSubmissions = _getDeletedSubmissions;
        vm.nextDeletedSubmission = _nextDeletedSubmission;
        vm.previousDeletedSubmission = _previousDeletedSubmission;
        vm.getFirstDeletedSubmissionPage = _getFirstDeletedSubmissionPage;
        vm.getLastDeletedSubmissionPage = _getLastDeletedSubmissionPage;
        vm.getDeletedSubmissionView = _getDeletedSubmissionView;
        vm.deleteSubmission = _deleteSubmission;
        vm.isMaster = _isMaster;
        vm.searchSubmission = _searchSubmission;
        vm.searchDeletedSubmission = _searchDeletedSubmission;
        vm.checkDeadline = _checkDeadline;
        vm.getSubmissionsReport = _getSubmissionsReport;
        vm.resetDownloadLink = _resetDownloadLink;

        // function calls
        _getAllSubmissions(vm.sindex);
        _getSubmissionTypes();
        _getTopics();
        _getTemplates();
        _getDeletedSubmissions(vm.dindex);
        _isMaster();
        _getSubmissionDeadlines();


        // functions implementations

        /* Sets every field to a default value */
        function _clear() {
            // custom Submission class fields
            vm.assignEval = false;
            vm.submissionID = 0;
            vm.evaluatorID = 0;
            vm.topic = null;
            vm.userType = null;
            vm.submissionTypeID = 0;
            vm.submissionTypeName = "";
            vm.submissionTitle = "";
            vm.topiccategoryID = 0;
            vm.status = "";
            vm.isEvaluated = false;
            vm.isFinalSubmission = false;
            vm.finalSubmissionAllowed = false;
            vm.acceptanceStatus = "";
            vm.avgScore = 0;
            // submission fields
            vm.submissionID = 0;
            vm.topicID = 0;
            vm.topiccategoryID = 0;
            vm.submissionTypeID = 0;
            vm.submissionAbstract = "";
            vm.title = "";
            vm.status = "";
            vm.byAdmin = false;
            // evaluation-submission fields
            vm.evaluatiorSubmissionID = 0;
            vm.evaluatorID = 0;
            vm.statusEvaluation = "";
            // evaluation fields
            vm.evaluationsubmittedID = 0;
            vm.evaluationName = "";
            vm.evaluationFile = "";
            vm.score = 0;
            vm.avgScore = 0;
            vm.publicFeedback = "";
            vm.privateFeedback = "";
            vm.evaluatorFirstName = "";
            vm.evaluatorLastName = "";
            vm.evaluationsList = [];
            vm.prevEvaluationsList = [];
            vm.evaluatorsList = [];
            vm.documentsList = [];
            vm.selectedUser = '';
            vm.TEMP = null;
            // modal
            vm.modalsubmissionID = 0;
            vm.modaluserType = "";
            vm.modalsubmissionTitle = "";
            vm.modaltopic = "";
            vm.modaltopiccategoryID = 0;
            vm.modalsubmissionAbstract = "";
            vm.modalsubmissionFileList = [];
            vm.modalsubmissionTypeName = "";
            vm.modalsubmissionTypeID = 0;
            vm.modalpanelistNames = [];
            vm.modalplan = "";
            vm.modalguideQuestions = "";
            vm.modalformat = "";
            vm.modalequipment = "";
            vm.modalduration = "";
            vm.modaldelivery = "";
            vm.modalsubIsEvaluated = false;
            vm.modalpublicFeedback = "";
            vm.modalprivateFeedback = "";
            vm.CTYPE = vm.topicsList[0];
            vm.searchUser = null;
            vm.selectedUser = null;
            vm.selected = false;
            vm.documentsList = [];
            vm.saved = false;
            if (vm.myFile != undefined) {
                vm.myFile = undefined;
            }
        }

        /*  Display dialogs */
        vm.obj = {};
        vm.toggleModal = function (action) {



            if (action == "errorfile") {

                vm.obj.title = "File Error",
                vm.obj.message1 = "Please refresh the page and try again to submit or download your Files.",

                vm.obj.message2 = vm.keyPop,
                vm.obj.label = "",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "OK",
                vm.obj.cancelbutton = false,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
                vm.okFunc = vm.deleteComplemetaryKey;
                vm.cancelFunc;

            }
            else if (action == "error") {
                vm.obj.title = "Server Error",
               vm.obj.message1 = "Please refresh the page and try again.",
               vm.obj.message2 = "",
               vm.obj.label = "",
               vm.obj.okbutton = true,
               vm.obj.okbuttonText = "OK",
               vm.obj.cancelbutton = false,
               vm.obj.cancelbuttoText = "Cancel",
               vm.showConfirmModal = !vm.showConfirmModal;
            }
        };
        //start pagination code
        /* Retrieves every submission in the system */
        function _getAllSubmissions(index) {
            restApi.getAllSubmissions(index).
                   success(function (data, status, headers, config) {
                       if (data.results == null)
                           vm.empty1 = true;
                       vm.smaxIndex = data.maxIndex;
                       if (vm.smaxIndex == 0) {
                           vm.sindex = 0;
                           vm.submissionsList = [];
                       }
                       else if (vm.sindex >= vm.smaxIndex) {
                           vm.sindex = vm.smaxIndex - 1;
                           _getAllSubmissions(vm.sindex);
                       }
                       else {
                           vm.submissionsList = data.results;
                       }
                       vm.submissionsList.forEach(function (sub, index2) {
                           sub.acceptanceStatus = sub.status;
                       });
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal("error");
                   });
        }
        function _nextSubmission() {
            if (vm.sindex < vm.smaxIndex - 1) {
                vm.sindex += 1;
                _getAllSubmissions(vm.sindex);
            }
        }


        function _previousSubmission() {
            if (vm.sindex > 0) {
                vm.sindex -= 1;
                _getAllSubmissions(vm.sindex);
            }
        }

        function _getFirstSubmissionPage() {
            vm.sindex = 0;
            _getAllSubmissions(vm.sindex);
        }

        function _getLastSubmissionPage() {
            vm.sindex = vm.smaxIndex - 1;
            _getAllSubmissions(vm.sindex);
        }
        //----END PAGINATON CODE---

        /* [Randy] Download a file through the browser */
        function _downloadPDFFile(id) {
            restApi.getSubmissionFile(id).
                success(function (data, status, headers, config) {
                    //window.open(data.document);

                    $("#file-" + id).attr("href", data.document).attr("download", data.documentName);
                    
                    //var file = new Blob([data.document]);
                    //saveAs(file, data.documentName);
                }).
                error(function (data, status, headers, config) {
                   
                    vm.toggleModal("errorfile");
                });
        }

        /* [Randy] Download a file through the browser */
        function _downloadEvaluationFile(id) {
            $("#file-" + id).attr("href", vm.evaluationFile).attr("download", vm.evaluationName);
        }

        /* reset the link to default */
        function _resetDownloadLink(id) {
            $("#file-" + id).attr("href", "").removeAttr("download");
        }

        /* Set all fields with the submission information */
        function _getSubmissionView(submissionID) {
            restApi.getUserSubmission(submissionID).
                    success(function (data, status, headers, config) {
                        vm.submitterFirstName = data.submitterFirstName;
                        vm.submitterLastName = data.submitterLastName;
                        vm.submitterEmail = data.submitterEmail;
                        vm.submissionID = data.submissionID;
                        vm.userType = data.userType;
                        vm.submissionTitle = data.submissionTitle;
                        vm.topic = data.topic;
                        vm.topiccategoryID = data.topiccategoryID;
                        vm.submissionAbstract = data.submissionAbstract;
                        vm.submissionFileList = data.submissionFileList;
                        vm.submissionTypeName = data.submissionType;
                        vm.submissionTypeID = data.submissionTypeID;
                        vm.panelistNames = data.panelistNames;
                        vm.plan = data.plan;
                        vm.guideQuestions = data.guideQuestions;
                        vm.format = data.format;
                        vm.equipment = data.equipment;
                        vm.duration = data.duration;
                        vm.delivery = data.delivery;
                        vm.subIsEvaluated = data.subIsEvaluated;
                        vm.publicFeedback = data.publicFeedback;
                        vm.privateFeedback = data.privateFeedback;
                        //for previous submissions
                        vm.hasPrevVersion = data.hasPrevVersion;
                        vm.prevSubmissionID = data.prevSubmissionID;
                        vm.prevSubmissionTitle = data.prevSubmissionTitle;
                        vm.prevTopic = data.prevTopic;
                        vm.prevSubmissionAbstract = data.prevSubmissionAbstract;
                        vm.prevSubmissionFileList = data.prevSubmissionFileList;
                        vm.prevSubmissionType = data.prevSubmissionType;
                        vm.prevPanelistNames = data.prevPanelistNames;
                        vm.prevPlan = data.prevPlan;
                        vm.prevGuideQuestions = data.prevGuideQuestions;
                        vm.prevFormat = data.prevFormat;
                        vm.prevEquipment = data.prevEquipment;
                        vm.prevDuration = data.prevDuration;
                        vm.prevDelivery = data.prevDelivery;
                        vm.prevSubIsEvaluated = data.prevSubIsEvaluated;
                        vm.prevPublicFeedback = data.prevPublicFeedback;
                        vm.prevPrivateFeedback = data.prevPrivateFeedback;
                        //for modal
                        vm.modalsubmissionID = data.submissionID;
                        vm.modaluserType = data.userType;
                        vm.modalsubmissionTitle = data.submissionTitle;
                        vm.modaltopic = data.topic;
                        vm.modaltopiccategoryID = data.topiccategoryID;
                        vm.modalsubmissionAbstract = data.submissionAbstract;
                        vm.modalsubmissionFileList = data.submissionFileList;
                        vm.modalsubmissionTypeName = data.submissionType;
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
                        vm.modalprivateFeedback = data.privateFeedback;
                        vm.documentsList = data.submissionFileList.slice(0);
                        vm.deleted = false;

                        vm.topicsList.forEach(function (t, index) {
                            if (t.topiccategoryID == data.topiccategoryID)
                                vm.CTYPE = vm.topicsList[index];
                        });

                        vm.templatesList.forEach(function (tem, index) {
                            if (tem.templateID == data.templateID) {
                                vm.TEMP = vm.templatesList[index];
                                vm.assignEval = true;
                            }
                        });

                        _getEvaluationsForSubmission(submissionID);
                    }).
                   error(function (data, status, headers, config) {
                       vm.submissionsList = data;
                       vm.toggleModal("error");
                   });
        }


        /* Set all fields with the deleted submission information */
        function _getDeletedSubmissionView(submissionID) {
            vm.loading = true;
            restApi.getADeletedSubmission(submissionID).
                    success(function (data, status, headers, config) {

                        vm.submitterFirstName = data.submitterFirstName;
                        vm.submitterLastName = data.submitterLastName;
                        vm.submitterEmail = data.submitterEmail;
                        vm.submissionID = data.submissionID;
                        vm.userType = data.userType;
                        vm.submissionTitle = data.submissionTitle;
                        vm.topic = data.topic;
                        vm.topiccategoryID = data.topiccategoryID;
                        vm.submissionAbstract = data.submissionAbstract;
                        vm.submissionFileList = data.submissionFileList;
                        vm.submissionTypeName = data.submissionType;
                        vm.submissionTypeID = data.submissionTypeID;
                        vm.panelistNames = data.panelistNames;
                        if (data.submissionTypeID == 3){
                            vm.plan = data.planPanel;
                            vm.equipment = data.equipmentPanel;
                        }
                        else if (data.submissionTypeID == 5){
                            vm.plan = data.planWorkshop;
                            vm.equipment = data.equipmentWorkshop;
                        }
                        vm.guideQuestions = data.guideQuestions;
                        vm.format = data.format;
                        vm.duration = data.duration;
                        vm.delivery = data.delivery;
                        vm.subIsEvaluated = data.subIsEvaluated;
                        vm.publicFeedback = data.publicFeedback;
                        vm.privateFeedback = data.privateFeedback;
                        //for previous submissions
                        vm.hasPrevVersion = data.hasPrevVersion;
                        vm.prevSubmissionID = data.prevSubmissionID;
                        vm.prevSubmissionTitle = data.prevSubmissionTitle;
                        vm.prevTopic = data.prevTopic;
                        vm.prevSubmissionAbstract = data.prevSubmissionAbstract;
                        vm.prevSubmissionFileList = data.prevSubmissionFileList;
                        vm.prevSubmissionType = data.prevSubmissionType;
                        vm.prevPanelistNames = data.prevPanelistNames;
                        vm.prevPlan = data.prevPlan;
                        vm.prevGuideQuestions = data.prevGuideQuestions;
                        vm.prevFormat = data.prevFormat;
                        vm.prevEquipment = data.prevEquipment;
                        vm.prevDuration = data.prevDuration;
                        vm.prevDelivery = data.prevDelivery;
                        vm.prevSubIsEvaluated = data.prevSubIsEvaluated;
                        vm.prevPublicFeedback = data.prevPublicFeedback;
                        vm.prevPrivateFeedback = data.prevPrivateFeedback;
                        //for modal
                        vm.modalsubmissionID = data.submissionID;
                        vm.modaluserType = data.userType;
                        vm.modalsubmissionTitle = data.submissionTitle;
                        vm.modaltopic = data.topic;
                        vm.modaltopiccategoryID = data.topiccategoryID;
                        vm.modalsubmissionAbstract = data.submissionAbstract;
                        vm.modalsubmissionFileList = data.submissionFileList;
                        vm.modalsubmissionTypeName = data.submissionType;
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
                        vm.modalprivateFeedback = data.privateFeedback;
                        vm.documentsList = data.submissionFileList;
                        vm.deleted = true;

                        vm.TEMP = { templateID: data.templateID, templateName: data.templateName };

                        _getEvaluationsForSubmission(submissionID);
                    }).
                   error(function (data, status, headers, config) {
                       vm.submissionsList = data;
                       vm.toggleModal("error");
                   });
        }

        /* Get all evaluations of a single submission */
        function _getEvaluationsForSubmission(submissionID) {
            vm.evaluationsList = [];
            vm.prevEvaluationsList = [];
            restApi.getEvaluationsForSubmission(submissionID).
                  success(function (data, status, headers, config) {
                      data.forEach(function (eva, index) {
                          if (!eva.isPrevSub)
                              vm.evaluationsList.push(eva);
                          else
                              vm.prevEvaluationsList.push(eva);
                      });

                  }).
                  error(function (data, status, headers, config) {
                      vm.toggleModal("error");
                      vm.evaluationsList = data;
                  });
        }

        /* Get details of an evaluation */
        function _getEvaluationDetails(submissionID, evaluatorID) {
            var eva = { submissionID: submissionID, evaluatorID: evaluatorID };
            restApi.getEvaluationDetails(eva).
                  success(function (data, status, headers, config) {
                      vm.evaluationsubmittedID = data.evaluationsubmittedID;
                      vm.evaluationName = data.evaluationFileName;
                      vm.evaluationFile = data.evaluationFile;
                      vm.score = data.score;
                      vm.avgScore = data.avgScore;
                      vm.publicFeedback = data.publicFeedback;
                      vm.privateFeedback = data.privateFeedback;
                      vm.evaluatorFirstName = data.evaluatorFirstName;
                      vm.evaluatorLastName = data.evaluatorLastName;
                      vm.score = data.score;
                      vm.deleted = data.deleted;
                  }).
                  error(function (data, status, headers, config) {
                      vm.toggleModal("error");
                  });
        }

        /* Get list of evaluators */
        function _getAllEvaluators() {
            restApi.getAllEvaluators().
                  success(function (data, status, headers, config) {
                      vm.evaluatorsList = data;
                  }).
                  error(function (data, status, headers, config) {
                      vm.toggleModal("error");
                      vm.evaluatorsList = data;
                  });
        }

        /* Assign an evaluator to a submission */
        function _assignEvaluator(submissionID, evaluatorID) {
            vm.processing = true;
            var IDs = { submissionID: submissionID, evaluatorID: evaluatorID }

            vm.exists = false;
            vm.evaluationsList.forEach(function (eva, index) {
                if (eva.evaluatorID == evaluatorID) {
                    vm.exists = true;
                    vm.processing = false;
                }
            });

            if (!vm.exists) {
                restApi.assignEvaluator(IDs).
                  success(function (data, status, headers, config) {
                      vm.evaluationsList.push(data);
                      vm.evaluatorID = "";
                      vm.processing = false;
                  }).
                  error(function (data, status, headers, config) {
                      vm.toggleModal("error");
                  });                
            }


            
        }

        /* Remove an assigned evaluator from a submission */
        function _removeEvaluator(evaluatorSubmissionID) {
            vm.removing = true;
            restApi.removeEvaluator(evaluatorSubmissionID).
                  success(function (data, status, headers, config) {
                      vm.evaluationsList.forEach(function (eva, index) {
                          if (eva.evaluatorSubmissionID == data) {
                              vm.evaluationsList.splice(index, 1);
                              vm.removing = false;
                          }
                      });
                  }).
                  error(function (data, status, headers, config) {
                      vm.toggleModal("error");
                  });
        }

        /* Assign a template to a submission */
        function _assignTemplate(submissionID, templateID) {
            var IDs = { submissionID: submissionID, templateID: templateID }
            restApi.assignTemplate(IDs).
                  success(function (data, status, headers, config) {
                      vm.saved = true;
                      vm.assignEval = true;
                  }).
                  error(function (data, status, headers, config) {
                      vm.toggleModal("error");
                  });
        }

        /* Get Submission Types for Dropdown menu */
        function _getSubmissionTypes() {
            restApi.getSubmissionTypes().
                   success(function (data, status, headers, config) {
                       vm.submissionTypeList = data;
                       if (data != null)
                           vm.TYPE = vm.submissionTypeList[0];
                       var other = vm.submissionTypeList[3];
                       var last = vm.submissionTypeList[4];
                       vm.submissionTypeList[3] = last;
                       vm.submissionTypeList[4] = other;
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal("error");
                   });
        }

        /* Get Topics for Dropdown menu */
        function _getTopics() {
            restApi.getTopics()
            .success(function (data, status, headers, config) {
                vm.topicsList = data;
                if (data != null)
                    vm.CTYPE = vm.topicsList[0];
            })
           .error(function (data, status, headers, config) {
               vm.toggleModal("error");
           });
        }

        /* Add a new Submission */
        function _addAdminSubmission(file) {
        vm.loading =true;
            if(vm.viewModal == 'add') {
                if (vm.TYPE.submissionTypeID == 1 || vm.TYPE.submissionTypeID == 2 || vm.TYPE.submissionTypeID == 4) {//if paper, poster o bof
                    var submission = {
                        submissionID: vm.modalsubmissionID,
                        userID: vm.selectedUser, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.TYPE.submissionTypeID,
                        submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle
                    }
                }
                else if (vm.TYPE.submissionTypeID == 3) {//if pannel
                    var submission = {
                        submissionID: vm.modalsubmissionID,
                        userID: vm.selectedUser, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.TYPE.submissionTypeID,
                        submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle, panelistNames: vm.modalpanelistNames,
                        plan: vm.modalplan, guideQuestion: vm.modalguideQuestions, formatDescription: vm.modalformat, necessaryEquipment: vm.modalequipment
                    }
                }
                else if (vm.TYPE.submissionTypeID == 5) {//if workshops
                    var submission = {
                        submissionID: vm.modalsubmissionID,
                        userID: vm.selectedUser, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.TYPE.submissionTypeID,
                        submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle, plan: vm.modalplan, duration: vm.modalduration,
                        delivery: vm.modaldelivery, necessary_equipment: vm.modalequipment
                    }
                }
                restApi.postAdminSubmission(submission)
                        .success(function (data, status, headers, config) {
                     
                                   /* $('#success').modal({                    // wire up the actual modal functionality and show the dialog
                                        "backdrop": "static",
                               "keyboard" : true,
                                        "show": true                     // ensure the modal is shown immediately
                                    });*/
                            
                            if (vm.TYPE.submissionTypeID != 4){
                            vm.documentsList.forEach(function(file, index) {
                            file.submissionID = data.submissionID;
                                restApi.addFileToSubmission(file)
                                    .success(function(data2, status2, headers2, config2) {

                                    	if(index == vm.documentsList.length -1){
                                    		vm.loading = false;
                                    		 $('#addSubmissionModal').modal('hide');
                                    		    _clear();
                                    		}
                                       })
                                    .error(function(error) { 
                                   		 vm.loading = false;
                                          $('#addSubmissionModal').modal('hide');
                                         vm.toggleModal("error");

                                       });
                                });
                            }
                            _getAllSubmissions(vm.sindex);

                        })
                        .error(function (error) {
                         vm.loading = false;
                          $('#addSubmissionModal').modal('hide');
                            _getAllSubmissions(vm.sindex);
                            vm.toggleModal("error");
                        });
            }
            else if (vm.viewModal == 'edit') { //if updating submission
                if (vm.modalsubmissionTypeID == 1 || vm.modalsubmissionTypeID == 2 || vm.modalsubmissionTypeID == 4) {//if paper, poster o bof
                    var submission = {
                        submissionID: vm.modalsubmissionID,
                        userID: vm.selectedUser, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.modalsubmissionTypeID,
                        submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle
                    }
                }
                else if (vm.modalsubmissionTypeID == 3) {//if pannel
                    var submission = {
                        submissionID: vm.modalsubmissionID,
                        userID: vm.selectedUser, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.modalsubmissionTypeID,
                        submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle, panelistNames: vm.modalpanelistNames,
                        plan: vm.modalplan, guideQuestion: vm.modalguideQuestions, formatDescription: vm.modalformat, necessaryEquipment: vm.modalequipment
                    }
                }
                else if (vm.modalsubmissionTypeID == 5) {//if workshops
                    var submission = {
                        submissionID: vm.modalsubmissionID,
                        userID: vm.selectedUser, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.modalsubmissionTypeID,
                        submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle, plan: vm.modalplan, duration: vm.modalduration,
                        delivery: vm.modaldelivery, necessary_equipment: vm.modalequipment
                    }
                }
                if (vm.myFile != undefined) {
                    submission.document = vm.content;
                    submission.documentName = vm.myFile.name;
                    vm.myFile.name = "";
                }
                submission.documentssubmitteds = vm.documentsList;
                restApi.editSubmission(submission)
                       .success(function (data, status, headers, config) {
                          /* $('#success').modal({                    // wire up the actual modal functionality and show the dialog
                               "backdrop": "static",
                               "keyboard": true,
                               "show": true                     // ensure the modal is shown immediately
                           });*/ 
                           vm.loading =false;

                            
                                   _getSubmissionView(vm.submissionID);
                                   _getAllSubmissions(vm.sindex);
                                    $('#editSubmissionModal').modal('hide');
                               })
                               .error(function (error) {
                                 $('#editSubmissionModal').modal('hide');
                                vm.loading = false;
                                vm.toggleModal("error");
                       });                
            }
            else if (vm.viewModal == "final") {
                    if (vm.submissionTypeID == 1 || vm.submissionTypeID == 2 || vm.submissionTypeID == 4) {//if paper, poster o bof
                        var submission = {
                            initialSubmissionID: vm.modalsubmissionID,
                            userID: vm.selectedUser, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.submissionTypeID,
                            submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle
                        }
                    }
                    else if (vm.submissionTypeID == 3) {//if pannel
                        var submission = {
                            initialSubmissionID: vm.modalsubmissionID,
                            userID: vm.selectedUser, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.submissionTypeID,
                            submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle, panelistNames: vm.modalpanelistNames,
                            plan: vm.modalplan, guideQuestion: vm.modalguideQuestions, formatDescription: vm.modalformat, necessaryEquipment: vm.modalequipment
                        }
                    }
                    else if (vm.submissionTypeID == 5) {//if workshops
                        var submission = {
                            initialSubmissionID: vm.modalsubmissionID,
                            userID: vm.selectedUser, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.submissionTypeID,
                            submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle, plan: vm.modalplan, duration: vm.modalduration,
                            delivery: vm.modaldelivery, necessary_equipment: vm.modalequipment
                        }
                    }

                    submission.documentssubmitteds = vm.documentsList;
                    submission.byAdmin = true;
                    vm.loading = true;
                    restApi.postAdminFinalSubmission(submission)
                            .success(function (data, status, headers, config) {
                             /*   $('#success').modal({                    // wire up the actual modal functionality and show the dialog
                                    "backdrop": "static",
                                    "keyboard": true,
                                    "show": true                     // ensure the modal is shown immediately
                                });*/
                               
                                 
                                vm.documentsList.forEach(function (file, index) {
                                if (file.document != null && file.document != "") {
                                    file.submissionID = data.submissionID;
                                     
                                    restApi.addFileToSubmission(file)

                                    .success(function (data2, status2, headers2, config2) {
                                    if(index== vm.documentsList.length -1){
                                    		vm.loading = false;
                                    		 $('#editSubmissionModal').modal('hide');
                                    		   
                                    		}
                                    }).error(function (error) {
                                                        $('#editSubmissionModal').modal('hide');
                                                        vm.loading = false;
                                                        vm.toggleModal("errorfile");
                                                    });
                                            }
                                    });
                                _getAllSubmissions(vm.sindex);
                                vm.view = false;
                                 $('#editSubmissionModal').modal('hide');
                            })
                            .error(function (error) {
                               $('#editSubmissionModal').modal('hide');
                                vm.loading = false;
                                vm.toggleModal("error");
                                
                            });
            }
                    
            
        }

        /* Add document to documents pool */
        function _addDocument() {
         vm.loading = true;
            vm.document = vm.content;
            vm.documentName = vm.myFile.name;
            vm.myFile = { document: vm.document, documentName: vm.documentName };
            vm.newdocument = [];
            vm.newdocument.push(vm.myFile);

            if (vm.modalsubmissionID != null && vm.modalsubmissionID != "" && vm.viewModal != "final") {
                vm.myFile.submissionID = vm.modalsubmissionID;
                restApi.addFileToSubmission(vm.myFile)
                  .success(function (data, status, headers, config) {
                   vm.loading = false;
                      if (data != null && data != "") {
                          vm.myFile.document = "";
                          vm.myFile.documentssubmittedID = data.documentssubmittedID;
                          vm.documentsList.push(vm.myFile);
                      }
                  })
                  .error(function (error) {
                         vm.loading = false;
                      vm.toggleModal("errorfile");
                  });
            }
            else {
                //assume new submission
                 vm.loading = false;
            vm.documentsList.push(vm.myFile);
        }
        }

        /* Delete document from documents pool */
        function _deleteDocument(document) {
            vm.documentsList.forEach(function (doc, index) {
                if (doc.documentssubmittedID == document.documentssubmittedID) {
                    vm.documentsList.splice(index, 1);
                }
            });
        }

        /* Get all templates for dropdown */
        function _getTemplates() {
            restApi.getTemplates().
                   success(function (data, status, headers, config) {
                       vm.templatesList = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal("error");
                       vm.templatesList = data;
                       _clear();
                   });
        }

        /* Update the status of the selected submission */
        function _changeSubmissionStatus(submissionID, status) {
            var obj = { status: status, submissionID: submissionID };
            restApi.changeSubmissionStatus(obj).
                   success(function (data, status, headers, config) {
                       vm.alert;
                       (data.changedAcceptanceStatus) ?
                        vm.alert = "Submission Accepted. This person has been accepted to attend the conference." :
                        vm.alert = "Submission " + data.status + ".";

                       $('#statusChanged').modal('show');
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal("error");
                   });
        }

        /* Selects a user from the search results */
        function _selectUser(user){
            vm.searchUser = user.firstName + ' ' + user.lastName;
            vm.selectedUser = user.userID;
        }

        function _getListOfUsers() {
            restApi.getListOfUsers().
                   success(function (data, status, headers, config) {
                       vm.usersList = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.usersList = data;
                       vm.toggleModal("error");
                   });
        }

        /* Get all deleted submissions */
        function _getDeletedSubmissions(index) {
            restApi.getDeletedSubmissions(index).
                   success(function (data, status, headers, config) {
                       if (data.results == null)
                           vm.empty2 = true;
                       vm.dmaxIndex = data.maxIndex;
                       if (vm.dmaxIndex == 0) {
                           vm.dindex = 0;
                           vm.deletedSubmissionsList = [];
                       }
                       else if (vm.dindex >= vm.dmaxIndex) {
                           vm.dindex = vm.dmaxIndex - 1;
                           _getDeletedSubmissions(vm.dindex);
                       }
                       else {
                           vm.empty2 = false;
                           vm.deletedSubmissionsList = data.results;
                       }
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal("error");
                   });
        }
        function _nextDeletedSubmission() {
            if (vm.dindex < vm.dmaxIndex - 1) {
                vm.dindex += 1;
                _getDeletedSubmissions(vm.dindex);
            }
        }


        function _previousDeletedSubmission() {
            if (vm.dindex > 0) {
                vm.dindex -= 1;
                _getDeletedSubmissions(vm.dindex);
            }
        }

        function _getFirstDeletedSubmissionPage() {
            vm.dindex = 0;
            _getDeletedSubmissions(vm.dindex);
        }

        function _getLastDeletedSubmissionPage() {
            vm.dindex = vm.dmaxIndex - 1;
            _getDeletedSubmissions(vm.dindex);
        }
        //----END PAGINATON CODE---

        /* Determine whether the currently logged user is a Master user */
        function _isMaster() {
            restApi.isMaster(userID).
                   success(function (data, status, headers, config) {
                       vm.isMaster = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.isMaster = data;
                       vm.toggleModal("error");
                   });
        }

        /* Remove the selected submission */
        function _deleteSubmission(id) {
         vm.loading = true;
            restApi.deleteSubmission(id).
                success(function (data, status, headers, config) {
                 vm.loading = false;
                    _getAllSubmissions(vm.sindex);
                    _getDeletedSubmissions(vm.dindex);
                    vm.submissionsList.forEach(function (submission, index) {
                       
                    });
                }).
                error(function (data, status, headers, config) {
                 vm.loading = false;
                    vm.toggleModal("error");
                });
        }

        /* check extension */
        $scope.showContent = function ($fileContent) {
            vm.content = $fileContent;
            vm.fileext = vm.myFile.name.split(".", 2)[1];
            if (vm.fileext == "pdf" || vm.fileext == "doc" || vm.fileext == "docx" || vm.fileext == "ppt" || vm.fileext == "pptx")
                vm.ext = false;
            else {
                document.getElementById("documentFile").value = "";
                vm.ext = true;
                $scope.content = "";
                $fileContent = "";
                vm.myFile = "";
            }
        };


        /* Search within the list with a certain criteria */
        function _searchSubmission() {
            vm.sindex = 0;
            var params = {index: vm.sindex, criteria: vm.criteria};
            restApi.searchSubmission(params).
                success(function (data, status, headers, config) {
                    vm.smaxIndex = data.maxIndex;
                    if (vm.smaxIndex == 0) {
                        vm.sindex = 0;
                        vm.submissionsList = [];
                    }
                    else if (vm.sindex >= vm.smaxIndex) {
                        vm.sindex = vm.smaxIndex - 1;
                        _searchSubmission(vm.sindex);
                    }
                    else {
                        vm.submissionsList = data.results;
                    }
                    vm.submissionsList.forEach(function (sub, index2) {
                        sub.acceptanceStatus = sub.status;
                    });
                }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal("error");
                   });
        }

        /* search deleted submissions within the list */
        function _searchDeletedSubmission() {
            vm.dindex = 0;
            var params = { index: vm.dindex, criteria: vm.dcriteria };
            restApi.searchDeletedSubmission(params).
                success(function (data, status, headers, config) {
                    vm.dmaxIndex = data.maxIndex;
                    if (vm.dmaxIndex == 0) {
                        vm.dindex = 0;
                        vm.deletedSubmissionsList = [];
                    }
                    else if (vm.dindex >= vm.dmaxIndex) {
                        vm.dindex = vm.dmaxIndex - 1;
                        _searchDeletedSubmission(vm.dindex);
                    }
                    else {
                        vm.deletedSubmissionsList = data.results;
                    }
                }).
                error(function (data, status, headers, config) {
                    vm.toggleModal("error");

                });
        }

        /* get whether a deadline has passed or not */
        function _getSubmissionDeadlines() {
            restApi.getSubmissionDeadlines().
                success(function (data, status, headers, config) {
                    vm.deadlinesList = data;
                }).
                error(function (data, status, headers, config) {
                    vm.toggleModal("error");

                });
        }

        /* return whether deadline has passed or not */
        function _checkDeadline(i) {
            vm.onTime = vm.deadlinesList[i-1];
        }

        /* download submissions report */
        function _getSubmissionsReport() {
            restApi.getSubmissionsReport().
                success(function (data, status, headers, config) {
                    //window.open(data);

                    if (data != null && data != "") {
                        var report = data;
                        //vm.downloadLoading = false;
                        //vm.loading = false;

                        if (report != "" && report != undefined) {
                            var blob = new Blob([report], { type: "text/plain;charset=utf-8" });
                            saveAs(blob, "Submissions_Report.csv");
                        }
                    }

                    //var file = new Blob([data]);
                    //saveAs(file, "Submissions_Report.csv");
                }).
                error(function (data, status, headers, config) {
                    vm.toggleModal("error");
                });
        }
    }
})();