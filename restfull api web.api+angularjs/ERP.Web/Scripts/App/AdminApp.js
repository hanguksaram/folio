var ERPAdminApp = angular.module('ERPAdminApp', ['ngRoute', 'angular-loading-bar', 'ngAnimate','multi-select', 'datatables', "xeditable"]).directive('match', function () {
    return {
        require: 'ngModel',
        restrict: 'A',
        scope: {
            match: '='
        },
        link: function (scope, elem, attrs, ctrl) {
            scope.$watch(function () {
                var modelValue = ctrl.$modelValue || ctrl.$$invalidModelValue;
                return (ctrl.$pristine && angular.isUndefined(modelValue)) || scope.match === modelValue;
            }, function (currentValue) {
                ctrl.$setValidity('match', currentValue);
            });
        }
    };
});

ERPAdminApp.controller('AdminPageController', AdminPageController);
ERPAdminApp.controller('UsersController', UsersController);
ERPAdminApp.controller('CompaniesController', CompaniesController);
ERPAdminApp.controller('EditCompanyController', EditCompanyController);
//ERPAdminApp.controller('WorkScheduleController', WorkScheduleController);

var configFunction = function ($routeProvider, $locationProvider) {
    $locationProvider.html5Mode(true).hashPrefix('!');

    $routeProvider.
        when('/admin', {
            templateUrl: 'dashboard/SA'
        })
        .when('/admin/schedule', {
            templateUrl: 'workschedule'
        })
        .when('/admin/companies', {
            templateUrl: 'companies'
        })
        .when('/admin/companies/add', {
            templateUrl: 'companies/edit/0'
        }).when('/admin/companies/edit/:id', {
            templateUrl: function (params) { return '/companies/edit/' + params.id; }
        })
        .when('/admin/users', {
            templateUrl: 'users'
        }).when('/logout', {
            templateUrl: "/account/logout"
        });
}
configFunction.$inject = ['$routeProvider', '$locationProvider'];

ERPAdminApp.config(configFunction);


//directives

ERPAdminApp.directive('convertToNumber', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$parsers.push(function (val) {
                return val != null ? parseInt(val, 10) : null;
            });
            ngModel.$formatters.push(function (val) {
                return val != null ? '' + val : null;
            });
        }
    };
});

//ERPAdminApp