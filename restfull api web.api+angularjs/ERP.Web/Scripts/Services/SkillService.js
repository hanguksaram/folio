var injectParamsSkillService = ['$http'];

function SkillService($http) {
    var url = "/api/skills";
    var SkillService = {};

    // Service function for converting Date
   

    // Create
    SkillService.Create = function (skillData) {
        return $http.post(url, skillData);
    }

    // Read
    SkillService.Read = function() {
        // If method Read() has no arguments - download all employees from server.
        
            return $http
                .get(url)
                .then(
                    function success(response) {
                        var skillsDataArray = response.data.response;
                        return skillsDataArray.map(s => ({ text: s.SkillName }));
                    });

    }

    

    return SkillService;//
};

SkillService.$inject = injectParamsSkillService;
