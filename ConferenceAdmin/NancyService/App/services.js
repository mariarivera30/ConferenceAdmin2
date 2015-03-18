(function () {
    'use strict';

    var serviceId = 'restApi';

    // TODO: replace app with your module name
    angular.module('app').factory(serviceId, ['$http', addrestApi]);

    function addrestApi($http) {
        var service = {
            getSponsorTypesList:_getSponsorTypesList,
            updateSponsor:_updateSponsor,
            login: _login,
            postNewSponsor: _postNewSponsor,
            postUser: _postUser,
            getSponsors: _getSponsors,
            postNewTopic: _postNewTopic,
            getTopics: _getTopics,
            deleteTopic: _deleteTopic,
            updateTopic: _updateTopic,
        };

        return service;
        function _updateSponsor(data) {
            return $http.put('/admin/updateSponsor',data);
        };

        function _getSponsorTypesList() {
            return $http.get('/admin/getSponsorTypesList');
        };

        function _login(data) {
            return $http.post('/auth/login', { email: data.email, password: data.password });
        };

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

        function _postNewTopic(data) {
            return $http.post('/admin/addTopic', {
                name: data,
            });
        };

        function _getTopics() {
            return $http.get('/admin/getTopic');
        };

        function _deleteTopic(data) {
            return $http.delete('/admin/deleteTopic/' + data);
        };

        function _updateTopic(data) {
            return $http.put('/admin/updateTopic', { topiccategoryID: data.topiccategoryID, name: data.name });
        };     
    }
}

)();

