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

        //Functions
        vm.viewProgram = _viewProgram;
        vm.viewAbstract = _viewAbstract;

        function activate() {

        }

        function _selectedFile(filename, name) {
            vm.file = filename;
            vm.name = name;
        }

        function _viewProgram() {

            restApi.getProgramDocument()
                .success(function (data, status, headers, config) {
                    if (data != null) {
                        vm.program = data.program;
                        if (vm.program != undefined && vm.program != "") {
                            window.open(vm.program);
                        }
                    }
                })

                .error(function (error) {

                });
        }

        function _viewAbstract() {
            restApi.getAbstractDocument()
               .success(function (data, status, headers, config) {
                   if (data != null) {
                       vm.abstracts = data.abstracts;
                       if (vm.abstracts != undefined && vm.abstracts != "") {
                           window.open(vm.abstracts);
                       }
                   }
               })

               .error(function (error) {

               });
        }
    }
})();