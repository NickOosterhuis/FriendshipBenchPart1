var client = angular.module('client', []);

client.controller('clientcontroller', function ($scope, $http, $location, $window) {
    //console.log('http://127.0.0.1:54618/api/clients/' + location.pathname.split("/").pop());

    // default method run at pageload, opens the client with requested id.
    $http.get('http://127.0.0.1:54618/api/Clients/' + location.pathname.split("/").pop())
        .then(function (response) {
            $scope.client = response.data;
        }, function (response) {
            $scope.client = "something went wrong!";
        });
    // method to save client to the API and redirects to client list.
    $scope.saveclient = function (method) {
        if (method == "put") {
            console.log('method is put');
            console.log($scope.client);
            $http.put('http://127.0.0.1:54618/api/Clients/' + location.pathname.split("/").pop(), $scope.client)
                .then(
                function (response) {
                    console.log('edited!');
                    $scope.redirectToClients();
                },
                function (response) {
                    console.log('failed!');
                });
        } 
    }
    // method to delete client from the API and redirects to client list.
    $scope.deleteclient = function () {
        console.log('test');
        $http.delete('http://127.0.0.1:54618/api/Clients/' + $scope.client.id)
            .then(
            function (response) {
                console.log('deleted!');
                $scope.redirectToClients();
            },
            function (response) {
                console.log('failed to delete!');
            });
    }
    $scope.redirectToClients = function () {
        $window.location.href = "https://localhost:44314/adminClients/";
    }
});