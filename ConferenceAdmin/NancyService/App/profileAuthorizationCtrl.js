(function () {
    'use strict';

    var controllerId = 'profileAuthorizationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', profileAuthorizationCtrl]);

    function profileAuthorizationCtrl($scope, $http, restApi) {
        var vm = this;

        vm.userID = 1;
        vm.authorizationStatus;
        vm.authorization;
        vm.authorizationName;
        vm.authorizationFile;
        vm.template;
        vm.templateName;
        vm.templateFile;
        vm.myFyle;
        vm.templatesList = {};

        // function definitions
        vm.activate = activate;
        vm.uploadDocument = _uploadDocument;
        vm.getTemplates = _getTemplates;
        vm.downloadTemplate = _downloadTemplate;

        _getTemplates();

        function activate() {

        }

        $scope.showContent = function ($fileContent) {
            $scope.content = $fileContent;
        };

        function _getTemplates() {
            restApi.getTemplates().
                   success(function (data, status, headers, config) {
                       vm.templatesList = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.templatesList = data;
                   });
        }

        function _downloadTemplate(doc) {
            window.open(doc.authorizationFile);
        }

        function _uploadDocument() {
            vm.authorizationFile = $scope.content;
            vm.authorizationName = vm.myFile.name;

            restApi.uploadDocument(vm)
                     .success(function (data, status, headers, config) {
                         alert("Success! :)");
                     })

                     .error(function (error) {
                         alert("Error! :(");
                     });
        }


        function _downloadDocument() {

        }
        
    }
})();
