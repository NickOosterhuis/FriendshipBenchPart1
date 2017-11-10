//module
var appClient = angular.module('clients', ['ngRoute']);

appClient.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode(true);
    $locationProvider.hashPrefix('');
}]);

//controller
appClient.controller('clientsCtrl', function ($scope, $http, $location) {

    //get all clients connected to the logged in healthworker
    $scope.listConnectedClients = function () {
        $http.get('http://127.0.0.1:54618/api/ClientUsers/')
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

appClient.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/clients', { templateUrl: '/app/views/clients/list.html', controller: 'clientsCtrl' });
    $routeProvider.when('/clients/:id', { templateUrl: '/app/views/clients/show.html', controller: 'showClientCtrl' });
    $routeProvider.when('/clients/:id/questionnaires', { templateUrl: '/app/views/clients/questionnaireList.html', controller: 'showQuestionnairesCtrl' });
    $routeProvider.when('/questionnaire/:id', { templateUrl: '/app/views/clients/questionnaire.html', controller: 'showQuestionnairesCtrl' });
    $routeProvider.otherwise({ redirectTo: '/' });
}]);


