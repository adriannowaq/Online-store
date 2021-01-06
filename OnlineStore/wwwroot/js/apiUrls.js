const apiUrls = {};

function addApiUrl(key, url) {
    if (apiUrls[key] === undefined)
        apiUrls[key] = url;
}