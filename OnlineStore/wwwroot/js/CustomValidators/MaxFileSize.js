$.validator.addMethod("max-file-size", function (value, element, params) {
    if (element.files[0].size > params)
        return false;

    return true;
});

$.validator.unobtrusive.adapters.add('max-file-size', ['value'], function (options) {
    options.rules['max-file-size'] = parseInt(options.params['value']);
    options.messages['max-file-size'] = options.message;
})