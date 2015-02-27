(function () {
    'use strict';

    var controllerId = 'sponsorCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', sponsorCtrl]);

    function sponsorCtrl($scope, $http) {
            var vm = this;
            vm.list = [{
                name: "Randy Soto",
                email: "randy.soto@upr.edu",
                key: 245646545
               
            },  {
                name: "Juan Retrolla",
                email: "juan.retorlla@upr.edu",
                key: 245646546
            }, 
            {
                name: "unused",
                email: "unused",
                key:245646547
            },
            {
                name: "unused",
                email: "unused",
                key: 245646548
            }
            ];


            vm.activate = activate;
            vm.title = 'administratorCtrl';
            // Functions



            function activate() {

            }


        }
    })();


