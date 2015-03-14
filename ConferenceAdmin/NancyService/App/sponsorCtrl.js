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
        vm.email ;
        vm.phone;
        vm.sponsorType;
        vm.firstName;
        vm.lastName;
        vm.titleSponsor;
        vm.company;
        vm.logo;
        vm.sponsorsList = {};
        // Functions
        vm.addSponsor = _addSponsor;
        vm.getSponsors = _getSponsors;

        
       
        _getSponsors();

        
                  
            // Functions
            function activate() {
                vm.email = "";
                vm.phone = "";
                vm.firstName = "";
            }
            
            function _addSponsor() {
                restApi.postNewSponsor(vm)
                    .success(function (data, status, headers, config) {
                        vm.sponsorsList.push(vm.firstName);
                        activate();
                         })

                    .error(function (error) {
                
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

           


        }
    })();


