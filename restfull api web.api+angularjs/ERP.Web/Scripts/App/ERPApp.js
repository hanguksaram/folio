var ERPApp = angular.module('ERPApp', ['ngRoute','ui.grid', 'ui.grid.pinning', 'ngTagsInput', 'ui.grid.cellNav', 'angular-loading-bar', 'datatables', 'multi-select', "xeditable", 'ngAnimate', 'EmployeeApp']).directive('match', function () {
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

/*ERPApp.config(function(formValidationDecorationsProvider, formValidationErrorsProvider) {
    formValidationDecorationsProvider
        .useBuiltInDecorator('bootstrap');
    formValidationErrorsProvider
        .useBuiltInErrorListRenderer('bootstrap');
    formValidationErrorsProvider.setLanguage('en');
});*/
ERPApp.directive('navigatable', function() {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            
            element.on('keydown', function (e) {
                var key = e.keyCode ? e.keyCode : e.which;
                var arrow = { left: 37, up: 38, right: 39, down: 40 };
                if ($.inArray(key, [arrow.left, arrow.up, arrow.right, arrow.down]) >= 0) {
                        var input = e.target;
                        var td = $(e.target).closest('td');
                        var moveTo = null;

                        switch (key) {
                            case arrow.left:
                                {
                                    if (input.selectionStart == 0) {
                                        moveTo = td.prevAll().filter(':has(a[e-navigatable]):visible').first();
                                    }
                                    break;
                                }
                            case arrow.right:
                                {
                                    if (input.selectionEnd == input.value.length) {
                                        moveTo = td.nextAll().filter(':has(a[e-navigatable]):visible').first();
                                    }
                                    break;
                                }
                            case arrow.up:
                            case arrow.down:
                                {
                                    if (!$(input).is('input'))
                                        if (input.selectionEnd != input.value.length && input.selectionStart != 0)
                                            break;
                                    var tr = td.closest('tr');
                                    var pos = td[0].cellIndex;

                                    var moveToRow = null;
                                    if (key == arrow.down) {
                                        moveToRow = tr.next('tr:has(a[e-navigatable])');
                                        if (moveToRow.length == 0) {
                                            moveToRow = tr.closest('tbody').next().find('tr:has(a[e-navigatable])').first();
                                        }
                                    } else if (key == arrow.up) {
                                        moveToRow = tr.prev('tr:has(a[e-navigatable])');
                                        if (moveToRow.length == 0) {
                                            moveToRow = tr.closest('tbody').prev().find('tr:has(a[e-navigatable])').last();
                                        }
                                    }

                                    if (moveToRow.length) {
                                        moveTo = $(moveToRow[0].cells[pos]);
                                    }

                                    break;
                                }
                        }
                        if (moveTo && moveTo.length) {

                            e.preventDefault();

                            moveTo.find('a').each(function (i, input) {
                                input.click();
                            });

                        }
                } else if (key === 13) {
                    if (!$(e.target).is('input'))
                        return;
                    var td = $(e.target).closest('td');
                    var next = td.nextAll().filter(':has(a[e-navigatable]):visible').first();
                    if (next.length == 0) {
                        var tr = td.closest('tr');
                        moveToRow = tr.next('tr:has(a[e-navigatable])');
                        if (moveToRow.length == 0) {
                            moveToRow = tr.closest('tbody').next().find('tr:has(a[e-navigatable])').first();
                        }
                        if (moveToRow.length != 0) {
                            next = moveToRow.find('td:visible').filter(':has(a[e-navigatable])').first();
                        }
                    }
                    if (next.length != 0) {
                        next.find('a').click();
                    }

                }
            });
        }
    };
});

ERPApp.directive('selectOnFocus', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.on('focus', function () {
                this.select();
            });
        }
    };
});
ERPApp.directive('selectOnClick', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.on('click', function () {
                this.select();
            });
        }
    };
});
ERPApp.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            modelCtrl.$parsers.push(function (inputValue) {
                // this next if is necessary for when using ng-required on your input. 
                // In such cases, when a letter is typed first, this parser will be called
                // again, and the 2nd time, the value will be undefined
                if (inputValue == undefined) return ''
                var transformedInput = inputValue.replace(/[^0-9+.]/g, '');
                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }

                return transformedInput;
            });
        }
    };
});
ERPApp.directive('ssUnique',function ($parse) {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                function validate(value) {                  
                    var model = $parse(attrs.section);
                    var modelIndex = $parse(attrs.sindex);
                    var index = modelIndex(scope);//index of el in array
                    var m = model(scope).SubSections;
                    var uniq = true;
                    for (var i = 0; i < m.length; i++) {
                        if (i != index) {
                            if (m[i].Name == value) {
                                uniq = false;
                            }
                        }
                    }
                    ngModel.$setValidity('ssUnique', uniq);
                    return value;
                }
                ngModel.$parsers.unshift(validate);
                ngModel.$formatters.unshift(validate);
            }
        }
    });
