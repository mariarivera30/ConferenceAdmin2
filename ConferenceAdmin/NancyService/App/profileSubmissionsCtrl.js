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
        vm.isAssigned;
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
        vm.getSubmissionDeadlines = _getSubmissionDeadlines;
        vm.checkDeadline = _checkDeadline;

        //_getSubmissionDeadline();
        _getUserSubmissions(currentUserID);
        _getSubmissionTypes();
        _getTopics();
        _getSubmissionDeadlines();

        //Functions:
        function activate() {
            
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

        /* [Randy] add a document to the list */
        function _addDocument() {
        vm.loading = true;
            vm.document = vm.content;
            vm.documentName = vm.myFile.name;
            vm.myFile = { document: vm.document, documentName: vm.documentName };
            vm.newdocument = [];
            vm.newdocument.push(vm.myFile);

            if (vm.modalsubmissionID != null && vm.modalsubmissionID != "" && vm.viewModal != "addFinal") {
                vm.myFile.submissionID = vm.modalsubmissionID;
                vm.loading = true;
                restApi.addFileToSubmission(vm.myFile)
                  .success(function (data, status, headers, config) {
                    vm.loading =false;
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
                vm.loading =false;
                vm.documentsList.push(vm.myFile);
            }
        }

        /* [Randy] remove document from the list */
        function _deleteDocument(document) {

            vm.documentsList.forEach(function (doc, index) {
                if (doc.documentssubmittedID == document.documentssubmittedID) {
                    vm.documentsList.splice(index, 1);
                }
            });
        }

        /* [Jaimeiris] prepare final version page */
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
                      });
                      data.submissionFileList.forEach(function (doc, index) {
                          vm.documentsList.push({ documentssubmittedID: doc.documentssubmittedID, documentName: doc.documentName });
                      });
                      vm.submissionTypeList.forEach(function (type, index) {
                          if (type.submissionTypeID == vm.modalsubmissionTypeID) {
                              vm.TYPE = vm.submissionTypeList[index];
                          }
                      });
                  }).
              error(function (data, status, headers, config) {
                  vm.toggleModal("error");
                  vm.submissionlist = data;
              });


            vm.viewModal = "addFinal";
            
        }

        /* [Randy] resets all attributes */
        function _clear() {
            vm.modalsubmissionID = null;
            vm.modaluserType = null;
            vm.modalsubmissionTitle = null;
            vm.modaltopic = null;
            vm.modaltopiccategoryID = null;
            vm.modalsubmissionAbstract = null;
            vm.modalsubmissionFileList = [];
            vm.modalsubmissionTypeName = null;
            vm.modalsubmissionTypeID = null;
            vm.modalpanelistNames = [];
            vm.modalplan = null;
            vm.modalguideQuestions = null;
            vm.modalformat = null;
            vm.modalequipment = null;
            vm.modalduration = null;
            vm.modaldelivery = null;
            vm.modalsubIsEvaluated = null;
            vm.modalpublicFeedback = null;
            vm.CTYPE = vm.topicsList[0];
            vm.documentsList = [];
            if (vm.myFile != undefined) {
                vm.myFile = undefined;
            }
            vm.content = "";
           /* $scope.$fileContent = "";
            if (document.getElementById("documentFile") != undefined) {
                document.getElementById("documentFile").value = "";
            }*/
        }

        /* [Jaimeiris] open add modal */
        function _viewAdd() {
            vm.viewModal = "Add";
            _clear();   
        }

        /* [Jaimeiris] prepares selected submission view */
        function _selectedSubmission(topiccategoryID, action) {
            vm.topicsList.forEach(function (topic, index) {
                if (topic.topiccategoryID == topiccategoryID) {
                    vm.CTYPE = topic;
                }
            })            
        }

        /* [Jaimeiris] open edit submission */
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

                      /*data.submissionFileList.forEach(function (doc, index) {
                          vm.documentsList.push({ documentssubmittedID: doc.documentssubmittedID, documentName: doc.documentName });
                      });*/
                      vm.documentsList = data.submissionFileList;

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
                  vm.toggleModal("error");
                  vm.submissionlist = data;
              });
        }

        /* [Jaimeiris] open view submission */
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
                      vm.toggleModal("error");
                  });
        }
        function _backToList() {
            vm.view = "Home";
        }

        /* [Jaimeiris] manage content of a file */
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
       
        /* [Jaimeiris] add a new submission */
        function _addSubmission() {           
        vm.loading = true;
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

                restApi.postSubmission(submission)
               
                        .success(function (data, status, headers, config) {
                         vm.loading = false;
                            if (vm.TYPE.submissionTypeID != 4) {
                             vm.loading = true;
                                vm.documentsList.forEach(function (file, index) {
                                    file.submissionID = data.submissionID;
                           restApi.addFileToSubmission(file)
                                .success(function (data2, status2, headers2, config2) {
                                	if(index==vm.documentsList.length -1){
                                    		vm.loading = false;
                                    		 $('#addSubmissionModal').modal('hide');
                                    		    _clear();
                                    		}
       
                                            })
                                            .error(function (error) {
                                            vm.loading = false;
                                                vm.toggleModal("errorfile");
                                               $('#addSubmissionModal').modal('hide');
                                            });
                                    });

                            }
                            vm.submissionlist.push(data);

                          
                         
                        })
                        .error(function (error) {
                            vm.loading = false;
                            _clear();
                             $('#addSubmissionModal').modal('hide');
                            vm.toggleModal("error");
                        });
            }
            else if (vm.viewModal == 'Edit') { //if updating submission
                    vm.loading = true;
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
 
                submission.documentssubmitteds = vm.documentsList;
                restApi.editSubmission(submission)
                       .success(function (data, status, headers, config) {
                        
                           if (data != null && data != "") {
                               vm.documentsList = data.documentssubmitteds;

                               vm.submissionlist.forEach(function (esub, index) {
                                   if (esub.submissionID == data.submissionID) {
                                       esub.submissionTitle = data.submissionTitle;
                                   }
                                    vm.loading = false;
                         			$('#editSubmissionModal').modal('hide');
                           });
                           }


                       })
                       .error(function (error) {
                        vm.loading = false;
                           vm.toggleModal("error");
                           $('#editSubmissionModal').modal('hide');
                           _clear();
                       });
                
            }
            else if (vm.viewModal == "addFinal") {
                    vm.loading = true;
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
                submission.documentssubmitteds = vm.documentsList;
                  vm.loading = true;
                restApi.postFinalSubmission(submission)
               
                        .success(function (data, status, headers, config) {
                            vm.documentsList.forEach(function (file, index) {
                               vm.loading = false;
                                if (file.document != null && file.document != "") {
                                  vm.loading = true;
                                    file.submissionID = data.submissionID;
                                    restApi.addFileToSubmission(file)
                                .success(function (data2, status2, headers2, config2) {
                                            /*if (data2 != null && data2 != "") {
                                                file.document = "";
                                                file.documentssubmittedID = data2.documentssubmittedID;

                                            }*/
                                      	if(index==vm.documentsList.length -1){
                                    		vm.loading = false;
                                    		}
                                            
                                            })
                                            .error(function (error) {
                                             vm.loading = false;
                                             $('#editSubmissionModal').modal('hide');
                                                vm.toggleModal("errorfile");
                                            });
                                    //end add new files\
                                }
                            });

                        
                            vm.submissionlist.forEach(function (submission, index) {
                                if (submission.submissionID == vm.modalsubmissionID) {
                                    vm.submissionlist.splice(index, 1);
                                }
                            });
                             $('#editSubmissionModal').modal('hide');
                            vm.submissionlist.push(data);
                            _clear();
                        })
                        .error(function (error) {
                         vm.loading = false;
                         $('#editSubmissionModal').modal('hide');
                            _clear();
                            vm.toggleModal("error");
                        });
            }
           // $('#editSubmissionModal').modal('hide');
        }

        function getSubmission() {

        }

        /*function _getSubmissionDeadline() {
            restApi.getSubmissionDeadline()
                       .success(function (data, status, headers, config) {
                           vm.submissionDeadlinePassed = data;
                       })
                       .error(function (error) {
                           vm.submissionDeadlinePassed = data;
                       });
        }*/

        /* [Jaimeiris] alert of deletion */
        function _deleteSelectedSubmission(submissionID) {
            vm.currentSubmissionID = submissionID;
        }

        /* [Jaimeiris] delete a specific submission */
        function _deleteSubmission() {
             vm.loading = true;
            if (vm.currentSubmissionID != undefined) {
                restApi.deleteSubmission(vm.currentSubmissionID)
                .success(function (data, status, headers, config) {
                    _getUserSubmissions(currentUserID);
                    vm.loading = false;
                    $('#deleteSubmission').modal('hide');
                    /*vm.submissionlist.forEach(function (submission, index) {
                        if (submission.submissionID == vm.currentSubmissionID) {
                            vm.submissionlist.splice(index, 1);
                        }
                    });
                    if(data != null || data != undefined || data != "")
                    vm.submissionlist.push(data);*/
                })
                .error(function (data, status, headers, config) {
                    vm.toggleModal("error"); 
                    $('#deleteSubmission').modal('hide');
                     vm.loading = false;        
                });
            }
        }

        /* [Jaimeiris] get list of submissions */
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
                       vm.toggleModal("error");
                   });
        }

        /* [Jaimeiris] get list of types */
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
                     //  alert("add un alert de submission type list");
                       vm.toggleModal("error");
                   });
        }

        /* [Jaimeiris] get list of topics */
        function _getTopics() {
            restApi.getTopics()
            .success(function (data, status, headers, config) {
                vm.topicsList = data;
                if (data != null)
                    vm.CTYPE = vm.topicsList[0];
            })
           .error(function (data, status, headers, config) {
               vm.toggleModal("error");
              // alert("An error ocurred.");
           });
        }

        /* [Randy] Download a file through the browser */
        function _downloadPDFFile(id) {
             vm.loading = true;
            restApi.getSubmissionFile(id).
                success(function (data, status, headers, config) {
                    //window.open(data.document);
             
                    $("#file-" + id).attr("href", data.document).attr("download", data.documentName);
                       vm.loading = false;
                    //var file = new Blob([data.document]);
                    //saveAs(file, data.documentName);
                }).
                error(function (data, status, headers, config) {
                   vm.loading = false;
                    vm.toggleModal("error");
                
                    //alert("An error ocurred while downloading the file.");
                });
        }

        /* [Randy] reset the link to default */
        function _resetDownloadLink(id) {
            $("#file-" + id).attr("href", "").removeAttr("download");
        }

        /* [Randy] get whether a deadline has passed or not */
        function _getSubmissionDeadlines() {
            restApi.getSubmissionDeadlines().
                success(function (data, status, headers, config) {
                    vm.deadlinesList = data;
                }).
                error(function (data, status, headers, config) {
                    vm.toggleModal("error");
                });
        }

        /* [Randy] return whether deadline has passed or not */
        function _checkDeadline(i) {
            vm.onTime = vm.deadlinesList[i - 1];
        }

    }
})();
