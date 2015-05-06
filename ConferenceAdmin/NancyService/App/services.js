(function () {
    'use strict';

    var serviceId = 'restApi';

    // TODO: replace app with your module name
    angular.module('app').factory(serviceId, ['$http', addrestApi]);

    function addrestApi($http) {
        var service = {
            getSponsorDeadline:_getSponsorDeadline,
            getSponsorPayments:_getSponsorPayments,
            changePassword: _changePassword,
            checkEmail: _checkEmail,
            requestPass: _requestPass,
            accountConfirmation: _accountConfirmation,
            createUser: _createUser,
            deleteSponsorComplemetaryKey: _deleteSponsorComplemetaryKey,
            deleteComplemetaryKey: _deleteComplemetaryKey,
            addComplementaryKey: _addComplementaryKey,
            getSponsorComplementaryKeys: _getSponsorComplementaryKeys,
            getSponsorComplementaryKeysFromIndex: _getSponsorComplementaryKeysFromIndex,
            getComplemetnaryKeys: _getComplemetnaryKeys,
            deleteAuthTemplate: _deleteAuthTemplate,
            getAuthTemplatesAdmin: _getAuthTemplatesAdmin,
            getAuthTemplatesAdminListIndex: _getAuthTemplatesAdminListIndex,
            updateAuthTemplate: _updateAuthTemplate,
            addAuthTemplate: _addAuthTemplate,
            deleteTemplate: _deleteTemplate,
            getTemplates: _getTemplates,
            getTemplatesAdmin: _getTemplatesAdmin,
            getTemplatesAdminListIndex: _getTemplatesAdminListIndex,
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
            getSponsorsListIndex: _getSponsorsListIndex,
            getTopics: _getTopics,
            deleteTopic: _deleteTopic,
            updateTopic: _updateTopic,
            getRegistrations: _getRegistrations,
            searchRegistration: _searchRegistration,
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
            searchAssignedSubmission: _searchAssignedSubmission,
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
            getEvaluatorListFromIndex: _getEvaluatorListFromIndex,
            updateEvaluatorAcceptanceStatus: _updateEvaluatorAcceptanceStatus,
            getNewEvaluator: _getNewEvaluator,
            postNewEvaluator: _postNewEvaluator,
            getHome: _getHome,
            getHomeImage: _getHomeImage,
            getWebsiteLogo: _getWebsiteLogo,
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
            getInterfaceDeadlines: _getInterfaceDeadlines,
            saveDeadlines: _saveDeadlines,
            getCommitteeInterface: _getCommitteeInterface,
            saveCommitteeInterface: _saveCommitteeInterface,
            getAdminSponsorBenefits: _getAdminSponsorBenefits,
            saveAdminSponsorBenefits: _saveAdminSponsorBenefits,
            saveInstructions: _saveInstructions,
            getInstructions: _getInstructions,
            getAllSponsorBenefits: _getAllSponsorBenefits,
            getGeneralInfo: _getGeneralInfo,
            saveGeneralInfo: _saveGeneralInfo,
            getPendingListFromIndex: _getPendingListFromIndex,
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
            getAttendanceReport: _getAttendanceReport,
            getRegistrationPaymentsFromIndex: _getRegistrationPaymentsFromIndex,
            getSponsorPaymentsFromIndex: _getSponsorPaymentsFromIndex,
            getEvaluationsForSubmission: _getEvaluationsForSubmission,
            getSubmissionDeadline: _getSubmissionDeadline,
            getAllEvaluators: _getAllEvaluators,
            assignEvaluator: _assignEvaluator,
            getEvaluationDetails: _getEvaluationDetails,
            assignTemplate: _assignTemplate,
            removeEvaluator: _removeEvaluator,
            changeSubmissionStatus: _changeSubmissionStatus,
            postAdminSubmission: _postAdminSubmission,
            getDeletedSubmissions: _getDeletedSubmissions,
            getListOfUsers: _getListOfUsers,
            getADeletedSubmission: _getADeletedSubmission,
            sponsorPayment: _sponsorPayment,
            getPayment: _getPayment,
            postAdminFinalSubmission: _postAdminFinalSubmission,
            editAdminSubmission: _editAdminSubmission,
            isMaster: _isMaster,
            getBanners: _getBanners,
            searchSubmission: _searchSubmission,
            searchDeletedSubmission: _searchDeletedSubmission,
            searchGuest: _searchGuest,
            searchReport: _searchReport,
            searchSponsors: _searchSponsors,
            searchAdmin: _searchAdmin,
            searchEvaluators: _searchEvaluators,
            getSubmissionFile: _getSubmissionFile,
            getAuthorizationFile: _getAuthorizationFile,
            getTemplateFile: _getTemplateFile,
            getEvaluationFile: _getEvaluationFile,
            getEvaluationTemplate: _getEvaluationTemplate,
            addFileToSubmission: _addFileToSubmission,
            manageExistingFiles: _manageExistingFiles,
            createFinalSubmissionFiles: _createFinalSubmissionFiles,
            sendContactEmail: _sendContactEmail,
            getSubmissionsReport: _getSubmissionsReport,
            getSubmissionDeadlines: _getSubmissionDeadlines,
            searchKeyCodes: _searchKeyCodes,
            userPayment: _userPayment,
            getUserPayment: _getUserPayment,
            getUserPriceInDeadline: _getUserPriceInDeadline,
        };  

        return service;

        //------------------------------------------ PAYMENT ------------------------------------------
        //[Maria] make payment when registering a user
        function _userPayment(data) {
            return $http.put('/payment/userPayment', data);
        };
        //[Maria] make payment as a sponsor
        function _sponsorPayment(data) {
            return $http.put('/payment/SponsorPayment', data);
        };
        //[Maria] get post-deadline fee
        function _getUserPriceInDeadline(data) {
            return $http.get('/payment/getUserPriceInDeadline/'+ data);
        };
        //[Maria] get user payment bill
        function _getUserPayment(data) {
            return $http.get('/payment/getUserPayment/' + data);
        };
        //[Maria] get sponsor payment bill
        function _getPayment(data) {
            return $http.get('/payment/GetPayment/' + data);
        };
        //[Maria] get list of sponsor payment bills
        function _getSponsorPayments(data) {
            return $http.get('/payment/getsponsorpayments/' + data);
        };
      
        //----------------------------------- SPONSOR COMPLEMENTARY -----------------------------------
        //[Maria] delete a specific sponsor complementary key
        function _deleteSponsorComplemetaryKey(data) {
            return $http.put('/admin/deleteSponsorComplementaryKey', data);
        };
        //[Maria] delete a specific complementary key
        function _deleteComplemetaryKey(data) {
            return $http.put('/admin/deleteComplementaryKey', data);
        };
        //[Maria] get list of all complementary keys
        function _getComplemetnaryKeys( ) {
            return $http.get('/admin/getComplementaryKeys');
        };
        //[Maria] add a new complementary key
        function _addComplementaryKey(data) {
            return $http.post('/admin/addSponsorComplementaryKeys', data);
        };
        //[Maria] get list of all complementary keys
        function _getSponsorComplementaryKeys(data) {
            return $http.get('/admin/getSponsorComplementaryKeys/' + data);
        };
        //[Maria] get list of all complementary keys of a specific sponsor
        function _getSponsorComplementaryKeysFromIndex(data) {
            return $http.get('/admin/getSponsorComplementaryKeysFromIndex/'+data.index+'/'+data.sponsorID);
        };
        //[Heidi] search for key codes using a certain criteria
        function _searchKeyCodes(data) {
            return $http.get('admin/searchKeyCodes/' + data.index + '/' + data.sponsorID + '/' + data.criteria);
        };

        //----------------------------------- SPONSOR -----------------------------------
        //[Maria] get deadline for becoming a sponsor
        function _getSponsorDeadline() {
            return $http.get('/admin/getSponsorDeadline/' );
        };
        //[Maria] edit a specific sponsor
        function _updateSponsor(data) {
            return $http.put('/admin/updateSponsor', data);
        };
        //[Maria] delete a specific sponsor
        function _deleteSponsor(data) {
            return $http.put('/admin/deleteSponsor/', data);
        };
        //[Maria] get a specific sponsor using an ID
        function _getSponsorbyID(data) {
            return $http.get('/admin/getSponsorbyID/' + data);
        };
        //[Maria] get list of all sponsor types
        function _getSponsorTypesList() {
            return $http.get('/admin/getSponsorTypesList');
        };
        //[Maria] add a new sponsor
        function _postNewSponsor(data) {
            return $http.post('/admin/addsponsor', data);
        };
        //[Maria] get list of all sponsors
        function _getSponsors() {
            return $http.get('/admin/getSponsor');
        };
        //[Maria] get index for list of sponsors
        function _getSponsorsListIndex(data) {
            return $http.get('/admin/getSponsorListIndex/'+data);
        };
        //[Heidi] seach for sponsors using a certain criteria
        function _searchSponsors(data) {
            return $http.get('admin/searchSponsors/' + data.index + '/' + data.criteria);
        };

        //----------------------------------- TEMPLATES -------------------------------
        //[Maria] edit a specific template
        function _updateTemplate(data) {
            return $http.put('/admin/updateTemplate', data);
        };
        //[Maria] delete a specific template
        function _deleteTemplate(data) {
            return $http.put('/admin/deleteTemplate/', data);
        };
        //[Maria] get list of templates
        function _getTemplatesAdmin() {
            return $http.get('/admin/getTemplatesAdmin');
        };
        //[Maria] get index for list of templates
        function _getTemplatesAdminListIndex(data) {
            return $http.get('/admin/getTemplatesAdminListIndex/'+data);
        };
        //[Maria] add a new template
        function _addTemplate(data) {
            return $http.post('/admin/addTemplate', data);
        };

        //----------------------------------- AUTHORIZATION TEMPLATES -------------------------------
        //[Maria] edit a specific authorization template
        function _updateAuthTemplate(data) {
            return $http.put('/admin/updateAuthTemplate', data);
        };
        //[Maria] delete a specific authorization template
        function _deleteAuthTemplate(data) {
            return $http.put('/admin/deleteAuthTemplate', data);
        };
        //[Maria] get list of authorization templates
        function _getAuthTemplatesAdmin() {
            return $http.get('/admin/getAuthTemplatesAdmin');
        };
        //[Maria] get index for list of authorization templates
        function _getAuthTemplatesAdminListIndex(data) {
            return $http.get('/admin/getAuthTemplatesAdminListIndex/'+data);
        };
        //[Maria] add a new authorization template
        function _addAuthTemplate(data) {
            return $http.post('/admin/addAuthTemplate', data);
        };

        //----------------------------------- LOGIN -----------------------------------
        //[Maria] login with user name and password
        function _login(data) {
            return $http.post('/auth/login', { email: data.email, password: data.password });
        };
        //----------------------------------- SIGN-UP -----------------------------------
        //[Maria] add new user to the system
        function _createUser(data) {
            return $http.post('/auth/createUser', data);
        };
        //[Maria] confirm the newly added user
        function _accountConfirmation(data) {
            return $http.get('/auth/accountConfirmation/' + data);
        };
        //---------------------------------- RECOVER PASSWORD --------------------------------
        //[Maria] verifies email of user
        function _checkEmail(data) {
            return $http.get('/auth/checkEmail/' + data);
        };
        //[Maria] request a change of password
        function _requestPass(data) {
            return $http.get('/auth/requestPass/' + data);
        };
        //[Maria] edit password of the user
        function _changePassword(data) {
            return $http.post('/auth/changePassword/', data);
        };
        //----------------------------------- TOPICS -----------------------------------
        //[Heidi] get list of all topics
        function _getTopics() {
            return $http.get('/admin/getTopic');
        };
        //[Heidi] add new topic
        function _postNewTopic(data) {
            return $http.post('/admin/addTopic', {
                name: data,
            });
        };
        //[Heidi] edit a specific topic
        function _updateTopic(data) {
            return $http.put('/admin/updateTopic', { topiccategoryID: data.topiccategoryID, name: data.name });
        };
        //[Heidi] delete a specific topic
        function _deleteTopic(data) {
            return $http.put('/admin/deleteTopic/' + data);
        };

        //----------------------------------- ADMINISTRATORS -----------------------------------
        //[Heidi] get list of all administrators
        function _getAdministrators(data) {
            return $http.get('/admin/getAdministrators/'+data);
        };
        //[Heidi] get admin by specifying email
        function _getNewAdmin(email) {
            return $http.get('/admin/getNewAdmin/' + email);
        };
        //[Heidi] get list of all privileges 
        function _getPrivilegesList() {
            return $http.get('/admin/getPrivilegesList');
        };
        //[Heidi] add a new admin
        function _postNewAdmin(data) {
            return $http.post('/admin/addAdmin', {
                firstName: data.firstName,
                lastName: data.lastName,
                email: data.email,
                privilegeID: data.privilegeID,
            });
        };
        //[Heidi] edit a specific admin
        function _editAdmin(id, privilegeID, oldPrivilegeID) {
            return $http.put('/admin/editAdmin/', { userID: id, privilegeID: privilegeID, oldPrivilegeID: oldPrivilegeID });
        };
        //[Heidi] delete a specific admin
        function _deleteAdmin(userid, privilegeid) {
            return $http.put('/admin/deleteAdmin/', {
                userID: userid,
                privilegeID: privilegeid
            });
        };
        //[Heidi] search for admins using a certain criteria
        function _searchAdmin(data) {
            return $http.get('admin/searchAdmin/' + data.index + '/' + data.criteria);
        };

        //----------------------------------- REGISTRATIONS -----------------------------------
        //[Randy] get list of all registrations
        function _getRegistrations(data) {
            return $http.get('/admin/getRegistrations/' + data);
        };
        //[Randy] search for a registration entry with a certain criteria 
        function _searchRegistration(data) {
            return $http.get('/admin/searchRegistration/' + data.index + '/' + data.criteria);
        };
        //[Randy] get list of all types of user
        function _getUserTypes() {
            return $http.get('/admin/getUserTypes');
        };
        //[Randy] edit a registration entry
        function _updateRegistration(data) {
            return $http.put('/admin/updateRegistration', { registrationID: data.registrationID, firstname: data.firstname, lastname: data.lastname, usertypeid: data.usertypeid, affiliationName: data.affiliationName, date1: data.date1, date2: data.date2, date3: data.date3, notes: data.notes });
        };
        //[Randy] delete a specific registration entry
        function _deleteRegistration(data) {
            return $http.delete('/admin/deleteRegistration/' + data);
        };
        //[Randy] add a new registration entry
        function _postNewRegistration(data) {
            return $http.post('/admin/addRegistration', data);
        };
        //[Randy] get conference dates
        function _getDates() {
            return $http.get('/admin/getDates');
        };
        //[Heidi] download report of attendees
        function _getAttendanceReport() {
            return $http.get('admin/getAttendanceReport');
        };
        //----------------------------------- GUESTS -----------------------------------
        //[Jaimeiris] get guest list for admin
        function _getGuestList(data) {
            return $http.get('admin/getGuestList/' + data);
        };
        //[Jaimeiris] update guest acceptance status
        function _updateAcceptanceStatus(data) {
            return $http.put('admin/updateAcceptanceStatus', { id: data.ID, status: data.status });
        };
        //[Jaimeiris] get minor authorizations
        function _displayAuthorizations(data) {
            return $http.get('admin/displayAuthorizations/' + data);
        };
        //[Jaimeiris] Change registration status to Rejected
        function _rejectRegisteredGuest(data) {
            return $http.put('admin/rejectRegisteredGuest/' + data);
        };
        //[Randy] Search within the list with a certain criteria
        function _searchGuest(data) {
            return $http.get('admin/searchGuest/' + data.index + '/' + data.criteria);
        };

        //----------------------------------- PROFILE-INFO -----------------------------------
        //[Randy] get information for the user profile
        function _getProfileInfo(data) {
            return $http.get('profile/getProfileInfo/' + data);
        };
        //[Randy] edit information on user profile
        function _updateProfileInfo(data) {
            return $http.put('profile/updateProfileInfo/', data);
        };
        //[Randy] make application to attend conference
        function _apply(data) {
            return $http.put('profile/apply/', data);
        };
        //[Randy] redirect to payment page
        function _makePayment(data) {
            return $http.put('profile/makePayment/', data);
        };
        //[Randy] register as a complementary user
        function _complementaryPayment(data) {
            return $http.put('profile/complementaryPayment/', data);
        };
        //[Randy] validate complementary key
        function _checkComplementaryKey(data) {
            return $http.get('profile/checkComplementaryKey/' + data);
        };

        //--------------------------------- PROFILE-SUBMISSIONS ---------------------------
        //[Jaimeiris] get list of submissions assigned to the evalutor currently logged in
        function _getAssignedSubmissions(data) {
            return $http.get('profile/getAssignedSubmissions/' + data.evaluatorUserID + '/' + data.index);
        };
        //[Randy] Search within a list with a specific criteria
        function _searchAssignedSubmission(data) {
            return $http.get('profile/searchAssignedSubmission/' + data.evaluatorUserID + '/' + data.index + '/' + data.criteria);
        };
        //[Jaimeiris] get details of submission with ID submissionID
        function _getSubmissionDetails(data) {
            return $http.get('profile/getSubmission/' + data.submissionID + '/' + data.evaluatorID);
        };
        //[Jaimeiris] add new evalution for a submission
        function _postEvaluation(data) {
            return $http.post('profile/addEvaluation', data);
        };
        //[Jaimeiris] edit evaluation for a submission
        function _editEvaluation(data) {
            return $http.put('profile/editEvaluation', data);
        };
        //[Jaimeiris] get the template assigned to the submission
        function _getEvaluationTemplate(data) {
            return $http.get('profile/getEvaluationTemplate/' + data);
        };
        //[Jaimeiris] get the file uploaded to the submission
        function _getEvaluationFile(data) {
            return $http.get('profile/getEvaluationFile/' + data.submissionID + '/' + data.evaluatorID);
        };
        //[Randy] add a single file to a submission
        function _addFileToSubmission(data) {
            return $http.put('profile/addSubmissionFile', data);
        };
        //[Randy] manage existing list of files
        function _manageExistingFiles(data) {
            return $http.put('profile/manageExistingFiles', data);
        };
        //[Jaimeiris] re-create final submission files
        function _createFinalSubmissionFiles(data) {
            return $http.put('profile/createFinalSubmissionFiles', data);
        };
        //----------------------------------- PROFILE-AUTHORIZATION -----------------------------------
        //[Randy] add authorization document
        function _uploadDocument(data) {
            return $http.put('/profile/uploadDocument/', data);
        };
        //[Randy] get list of authorization templates
        function _getTemplates() {
            return $http.get('profile/getTemplates');
        };
        //[Randy] get list of authorization documents added
        function _getDocuments(data) {
            return $http.get('profile/getDocuments/' + data);
        };
        //[Randy] delete a specific authorization document
        function _deleteDocument(data) {
            return $http.put('profile/deleteDocument/', data);
        };
        //[Randy] validate companion key and assign the minor to the companion
        function _selectCompanion(data) {
            return $http.post('profile/selectCompanion/', data);
        };
        //[Randy] get the submitted companion key
        function _getCompanionKey(data) {
            return $http.get('profile/getCompanionKey/' + data);
        };
        //[Randy] download selected template file
        function _getTemplateFile(data) {
            return $http.get('profile/getTemplateFile/' + data);
        };
        //[Randy] download selected authorization file
        function _getAuthorizationFile(data) {
            return $http.get('profile/getAuthorizationFile/' + data);
        };

        //----------------------------------- EVALUATORS ---------------------------------
        //[Heidi] get list of evaluators starting at a specified index
        function _getEvaluatorListFromIndex(data) {
            return $http.get('admin/getEvaluatorListFromIndex/' + data.index + '/' + data.id);
        };
        //[Heidi] get list of pending evaluators starting at a specified index
        function _getPendingListFromIndex(data) {
            return $http.get('admin/getPendingListFromIndex/'+data);
        };
        //[Heidi] get evaluator by specifying the email
        function _getNewEvaluator(email) {
            return $http.get('/admin/getNewEvaluator/' + email);
        };
        //[Heidi] search for evaluators using a certain criteria
        function _searchEvaluators(data) {
            return $http.get('admin/searchEvaluators/' + data.index + '/' + data.criteria);
        };
        //[Heidi] add a new evaluator
        function _postNewEvaluator(email) {
            return $http.post('/admin/addEvaluator/' + email);
        };
        //[Heidi] edit a specific evaluator's acceptance status
        function _updateEvaluatorAcceptanceStatus(data) {
            return $http.put('admin/updateEvaluatorAcceptanceStatus', { userID: data.userID, acceptanceStatus: data.acceptanceStatus });
        };

        //--------------------------------- WEBSITE CONTENT ----------------------------------------------
        //[Heidi] get information for the Home tab
        function _getHome() {
            return $http.get('/admin/getHome');
        };
        //[Heidi] get image for the Home tab
        function _getHomeImage() {
            return $http.get('/admin/getHomeImage');
        };
        //[Heidi] get logo of the website
        function _getWebsiteLogo() {
            return $http.get('/admin/getWebsiteLogo');
        };
        //[Heidi] save information for the  tab
        function _saveHome(data) {
            return $http.put('/admin/saveHome', data);
        };
        //[Heidi] remove a file
        function _removeFile(data) {
            return $http.put('/admin/removeFile/' + data);
        };
        //[Heidi] get information for the Venue tab
        function _getVenue() {
            return $http.get('/admin/getVenue');
        };
        //[Heidi] save information for the Venue tab
        function _saveVenue(data) {
            return $http.put('/admin/saveVenue', data);
        };
        //[Heidi] get information for the Contact tab
        function _getContact() {
            return $http.get('/admin/getContact');
        };
        //[Heidi] save information for the Contact tab
        function _saveContact(data) {
            return $http.put('/admin/saveContact', data);
        };
        //[Heidi] send an email to the specified address
        function _sendContactEmail(data) {
            return $http.post('/admin/sendContactEmail', data);
        };
        //[Heidi] get information for the Call for Participation tab
        function _getParticipation() {
            return $http.get('/admin/getParticipation');
        };
        //[Heidi] save information for the Call for Participation tab
        function _saveParticipation(data) {
            return $http.put('/admin/saveParticipation', data);
        };
        //[Heidi] save information for the Registration tab
        function _saveRegistrationInfo(data) {
            return $http.put('/admin/saveRegistrationInfo', data);
        };
        //[Heidi] get information for the Registration tab
        function _getRegistrationDetails() {
            return $http.get('/admin/getRegistrationInfo');
        };
        //[Heidi] get information for the Deadlines tab
        function _getDeadlines() {
            return $http.get('/admin/getDeadlines');
        };
        //[Heidi] get list of deadlines on the interface
        function _getInterfaceDeadlines() {
            return $http.get('/admin/getInterfaceDeadlines');
        };
        //[Heidi] get deadlines of the interface
        function _saveDeadlines(data) {
            return $http.put('/admin/saveDeadlines', data);
        };
        //[Heidi] get information for the Committee tab
        function _getCommitteeInterface() {
            return $http.get('admin/getCommitteeInterface');
        };
        //[Heidi] save information for the Committee tab
        function _saveCommitteeInterface(data) {
            return $http.put('/admin/saveCommitteeInterface', data);
        };
        //[Heidi] get information for Admin Sponsor
        function _getAdminSponsorBenefits(data) {
            return $http.get('admin/getAdminSponsorBenefits/' + data);
        };
        //[Heidi] get information for the Sponsors tab
        function _saveAdminSponsorBenefits(data) {
            return $http.put('/admin/saveAdminSponsorBenefits', data);
        };
        //[Heidi] save information of the instructions
        function _saveInstructions(data) {
            return $http.put('/admin/saveInstructions', data);
        };
        //[Heidi] get information of the instructions
        function _getInstructions() {
            return $http.get('admin/getSponsorInstructions');
        };
        //[Heidi] get information for the Sponsors tab
        function _getAllSponsorBenefits() {
            return $http.get('admin/getAllSponsorBenefits');
        };
        //[Heidi] get information for the General Information tab
        function _getGeneralInfo() {
            return $http.get('/admin/getGeneralInfo');
        };
        //[Heidi] save information for the General Information tab
        function _saveGeneralInfo(data) {
            return $http.put('/admin/saveGeneralInfo', data);
        };
        //[Heidi] get information for the Program tab
        function _getProgram() {
            return $http.get('/admin/getProgram');
        };
        //[Heidi] open/download the abstract file
        function _getAbstractDocument() {
            return $http.get('/admin/getAbstractDocument');
        };
        //[Heidi] open/download the program file
        function _getProgramDocument() {
            return $http.get('/admin/getProgramDocument');
        };
        //[Heidi] save the program file
        function _saveProgram(data) {
            return $http.put('/admin/saveProgram', data);
        };
        //[Heidi] get report for billing
        function _getBillReport() {
            return $http.get('admin/getBillReport');
        };
        //[Heidi] get list of registration payments starting on a specified index
        function _getRegistrationPaymentsFromIndex(data) {
            return $http.get('/admin/getRegistrationPayments/'+data);
        };
        //[Heidi] get list of sponsor payments starting on a specified index
        function _getSponsorPaymentsFromIndex(data) {
            return $http.get('/admin/getSponsorPayments/'+data);
        };
        //[Heidi] seach for reports using a certain criteria
        function _searchReport(data) {
            return $http.get('admin/searchReport/' + data.index + '/' + data.criteria);
        };

        //---------------------------------- USER SUBMISSIONS ----------------------------------
        //[Jaimeiris] gets the submissions of the user currently logged in
        function _getUserSubmissionList(data) {
            return $http.get('profile/getUserSubmissionList/' + data);
        };
        //[Jaimeiris] get a specific user submission
        function _getUserSubmission(data) {
            return $http.get('profile/getUserSubmission/' + data);
        };
        //[Jaimeiris] get list of submission types
        function _getSubmissionTypes() {
            return $http.get('profile/getSubmissionTypes');
        };
        //[Jaimeiris] delete a submission
        function _deleteSubmission(data) {
            return $http.delete('profile/deleteSubmission/' + data);
        };
        //[Jaimeiris] add a submission
        function _postSubmission(data) {
            return $http.post('profile/postSubmission', data);
        };
        //[Jaimeiris] edit a submission
        function _editSubmission(data) {
            return $http.put('profile/editSubmission', data);
        };
        //[Jaimeiris] posts a final submission 
        function _postFinalSubmission(data) {
            return $http.post('profile/postFinalSubmission', data);
        };
        //[Jaimeiris] Get the submission deadline in order to close the option to add submissions after said deadline
        function _getSubmissionDeadline() {
            return $http.get('profile/getSubmissionDeadline');
        };

        //-------------------------------------------- ADMIN-SUBMISSIONS ------------------------------------
        //[Jaimeiris] Gets all submissions that have not been deleted
        function _getAllSubmissions(data) {
            return $http.get('admin/getAllSubmissions/' + data);
        };
        //[Jaimeiris] Gets the evaluations for the submission with submissionID
        function _getEvaluationsForSubmission(data) {
            return $http.get('admin/getEvaluationsForSubmission/' + data);
        };
        //[Jaimeiris] Gets the list of evaluators on the system
        function _getAllEvaluators() {
            return $http.get('admin/getAllEvaluators');
        };
        //[Jaimeiris] Assigns chosen evaluator to the given submission with submissionID
        function _assignEvaluator(data) {
            return $http.post('admin/assignEvaluator/' + data.submissionID + '/' + data.evaluatorID);
        };
        //[Jaimeiris] Adds a submission submitted by the administrator for someone else
        function _addSubmissionByAdmin(data) {
            return $http.post('admin/addSubmissionByAdmin', data);
        };
        //[Jaimeiris] get details of evaluation with submissionID and evaluatorID
        function _getEvaluationDetails(data) {
            return $http.get('admin/getEvaluationDetails/' + data.submissionID + '/' + data.evaluatorID);
        };
        //[Jaimeiris] Assigns chosen template to the given submission with submissionID
        function _assignTemplate(data) {
            return $http.post('admin/assignTemplate/' + data.submissionID + '/' + data.templateID);
        };
        //[Jaimeiris] removes relation between evaluator and assigned submission
        function _removeEvaluator(data) {
            return $http.put('admin/removeEvaluatorSubmission/' + data);
        };
        //[Jaimeiris] Changes submission status
        function _changeSubmissionStatus(data) {
            return $http.put('admin/changeSubmissionStatus/' + data.status + '/' + data.submissionID);
        };
        //[Jaimeiris] Adds a submission submitted by the admin        
        function _postAdminSubmission(data) {
            return $http.post('admin/postAdminSubmission', data);
        };
        //[Randy] get list of all users
        function _getListOfUsers() {
            return $http.get('admin/getListOfUsers')
        };
        //[Jaimeiris] add a final submission submitted by the admin        
        function _postAdminFinalSubmission(data) {
            return $http.post('admin/postAdminFinalSubmission', data);
        };
        //[Jaimeiris] edit a submission
        function _editAdminSubmission(data) {
            return $http.put('admin/editAdminSubmission/', data);
        };
        //[Jaimeiris] determines whether the logged in user is a Master user
        function _isMaster(data) {
            return $http.get('admin/isMaster/' + data);
        };
        //[Randy] gets all deleted submissions
        function _getDeletedSubmissions(data) {
            return $http.get('admin/getDeletedSubmissions/' + data);
        };
        //[Randy] get details of a deleted submission
        function _getADeletedSubmission(data) {
            return $http.get('admin/getADeletedSubmission/' + data);
        };
        //[Randy] search within the list with a certain criteria
        function _searchSubmission(data) {
            return $http.get('admin/searchSubmission/' + data.index + '/' + data.criteria);
        };
        //[Randy] search within the list with a certain criteria
        function _searchDeletedSubmission(data) {
            return $http.get('admin/searchDeletedSubmission/' + data.index + '/' + data.criteria);
        };
        //[Randy] get the file to download
        function _getSubmissionFile(data) {
            return $http.get('admin/getSubmissionFile/' + data);
        };
        //[Randy] get submissions report
        function _getSubmissionsReport() {
            return $http.get('admin/getSubmissionsReport');
        };
        //[Randy] get submission deadlines
        function _getSubmissionDeadlines() {
            return $http.get('profile/getSubmissionDeadlines');
        };
        //[Randy] get list of templates
        function _getTemplates() {
            return $http.get('/admin/getTemplates');
        };
        //------------------------------------ BANNER -------------------------------------
        //[Heidi] get sponsor logos for banner
        function _getBanners(data) {
            return $http.get('admin/getBanners/' + data.index + '/' + data.sponsor);
        };
    }
}

)();

