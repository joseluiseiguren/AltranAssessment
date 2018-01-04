class PoliciesSearch {

    private ordenamiento: OrdenamientoDto = new OrdenamientoDto();
    private paginacion: PaginacionDto = new PaginacionDto();
    private page: JQuery;
    private loader: JQuery;
    private clientId: string;

    constructor(page: JQuery, loader: JQuery, clientId: string) {
        let that = this;

        this.page = page;
        this.loader = loader;
        this.clientId = clientId;

        this.ordenamiento.asc = true;
        this.ordenamiento.columnaOrdenamiento = "id";
        this.paginacion.pagina = 1;
        this.paginacion.filasPorPagina = that.GetFilasPorPagina();

        // inicio de la pagina
        page.ready(function () {
            that.SetDefaultOrder();
            that.QueryData(
                that.GetPolicyIdFilter(),
                that.GetAmountInsuredFilter(),
                that.GetEmailFilter(),
                that.GetInstallmentPaymentFilter(),
                that.GetInceptionDateFilter(),
                that.paginacion,
                that.ordenamiento);
        });

        // submit de filtros
        page.find("#frmFiltros").submit(function (event) {
            event.preventDefault();
            that.SetDefaultOrder();
            that.QueryData(
                that.GetPolicyIdFilter(),
                that.GetAmountInsuredFilter(),
                that.GetEmailFilter(),
                that.GetInstallmentPaymentFilter(),
                that.GetInceptionDateFilter(),
                that.paginacion,
                that.ordenamiento);
        });

        // cambio en el combo de filas por pagina
        page.find("#filasPorPagina").change(function () {
            that.paginacion.pagina = 1;
            that.SetPaginaActual(that.paginacion.pagina);
            that.paginacion.filasPorPagina = that.GetFilasPorPagina();
            that.QueryData(
                that.GetPolicyIdFilter(),
                that.GetAmountInsuredFilter(),
                that.GetEmailFilter(),
                that.GetInstallmentPaymentFilter(),
                that.GetInceptionDateFilter(),
                that.paginacion,
                that.ordenamiento);
        });

        // siguiente pagina
        page.find("#nextPage").click(function () {
            that.paginacion.pagina += 1;
            that.SetPaginaActual(that.paginacion.pagina);
            that.QueryData(
                that.GetPolicyIdFilter(),
                that.GetAmountInsuredFilter(),
                that.GetEmailFilter(),
                that.GetInstallmentPaymentFilter(),
                that.GetInceptionDateFilter(),
                that.paginacion,
                that.ordenamiento);
        });

        // pagina anterior
        page.find("#prevPage").click(function () {
            that.paginacion.pagina -= 1;
            that.SetPaginaActual(that.paginacion.pagina);
            that.QueryData(
                that.GetPolicyIdFilter(),
                that.GetAmountInsuredFilter(),
                that.GetEmailFilter(),
                that.GetInstallmentPaymentFilter(),
                that.GetInceptionDateFilter(),
                that.paginacion,
                that.ordenamiento);
        });

        // ordenamiento por id
        page.find("#colPolicyId").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == "id") ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = "id";
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(
                that.GetPolicyIdFilter(),
                that.GetAmountInsuredFilter(),
                that.GetEmailFilter(),
                that.GetInstallmentPaymentFilter(),
                that.GetInceptionDateFilter(),
                that.paginacion,
                that.ordenamiento);
        });

        // ordenamiento por amount insured
        page.find("#colAmount").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == "AmountInsured") ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = "AmountInsured";
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(
                that.GetPolicyIdFilter(),
                that.GetAmountInsuredFilter(),
                that.GetEmailFilter(),
                that.GetInstallmentPaymentFilter(),
                that.GetInceptionDateFilter(),
                that.paginacion,
                that.ordenamiento);
        });

        // ordenamiento por email
        page.find("#colEmail").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == "Email") ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = "Email";
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(
                that.GetPolicyIdFilter(),
                that.GetAmountInsuredFilter(),
                that.GetEmailFilter(),
                that.GetInstallmentPaymentFilter(),
                that.GetInceptionDateFilter(),
                that.paginacion,
                that.ordenamiento);
        });

        // ordenamiento por inception date
        page.find("#colInceptionDate").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == "InceptionDate") ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = "InceptionDate";
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(
                that.GetPolicyIdFilter(),
                that.GetAmountInsuredFilter(),
                that.GetEmailFilter(),
                that.GetInstallmentPaymentFilter(),
                that.GetInceptionDateFilter(),
                that.paginacion,
                that.ordenamiento);
        });

        // ordenamiento por installment payment
        page.find("#colInstallmentPayment").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == "InstallmentPayment") ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = "InstallmentPayment";
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(
                that.GetPolicyIdFilter(),
                that.GetAmountInsuredFilter(),
                that.GetEmailFilter(),
                that.GetInstallmentPaymentFilter(),
                that.GetInceptionDateFilter(),
                that.paginacion,
                that.ordenamiento);
        });
    }

    private QueryData(
        policyId: string,
        amountInsured: number,
        email: string,
        installmentPayment: boolean,
        inceptionDate: Date,
        paginado: PaginacionDto,
        orden: OrdenamientoDto): void {
        let that = this;
        var url = "/api/policies?clientid=" + this.clientId +
            "&policyId=" + policyId +
            ((amountInsured > 0) ? ("&amountInsured=" + amountInsured) : ('')) +
            "&installmentPayment=" + installmentPayment +
            "&inceptionDate=" + inceptionDate +
            "&email=" + email +
            "&pagina=" + paginado.pagina +
            "&filasPorPagina=" + paginado.filasPorPagina +
            "&Asc=" + orden.asc +
            "&ColumnaOrdenamiento=" + orden.columnaOrdenamiento;
        that.HideLoader(false);
        that.HideTable(true);
        that.DisableFilters(true);
        that.SetMessage('');
        that.EmptyTable();
        $.getJSON(url, function (data) {
            that.SetTotalRegistros(data.CantidadRegistros);
            that.SetCantidadPaginas(data.CantidadPaginas);
            that.SetPaginaActual(data.PaginaActual);

            if (data.Lista.length > 0) {
                var trHTML = '';
                $.each(data.Lista, function () {
                    trHTML += that.RenderRow(this);
                });
                that.SetTableBody(trHTML);
                that.HideTable(false);
                that.DisableFilters(false);
            } else {
                that.SetMessage('No records found');
                that.DisableFilters(false);
            }

            // se acomoda el paginado (next)
            if (that.paginacion.pagina >= data.CantidadPaginas) {
                that.DisableNext(true);
            } else {
                that.DisableNext(false);
            }

            // se acomoda el paginado (prev)
            if (that.paginacion.pagina == 1) {
                that.DisablePrev(true);
            } else {
                that.DisablePrev(false);
            }

            that.HideLoader(true);

        }).fail(function (data, textStatus, xhr) {
            that.HideLoader(true);
            that.DisableFilters(false);

            // se valida si el error fue por unauthorized
            ValidateUnauthorized(data.status);

            // TODO: mostrar bien el error
            that.SetMessage('Error: ' + data.status);
            console.log(data);
            console.log(textStatus);
            console.log(xhr);
        });
    }

    private GetFilasPorPagina(): number {
        return Number(this.page.find('#filasPorPagina').val());
    }

    private RemoveOrderIcons(): void {
        TableHeaderRemoveOrderIcons(this.page.find('#tPolicies .th i'));
    }

    private SetDefaultOrder(): void {
        var that = this;
        that.RemoveOrderIcons();
        TableHeaderOrderIconAsc(this.page.find('#tPolicies #colPolicyId i'));
        this.ordenamiento.columnaOrdenamiento = "id";
        this.paginacion.pagina = 1;
    }

    private HideLoader(hide: boolean): void {
        if (hide) {
            this.loader.hide();
        } else {
            this.loader.show();
        }
    }

    private EmptyTable(): void {
        this.page.find('#tPolicies > tbody').empty();
    }

    private HideTable(hide: boolean): void {
        if (hide) {
            this.page.find('#tPolicies').hide();
        } else {
            this.page.find('#tPolicies').show();
        }
    }

    private DisableFilters(disable: boolean): void {
        this.page.find('.filtros :input').prop("disabled", disable);
    }

    private SetMessage(message: string): void {
        this.page.find('#seachMessages').text(message);
    }

    private SetTableBody(html: string): void {
        this.page.find('#tPolicies > tbody').append(html);
    }

    private SetTotalRegistros(tot: number): void {
        this.page.find('#totalregistros').text(tot);
    }

    private SetCantidadPaginas(tot: number): void {
        this.page.find('#totalpaginas').text(tot);
    }

    private SetPaginaActual(tot: number): void {
        this.page.find('#paginaactual').text(tot);
    }

    private RenderRow(data): string {
        var trHTML = '';
        var installmentPayment = (data.InstallmentPayment == true) ? 'Yes' : 'No';
        trHTML = '<tr class="tr"><td class="td">' + data.Id + '</td><td class="td right">' + data.AmountInsured + '</td><td class="td">' + data.Email + '</td><td class="td">' + data.InceptionDate + '</td><td class="td">' + installmentPayment + '</td></tr>';
        return trHTML;
    }

    private DisableNext(disable: boolean): void {
        this.page.find('#nextPage').prop("disabled", disable);
    }

    private DisablePrev(disable: boolean): void {
        this.page.find('#prevPage').prop("disabled", disable);
    }

    private GetPolicyIdFilter(): string {
        return this.page.find('#filterId').val();
    }

    private GetAmountInsuredFilter(): number {
        return Number(this.page.find('#filterAmount').val());
    }

    private GetEmailFilter(): string {
        return this.page.find('#filterEmail').val();
    }

    private GetInstallmentPaymentFilter(): boolean {
        if (this.page.find('#filterInstallmentPayment').prop("selectedIndex") < 2) {
            return null;
        }
        return this.page.find('#filterInstallmentPayment').val();
    }

    private GetInceptionDateFilter(): Date {
        return this.page.find('#filterInceptionDate').val();
    }
}