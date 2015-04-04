(function () {
    'use strict';

    var controllerId = 'generalInfoCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$rootScope', '$http', 'restApi', generalInfoCtrl]);
    function generalInfoCtrl($scope, $rootScope, $http, restApi) {

        //Variables
        var vm = this;
        vm.activate = activate;
        vm.title = 'generalInfoCtrl';
        vm.show = false;
        vm.imgExist = false;
        vm.pshow = false;
        vm.disabled = false;

        //From Admin Website
        vm.conferenceAcronym;
        vm.conferenceName;
        vm.dateFrom;
        vm.dateTo;
        vm.logo;

        //InterfaceElements
        vm.iconferenceAcronym;
        vm.iconferenceName;
        vm.idateFrom;
        vm.idateTo;

        //Functions
        vm.getGeneralInfo = _getGeneralInfo;
        vm.saveGeneralInfo = _saveGeneralInfo;
        vm.removeImage = _removeImage;
        vm.clear = _clear;

        _getGeneralInfo();

        function activate() {

        }

        function _clear() {
            if (document.getElementById("imageFile") != undefined) {
                document.getElementById("imageFile").value = "";
            }
            $scope.img = "";
        }

        $scope.saveImg = function ($fileContent) {
            if ($fileContent != undefined) {
                $scope.img = $fileContent;
                vm.show = true;
            }
        };

        $scope.showContent = function (data) {
            if (data != undefined) {
                $scope.content = data;
                vm.imgExist = true;
            }
        };

        function _downloadLogo() {
            window.open(vm.logo);
        }

        function _getGeneralInfo() {
            restApi.getGeneralInfo()
            .success(function (data, status, headers, config) {
                if (data != null) {
                    vm.iconferenceAcronym = data.conferenceAcronym;
                    vm.iconferenceName = data.conferenceName;
                    vm.idateFrom = data.dateFrom;
                    vm.idateTo = data.dateTo;
                    vm.conferenceAcronym = data.conferenceAcronym;
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

            vm.disabled = true;
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

            var info = {
                conferenceAcronym: vm.conferenceAcronym,
                conferenceName: vm.conferenceName,
                dateFrom: d1,
                dateTo: d2,
                logo: $scope.img
            }
            restApi.saveGeneralInfo(info)
            .success(function (data, status, headers, config) {
                if (data) {
                    vm.show = false;
                    if (vm.iconferenceName != vm.conferenceName) {
                        vm.iconferenceName = vm.conferenceName;
                        $rootScope.$emit('ConferenceName', vm.conferenceName);
                    }

                    if (vm.iconferenceAcronym != vm.conferenceAcronym) {
                        vm.iconferenceAcronym = vm.conferenceAcronym;
                        $rootScope.$emit('ConferenceAcronym', vm.conferenceAcronym);
                    }

                    if (info.logo != "" && info.logo != undefined) {
                        vm.logo = info.logo;
                        $scope.showContent(vm.logo);
                        $rootScope.$emit('ConferenceLogo', vm.logo);
                    }

                    _clear();
                    $("#updateConfirm").modal('show');
                }
                else {
                    _clear();
                    $("#updateError2").modal('show');
                }
            })
            .error(function (error) {
                $("#updateError").modal('show');
            });

            vm.disabled = false;
        }

        function _removeImage() {
            restApi.removeFile("logo")
           .success(function (data, status, headers, config) {
               if (data) {
                   vm.logo = "";
                   $scope.content = "";
                   vm.imgExist = false;
                   $rootScope.$emit('ConferenceLogo', vm.logo);
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
            if (document.getElementById("loading-icon") != null) {
                document.getElementById("loading-icon").style.visibility = "hidden";
            }
            if (document.getElementById("body") != null) {
                document.getElementById("body").style.visibility = "visible";
            }
        };
    }
})();