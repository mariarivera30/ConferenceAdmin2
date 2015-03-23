(function () {
    'use strict';

    var controllerId = 'layoutCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http','$window', layoutCtrl]);

    function layoutCtrl($scope, $http, $window) {
        
        var vm = this;

        vm.activate = activate;
        vm.title = 'layoutCtrl';
        
        activate();

        function activate() {
          
            

        }

      

    }
})();
