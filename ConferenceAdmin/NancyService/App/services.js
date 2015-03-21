(function () {
    'use strict';

    var serviceId = 'restApi';

    // TODO: replace app with your module name
    angular.module('app').factory(serviceId, ['$http', addrestApi]);

    function addrestApi($http) {
        var service = {
            getSponsorTypesList:_getSponsorTypesList,
            updateSponsor: _updateSponsor,
            deleteSponsor: _deleteSponsor,
            login: _login,
            postNewSponsor: _postNewSponsor,
            postUser: _postUser,
            getSponsors: _getSponsors,
            postNewTopic: _postNewTopic,
            getTopics: _getTopics,
            deleteTopic: _deleteTopic,
            updateTopic: _updateTopic,
            getRegistrations: _getRegistrations,
            postNewRegistration: _postNewRegistration,
            updateRegistration: _updateRegistration,
            deleteRegistration: _deleteRegistration,
            getGuestList: _getGuestList,
            updateAcceptanceStatus: _updateAcceptanceStatus,
            displayAuthorizations: _displayAuthorizations,
            rejectRegisteredGuest: _rejectRegisteredGuest
        };

        return service;
        function _updateSponsor(data) {
            return $http.put('/admin/updateSponsor',data);
        };
        function _deleteSponsor(data) {
            return $http.put('/admin/deleteSponsor/' , data);
        };

        function _getSponsorTypesList() {
            return $http.get('/admin/getSponsorTypesList');
        };

        function _login(data) {
            return $http.post('/auth/login', { email: data.email, password: data.password });
        };

        function _postNewSponsor(data) {
            return $http.post('/admin/addsponsor', data)
               
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
            return $http.put('/admin/deleteTopic/' + data);
        };

        function _updateTopic(data) {
            return $http.put('/admin/updateTopic', { topiccategoryID: data.topiccategoryID, name: data.name });
        };

        function _getRegistrations() {
            return $http.get('/admin/getRegistrations');
        };

        function _updateRegistration(data) {
            return $http.put('/admin/updateRegistration', { registrationID: data.registrationID, firstname: data.firstname });
        };

        function _deleteRegistration(data) {
            return $http.delete('/admin/deleteRegistration/' + data);
        };

        function _postNewRegistration(data) {
            return $http.post('/admin/addRegistration', data);
        };
        //-----------------------------------GUESTS-----------------------------------
        //get guest list for admin
        function _getGuestList() {
            return $http.get('admin/getGuestList');
        };

        //update guest acceptance status
        function _updateAcceptanceStatus(data) {
            return $http.put('admin/updateAcceptanceStatus', { id: data.ID, status: data.status })
        };

        //get minor authorizations
        function _displayAuthorizations(data) {
            return $http.get('admin/displayAuthorizations/' + data);
        };

        //Change registration status to Rejected
        function _rejectRegisteredGuest(data) {
            return $http.put('admin/rejectRegisteredGuest/' + data);
        };
    }
}

)();

