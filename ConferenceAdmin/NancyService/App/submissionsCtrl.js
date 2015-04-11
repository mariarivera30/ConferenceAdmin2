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
        vm.evaluationname;
        vm.evaluationFile;
        vm.score;
        vm.avgScore;
        vm.publicFeedback;
        vm.privateFeedback;

        //functions
        vm.clear = _clear;
        vm.getAllSubmissions = _getAllSubmissions;
        vm.downloadPDFFile = _downloadPDFFile;

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
            vm.evaluationname = "";
            vm.evaluationFile = "";
            vm.score = 0;
            vm.avgScore = 0;
            vm.publicFeedback = "";
            vm.privateFeedback = "";
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

        function _getSubmissionView(submissionID) {
            restApi.getUserSubmission().
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
                        vm.view = true;
                    }).
                   error(function (data, status, headers, config) {
                       vm.submissionlist = data;
                   });
        }

    }
})();
