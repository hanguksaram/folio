var injectParamsSkillGroupService = ['$http'];

function SkillGroupService($http) {
    var url = "/api/groupsofskills";
    var SkillGroupService = {};

    // Service function for converting Date
   

    // Create
    SkillGroupService.Create = function (skillGroupData) {
        return $http.post(url, skillGroupData);
    }

    // Read
    SkillGroupService.Read = function() {
        // If method Read() has no arguments - download all employees from server.
        
            return $http
                .get(url)
                .then(
                    function success(response) {
                        var skillsGroupDataArray = response.data.response;
                        return skillsGroupDataArray;
                    });

    }

    

    return SkillGroupService;//
};

SkillGroupService.$inject = injectParamsSkillGroupService;
