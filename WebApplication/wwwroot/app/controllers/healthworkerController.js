var healthworker = angular.module('healthworker', []);

healthworker.controller('healthworkercontroller', function ($scope, $http, $location, $window) {
    //console.log('http://127.0.0.1:54618/api/healthworkers/' + location.pathname.split("/").pop());

    // default method run at pageload, opens the healthworker with requested id.
    $http.get('http://127.0.0.1:54618/api/healthworkers/' + location.pathname.split("/").pop())
        .then(function (response) {
            $scope.healthworker = response.data;
        }, function (response) {
            $scope.healthworker = "something went wrong!";
        });
    // method to save healthworker to the API and redirects to healthworker list.
    $scope.savehealthworker = function (method) {
        if (method == "put") {
            console.log('method is put');
            console.log($scope.healthworker);
            $http.put('http://127.0.0.1:54618/api/healthworkers/' + location.pathname.split("/").pop(), $scope.healthworker)
                .then(
                function (response) {
                    console.log('edited!');
                    //$scope.redirectTohealthworkers();
                },
                function (response) {
                    console.log('failed!');
                });
        } else if (method == "post") {
            console.log('method is post');
            console.log(angular.toJson($scope.healthworkeradd));
            $http.post('http://127.0.0.1:54618/api/Account/register/healthworker/', $scope.healthworkeradd)
                .then(
                function (response) {
                    console.log('added!');
                    $scope.redirectTohealthworkers();
                },
                function (response) {
                    console.log('failed!');
                });
        }

    }
    // method to delete healthworker from the API and redirects to healthworker list.
    $scope.deletehealthworker = function () {
        console.log('test');
        $http.delete('http://127.0.0.1:54618/api/healthworkers/' + $scope.healthworker.id)
            .then(
            function (response) {
                console.log('deleted!');
                $scope.redirectTohealthworkes();
            },
            function (response) {
                console.log('failed to delete!');
            });
    }
    $scope.redirectTohealthworkers = function () {
        $window.location.href = "https://localhost:44314/adminhealthworkers/";
    }
});