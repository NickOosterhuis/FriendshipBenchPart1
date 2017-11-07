//module
var app = angular.module('appointments', []);

//controller
app.controller('appointmentCtrl', function ($scope, $http) {
        //get all appointments
        $http.get('http://127.0.0.1:54618/api/Appointments')
        .then(function (response) {
            //first function handles succes
            $scope.appointments = response.data;
        }, function (response) {
            //second function handles error
            $scope.appointments = "something went wrong!";

        });

        //edit appointment
        $scope.editUser = function (appointmentID) {
            $http.get("http://127.0.0.1:54618/api/Appointments/" + appointmentID)
                .then(
                function (response) {
                    //succes
                    console.log(response)

                },
                function (response) {
                    //failure

                }
            );


            $http.post('/api/trivia', { 'questionId': option.questionId, 'optionId': option.id }).success(function (data, status, headers, config) {
                $scope.correctAnswer = (data === true);
                $scope.working = false;
            }).error(function (data, status, headers, config) {
                $scope.title = "Oops... something went wrong";
                $scope.working = false;
            });
        }
});