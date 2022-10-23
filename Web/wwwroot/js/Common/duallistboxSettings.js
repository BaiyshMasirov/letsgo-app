$(document).ready(function ()
{
    $('.bootstrap-duallistbox').bootstrapDualListbox({
        preserveSelectionOnMove: 'moved',
        moveOnSelect: false,
        filterPlaceHolder: "Поиск",
        infoText: "Показаны все {0}",
        infoTextEmpty: "Данных нет",
        removeAllLabel: "Удалить все",
        moveAllLabel: "Добавить все",
        filterTextClear: "Показать все",
        infoTextFiltered: '<span class="label label-warning">Отфильтровано</span> {0} из {1}'
    });
});