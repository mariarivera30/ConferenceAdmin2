(function () {
    'use strict';

    var serviceId = 'restApi';

    // TODO: replace app with your module name
    angular.module('app').factory(serviceId, ['$http', addrestApi]);

    function addrestApi($http) {
        var service = {
            changePassword: _changePassword,
            checkEmail: _checkEmail,
            requestPass: _requestPass,
            accountConfirmation: _accountConfirmation,
            createUser: _createUser,
            deleteSponsorComplemetaryKey: _deleteSponsorComplemetaryKey,
            deleteComplemetaryKey: _deleteComplemetaryKey,
            addComplementaryKey: _addComplementaryKey,
            getSponsorComplementaryKeys: _getSponsorComplementaryKeys,
            getComplemetnaryKeys: _getComplemetnaryKeys,
            deleteAuthTemplate: _deleteAuthTemplate,
            getAuthTemplatesAdmin: _getAuthTemplatesAdmin,
            updateAuthTemplate: _updateAuthTemplate,
            addAuthTemplate: _addAuthTemplate,
            deleteTemplate: _deleteTemplate,
            getTemplatesAdmin: _getTemplatesAdmin,
            updateTemplate: _updateTemplate,
            addTemplate: _addTemplate,
            getSponsorbyID: _getSponsorbyID,
            deleteSponsor: _deleteSponsor,
            getSponsorTypesList: _getSponsorTypesList,
            updateSponsor: _updateSponsor,
            login: _login,
            postNewSponsor: _postNewSponsor,
            postNewTopic: _postNewTopic,
            getSponsors: _getSponsors,
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
            complementaryPayment: _complementaryPayment,
            getAssignedSubmissions: _getAssignedSubmissions,
            getSubmissionDetails: _getSubmissionDetails,
            postEvaluation: _postEvaluation,
            editEvaluation: _editEvaluation,
            uploadDocument: _uploadDocument,
            getTemplates: _getTemplates,
            getDocuments: _getDocuments,
            deleteDocument: _deleteDocument,
            selectCompanion: _selectCompanion,
            getCompanionKey: _getCompanionKey,
            checkComplementaryKey: _checkComplementaryKey,
            getNewAdmin: _getNewAdmin,
            getEvaluatorList: _getEvaluatorList,
            updateEvaluatorAcceptanceStatus: _updateEvaluatorAcceptanceStatus,
            getNewEvaluator: _getNewEvaluator,
            postNewEvaluator: _postNewEvaluator,
            getHome: _getHome,
            saveHome: _saveHome,
            removeFile: _removeFile,
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
            getPendingList: _getPendingList,
            getUserSubmissionList: _getUserSubmissionList,
            getUserSubmission: _getUserSubmission,
            getSubmissionTypes: _getSubmissionTypes,
            deleteSubmission: _deleteSubmission,
            postSubmission: _postSubmission,
            editSubmission: _editSubmission,
            getProgram: _getProgram,
            saveProgram: _saveProgram,
            getProgramDocument: _getProgramDocument,
            getAbstractDocument: _getAbstractDocument,
            getDates: _getDates,
            postFinalSubmission: _postFinalSubmission,
            getAllSubmissions: _getAllSubmissions,
            getBillReport: _getBillReport,
            getRegistrationPaymentsFromIndex: _getRegistrationPaymentsFromIndex,
            getSponsorPaymentsFromIndex: _getSponsorPaymentsFromIndex,
            getEvaluationsForSubmission: _getEvaluationsForSubmission,
            getSubmissionDeadline: _getSubmissionDeadline,
            getAllEvaluators: _getAllEvaluators,
            assignEvaluator: _assignEvaluator
        };

        return service;
        //-----------------------------------Sponsor Complementary-----------------------------------


        function _deleteSponsorComplemetaryKey(data) {
            return $http.put('/admin/deleteSponsorComplementaryKey', data);
        };

        function _deleteComplemetaryKey(data) {
            return $http.put('/admin/deleteComplementaryKey', data);
        };

        function _getComplemetnaryKeys() {
            return $http.get('/admin/getComplementaryKeys');
        };

        function _addComplementaryKey(data) {
            return $http.post('/admin/addSponsorComplementaryKeys', data)

        };
        function _getSponsorComplementaryKeys(data) {
            return $http.get('/admin/getSponsorComplementaryKeys/' + data);
        };

        //-----------------------------------Sponsor-----------------------------------
        function _updateSponsor(data) {
            return $http.put('/admin/updateSponsor', data);
        };
        function _deleteSponsor(data) {
            return $http.put('/admin/deleteSponsor/', data);
        };

        function _getSponsorbyID(data) {
            return $http.get('/admin/getSponsorbyID/' + data);
        };
        function _getSponsorTypesList() {
            return $http.get('/admin/getSponsorTypesList');
        };

        function _postNewSponsor(data) {
            return $http.post('/admin/addsponsor', data)

        };
        function _getSponsors() {
            return $http.get('/admin/getSponsor');
        };

        //-----------------------------------Templates-------------------------------
        function _updateTemplate(data) {
            return $http.put('/admin/updateTemplate', data);
        };
        function _deleteTemplate(data) {
            return $http.put('/admin/deleteTemplate/', data);
        };

        function _getTemplatesAdmin() {
            return $http.get('/admin/getTemplatesAdmin');
        };

        function _addTemplate(data) {
            return $http.post('/admin/addTemplate', data)

        };

        //-----------------------------------Authorization Templates-------------------------------
        function _updateAuthTemplate(data) {
            return $http.put('/admin/updateAuthTemplate', data);
        };
        function _deleteAuthTemplate(data) {
            return $http.put('/admin/deleteAuthTemplate', data);
        };

        function _getAuthTemplatesAdmin() {
            return $http.get('/admin/getAuthTemplatesAdmin');
        };

        function _addAuthTemplate(data) {
            return $http.post('/admin/addAuthTemplate', data)

        };

        //-----------------------------------LOGIN-----------------------------------
        function _login(data) {
            return $http.post('/auth/login', { email: data.email, password: data.password });
        };
        //-----------------------------------SignUp-----------------------------------
        function _createUser(data) {
            return $http.post('/auth/createUser', data);
        };
        function _accountConfirmation(data) {
            return $http.get('/auth/accountConfirmation/' + data);
        };
        //----------------------------------RecoverPass--------------------------------
        function _checkEmail(data) {
            return $http.get('/auth/checkEmail/' + data);
        };
        function _requestPass(data) {
            return $http.get('/auth/requestPass/' + data);
        };
        function _changePassword(data) {
            return $http.post('/auth/changePassword/', data);
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
            return $http.put('/admin/updateRegistration', { registrationID: data.registrationID, firstname: data.firstname, lastname: data.lastname, usertypeid: data.usertypeid, affiliationName: data.affiliationName, date1: data.date1, date2: data.date2, date3: data.date3, notes: data.notes });
        };

        function _deleteRegistration(data) {
            return $http.delete('/admin/deleteRegistration/' + data);
        };

        function _postNewRegistration(data) {
            return $http.post('/admin/addRegistration', data);
        };

        function _getDates() {
            return $http.get('/admin/getDates');
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

        function _complementaryPayment(data) {
            return $http.put('profile/complementaryPayment/', data);
        };

        function _checkComplementaryKey(data) {
            return $http.get('profile/checkComplementaryKey/' + data);
        };

        //---------------------------------PROFILE-SUBMISSIONS---------------------------
        //get list of submissions assigned to the evalutor currently logged in
        function _getAssignedSubmissions(data) {
            return $http.get('profile/getAssignedSubmissions/' + data);
        };
        //get details of submission with ID submissionID
        function _getSubmissionDetails(data) {
            return $http.get('profile/getSubmission/' + data.submissionID + '/' + data.evaluatorID);
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

        function _removeFile(data) {
            return $http.put('/admin/removeFile/' + data);
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

        function _getProgram() {
            return $http.get('/admin/getProgram');
        };

        function _getAbstractDocument() {
            return $http.get('/admin/getAbstractDocument');
        };

        function _getProgramDocument() {
            return $http.get('/admin/getProgramDocument');
        };

        function _saveProgram(data) {
            return $http.put('/admin/saveProgram', data);
        };

        function _getBillReport() {
            return $http.get('admin/getBillReport');
        };

        function _getRegistrationPaymentsFromIndex(data) {
            return $http.get('/admin/getRegistrationPayments/'+data);
        };

        function _getSponsorPaymentsFromIndex(data) {
            return $http.get('/admin/getSponsorPayments/'+data);
        };

        //----------------------------------USER SUBMISSIONS----------------------------------
        //gets the submissions of the user currently logged in
        function _getUserSubmissionList(data) {
            return $http.get('profile/getUserSubmissionList/' + data);
        };
        //get a specific user submission
        function _getUserSubmission(data) {
            return $http.get('profile/getUserSubmission/' + data);
        };
        //get list of submission types
        function _getSubmissionTypes() {
            return $http.get('profile/getSubmissionTypes');
        };
        //delete a submission
        function _deleteSubmission(data) {
            return $http.delete('profile/deleteSubmission/' + data);
        };
        //add a submission
        function _postSubmission(data) {
            return $http.post('profile/postSubmission', data)
        };
        //edit a submission
        function _editSubmission(data) {
            return $http.put('profile/editSubmission', data)
        };
        //posts a final submission 
        function _postFinalSubmission(data) {
            return $http.post('profile/postFinalSubmission', data)
        };
        //Get the submission deadline in order to close the option to add submissions after said deadline
        function _getSubmissionDeadline() {
            return $http.get('profile/getSubmissionDeadline');
        };
        //--------------------------------------------ADMIN-SUBMISSIONS------------------------------------
        //Gets all submissions that have not been deleted
        function _getAllSubmissions() {
            return $http.get('admin/getAllSubmissions');
        };
        //Gets the evaluations for the submission with submissionID
        function _getEvaluationsForSubmission(data) {
            return $http.get('admin/getEvaluationsForSubmission/' + data);
        };
        //Gets the list of evaluators on the system
        function _getAllEvaluators() {
            return $http.get('admin/getAllEvaluators');
        };
        //Assigns chosen evaluator to the given submission with submissionID
        function _assignEvaluator(data) {
            return $http.post('admin/assignEvaluator', data);
        };
        //Adds a submission submitted by the administrator for someone else
        function _addSubmissionByAdmin(data) {
            return $http.post('admin/addSubmissionByAdmin', data);
        }
    }
}

)();

