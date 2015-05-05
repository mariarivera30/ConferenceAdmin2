//By: Heidi
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

        //Sponsor lists
        vm.diamondSponsors = [];
        vm.platinumSponsors = [];
        vm.goldSponsors = [];
        vm.silverSponsors = [];
        vm.bronzeSponsors = [];
        vm.showSponsor = false;

        //Banner Slides
        vm.platinumBanner = [];
        vm.goldBanner = [];
        vm.silverBanner = [];
        vm.bronzeBanner = [];

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

        //Error Modal
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

        // Functions
        vm.getBanners = _getBanners;
       
        _getBanners();

        function activate() {

        }

        //Display Banners

        function _showPlatinum() {
            //Platinum up to two logos per slide
            vm.platinumBanner = [];
            var i, j, temparray, size = 2;
            for (i = 0, j = vm.platinumSponsors.length; i < j; i += size) {
                temparray = vm.platinumSponsors.slice(i, i + size);
                vm.platinumBanner.push(temparray);
                //alert(temparray[0].logo);
            }
        }

        function _showGold() {
            //Platinum up to four logos per slide
            vm.goldBanner = [];
            var i, j, temparray, size = 4;
            for (i = 0, j = vm.goldSponsors.length; i < j; i += size) {
                temparray = vm.goldSponsors.slice(i, i + size);
                vm.goldBanner.push(temparray);
                //alert(temparray[0].logo);
            }
        }

        function _showSilver() {
            //Platinum up to six logos per slide
            vm.silverBanner = [];
            var i, j, temparray, size = 6;
            for (i = 0, j = vm.silverSponsors.length; i < j; i += size) {
                temparray = vm.silverSponsors.slice(i, i + size);
                vm.silverBanner.push(temparray);
                //alert(temparray[0].logo);
            }
        }

        function _showBronze() {
            //Platinum up to eight logos per slide
            vm.bronzeBanner = [];
            var i, j, temparray, size = 8;
            for (i = 0, j = vm.bronzeSponsors.length; i < j; i += size) {
                temparray = vm.bronzeSponsors.slice(i, i + size);
                vm.bronzeBanner.push(temparray);
                //alert(temparray[0].logo);
            }
        }

        //get Banners information
        function _getBanners() {

            var index=0;
            _getSponsor("Diamond", index);
            _getSponsor("Platinum", index);
            _getSponsor("Gold", index);
            _getSponsor("Silver", index);
            _getSponsor("Bronze", index);

        }

        function _getSponsor(sponsor, index) {
            var info = {
                sponsor:sponsor,
                index: index
            }
            restApi.getBanners(info)
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {

                    var maxIndex = data.maxIndex;

                    if (sponsor == "Diamond") {
                        vm.diamondSponsors = vm.diamondSponsors.concat(data.results);
                    }
                    else if (sponsor == "Platinum") {
                        vm.platinumSponsors = vm.platinumSponsors.concat(data.results);
                    }
                    else if (sponsor == "Gold") {
                        vm.goldSponsors = vm.goldSponsors.concat(data.results);
                    }
                    else if (sponsor == "Silver") {
                        vm.silverSponsors = vm.silverSponsors.concat(data.results);
                    }
                    else if (sponsor == "Bronze") {
                        vm.bronzeSponsors = vm.bronzeSponsors.concat(data.results);
                    }

                    if (index < maxIndex-1) {
                        index += 1;
                        _getSponsor(sponsor, index);
                    }
                    else {
                        if (sponsor == "Platinum" && vm.platinumSponsors.length > 0) {
                            _showPlatinum();
                        }
                        if (sponsor == "Gold" && vm.goldSponsors.length > 0) {
                            _showGold();
                        }
                        if (sponsor == "Silver" && vm.silverSponsors.length > 0) {
                            _showSilver();
                        }
                        if (sponsor == "Bronze" && vm.bronzeSponsors.length > 0) {
                            _showBronze();
                        }
                    }
                }
            })
           .error(function (data, status, headers, config) {
               vm.toggleModal('error');
           });
        }

        //Carousel Play
        $(function () {
            $('#homeCarousel').carousel({
                interval: 5000,
                pause: "false"
            });
            $('#playButton').click(function () {
                $('#homeCarousel').carousel('cycle');
            });
            $('#pauseButton').click(function () {
                $('#homeCarousel').carousel('pause');
            });
        });
    }
})();