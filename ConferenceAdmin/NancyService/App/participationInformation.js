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
            name: "University Student (Undergraduate & Graduate)",
            description: "Poster Instructions",
            date1: "2/12/2015",
            date2: "2/13/2015",
            registrationPrice: "$25.00",
            withoutFee: "$25.00",
            withFee: "35.00"
        },         {
            name: "Professional - Industry",
            description: "Consider attending the first day of the conference where activities for female High School students will take place (hands on activities, presentations on computing, talk to females studying computing, attend the Puerto Rico Aspirations in Computing Award ceremony). Every high school student must be accompanied by a responsible adult (21 years or older).",
            date1: "2/12/2015",
            date2: "2/13/2015",
            registrationPrice: "$25.00",
            withoutFee: "$25.00",
            withFee: "35.00"
        },
        {
            name: "Professional - Academy",
            description: "Consider attending the first day of the conference where activities for female High School students will take place (hands on activities, presentations on computing, talk to females studying computing, attend the Puerto Rico Aspirations in Computing Award ceremony). Every high school student must be accompanied by a responsible adult (21 years or older).",
            date1: "2/12/2015",
            date2: "2/13/2015",
            registrationPrice: "$25.00",
            withoutFee: "$25.00",
            withFee: "35.00"
        },
        {
            name: "High School Student",
            description: "Consider attending the first day of the conference where activities for female High School students will take place (hands on activities, presentations on computing, talk to females studying computing, attend the Puerto Rico Aspirations in Computing Award ceremony). Every high school student must be accompanied by a responsible adult (21 years or older).",
            date1: "2/12/2015",
            date2: "2/13/2015",
            registrationPrice: "$25.00",
            withoutFee: "$25.00",
            withFee:"35.00"
        }, {
            name: "High School Student- Companion",
            description: "Consider attending the first day of the conference where activities for female High School students will take place (hands on activities, presentations on computing, talk to females studying computing, attend the Puerto Rico Aspirations in Computing Award ceremony). Every high school student must be accompanied by a responsible adult (21 years or older).",
            date1: "2/12/2015",
            date2: "2/13/2015",
            registrationPrice: "$25.00",
            withoutFee: "$25.00",
            withFee: "35.00"
        }];
        function activate() {

        }


    }
})();
