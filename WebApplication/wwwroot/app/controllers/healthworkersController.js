﻿var healthworkers = angular.module('healthworkers', []);

healthworkers.controller('healthworkerscontroller', function ($scope, $http) {
    //var headers = buildHeaders();
    //healthworkers.run(['$http', function ($http) {
    //    $http.defaults.headers.common['www:authentication: bearer'] = getCookie();
    //    $http.defaults.headers['www:authentication: bearer'] = getCookie();
   // }]);   

    var cookie = getCookie("JWT");
    $http.get('http://127.0.0.1:54618/api/HealthWorkers/', {
        headers: {
            'Authorization': "Bearer " + cookie
        }
    })
    .then(function (response) {
        //first function handles succes
        $scope.healthworkers = response.data;
    }, function (response) {
        //second function handles error
        console.log($http.headers);
        $scope.statuses = "something went wrong! Please contact an administrator.";
    });
});