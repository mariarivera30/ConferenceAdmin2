(function () {
    'use strict';

    var controllerId = 'programCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', programCtrl]);
    function programCtrl($scope, $http, restApi) {

        //Variables
        var vm = this;
        vm.activate = activate;
        vm.title = 'programCtrl';
        vm.program;
        vm.abstracts;
        vm.documentsList = [];
        vm.file;
        vm.name;
        vm.myFile;
        vm.myFile2;
        vm.loading = false;

        //For error modal:
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

        vm.toggleModal = function (action) {

            if (action == "error")
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

        //Functions
        vm.getProgram = _getProgram;
        vm.saveProgram = _saveProgram;
        vm.viewDocument = _viewDocument;
        vm.removeFile = _removeFile;
        vm.selectedFile = _selectedFile;

        _getProgram();

        function activate() {

        }

        function _clear() {
            if (document.getElementById("programFile") != undefined) {
                document.getElementById("programFile").value = "";
            }
            if (document.getElementById("abstractFile") != undefined) {
                document.getElementById("abstractFile").value = "";
            }
            $scope.pfile = "";
            $scope.afile = "";
        }

        function _selectedFile(filename, name) {
            vm.file = filename;
            vm.name = name;
        }

        function _viewDocument(doc) {
            window.open(doc);
        }

        $scope.saveProgramFile = function ($fileContent) {
            if ($fileContent != undefined) {
                $scope.pfile = $fileContent;
            }
        };

        $scope.saveAbstractFile = function ($fileContent) {
            if ($fileContent != undefined) {
                $scope.afile = $fileContent;
            }
        };

        $scope.programContent = function (data, attribute) {
            var add = true;
            if (data != undefined) {
                var doc = {
                    name: "Program Schedule",
                    db: attribute,
                    file: data
                }

                vm.documentsList.forEach(function (file, index) {
                    if (file.name == doc.name) {
                        add = false;
                        file.file = doc.file;
                    }
                });

                if (add) {
                    vm.documentsList.push(doc);
                }
            }
        };

        $scope.abstractContent = function (data, attribute) {
            var add = true;

            if (data != undefined) {
                var doc = {
                    name: "Abstracts/ Bios",
                    db: attribute,
                    file: data
                }

                vm.documentsList.forEach(function (file, index) {
                    if (file.name == doc.name) {
                        add = false;
                        file.file = doc.file;
                    }
                });

                if (add) {
                    vm.documentsList.push(doc);
                }

            }
        };

        function _getProgram() {
            restApi.getProgram()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.program = data.program;
                    vm.abstracts = data.abstracts;

                    if (vm.program != "" && vm.program != undefined) {
                        $scope.programContent(vm.program, data.pattribute);
                    }
                    if (vm.abstracts != "" && vm.abstracts != undefined) {
                        $scope.abstractContent(vm.abstracts, data.aattribute);
                    }
                }

                load();
            })

            .error(function (error) {
                load();
                vm.toggleModal('error');
            });
        }

        function _saveProgram() {
            vm.loading = true;
            var info = {
                program: $scope.pfile,
                abstracts: $scope.afile
            }
            restApi.saveProgram(info)
            .success(function (data, status, headers, config) {
                if (data) {

                    if (info.program != "" && info.program != undefined) {
                        vm.program = info.program;
                        $scope.programContent(vm.program);
                    }

                    if (info.abstracts != "" && info.abstracts != undefined) {
                        vm.abstracts = info.abstracts;
                        $scope.abstractContent(vm.abstracts);
                    }

                    _clear();
                    $("#updateConfirm").modal('show');
                }
                else {
                    _clear();
                    $("#updateError2").modal('show');
                }
                vm.loading = false;
            })
            .error(function (error) {
                vm.loading = false;
                vm.toggleModal('error');
            });
        }

        function _removeFile() {
            if (vm.name != "" && vm.name != undefined && vm.file != undefined && vm.file != "") {
                restApi.removeFile(vm.name)
                .success(function (data, status, headers, config) {
                    if (data) {
                        vm.documentsList.forEach(function (file, index) {
                            if (file.db == vm.name) {
                                vm.documentsList.splice(index, 1);
                            }
                        });

                        if (vm.file == "Program Schedule") {
                            vm.program = "";
                        }
                        else if (vm.file == "Abstracts/ Bios") {
                            vm.abstracts = "";
                        }

                        $("#deleteFileConfirm").modal('show');
                    }
                })
                .error(function (error) {
                    vm.toggleModal('error');
                });
            }
        }

        //Avoid flashing when page loads
        var load = function () {
            if (document.getElementById('loading-icon') != null) {
                document.getElementById("loading-icon").style.visibility = "hidden";
            }
            document.getElementById("body").style.visibility = "visible";
        };

    }
})();