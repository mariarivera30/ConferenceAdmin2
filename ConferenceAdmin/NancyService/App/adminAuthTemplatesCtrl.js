(function () {
    'use strict';

    var controllerId = 'adminAuthTemplates';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', adminAuthTemplates]);

    function adminAuthTemplates($scope, $http, restApi) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'adminAuthTemplates';
        vm.headerModal;
        vm.add;
        vm.edit;
        vm.view;
        vm.loading = true;
        vm.template = {authorizationID:0,
            authorizationName:"",
            authorizationDocument:""
        };
           
        vm.obj = {
            title: "",
            message1: "",
            message2: "",
            label: "",
            okbutton: false,
            okbuttonText: "",
            cancelbutton: false,
            cancelbuttoText: "Cancel",
        };
        vm.okFunc;
        vm.cancelFunc;
        vm.clearPic = _clearPic;

        //Authorization Templates- Variables (Paging)
        vm.templatesList = []; //Results to Display
        vm.tindex = 0;  //Page index [Goes from 0 to tmaxIndex-1]
        vm.tmaxIndex = 0;   //Max page number

        vm.toggleModal = function (action) {
            
           
            if (action === "missingDoc") {

                vm.obj.title = "Document Requiered",
                vm.obj.message1 = "A template selection is necessary.",

                vm.obj.message2 = vm.keyPop,
                vm.obj.label ="" ,
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "OK",
                vm.obj.cancelbutton = false,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
                vm.okFunc ;
                vm.cancelFunc;

            }
            else if (action == "error")
                vm.obj.title = "Server Error",
               vm.obj.message1 = "Please refresh the page and try again.",
               vm.obj.message2 = "",
               vm.obj.label = "",
               vm.obj.okbutton = true,
               vm.obj.okbuttonText = "OK",
               vm.obj.cancelbutton = false,
               vm.obj.cancelbuttoText = "Cancel",
               vm.showConfirmModal = !vm.showConfirmModal;
        };

        // Functions
        vm.addTemplate = _addTemplate;
        vm.selectedTemplate = _selectedTemplate;
        vm.clear = _clear;
        vm.updateTemplate = _updateTemplate;
        vm.deleteTemplate = _deleteTemplate;
        vm.addValues = _addValues;
        vm.editValues = _editValues;
        vm.viewValues = _viewValues;
        vm.download = _download;

        //Functions- Template (Paging)
        vm.getTemplatesFromIndex = _getTemplatesFromIndex;
        vm.previousTemplate = _previousTemplate;
        vm.nextTemplate = _nextTemplate;
        vm.getFirstTemplatePage = _getFirstTemplatePage;
        vm.getLastTemplatePage = _getLastTemplatePage;
 

        activate();

        $scope.showContent = function ($fileContent, File) {
            $scope.content = $fileContent;
            vm.newTempName = File.name;
            vm.fileext = File.name.split(".", 2)[1];
            if (vm.fileext == "pdf" || vm.fileext == "doc" || vm.fileext == "docx" || vm.fileext == "ppt")
                vm.ext = false;
            else {
                document.getElementById("inputFile").value = "";
                vm.ext = true;
                vm.newTempName = "Empty";
            }
        };
        function _clearPic(File) {


            File = undefined;
            $scope.content = ""
            vm.newTempName = "Empty";
            vm.ext = false;
            document.getElementById("inputFile").value = "";

        }

        // Functions
        function activate() {
            _getTemplatesFromIndex(vm.tindex);
        }

        function _selectedTemplate(template, action) {

            vm.template = JSON.parse(JSON.stringify(template));
        }

        function _clear() {
            vm.template = null;
            $scope.content = "";
            $scope.$fileContent = "";
            document.getElementById("addTemplateForm").reset();


        }

        function _addValues() {
            document.getElementById("addTemplateForm").reset();
            vm.add = true;
            vm.edit = false;
            vm.view = false;
            vm.newTempName = "Empty";
            vm.template = {};
            $scope.content = undefined;
            vm.headerModal = "Add New Authorization Template";


        }
        function _viewValues() {
            vm.view = true;
            vm.add = false;
            vm.edit = false;
            vm.headerModal = "Download Authorization Template";
            $scope.content = vm.template.authorizationDocument;

        }
        function _editValues() {
            document.getElementById("addTemplateForm").reset();
            vm.edit = true;
            vm.add = false;
            vm.view = false;
            vm.headerModal = "Edit Authorization Template";
            $scope.content = vm.template.authorizationDocument;

            vm.newTempName = "Empty";
           

        }

        //Download Document
        function _download() {
            if ($scope.content != undefined && $scope.content != "") {
                // vm.sub = $scope.content.substring(17, 21).split(";")[0];
                var element = document.createElement('a');
                element.setAttribute("href", $scope.content);
                element.setAttribute("download", vm.template.authorizationName);
                element.click();
            }
        }
        
                   

        function _addTemplate(File) {
            vm.template.authorizationDocument = $scope.content;
            if ($scope.content != "" && $scope.content != undefined) {
                vm.template.authorizationName = File.name;
              
                vm.loadingUpload = true;

                restApi.addAuthTemplate(vm.template)
                         .success(function (data, status, headers, config) {
                           
                                 vm.template.authorizationID = data.authorizationID;                               

                                 if (vm.templatesList.length < 10) {
                                     vm.templatesList.push(vm.template);
                                 }
                                 else {
                                     _getTemplatesFromIndex(vm.tindex);
                                 }

                                 vm.loadingUpload = false;
                                 _clear();
                                 $('#addTemplate').modal('hide');

                         })

                         .error(function (error) {
                             vm.loadingUpload = false;
                             vm.toggleModal('error');
                             vm.loadingUpload = false;
                             $('#addTemplate').modal('hide');
                             _clear();
                          

                         });

            }

            else {
                vm.toggleModal('missingDoc');
            }

        }

        function _getTemplatesFromIndex(index) {
            vm.loadingUpload = true;
            restApi.getAuthTemplatesAdminListIndex(index).
                   success(function (data, status, headers, config) {
                       vm.tmaxIndex = data.maxIndex;
                       if (vm.tmaxIndex == 0) {
                           vm.tindex = 0;
                           vm.templatesList = [];
                       }
                       else if (vm.tindex >= vm.tmaxIndex) {
                           vm.tindex = vm.tmaxIndex - 1;
                           _getTemplatesFromIndex(vm.tindex);
                       }
                       else {
                           vm.templatesList = data.results;
                       }

                       load();
                       vm.loadingUpload = false;
                   }).
                   error(function (data, status, headers, config) {
                       vm.loadingUpload = false;
                       vm.toggleModal('error');
                      
                       _clear();
                   });
        }

        function _nextTemplate() {
            if (vm.tindex < vm.tmaxIndex - 1) {
                vm.tindex += 1;
                _getTemplatesFromIndex(vm.tindex);
            }
        }

        function _previousTemplate() {
            if (vm.tindex > 0) {
                vm.tindex -= 1;
                _getTemplatesFromIndex(vm.tindex);
            }
        }

        function _getFirstTemplatePage() {
            vm.tindex = 0;
            _getTemplatesFromIndex(vm.tindex);
        }

        function _getLastTemplatePage() {
            vm.tindex = vm.tmaxIndex - 1;
            _getTemplatesFromIndex(vm.tindex);
        }

        function _updateTemplate(File) {
            vm.loadingUpload = true;

            if (File != undefined && vm.newTempName != "Empty") {
                vm.template.authorizationDocument = $scope.content;
                vm.template.authorizationName = File.name;
            }
            if ($scope.content != "" && $scope.content != undefined) {         
                restApi.updateAuthTemplate(vm.template)
                .success(function (data, status, headers, config) {
                    vm.templatesList.forEach(function (template, index) {
                        if (template.authorizationID == vm.template.authorizationID) {
                            vm.templatesList[index] = JSON.parse(JSON.stringify(vm.template));
                            vm.loadingUpload = false;
                            $('#addTemplate').modal('hide');


                        }


                    });
                    _clear();

                }
            
            )
            .error(function (data, status, headers, config) {
                vm.toggleModal('error');
                _clear();
                vm.edit = false;
            });
            }
            else {
                vm.loadingUpload = false;
                vm.toggleModal('missingDoc');
            }
        }
        function _deleteTemplate() {
            vm.loadingRemoving = true;
            restApi.deleteAuthTemplate(vm.template.authorizationID)
            .success(function (data, status, headers, config) {
                vm.templatesList.forEach(function (template, index) {
                    if (template.authorizationID == vm.template.authorizationID) {
                        vm.templatesList.splice(index, 1);
                        vm.loadingRemoving = false;
                        $('#delete').modal('hide');
                        return;
                    }

                });
                vm.template = {};
            })

            .error(function (data, status, headers, config) {
                vm.toggleModal('error');
                vm.loadingRemoving = false;
                $('#delete').modal('hide');
            });
        }

       
        //Avoid flashing when page loads
        var load = function () {
            vm.loading = false;
        };



    }
})();
