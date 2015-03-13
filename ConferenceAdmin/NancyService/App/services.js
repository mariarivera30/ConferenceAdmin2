(function () {
    'use strict';

    var serviceId = 'restApi';

    // TODO: replace app with your module name
    angular.module('app').factory(serviceId, ['$http', addrestApi]);

    function addrestApi($http) {
        var service = {
            postNewSponsor: _postNewSponsor,
            postUser: _postUser,
            getSponsors: _getSponsors,
          

        };

        return service;

        function _postNewSponsor(data) {
            return $http.post('/admin/addsponsor', {
                sponsorType: 1, firstName: data.firstName,
                lastName: data.lastName, title: data.titleSponsor, company: data.company, logo: data.logo, email: data.email, phone: data.phone, addressID: 1
            });
        };

        function _getSponsors() {
            return $http.get('/admin/getSponsor');
        };

        function _postUser(newUser) {
            return $http.post('/addNewUser', { email: newUser.email, password: newUser.password }).
                 success(function (data, status, headers, config) {
                     // this callback will be called asynchronously
                     // when the response is available
                 }).
                 error(function (data, status, headers, config) {
                     // called asynchronously if an error occurs
                     // or server returns response with an error status.
                 });
        };

      
    }


}

)();

