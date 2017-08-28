var EmployeeEditController = function ($scope, $filter, $routeParams, EmployeeService, positionService, propertyTypeService) {
    var contactReg = /e-mail|email|phone|address|whatsapp|telegram|vk/i;
    var primaryContactReg = /Skype|E-mail|Additional e-mail|Phone/;

    $scope.employee = {};
    $scope.propertyTypes = [];
    function loadPropertyTypes() {
        if (!$scope.propertyTypes.length) {
            propertyTypeService.getAll().then(
                function (response) { $scope.propertyTypes = response; },
                function (error) { Core.error(error); }
            );
        }
    };
    function loadEmployee() {
        EmployeeService.Read($routeParams.id).then(
            function (response) { $scope.employee = response; },
            function () { Core.error('Employee not load.'); }
        );
    };
    loadEmployee();
    
    $scope.positions = [];
    $scope.loadPositions = function () {
        if (!$scope.positions.length) {
            positionService.getAll().then(
                function (response) { $scope.positions = response; },
                function (error) { Core.error(error); }
            );
        }
    };
    $scope.$watch('employee.Position.ID', function (newVal, oldVal) {
        if (!oldVal)
            return;
        if (newVal !== oldVal) {
            var selected = $filter('filter')($scope.positions, { ID: $scope.employee.Position.ID });
            $scope.employee.Position.PositionName = selected.length ? selected[0].PositionName : null;
        }
    });
    $scope.getTypeProperty = function (property) {
        var regEmail = /e-mail|email/i;
        var regBool = /^(is\s|has\s)/i;
        var regPhone = /phone|whatsapp|telegram/i;
        if (regEmail.test(property.PropertyType))
            return 'email';
        else if (regBool.test(property.PropertyType))
            return 'bool';
        else if (regPhone.test(property.PropertyType))
            return 'phone';
        else if (property.PropertyType == null)
            return 'null';
        else
            return 'text';
    };
    $scope.showProperties = false;
    $scope.$watch('employee.Properties.length', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            if (angular.isDefined($scope.employee.Properties)) {
                var props = $scope.employee.Properties.filter($scope.propertyFilter);
                $scope.showProperties = props.length ? true : false;
            }
        }
    });

    $scope.saveEmployee = function () {
        EmployeeService.Update($routeParams.id, $scope.employee).then(
            function (response) { Core.notify('Employee saved.'); },
            function (error) { Core.error(error); }
        );
    };
    $scope.cancel = function () {
        loadEmployee();
    };

    $scope.contactFilter = function (item) {
        return !primaryContactReg.test(item.PropertyType) && contactReg.test(item.PropertyType);
    };
    $scope.primaryContactFilter = function (item) {
        return primaryContactReg.test(item.PropertyType);
    };
    $scope.propertyFilter = function (item) {
        return !primaryContactReg.test(item.PropertyType) && !contactReg.test(item.PropertyType);
    };
    $scope.propertyTypeFilter = function (item) {
        return !primaryContactReg.test(item.PropertyTypeName) && !contactReg.test(item.PropertyTypeName);
    };
    $scope.contactTypeFilter = function (item) {
        return primaryContactReg.test(item.PropertyTypeName) || contactReg.test(item.PropertyTypeName);
    };
    $scope.alreadyAddedFilter = function (item) {
        for (var i in $scope.employee.Properties) {
            var prop = $scope.employee.Properties[i];
            if (item.PropertyTypeName == prop.PropertyType)
                return false;
        };
        return true;
    };
    $scope.primaryContactOrder = function (item) {
        switch (item.PropertyType) {
            case 'Skype':
                return 1;
            case 'Phone':
                return 2;
            case 'E-mail':
                return 3;
            case 'Additional e-mail':
                return 4;
            default:
                return 500;
        }
    };

    $scope.newProperty = null;
    $scope.addNewProperty = function () {
        loadPropertyTypes();
        $scope.newProperty = {
            PropertyType: null,
            PropertyValue: null
        };
    };
    $scope.cancelAddProperty = function () {
        $scope.newProperty = null;
    };
    $scope.addNewPropertyToEmployee = function () {
        $scope.propertyTypes.forEach(function (item) {
            if (item.PropertyTypeName == $scope.newProperty.PropertyType)
                $scope.newProperty.PropertyTypeId = item.ID;
        });
        if (/^(has\s|is\s)/i.test($scope.newProperty.PropertyType)) {
            if (/^(1|true|yes)$/i.test($scope.newProperty.PropertyValue.trim()))
                $scope.newProperty.PropertyValue = 'true';
            else 
                $scope.newProperty.PropertyValue = 'false';
        }
        $scope.employee.Properties.push($scope.newProperty);
        $scope.newProperty = null;
    };
    $scope.deletePropertyFromEmployee = function (property) {
        let propertiesArray = $scope.employee.Properties;
        for (let i = 0; i < propertiesArray.length; i++) {
            if (property.PropertyType === propertiesArray[i].PropertyType) {
                propertiesArray.splice(i, 1);
            }
        }
    };

    $scope.newContact = null;
    $scope.addNewContact = function () {
        loadPropertyTypes();
        $scope.newContact = {
            PropertyType: null,
            PropertyValue: null
        };
    };
    $scope.cancelAddContact = function () {
        $scope.newContact = null;
    };
    $scope.addNewContactToEmployee = function () {
        $scope.propertyTypes.forEach(function (item) {
            if (item.PropertyTypeName == $scope.newContact.PropertyType)
                $scope.newContact.PropertyTypeId = item.ID;
        });
        $scope.employee.Properties.push($scope.newContact);
        $scope.newContact = null;
    };
    
};