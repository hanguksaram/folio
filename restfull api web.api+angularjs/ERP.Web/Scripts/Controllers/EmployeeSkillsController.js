var EmployeeSkillsController = function ($scope, $routeParams, $filter, $element, EmployeeService, historyService,
                                         groupsOfSkillsService, certificateService) {
    $scope.employee = {};
    $scope.employeeSkills = {};
    $scope.history = [];
    function splitEmployeeSkillsFromGroups() {
        if (!$scope.groups)
            return;
        $scope.employeeSkills = [];
        $scope.groups.forEach(function (group) {
            var arr = [];
            group.Skills.forEach(function (item) {
                $scope.employee.Skills.forEach(function (skill) {
                    if (item.SkillName === skill.SkillName)
                        arr.push(skill);
                });
            });
            arr = $filter('orderBy')(arr, '-Level');
            $scope.employeeSkills.push({ group: group, skills: arr});
        });
    };
    function loadGroups() {
        if (!$scope.groups) {
            groupsOfSkillsService.getAll().then(
                function (response) {
                    $scope.groups = response;
                    splitEmployeeSkillsFromGroups();
                },
                function (error) { Core.error(error); }
            );
        }
        else {
            splitEmployeeSkillsFromGroups();
        }
    };
    function loadHistory() {
        historyService.getShortHistory($scope.employee.ID).then(
            function (response) {
                $scope.history = response;
                $scope.isShowMore = true;
            },
            function (error) { Core.error(error); }
        );
    }
    function loadEmployee() {
        EmployeeService.Read($routeParams.id).then(
            function (response) {
                $scope.employee = response;
                loadGroups();
                loadHistory();
            },
            function () { Core.error('Employee not load.'); }
        );
    };
    loadEmployee();
    $scope.saveEmployee = function () {
        EmployeeService.Update($routeParams.id, $scope.employee).then(
            function (response) {
                Core.notify('Employee saved.');
                loadEmployee();
            },
            function (error) { Core.error(error); }
        );
    };
    $scope.cancel = function () {
        loadEmployee();
    };
    
    $scope.isShowMore = true;
    $scope.showMore = function () {
        historyService.getHistory($scope.employee.ID).then(
            function (response) {
                $scope.history = response;
                $scope.isShowMore = false;
            },
            function (error) { Core.error(error); }
        );
    };

    $scope.insCertificate = null;
    $scope.certificates = [];
    loadCertificates = function() {
        if (!$scope.certificates.length) {
            certificateService.getAll().then(
                function (response) { $scope.certificates = response; },
                function (error) { Core.error(error); }
            );
        }
    };
    $scope.$watch('insCertificate.CertificateId', function (newVal, oldVal) {
        if (!newVal)
            return;
        if (newVal !== oldVal) {
            var selected = $filter('filter')($scope.certificates, { ID: $scope.insCertificate.CertificateId });
            $scope.insCertificate.CertificateName = selected.length ? selected[0].CertificateName : null;
        }
    });

    $element.find("#help-for-skills").popover({
        html: true,
        content: 'Enter skill in format <code>Skill name | Level | Preference</code><br/>'
               + 'Level <code>0..5</code><br/>'
               + 'Preference <code>0</code> or <code>1</code> (0 - no, 1 - yes, leave this field empty for "Not sure")<br/>'
               + '<br/>'
               + 'For example <code>C# | 5 | 1</code> or <code>JS | 4</code>',
        placement: 'right',
        container: 'body',
        trigger: 'hover'
    });

    $scope.addCertificate = function() {
        loadCertificates();
        $scope.insCertificate = {
            CertificateId: 0,
            CertificateName: null,
            ReceiptDate: null,
            ExpireDate: null,
            Pending: true
        };
    };
    $scope.cancelAddingCertificate = function () {
        $scope.insCertificate = null;
    };
    $scope.addCertificateToEmployee = function () {
        for (var i in $scope.certificates) {
            if ($scope.certificates[i].CertificateName == $scope.insCertificate.CertificateName) {
                $scope.insCertificate.CertificateId = $scope.certificates[i].ID;
                $scope.employee.Certificates.push($scope.insCertificate);
                $scope.insCertificate = null;
                break;
            }
        };
    };
    $scope.removeCertificate = function (certificate) {
        for (var i = 0; i < $scope.employee.Certificates.length; i++) {
            if (certificate === $scope.employee.Certificates[i]) {
                $scope.employee.Certificates.splice(i, 1);
                return;
            };
        };
    };
    $scope.getStatus = function (certificate) {
        var currentDate = new Date();
        if (certificate.ExpireDate !== null)
            if ((certificate.ExpireDate.getTime() + 24 * 3600000 - 1) >= currentDate.getTime())
                return 'Active';
            else
                return 'Expired';
        if (certificate.Pending)
            return 'Pending';
        return 'Expired';
    };
    
    $scope.loadSkills = function ($query, group) {
        return $query ? group.Skills.filter(function (skill) {
            for (var i in $scope.employee.Skills) {
                var item = $scope.employee.Skills[i];
                if (item.SkillName == skill.SkillName)
                    return false;
            };
            return skill.SkillName.toLowerCase().indexOf($query.toLowerCase()) != -1;
        }) : group.Skills;
    };
    $scope.onTagAdding = function (tag, $group) {
        var newSkill = {};
        var fields = tag.SkillName.split('|');
        newSkill.SkillName = fields[0].trim();
        newSkill.Level = parseInt(fields[1].trim());
        newSkill.Preference = (fields.length > 2) ? parseInt(fields[2].trim()) : 2;
        tag.Level = newSkill.Level;
        tag.Preference = newSkill.Preference;
        // if emloyee has skill yet return false
        for (var i in $scope.employee.Skills) {
            var skill = $scope.employee.Skills[i];
            if (skill.SkillName.toLowerCase() == newSkill.SkillName.toLowerCase()) {
                Core.error('Skill "' + skill.SkillName + '" already added.');
                return false;
            }
        };
        // if group contain skill add skill and rag
        for (var i in $group.Skills) {
            var skill = $group.Skills[i];
            if (skill.SkillName.toLowerCase() == newSkill.SkillName.toLowerCase()) {
                newSkill.SkillName = skill.SkillName;
                newSkill.SkillId = skill.ID;
                $scope.employee.Skills.push(newSkill);
                tag.SkillName = newSkill.SkillName;
                return true;
            }
        };
        // if if skill already exist
        for (var i in $scope.groups) {
            var group = $scope.groups[i];
            for (var j in group.Skills) {
                var skill = group.Skills[j];
                if (skill.SkillName.toLowerCase() == newSkill.SkillName.toLowerCase()) {
                    Core.error('Skill "' + skill.SkillName + '" already exist in gropup "' + group.GroupName + '".');
                    return false;
                }
            }
        }
        // add skill in database
        var updatedGroup = angular.copy($group);
        updatedGroup.Skills.push({ SkillName: newSkill.SkillName });
        return groupsOfSkillsService.update($group.ID, updatedGroup).then(function () {
            return groupsOfSkillsService.get($group.ID).then(function (response) {
                $group = response;
                for (var i in $group.Skills) {
                    var skill = $group.Skills[i];
                    if (skill.SkillName.toLowerCase() == newSkill.SkillName.toLowerCase()) {
                        newSkill.SkillId = skill.ID;
                        newSkill.SkillName = skill.SkillName;
                        $scope.employee.Skills.push(newSkill);
                        tag.SkillName = newSkill.SkillName;
                        $scope.employeeSkills.forEach(function (employeeSkill) {
                            if (employeeSkill.group.GroupName == $group.GroupName)
                                employeeSkill.group = $group;
                        });
                        for (var i in $scope.groups) {
                            if ($scope.groups[i].GroupName == $group.GroupName)
                                $scope.groups[i] = $group;
                        }
                        return true;
                    }
                };
                return false;
            });
        });
    };
    $scope.onTagClicked = function (tag) {
        //console.log(tag);
    };
    $scope.onTagAdded = function (tag) {
        //console.log(tag);
    };
    $scope.onTagRemoving = function (tag) {
        if (!tag)
            return false;
        $scope.employee.Skills.forEach(function (skill, key) {
            if (skill.SkillName == tag.SkillName) {
                $scope.employee.Skills.splice(key, 1);
                return true;
            }
        });
    };
};