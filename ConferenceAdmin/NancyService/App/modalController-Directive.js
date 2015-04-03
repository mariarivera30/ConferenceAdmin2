(function () {
    angular.module('app')
        .directive('modal', ModalNobuttonDirective);

    function ModalNobuttonDirective() {
        var directive = {
            restrict: 'E',
            templateUrl: 'Views/Modal.html',
          
            scope: {
                source: '=',
                visible: '=',
                okcallback: '&',
                cancecallback: '&',
                okbutton: '@',
                okbuttonText: '@' ,
                cancelbutton: '@' ,
                cancelbuttoText: '@',

            },
            controller: modalController,
            controllerAs: 'vm',
            bindToController: true,
            link: watcher
        };

        return directive;
    };

    modalController.$inject = ['$scope']

    function modalController($scope) {

        /* jshint validthis: true */
        var vm = this;

    }

    function watcher(scope, element, attrs) {
    

        scope.cancelfunc = function () {
            scope.vm.cancecallback();
            $("#modal").modal('hide');
        };

        scope.okfunc = function () {
            scope.vm.okcallback();
            $("#modal").modal('hide');
        };

        scope.$watch("vm.visible", function () {
            if (scope.vm.visible) {
                $("#modal").modal('show');
            };
        });
    }
})();





