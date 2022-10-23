$(document).ready(function () {

    $('#region-select').on('change', function () {
        let regionId = $(this).val();

        $.ajax({
            type: 'GET',
            url: '/ettn/Coate/districts',
            dataType: 'json',
            data: {
                regionId: regionId
            },
            success: function (districts) {
                $('#rayon-code').empty();
                $('#district-select').empty();

                $('#district-select').append('<option value="">Выберите район или город областного значения</option>');

                if (districts.length > 0) {
                    $.each(districts, function (key, value) {
                        $('#district-select').append(new Option(value.name, value.id));

                    });
                }               

                $('#countryside-select').empty();

                $('#countryside-select').append('<option value="">Выберите айыльный аймак</option>');

                $('#village-select').empty();

                $('#village-select').append('<option value="">Выберите поселок</option>');
            }
        });
    });


    $('#district-select').on('change', function () {
        let districtId = $(this).val();

        $.ajax({
            type: 'GET',
            url: '/ettn/Coate/countrysides',
            dataType: 'json',
            data: {
                districtId: districtId
            },
            success: function (countrysides) {
                $('#countryside-select').empty();
                $('#rayon-code').empty();

                $('#countryside-select').append('<option value="">Выберите айыльный аймак</option>');

                if (countrysides.length > 0) {
                    $.each(countrysides, function (key, value) { 
                        $('#countryside-select').append(new Option(value.name, value.id));                       
                    });
                }
                else {
                    setupRayonCodeByDistrict(districtId);
                }
                $('#village-select').empty();

                $('#village-select').append('<option value="">Выберите поселок</option>');
            }
        });
    });

    function setupRayonCodeByDistrict(districtId) {
        $.ajax({
            type: 'GET',
            url: '/ettn/coate/getrayonbydistrict',
            dataType: 'json',
            data: {
                districtId: districtId
            },
            success: function (result) { 
                var option = new Option(result.rayonCode + ' - ' + result.rayonName, result.rayonCode, true, true);
                $('#rayon-code').append(option);
            } 
        });
    }

    $('#countryside-select').on('change', function () {
        let countrysideId = $(this).val();

        $.ajax({
            type: 'GET',
            url: '/ettn/Coate/villages',
            dataType: 'json',
            data: {
                countrysideId: countrysideId
            },
            success: function (villages) {
                $('#rayon-code').empty();
                $('#village-select').empty();

                $('#village-select').append('<option value="">Выберите поселок</option>');

                if (villages.length > 0) {
                    $.each(villages, function (key, value) {
                        $('#village-select').append(new Option(value.name, value.id));
                    });
                }
                setupRayonCodeByCountrySide(countrysideId);
            }
        });
    });

    function setupRayonCodeByCountrySide(countrysideId) {
        $.ajax({
            type: 'GET',
            url: '/ettn/coate/getrayonbycountryside',
            dataType: 'json',
            data: {
                countrysideId: countrysideId
            },
            success: function (result) {
                var option = new Option(result.rayonCode + ' - ' + result.rayonName, result.rayonCode, true, true);
                $('#rayon-code').append(option);
            } 
        });
    }
});