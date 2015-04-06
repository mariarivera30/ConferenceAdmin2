(function () {
    'use strict';

    var controllerId = 'registrationWebCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', registrationWebCtrl]);
    function registrationWebCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        vm.title = 'homeCtrl';

        //From Admin Website
        vm.temp;
        vm.registrationTitle1;
        vm.registrationParagraph1;
        vm.registrationTitle2;
        vm.registrationParagraph2;
        vm.registrationNotes;

        vm.undergraduateStudentFee;
        vm.graduateStudentFee;
        vm.highSchoolStudentFee;
        vm.companionStudentFee;
        vm.professionalAcademyFee;
        vm.professionalIndustryFee;

        vm.undergraduateStudentLateFee;
        vm.graduateStudentLateFee;
        vm.highSchoolStudentLateFee;
        vm.companionStudentLateFee;
        vm.professionalAcademyLateFee;
        vm.professionalIndustryLateFee;

        //InterfaceElements
        vm.iregistrationTitle1;
        vm.iregistrationParagraph1;
        vm.iregistrationTitle2;
        vm.iregistrationParagraph2;
        vm.iregistrationNotes;

        vm.iundergraduateStudentFee;
        vm.igraduateStudentFee;
        vm.ihighSchoolStudentFee;
        vm.icompanionStudentFee;
        vm.iprofessionalAcademyFee;
        vm.iprofessionalIndustryFee;

        vm.iundergraduateStudentLateFee;
        vm.igraduateStudentLateFee;
        vm.ihighSchoolStudentLateFee;
        vm.icompanionStudentLateFee;
        vm.iprofessionalAcademyLateFee;
        vm.iprofessionalIndustryLateFee;

        //Functions
        vm.getRegistrationInfo = _getRegistrationInfo;
        vm.saveRegistrationInfo = _saveRegistrationInfo;
        vm.reset = _reset;

        _getRegistrationInfo();

        function activate() {

        }

        function _reset() {
            vm.registrationTitle1 = vm.temp.registrationTitle1;
            vm.registrationParagraph1 = vm.temp.registrationParagraph1;
            vm.registrationTitle2 = vm.temp.registrationTitle2;
            vm.registrationParagraph2 = vm.temp.registrationParagraph2;
            vm.registrationNotes = vm.temp.registrationNotes;

            vm.undergraduateStudentFee = vm.temp.undergraduateStudentFee;
            vm.graduateStudentFee = vm.temp.graduateStudentFee;
            vm.highSchoolStudentFee = vm.temp.highSchoolStudentFee;
            vm.companionStudentFee = vm.temp.companionStudentFee;
            vm.professionalAcademyFee = vm.temp.professionalAcademyFee;
            vm.professionalIndustryFee = vm.temp.professionalIndustryFee;

            vm.undergraduateStudentLateFee = vm.temp.undergraduateStudentLateFee;
            vm.graduateStudentLateFee = vm.temp.graduateStudentLateFee;
            vm.highSchoolStudentLateFee = vm.temp.highSchoolStudentLateFee;
            vm.companionStudentLateFee = vm.temp.companionStudentLateFee;
            vm.professionalAcademyLateFee = vm.temp.professionalAcademyLateFee;
            vm.professionalIndustryLateFee = vm.temp.professionalIndustryLateFee;
        }

        function _getRegistrationInfo() {
            restApi.getRegistrationDetails()
            .success(function (data, status, headers, config) {
                if (data != null) {
                    vm.temp = data;
                    vm.iregistrationTitle1 = data.registrationTitle1;
                    vm.iregistrationParagraph1 = data.registrationParagraph1;
                    vm.iregistrationTitle2 = data.registrationTitle2;
                    vm.iregistrationParagraph2 = data.registrationParagraph2;
                    vm.iregistrationNotes = data.registrationNotes;

                    vm.iundergraduateStudentFee = data.undergraduateStudentFee;
                    vm.igraduateStudentFee = data.graduateStudentFee;
                    vm.ihighSchoolStudentFee = data.highSchoolStudentFee;
                    vm.icompanionStudentFee = data.companionStudentFee;
                    vm.iprofessionalAcademyFee = data.professionalAcademyFee;
                    vm.iprofessionalIndustryFee = data.professionalIndustryFee;

                    vm.iundergraduateStudentLateFee = data.undergraduateStudentLateFee;
                    vm.igraduateStudentLateFee = data.graduateStudentLateFee;
                    vm.ihighSchoolStudentLateFee = data.highSchoolStudentLateFee;
                    vm.icompanionStudentLateFee = data.companionStudentLateFee;
                    vm.iprofessionalAcademyLateFee = data.professionalAcademyLateFee;
                    vm.iprofessionalIndustryLateFee = data.professionalIndustryLateFee;

                    vm.registrationTitle1 = data.registrationTitle1;
                    vm.registrationParagraph1 = data.registrationParagraph1;
                    vm.registrationTitle2 = data.registrationTitle2;
                    vm.registrationParagraph2 = data.registrationParagraph2;
                    vm.registrationNotes = data.registrationNotes;

                    vm.undergraduateStudentFee = data.undergraduateStudentFee;
                    vm.graduateStudentFee = data.graduateStudentFee;
                    vm.highSchoolStudentFee = data.highSchoolStudentFee;
                    vm.companionStudentFee = data.companionStudentFee;
                    vm.professionalAcademyFee = data.professionalAcademyFee;
                    vm.professionalIndustryFee = data.professionalIndustryFee;

                    vm.undergraduateStudentLateFee = data.undergraduateStudentLateFee;
                    vm.graduateStudentLateFee = data.graduateStudentLateFee;
                    vm.highSchoolStudentLateFee = data.highSchoolStudentLateFee;
                    vm.companionStudentLateFee = data.companionStudentLateFee;
                    vm.professionalAcademyLateFee = data.professionalAcademyLateFee;
                    vm.professionalIndustryLateFee = data.professionalIndustryLateFee;

                    load();
                }
            })

            .error(function (error) {

            });
        }

        function _saveRegistrationInfo() {
            var newRegistration = {
                registrationTitle1: vm.registrationTitle1,
                registrationParagraph1: vm.registrationParagraph1,
                registrationTitle2: vm.registrationTitle2,
                registrationParagraph2: vm.registrationParagraph2,
                registrationNotes: vm.registrationNotes,

                undergraduateStudentFee: vm.undergraduateStudentFee,
                graduateStudentFee: vm.graduateStudentFee,
                highSchoolStudentFee: vm.highSchoolStudentFee,
                companionStudentFee: vm.companionStudentFee,
                professionalAcademyFee: vm.professionalAcademyFee,
                professionalIndustryFee: vm.professionalIndustryFee,

                undergraduateStudentLateFee: vm.undergraduateStudentLateFee,
                graduateStudentLateFee: vm.graduateStudentLateFee,
                highSchoolStudentLateFee: vm.highSchoolStudentLateFee,
                companionStudentLateFee: vm.companionStudentLateFee,
                professionalAcademyLateFee: vm.professionalAcademyLateFee,
                professionalIndustryLateFee: vm.professionalIndustryLateFee
            }
            restApi.saveRegistrationInfo(newRegistration)
            .success(function (data, status, headers, config) {
                if (data != null) {
                    vm.temp = newRegistration;
                    $("#updateConfirm").modal('show');
                }
            })
            .error(function (error) {
                $("#updateError").modal('show');
            });
        }

        //Avoid flashing when page loads
        var load = function () {
            document.getElementById("body").style.visibility = "visible";
        };
    }
})();