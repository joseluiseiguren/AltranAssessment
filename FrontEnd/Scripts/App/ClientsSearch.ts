/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/knockout/bindinghandlers.d.ts" />


class ClientsSearch {
    // Filters
    clientIdFilter: KnockoutObservable<string>
    nameFilter: KnockoutObservable<string>
    emailFilter: KnockoutObservable<string>
    roleFilterValues: KnockoutObservableArray<CboRole>;
    roleFilter: KnockoutObservable<number>;

    // Paginacion
    rowsPerPageValues: KnockoutObservableArray<number>;
    rowsPerPage: KnockoutObservable<number>;
    totalRecords: KnockoutObservable<number>
    totalPages: KnockoutObservable<number>
    actualPage: KnockoutObservable<number>

    // Ordenamiento
    ordenamientoCol: KnockoutObservable<string>;
    ordenamientoAsc: KnockoutObservable<boolean>;

    // lista de clientes
    clients = ko.observableArray<ClientDto>();

    // para saber si habilitar o deshabilitar los controles
    isWorking: KnockoutComputed<boolean>
    private _isWorking: KnockoutObservable<boolean>;

    // para mostrar distintos tipos de mensajes
    messages: KnockoutObservable<string>

    private colsOrdenamiento = {
        clientId: "id",
        name: "name",
        email: "email",
        role: "role"
    };

    constructor() {
        this.clientIdFilter = ko.observable(null);
        this.nameFilter = ko.observable(null);
        this.emailFilter = ko.observable(null);
        this.roleFilter = ko.observable(null);

        this.roleFilterValues = ko.observableArray<CboRole>();
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
                    } else {
                        viewModel.ordenamientoAsc(true);
                        viewModel.ordenamientoCol(prop);
                    }

                    viewModel.actualPage(1);
                    viewModel.QueryData();
                };
            }
        };
    }

    private setDefaultOrder() {
        this.ordenamientoAsc = ko.observable(true);
        this.ordenamientoCol = ko.observable(this.colsOrdenamiento.clientId);
    }

    public applyFilter() {
        this.actualPage(1);
        this.QueryData();
    }

    public nextPage() {
        if (this.actualPage() >= this.totalPages()) {
            return;
        }
        this.actualPage(this.actualPage() + 1);
        this.QueryData();
    }

    public prevPage() {
        if (this.actualPage() == 1) {
            return;
        }
        this.actualPage(this.actualPage() - 1);
        this.QueryData();
    }

    private QueryData(): void {
        let that = this;

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
            } else {
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
    }

    private RoleToString(): string {
        if (this.roleFilter() == 0) {
            return 'Admin';
        }

        if (this.roleFilter() == 1) {
            return 'User';
        }

        return '';
    }


}