ERPApp.filter('accountsFilter', [function () {
    return function (clients, selectedCompany) {        
        if (!angular.isUndefined(clients) && !angular.isUndefined(selectedCompany) && selectedCompany.length > 0) {
            var tempClients = [];
            angular.forEach(selectedCompany, function (id) {
                angular.forEach(clients, function (client) {
                    if (angular.equals(client.AccountId, id)) {
                        tempClients.push(client);
                    }
                });
            });
            return tempClients;
        } else {
            return clients;
        }
    };
}]);
ERPApp.filter('newlines', function() {
    return function(text) {
        if (text && text.length) {
            return text.replace(/\n/g, '<br/>');
        }
        return "";
    }
});
ERPApp.controller('MainController', MainController);

ERPApp.service('EmployeeService', EmployeeService);
ERPApp.service('PositionService', PositionService);
ERPApp.service('SkillService', SkillService);
ERPApp.service('CertificateService', CertificateService);
ERPApp.service('SkillGroupService', SkillGroupService);
ERPApp.filter('myTransformMvcDateToJsDate', myTransformMvcDateToJsDate);
ERPApp.controller('EmployeeTableController', EmployeeTableController);


var configFunction = function($routeProvider, $locationProvider) {
    $locationProvider.html5Mode(true).hashPrefix('!');

    $routeProvider.
        when('/app/dashboard', {
            templateUrl: 'dashboard/User'
        }).
        when('/app/invoices/', {
            templateUrl: '/invoices'
        }).
        when('/app/invoices/edit/:id', {
            templateUrl: function(params) { return '/invoices/edit/' + params.id; }
        }).
        when('/app/invoices/show/:id', {
            templateUrl: function(params) { return '/invoices/show/' + params.id; }
        }).
        when('/app/invoices/send/:id', {
            templateUrl: function(params) { return '/invoices/send/' + params.id; }
        }).
        when('/app/', {
            templateUrl: 'dashboard/User'
        })
        .when('/app/accounts', {
            templateUrl: 'accounts'
        })
        .when('/app/effficiency', {
            templateUrl: 'effficiency'
        })
        .when('/app/accounts/add', {
            templateUrl: 'accounts/edit/0'
        }).when('/app/accounts/edit/:id', {
            templateUrl: function(params) { return '/accounts/edit/' + params.id; }
        }).when('/app/agreenments/edit/:id', {
            templateUrl: function(params) { return '/agreenment/edit/' + params.id; }
        }).when('/app/agreenments/add/:id', {
            templateUrl: function(params) { return '/agreenment/add/' + params.id; }
        }).when('/logout', {
            templateUrl: "/account/logout"
        }).when('/app/reports/create', {
            templateUrl: '/reports/create'
        }).when('/app/reports/validate/:status/:id/:from/:to', {
            templateUrl: function(params) {
                var st = params.status == "billing" ? 0 : 1;
                return '/reports/validate/?status=' + st + '&agreenmentId=' + params.id + "&start=" + params.from + "&end=" + params.to;
            }
        }).when('/app/reports/remainingHours/:agreementID', {
            templateUrl: function(params) {
                return '/reports/remainingHours/?agreementId=' + params.agreementID;
            }
        }).when('/app/reports/list/', {
            templateUrl: '/reports/list'
        }).when('/app/reports/list/byAccount/:accountID', {
            templateUrl: function(params) { return '/reports/list/?accountID=' + params.accountID; }

        }).when('/app/reports/list/byAgreement/:agreementID', {
            templateUrl: function(params) { return '/reports/list/?agreementID=' + params.agreementID; }

        })
        .when('/app/reports/show/:id', {
            templateUrl: function(params) { return '/reports/view/' + params.id; }

        }).when('/app/invoices/send/:id', {
            templateUrl: function(params) { return '/invoices/sendEmail/' + params.id; }
        }).when('/app/KPIreports/', {
            templateUrl: "/KPIreports"
        })
        .when('/app/KPIreports/Details/:id/:from/:to/:agrementId/:onlyBillable', {
            templateUrl: function(params) { return '/KPIreports/Details/?userId=' + params.id + "&from=" + params.from + "&to=" + params.to + "&onlyBillable=" + params.onlyBillable + "&agrementId=" + params.agrementId; }
        })
        .otherwise({
            //templateUrl: 'dashboard/User'
        
        });
}
configFunction.$inject = ['$routeProvider', '$locationProvider'];

ERPApp.config(configFunction);

ERPApp.run(function (editableOptions) {
    editableOptions.theme = 'bs3';

});