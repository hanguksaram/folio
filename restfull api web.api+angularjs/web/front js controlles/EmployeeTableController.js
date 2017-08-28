var EmployeeTableController = function ($scope, $location, EmployeeService, PositionService, SkillService, CertificateService, SkillGroupService, uiGridConstants) {
    var skillrespone;
    $scope.ResetAllFilters = function () {
        $scope.selectedPosition = undefined;
        $scope.filterValue = new String();

        var bak = angular.copy($scope.skillsGroup);
        $scope.filterGroups.GroupName = "";

        $scope.skillsGroup = bak;

        $scope.skillsForFilter = [];
       // console.log($scope.filterValue);
       // console.log("MALOVE");

    };
    $scope.loadSkills = function ($query) {
        if ($scope.autocompleteSkills === undefined || $scope.filterGroups.GroupName === "") {
            console.log(skillrespone);
            return skillrespone.filter(function(skill) {
                return skill.text.toLowerCase().indexOf($query.toLowerCase()) != -1;
            });
        }
            //else if (filterGroups.GroupName === "") {
       // return countries.filter(function(country) {
          //  return country.name.toLowerCase().indexOf($query.toLowerCase()) != -1;
            
        //}
        else
        {
            
            return $scope.autocompleteSkills.filter(function (skill) {
                return skill.text.toLowerCase().indexOf($query.toLowerCase()) != -1;
            });
        }
    }
    let specifiedEmployee = [];
    //var savedTagSkill;
    //$scope.autocompleteSkills = [];
        $scope.columns = [
        { name: 'id', width: 50, enablePinning: false, },
        {
            name: 'name',
            width: 200,
            pinnedLeft: true,
            displayName: 'name',
            field: 'name',
            enableFiltering: false,
            cellTemplate: '<div class="ui-grid-cell-contents" ><a href="/app/dashboard/employee/skills/{{row.entity.id}}">{{grid.getCellValue(row, col)}}</a></div>'
        // filter: {
        // flags: { caseSensitive: true }
        //  }
    },
    ];
    var employeeforfilter;
    $scope.filterGroups = [];
    $scope.skillsGroup = [];
    $scope.selectedPosition;
    $scope.positions = [{ positionName: "petyya" }, { positionName: "vasya" }];//PositionService.Read();
    $scope.skillsForFilter = [];
    $scope.$watch('selectedPosition', function () {
        console.log($scope.selectedPosition);
        $scope.gridApi.grid.refresh();

    });
    $scope.$watch('skillsForFilter', function () {
        console.log('halo');
        $scope.$apply();
        $scope.gridApi.grid.refresh();
        //   $scope.$digest();
    });
    $scope.$watch('filterValue', function () {
        console.log($scope.filterValue);
        $scope.gridApi.grid.refresh();
    })
    //console.log('Our test:', 
    PositionService.Read()
        .then(function success(response) {
            console.log(response);
            $scope.positions = response;
        });
    //    {
    //        skillName: "123",
    //        lastName: [
    //            { FirstName: "Galina", SecondName: "Petya" },
    //            { FirstName: "alina", LastName: "Grigorieva" }
    //        ]
    //    },
    //
    //    {
    //        skillName: "153",
    //        lastName: [
    //            { FirstName: "Lena", SecondName: "Petya" },
    //            { FirstName: "alina", LastName: "Grigorieva" }
    //        ]
    //    }
    //].map(s => s.lastName).some(f => f.FirstName === "Lena"));

    $scope.gridOptions = {
        //  minRowsToShow: 15,
        virtualizationThreshold: 100,
        enableFiltering: true,
        enableRowSelection: true,
        enableColumnMenu: false,
        columnDefs: $scope.columns,
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
            $scope.gridApi.grid.registerRowsProcessor($scope.singleFilter, 200);

            // $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row) {
            //    alert('BABAH');
            //  $location.path("contact.details.view");
            // });

        },
        // filter: {
        // flags: { caseSensitive: true }
        // }
    };
    $scope.UnPinIt = function($tag) {
        if ($scope.skillsForFilter !== undefined) {
            console.log($tag);
            var some = $scope.columns.find(c => c.name === $tag.text);
            //console.log(savedTagSkill);
            //console.log(some);
            var ind = $scope.columns.findIndex(c => c.name === $tag.text);
            //some['pinnedLeft'] = undefined;
            //some['filters'] = undefined;
           // console.log($scope.columns);
            // $scope.columns.pop();
            console.log($scope.skillsForFilter);
          //  $scope.columns[ind].filters = undefined;
            $scope.columns.sort((a, b) => a.name > b.name);
            
           //$scope.columns.push({ name: some.width, field: some.width, width: some.width });//$scope.gridApi.grid.();
           // $scope.columns.push({ name: some.width, field: some.width, width: some.width });
            //console.log()
            //console.log($scope.columns);
            // $scope.columns.splice(ind, 1, { name: some.name, field: some.field, width: 100, unpin: true });
            //  $scope.column.push({ name: some.name, field: some.field, width: 100 });
            //console.log($scope.skillsForFilter);
           $scope.gridApi.grid.refresh();
           //grid.api.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
            //$scope.$apply();
            // $scope.gridOptions.data = specifiedEmployee;
            //console.log($scope.columns.find(c => c.name === some.name));
            // console.log(some);
            //console.log($scope.columns.length);

        }

    }
    $scope.add
    $scope.filterIt = function() {
        if ($scope.skillsForFilter !== undefined) {
          //  console.log($scope.skillsForFilter);
            savedTagSkill = $scope.skillsForFilter[length - 1];
            for (let skill of $scope.skillsForFilter) {
               // console.log($scope.columns.find(c => c.name === skill.text));
                var some = $scope.columns.find(c => c.name === skill.text);
                var ind = $scope.columns.findIndex(c => c.name === skill.text);
                
               // console.log(some);
               // some['pinnedLeft'] = true;
                //some['filters'] = [
                   // {
                     //   condition: uiGridConstants.filter.GREATER_THAN,
                     //   placeholder: 'zgreater than'
                   // }
               // ];
               // console.log(some);
               // console.log($scope.columns.length);
                $scope.columns.splice(ind, 1);
                $scope.columns.unshift(some);
                //$scope.columns.sort();
                //  console.log($scope.columns.length);
                // console.log($scope.columns.find(c => c.name === skill.text));
            } // $scope.skillsForFilter
            //$scope.columns.push({ field: 'company', enableSorting: false, pinnedLeft: true });

            // $scope.gridApi.grid.refresh();
            //alert("fILTERED");
            $scope.gridApi.grid.refresh();
        }
    };
    // $scope.$watch("skillsForFilter", function () {
    //    
    // });


    $scope.singleFilter = function (renderableRows) {
        let employeeFilter = true;
        var matcher = $scope.filterValue;
        matcher = new RegExp(matcher, "ig");


        renderableRows.forEach(function (row) {
            var sometest;
            var test1;
            var test2;
            var booleanSuper = true;

            if ($scope.skillsForFilter[length] === 0 && ($scope.filterValue[length] === 0) ) {
                (function() {
                    row.visible = true;
                })
                ();
            }
            if ($scope.skillsForFilter !== null) {
                for (let i = 0; i < $scope.skillsForFilter.length; i++) {
                    let employee = employeeforfilter.filter(e => e.ID === row.entity.id);
                    
                    for (let skill of $scope.skillsForFilter) {
                        let contains = false;
                        for (let s of employee[0].Skills) {
                            if (skill.text == s.SkillName)
                                contains = true;
                        }
                        if (!contains) {
                            row.visible = false;
                            break;
                        }
                    }
                    


                    //if (test1[0].Skills.findIndex(s => s.SkillName === $scope.skillsForFilter[i].text) === -1) {
                    //    row.visible = false;
                    //    break;
                    //}
                
                }
            }
     //      if (angular.isDefined($scope.filterGroups) && $scope.filterGroups.Skills != null && $scope.filterGroups.Skills.length > 0) {
     //          //console.log()
     //          let some = $scope.filterGroups.Skills.map(s => s.SkillName);
     //          for (let ski of some) {
     //              for (e of employeeforfilter) {
     //                  let mn = (e.Skills.some(s => s.SkillName === ski));
     //                  console.log(mn);
     //                    // if (!mn) {
     //                        // console.log(mn);
     //                        // console.log("row" + row);
     //                         row.visible = false;
     //                    // }
     //              } 
     //          }
     //      }
            if ($scope.selectedPosition !== undefined) {
                test1 = employeeforfilter.filter(e => e.ID === row.entity.id);
                if (test1[0].Position.PositionName !== $scope.selectedPosition)
                    row.visible = false;
               
            };
         
            if ($scope.filterValue !== undefined) {
                var match = false;
                ['name'].forEach(function (field) {
                    if (row.entity[field].match(matcher)) {
                        match = true;
                    }
                });
                if (!match) {
                    row.visible = false;
                }
            }
            if (angular.isDefined($scope.filterGroups) && $scope.filterGroups.Skills != null && $scope.filterGroups.Skills.length > 0) {
                let skills = $scope.filterGroups.Skills.map(s => s.SkillName);
                let employee = employeeforfilter.filter(e => e.ID === row.entity.id);
                let contains = false;
                for (let skill of skills) {
                    for (let s of employee[0].Skills) {
                        if (skill == s.SkillName)
                            contains = true;
                    }
                }
                if (!contains) 
                    row.visible = false;
            }
        });
        return renderableRows;
    };


    


    // $scope.gridOptions = {};
    $scope.$watch("filterGroups", function () {


        $scope.gridApi.grid.refresh();
        //$scope.autocompleteSkills.push("HelloWorld");
        //$scope.autocompleteSkills = $scope.filterGroups.Skills.map(s => ({ text: s.SkillName }));//.reduce((a,x) => a["skillName"] = x, 0);
        //   console.log($scope.autocompleteSkills);
    });

    SkillGroupService.Read()
        .then(function success(response) {
            $scope.skillsGroup = response;//response.map(g => g.GroupName);
            $scope.skillsGroup.push({ GroupName: "" });
            //   console.log($scope.skillsGroup); // $scope.skillsGroup = response;
        });
    let some = [];
    SkillService.Read()
        .then(function success(response) {
            let i = 0;
            for (let skill of response) {
                skillrespone = response;
                $scope.columns.push({
                    name: skill.text, field: skill.text, width: 100, filters: [
                    {
                        condition: uiGridConstants.filter.GREATER_THAN,
                        placeholder: 'greater than'
                    }
               ] 
                });
                i++;

            };
        });
    CertificateService.Read()
        .then(function succes(response) {
            let i = 0;
            for (let certificate of response) {
                $scope.columns.push({ name: certificate, field: certificate, width: 100, enableFiltering: false }); //pinnedRight: true });
                i++;
            };
        });


 //  $scope.gridOptions.columnDefs = [
 // { name: 'id', width: 50, enablePinning: false },
 //      {
 //          name: 'name', width: 200, pinnedLeft: true,
 //          displayName: 'name',
 //          field: 'name',
 //          enableFiltering: false,
 //          cellTemplate: '<div class="ui-grid-cell-contents" ><a href="/app/dashboard/employee/{{row.entity.id}}">{{grid.getCellValue(row, col)}}</a></div>'
 //          // filter: {
 //          // flags: { caseSensitive: true }
 //          //  }
 //      },
 //
 //  ];


    EmployeeService.Read()
        .then(function success(response) {
            employeeforfilter = response;

            
            for(let employee of response) {
                var temployee = {};
                temployee['id'] = employee.ID;
                temployee['name'] = employee.LastName + ' ' + employee.FirstName; //so on
                for(let skill of employee.Skills) {
                    temployee[skill.SkillName] = skill.Level;
                }
                for(let certificate of employee.Certificates) {
                    temployee[certificate.CertificateName] = certificate.ReceiptDate;
                }
                specifiedEmployee.push(temployee);

            }
            //  console.log(specifiedEmployee);
            $scope.gridOptions.data = specifiedEmployee;

            // console.log(employeeforfilter);
            // console.log(employeeforfilter);

        });


    // console.log(CertificateService.Read());


}
EmployeeTableController.$inject = ['$scope', '$location', 'EmployeeService', 'PositionService', 'SkillService', 'CertificateService', 'SkillGroupService', 'uiGridConstants'];