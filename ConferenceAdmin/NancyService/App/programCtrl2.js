(function () {
    'use strict';

    var controllerId = 'programCtrl2';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', programCtrl2]);
    function programCtrl2($scope, $http, restApi) {

        //Variables
        var vm = this;
        vm.activate = activate;
        vm.title = 'programCtrl2';
        vm.program;
        vm.abstracts;

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

        //Error modal
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
        vm.viewProgram = _viewProgram;
        vm.viewAbstract = _viewAbstract;

        function activate() {

        }

        function _selectedFile(filename, name) {
            vm.file = filename;
            vm.name = name;
        }

        //Download Program Schedule
        function _viewProgram() {

            restApi.getProgramDocument()
                .success(function (data, status, headers, config) {
                    if (data != null && data != "") {
                        vm.program = data.program;
                        if (vm.program != undefined && vm.program != "") {
                            var element = document.createElement('a');
                            element.setAttribute("href", vm.program);
                            element.setAttribute("download", "program.pdf");
                            element.click();

                            //$("#program").attr("href", vm.program).attr("download", "program.pdf");
                            //window.open(vm.program);
                        }
                    }
                })

                .error(function (error) {
                    vm.toggleModal('error');
                });
        }

        //Download abstract
        function _viewAbstract() {
            restApi.getAbstractDocument()
               .success(function (data, status, headers, config) {
                   if (data != null && data != "") {
                       vm.abstracts = data.abstracts;
                       if (vm.abstracts != undefined && vm.abstracts != "") {
                           var element = document.createElement('a');
                           element.setAttribute("href", vm.abstracts);
                           element.setAttribute("download", "abstract.pdf");
                           element.click();
                           //$("#abstract").attr("href", vm.program).attr("download", "abstract.pdf");
                           //window.open(vm.abstracts);
                       }
                   }
               })

               .error(function (error) {
                   vm.toggleModal('error');
               });
        }
    }
})();