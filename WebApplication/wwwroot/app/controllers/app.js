//module
var app = angular.module('appointments', ['ngRoute']);

app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode(true);
    $locationProvider.hashPrefix('');
}]);

//controller
app.controller('appointmentCtrl', function ($scope, $http, $location) {

    //get all appointments
    $scope.listAppointments = function () {
        $http.get('http://127.0.0.1:54618/api/Appointments')
            .then(function (response) {
                //first function handles succes
                $scope.appointments = response.data;
                $location.path("/appointments")

            }, function (response) {
                //second function handles error
                $scope.appointments = "something went wrong!";

            });
    };

    $scope.listAppointments();


    // callback for ng-click 'editUser':
    $scope.editAppointment = function (appointmentID) {
        $location.path("/appointments/" + appointmentID);
    };

    //callback for ng-click 'createUser':
    $scope.createAppointment = function () {
        $location.path("/create/appointment");
    }

    //delete appointment
    $scope.deleteAppointment = function (appointmentID) {
        $http.delete("http://127.0.0.1:54618/api/Appointments/" + appointmentID)
            .then(
            function (response) {
                //succes
                console.log('succes');
                //$location.path('/appointments');
                $scope.listAppointments();
                

            },
            function (response) {
                //failure
                console.log('failure');
            }
            );
    }

 });

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/create/appointment', { templateUrl: '/app/views/appointments/create.html', controller: 'createAppCtrl' });
    $routeProvider.when('/appointments', { templateUrl: '/app/views/appointments/list.html', controller: 'appointmentCtrl' });
    $routeProvider.when('/appointments/:id', { templateUrl: '/app/views/appointments/show.html', controller: 'showAppCtrl' });
    $routeProvider.when('/create-appointment', { templateUrl: '/app/views/appointments/create.html', controller: 'appointmentCtrl' });
    $routeProvider.when('/clients', { templateUrl: '/app/views/clients/show.html', controller: 'appointmentCtrl' });
    $routeProvider.otherwise({ redirectTo: '/' });
}]);


