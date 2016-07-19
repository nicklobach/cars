(function update(maker, id) {
    $http({
        method: "GET",
        url: '@Url.Action("_MakersInBikes")',
        date: {maker : maker},
        succes: function(data) {
            $(id).replaceWith(data);
        }
    });
});