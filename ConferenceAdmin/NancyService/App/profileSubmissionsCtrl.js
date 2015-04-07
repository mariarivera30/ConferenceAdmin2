(function () {
    'use strict';

    var controllerId = 'profileSubmissionCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', '$window', profileSubmissionCtrl]);

    function profileSubmissionCtrl($scope, $http, restApi, $window) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'profileSubmissionCtrl';
        var currentUserID = $window.sessionStorage.getItem('userID');
        vm.submission;
        vm.submissionID;
        vm.submissionType;
        vm.submissionTypeID;
        vm.submissionTypeName;
        vm.submissionTitle;
        vm.topiccategoryID;
        vm.status;
        vm.topicsList;
        vm.isEvaluated;
        vm.isFinalSubmission;
        vm.finalSubmissionAllowed;
        vm.view = "Home";
        vm.topicObj = null;
        vm.content;
        vm.documentsList = [];
        //for previous submissions
        vm.hasPrevVersion;
        vm.prevSubmissionID;
        vm.prevSubmissionTitle;
        vm.prevTopic;
        vm.prevSubmissionAbstract;
        vm.prevSubmissionFileList;
        vm.prevSubmissionType;
        vm.prevPanelistNames;
        vm.prevPlan;
        vm.prevGuideQuestions;
        vm.prevFormat;
        vm.prevEquipment;
        vm.prevDuration;
        vm.prevDelivery;
        vm.prevSubIsEvaluated;
        vm.prevPublicFeedback;


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
        vm.getTopics = _getTopics;
        vm.selectedSubmission = _selectedSubmission;
        vm.viewAdd = _viewAdd;
        vm.addSubmission = _addSubmission;
        vm.clear = _clear;
        vm.selectFinalversion = _selectFinalversion;
        vm.addDocument = _addDocument;
        vm.deleteDocument = _deleteDocument;

        _getUserSubmissions(currentUserID);
        _getSubmissionTypes();
        _getTopics();

        //Functions:
        function activate() {
            
        }

        function _addDocument() {
            vm.documentFile = vm.content;
            vm.documentName = vm.myFile.name;
            vm.myFile = { documentFile: vm.documentFile, documentName: vm.documentName };

            vm.documentsList.push(vm.myFile);
        }

        function _deleteDocument() {
            vm.documentsList.forEach(function (doc, index) {
                if (doc.documentFile == vm.documentFile) {
                    vm.documentsList.splice(index, 1);
                }
            });
        }

        function _selectFinalversion(submissionID) {
            restApi.getUserSubmission(submissionID).
                  success(function (data, status, headers, config) {
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
                      vm.topicsList.forEach(function (topic, index) {
                          if (topic.topiccategoryID == data.topiccategoryID) {
                              vm.CTYPE = vm.topicsList[index];
                              //myFile = null;
                          }
                      })
                      vm.submissionTypeList.forEach(function (type, index) {
                          if (type.submissionTypeID == vm.modalsubmissionTypeID) {
                              vm.TYPE = vm.submissionTypeList[index];
                          }
                      });
                  }).
              error(function (data, status, headers, config) {
                  // called asynchronously if an error occurs
                  // or server returns response with an error status.
                  vm.submissionlist = data;
              });


            vm.viewModal = "addFinal";
            
        }

        function _clear() {
            vm.modalsubmissionID = null;
            vm.modaluserType = null;
            vm.modalsubmissionTitle = null;
            vm.modaltopic = null;
            vm.modaltopiccategoryID = null;
            vm.modalsubmissionAbstract = null;
            vm.modalsubmissionFileList = null;
            vm.modalsubmissionTypeName = null;
            vm.modalsubmissionTypeID = null;
            vm.modalpanelistNames = null;
            vm.modalplan = null;
            vm.modalguideQuestions = null;
            vm.modalformat = null;
            vm.modalequipment = null;
            vm.modalduration = null;
            vm.modaldelivery = null;
            vm.modalsubIsEvaluated = null;
            vm.modalpublicFeedback = null;
            vm.CTYPE = vm.topicsList[0];
            if (vm.myFile != undefined) {
                vm.myFile = undefined;
            }
            vm.content = "";
           /* $scope.$fileContent = "";
            if (document.getElementById("documentFile") != undefined) {
                document.getElementById("documentFile").value = "";
            }*/
        }

        function _viewAdd() {
            vm.viewModal = "Add";
            _clear();   
        }

        function _selectedSubmission(topiccategoryID, action) {
            vm.topicsList.forEach(function (topic, index) {
                if (topic.topiccategoryID == topiccategoryID) {
                    vm.CTYPE = topic;
                }
            })            
        }

        function _viewEditForm(submissionID) {
            _clear();
            vm.viewModal = "Edit";
            //vm.itemSubmissionType = submissionTypeName;
            restApi.getUserSubmission(submissionID).
                  success(function (data, status, headers, config) {
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
                      vm.topicsList.forEach(function (topic, index) {
                          if (topic.topiccategoryID == data.topiccategoryID) {
                              vm.CTYPE = vm.topicsList[index];
                              //myFile = null;
                          }                               
                      })     
              }).
              error(function (data, status, headers, config) {
                  // called asynchronously if an error occurs
                  // or server returns response with an error status.
                  vm.submissionlist = data;
              });
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
                      //for previous submissions
                      vm.modalhasPrevVersion = data.hasPrevVersion;
                      vm.modalprevSubmissionID = data.prevSubmissionID;
                      vm.modalprevSubmissionTitle = data.prevSubmissionTitle;
                      vm.modalprevTopic = data.prevTopic;
                      vm.modalprevSubmissionAbstract = data.prevSubmissionAbstract;
                      vm.modalprevSubmissionFileList = data.prevSubmissionFileList;
                      vm.modalprevSubmissionType = data.prevSubmissionType;
                      vm.modalprevPanelistNames = data.prevPanelistNames;
                      vm.modalprevPlan = data.prevPlan;
                      vm.modalprevGuideQuestions = data.prevGuideQuestions;
                      vm.modalprevFormat = data.prevFormat;
                      vm.modalprevEquipment = data.prevEquipment;
                      vm.modalprevDuration = data.prevDuration;
                      vm.modalprevDelivery = data.prevDelivery;
                      vm.modalprevSubIsEvaluated = data.prevSubIsEvaluated;
                      vm.modalprevPublicFeedback = data.prevPublicFeedback;
                  }).
                  error(function (data, status, headers, config) {
                      // called asynchronously if an error occurs
                      // or server returns response with an error status.
                      vm.submissionlist = data;
                  });
        }
        function _backToList() {
            vm.view = "Home";
        }

        //para preview la imagen
        $scope.showContent = function ($fileContent) {
            vm.content = $fileContent;
        };
       
        function _addSubmission() {           
            //if submiting for the first time
            if (vm.viewModal == "Add") {
                if (vm.TYPE.submissionTypeID == 1 || vm.TYPE.submissionTypeID == 2 || vm.TYPE.submissionTypeID == 4) {//if paper, poster o bof
                    var submission = {
                        submissionID: vm.modalsubmissionID,
                        userID: currentUserID, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.TYPE.submissionTypeID,
                        submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle
                    }
                }
                else if (vm.TYPE.submissionTypeID == 3) {//if pannel
                    var submission = {
                        submissionID: vm.modalsubmissionID,
                        userID: currentUserID, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.TYPE.submissionTypeID,
                        submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle, panelistNames: vm.modalpanelistNames,
                        plan: vm.modalplan, guideQuestion: vm.modalguideQuestions, formatDescription: vm.modalformat, necessaryEquipment: vm.modalequipment
                    }
                }
                else if (vm.TYPE.submissionTypeID == 5) {//if workshops
                    var submission = {
                        submissionID: vm.modalsubmissionID,
                        userID: currentUserID, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.TYPE.submissionTypeID,
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
                restApi.postSubmission(submission)
                        .success(function (data, status, headers, config) {
                            vm.submissionlist.push(data);
                            //myFile = null;
                        })
                        .error(function (error) {

                        });
            }
            else if (vm.viewModal == 'Edit') { //if updating submission
                if (vm.modalsubmissionTypeID == 1 || vm.modalsubmissionTypeID == 2 || vm.modalsubmissionTypeID == 4) {//if paper, poster o bof
                    var submission = {
                        submissionID: vm.modalsubmissionID,
                        userID: currentUserID, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.modalsubmissionTypeID,
                        submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle
                    }
                }
                else if (vm.modalsubmissionTypeID == 3) {//if pannel
                    var submission = {
                        submissionID: vm.modalsubmissionID,
                        userID: currentUserID, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.modalsubmissionTypeID,
                        submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle, panelistNames: vm.modalpanelistNames,
                        plan: vm.modalplan, guideQuestion: vm.modalguideQuestions, formatDescription: vm.modalformat, necessaryEquipment: vm.modalequipment
                    }
                }
                else if (vm.modalsubmissionTypeID == 5) {//if workshops
                    var submission = {
                        submissionID: vm.modalsubmissionID,
                        userID: currentUserID, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.modalsubmissionTypeID,
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
                           vm.submissionlist.forEach(function (submission, index) {
                               if (submission.submissionID == vm.modalsubmissionID) {
                                   submission.submissionTitle = data.submissionTitle;
                                   //myFile = null;
                               }                               
                           }
                       )
                       })
                       .error(function (error) {
                       });
            }
            else if (vm.viewModal == "addFinal") {
                if (vm.TYPE.submissionTypeID == 1 || vm.TYPE.submissionTypeID == 2 || vm.TYPE.submissionTypeID == 4) {//if paper, poster o bof
                    var submission = {
                        initialSubmissionID: vm.modalsubmissionID,
                        userID: currentUserID, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.TYPE.submissionTypeID,
                        submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle
                    }
                }
                else if (vm.TYPE.submissionTypeID == 3) {//if pannel
                    var submission = {
                        initialSubmissionID: vm.modalsubmissionID,
                        userID: currentUserID, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.TYPE.submissionTypeID,
                        submissionAbstract: vm.modalsubmissionAbstract, title: vm.modalsubmissionTitle, panelistNames: vm.modalpanelistNames,
                        plan: vm.modalplan, guideQuestion: vm.modalguideQuestions, formatDescription: vm.modalformat, necessaryEquipment: vm.modalequipment
                    }
                }
                else if (vm.TYPE.submissionTypeID == 5) {//if workshops
                    var submission = {
                        initialSubmissionID: vm.modalsubmissionID,
                        userID: currentUserID, topicID: vm.CTYPE.topiccategoryID, submissionTypeID: vm.TYPE.submissionTypeID,
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
                restApi.postFinalSubmission(submission)
                        .success(function (data, status, headers, config) {
                            vm.submissionlist.push(data);
                            vm.submissionlist.forEach(function(submission, index){
                                if(submission.submissionID == vm.modalsubmissionID){
                                    vm.submissionlist.splice(index, 1);
                                }
                            })
                        })
                        .error(function (error) {

                        });
            }
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
            vm.uploadingComp = true;
            restApi.getUserSubmissionList(currentUserID).
                   success(function (data, status, headers, config) {
                       // this callback will be called asynchronously
                       // when the response is available
                       vm.uploadingComp = false;
                       vm.submissionlist = data;
                   }).
                   error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       vm.uploadingComp = false;
                       vm.submissionlist = data;
                   });
        }

        function _getSubmissionTypes() {
            restApi.getSubmissionTypes().
                   success(function (data, status, headers, config) {
                       vm.submissionTypeList = data;
                       if (data != null)
                           vm.TYPE = vm.submissionTypeList[0];                      
                   }).
                   error(function (data, status, headers, config) {
                       alert("add un alert de submission type list");
                   });
        }

        function _getTopics() {
            restApi.getTopics()
            .success(function (data, status, headers, config) {
                vm.topicsList = data;
                if (data != null)
                    vm.CTYPE = vm.topicsList[0];
            })
           .error(function (data, status, headers, config) {
               alert("add un alert sexy");
           });
        }

        function _downloadPDFFile(document) {
            window.open(document);
        }

    }
})();
