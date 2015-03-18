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
        vm.selectedSponsorUpdate = _selectedSponsorUpdate;
        vm.selectedSponsorDelete = _selectedSponsorDelete;
        vm.clearSponsor = _clearSponsor;
        vm.updateSponsor = _updateSponsor;
        activate();
        
                  
            // Functions
            function activate() {
                _getSponsors();
                _getSponsorTypes();
            }

            function _selectedSponsorUpdate(sponsor) {
               vm.sponsor = sponsor;
              
            }
            function _clearSponsor() {
                vm.sponsor = null;

            }

            function _selectedSponsorDelete(id) {
                vm.currentid = id;
            }
            
            function _addSponsor() {
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

            function _updateSponsor() {
                if (true) {
             
                    restApi.updateSponsor(vm.sponsor)
                    .success(function (data, status, headers, config) {
                        vm.sponsorsList.forEach(function (sponsor) {
                            if (sponsor.sponsorID == vm.sponsor.sponsorID) {
                                sponsor = vm.sponsor;
                            }
                            vm.sponsor = {};
                        });
                    })
                    .error(function (data, status, headers, config) {
                    });
                }
                else {
                    alert("You must provide a valid name.");
                }
              
            }

           


        }
    })();


