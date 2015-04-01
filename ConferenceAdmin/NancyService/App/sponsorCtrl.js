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
        vm.loading;
        vm.addComplementaryObj = { sponsorID: 0, quantity: 0, company: "" };



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
        vm.complementaryValues = _complementaryValues;
        vm.getSponsorComplementaryKeys = _getSponsorComplementaryKeys;
        vm.deleteComplemetaryKey = _deleteComplemetaryKey;
        vm.deleteSponsorComplemetaryKey = _deleteSponsorComplemetaryKey;
        vm.addComplementaryKey = _addComplementaryKey;
        vm.selectedKey = _selectedKey;



        activate();

        $scope.showContent = function ($fileContent) {

            $scope.content = $fileContent;


        };




        // Functions
        function activate() {
            _getSponsors();
            _getSponsorTypes();
            vm.loading = true;
            vm.complementaryView = false;

        }

        function _selectedSponsor(sponsor, action) {

            vm.sponsor = JSON.parse(JSON.stringify(sponsor));

            vm.TYPE = vm.sponsorsTypeList[vm.sponsor.sponsorType - 1];

        }

        function _selectedKey(key) {
            vm.key = key;
        }

        function _clearSponsor() {
            vm.sponsor = null;
            $scope.content = "";
            $scope.$fileContent = "";
            document.getElementById("addSponsorForm").reset();

        }
        function _addValues() {
            vm.add = true;
            vm.edit = false;
            vm.view = false;
            vm.headerModal = "Add Sponsor";
           
            _clearSponsor();
            vm.TYPE = vm.sponsorsTypeList[0];
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
            vm.TYPE = vm.sponsorsTypeList[vm.sponsor.sponsorType - 1];
           


        }
        function _complementaryValues(sponsor) {
            vm.complementaryview = true;
            vm.sponsor = sponsor;
            _getSponsorComplementaryKeys();

        }

        function _submitForm() {

            // check to make sure the form is completely valid


        };
        function _downloadLogo() {
            window.open(vm.sponsor.logo);

        }


        //--------------------------Complemetnary----------------------------------------
        function _getSponsorComplementaryKeys() {
            vm.loadingComp = true;
            restApi.getSponsorComplementaryKeys(vm.sponsor.sponsorID).
                   success(function (data, status, headers, config) {
                       vm.sponsorKeys = data;
                       vm.loadingComp = false;
                   }).
                   error(function (data, status, headers, config) {
                       vm.loadingComp = false;
                       alert("add un aler sexy");

                   });
        }
        function _deleteComplemetaryKey() {

            vm.loadingRemovingComp = true;
            restApi.deleteComplemetaryKey(vm.key.complementarykeyID)
            .success(function (data, status, headers, config) {
                vm.sponsorKeys.forEach(function (key, index) {
                    if (key.complementarykeyID == vm.key.complementarykeyID) {
                        vm.sponsorKeys.splice(index, 1);
                        vm.loadingRemovingComp = false;
                        $('#delete').modal('hide');
                    }

                });

            })

            .error(function (data, status, headers, config) {
                alert("add un aler sexy");
                vm.loadingRemovingComp = false;
                $('#delete').modal('hide');
                _clearSponsor();

            });
        }

        function _deleteSponsorComplemetaryKey() {

            vm.loadingRemovingComp = true;
            restApi.deleteSponsorComplemetaryKey(vm.sponsor.sponsorID)
            .success(function (data, status, headers, config) {
                vm.sponsorKeys = data;
                vm.loadingRemovingComp = false;
            })

            .error(function (data, status, headers, config) {
                alert("add un aler sexy");
                vm.loadingRemovingComp = false;


            });
        }
        function _addComplementaryKey() {
            vm.addComplementaryObj.sponsorID = vm.sponsor.sponsorID;
            vm.addComplementaryObj.quantity = vm.quantity;
            vm.addComplementaryObj.company = vm.sponsor.company;
            vm.uploadingComp = true;
            restApi.addComplementaryKey(vm.addComplementaryObj)
                     .success(function (data, status, headers, config) {
                         vm.sponsorKeys = data;
                         vm.uploadingComp = false;
                         vm.quantity = 0;

                     })

                     .error(function (error) {
                         vm.uploadingComp = false;
                         vm.quantity = 0;
                         alert("add un aler sexy");

                     });
        }

        //---------------------------Sponsor-------------------------------------------------
        function _addSponsor(File) {
            vm.sponsor.sponsorType = vm.TYPE.sponsortypeID;
            vm.sponsor.typeName = vm.TYPE.name;
            if (File != undefined) {
                vm.sponsor.logo = $scope.content;
                vm.sponsor.logoName = File.name;
                File = null;

            }
            else {
                vm.sponsor.logo = "";
                vm.sponsor.logoName = "Empty";
            }
            vm.loadingUploading = true;
            restApi.postNewSponsor(vm.sponsor)
                     .success(function (data, status, headers, config) {
                         vm.sponsorsList.push(data);
                         vm.loadingUploading = false;
                         $('#addSponsor').modal('hide');
                         _clearSponsor();


                     })

                     .error(function (error) {
                         vm.loadingUploading = false;
                         $('#addSponsor').modal('hide');
                         alert("add un aler sexy");
                         _clearSponsor();
                     });
        }

        function _getSponsorTypes() {
            restApi.getSponsorTypesList().
                   success(function (data, status, headers, config) {
                       vm.sponsorsTypeList = data;
                       if (data != null)
                           vm.TYPE = vm.sponsorsTypeList[0];
                   }).
                   error(function (data, status, headers, config) {
                       alert("add un aler sexy");

                   });
        }

        function _getSponsors() {
            restApi.getSponsors().
                   success(function (data, status, headers, config) {
                       vm.sponsorsList = data;
                       vm.loading = false;

                   }).
                   error(function (data, status, headers, config) {
                       alert("add un aler sexy");
                       vm.loading = false;
                   });
        }


        function _updateSponsor(myFile) {

            vm.sponsor.sponsorType = vm.TYPE.sponsortypeID;
            vm.sponsor.typeName = vm.TYPE.name;

            if (myFile != undefined) {
                vm.sponsor.logoName = myFile.name;
                vm.sponsor.logo = $scope.content;

            }

            vm.loadingUploading = true;
            restApi.updateSponsor(vm.sponsor)
            .success(function (data, status, headers, config) {
                vm.sponsorsList.forEach(function (sponsor, index) {
                    if (sponsor.sponsorID == vm.sponsor.sponsorID) {
                        vm.sponsorsList[index] = JSON.parse(JSON.stringify(vm.sponsor));
                    }
                    vm.loadingUploading = false;
                    $('#addSponsor').modal('hide');

                }); _clearSponsor();
            }

            )
            .error(function (data, status, headers, config) {
                vm.edit = false;
                _clearSponsor();
                vm.loadingUploading = false;
                $('#addSponsor').modal('hide');
                alert("add un aler sexy");
                _clearSponsor();
            });


        }
        function _deleteSponsor() {
            vm.loadingRemoving = true;
            restApi.deleteSponsor(vm.sponsor.sponsorID)
            .success(function (data, status, headers, config) {
                vm.sponsorsList.forEach(function (sponsor, index) {
                    if (sponsor.sponsorID == vm.sponsor.sponsorID) {
                        vm.sponsorsList.splice(index, 1);

                    }

                });
                vm.loadingRemoving = false;
                $('#delete').modal('hide');
            })

            .error(function (data, status, headers, config) {
                alert("add un aler sexy");
                vm.loadingRemoving = false;
                $('#delete').modal('hide');
                _clearSponsor();

            });
        }





    }
})();





