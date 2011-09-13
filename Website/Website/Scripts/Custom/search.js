﻿var t;
var list = {};

$(document).ready(function () {
    t = new $.TextboxList('#t2', { unique: true });
});

function add() {
    var value = $("#t1").val();
    var e = document.getElementById("search");
    var strUser = e.options[e.selectedIndex].value;
    t.add(strUser + '=' + value);
};

var unique = function (origArr) {
    var newArr = [],
        origLen = origArr.length,
        found,
        x, y;

    for (x = 0; x < origLen; x++) {
        found = undefined;
        for (y = 0; y < newArr.length; y++) {
            if (origArr[x] === newArr[y]) {
                found = true;
                break;
            }
        }
        if (!found) newArr.push(origArr[x]);
    }
    return newArr;
};
function getSearchValues() {
    list = {};
    for (i = 0; i < t.getValues().length; i++) {
        var searchStr = t.getValues()[i][1];
        var splitStr = searchStr.split('=');
        list[splitStr[0]] = splitStr[1];
    }
};

function onDataBinding(e) 
{
    var grid = $(this).data('tGrid');
    getSearchValues();
  
    // call the twitter search api
    $.ajax({
        url: 'http://localhost/Kallivayalil/Search/Search',
        type: 'POST',
        dataType: 'json',
        error: function (xhr, status) {
            alert(status);
        },
        data: list,
        accept: "application/json",
        success: function (result) {
            grid.dataBind(result);
  
        }
    });
};



function search() {
  
    $('#ConstituentsGrid').data('tGrid').ajaxRequest();
}
