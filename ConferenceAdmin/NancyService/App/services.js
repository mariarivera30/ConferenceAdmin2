(function () {
    'use strict';

    var serviceId = 'restApi';

    // TODO: replace app with your module name
    angular.module('app').factory(serviceId, ['$http', addrestApi]);

    function addrestApi($http) {
        var service = {
            deleteSponsor : _deleteSponsor,
            getSponsorTypesList:_getSponsorTypesList,
            updateSponsor: _updateSponsor,
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
            getUserTypes: _getUserTypes,
            getGuestList: _getGuestList,
            updateAcceptanceStatus: _updateAcceptanceStatus,
            displayAuthorizations: _displayAuthorizations,
            rejectRegisteredGuest: _rejectRegisteredGuest,
            getAdministrators: _getAdministrators,
            deleteAdmin: _deleteAdmin,
            editAdmin: _editAdmin,
            postNewAdmin: _postNewAdmin,
            getPrivilegesList: _getPrivilegesList,
            getProfileInfo: _getProfileInfo,
            updateProfileInfo: _updateProfileInfo,
            apply: _apply,
            makePayment: _makePayment,
            getAssignedSubmissions: _getAssignedSubmissions,
            getSubmission: _getSubmission,
            postEvaluation: _postEvaluation,
            editEvaluation: _editEvaluation,
            uploadDocument: _uploadDocument,
            getTemplates: _getTemplates,
            getDocuments: _getDocuments,
            deleteDocument: _deleteDocument,
            selectCompanion: _selectCompanion,
            getCompanionKey: _getCompanionKey,
            getNewAdmin: _getNewAdmin,
            getEvaluatorList: _getEvaluatorList,
            updateEvaluatorAcceptanceStatus: _updateEvaluatorAcceptanceStatus,
            getNewEvaluator: _getNewEvaluator,
            postNewEvaluator: _postNewEvaluator,
            getHome: _getHome,
            saveHome: _saveHome,
            removeImage: _removeImage,
            getVenue: _getVenue,
            saveVenue: _saveVenue,
            getContact: _getContact,
            saveContact: _saveContact,
            getParticipation: _getParticipation,
            saveParticipation: _saveParticipation,
            saveRegistrationInfo: _saveRegistrationInfo,
            getRegistrationDetails: _getRegistrationDetails,
            getDeadlines: _getDeadlines,
            saveDeadlines: _saveDeadlines,
            getPlanningCommittee: _getPlanningCommittee,
            postNewCommittee: _postNewCommittee,
            editCommittee: _editCommittee,
            deleteCommittee: _deleteCommittee,
            getCommitteeInterface: _getCommitteeInterface,
            getAdminSponsorBenefits: _getAdminSponsorBenefits,
            saveAdminSponsorBenefits: _saveAdminSponsorBenefits,
            saveInstructions: _saveInstructions,
            getInstructions: _getInstructions,
            getAllSponsorBenefits: _getAllSponsorBenefits,
            getGeneralInfo: _getGeneralInfo,
            saveGeneralInfo: _saveGeneralInfo,
            getPendingList: _getPendingList
        };

        return service;
        function _updateSponsor(data) {
            return $http.put('/admin/updateSponsor', data);
        };
        function _deleteSponsor(data) {
            return $http.put('/admin/deleteSponsor/', data);
        };

        function _getSponsorTypesList() {
            return $http.get('/admin/getSponsorTypesList');
        };

        function _postNewSponsor(data) {
            return $http.post('/admin/addsponsor', data)

        };

        function _login(data) {
            return $http.post('/auth/login', { email: data.email, password: data.password });
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

        //-----------------------------------TOPICS-----------------------------------

        function _getTopics() {
            return $http.get('/admin/getTopic');
        };

        function _postNewTopic(data) {
            return $http.post('/admin/addTopic', {
                name: data,
            });
        };

        function _updateTopic(data) {
            return $http.put('/admin/updateTopic', { topiccategoryID: data.topiccategoryID, name: data.name });
        };

        function _deleteTopic(data) {
            return $http.put('/admin/deleteTopic/' + data);
        };

        //-----------------------------------ADMINISTRATORS-----------------------------------

        function _getAdministrators() {
            return $http.get('/admin/getAdministrators');
        };

        function _getNewAdmin(email) {
            return $http.get('/admin/getNewAdmin/' + email);
        };

        function _getPrivilegesList() {
            return $http.get('/admin/getPrivilegesList');
        };

        function _postNewAdmin(data) {
            return $http.post('/admin/addAdmin', {
                firstName: data.firstName,
                lastName: data.lastName,
                email: data.email,
                privilegeID: data.privilegeID,
            });
        };

        function _editAdmin(id, privilegeID, oldPrivilegeID) {
            return $http.put('/admin/editAdmin/', { userID: id, privilegeID: privilegeID, oldPrivilegeID: oldPrivilegeID });
        };

        function _deleteAdmin(userid, privilegeid) {
            return $http.put('/admin/deleteAdmin/', {
                userID: userid,
                privilegeID: privilegeid
            });
        };

        //-----------------------------------REGISTRATIONS-----------------------------------

        function _getRegistrations() {
            return $http.get('/admin/getRegistrations');
        };

        function _getUserTypes() {
            return $http.get('/admin/getUserTypes');
        };

        function _updateRegistration(data) {
            return $http.put('/admin/updateRegistration', { registrationID: data.registrationID, firstname: data.firstname, lastname: data.lastname, usertypeid: data.usertypeid, affiliationName: data.affiliationName, date1: data.date1, date2: data.date2, date3: data.date3 });
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

        //-----------------------------------PROFILE-INFO-----------------------------------
        function _getProfileInfo(data) {
            return $http.get('profile/getProfileInfo/' + data);
        };

        function _updateProfileInfo(data) {
            return $http.put('profile/updateProfileInfo/', data);
        };

        function _apply(data) {
            return $http.put('profile/apply/', data);
        };
        
        function _makePayment(data) {
            return $http.put('profile/makePayment/', data);
        };

        //---------------------------------PROFILE-SUBMISSIONS---------------------------
        //get list of submissions assigned to the evalutor currently logged in
        function _getAssignedSubmissions(data) {
            return $http.get('profile/getAssignedSubmissions/' + data);
        };
        //get details of submission with ID submissionID
        function _getSubmission(data) {
            return $http.get('profile/getSubmission/' + data);
        };
        //add new evalution for a submission
        function _postEvaluation(data) {
            return $http.post('profile/addEvaluation', data);
        };
        //edit evaluation for a submission
        function _editEvaluation(data) {
            return $http.put('profile/editEvaluation', data)
        };

        //-----------------------------------PROFILE-AUTHORIZATION-----------------------------------
        function _uploadDocument(data) {
            return $http.put('/profile/uploadDocument/', data);
        };

        function _getTemplates() {
            return $http.get('profile/getTemplates');
        };

        function _getDocuments(data) {
            return $http.get('profile/getDocuments/' + data);
        };

        function _deleteDocument(data) {
            return $http.put('profile/deleteDocument/', data);
        };

        function _selectCompanion(data) {
            return $http.post('profile/selectCompanion/', data);
        };

        function _getCompanionKey(data) {
            return $http.get('profile/getCompanionKey/' + data);
        };

        //-----------------------------------EVALUATORS---------------------------------

        function _getEvaluatorList() {
            return $http.get('admin/getEvaluatorList');
        };

        function _getPendingList() {
            return $http.get('admin/getPendingList');
        };

        function _getNewEvaluator(email) {
            return $http.get('/admin/getNewEvaluator/' + email);
        };

        function _postNewEvaluator(email) {
            return $http.post('/admin/addEvaluator/' + email);
        };

        function _updateEvaluatorAcceptanceStatus(data) {
            return $http.put('admin/updateEvaluatorAcceptanceStatus', { userID: data.userID, acceptanceStatus: data.acceptanceStatus })
        };

        //---------------------------------WEBSITE CONTENT----------------------------------------------

        function _getHome() {
            return $http.get('/admin/getHome');
        };

        function _saveHome(data) {
            return $http.put('/admin/saveHome', data);
        };

        function _removeImage(data) {
            return $http.put('admin/removeImage/' + data);
        };

        function _getVenue() {
            return $http.get('/admin/getVenue');
        };

        function _saveVenue(data) {
            return $http.put('/admin/saveVenue', data);
        };

        function _getContact() {
            return $http.get('/admin/getContact');
        };

        function _saveContact(data) {
            return $http.put('/admin/saveContact', data);
        };

        function _getParticipation() {
            return $http.get('/admin/getParticipation');
        };

        function _saveParticipation(data) {
            return $http.put('/admin/saveParticipation', data);
        };

        function _saveRegistrationInfo(data) {
            return $http.put('/admin/saveRegistrationInfo', data);
        };

        function _getRegistrationDetails() {
            return $http.get('/admin/getRegistrationInfo');
        };

        function _getDeadlines() {
            return $http.get('/admin/getDeadlines');
        };

        function _saveDeadlines(data) {
            return $http.put('/admin/saveDeadlines', data);
        };

        function _getPlanningCommittee() {
            return $http.get('admin/getPlanningCommittee');
        };

        function _postNewCommittee(data) {
            return $http.post('/admin/addNewCommittee', data);
        };

        function _editCommittee(data) {
            return $http.put('/admin/editCommittee', data);
        };

        function _deleteCommittee(data) {
            return $http.put('/admin/deleteCommittee', data);
        };

        function _getCommitteeInterface() {
            return $http.get('admin/getCommitteeInterface');
        };

        function _getAdminSponsorBenefits(data) {
            return $http.get('admin/getAdminSponsorBenefits/' + data);
        };

        function _saveAdminSponsorBenefits(data) {
            return $http.put('/admin/saveAdminSponsorBenefits', data);
        };

        function _saveInstructions(data) {
            return $http.put('/admin/saveInstructions/' + data);
        };

        function _getInstructions() {
            return $http.get('admin/getSponsorInstructions');
        };

        function _getAllSponsorBenefits() {
            return $http.get('admin/getAllSponsorBenefits');
        };

        function _getGeneralInfo() {
            return $http.get('/admin/getGeneralInfo');
        };

        function _saveGeneralInfo(data) {
            return $http.put('/admin/saveGeneralInfo', data);
        };
    }
}

)();

