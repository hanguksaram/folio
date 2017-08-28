var injectParamsEmployeeService = ['$http', '$q', 'myTransformMvcDateToJsDateFilter'];

function EmployeeService($http, $q, myTransformMvcDateToJsDateFilter) {
    var url = "/api/employees";
    var EmployeeService = {};

    // Service function for converting Date
    function convertDate (employeeData) {

        if (employeeData.BirthDate) {
            employeeData.BirthDate = myTransformMvcDateToJsDateFilter(employeeData.BirthDate);
        }
        if (employeeData.FiredDate) {
            employeeData.FiredDate = myTransformMvcDateToJsDateFilter(employeeData.FiredDate);
        }
        if (employeeData.HiringDate) {
            employeeData.HiringDate = myTransformMvcDateToJsDateFilter(employeeData.HiringDate);
        }

        if (employeeData.Certificates) {
            for (var i = 0; i < employeeData.Certificates.length; i++) {
                if (employeeData.Certificates[i].ExpireDate) {
                    employeeData.Certificates[i].ExpireDate = myTransformMvcDateToJsDateFilter(employeeData.Certificates[i].ExpireDate);
                }
                if (employeeData.Certificates[i].ReceiptDate) {
                    employeeData.Certificates[i].ReceiptDate = myTransformMvcDateToJsDateFilter(employeeData.Certificates[i].ReceiptDate);
                }
            }
        }

    }

    // Create
    EmployeeService.Create = function (employeeData) {
        return $http.post(url, employeeData);
    }

    // Read
    EmployeeService.Read = function (id) {
        // If method Read() has no arguments - download all employees from server.
        if (arguments.length === 0) {
            return $http
                .get(url)
                .then(
                    function success(response) {
                        var employeeDataArray = response.data.response;

                        //TODO: Delete if fix Date-format on server.
                        for (var i = 0; i < employeeDataArray.length; i++) {
                            convertDate(employeeDataArray[i]);
                        }

                        return employeeDataArray;
                    });
        }
            // If method Read(id) has id-argument - download one employee from server by id.
        else {
            return $http
            .get(url + "/" + id)
            .then(
                function success(response) {
                    var employeeData = response.data.response;

                    //TODO: Delete if fix Date-format on server.
                    convertDate(employeeData);

                    return employeeData;
                }
            );
        }
    }

    // Update
    EmployeeService.Update = function (id, employeeData) {
        return $http.put(url + "/" + id, employeeData).then(
            function (data) {
                return data.data.success ? data.data.response : $q.reject('Employee not saved.');
            },
            function () { return $q.reject('Employee not saved.'); }
        );
    }

    // Delete
    EmployeeService.Delete = function (id) {
        return $http.delete(url + "/" + id);
    }

    return EmployeeService;
};

EmployeeService.$inject = injectParamsEmployeeService;