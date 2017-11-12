//module
var app = angular.module('questionnaires', ['ngRoute']);

app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode(true);
    $locationProvider.hashPrefix('');
}]);

//controller
app.controller('questionnairesCtrl', function ($scope, $http, $location) {

    $scope.listQuestionnaires = function () {
        $http.get('http://127.0.0.1:54618/api/Questionnaires')
            .then(function (response) {
                //succes
                console.log(response.data);
                $scope.questionnaires = response.data;
                $location.path('/questionnaires');
            }, function (response) {
                //failure
                alert('not able to retrieve the questionnaires');

            });
    }

    $scope.listQuestionnaires();
    
    $scope.showAnswers = function (questionnaireId) {
        $location.path('questionnaire/:id');
    }
});

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/questionnaires', { templateUrl: '/app/views/questionnaires/list.html', controller: 'questionnairesCtrl' });
    $routeProvider.when('/questionnaires/show', { templateUrl: '/app/views/questionnaires/show.html', controller: 'questionnairesCtrl' });
    $routeProvider.when('/questionnaires/:id', { templateUrl: '/app/views/questionnaires/answers.html', controller: 'answersCtrl' });
    $routeProvider.otherwise({ redirectTo: '/' });
}]);


