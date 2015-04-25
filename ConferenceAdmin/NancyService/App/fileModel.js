angular.module('app').directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

angular.module('app').directive('onReadFile', function ($parse) {
    return {
        restrict: 'A',
        scope: false,
        link: function (scope, element, attrs) {
            var fn = $parse(attrs.onReadFile);

            element.on('change', function (onChangeEvent) {
                var reader = new FileReader();

                reader.onload = function (onLoadEvent) {
                    scope.$apply(function () {
                        fn(scope, { $fileContent: onLoadEvent.target.result });
                    });
                };

                reader.readAsDataURL((onChangeEvent.srcElement || onChangeEvent.target).files[0]);
            });
        }
    };
});

angular.module('app').directive('parseUrl', function () {
    var urlPattern = /(http|ftp|https):\/\/[\w-]+(\.[\w-]+)+([\w.,@?^=%&amp;:\/~+#-]*[\w@?^=%&amp;\/~+#-])?/gi;
    return {
        restrict: 'A',
        require: 'ngModel',
        replace: true,
        scope: {
            props: '=parseUrl',
            ngModel: '=ngModel'
        },
        link: function compile(scope, element, attrs, controller) {
            scope.$watch('ngModel', function (value) {
                if (value != undefined) {
                    var html = value.replace(urlPattern, '<a target="' + '_blank' + '" href="$&">$&</a>');
                    element.html(html);
                }
            });
        }
    };
});




