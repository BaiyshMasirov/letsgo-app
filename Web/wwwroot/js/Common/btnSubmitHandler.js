$('form').submit(function () {
    if ($("form").valid()) {
        $(this).find("button[type='submit']").prop('disabled', true);
    }
});