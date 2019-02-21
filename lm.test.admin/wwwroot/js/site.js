function fillContent(data) {
    $("#content").html("").html(data);
}

function goAction(ele) {
    var action = $(ele).data("action");
    $.get(action, {}, function (data) {
        fillContent(data)
    });
}

function blogCreateSubmit() {
    $("#blog-create").ajaxSubmit(function (data) {
        fillContent(data)
    });
    return false;
}

function blogEditSubmit() {
    $("#blog-edit").ajaxSubmit(function (data) {
        fillContent(data)
    });
    return false;
}

