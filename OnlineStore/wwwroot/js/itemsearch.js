$(document).ready(function () {
    var
        content = [
            {
                title: 'Horse',
                description: 'An Animal',
            },
            {
                title: 'Cow',
                description: 'Another Animal',
            }
        ]
        ;
    $('.ui.search')
        .search({
            source: content
        })
        ;
});