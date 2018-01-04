function TableHeaderRemoveOrderIcons(item) {
    item.removeClass('fa fa-caret-up');
    item.removeClass('fa fa-caret-down');
}
function TableHeaderOrderIconAsc(item) {
    item.addClass('fa fa-caret-up');
}
function TableHeaderOrderIconDesc(item) {
    item.addClass('fa fa-caret-down');
}
function ValidateUnauthorized(status) {
    if (status == 401) {
        window.location.replace("users/logout");
    }
}
//# sourceMappingURL=Globales.js.map