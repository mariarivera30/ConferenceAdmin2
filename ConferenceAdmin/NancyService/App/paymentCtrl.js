(function () {
    'use strict';

    var controllerId = 'paymentCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', paymentCtrl]);

    function paymentCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'paymentCtrl';

        function activate() {

        }
        $(function () {
            $('#homeCarousel').carousel({
                interval: 2000,
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
