(function () {
    'use strict';

    var controllerId = 'participationCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', participationCtrl]);

    function participationCtrl($scope, $http) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'participationCtrl';
        vm.list = [{
            name: "Poster Instructions",
            description: "Consider attending the first day of the conference where activities for female High School students will take place (hands on activities, presentations on computing, talk to females studying computing, attend the Puerto Rico Aspirations in Computing Award ceremony). Every high school student must be accompanied by a responsible adult (21 years or older).",
            elegibility: "Must be undergraduate or graduate student",
        },         {
            name: "Paper Instructions",
            description: "Consider attending the first day of the conference where activities for female High School students will take place (hands on activities, presentations on computing, talk to females studying computing, attend the Puerto Rico Aspirations in Computing Award ceremony). Every high school student must be accompanied by a responsible adult (21 years or older).",
            elegibility: "Must be undergraduate or graduate student",
        },
        {
            name: "BoF Instructions",
            description: "Consider attending the first day of the conference where activities for female High School students will take place (hands on activities, presentations on computing, talk to females studying computing, attend the Puerto Rico Aspirations in Computing Award ceremony). Every high school student must be accompanied by a responsible adult (21 years or older).",
            elegibility: "Must be undergraduate or graduate student",
        },
        {
            name: "Workshop Instructions",
            description: "Consider attending the first day of the conference where activities for female High School students will take place (hands on activities, presentations on computing, talk to females studying computing, attend the Puerto Rico Aspirations in Computing Award ceremony). Every high school student must be accompanied by a responsible adult (21 years or older).",
            elegibility: "Must be undergraduate or graduate student",
        }];
        function activate() {

        }


    }
})();
