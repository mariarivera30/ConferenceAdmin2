(function () {
    'use strict';

    var controllerId = 'sponsorCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', sponsorCtrl]);

    function sponsorCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        //add sponsor fields
        vm.title = 'sponsorCtrl';
        vm.sponsor;


        // Functions
        vm.addSponsor = _addSponsor;
        vm.getSponsors = _getSponsors;
        vm.getSponsorTypes = _getSponsorTypes;
        vm.selectedSponsor = _selectedSponsor;
        vm.clearSponsor = _clearSponsor;
        vm.updateSponsor = _updateSponsor;
        vm.deleteSponsor = _deleteSponsor;
        vm.submitForm = _submitForm;
        vm.addValues = _addValues;
        vm.editValues = _editValues;
        vm.viewValues = _viewValues;
        vm.downloadLogo = _downloadLogo;
        activate();

        $scope.showContent = function ($fileContent) {
            $scope.content = $fileContent;
        };




        // Functions
        function activate() {
            _getSponsors();
            _getSponsorTypes();

        }

        function _selectedSponsor(sponsor, action) {

            vm.sponsor = JSON.parse(JSON.stringify(sponsor));

            vm.TYPE = vm.sponsorsTypeList[vm.sponsor.sponsorType - 1];

        }
        function _clearSponsor() {
            vm.sponsor = null;
            vm.TYPE = vm.sponsorsTypeList[0];
            $scope.content = "";
            $scope.$fileContent = "";

        }
        function _addValues() {
            vm.add = true;
            vm.edit = false;
            vm.view = false;
            vm.headerModal = "Add Sponsor";
            _clearSponsor();
        }
        function _viewValues() {
            vm.view = true;
            vm.add = false;
            vm.edit = false;
            vm.headerModal = "View Sponsor";
        }
        function _editValues() {
            vm.edit = true;
            vm.add = false;
            vm.view = false;
            vm.headerModal = "Edit Sponsor";
            $scope.content = vm.sponsor.logo;

        }

        function _submitForm() {

            // check to make sure the form is completely valid


        };
        function _downloadLogo() {
            window.open(vm.sponsor.logo);
        }
        function _addSponsor(myFile) {
            vm.sponsor.sponsorType = vm.TYPE.sponsortypeID;
            vm.sponsor.typeName = vm.TYPE.name;
            vm.sponsor.logo = $scope.content;
            vm.sponsor.logoName = myFile.name;



            restApi.postNewSponsor(vm.sponsor)
                     .success(function (data, status, headers, config) {
                         vm.sponsorsList.push(vm.sponsor);
                         _clearSponsor();
                     })

                     .error(function (error) {

                     });
        }
        function _getSponsorTypes() {
            restApi.getSponsorTypesList().
                   success(function (data, status, headers, config) {
                       // this callback will be called asynchronously
                       // when the response is available
                       vm.sponsorsTypeList = data;
                   }).
                   error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       vm.sponsorsTypeList = data;
                   });
        }

        function _getSponsors() {
            restApi.getSponsors().
                   success(function (data, status, headers, config) {
                       // this callback will be called asynchronously
                       // when the response is available
                       vm.sponsorsList = data;
                   }).
                   error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                       vm.sponsorsList = data;
                   });
        }

        function _updateSponsor(myFile) {

            vm.sponsor.sponsorType = vm.TYPE.sponsortypeID;
            vm.sponsor.typeName = vm.TYPE.name;
            vm.sponsor.logo = $scope.content;
            vm.sponsor.logoName = myFile.name;
            restApi.updateSponsor(vm.sponsor)
            .success(function (data, status, headers, config) {
                vm.sponsorsList.forEach(function (sponsor, index) {
                    if (sponsor.sponsorID == vm.sponsor.sponsorID) {
                        vm.sponsorsList[index] = JSON.parse(JSON.stringify(vm.sponsor));
                    }

                });

                $scope.content = "";
                $scope.$fileContent = "";
                _clearSponsor();
            }
            )
            .error(function (data, status, headers, config) {
                $scope.content = "";
                $scope.$fileContent = "";
                vm.edit = false;
            });


        }
        function _deleteSponsor() {

            restApi.deleteSponsor(vm.sponsor.sponsorID)
            .success(function (data, status, headers, config) {
                vm.sponsorsList.forEach(function (sponsor, index) {
                    if (sponsor.sponsorID == vm.sponsor.sponsorID) {
                        vm.sponsorsList.splice(index, 1);
                    }

                });
                vm.sponsor = {};
            })

            .error(function (data, status, headers, config) {
            });
        }





    }
})();





