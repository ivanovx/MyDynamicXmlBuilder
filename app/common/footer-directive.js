function footerDirective() {
    return {
        restrict: 'A',
        scope: false,
        templateUrl: 'app/common/footer-directive.html'
    };
}

angular
    .module('enWebSite.directives', [])
    .directive('siteFooter', footerDirective);