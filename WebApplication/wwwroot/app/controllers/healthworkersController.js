var healthworkers = angular.module('healthworkers', []);

healthworkers.controller('healthworkerscontroller', function ($scope, $http) {
    $http.get('http://127.0.0.1:54618/api/HealthWorkers')
        .then(function (response) {
            //first function handles succes
            $scope.healthworkers = response.data;
        }, function (response) {
            //second function handles error
            $scope.statuses = "something went wrong! Please contact an administrator.";
        });
});