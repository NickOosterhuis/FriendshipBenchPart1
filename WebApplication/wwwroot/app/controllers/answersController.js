app.controller('appointmentCtrl', function ($scope, $http, $location, $route, $routeParams) {
    //show questionnaire by questionnaireId
    this.route = $route;
    this.routeParams = $routeParams;
    var questionnaireId = this.routeParams['id'];



});