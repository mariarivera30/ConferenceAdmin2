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
        vm.temp;
        vm.contactName;
        vm.contactPhone;
        vm.contactEmail;
        vm.contactAdditionalInfo;
        vm.loading = false;

        //Interface
        vm.icontactName;
        vm.icontactPhone;
        vm.icontactEmail;
        vm.icontactAdditionalInfo;
        vm.iloading;
        vm.senderName;
        vm.senderEmail;
        vm.senderMessage;

        //For error modal:
        vm.obj = {
            title: "",
            message1: "",
            message2: "",
            label: "",
            okbutton: false,
            okbuttonText: "",
            cancelbutton: false,
            cancelbuttoText: "Cancel",
        };
        vm.okFunc;
        vm.cancelFunc;

        vm.toggleModal = function (action) {

            if (action == "error")
                vm.obj.title = "Server Error",
                vm.obj.message1 = "Please refresh the page and try again.",
                vm.obj.message2 = "",
                vm.obj.label = "",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "OK",
                vm.obj.cancelbutton = false,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
        };

        //Functions
        vm.getContact = _getContact;
        vm.saveContact = _saveContact;
        vm.reset = _reset;
        vm.sendEmail = _sendEmail;

        _getContact();

        function activate() {

        }

        function _clear() {
            vm.senderName = "";
            vm.senderEmail = "";
            vm.senderMessage = "";
        }

        function _reset() {
            if (vm.temp != null && vm.temp != "") {
                vm.contactName = vm.temp.contactName;
                vm.contactPhone = vm.temp.contactPhone;
                vm.contactEmail = vm.temp.contactEmail;
                vm.contactAdditionalInfo = vm.temp.contactAdditionalInfo;
            }
        }

        function _getContact() {
            restApi.getContact()
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.temp = data;
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
                load();
                vm.toggleModal('error');
            });
        }

        function _saveContact() {
            vm.loading = true;
            var newContact = {
                contactName: vm.contactName,
                contactPhone: vm.contactPhone,
                contactEmail: vm.contactEmail,
                contactAdditionalInfo: vm.contactAdditionalInfo,
            }
            restApi.saveContact(newContact)
            .success(function (data, status, headers, config) {
                if (data != null && data != "") {
                    vm.temp.contactName = newContact.contactName;
                    vm.temp.contactPhone = newContact.contactPhone;
                    vm.temp.contactEmail = newContact.contactEmail;
                    vm.temp.contactAdditionalInfo = newContact.contactAdditionalInfo;
                    vm.loading = false;
                    $("#updateConfirm").modal('show');
                }
            })
            .error(function (error) {
                vm.loading = false;
                vm.toggleModal('error');
            });
        }

        function _sendEmail() {
            if (vm.contactEmail != null && vm.contactEmail != "") {
                vm.iloading = true;
                var info = {
                    name: vm.senderName,
                    email: vm.senderEmail,
                    message: vm.senderMessage,
                    contactEmail: vm.contactEmail
                }
                restApi.sendContactEmail(info)
                .success(function (data, status, headers, config) {
                    if (data) {
                        vm.iloading = false;
                        _clear();
                        $("#emailConfirm").modal('show');
                    }
                })
                .error(function (error) {
                    vm.iloading = false;
                    vm.toggleModal('error');
                });
            }
        }

        //Avoid flashing when page loads
        var load = function () {
            if (document.getElementById('loading-icon') != null) {
                document.getElementById("loading-icon").style.visibility = "hidden";
            }
            document.getElementById("body").style.visibility = "visible";
        };

    }
})();