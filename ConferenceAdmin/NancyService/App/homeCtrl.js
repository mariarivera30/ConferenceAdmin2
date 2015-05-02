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
        vm.saveLoading = false;

        //From Admin Website
        vm.temp;
        vm.homeMainTitle;
        vm.homeParagraph1;
        vm.img;

        //Functions
        vm.getHome = _getHome;
        vm.saveHome = _saveHome;
        vm.removeImage = _removeImage;
        vm.clear = _clear;
        vm.reset = _reset;

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

        _getHome();
        activate();

        function activate() {

        }

        function _reset() {
            if (vm.temp != null) {
                vm.homeMainTitle = vm.temp.homeMainTitle;
                vm.homeParagraph1 = vm.temp.homeParagraph1;
                vm.img = vm.temp.image;
                _clear();
            }
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
                if (data != null && data != "") {
                    vm.temp = data;
                    vm.homeMainTitle = data.homeMainTitle;
                    vm.homeParagraph1 = data.homeParagraph1;
                    
                    _getImage();

                    load();
                }
            })

            .error(function (error) {
                load();
                vm.toggleModal('error');
            });
        }

        function _getImage() {
            restApi.getHomeImage()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.img = data.image;
                    vm.temp.image = data.image;

                    if (vm.img != "" && vm.img != undefined) {
                        $scope.showContent(vm.img);
                    }
                    else {
                        vm.show = false;
                    }
                }
            })

            .error(function (error) {
                vm.toggleModal('error');
            });
        }

        function _saveHome() {
            vm.disabled = true;
            vm.saveLoading = true;

            var newHome = {
                homeMainTitle: vm.homeMainTitle,
                homeParagraph1: vm.homeParagraph1,
                image: $scope.img
            }
            restApi.saveHome(newHome)
            .success(function (data, status, headers, config) {
                if (data) {

                    vm.temp.homeMainTitle = newHome.homeMainTitle;
                    vm.temp.homeParagraph1 = newHome.homeParagraph1;

                    if (newHome.image != "" && newHome.image != undefined) {
                        vm.img = newHome.image;
                        vm.temp.image = newHome.image;
                        $scope.showContent(vm.img);
                        _clear();
                    }
                    $("#updateConfirm").modal('show');
                }

                vm.saveLoading = false;
                vm.disabled = false;
            })
            .error(function (error) {
                vm.saveLoading = false;
                vm.disabled = false;
                $("#updateError").modal('show');
            });

        }

        function _removeImage() {
            restApi.removeFile("homeImage")
           .success(function (data, status, headers, config) {
               if (data) {
                   vm.img = "";
                   vm.temp.image = "";
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