var MainController = function ($scope, $http, $templateCache) {
    $scope.title = "Dashboard";
    $scope.userName = window.userName;
    $scope.breadcrumbs = [];
    $scope.activeBreadcrumb = "";
    $scope.breadcrumbs.push({ Name: 'ERP Dashboard' , Url:''});

    $scope.navBars = [
        { active: "active", url: "/app/dashboard", name: "Dashboard", icon: "fa fa-tachometer" },
        { active: "", url: "/app/accounts", name: "Accounts", icon: "fa fa-user" },
        { active: "", url: "/app/reports/create", name: "Create Report", icon: "fa fa-file-o" },
        { active: "", url: "/app/reports/list", name: "Reports", icon: "fa fa-files-o" },
        {
            active: "", isSub: true, name: "Employees", data: [
                { active: "", url: "/app/dashboard/employeecontacts", name: "Contacts", icon: "fa fa-phone" },
                { active: "", url: "/app/dashboard/employeeskills", name: "Skills", icon: "fa fa-graduation-cap" }
            ]
        }
    ];
    if ($('#isA').val() == "1") {
        var sub = {
            isSub: true, active: "", name: 'KPI Reports', data: [
                { active: "", url: "/app/KPIreports", name: "KPI Report", icon: "fa fa-file-o" },
                { active: "", url: "/app/effficiency ", name: "Effficiency Report", icon: "fa fa-bar-chart" }
            ]
        }
        $scope.navBars.push(sub);
        $scope.navBars.push({ active: "", url: "/app/invoices", name: "Invoices", icon: "fa fa-dollar" });        
    }    

    $scope.$on('$routeChangeSuccess', function () {
        window.scrollTo(0, 0);
        return; // TODO: remove this line, for debug
        Core.ajax('/Common/GetPageMetadata?url=' + window.location.pathname, function (data) {
            $scope.$apply(function () {
                $scope.breadcrumbs = [];
                var bs = data.Breadcrumbs;
                for (var i = 0; i < bs.length; i++) {
                    if (i != bs.length - 1) {
                        $scope.breadcrumbs.push(data.Breadcrumbs[i]);
                    } else {
                        $scope.activeBreadcrumb = data.Breadcrumbs[i].Name;
                    }
                }
            });            

            for (var i = 0; i < $scope.navBars.length; i++) {
                var s = $scope.navBars[i];
                if (s.name == data.Navbar) {                    
                    $scope.$apply(function() {
                        s.active = "active";
                        $scope.title = s.name;
                    });

                } else {
                    $scope.$apply(function () {
                        s.active = "";
                    });                    
                }
            }
            if (data.Title != null)
            {
                $scope.$apply(function()
                {
                    $scope.title = data.Title;
                });
               
            }
        });
    });

    $scope.$on('$routeChangeStart', function () {
        //$templateCache.removeAll(); // this method deleted directive's templates 
    });
    
    $scope.changePassword = function (oldPassword, newPassword, confirmedNewPassword) {
        if (newPassword == confirmedNewPassword)
        {
            $http.post("/Account/ChangePassword/", '{ oldPassword:\'' + oldPassword + '\', newPassword:\'' + newPassword + '\'}')
                .success(function(resp)
                {
                    if (resp.success)
                    {
                        Core.notify('Password has been changed');
                        $('#modalChangePassword').modal('hide');
                    } else
                    {
                        Core.error("Old password is incorrect");
                    }
                })
                .error(function(resp)
                {
                    Core.error("Error occured");
                });
        }
        else//newPassword != confirmedNewPassword
        {
            Core.error("Please confirm new password again.");
        }
    

   };
}

MainController.$inject = ['$scope','$http', '$templateCache'];

