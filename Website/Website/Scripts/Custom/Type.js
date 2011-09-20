  
function onEdit(e) {
    $(e.form).find('#Type').data('tDropDownList').select(function (dataItem) {
        return dataItem.Text == e.dataItem['Type'];
    });
}


function onAutoCompleteChange(e) {
    alert(e.value);
}

