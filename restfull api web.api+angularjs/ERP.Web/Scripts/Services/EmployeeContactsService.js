var injectParamsEmployeeContactsService = ['EmployeeService'];


function EmployeeContactsService(EmployeeService) {

    var EmployeeContactsService = {};

    /*
     * Returns promis with
     * [
     *      {
     *          EmployeeId      : 1
     *          FullName        : "Ivan Ivanov"
     *          Skype           : "ivanov@skype"
     *          Phone           : "+71231234567"
     *          Email           : "ivanov@velvetech.com"
     *          AdditionalEmail : "ivanov@gmail.com"
     *      }
     *      { .. }
     *      { .. }
     * ]
     */
    EmployeeContactsService.Read = function() {
        return EmployeeService.Read()
            .then(
                function success(response) {
                    var employeeContactsArray = [];
                    
                    for (let i = 0; i < response.length; i++) {
                        var employeeContacts = {};

                        employeeContacts['EmployeeId'] = response[i].ID;
                        employeeContacts['FullName'] = response[i].FirstName + ' ' + response[i].LastName;
                        
                        for (let j = 0, properties = response[i].Properties; j < properties.length; j++) {
                            switch (properties[j].PropertyType) {
                                case 'Phone':
                                    employeeContacts['Phone'] = properties[j].PropertyValue;
                                    break;
                                case 'E-mail':
                                    employeeContacts['Email'] = properties[j].PropertyValue;
                                    break;
                                case 'Additional e-mail':
                                    employeeContacts['AdditionalEmail'] = properties[j].PropertyValue;
                                    break;
                                case 'Skype':
                                    employeeContacts['Skype'] = properties[j].PropertyValue;
                                    break;
                                default:
                                    break;
                            }
                        }

                        employeeContactsArray = employeeContactsArray.concat(employeeContacts);
                    }
                    
                    return employeeContactsArray;
                }
            );
    }

    return EmployeeContactsService;
};

EmployeeContactsService.$inject = injectParamsEmployeeContactsService;