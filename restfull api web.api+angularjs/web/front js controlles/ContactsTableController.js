var injectParamsContactsTableController = ['$scope', 'EmployeeContactsService'];

function ContactsTableController($scope, EmployeeContactsService) {

    $scope.uriToEmployee = '/app/dashboard/employee/contacts/';

    $scope.sortType = 'FullName';
    $scope.sortReverse = false;
    $scope.searchEmployeeName = '';
    
    EmployeeContactsService.Read()
        .then(
            function success(response) {
                $scope.employees = response;
            }
        );

}

ContactsTableController.$inject = injectParamsContactsTableController;