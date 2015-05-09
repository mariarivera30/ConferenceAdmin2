(function () {
    'use strict';

    var controllerId = 'profileAuthorizationCtrl';

    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi','$window', profileAuthorizationCtrl]);

    function profileAuthorizationCtrl($scope, $http, restApi, $window) {
        var vm = this;
        // attributes
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
        vm.resetDownloadLink = _resetDownloadLink;

        //function calls
        _getTemplates();
        _getDocuments();
        _getProfileInfo(vm.userID);

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

        /* [Randy] Get information for the user profile */
        function _getProfileInfo(userID) {
            restApi.getProfileInfo(userID).
                   success(function (data, status, headers, config) {
                       vm.hasApplied = data.hasApplied;
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal("error");
                      // alert("An error occurred trying to access your Profile Information.");
                   });
        }

        /* [Randy] Manage content of browsed file */
        $scope.showContent = function ($fileContent) {
            $scope.content = $fileContent;
            vm.fileext = vm.myFile.name.split(".", 2)[1];
            if (vm.fileext == "pdf" || vm.fileext == "doc" || vm.fileext == "docx" || vm.fileext == "ppt" ||  vm.fileext == "pptx")
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

        /* [Randy] Get list of all authorization templates */
        function _getTemplates() {
            restApi.getAuthTemplates().
                   success(function (data, status, headers, config) {
                       vm.templatesList = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.templatesList = data;
                       vm.toggleModal("error");
                   });
        }

        /* [Randy] Download the selected authorization template */
        function _downloadTemplate(id) {
            //window.open(doc.authorizationDocument);
            restApi.getTemplateFile(id).
                success(function (data, status, headers, config) {
                    //window.open(data.templateFile);

                    $("#file-" + id).attr("href", data.templateFile).attr("download", data.templateName);

                    //var file = new Blob([data.templateFile]);
                    //saveAs(file, data.templateName);
                }).
                error(function (data, status, headers, config) {
                    vm.toggleModal("errorfile");
                   // alert("An error ocurred while downloading the file.");
                });
        }

        /* [Randy] Reset the link to default */
        function _resetDownloadLink(id) {
            $("#file-" + id).attr("href", "").removeAttr("download");
        }

        /* [Randy] Upload an authorization document */
        function _uploadDocument() {
            vm.authorizationFile = $scope.content;
            vm.authorizationName = vm.myFile.name;
            vm.myFile = { authorizationFile: vm.authorizationFile, authorizationName: vm.authorizationName };

            restApi.uploadDocument(vm)
                     .success(function (data, status, headers, config) {
                         vm.myFile.authorizationID = data;
                         vm.documentsList.push(vm.myFile);
                         vm.myFile = null;
                     })

                     .error(function (error) {
                        // alert("Something went wrong. Please try again.");
                         vm.toggleModal("error");
                     });
        }

        /* [Randy] Get list of all submitted authorization documents */
        function _getDocuments() {
            restApi.getDocuments(vm.userID).
                   success(function (data, status, headers, config) {
                       vm.documentsList = data;
                   }).
                   error(function (data, status, headers, config) {
                       vm.documentsList = data;
                   });
        }

        /* [Randy] Download the seleted authorization document */
        function _downloadDocument(id) {
            //window.open(doc.authorizationFile);
            restApi.getAuthorizationFile(id).
                success(function (data, status, headers, config) {
                    //window.open(data.authorizationFile);

                    $("#file-" + id).attr("href", data.authorizationFile).attr("download", data.authorizationName);

                    //var file = new Blob([data.authorizationFile]);
                    //saveAs(file, data.authorizationName);
                }).
                error(function (data, status, headers, config) {
                    //alert("An error ocurred while downloading the file.");
                    vm.toggleModal("errorfile");
                });
        }

        /* [Randy] Delete a specific authorization document */
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
                  //  alert("Something went wrong. Please try again.");
                    vm.toggleModal("error");
                });
            }
        }

        /* [Randy] Specify ID of authorization document to delete */
        function _selectedDocumentDelete(id) {
            vm.authorizationID = id;
        }

        /* [Randy] Make application to attend to conference */
        function _apply() {
            restApi.apply(vm).
                    success(function (data, status, headers, config) {
                        vm.hasApplied = true;
                    }).
                    error(function (data, status, headers, config) {
                     //   alert("An error occurred");
                        vm.toggleModal("error");
                    });
        }
    }
})();
