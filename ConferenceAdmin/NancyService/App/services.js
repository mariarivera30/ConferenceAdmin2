(function () {
    'use strict';

    var serviceId = 'restApi';

    // TODO: replace app with your module name
    angular.module('app').factory(serviceId, ['$http', addrestApi]);

    function addrestApi($http) {
        var service = {
           // getUsers: getUsers
           

        };

        return service;


      /*  function getUsers() {
            return $http.get('/allUsers');
        };*/


    }


}

)();

