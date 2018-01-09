/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/knockout/bindinghandlers.d.ts" />


class PoliciesSearch {
    // Filters
    policyIdFilter: KnockoutObservable<string>
    amountInsuredFilter: KnockoutObservable<number>
    emailFilter: KnockoutObservable<string>
    inceptionDateFilter: KnockoutObservable<Date>
    installmentPaymentFilterValues: KnockoutObservableArray<CboInstallmentPayment>;
    installmentPaymentFilter: KnockoutObservable<number>;

    // Paginacion
    rowsPerPageValues: KnockoutObservableArray<number>;
    rowsPerPage: KnockoutObservable<number>;
    totalRecords: KnockoutObservable<number>
    totalPages: KnockoutObservable<number>
    actualPage: KnockoutObservable<number>

    // Ordenamiento
    ordenamientoCol: KnockoutObservable<string>;
    ordenamientoAsc: KnockoutObservable<boolean>;

    // lista de policies
    policies = ko.observableArray<PolicyDto>();

    // para saber si habilitar o deshabilitar los controles
    isWorking: KnockoutComputed<boolean>
    private _isWorking: KnockoutObservable<boolean>;

    // client id para buscar las policies
    private clientId: string;

    // para mostrar distintos tipos de mensajes
    messages: KnockoutObservable<string>

    private colsOrdenamiento = {
        policyId: "id",
        amountInsured: "amountinsured",
        email: "email",
        inceptionDate: "inceptiondate",
        installmentPayment: "installmentpayment"
    };

    constructor(clientId: string) {
        this.clientId = clientId;
        this.policyIdFilter = ko.observable(null);
        this.amountInsuredFilter = ko.observable(null);
        this.emailFilter = ko.observable(null);
        this.inceptionDateFilter = ko.observable(null);

        this.installmentPaymentFilterValues = ko.observableArray<CboInstallmentPayment>();
        this.installmentPaymentFilterValues.push(new CboInstallmentPayment(-1, '<Any>'));
        this.installmentPaymentFilterValues.push(new CboInstallmentPayment(1, 'Yes'));
        this.installmentPaymentFilterValues.push(new CboInstallmentPayment(0, 'No'));
        this.installmentPaymentFilter = ko.observable(-1);

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
        this.ordenamientoCol = ko.observable(this.colsOrdenamiento.policyId);
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
        that.policies.removeAll();

        var url = "/api/policies?clientid=" + this.clientId +
            "&policyId=" + ((this.policyIdFilter() === null) ? '' : this.policyIdFilter()) +
            "&amountInsured=" + ((this.amountInsuredFilter() === null) ? '' : this.amountInsuredFilter()) +
            "&installmentPayment=" + this.InstallmentPaymentFilterToString() + 
            "&inceptionDate=" + ((this.inceptionDateFilter() === null) ? '' : this.inceptionDateFilter()) +
            "&email=" + ((this.emailFilter() === null) ? '' : this.emailFilter()) +
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
                    var p = new PolicyDto();
                    p.amountInsured = this.AmountInsured;
                    p.email = this.Email;
                    p.id = this.Id;
                    p.inceptionDate = this.InceptionDate;
                    p.installmentPayment = this.InstallmentPayment;

                    that.policies.push(p);
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

    private InstallmentPaymentFilterToString(): string {
        if (this.installmentPaymentFilter() == 0) {
            return 'false';
        }

        if (this.installmentPaymentFilter() == 1) {
            return 'true';
        }

        return '';
    }

    
}



