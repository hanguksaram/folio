angular.module('EmployeeApp', ['ngRoute', 'xeditable', 'ngTagsInput'])
.controller('EmployeeEditController', EmployeeEditController)
.controller('EmployeeSkillsController', EmployeeSkillsController)
.controller('EmployeeTableController', EmployeeTableController)
.constant('employeeURL', '/api/employees')
.constant('groupsOfSkillsURL', '/api/groupsofskills')
.constant('certificateURL', '/api/certificates')
.constant('positionURL', '/api/positions')
.constant('propertyTypeURL', '/api/propertytype')
.filter('preference', function () {
    return function (preference) {
        var value = parseInt(preference);
        if (!angular.isNumber(value))
            return preference;
        switch (value) {
            case 0:
                return 'no';
            case 1:
                return 'yes';
            default:
                return null;
        }
    };
})
.filter('localTimeFilter', function () {
    function UTCToLocalTime(UTCTime) {
        var offset = (new Date()).getTimezoneOffset() * 60000;
        return UTCTime - offset;
    }
    
    return function (MVCDate) {
        if (angular.isString(MVCDate)) {
            var time = parseInt(/\d+/.exec(MVCDate));
            return angular.isNumber(time) ? new Date(UTCToLocalTime(time)) : MVCDate;
        }
        else {
            return MVCDate;
        }
    };
})
.directive('employeeProp', function ($compile) {
    return {
        restrict: 'E',
        scope: {
            property: '=property',
            type: '=type',
            methodForButton: '&methodForButton'
        },
        link: function(scope) {
            scope.values = [{ value: 'false', text: 'no' }, { value: 'true', text: 'yes' }];
            scope.getText = function(value) {
                for (var i in scope.values) {
                    if (scope.values[i].value == value)
                        return scope.values[i].text;
                }
                return 'not set';
            }
        },
        templateUrl: 'Content/Templates/Dashboard/property-template.html'
    };
})
.directive('inputEmployeeProp', function ($compile) {
    return {
        restrict: 'E',
        scope: {
            property: '=property',
            type: '=type',
        },
        templateUrl: 'Content/Templates/Dashboard/input-property.html'
    };
})
.service('historyService', function ($http, $q, employeeURL) {
    return {
        getShortHistory: function (employeeId) {
            return $http.get(employeeURL + '/' + employeeId + '/shorthistory').then(
                function (data) {
                    return data.data.success ? data.data.response : $q.reject('History not load.');
                },
                function () { return $q.reject('History not load.'); }
            );
        },
        getHistory: function (employeeId) {
            return $http.get(employeeURL + '/' + employeeId + '/history').then(
                function (data) {
                    return data.data.success ? data.data.response : $q.reject('History not load.');
                },
                function () { return $q.reject('History not load.'); }
            );
        }
    };
})
.service('groupsOfSkillsService', function ($http, $q, groupsOfSkillsURL) {
    return {
        getAll: function () {
            return $http.get(groupsOfSkillsURL).then(
                function (data) {
                    return data.data.success ? data.data.response : $q.reject('Groups of skills not load.');
                },
                function () { return $q.reject('Groups of skills not load.'); }
            );
        },
        get: function (id) {
            return $http.get(groupsOfSkillsURL + '/' + id).then(
                function (data) {
                    return data.data.success ? data.data.response : $q.reject('Group of skills not load.');
                },
                function () { return $q.reject('Group of skills not load.'); }
            );
        },
        update: function (id, group) {
            return $http.put(groupsOfSkillsURL + '/' + id, group).then(
                function (data) {
                    return data.data.success ? data.data.response : $q.reject('Group of skills not updated.');
                },
                function () { return $q.reject('Group of skills not updated.'); }
            );
        }
    };
})
.service('certificateService', function ($http, $q, certificateURL) {
    return {
        getAll: function () {
            return $http.get(certificateURL).then(
                function (data) {
                    return data.data.success ? data.data.response : $q.reject('Certificates not load.');
                },
                function () { return $q.reject('Certificates not load.'); }
            );
        }
    };
})
.service('positionService', function ($http, $q, positionURL) {
    return {
        getAll: function () {
            return $http.get(positionURL).then(
                function (data) {
                    return data.data.success ? data.data.response : $q.reject('Positions not load.');
                },
                function () { return $q.reject('Positions not load.'); }
            );
        }
    };
})
.service('propertyTypeService', function ($http, $q, propertyTypeURL) {
    return {
        getAll: function () {
            return $http.get(propertyTypeURL).then(
                function (data) {
                    return data.data.success ? data.data.response : $q.reject('Property types not load.');
                },
                function () { return $q.reject('Property types not load.'); }
            );
        }
    };
}).service('EmployeeService', EmployeeService)
.service('PositionService', PositionService)
.service('SkillService', SkillService)
.service('CertificateService', CertificateService)
.service('SkillGroupService', SkillGroupService)
.config(function ($routeProvider) {
    $routeProvider.when('/app/dashboard/employeecontacts', {
            templateUrl: 'Content/Templates/Dashboard/ContactsTable.html',
            controller: 'ContactsTableController',
            controllerAs: 'Controller'
        }).
        when('/app/dashboard/employee/contacts/:id', {
            templateUrl: 'Content/Templates/Dashboard/employeecontacts.html'
        }).
        when('/app/dashboard/employee/skills/:id', {
            templateUrl: 'Content/Templates/Dashboard/employeeskills.html'
        }).
        when('/app/dashboard/employeeskills', {
            templateUrl: '/Content/EmployeeTable.html',
            controller: 'EmployeeTableController'
        });
});

angular
    .module('EmployeeApp')
    .filter('myTransformMvcDateToJsDate', myTransformMvcDateToJsDate)
    .service('EmployeeService', EmployeeService)
    .service('EmployeeContactsService', EmployeeContactsService)
    .controller('ContactsTableController', ContactsTableController);