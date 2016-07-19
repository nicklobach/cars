﻿angular.module("app", [])

    .controller("NavigationBikes", function ($scope, $http, $rootScope) {
    var pageCount = 0;

    $scope.previo = "disabled";

    $scope.query = "";
    $http({method: "GET", url: "GetProductDate", params : {sql:$scope.query}})
        .success(function(date) {
            $scope.products = date;

            $http({method: "GET", url: "GetCountProduct", params: {sql:$scope.query} })
                .success(function (date) {
                    $scope.pages = date;
                })
                .error(function (err) {
                    Console.log("error ", err);
                });
        })
        .error(function(err) {
            Console.log("error ", err);
        });

    $scope.NumberClick = function($index) {

        var i = 0;
        for (i = 0; i < $scope.pages.length; i++) {
            if ($scope.pages[i].activeName != null) {
                $scope.pages[i].activeName = "";
            }
        }
        $scope.pages[$index].activeName = "active";
        if ($index === 0) {
            $scope.previo = "disabled";
        } else {
            $scope.previo = "";
        }
        if ($index === $scope.pages.length-1) {
            $scope.next = "disabled";
        } else {
            $scope.next = "";
        }

        $http({ method: "GET", url: "GetProductDateNamber", params: { sql: $scope.query, number: $index } })
            .success(function(date) {
                $scope.products = date;
            })
            .error(function(err) {
                Console.log("err", err);
            });
        $scope.activeCount = $index;
    };

    $scope.PreviousClick = function () {
        if ($scope.previo == "disabled") {
            return;
        }
        var i = 0, count = 0;
        for (i = 0; i < $scope.pages.length; i++) {
            if ($scope.pages[i].activeName !== "" && i !== 0) {
                $scope.pages[i].activeName = "";
                $scope.pages[i - 1].activeName = "active";
                count = i - 1;
                $scope.activeCount = count;
                break;
            }
        }
        if (count === 0) {
            $scope.previo = "disabled";
        } else {
            $scope.previo = "";
        }
        $scope.next = "";

        $http({ method: "GET", url: "GetProductDateNamber", params: { sql: $scope.query,number: count } })
            .success(function(date) {
                $scope.products = date;
            })
            .error(function(err) {
                Console.log("err", err);
            });
    };

    $scope.NextClick = function () {
        if ($scope.next == "disabled") {
            return;
        }
        var i = 0, count = 0;
        for (i = 0; i < $scope.pages.length; i++) {
            if ($scope.pages[i].activeName !== "" && i !== 4) {
                $scope.pages[i].activeName = "";
                $scope.pages[i + 1].activeName = "active";
                count = i + 1;
                $scope.activeCount = count;
                break;
            }
        }
        $scope.previo = "";

        if (count === $scope.pages.length-1) {
            $scope.next = "disabled";

        } else {
            $scope.next = "";
        }

        $http({ method: "GET", url: "GetProductDateNamber", params: {sql:$scope.query, number: count } })
            .success(function(date) {
                $scope.products = date;
            })
            .error(function(err) {
                Console.log("err", err);
            });

    };

    $scope.AllClick = function () {
        $scope.query = " ";
        $http({method: "GET", url: "GetProductDate", params : {sql: $scope.query}})
        .success(function (date) {
            $scope.products = date;
            $http({ method: "GET", url: "GetCountProduct", params: { sql: $scope.query } })
                .success(function (date) {
                    $scope.pages = date;
                    if ($scope.pages.length == 1) {
                        $scope.previo = "disabled";
                        $scope.next = "disabled";
                    } else {
                        $scope.previo = "disabled";
                        $scope.next = "";
                    }
                })
                .error(function (err) {
                    Console.log("error ", err);
                });
        })
        .error(function (err) {
            Console.log("error ", err);
        });
    };


    /* $scope.ItemClick = function ($index) {

         alert($scope.products[$index].id);

     };*/

    $scope.TypeTruckClick = function(type) {
        $scope.query = " where Type_bike = '" + type + "'";
        $http({ method: "GET", url: "GetProductDate", params: { sql: $scope.query } })
            .success(function (date) {
                $scope.products = date;
                $http({ method: "GET", url: "GetCountProduct", params: { sql: $scope.query } })
                .success(function (date) {
                    $scope.pages = date;
                    if ($scope.pages.length == 1) {
                        $scope.previo = "disabled";
                        $scope.next = "disabled";
                    } else {
                        $scope.previo = "disabled";
                        $scope.next = "";
                    }
                })
                .error(function (err) {
                    Console.log("error ", err);
                });
            })
            .error(function (err) {
                Console.log("err", err);
            });
            
        $scope.previo = "disabled";
    };

    $scope.Root = function() {
        $http({ method: "GET", url: "GetProductDate", params: { sql: $scope.query } })
            .success(function (date) {
                $scope.products = date;
                if (date.length == 0) {
                    alert("Entered your type of bike is not defined");
                }

                $http({ method: "GET", url: "GetCountProduct", params: { sql: $scope.query } })
                    .success(function (date) {
                        $scope.pages = null;
                        $scope.pages = date;
                        if ($scope.pages.length == 1) {
                            $scope.previo = "disabled";
                            $scope.next = "disabled";
                        } else {
                            $scope.previo = "disabled";
                            $scope.next = "";
                        }
                    })
                    .error(function (err) {
                        Console.log("error ", err);
                    });
            })
            .error(function (err) {
                Console.log("error ", err);
            });
    };

    $rootScope.$on("rootScope:emit", function (event, query) {
        $scope.query = query;
        $scope.Root();
    });


})

    .controller("SearchController", function ($scope, $http, $rootScope) {
        
        $http.get("BikesModelItem")
            .success(function (date) {
                $scope.models = date;
                $scope.maker = date.models.maker[0].Text;
            })
            .error(function (err) {
                Console.log("error ", err);
            });

        
        $scope.MakerClick = function() {
            var mak = $scope.maker;
            $http({ method: "GET", url: "_MakersInBikes", params: { maker: mak } })
                .success(function(date) {
                    $scope.models.model = date;
                })
                .error(function(err) {
                    Console.log("error ", err);
                });
        };

        $scope.Search = function() {
            var query = "";
            var oneParam = true;

            //запрос на марку
            if ($scope.maker != null && $scope.maker != "" && $scope.maker != "0") {
                query += " where Maker = '" + $scope.maker + "'";
                oneParam = false;
            }
            //добавляем поиск по модели
            if ($scope.modela != null && $scope.modela != "" && $scope.modela != "0") {
                if (oneParam) {
                    query += " where Model = '" + $scope.modela + "'";
                    oneParam = false;
                } else {
                    query += " And Model = '" + $scope.modela + "'";
                }
            }
            //добавляем поиск по типу топлива
            if ($scope.fuel != null && $scope.fuel != "" && $scope.fuel != "0") {
                if (oneParam) {
                    query += " where Type_fuel = '" + $scope.fuel + "'";
                    oneParam = false;
                } else {
                    query += " And Type_fuel = '" + $scope.fuel + "'";
                }
            }
            //добавляем запрос по объему двигателя
            if (($scope.v1 != null && $scope.v1 != "") && ($scope.v2 != null && $scope.v2 != "")) {
                if (oneParam) {
                    query += " where V_engine >= '" + $scope.v1 + "' And V_engine <= '" + $scope.v2 + "'";
                    oneParam = false;
                } else {
                    query += " And V_engine >= '" + $scope.v1 + "' And V_engine <= '" + $scope.v2 + "'";
                }
            }
            if (($scope.v1 != null && $scope.v1 != "") && !($scope.v2 != null && $scope.v2 != "")) {
                if (oneParam) {
                    query += " where V_engine >= '" + $scope.v1 + "'";
                    oneParam = false;
                } else {
                    query += " And V_engine >= '" + $scope.v1 + "'";
                }
            }
            if (!($scope.v1 != null && $scope.v1 != "") && ($scope.v2 != null && $scope.v2 != "")) {
                if (oneParam) {
                    query += " where V_engine <= '" + $scope.v2 + "'";
                    oneParam = false;
                } else {
                    query += " And V_engine <= '" + $scope.v2 + "'";
                }
            }
            //добавляем запрос по цене
            if (($scope.price1 != null && $scope.price1 != "") && ($scope.price2 != null && $scope.price2 != "")) {
                if (oneParam) {
                    query += " where Price >= '" + $scope.price1 + "' And Price <= '" + $scope.price2 + "'";
                    oneParam = false;
                } else {
                    query += " And Price >= '" + $scope.price1 + "' And Price <= '" + $scope.price2 + "'";
                }
            }
            if (($scope.price1 != null && $scope.price1 != "") && !($scope.price2 != null && $scope.price2 != "")) {
                if (oneParam) {
                    query += " where Price >= '" + $scope.price1 + "'";
                    oneParam = false;
                } else {
                    query += " And Price >= '" + $scope.price1 + "'";
                }
            }
            if (!($scope.price1 != null && $scope.price1 != "") && ($scope.price2 != null && $scope.price2 != "")) {
                if (oneParam) {
                    query += " where Price <= '" + $scope.price2 + "'";
                    oneParam = false;
                } else {
                    query += " And Price <= '" + $scope.price2 + "'";
                }
            }
            //добавляем запрос по пробегу
            if (($scope.run1 != null && $scope.run1 != "") && ($scope.run2 != null && $scope.run2 != "")) {
                if (oneParam) {
                    query += " where Milleage >= '" + $scope.run1 + "' And Milleage <= '" + $scope.run2 + "'";
                    oneParam = false;
                } else {
                    query += " And Milleage >= '" + $scope.run1 + "' And Milleage <= '" + $scope.run2 + "'";
                }
            }
            if (($scope.run1 != null && $scope.run1 != "") && !($scope.run2 != null && $scope.run2 != "")) {
                if (oneParam) {
                    query += " where Milleage >= '" + $scope.run1 + "'";
                    oneParam = false;
                } else {
                    query += " And Milleage >= '" + $scope.run1 + "'";
                }
            }
            if (!($scope.run1 != null && $scope.run1 != "") && ($scope.run2 != null && $scope.run2 != "")) {
                if (oneParam) {
                    query += " where Milleage <= '" + $scope.run2 + "'";
                    oneParam = false;
                } else {
                    query += " And Milleage <= '" + $scope.run2 + "'";
                }
            }
            $rootScope.$emit("rootScope:emit", query);
        };

        $scope.Clear = function () {
            $scope.maker = "";
            $scope.modela = "";
            $scope.fuel = "";
            $scope.v1 = "";
            $scope.v2 = "";
            $scope.price1 = "";
            $scope.price2 = "";
            $scope.run1 = "";
            $scope.run2 = "";
        };
    })

    .controller("IndexMakers", function ($scope, $http) {

        $http({ method: "GET", url: "GetMakers", param: { sql: "" } })
            .success(function (date) {
                $scope.makers = date;
            })
            .error(function (err) {
                Console.log("error ", err);
            });

    })