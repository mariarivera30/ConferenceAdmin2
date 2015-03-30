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

        //From Admin Website
        vm.conferenceName;
        vm.homeMainTitle;
        vm.homeTitle1;
        vm.homeParagraph1;
        vm.homeTitle2;
        vm.homeParagraph2;
        vm.img;

        //InterfaceElements
        vm.iconferenceName;
        vm.ihomeMainTitle;
        vm.ihomeTitle1;
        vm.ihomeParagraph1;
        vm.ihomeTitle2;
        vm.ihomeParagraph2;
        vm.iimg;

        //Functions
        vm.getHome = _getHome;
        vm.saveHome = _saveHome;
        vm.removeImage = _removeImage;

        _getHome();
        activate();

        function activate() {

        }

        $scope.saveImg = function ($fileContent) {
            $scope.img = $fileContent;
        };

        $scope.showContent = function (data) {
            $scope.content = data;
            vm.show = true;
        };

        function _downloadLogo() {
            window.open(vm.img);
        }

        function _getHome() {
            restApi.getHome()
            .success(function (data, status, headers, config) {
                if (data != null) {
                    vm.ihomeMainTitle = data.homeMainTitle;
                    vm.ihomeTitle1 = data.homeTitle1;
                    vm.ihomeParagraph1 = data.homeParagraph1;
                    vm.ihomeTitle2 = data.homeTitle2;
                    vm.ihomeParagraph2 = data.homeParagraph2;
                    vm.iimg = data.image;

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

            vm.img = $scope.img;

            var newHome = {
                homeMainTitle: vm.homeMainTitle,
                homeTitle1: vm.homeTitle1,
                homeParagraph1: vm.homeParagraph1,
                homeTitle2: vm.homeTitle2,
                homeParagraph2: vm.homeParagraph2,
                image: vm.img
            }
            restApi.saveHome(newHome)
            .success(function (data, status, headers, config) {
                if (data != null) {
                    if (vm.img != "" && vm.img != undefined) {
                        $scope.showContent(vm.img);
                        $scope.img = "";
                        $scope.$fileContent = "";
                    }
                    else {
                        vm.show = false;
                    }
                    $("#updateConfirm").modal('show');
                }
            })
            .error(function (error) {
                $("#updateError").modal('show');
            });
        }

        function _removeImage() {
            restApi.removeImage("homeImage")
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