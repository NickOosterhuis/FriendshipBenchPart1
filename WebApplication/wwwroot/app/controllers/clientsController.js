var clients = angular.module('clients', []);

clients.controller('clientscontroller', function ($scope, $http) {
    //var headers = buildHeaders();
    //clients.run(['$http', function ($http) {
    //    $http.defaults.headers.common['www:authentication: bearer'] = getCookie();
    //    $http.defaults.headers['www:authentication: bearer'] = getCookie();
   // }]);   

    var config = {
        headers: {
            'www:authentication: bearer': getCookie("JWT"),         
        }
    };
    console.log(config);
    var cookie = getCookie("JWT");
    $http.get('http://127.0.0.1:54618/api/clients/', {
        headers: {
            'WWW-Authenticate': "Bearer " + cookie
        }
    })
    .then(function (response) {
        //first function handles succes
        $scope.clients = response.data;
    }, function (response) {
        //second function handles error
        console.log($http.headers);
        $scope.statuses = "something went wrong! Please contact an administrator.";
    });
});