(function () {
    app.Controller('ProductList', function ($scope, $http) {
        $scope.InvoiceCart = [];
        var init = function () {

        };
        init();
        $scope.AddNewRow = function () {
            $scope.InvoiceCart.push({ CID: 0, SCID: 0, ITEMSL: 0, QTY:0,RATE:0,AMOUNT:0 });
        }
    });
}).call(angular); 