(function () {
    'use strict';

    var controllerId = 'submissionsCtrl';

    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', '$window', submissionsCtrl]);

    function submissionsCtrl($scope, $http, restApi, $window) {
        var vm = this;
        var userID = $window.sessionStorage.getItem('userID');
        vm.submissionsList = [];
        vm.acceptanceStatusList = ['Accepted', 'Rejected'];
        vm.evaluationsList = [];
        vm.evaluatorsList = [];
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

        //functions
        vm.clear = _clear;
        vm.getAllSubmissions = _getAllSubmissions;
        vm.downloadPDFFile = _downloadPDFFile;
        vm.getSubmissionView = _getSubmissionView;
        vm.getEvaluationsForSubmission = _getEvaluationsForSubmission;
        vm.getEvaluationDetails = _getEvaluationDetails;
        vm.getAllEvaluators = _getAllEvaluators;
        vm.assignEvaluator = _assignEvaluator;
        vm.removeEvaluator = _removeEvaluator;

        // function calls
        _getAllSubmissions();


        // functions implementations

        /* Sets every field to a default value */
        function _clear() {
            // custom Submission class fields
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
        }

        /* Retrieves every submission in the system */
        function _getAllSubmissions() {
            restApi.getAllSubmissions().
                   success(function (data, status, headers, config) {
                       vm.submissionsList = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.submissionsList = data;
                   });
        }

        /* Download a file through the browser */
        function _downloadPDFFile(document) {
            window.open(document);
        }

        /* Set all fields with the submission information */
        function _getSubmissionView(submissionID) {
            restApi.getUserSubmission(submissionID).
                    success(function (data, status, headers, config) {
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

                        _getEvaluationsForSubmission(submissionID);
                    }).
                   error(function (data, status, headers, config) {
                       vm.submissionlist = data;
                   });
        }

        /* Get all evaluations of a single submission */
        function _getEvaluationsForSubmission(submissionID) {
            restApi.getEvaluationsForSubmission(submissionID).
                  success(function (data, status, headers, config) {
                      vm.evaluationsList = data;
                  }).
                  error(function (data, status, headers, config) {
                      vm.evaluationsList = data;
                  });
        }

        /* Get details of an evaluation */
        function _getEvaluationDetails(evaluatorID) {
            var data = { submissionID: vm.submissionID, evaluatorID: evaluatorID };
            restApi.getSubmissionDetails(data).
                  success(function (data, status, headers, config) {
                      vm.evaluationsubmittedID = data.evaluationsubmittedID;
                      vm.evaluationName = data.evaluationName;
                      vm.evaluationFile = data.evaluationFile;
                      vm.score = data.score;
                      vm.avgScore = data.avgScore;
                      vm.publicFeedback = data.publicFeedback;
                      vm.privateFeedback = data.privateFeedback;
                      vm.evaluatorFirstName = data.evaluation.evaluatorFirstName;
                      vm.evaluatorLastName = data.evaluation.evaluatorLastName;
                      vm.score = data.evaluation.score;
                  }).
                  error(function (data, status, headers, config) {
                      
                  });
        }

        /* Get list of evaluators */
        function _getAllEvaluators() {
            restApi.getAllEvaluators().
                  success(function (data, status, headers, config) {
                      vm.evaluatorsList = data;
                  }).
                  error(function (data, status, headers, config) {
                      vm.evaluatorsList = data;
                  });
        }

        /* Assign an evaluator to a submission */
        function _assignEvaluator(evaluatorID) {
            restApi.assignEvaluator(evaluatorID).
                  success(function (data, status, headers, config) {

                  }).
                  error(function (data, status, headers, config) {

                  });
        }

        /* Remove an assigned evaluator from a submission */
        function _removeEvaluator(evaluatorID) {
            restApi.removeEvaluator(evaluatorID).
                  success(function (data, status, headers, config) {

                  }).
                  error(function (data, status, headers, config) {

                  });
        }
    }
})();
