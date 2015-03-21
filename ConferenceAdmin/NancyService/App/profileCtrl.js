(function () {
    'use strict';

    var controllerId = 'profileCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', '$window', '$location', profileCtrl]);

    function profileCtrl($scope, $http, restApi, $window, $location) {
        var vm = this;
        //Website content tabs
        vm.activate = activate;
        vm.generalInfo = false;
        vm.application = false;
        vm.submission = false;
        vm.authorization = false;
        vm.receipt = false;
        vm.evaluation = false;
        

        // Functions
        vm.tabViewControl = _tabViewControl;
        activate();
        function activate() {
        //    _tabViewControl();
            
        }

        function _tabViewControl() {
            var list = JSON.parse(sessionStorage.getItem('claims'));
           
                list.forEach(function (claim) {


                    if (claim.localeCompare('minor') == 0) {
                        vm.generalInfo = true;
                        vm.application = true;
                        vm.submission = false;
                        vm.authorization = true;
                        vm.receipt = true;
                        vm.evaluation = false;

                    }
                    if (claim.localeCompare('participant') == 0) {
                        vm.generalInfo = true;
                        vm.application = false;
                        vm.submission = true;
                        vm.authorization = false;
                        vm.receipt = true;
                        vm.evaluation = false;
                       
                    }

                    if (claim.localeCompare('companion') == 0) {
                        vm.generalInfo = true;
                        vm.application = true;
                        
                    }
                    if (claim.localeCompare('evaluator') == 0) {
                        vm.evaluation = true;
                    }
                    //Esto lo puedo quitar cuando se de unable al button de Profile.
                    if (claim.localeCompare('adminFinance') == 0 || claim.localeCompare('adminCommittee') == 0 )  {
                        vm.generalInfo = false;
                        vm.application = false;
                        vm.receipt = false;
                        vm.authorization = false;
                        vm.evaluation = false;
                        vm.submission = false;

                    }
                });

            

            };






        }
    }
)();
