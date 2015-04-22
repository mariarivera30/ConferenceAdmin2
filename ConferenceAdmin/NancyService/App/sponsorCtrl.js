(function () {
    'use strict';

    var controllerId = 'sponsorCtrl';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$scope', '$http', 'restApi', sponsorCtrl]);

    function sponsorCtrl($scope, $http, restApi) {
        var vm = this;
        vm.activate = activate;
        //add sponsor fields
        vm.title = 'sponsorCtrl';
        vm.sponsor;
        vm.loading;
        vm.addComplementaryObj = { sponsorID: 0, quantity: 0, company: "" };
        vm.TYPE = {};
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

        //Sponsor Payments- Variables (Paging)
        vm.sponsorsList = []; //Results to Display
        vm.sindex = 0;  //Page index [Goes from 0 to smaxIndex-1]
        vm.smaxIndex = 0;   //Max page number

        //Complimentary Keys Payments- Variables (Paging)
        vm.sponsorKeys = []; //Results to Display
        vm.cindex = 0;  //Page index [Goes from 0 to cmaxIndex-1]
        vm.cmaxIndex = 0;   //Max page number

        //Search List Variables (Paging)
        vm.searchList = [];
        vm.searchIndex = 0;  //Page index [Goes from 0 to pmaxIndex-1]
        vm.searchMaxIndex = 0;   //Max page number
        vm.criteria;
        vm.showSearch = false;
        vm.showResults = false;

        // Functions
        vm.addSponsor = _addSponsor;
        vm.getSponsorTypes = _getSponsorTypes;
        vm.selectedSponsor = _selectedSponsor;
        vm.clearSponsor = _clearSponsor;
        vm.updateSponsor = _updateSponsor;
        vm.deleteSponsor = _deleteSponsor;
        vm.submitForm = _submitForm;
        vm.addValues = _addValues;
        vm.editValues = _editValues;
        vm.viewValues = _viewValues;
        vm.downloadLogo = _downloadLogo;
        vm.complementaryValues = _complementaryValues;
        vm.getSponsorComplementaryKeys = _getSponsorComplementaryKeys;
        vm.deleteComplemetaryKey = _deleteComplemetaryKey;
        vm.deleteSponsorComplemetaryKey = _deleteSponsorComplemetaryKey;
        vm.addComplementaryKey = _addComplementaryKey;
        vm.selectedKey = _selectedKey;
        vm.clearPic = _clearPic;

        //Functions- Sponsors (Paging)
        vm.getSponsorListFromIndex = _getSponsorListFromIndex;
        vm.previousSponsor = _previousSponsor;
        vm.nextSponsor = _nextSponsor;
        vm.getFirstSponsorPage = _getFirstSponsorPage;
        vm.getLastSponsorPage = _getLastSponsorPage;

        //Functions- Complimentary Keys (Paging)
        vm.previousComplimentary = _previousComplimentary;
        vm.nextComplimentary = _nextComplimentary;
        vm.getFirstComplimentaryPage = _getFirstComplimentaryPage;
        vm.getLastComplimentaryPage = _getLastComplimentaryPage;

        //Functions- Search (Paging)
        vm.search = _search;
        vm.previousSearch = _previousSearch;
        vm.nextSearch = _nextSearch;
        vm.getFirstSearch = _getFirstSearch;
        vm.getLastSearch = _getLastSearch;
        vm.back = _back;

        vm.pdf = "pdf";

        activate();

        $scope.showContent = function ($fileContent,file) {
            if ($fileContent != undefined) {
                $scope.content = $fileContent;
                vm.fileext = file.name.split(".", 2)[1];
                if (vm.fileext == "jpg" || vm.fileext == "png" || vm.fileext == "jpeg" || vm.fileext == "pic" || vm.fileext == "pict" || vm.fileext == "gif")
                    vm.ext = false;
                else {
                    document.getElementById("inputFile").value = "";
                    $scope.content = "";
                    $fileContent = "";
                    vm.ext = true;
                }

               
            }
        };
 
        // Functions
        function activate() {
            _getSponsorListFromIndex(vm.sindex);
            _getSponsorTypes();
            vm.loading = true;
            vm.complementaryView = false;

        }


        vm.toggleModal = function (action) {
            if (action == "remove") {

                vm.obj.title = "Remove Sponsor",
                vm.obj.message1 = "This action will remove the sponsor. Are you sure you want to continue?",
                vm.obj.message2 = vm.sponsor.firstName + " " + vm.sponsor.lastName,
                vm.obj.label = "Sponsor Name:",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "Remove",
                vm.obj.cancelbutton = true,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
                vm.okFunc = vm.deleteSponsor;
                vm.cancelFunc;

            }
            if (action == "removeKeys") {

                vm.obj.title = "Remove Complementary Key",
                vm.obj.message1 = "This action will remove all complementary keys of this sponsor. Are you sure you want to continue?",
                vm.obj.message2 = "",
                vm.obj.label = "",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "Remove",
                vm.obj.cancelbutton = true,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
                vm.okFunc = vm.deleteSponsorComplemetaryKey;
                vm.cancelFunc;

            }
            if (action == "errorFile") {

                vm.obj.title = "File type error",
                vm.obj.message1 = "That file extention is not support by this system. Try one of these(png, jpg, ext, gif, jpeg, pic,pict)";
                
                vm.obj.message2 = "",
                vm.obj.label = "",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "",
                vm.obj.cancelbutton = true,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
                vm.okFunc="";
                vm.cancelFunc;

            }
            if (action == "removeKey") {

                vm.obj.title = "Remove Complementary Key",
                vm.obj.message1 = "This action will remove a complementary key. Are you sure you want to continue?",

                vm.obj.message2 = vm.keyPop,
                vm.obj.label = "Complementary Key:",
                vm.obj.okbutton = true,
                vm.obj.okbuttonText = "Remove",
                vm.obj.cancelbutton = true,
                vm.obj.cancelbuttoText = "Cancel",
                vm.showConfirmModal = !vm.showConfirmModal;
                vm.okFunc = vm.deleteComplemetaryKey;
                vm.cancelFunc;

            }
            else if (action == "error")
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

        function _selectedSponsor(sponsor, action) {

            vm.sponsor = JSON.parse(JSON.stringify(sponsor));
            //  vm.typeID = sponsor.sponsorType ;

        }

        function _selectedKey(key) {
            vm.key = key;
            vm.keyPop = key.key;
        }
        function _clearPic(File){
            $scope.content = "";
            $scope.$fileContent = "";
            File = undefined;
            vm.sponsor.logo = "";
            vm.ext = false;
            document.getElementById("inputFile").value = "";
      }

        function _clearSponsor() {
            vm.sponsor = null;
            $scope.content = "";
            $scope.$fileContent = "";
            document.getElementById("addSponsorForm").reset();

        }
        function _addValues() {
            vm.add = true;
            vm.edit = false;
            vm.view = false;
            vm.headerModal = "Add Sponsor";
           
            _clearSponsor();
            vm.sponsor = {};
            vm.sponsor.sponsorType = 1;
        }
        function _viewValues() {
            vm.view = true;
            vm.add = false;
            vm.edit = false;
            vm.headerModal = "View Sponsor";


        }
        function _editValues() {
            vm.edit = true;
            vm.add = false;
            vm.view = false;
            vm.headerModal = "Edit Sponsor";
            $scope.content = vm.sponsor.logo;
          //  vm.typeID = vm.sponsor.sponsorType;
  

        }
        function _complementaryValues(sponsor) {
            vm.complementaryview = true;
            vm.sponsor = sponsor;
            vm.cindex = 0;
            vm.loadingComp = true;
            _getSponsorComplementaryKeys(vm.cindex);
        }

        function _submitForm() {

            // check to make sure the form is completely valid


        };
        function _downloadLogo() {
            if (vm.sponsor.logo != undefined && vm.sponsor.logo != "")
                window.open(vm.sponsor.logo);

        }


        //--------------------------Complemetnary----------------------------------------
        function _getSponsorComplementaryKeys(index) {
            var info = {
                index: index,
                sponsorID: vm.sponsor.sponsorID
            }
            restApi.getSponsorComplementaryKeysFromIndex(info).
                   success(function (data, status, headers, config) {
                       vm.cmaxIndex = data.maxIndex;
                       if (vm.cmaxIndex == 0) {
                           vm.cindex = 0;
                           vm.sponsorKeys = [];
                       }
                       else if (vm.cindex >= vm.cmaxIndex) {
                           vm.cindex = vm.cmaxIndex - 1;
                           _getSponsorComplementaryKeys(vm.cindex);
                       }
                       else {
                           vm.sponsorKeys = data.results;
                       }
                       vm.loadingComp = false;
                   }).
                   error(function (data, status, headers, config) {
                       vm.loadingComp = false;
                       vm.toggleModal('error');

                   });
        }

        function _deleteComplemetaryKey() {

            vm.loadingRemovingComp = true;
          
            restApi.deleteComplemetaryKey(vm.key.complementarykeyID)
            .success(function (data, status, headers, config) {
                vm.sponsorKeys.forEach(function (key, index) {
                    if (key.complementarykeyID == vm.key.complementarykeyID) {
                        vm.sponsorKeys.splice(index, 1);
                        vm.loadingRemovingComp = false;
                        $('#delete').modal('hide');
                    }

                });

            })

            .error(function (data, status, headers, config) {
                vm.toggleModal('error');
                vm.loadingRemovingComp = false;
                $('#delete').modal('hide');
                _clearSponsor();

            });
        }

        //cambiar
        function _deleteSponsorComplemetaryKey() {

            vm.loadingRemovingComp = true;
            restApi.deleteSponsorComplemetaryKey(vm.sponsor.sponsorID)
            .success(function (data, status, headers, config) {
                vm.cmaxIndex = data.maxIndex;
                if (vm.cmaxIndex == 0) {
                    vm.cindex = 0;
                    vm.sponsorKeys = [];
                }
                else if (vm.cindex >= vm.cmaxIndex) {
                    vm.cindex = vm.cmaxIndex - 1;
                    _getSponsorComplementaryKeys(vm.cindex);
                }
                else {
                    vm.sponsorKeys = data.results;
                }
                vm.loadingRemovingComp = false;
            })

            .error(function (data, status, headers, config) {
                vm.toggleModal('error');
                vm.loadingRemovingComp = false;


            });
        }

        function _addComplementaryKey() {
            vm.addComplementaryObj.sponsorID = vm.sponsor.sponsorID;
            vm.addComplementaryObj.quantity = vm.quantity;
            vm.addComplementaryObj.company = vm.sponsor.company;
            vm.uploadingComp = true;
            restApi.addComplementaryKey(vm.addComplementaryObj)
                     .success(function (data, status, headers, config) {
                         vm.cmaxIndex = data.maxIndex;
                         if (vm.cmaxIndex == 0) {
                             vm.cindex = 0;
                             vm.sponsorKeys = [];
                         }
                         else if (vm.cindex >= vm.cmaxIndex) {
                             vm.cindex = vm.cmaxIndex - 1;
                             _getSponsorComplementaryKeys(vm.cindex);
                         }
                         else {
                             vm.sponsorKeys = data.results;
                         }
                         vm.uploadingComp = false;
                         vm.quantity = 0;

                     })

                     .error(function (error) {
                         vm.uploadingComp = false;
                         vm.quantity = 0;
                         vm.toggleModal('error');

                     });
        }

        function _nextComplimentary() {
            if (vm.cindex < vm.cmaxIndex - 1) {
                vm.cindex += 1;
                _getSponsorComplementaryKeys(vm.cindex);
            }
        }

        function _previousComplimentary() {
            if (vm.cindex > 0) {
                vm.cindex -= 1;
                _getSponsorComplementaryKeys(vm.cindex);
            }
        }

        function _getFirstComplimentaryPage() {
            vm.cindex = 0;
            _getSponsorComplementaryKeys(vm.cindex);
        }

        function _getLastComplimentaryPage() {
            vm.cindex = vm.cmaxIndex - 1;
            _getSponsorComplementaryKeys(vm.cindex);
        }

        //---------------------------Sponsor-------------------------------------------------
        function _addSponsor(File) {
       
            vm.TYPE = vm.sponsorsTypeList[vm.sponsor.sponsorType-1];
            vm.sponsor.typeName = vm.TYPE.name;
            if (File != undefined) {

                vm.sponsor.logo = $scope.content;
                vm.sponsor.logoName = File.name;
                File = null;
            }
                    vm.loadingUploading = true;
                    restApi.postNewSponsor(vm.sponsor)
                             .success(function (data, status, headers, config) {

                                 if (vm.sponsorsList.length < 10) {
                                     vm.sponsorsList.push(data);
                                 }

                                 else {
                                     _getSponsorListFromIndex(vm.sindex);
                                 }

                                 vm.loadingUploading = false;
                                 $('#addSponsor').modal('hide');
                                 _clearSponsor();


                             })

                             .error(function (error) {
                                 vm.loadingUploading = false;
                                 $('#addSponsor').modal('hide');
                                 vm.toggleModal('error');
                                 _clearSponsor();
                             });
            
        
            //else {
            //    vm.sponsor.logo = "";
            //    vm.sponsor.logoName = "Empty";
            //}
          
        }

        function _getSponsorTypes() {
            restApi.getSponsorTypesList().
                   success(function (data, status, headers, config) {
                       vm.sponsorsTypeList = data;
                       if (data != null)
                          vm.typeID =0;
                      
                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');

                   });
        }

        function _getSponsorListFromIndex(index) {
            restApi.getSponsorsListIndex(index).
                   success(function (data, status, headers, config) {
                       vm.smaxIndex = data.maxIndex;
                       if (vm.smaxIndex == 0) {
                           vm.sindex = 0;
                           vm.sponsorsList = [];
                       }
                       else if (vm.sindex >= vm.smaxIndex) {
                           vm.sindex = vm.smaxIndex - 1;
                           _getSponsorListFromIndex(vm.sindex);
                       }
                       else {
                           vm.sponsorsList = data.results;
                       }
                       vm.loading = false;

                   }).
                   error(function (data, status, headers, config) {
                       vm.toggleModal('error');
                       vm.loading = false;
                   });
        }

        function _nextSponsor() {
            if (vm.sindex < vm.smaxIndex - 1) {
                vm.sindex += 1;
                _getSponsorListFromIndex(vm.sindex);
            }
        }

        function _previousSponsor() {
            if (vm.sindex > 0) {
                vm.sindex -= 1;
                _getSponsorListFromIndex(vm.sindex);
            }
        }

        function _getFirstSponsorPage() {
            vm.sindex = 0;
            _getSponsorListFromIndex(vm.sindex);
        }

        function _getLastSponsorPage() {
            vm.sindex = vm.smaxIndex - 1;
            _getSponsorListFromIndex(vm.sindex);
        }

        function _updateSponsor(myFile) {
            
            
            vm.TYPE = vm.sponsorsTypeList[vm.sponsor.sponsorType-1];
            vm.sponsor.typeName = vm.TYPE.name;

            if (myFile != undefined) {
                vm.sponsor.logoName = myFile.name;
                vm.sponsor.logo = $scope.content;

            }

            vm.loadingUploading = true;
            restApi.updateSponsor(vm.sponsor)
            .success(function (data, status, headers, config) {
                vm.sponsorsList.forEach(function (sponsor, index) {
                    if (sponsor.sponsorID == vm.sponsor.sponsorID) {
                        vm.sponsorsList[index] = JSON.parse(JSON.stringify(data));
                    }
                    vm.loadingUploading = false;
                    $('#addSponsor').modal('hide');

                }); _clearSponsor();
            }

            )
            .error(function (data, status, headers, config) {
                vm.edit = false;
                _clearSponsor();
                vm.loadingUploading = false;
                $('#addSponsor').modal('hide');
                vm.toggleModal('error');
                _clearSponsor();
            });


        }

        function _deleteSponsor() {
            vm.loadingRemoving = true;
            restApi.deleteSponsor(vm.sponsor.sponsorID)
            .success(function (data, status, headers, config) {
                vm.sponsorsList.forEach(function (sponsor, index) {
                    if (sponsor.sponsorID == vm.sponsor.sponsorID) {
                        vm.sponsorsList.splice(index, 1);

                    }

                });
                vm.loadingRemoving = false;
                $('#delete').modal('hide');
            })

            .error(function (data, status, headers, config) {
                vm.toggleModal('error');
                vm.loadingRemoving = false;
                $('#delete').modal('hide');
                _clearSponsor();

            });
        }

        //Search Methods
        function _search(index) {
            if (vm.criteria != "" && vm.criteria != null) {
                var info = { index: index, criteria: vm.criteria };
                restApi.searchSponsors(info).
                       success(function (data, status, headers, config) {
                           vm.showSearch = true;
                           vm.searchMaxIndex = data.maxIndex;
                           if (vm.searchMaxIndex == 0) {
                               vm.searchIndex = 0;
                               vm.searchResults = [];
                               vm.showResults = false;
                           }
                           else if (vm.searchIndex >= vm.searchMaxIndex) {
                               vm.searchIndex = vm.searchMaxIndex - 1;
                               _search(vm.searchIndex);
                           }
                           else {
                               vm.showResults = true;
                               vm.searchResults = data.results;
                           }
                       }).
                       error(function (data, status, headers, config) {
                       });
            }
        }

        function _nextSearch() {
            if (vm.searchIndex < vm.searchMaxIndex - 1) {
                vm.searchIndex += 1;
                _search(vm.searchIndex);
            }
        }

        function _previousSearch() {
            if (vm.searchIndex > 0) {
                vm.searchIndex -= 1;
                _search(vm.searchIndex);
            }
        }

        function _getFirstSearch() {
            vm.searchIndex = 0;
            _search(vm.searchIndex);
        }

        function _getLastSearch() {
            vm.searchIndex = vm.searchMaxIndex - 1;
            _search(vm.searchIndex);
        }

        function _back() {
            vm.criteria = "";
            vm.searchIndex = 0;
            vm.searchResults = [];
            vm.showSearch = false;
            vm.showResults = false;
        }
    }
})();





