(function () {
    'use strict';

    var serviceId = 'restApi';

    // TODO: replace app with your module name
    angular.module('app').factory(serviceId, ['$http', addrestApi]);

    function addrestApi($http) {
        var service = {
            getSponsorPayments:_getSponsorPayments,
            checkEmailSponsor:_checkEmailSponsor,
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
            searchKeyCodes: _searchKeyCodes
        };

        return service;

        //------------------------------------------Payment------------------------------------------
        
        function _sponsorPayment(data) {
            return $http.put('/payment/SponsorPayment', data);
        };
        function _getPayment(data) {
            return $http.get('/payment/GetPayment/' + data);
        };
        function _getSponsorPayments(data) {
            return $http.get('/payment/getsponsorpayments/' + data);
        };
        
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

        function _getSponsorComplementaryKeysFromIndex(data) {
            return $http.get('/admin/getSponsorComplementaryKeysFromIndex/'+data.index+'/'+data.sponsorID);
        };

        function _searchKeyCodes(data) {
            return $http.get('admin/searchKeyCodes/' + data.index + '/' + data.sponsorID +'/'+ data.criteria);
        }

        //-----------------------------------Sponsor-----------------------------------
        function _checkEmailSponsor(data) {
            return $http.get('/admin/checkEmailSponsor/' + data);
        };
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

        function _getSponsorsListIndex(data) {
            return $http.get('/admin/getSponsorListIndex/'+data);
        };

        function _searchSponsors(data) {
            return $http.get('admin/searchSponsors/' + data.index + '/' + data.criteria);
        }

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

        function _getTemplatesAdminListIndex(data) {
            return $http.get('/admin/getTemplatesAdminListIndex/'+data);
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

        function _getAuthTemplatesAdminListIndex(data) {
            return $http.get('/admin/getAuthTemplatesAdminListIndex/'+data);
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

        function _getAdministrators(data) {
            return $http.get('/admin/getAdministrators/'+data);
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

        function _searchAdmin(data) {
            return $http.get('admin/searchAdmin/' + data.index + '/' + data.criteria);
        }

        //-----------------------------------REGISTRATIONS-----------------------------------

        function _getRegistrations(data) {
            return $http.get('/admin/getRegistrations/' + data);
        };

        function _searchRegistration(data) {
            return $http.get('/admin/searchRegistration/' + data.index + '/' + data.criteria);
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

        function _getAttendanceReport() {
            return $http.get('admin/getAttendanceReport');
        };
        //-----------------------------------GUESTS-----------------------------------
        //get guest list for admin
        function _getGuestList(data) {
            return $http.get('admin/getGuestList/' + data);
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

        //Search within the list with a certain criteria
        function _searchGuest(data) {
            return $http.get('admin/searchGuest/' + data.index + '/' + data.criteria);
        }

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
            return $http.get('profile/getAssignedSubmissions/' + data.evaluatorUserID + '/' + data.index);
        };
        //Search within a list with a specific criteria
        function _searchAssignedSubmission(data) {
            return $http.get('profile/searchAssignedSubmission/' + data.evaluatorUserID + '/' + data.index + '/' + data.criteria);
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
            return $http.put('profile/editEvaluation', data);
        };
        //get the template assigned to the submission
        function _getEvaluationTemplate(data) {
            return $http.get('profile/getEvaluationTemplate/' + data);
        };
        //get the file uploaded to the submission
        function _getEvaluationFile(data) {
            return $http.get('profile/getEvaluationFile/' + data);
        };
        //add a single file to a submission
        function _addFileToSubmission(data) {
            return $http.put('profile/addSubmissionFile', data);
        };
        //manage existing list of files
        function _manageExistingFiles(data) {
            return $http.put('profile/manageExistingFiles', data);
        }
        //re-create final submission files
        function _createFinalSubmissionFiles(data) {
            return $http.put('profile/createFinalSubmissionFiles', data);
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
        function _getTemplateFile(data) {
            return $http.get('profile/getTemplateFile/' + data);
        };

        function _getAuthorizationFile(data) {
            return $http.get('profile/getAuthorizationFile/' + data);
        };

        //-----------------------------------EVALUATORS---------------------------------

        function _getEvaluatorListFromIndex(data) {
            return $http.get('admin/getEvaluatorListFromIndex/' + data.index + '/' + data.id);
        };

        function _getPendingListFromIndex(data) {
            return $http.get('admin/getPendingListFromIndex/'+data);
        };

        function _getNewEvaluator(email) {
            return $http.get('/admin/getNewEvaluator/' + email);
        };

        function _searchEvaluators(data) {
            return $http.get('admin/searchEvaluators/' + data.index + '/' + data.criteria);
        }

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

        function _getHomeImage() {
            return $http.get('/admin/getHomeImage');
        };

        function _getWebsiteLogo() {
            return $http.get('/admin/getWebsiteLogo');
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

        function _sendContactEmail(data) {
            return $http.post('/admin/sendContactEmail', data);
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

        function _getInterfaceDeadlines() {
            return $http.get('/admin/getInterfaceDeadlines');
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
            return $http.put('/admin/saveInstructions', data);
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

        function _searchReport(data) {
            return $http.get('admin/searchReport/' + data.index + '/' + data.criteria);
        }

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
        function _getAllSubmissions(data) {
            return $http.get('admin/getAllSubmissions/' + data);
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
            return $http.post('admin/assignEvaluator/' + data.submissionID + '/' + data.evaluatorID);
        };
        //Adds a submission submitted by the administrator for someone else
        function _addSubmissionByAdmin(data) {
            return $http.post('admin/addSubmissionByAdmin', data);
        };
        //get details of evaluation with submissionID and evaluatorID
        function _getEvaluationDetails(data) {
            return $http.get('admin/getEvaluationDetails/' + data.submissionID + '/' + data.evaluatorID);
        };
        //Assigns chosen template to the given submission with submissionID
        function _assignTemplate(data) {
            return $http.post('admin/assignTemplate/' + data.submissionID + '/' + data.templateID);
        };
        //removes relation between evaluator and assigned submission
        function _removeEvaluator(data) {
            return $http.put('admin/removeEvaluatorSubmission/' + data);
        };
        //Changes submission status
        function _changeSubmissionStatus(data) {
            return $http.put('admin/changeSubmissionStatus/' + data.status + '/' + data.submissionID);
        };
        //Adds a submission submitted by the admin        
        function _postAdminSubmission(data) {
            return $http.post('admin/postAdminSubmission', data)
        };
        //gets all deleted submissions
        function _getDeletedSubmissions(data) {
            return $http.get('admin/getDeletedSubmissions/' + data)
        };
        //get details of a deleted submission
        function _getADeletedSubmission(data){
            return $http.get('admin/getADeletedSubmission/' + data)
        };
        //get list of all users
        function _getListOfUsers() {
            return $http.get('admin/getListOfUsers')
        };
        //add a final submission submitted by the admin        
        function _postAdminFinalSubmission(data) {
            return $http.post('admin/postAdminFinalSubmission', data)
        };
        //edit a submission
        function _editAdminSubmission(data) {
            return $http.put('admin/editAdminSubmission/', data);
        };
        //determines whether the logged in user is a Master user
        function _isMaster(data) {
            return $http.get('admin/isMaster/' + data);
        }
        //search within the list with a certain criteria
        function _searchSubmission(data) {
            return $http.get('admin/searchSubmission/' + data.index + '/' + data.criteria);
        }
        //search within the list with a certain criteria
        function _searchDeletedSubmission(data) {
            return $http.get('admin/searchDeletedSubmission/' + data.index + '/' + data.criteria);
        }
        //get the file to download
        function _getSubmissionFile(data) {
            return $http.get('admin/getSubmissionFile/' + data);
        }
        //get submissions report
        function _getSubmissionsReport() {
            return $http.get('admin/getSubmissionsReport');
        };
        //get submission deadlines
        function _getSubmissionDeadlines() {
            return $http.get('profile/getSubmissionDeadlines');
        }
        //------------------------------------Banner-------------------------------------
        function _getBanners() {
            return $http.get('admin/getBanners')
        };
    }
}

)();

