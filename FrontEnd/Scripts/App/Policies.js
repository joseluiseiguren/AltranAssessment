var PoliciesSearch = (function () {
    function PoliciesSearch(page, loader, clientId) {
        this.ordenamiento = new OrdenamientoDto();
        this.paginacion = new PaginacionDto();
        this.colsOrdenamiento = {
            policyId: "id",
            amountInsured: "amountinsured",
            email: "email",
            inceptionDate: "inceptiondate",
            installmentPayment: "installmentpayment"
        };
        var that = this;
        this.page = page;
        this.loader = loader;
        this.clientId = clientId;
        this.ordenamiento.asc = true;
        this.ordenamiento.columnaOrdenamiento = that.colsOrdenamiento.policyId;
        this.paginacion.pagina = 1;
        this.paginacion.filasPorPagina = that.GetFilasPorPagina();
    }
    PoliciesSearch.prototype.QueryData = function (policyId, amountInsured, email, installmentPayment, inceptionDate, paginado, orden) {
        var that = this;
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
            }
            else {
                that.SetMessage('No records found');
                that.DisableFilters(false);
            }
            // se acomoda el paginado (next)
            if (that.paginacion.pagina >= data.CantidadPaginas) {
                that.DisableNext(true);
            }
            else {
                that.DisableNext(false);
            }
            // se acomoda el paginado (prev)
            if (that.paginacion.pagina == 1) {
                that.DisablePrev(true);
            }
            else {
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
    };
    PoliciesSearch.prototype.GetFilasPorPagina = function () {
        return Number(this.page.find('#filasPorPagina').val());
    };
    PoliciesSearch.prototype.RemoveOrderIcons = function () {
        TableHeaderRemoveOrderIcons(this.page.find('#tPolicies .th i'));
    };
    PoliciesSearch.prototype.SetDefaultOrder = function () {
        var that = this;
        that.RemoveOrderIcons();
        TableHeaderOrderIconAsc(this.page.find('#tPolicies #colPolicyId i'));
        this.ordenamiento.columnaOrdenamiento = that.colsOrdenamiento.policyId;
        this.paginacion.pagina = 1;
    };
    PoliciesSearch.prototype.HideLoader = function (hide) {
        if (hide) {
            this.loader.hide();
        }
        else {
            this.loader.show();
        }
    };
    PoliciesSearch.prototype.EmptyTable = function () {
        this.page.find('#tPolicies > tbody').empty();
    };
    PoliciesSearch.prototype.HideTable = function (hide) {
        if (hide) {
            this.page.find('#tPolicies').hide();
        }
        else {
            this.page.find('#tPolicies').show();
        }
    };
    PoliciesSearch.prototype.DisableFilters = function (disable) {
        this.page.find('.filtros :input').prop("disabled", disable);
    };
    PoliciesSearch.prototype.SetMessage = function (message) {
        this.page.find('#seachMessages').text(message);
    };
    PoliciesSearch.prototype.SetTableBody = function (html) {
        this.page.find('#tPolicies > tbody').append(html);
    };
    PoliciesSearch.prototype.SetTotalRegistros = function (tot) {
        this.page.find('#totalregistros').text(tot);
    };
    PoliciesSearch.prototype.SetCantidadPaginas = function (tot) {
        this.page.find('#totalpaginas').text(tot);
    };
    PoliciesSearch.prototype.SetPaginaActual = function (tot) {
        this.page.find('#paginaactual').text(tot);
    };
    PoliciesSearch.prototype.RenderRow = function (data) {
        var trHTML = '';
        var installmentPayment = (data.InstallmentPayment == true) ? 'Yes' : 'No';
        trHTML = '<tr class="tr"><td class="td">' +
            data.Id + '</td><td class="td right">' +
            data.AmountInsured + '</td><td class="td">' +
            data.Email + '</td><td class="td">' +
            data.InceptionDate + '</td><td class="td">' +
            installmentPayment + '</td></tr>';
        return trHTML;
    };
    PoliciesSearch.prototype.DisableNext = function (disable) {
        this.page.find('#nextPage').prop("disabled", disable);
    };
    PoliciesSearch.prototype.DisablePrev = function (disable) {
        this.page.find('#prevPage').prop("disabled", disable);
    };
    PoliciesSearch.prototype.GetPolicyIdFilter = function () {
        return this.page.find('#filterId').val();
    };
    PoliciesSearch.prototype.GetAmountInsuredFilter = function () {
        return Number(this.page.find('#filterAmount').val());
    };
    PoliciesSearch.prototype.GetEmailFilter = function () {
        return this.page.find('#filterEmail').val();
    };
    PoliciesSearch.prototype.GetInstallmentPaymentFilter = function () {
        if (this.page.find('#filterInstallmentPayment').prop("selectedIndex") < 2) {
            return null;
        }
        return this.page.find('#filterInstallmentPayment').val();
    };
    PoliciesSearch.prototype.GetInceptionDateFilter = function () {
        return this.page.find('#filterInceptionDate').val();
    };
    PoliciesSearch.prototype.Process = function () {
        var that = this;
        // inicio de la pagina
        that.page.ready(function () {
            that.SetDefaultOrder();
            $('[data-toggle="tooltip"]').tooltip();
            that.QueryData(that.GetPolicyIdFilter(), that.GetAmountInsuredFilter(), that.GetEmailFilter(), that.GetInstallmentPaymentFilter(), that.GetInceptionDateFilter(), that.paginacion, that.ordenamiento);
        });
        // submit de filtros
        that.page.find("#frmFiltros").submit(function (event) {
            event.preventDefault();
            that.SetDefaultOrder();
            that.QueryData(that.GetPolicyIdFilter(), that.GetAmountInsuredFilter(), that.GetEmailFilter(), that.GetInstallmentPaymentFilter(), that.GetInceptionDateFilter(), that.paginacion, that.ordenamiento);
        });
        // cambio en el combo de filas por pagina
        that.page.find("#filasPorPagina").change(function () {
            that.paginacion.pagina = 1;
            that.SetPaginaActual(that.paginacion.pagina);
            that.paginacion.filasPorPagina = that.GetFilasPorPagina();
            that.QueryData(that.GetPolicyIdFilter(), that.GetAmountInsuredFilter(), that.GetEmailFilter(), that.GetInstallmentPaymentFilter(), that.GetInceptionDateFilter(), that.paginacion, that.ordenamiento);
        });
        // siguiente pagina
        that.page.find("#nextPage").click(function () {
            that.paginacion.pagina += 1;
            that.SetPaginaActual(that.paginacion.pagina);
            that.QueryData(that.GetPolicyIdFilter(), that.GetAmountInsuredFilter(), that.GetEmailFilter(), that.GetInstallmentPaymentFilter(), that.GetInceptionDateFilter(), that.paginacion, that.ordenamiento);
        });
        // pagina anterior
        that.page.find("#prevPage").click(function () {
            that.paginacion.pagina -= 1;
            that.SetPaginaActual(that.paginacion.pagina);
            that.QueryData(that.GetPolicyIdFilter(), that.GetAmountInsuredFilter(), that.GetEmailFilter(), that.GetInstallmentPaymentFilter(), that.GetInceptionDateFilter(), that.paginacion, that.ordenamiento);
        });
        // ordenamiento por id
        that.page.find("#colPolicyId").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == that.colsOrdenamiento.policyId) ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = that.colsOrdenamiento.policyId;
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(that.GetPolicyIdFilter(), that.GetAmountInsuredFilter(), that.GetEmailFilter(), that.GetInstallmentPaymentFilter(), that.GetInceptionDateFilter(), that.paginacion, that.ordenamiento);
        });
        // ordenamiento por amount insured
        that.page.find("#colAmount").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == that.colsOrdenamiento.amountInsured) ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = that.colsOrdenamiento.amountInsured;
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(that.GetPolicyIdFilter(), that.GetAmountInsuredFilter(), that.GetEmailFilter(), that.GetInstallmentPaymentFilter(), that.GetInceptionDateFilter(), that.paginacion, that.ordenamiento);
        });
        // ordenamiento por email
        that.page.find("#colEmail").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == that.colsOrdenamiento.email) ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = that.colsOrdenamiento.email;
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(that.GetPolicyIdFilter(), that.GetAmountInsuredFilter(), that.GetEmailFilter(), that.GetInstallmentPaymentFilter(), that.GetInceptionDateFilter(), that.paginacion, that.ordenamiento);
        });
        // ordenamiento por inception date
        that.page.find("#colInceptionDate").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == that.colsOrdenamiento.inceptionDate) ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = that.colsOrdenamiento.inceptionDate;
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(that.GetPolicyIdFilter(), that.GetAmountInsuredFilter(), that.GetEmailFilter(), that.GetInstallmentPaymentFilter(), that.GetInceptionDateFilter(), that.paginacion, that.ordenamiento);
        });
        // ordenamiento por installment payment
        that.page.find("#colInstallmentPayment").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == that.colsOrdenamiento.installmentPayment) ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = that.colsOrdenamiento.installmentPayment;
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(that.GetPolicyIdFilter(), that.GetAmountInsuredFilter(), that.GetEmailFilter(), that.GetInstallmentPaymentFilter(), that.GetInceptionDateFilter(), that.paginacion, that.ordenamiento);
        });
    };
    return PoliciesSearch;
}());
//# sourceMappingURL=Policies.js.map