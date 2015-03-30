(function () {
    'use strict';

    var controllerId = 'contactCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', contactCtrl]);

    function contactCtrl($scope, $http, restApi) {
        var vm = this;

        vm.activate = activate;
        vm.title = 'contactCtrl';

        //Admin
        vm.contactName;
        vm.contactPhone;
        vm.contactEmail;
        vm.contactAdditionalInfo;

        //Interface
        vm.icontactName;
        vm.icontactPhone;
        vm.icontactEmail;
        vm.icontactAdditionalInfo;

        //Functions
        vm.getContact = _getContact;
        vm.saveContact = _saveContact;

        _getContact();

        function activate() {

        }

        function _getContact() {
            restApi.getContact()
            .success(function (data, status, headers, config) {
                if (data != null) {
                    vm.icontactName = data.contactName;
                    vm.icontactPhone = data.contactPhone;
                    vm.icontactEmail = data.contactEmail;
                    vm.icontactAdditionalInfo = data.contactAdditionalInfo;

                    vm.contactName = data.contactName;
                    vm.contactPhone = data.contactPhone;
                    vm.contactEmail = data.contactEmail;
                    vm.contactAdditionalInfo = data.contactAdditionalInfo;

                    load();
                }
            })

            .error(function (error) {

            });
        }

        function _saveContact() {
            var newContact = {
                contactName: vm.contactName,
                contactPhone: vm.contactPhone,
                contactEmail: vm.contactEmail,
                contactAdditionalInfo: vm.contactAdditionalInfo,
            }
            restApi.saveContact(newContact)
            .success(function (data, status, headers, config) {
                if (data != null) {
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