(function () {
    'use strict';

    var controllerId = 'homeCtrl2';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', homeCtrl2]);
    function homeCtrl2($scope, $http, restApi) {

        //Variables
        var vm = this;
        vm.activate = activate;
        vm.title = 'homeCtrl2';

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

        _getHome();
        activate();

        function activate() {

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
                    
                    _getImage();

                    load();
                }
            })

            .error(function (error) {

            });
        }

        function _getImage() {
            restApi.getHomeImage()
            .success(function (data, status, headers, config) {
                if (data != null) {
                    vm.iimg = data.image;
                }
            })

            .error(function (error) {

            });
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("loading-icon").style.visibility = "hidden";
            document.getElementById("body").style.visibility = "visible";
        };
    }
})();