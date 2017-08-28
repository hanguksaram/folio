var UsersController = function ($scope, $http, DTOptionsBuilder, DTColumnDefBuilder) {
    $scope.users = [];
    $scope.displayedCollection = [].concat($scope.users);
    $scope.modalUser = null;
    $scope.modalTitle = "Add User";
    $scope.repeatPass = "";
    $scope.masterUser = null;

    //init table
    $scope.load = function() {
        $http.get('/users/get').then(function (res) { $scope.users = res.data.response; });
    }

    $scope.load();

    $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers').withDisplayLength(25);
    $scope.dtOptions.bDestroy = true;
    $scope.dtOptions.bFilter = false;
    $scope.dtOptions.bRetrive = true;
    $scope.dtOptions.bLengthChange = false;
    $scope.dtColumnDefs = [
        DTColumnDefBuilder.newColumnDef(4).notSortable()
    ];


    //get user by ID
    $scope.getUserById = function(id) {
        for (var i = 0; i <= $scope.users.length - 1; i++) {
            var s = $scope.users[i];
            if (s && id == s.ID) {
                return s;
            }
        }
    }
    
    //edit user modal
    $scope.editUser = function (uid) {
        $scope.modalTitle = "Edit User";
        $scope.modalUser = angular.copy($scope.getUserById(uid.uid));
        $scope.masterUser = angular.copy($scope.getUserById(uid.uid));
        $scope.repeatPass = angular.copy($scope.modalUser.Password);
        $("#userModal").modal('show');
    }

    //remove user
    $scope.deleteUser = function(uid) {
        bootbox.confirm("Are you sure you want to remove this user?", function(e) {
            if (e) {
                $http.get('/users/remove/' + uid.uid).then(function (res) {
                    Core.notify("User successfully removed");
                    $scope.load();
                });
            }
        });
    }

    //add new user
    $scope.addUser = function() {
        $scope.modalTitle = "Add User";
        $scope.modalUser = {FirstName: "", LastName: "", Email: "", Password: "", Title:"", ID:0}
        $scope.masterUser = { FirstName: "", LastName: "", Email: "", Password: "", Title: "", ID: 0 }
        $scope.repeatPass = "";
        $("#userModal").modal('show');
    }

    //save user
    $scope.saveUser = function () {
        $http.post('/users/save', $scope.modalUser).success(function (data, status) {
            if (data.success) {
                $("#userModal").modal('hide');
                Core.notify('User saved successfully');
                $scope.load();
            } else {
                Core.error(data.response);
            }
            
        });
    }

    //check model state
    $scope.isUnchanged = function (user) {
        return angular.equals(user, $scope.masterUser);
    };

}

UsersController.$inject = ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnDefBuilder'];


var ModalCtrl = function ($rootScope, $scope, $modalInstance, title, message) {
    $scope.title = title;
    $scope.message = message;
};
