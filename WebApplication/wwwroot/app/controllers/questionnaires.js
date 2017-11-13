//module
var questionnaires = angular.module('questionnaires', ['ngRoute']);

questionnaires.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode(true);
    $locationProvider.hashPrefix('');
}]);

//controller
questionnaires.controller('questionnairesCtrl', function ($scope, $http, $location) {

    $scope.listQuestionnaires = function () {
        $http.get('http://127.0.0.1:54618/api/Questionnaires')
            .then(function (response) {
                //succes
                console.log(response.data);
                $scope.questionnaires = response.data;
                //for now a default value of true. redflag should be retrieved from databasse in future
                $scope.questionnaires.redflag = true;

                $location.path('/questionnaires');
            }, function (response) {
                //failure
                alert('not able to retrieve the questionnaires');

            });
    }

    $scope.listQuestionnaires();

    $scope.deleteQuestionnaire = function (questionnaireId) {
        //delete a questionnaire
        $http.delete("http://127.0.0.1:54618/api/Questionnaires/" + questionnaireId)
            .then(
            function (response) {
                //succes
                console.log('succes');
                $scope.listQuestionnaires();
            },
            function (response) {
                //failure
                console.log('failure');
                alert('not able to delete questionnaire');
            }
            );
    }
    
    $scope.showAnswers = function (questionnaireId) {
        $location.path('/questionnaire/' + questionnaireId);
    }
});

questionnaires.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/questionnaires', { templateUrl: '/app/views/questionnaires/list.html', controller: 'questionnairesCtrl' });
    $routeProvider.when('/questionnaires/show', { templateUrl: '/app/views/questionnaires/show.html', controller: 'questionnairesCtrl' });
    $routeProvider.when('/questionnaire/:id', { templateUrl: '/app/views/questionnaires/answers.html', controller: 'answersCtrl' });
    $routeProvider.otherwise({ redirectTo: '/' });
}]);


