(function () {
    'use strict';

    var controllerId = 'submissionCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', submissionCtrl]);

    function submissionCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        //object fields
        vm.title = 'submissionCtrl';
        vm.submissionlist = {};
        vm.submissionID;
        vm.submissionTypeName;
        vm.topic;
        vm.status;
        vm.avgScore;
        vm.numOfEvaluations;
        vm.submissionTypeID;
        vm.submissionTitle;
        vm.topiccategoryID;
        vm.topicsList;
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
        vm.prevPrivateFeedback;

        vm.acceptanceStatusList = ['Accepted', 'Rejected'];

        //Functions
        vm.getAllSubmissions = _getAllSubmissions;
        vm.getSubmissionView = _getSubmissionView;
        vm.getEvaluationsForSubmission = _getEvaluationsForSubmission;
        
        _getAllSubmissions();

        //Functions:
        function activate() {

        }
       
        //gets the list of all submissions
        function _getAllSubmissions() {
            restApi.getAllSubmissions().
                   success(function (data, status, headers, config) {
                       vm.submissionlist = data;                       
                   }).
                   error(function (data, status, headers, config) {
                       vm.submissionlist = data;
                   });
        }
        //gets the view of a submission, in the case of a final submission it also gets the info of the previous submission
        function _getSubmissionView(submissionID) {
            restApi.getUserSubmission().
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
                        vm.modalprivateFeedback = data.privateFeedback;
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
                        vm.modalprevPrivateFeedback = data.prevPrivateFeedback;
                   }).
                   error(function (data, status, headers, config) {
                       vm.submissionlist = data;
                   });
        }

        function _getEvaluationsForSubmission(submissionID) {
            restApi.getEvaluationsForSubmission(submissionID).
                  success(function (data, status, headers, config) {
                      //vm.submissionlist = data;
                  }).
                  error(function (data, status, headers, config) {
                      //vm.submissionlist = data;
                  });
        }

        function _downloadPDFFile(document) {
            window.open(document);
        }
    }
})();