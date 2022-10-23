$(document).ready(function () {
    $(".populate-search-select").select2({
        language: "ru",
        theme: "bootstrap",
        ajax: {
            url: '/ettn/tnvedProductCodeType/tnveds-list',
            data: function (params) {
                return {
                    codeOrName: params.term
                };
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
                        return {
                            id: item.code,
                            text: item.displayText
                        };
                    }),
                };
            }
        }
    });
});