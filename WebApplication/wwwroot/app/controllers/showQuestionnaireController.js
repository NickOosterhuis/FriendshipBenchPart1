app.controller('showQuestionnaireCtrl', function ($http, $scope, $location, $route, $routeParams) {
    //show appointment by appointmentId
    this.route = $route;
    this.routeParams = $routeParams;
    var QuestionnaireID = this.routeParams['id'];

    //retrieve all benches
    $http.get('http://127.0.0.1:54618/api/clients/questionnaire' + QuestionnaireID)
        .then(function (response) {
            //first function handles succes
            $scope.questionnaire = response.data;
        }, function (response) {
            //second function handles error
            console.log("something went wrong!");
        });

});