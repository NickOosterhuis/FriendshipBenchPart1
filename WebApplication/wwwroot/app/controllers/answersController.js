questionnaires.controller('answersCtrl', function ($scope, $http, $location, $route, $routeParams) {
    //show questionnaire by questionnaireId
    this.route = $route;
    this.routeParams = $routeParams;
    var questionnaireId = this.routeParams['id'];

    $http.get('http://127.0.0.1:54618/api/Questionnaires/' + questionnaireId)
        .then(function (response) {
            //succes
            console.log(response.data);
            $scope.questionnaire = response.data;

            //get client details
            $http.get('http://127.0.0.1:54618/api/clients/' + $scope.questionnaire.client_id)
                .then(function (response) {
                    //succes
                    $scope.questionnaire.client = response.data;
                    //TODO redflag genereren
                    $scope.questionnaire.redflag = true;
                }, function (response) {
                    //failure
                    alert('could not retrieve client data');
                });

            $location.path('/questionnaire/' + questionnaireId);
        }, function (response) {
            //failure
            alert('could not retrieve answers!');
            console.log(response.data);
        });



});