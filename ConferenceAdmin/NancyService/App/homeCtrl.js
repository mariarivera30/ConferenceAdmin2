(function () {
    'use strict';

    var controllerId = 'homeCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', homeCtrl]);
    function homeCtrl($scope, $http, restApi) {

        //Variables
        var vm = this;
        vm.activate = activate;
        vm.title = 'homeCtrl';
        vm.show = false;
        vm.disabled = false;

        //From Admin Website
        vm.homeMainTitle;
        vm.homeTitle1;
        vm.homeParagraph1;
        vm.homeTitle2;
        vm.homeParagraph2;
        vm.img;

        //Functions
        vm.getHome = _getHome;
        vm.saveHome = _saveHome;
        vm.removeImage = _removeImage;
        vm.clear = _clear;

        _getHome();
        activate();

        function activate() {

        }

        function _clear() {
            if (document.getElementById("imageFile") != undefined) {
                document.getElementById("imageFile").value = "";
                $scope.myFile = "";
            }
            $scope.img = "";
        }

        $scope.saveImg = function ($fileContent) {
            if ($fileContent != undefined) {
                var fileName = $scope.myFile.name;
                if (fileName != undefined) {
                    var ext = fileName.split(".", 2)[1];
                    if (ext == "png" || ext == "jpg" || ext == "gif" || ext == "jpeg" || ext == "pic" || ext == "pict") {
                        $scope.img = $fileContent;
                    }
                    else {
                        $("#fileExtError").modal('show');
                        if (document.getElementById("imageFile") != undefined) {
                            document.getElementById("imageFile").value = "";
                        }
                        $scope.img = "";
                    }
                }
            }
        };

        $scope.showContent = function (data) {
            if (data != null) {
                $scope.content = data;
                vm.show = true;
            }
        };

        function _getHome() {
            restApi.getHome()
            .success(function (data, status, headers, config) {
                if (data != null) {
                    vm.homeMainTitle = data.homeMainTitle;
                    vm.homeTitle1 = data.homeTitle1;
                    vm.homeParagraph1 = data.homeParagraph1;
                    vm.homeTitle2 = data.homeTitle2;
                    vm.homeParagraph2 = data.homeParagraph2;
                    vm.img = data.image;

                    if (vm.img != "" && vm.img != undefined) {
                        $scope.showContent(vm.img);
                    }
                    else {
                        vm.show = false;
                    }

                    load();
                }
            })

            .error(function (error) {

            });
        }

        function _saveHome() {
            vm.disabled = true;
            var newHome = {
                homeMainTitle: vm.homeMainTitle,
                homeTitle1: vm.homeTitle1,
                homeParagraph1: vm.homeParagraph1,
                homeTitle2: vm.homeTitle2,
                homeParagraph2: vm.homeParagraph2,
                image: $scope.img
            }
            restApi.saveHome(newHome)
            .success(function (data, status, headers, config) {
                if (data != null) {
                    if (newHome.image != "" && newHome.image != undefined) {
                        vm.img = newHome.image;
                        $scope.showContent(vm.img);
                        _clear();
                    }
                    $("#updateConfirm").modal('show');
                }
            })
            .error(function (error) {
                $("#updateError").modal('show');
            });

            vm.disabled = false;
        }

        function _removeImage() {
            restApi.removeFile("homeImage")
           .success(function (data, status, headers, config) {
               if (data) {
                   vm.img = "";
                   vm.show = false;
                   $scope.content = "";
                   $("#viewImg").modal('hide');
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
            document.getElementById("loading-icon").style.visibility = "hidden";
            document.getElementById("body").style.visibility = "visible";
        };
    }
})();