var injectParamsCertificateService = ['$http'];

function CertificateService($http) {
    var url = "/api/certificates";
    var CertificateService = {};

    // Service function for converting Date
   

    // Create
    CertificateService.Create = function (certificateData) {
        return $http.post(url, certificateData);
    }

    // Read
    CertificateService.Read = function() {
        // If method Read() has no arguments - download all employees from server.
        
            return $http
                .get(url)
                .then(
                    function success(response) {
                        var certificateDataArray = response.data.response;
                        return certificateDataArray.map(s => s.CertificateName);
                    });

    }

    

    return CertificateService;//
};

CertificateService.$inject = injectParamsCertificateService;
