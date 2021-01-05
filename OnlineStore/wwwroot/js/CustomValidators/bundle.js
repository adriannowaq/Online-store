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
$.validator.methods.range = function (value, element, param) {
    let globalizedValue = value.replace(",", ".");
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
}
 
$.validator.methods.number = function (value, element) {
    return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
}

$.validator.messages.number = 'Wprowadź poprawną liczbę.';
$.validator.addMethod("max-file-size", function (value, element, params) {
    if (element.files[0].size > params)
        return false;

    return true;
});

$.validator.unobtrusive.adapters.add('max-file-size', ['value'], function (options) {
    options.rules['max-file-size'] = parseInt(options.params['value']);
    options.messages['max-file-size'] = options.message;
})