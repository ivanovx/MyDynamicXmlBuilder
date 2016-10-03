angular
    .module("enWebSite.controllers", [])
    .controller("MainController", function ($scope) {
        $scope.author = "Ivan Ivanov";
        this.nuget = "";
        this.github = "";
        this.version = "";
    })
    .controller("HomeController", function () {
    });
    

angular
    .module("enWebSite.directives", [])
    .directive('siteHeader', function () {
        return {
            restrict: 'A',
            scope: false,
            templateUrl: 'app/common/header-directive.html'
        };
    })
    .directive('siteFooter', function () {
        return {
            restrict: 'A',
            scope: false,
            templateUrl: 'app/common/footer-directive.html'
        };
    });

angular
    .module("enWebSite", ["ngRoute", "enWebSite.controllers", "enWebSite.directives"])
    .config(function ($routeProvider, $locationProvider) {
         $locationProvider.html5Mode(false);

        $routeProvider
            .when("/", {
                templateUrl: 'app/home-page/home-page-view.html',
                controller: 'HomeController',
                controllerAs: "vm"
            })
            .when("/about", {
                templateUrl: 'app/about-page/about-page-view.html'
            })
    });