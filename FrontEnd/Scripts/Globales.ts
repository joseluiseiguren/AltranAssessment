function TableHeaderRemoveOrderIcons(item: JQuery): void {
    item.removeClass('fa fa-caret-up');
    item.removeClass('fa fa-caret-down');
}

function TableHeaderOrderIconAsc(item: JQuery): void {
    item.addClass('fa fa-caret-up');
}

function TableHeaderOrderIconDesc(item: JQuery): void {
    item.addClass('fa fa-caret-down');
}