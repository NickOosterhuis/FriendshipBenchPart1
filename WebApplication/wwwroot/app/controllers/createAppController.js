app.controller('createAppCtrl', function ($http, $scope, $location) {

    //retrieve all benches
    $http.get('http://127.0.0.1:54618/api/benches')
        .then(function (response) {
            //first function handles succes
            $scope.benches = response.data;
        }, function (response) {
            //second function handles error
            console.log("something went wrong!");
        });   

    $scope.sendData = function () {

        $scope.sendDataObject = {}
        
        $date = $scope.appointment.date;
        $time = $scope.appointment.time;
        $dateTime = $date + "T" + $time + ":00.00Z";

        $scope.sendDataObject.time = $dateTime;
        $scope.sendDataObject.benchId = $scope.appointment.bench.id;
        $scope.sendDataObject.clientId = $scope.appointment.clientId;
        $scope.sendDataObject.healthworkerId = "0bba7a60-d5ff-4e53-bdf9-e00bc7552f10";
        console.log($scope.sendDataObject);

        $http.post('http://127.0.0.1:54618/api/Appointments/', $scope.sendDataObject)
            .then(function (response) {
                alert('appointment has been created');
                $location.path('/appointments');

            }, function (response) {
                //second function handles error
                alert('something went wrong!');
                console.log(response);

            });
    };
});