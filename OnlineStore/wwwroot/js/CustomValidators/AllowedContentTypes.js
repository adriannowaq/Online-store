$.validator.addMethod("allowed-file-extensions", function (value, element, params) {
    let allowedExtensions = params.split(",");
    if (!allowedExtensions.includes(element.files[0].type))
        return false;

    return true;
});

$.validator.unobtrusive.adapters.add('allowed-file-extensions', ['value'], function (options) {
    options.rules['allowed-file-extensions'] = options.params['value'];
    options.messages['allowed-file-extensions'] = options.message;
})