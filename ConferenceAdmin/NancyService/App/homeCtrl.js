(function () {
    'use strict';

    var controllerId = 'homeCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http','restApi', homeCtrl]);

    function homeCtrl($scope, $http) {
        var vm = this;
        vm.home = {
            p1: "Welcome to the First Caribbean Celebration of Women in Computing (CCWiC 2014). Students from High School and Universities are all welcome. Also, we welcome participants from industry and academia. CCWiC 2014 is a bilingual conference.",
            p2: "The Caribbean Celebration of Women in Computing (CCWiC) 2014 promotes diversity with efforts that address the decline of women who choose computing related professions. CCWiC 2014 offers opportunities for mentoring, networking, and technical/career development to women in computing. We invite your participate and promote diversity in computing related fields. CCWiC 2014 is part of the Grace Hopper Celebration of Women in Computing Regional Consortium and the 2012 Grace Hopper Celebration attracted over 3,500 attendees from 41 countries. CCWiC 2014 will be held in Puerto Rico on February 26-27, 2014.",
            news1Title: "CCWIC",
            news1: "Meet women in computing, learn about computing as a career, expose you research, and meet and mentor the next generation of computing professionals. We welcome participants from industry, academia, High School, and Universities from all over the Caribbean."
        };
       
                    


        vm.activate = activate;
        vm.title = 'homeCtrl';
        vm.news =
                  {
                      tittle: "CCWIC",
                      description: "Meet women in computing, learn about computing as a career, expose you research, and meet and mentor the next generation of computing professionals. We welcome participants from industry, academia, High School, and Universities from all over the Caribbean",
                      name: "Maria Rivera",
                      date: "January 20, 2014"
                  };
              

        

        function activate() {
           
        }


    }
})();
