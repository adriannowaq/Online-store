$(document).ready(function () {
    $('.ui.search')
        .search({
            apiSettings: {
                url: `${apiUrls["search"]}?name={query}`
            },
            fields: {
                results: "products",
                title: "name",
                url: "url"
            },
            error: {
                noResults: 'Brak rezulatatów.',
            },
            minCharacters: 2
        });
});