/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/knockout/bindinghandlers.d.ts" />
var ClientsSearch = (function () {
    function ClientsSearch() {
        // lista de clientes
        this.clients = ko.observableArray();
        this.colsOrdenamiento = {
            clientId: "id",
            name: "name",
            email: "email",
            role: "role"
        };
        this.clientIdFilter = ko.observable(null);
        this.nameFilter = ko.observable(null);
        this.emailFilter = ko.observable(null);
        this.roleFilter = ko.observable(null);
        this.roleFilterValues = ko.observableArray();
        this.roleFilterValues.push(new CboRole(-1, '<Any>'));
        this.roleFilterValues.push(new CboRole(0, 'Admin'));
        this.roleFilterValues.push(new CboRole(1, 'User'));
        this.roleFilter = ko.observable(-1);
        this.totalRecords = ko.observable(null);
        this.totalPages = ko.observable(null);
        this.actualPage = ko.observable(1);
        this.rowsPerPageValues = ko.observableArray([10, 20, 30]);
        this.rowsPerPage = ko.observable(10);
        this._isWorking = ko.observable(true);
        this.isWorking = ko.computed({
            owner: this,
            read: function () {
                return this._isWorking();
            }
        });
        this.messages = ko.observable("");
        this.setDefaultOrder();
        this.QueryData();
        ko.bindingHandlers.sort = {
            init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                var asc = false;
                element.style.cursor = 'pointer';
                element.onclick = function () {
                    var value = valueAccessor();
                    var prop = value.prop;
                    if (prop == viewModel.ordenamientoCol()) {
                        viewModel.ordenamientoAsc(!viewModel.ordenamientoAsc());
                    }
                    else {
                        viewModel.ordenamientoAsc(true);
                        viewModel.ordenamientoCol(prop);
                    }
                    viewModel.actualPage(1);
                    viewModel.QueryData();
                };
            }
        };
    }
    ClientsSearch.prototype.setDefaultOrder = function () {
        this.ordenamientoAsc = ko.observable(true);
        this.ordenamientoCol = ko.observable(this.colsOrdenamiento.clientId);
    };
    ClientsSearch.prototype.applyFilter = function () {
        this.actualPage(1);
        this.QueryData();
    };
    ClientsSearch.prototype.nextPage = function () {
        if (this.actualPage() >= this.totalPages()) {
            return;
        }
        this.actualPage(this.actualPage() + 1);
        this.QueryData();
    };
    ClientsSearch.prototype.prevPage = function () {
        if (this.actualPage() == 1) {
            return;
        }
        this.actualPage(this.actualPage() - 1);
        this.QueryData();
    };
    ClientsSearch.prototype.QueryData = function () {
        var that = this;
        that.messages("");
        that._isWorking(true);
        that.clients.removeAll();
        var url = "/api/clients?id=" + ((this.clientIdFilter() === null) ? '' : this.clientIdFilter()) +
            "&name=" + ((this.nameFilter() === null) ? '' : this.nameFilter()) +
            "&email=" + ((this.emailFilter() === null) ? '' : this.emailFilter()) +
            "&role=" + this.RoleToString() +
            "&pagina=" + this.actualPage() +
            "&filasPorPagina=" + this.rowsPerPage() +
            "&Asc=" + ((that.ordenamientoAsc() == true) ? "true" : "false") +
            "&ColumnaOrdenamiento=" + that.ordenamientoCol();
        $.getJSON(url, function (data) {
            that.totalRecords(data.CantidadRegistros);
            that.totalPages(data.CantidadPaginas);
            that.actualPage(data.PaginaActual);
            if (data.Lista.length > 0) {
                $.each(data.Lista, function () {
                    var p = new ClientDto;
                    p.id = this.Id;
                    p.email = this.Email;
                    p.id = this.Id;
                    p.name = this.Name;
                    p.role = this.Role;
                    that.clients.push(p);
                });
            }
            else {
                that.messages("No records found");
            }
            that._isWorking(false);
        }).fail(function (data, textStatus, xhr) {
            that._isWorking(false);
            // se valida si el error fue por unauthorized
            ValidateUnauthorized(data.status);
            that.messages("Error: " + data.status);
            console.log(data);
            console.log(textStatus);
            console.log(xhr);
        });
    };
    ClientsSearch.prototype.RoleToString = function () {
        if (this.roleFilter() == 0) {
            return 'Admin';
        }
        if (this.roleFilter() == 1) {
            return 'User';
        }
        return '';
    };
    return ClientsSearch;
}());
//# sourceMappingURL=ClientsSearch.js.map