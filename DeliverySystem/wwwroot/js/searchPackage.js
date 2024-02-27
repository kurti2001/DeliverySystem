const txtSearchBoxQuery = $('#txtSearchBoxQuery');
function searchAsync() {
    const query = txtSearchBoxQuery.val();
    if (query && query.length && query.length > 2) {
        $.get(BASE_APP_PATH + '/package/search?q=' + query, function (res) {
            drawPackage(res);
        });
    }
    else {
        $('#search-results').html('');
    }
}

function drawPackage(products) {
    const html = []

    for (let package of package) {
        html.push(`<li>
                     <a href="${BASE_APP_PATH}/package/details/${package.id}">
                        ${package.name} - ${package.price.toFixed(2)}
                     </a>
                    </li>`);
    }

    if (!html.length) {
        html.push('<li>&#169; There is no info!</li>')
    }

    $('#search-results').html(html.join(''));
}