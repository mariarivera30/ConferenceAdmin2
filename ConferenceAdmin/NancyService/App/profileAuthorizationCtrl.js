(function () {
    'use strict';

    var controllerId = 'profileAuthorizationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi','$window', profileAuthorizationCtrl]);

    function profileAuthorizationCtrl($scope, $http, restApi, $window) {
        var vm = this;

        vm.userID = $window.sessionStorage.getItem('userID');
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
        vm.apply = _apply;
        vm.getProfileInfo = _getProfileInfo;

        _getTemplates();
        _getDocuments();
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
            vm.fileext = vm.myFile.name.split(".", 2)[1];
            if (vm.fileext == "pdf" || vm.fileext == "doc" || vm.fileext == "docx" || vm.fileext == "ppt")
                vm.ext = false;
            else {
                document.getElementById("input-1a").value = "";
                vm.ext = true;
                $scope.content = "";
                $fileContent = "";
                vm.myFile = undefined;
                File.name = "";
            }
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

        function _downloadTemplate(id) {
            //window.open(doc.authorizationDocument);
            restApi.getTemplateFile(id).
                success(function (data, status, headers, config) {
                    var file = new Blob([data]);
                    saveAs(file);
                }).
                error(function (data, status, headers, config) {
                    alert("An error ocurred while downloading the file.");
                });
        }

        function _uploadDocument() {
            vm.authorizationFile = $scope.content;
            vm.authorizationName = vm.myFile.name;
            vm.myFile = { authorizationFile: vm.authorizationFile, authorizationName: vm.authorizationName };

            restApi.uploadDocument(vm)
                     .success(function (data, status, headers, config) {
                         vm.myFile.authorizationID = data;
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

        function _downloadDocument(id) {
            //window.open(doc.authorizationFile);
            restApi.getAuthorizationFile(id).
                success(function (data, status, headers, config) {
                    var file = new Blob([data]);
                    saveAs(file);
                }).
                error(function (data, status, headers, config) {
                    alert("An error ocurred while downloading the file.");
                });
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
