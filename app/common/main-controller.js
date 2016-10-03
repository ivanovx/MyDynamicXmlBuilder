function mainController($scope) {
    $scope.author = "Ivan Ivanov";
    this.nuget = "";
    this.github = "";
    this.version = "";
}

angular
    .module("enWebSite.controllers", [])
    .controller("MainController", ["$scope", mainController]);