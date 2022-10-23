function TinCheckTaxPayer() {
    var pin = $('#Pin').val();
    $("#Name").val('');
    if (pin.length === 14) {
        ajaxRequestMade = true;
        $.ajax({
            type: 'GET',
            url: '/TinCheck/GetTaxPayer',
            dataType: 'json',
            data: {
                Tin: pin
            },
            statusCode: {
                200: function (data)
                {
                    if (data) {
                        $("#Name").val(data.name);
                    }
                }
            }
        });
    }
    else {
        ajaxRequestMade = false;
    }
};
$('#TinCheckSubjectBtn').click(function () {
    alert($(this).html());
});
function TinCheckSubject(id) {
    var pin = document.getElementById('Pin' + id).value;
 
    $("#Name" + id).val('');
    if (pin.length === 14) {
        ajaxRequestMade = true;
        $.ajax({
            type: 'GET',
            url: '/TinCheck/GetTaxPayer',
            dataType: 'json',
            data: {
                Tin: pin
            },
            statusCode: {
                200: function (data) {
                    if (data) {
                        $("#Name" + id).val(data.name); 
                    }
                }
            }
        });
    }
    else {
        ajaxRequestMade = false;
    }
};