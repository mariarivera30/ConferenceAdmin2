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
        vm.documentsList = {};
        vm.authorizationID;
        vm.companionKey;
        vm.wrongKey;
        vm.hasApplied;

        // function definitions
        vm.activate = activate;
        vm.uploadDocument = _uploadDocument;
        vm.getTemplates = _getTemplates;
        vm.downloadTemplate = _downloadTemplate;
        vm.downloadDocument = _downloadDocument;
        vm.getDocuments = _getDocuments;
        vm.deleteDocument = _deleteDocument;
        vm.selectedDocumentDelete = _selectedDocumentDelete;
        vm.selectCompanion = _selectCompanion;
        vm.getCompanionKey = _getCompanionKey;
        vm.apply = _apply;
        vm.getProfileInfo = _getProfileInfo;

        _getTemplates();
        _getDocuments();
        _getCompanionKey();
        _getProfileInfo(vm.userID);

        function activate() {

        }

        function _getProfileInfo(userID) {
            restApi.getProfileInfo(userID).
                   success(function (data, status, headers, config) {
                       vm.hasApplied = data.hasApplied;
                   }).
                   error(function (data, status, headers, config) {
                       alert("An error occurred trying to access your Profile Information.");
                   });
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
            window.open(doc.authorizationDocument);
        }

        function _uploadDocument() {
            vm.authorizationFile = $scope.content;
            vm.authorizationName = vm.myFile.name;
            vm.myFile = { authorizationFile: vm.authorizationFile, authorizationName: vm.authorizationName };

            restApi.uploadDocument(vm)
                     .success(function (data, status, headers, config) {
                         vm.documentsList.push(vm.myFile);
                     })

                     .error(function (error) {
                         alert("Something went wrong. Please try again.");
                     });
        }


        function _getDocuments() {
            restApi.getDocuments(vm.userID).
                   success(function (data, status, headers, config) {
                       vm.documentsList = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.documentsList = data;
                   });
        }

        function _downloadDocument(doc) {
            window.open(doc.authorizationFile);
        }


        function _deleteDocument() {
            if (vm.authorizationID != undefined) {
                restApi.deleteDocument(vm)
                .success(function (data, status, headers, config) {
                    vm.documentsList.forEach(function (doc, index) {
                        if (doc.authorizationID == vm.authorizationID) {
                            vm.documentsList.splice(index, 1);
                        }
                    });
                })

                .error(function (error) {
                    alert("Something went wrong. Please try again.");
                });
            }
        }


        function _selectedDocumentDelete(id) {
            vm.authorizationID = id;
        }

        function _selectCompanion(companionKey) {
            vm.companionKey = companionKey;
            restApi.selectCompanion(vm)
            .success(function (data, status, headers, config) {
                vm.wrongKey = false;
                vm.correctKey = true;
            })
            .error(function (error) {
                vm.wrongKey = true;
                vm.correctKey = false;
            });
        }
        
        function _getCompanionKey() {
            restApi.getCompanionKey(vm.userID)
            .success(function (data, status, headers, config) {
                vm.companionKey = data.companionKey;
                if (vm.companionKey != null)
                    vm.hasKey = true;
            })
            .error(function (error) {
                vm.hasKey = false;
            });
        }

        function _apply() {
            restApi.apply(vm).
                    success(function (data, status, headers, config) {
                        vm.hasApplied = true;
                    }).
                    error(function (data, status, headers, config) {
                        alert("An error occurred");
                    });
        }
    }
})();
