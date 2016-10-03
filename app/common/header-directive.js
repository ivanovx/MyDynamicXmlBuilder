function headerDirective() {
    return {
        restrict: 'A',
        scope: false,
        templateUrl: 'app/common/header-directive.html'
    };
}

angular
    .module('enWebSite.directives', [])
    .directive('siteHeader', headerDirective);