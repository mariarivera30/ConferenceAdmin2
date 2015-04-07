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
        vm.submissionTypeID;
        vm.submissionTitle;
        vm.topiccategoryID;
        vm.topic;
        vm.status;
        vm.acceptanceStatus;
        vm.avgScore;

        vm.acceptanceStatusList = ['Accepted', 'Rejected'];

        //Functions
        vm.getAllSubmissions = _getAllSubmissions;
        
        _getAllSubmissions();

        //Functions:
        function activate() {

        }

        function _getAllSubmissions() {
            restApi.getAllSubmissions().
                   success(function (data, status, headers, config) {
                       vm.submissionlist = data;                       
                   }).
                   error(function (data, status, headers, config) {
                       vm.submissionlist = data;
                   });
        }

        function _downloadPDFFile(document) {
            window.open(document);
        }
    }
})();