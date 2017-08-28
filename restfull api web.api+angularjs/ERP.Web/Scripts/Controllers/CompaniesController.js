var CompaniesController = function ($scope, $http, DTOptionsBuilder, DTColumnDefBuilder) {
    $scope.companies = [];    
    //init table
    $scope.load = function () {
        $http.get('/companies/get').then(function (res) { $scope.companies = res.data.response; });
    }

    $scope.load();

    $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers').withDisplayLength(25);
    $scope.dtOptions.bDestroy = true;
    $scope.dtOptions.bFilter = false;
    $scope.dtOptions.bRetrive = true;
    $scope.dtOptions.bLengthChange = false;
    $scope.dtColumnDefs = [
        DTColumnDefBuilder.newColumnDef(9).notSortable()
    ];
}

CompaniesController.$inject = ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnDefBuilder'];


var EditCompanyController = function ($scope, $http, DTOptionsBuilder, DTColumnDefBuilder) {
    $scope.company = window.comp;
    $scope.avalibleUsers = window.avalibUsers;
    $scope.managers = window.managers;    

    //Tab click event (show new tab logic)
    $('#myTab a').click(function (e) {
        e.preventDefault();
        var alwaysEnabled = $($(this)[0]).parent().hasClass('alwaysEnabled');        
        if ($scope.company.ID == 0 && !alwaysEnabled) {
            return false;
        } else {
            $(this).tab('show');
        }
        
    });

    CKEDITOR.replace('emailTemplate');

    //Datatables settings
    $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers').withDisplayLength(10);
    $scope.dtOptions.bDestroy = true;
    $scope.dtOptions.bFilter = false;
    $scope.dtOptions.bRetrive = true;
    $scope.dtOptions.bLengthChange = false;
    $scope.dtColumnDefs = [
        DTColumnDefBuilder.newColumnDef(3).notSortable()
    ];

    //Logo uploader init
    $scope.uploader = new qq.FileUploader({
        // pass the dom node (ex. $(selector)[0] for jQuery users)
        element: document.getElementById('file-uploader'),
        // path to server-side upload script
        action: '/Upload/Logo',
        allowedExtensions: ['jpeg', 'jpg', 'png', 'gif'],
        uploadButtonText : "<i class=\"fa fa-cloud-upload\"></i> Upload a File",
        onComplete: function (id, fileName, responseJSON) {            
            if (responseJSON.success) {
                $scope.company.Photo = responseJSON.response;
                Core.notify("Logo uploaded");
            } else {
                Core.error(responseJSON.response);
            }
        }
    });

    setTimeout(Core.maskPlugin, 150);

    //Save company
    $scope.save = function () {
        $scope.company.EmailTemplate = CKEDITOR.instances.emailTemplate.getData();

        $http.post('/companies/save',$scope.company).success(function (data, status) {
            if (data.success) {                
                Core.notify('Company saved successfully');
                $scope.company.ID = data.response;
            } else {
                Core.error(data.response);
            }
        });
    };

    //get users for selected domain
    $scope.getDomainUsers = function() {
        $http.get('/companies/getDomainUsers/?domains=' + $scope.company.Domains).then(function (res) { $scope.avalibleUsers = res.data.response; });
    }

    //add manager to the list
    $scope.addManager = function() {
        var user = JSON.parse($('#curManager').val());        
        for (var i = 0; i < $scope.managers.length; i++) {
            if ($scope.managers[i].ID == user.ID) {
                return;
            }
        }
        $http.get('/companies/addManager/?cId=' + $scope.company.ID + '&mId=' + user.ID);
        $scope.managers.push(user);
    }
    //delete manager
    $scope.deleteManager = function (item) {     
        bootbox.confirm("Are you sure you want to remove " + item.FirstName + " " + item.LastName + " from account managers?", function (e) {
            if (e) {
                $http.get('/companies/removeManager/?cId=' + $scope.company.ID + '&mId=' + item.ID);
                var index = $scope.managers.indexOf(item);
                $scope.$apply($scope.managers.splice(index, 1));
            }
        });
    }
}

EditCompanyController.$inject = ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnDefBuilder'];
