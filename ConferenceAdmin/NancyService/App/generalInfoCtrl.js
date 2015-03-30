(function () {
    'use strict';

    var controllerId = 'generalInfoCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', generalInfoCtrl]);
    function generalInfoCtrl($scope, $http, restApi) {

        //Variables
        var vm = this;
        vm.activate = activate;
        vm.title = 'generalInfoCtrl';
        vm.show = false;
        vm.imgExist = false;

        //From Admin Website
        vm.conferenceName;
        vm.dateFrom;
        vm.dateTo;
        vm.logo;

        //InterfaceElements
        vm.iconferenceName;
        vm.idateFrom;
        vm.idateTo;
        vm.ilogo;

        //Functions
        vm.getGeneralInfo = _getGeneralInfo;
        vm.saveGeneralInfo = _saveGeneralInfo;
        vm.removeImage = _removeImage;

        _getGeneralInfo();

        function activate() {

        }

        $scope.saveImg = function ($fileContent) {
            $scope.img = $fileContent;
            vm.show = true;
        };

        $scope.showContent = function (data) {
            $scope.content = data;
            vm.imgExist = true;
        };

        function _downloadLogo() {
            window.open(vm.logo);
        }

        function _getGeneralInfo() {
            restApi.getGeneralInfo()
            .success(function (data, status, headers, config) {
                if (data != null) {
                    vm.iconferenceName = data.conferenceName;
                    vm.idateFrom = data.dateFrom;
                    vm.idateTo = data.dateTo;
                    vm.ilogo = data.logo;

                    vm.conferenceName = data.conferenceName;
                    //Format Date: mm/dd/yyyy if the field is blank "", new Date returns "Invalid Date"
                    vm.dateFrom = new Date(data.dateFrom);
                    vm.dateTo = new Date(data.dateTo);
                    vm.logo = data.logo;

                    if (vm.logo != "" && vm.logo != undefined) {
                        $scope.showContent(vm.logo);
                    }
                    else {
                        vm.imgExist = false;
                    }

                    load();
                }
            })

            .error(function (error) {

            });
        }

        function _saveGeneralInfo() {

            var d1 = ""; var d2 = "";

            if (vm.dateFrom != null && vm.dateFrom != "Invalid Date") {
                d1 = (vm.dateFrom.getUTCMonth() + 1) + "/" + vm.dateFrom.getUTCDate() + "/" + vm.dateFrom.getUTCFullYear();
            }
            else {
                vm.dateFrom = new Date("");
            }

            if (vm.dateTo != null && vm.dateTo != "Invalid Date") {
                d2 = (vm.dateTo.getUTCMonth() + 1) + "/" + vm.dateTo.getUTCDate() + "/" + vm.dateTo.getUTCFullYear();
            }
            else {
                vm.dateTo = new Date("");
            }

            vm.logo = $scope.img;

            var info = {
                conferenceName: vm.conferenceName,
                dateFrom: d1,
                dateTo: d2,
                logo: vm.logo
            }
            restApi.saveGeneralInfo(info)
            .success(function (data, status, headers, config) {
                if (data) {
                    vm.show = false;
                    if (vm.logo != "" && vm.logo != undefined) {
                        $scope.showContent(vm.logo);
                    }
                    else {
                        vm.imgExist = false;
                    }
                    $("#updateConfirm").modal('show');
                }
                else {
                    $("#updateError2").modal('show');
                }
            })
            .error(function (error) {
                $("#updateError").modal('show');
            });
        }

        function _removeImage() {
            restApi.removeImage("logo")
           .success(function (data, status, headers, config) {
               if (data) {
                   vm.logo = "";
                   $scope.content = "";
                   vm.imgExist = false;
                   $("#deleteImgConfirm").modal('show');
               }
           })
           .error(function (error) {
               $("#viewImg").modal('hide');
               $("#deleteImgError").modal('show');
           });

        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("body").style.visibility = "visible";
        };
    }
})();