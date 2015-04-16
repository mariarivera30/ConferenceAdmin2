(function () {
    'use strict';

    var controllerId = 'bannerCtrl';
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', bannerCtrl]);

    function bannerCtrl($scope, $http, restApi) {
        //Variables
        var vm = this;
        vm.activate = activate;
        vm.title = 'bannerCtrl';
    
        vm.diamondSponsors = [];
        vm.platinumSponsors = [];
        vm.goldSponsors = [];
        vm.silverSponsors = [];
        vm.bronzeSponsors = [];
        vm.showSponsor = false;

        vm.platinumBanner = [];
        vm.goldBanner = [];
        vm.silverBanner = [];
        vm.bronzeBanner = [];

        // Functions
        vm.getBanners = _getBanners;
       
        _getBanners();

        function activate() {

        }

        function _showPlatinum() {
            var i, j, temparray, size = 2;
            for (i = 0, j = vm.platinumSponsors.length; i < j; i += size) {
                temparray = vm.platinumSponsors.slice(i, i + size);
                vm.platinumBanner.push(temparray);
                //alert(temparray[0].logo);
            }
        }

        function _showGold() {
            var i, j, temparray, size = 4;
            for (i = 0, j = vm.goldSponsors.length; i < j; i += size) {
                temparray = vm.goldSponsors.slice(i, i + size);
                vm.goldBanner.push(temparray);
                //alert(temparray[0].logo);
            }
        }

        function _showSilver() {
            var i, j, temparray, size = 6;
            for (i = 0, j = vm.silverSponsors.length; i < j; i += size) {
                temparray = vm.silverSponsors.slice(i, i + size);
                vm.silverBanner.push(temparray);
                //alert(temparray[0].logo);
            }
        }

        function _showBronze() {
            var i, j, temparray, size = 8;
            for (i = 0, j = vm.bronzeSponsors.length; i < j; i += size) {
                temparray = vm.bronzeSponsors.slice(i, i + size);
                vm.bronzeBanner.push(temparray);
                //alert(temparray[0].logo);
            }
        }

        function _getBanners() {
            restApi.getBanners()
            .success(function (data, status, headers, config) {
                vm.diamondSponsors = data.diamond;
                vm.platinumSponsors = data.platinum;
                vm.goldSponsors = data.gold;
                vm.silverSponsors = data.silver;
                vm.bronzeSponsors = data.bronze;

                if (vm.platinumSponsors.length > 0) {
                    _showPlatinum();
                }
                if (vm.goldSponsors.length > 0) {
                    _showGold();
                }
                if (vm.silverSponsors.length > 0) {
                    _showSilver();
                }
                if (vm.bronzeSponsors.length > 0) {
                    _showBronze();
                }

            })
           .error(function (data, status, headers, config) {
           });
        }
    }
})();