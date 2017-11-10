app.controller('showClientCtrl', function ($http, $scope, $location, $route, $routeParams) {
    //show client by ClientId
    this.route = $route;
    this.routeParams = $routeParams;
    var ClientID = this.routeParams['id'];

    //retrieve all client
    $http.get('http://127.0.0.1:54618/api/clients/' + ClientID)
        .then(function (response) {
            //first function handles succes
            $scope.client = response.data;
        }, function (response) {
            //second function handles error
            console.log("something went wrong!");
        });

    $scope.createNewAppointment = function (id) {
        //Kan ik een afspraak maken met een default ding?

        $location.path("/create-appointment/"+id);
    };

    $scope.showQuestionnaire = function (id) {

        //call dem questionarys
        $location.path("/questionnaire/"+id)

    }
});