var injectParamsPositionService = ['$http'];

function PositionService($http) {
    var url = "/api/positions";
    var PositionService = {};

    // Service function for converting Date
   

    // Create
    PositionService.Create = function (positionData) {
        return $http.post(url, positionData);
    }

    // Read
    PositionService.Read = function() {
        // If method Read() has no arguments - download all employees from server.
        
            return $http
                .get(url)
                .then(
                    function success(response) {
                        var positionDataArray = response.data.response;
                        return positionDataArray.map(p => p.PositionName);
                    });

    }

    

    return PositionService;//
};

PositionService.$inject = injectParamsPositionService;