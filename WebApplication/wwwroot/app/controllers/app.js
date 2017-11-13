//module
var app = angular.module('appointments', ['ngRoute']);

app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode(true);
    $locationProvider.hashPrefix('');
}]);

app.controller('test', function ($scope) {
    alert('test');
});

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


    // callback for ng-click 'cancelAppointment':
    $scope.cancelAppointment = function (appointmentID) {
        $http.get('http://127.0.0.1:54618/api/appointments/' + appointmentID)
            .then(function (response) {
                //succes
                $appointment = response.data;
                console.log($appointment);
                $scope.sendDataObject = {}

                $scope.sendDataObject.id = $appointment.id;
                $scope.sendDataObject.time = $appointment.time;
                $scope.sendDataObject.statusId = 3;
                $scope.sendDataObject.benchId = $appointment.bench.id
                $scope.sendDataObject.clientId = $appointment.client.id
                $scope.sendDataObject.healthworkerId = "da249e00-6506-4c55-9d5d-ce42371b57e6";
                console.log($scope.sendDataObject);

                $http.put('http://127.0.0.1:54618/api/Appointments/' + appointmentID, $scope.sendDataObject)
                    .then(function (response) {
                        alert('appointment has been updated');

                        $scope.listAppointments();

                    }, function (response) {
                        //second function handles error
                        alert('something went wrong!');

                    });

            }, function (response) {
                //failure
                alert('not able to retrieve appointment data');
            });
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
    $routeProvider.otherwise({ redirectTo: '/' });
}]);


