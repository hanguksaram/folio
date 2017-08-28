var AdminPageController = function ($scope) {
    $scope.title = "Dashboard";
     
    $scope.navBars = [
        { active: "active", url: "/admin", name: "Dashboard", icon: "fa fa-tachometer" },
        { active: "", url: "/admin/companies", name: "Companies", icon: "fa fa-building" },
        { active: "", url: "/admin/users", name: "Users", icon: "fa fa-users" },
        { active: "", url: "/admin/schedule", name: "Work Schedule", icon: "fa fa-calendar" }
    ];

    $scope.$on('$routeChangeSuccess', function () {
        var path = window.location.pathname;
        for (var i = 0; i < $scope.navBars.length; i++) {
            var s = $scope.navBars[i];
            var url = s.url.toLocaleLowerCase();
            var pathL = path.toLocaleLowerCase();
            if (url == pathL || (pathL.indexOf(url+'/') != -1 && url.length > 7)) {
                s.active = "active";
                $scope.title = s.name;                
            } else {
                s.active = "";
            }            
        }
    });

}

AdminPageController.$inject = ['$scope'];
