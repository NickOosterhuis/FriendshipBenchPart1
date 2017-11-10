//module
var app = angular.module('clients', ['ngRoute']);

app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode(true);
    $locationProvider.hashPrefix('');
}]);

//controller
app.controller('clientsCtrl', function ($scope, $http, $location) {

    //get all clients connected to the logged in healthworker
    $scope.listConnectedClients = function () {
        $http.get('http://127.0.0.1:54618/api/Clients/Connected')
            .then(function (response) {
                //first function handles succes
                $scope.clients = response.data;
                $location.path("/clients")

            }, function (response) {
                //second function handles error
                $scope.clients = "something went wrong!";

            });
    };

    $scope.listConnectedClients();


    // callback for ng-click 'showClient':
    $scope.showClient = function (ClientID) {
        $location.path("/clients/" + ClientID);
    };

 });

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/clients', { templateUrl: '/app/views/clients/list.html', controller: 'clientsCtrl' });
    $routeProvider.when('/clients/:id', { templateUrl: '/app/views/clients/show.html', controller: 'showClientCtrl' });
    $routeProvider.when('/questionnaire/:id', { templateUrl: '/app/views/clients/questionnaire.html', controller: 'showQuestionnaireCtrl' });
    $routeProvider.otherwise({ redirectTo: '/' });
}]);


