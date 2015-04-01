(function () {
    'use strict';

    var controllerId = 'layoutCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http','$window','$location', layoutCtrl]);

    function layoutCtrl($scope, $http, $window, $location) {
        
        var vm = this;

        vm.activate = activate;
        vm.title = 'layoutCtrl';
        vm.tabViewControl = _tabViewControl;
      
        activate();

        function activate() {
          
            _tabViewControl();
          

        }
        

        function _tabViewControl() {
            
            
            if ($window.sessionStorage.length != 0) {
               
                var list = JSON.parse(sessionStorage.getItem('claims'));
                list.forEach(function (claim) {

                    if (claim.localeCompare('admin') == 0 || claim.localeCompare('master') == 0 ||
                        claim.localeCompare('adminFinance') == 0 || claim.localeCompare('adminCommittee') == 0) {
                       
                    }
                
                });
            }
            else { vm.loged = false;}


        };

      

    }
})();
