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
        vm.temp;
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
        vm.reset = _reset;

        _getGeneralInfo();

        function activate() {

        }

        function _reset() {
            if (vm.temp != null) {

                vm.conferenceAcronym = vm.temp.conferenceAcronym;
                vm.conferenceName = vm.temp.conferenceName;
                vm.dateFrom = new Date(vm.temp.dateFrom.split('/')[2], vm.temp.dateFrom.split('/')[0] - 1, vm.temp.dateFrom.split('/')[1]); //Date(yyyy,mm-1,dd)
                vm.dateTo = new Date(vm.temp.dateTo.split('/')[2], vm.temp.dateTo.split('/')[0] - 1, vm.temp.dateTo.split('/')[1]);
                vm.logo = vm.temp.logo;
                _clear();
            }
        }

        function _clear() {
            if (document.getElementById("imageFile") != undefined) {
                document.getElementById("imageFile").value = "";
                $scope.myFile = "";
                vm.show = false;
            }
            $scope.img = "";
        }

        $scope.saveImg = function ($fileContent) {
            if ($fileContent != undefined) {
                var fileName = $scope.myFile.name;
                if (fileName != undefined) {
                    var ext = fileName.split(".", 2)[1];
                    if (ext == "png" || ext == "jpg" || ext == "gif" || ext == "jpeg" || ext == "pic" || ext == "pict") {
                        vm.show = true;
                        $scope.img = $fileContent;
                    }
                    else {
                        $("#fileExtError").modal('show');
                        if (document.getElementById("imageFile") != undefined) {
                            document.getElementById("imageFile").value = "";
                        }
                        $scope.img = "";
                        vm.show = false;
                    }
                }
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
                    vm.temp = data;
                    vm.iconferenceAcronym = data.conferenceAcronym;
                    vm.iconferenceName = data.conferenceName;
                    vm.idateFrom = data.dateFrom;
                    vm.idateTo = data.dateTo;
                    vm.conferenceAcronym = data.conferenceAcronym;
                    vm.conferenceName = data.conferenceName;
                    vm.dateFrom = new Date(data.dateFrom.split('/')[2], data.dateFrom.split('/')[0] - 1, data.dateFrom.split('/')[1]); //Date(yyyy,mm-1,dd)
                    vm.dateTo = new Date(data.dateTo.split('/')[2], data.dateTo.split('/')[0] - 1, data.dateTo.split('/')[1]);

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

                    //Update temp
                    vm.temp.conferenceAcronym= info.conferenceAcronym;
                    vm.temp.conferenceName = info.conferenceName;
                    vm.temp.dateFrom = info.dateFrom;
                    vm.temp.dateTo = info.dateTo;

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
                        vm.temp.logo = info.logo;
                        $scope.showContent(vm.logo);
                        $rootScope.$emit('ConferenceLogo', vm.logo);
                    }

                    _clear();
                    $("#updateConfirm").modal('show');
                    //$("#update").fadeIn(300).delay(1000).fadeOut(300);
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
                   vm.temp.logo = "";
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