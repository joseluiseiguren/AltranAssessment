var ClientesSearch = (function () {
    function ClientesSearch(page, loader) {
        this.ordenamiento = new OrdenamientoDto();
        this.paginacion = new PaginacionDto();
        var that = this;
        this.page = page;
        this.loader = loader;
        this.ordenamiento.asc = true;
        this.ordenamiento.columnaOrdenamiento = "id";
        this.paginacion.pagina = 1;
        this.paginacion.filasPorPagina = that.GetFilasPorPagina();
        // inicio de la pagina
        page.ready(function () {
            that.SetDefaultOrder();
            that.QueryData(that.GetId(), that.GetName(), that.GetEmail(), that.GetRole(), that.paginacion, that.ordenamiento);
        });
        // submit de filtros
        page.find("#frmFiltros").submit(function (event) {
            event.preventDefault();
            that.SetDefaultOrder();
            that.QueryData(that.GetId(), that.GetName(), that.GetEmail(), that.GetRole(), that.paginacion, that.ordenamiento);
        });
        // cambio en el combo de filas por pagina
        page.find("#filasPorPagina").change(function () {
            that.paginacion.pagina = 1;
            that.SetPaginaActual(that.paginacion.pagina);
            that.paginacion.filasPorPagina = that.GetFilasPorPagina();
            that.QueryData(that.GetId(), that.GetName(), that.GetEmail(), that.GetRole(), that.paginacion, that.ordenamiento);
        });
        // siguiente pagina
        page.find("#nextPage").click(function () {
            that.paginacion.pagina += 1;
            that.SetPaginaActual(that.paginacion.pagina);
            that.QueryData(that.GetId(), that.GetName(), that.GetEmail(), that.GetRole(), that.paginacion, that.ordenamiento);
        });
        // pagina anterior
        page.find("#prevPage").click(function () {
            that.paginacion.pagina -= 1;
            that.SetPaginaActual(that.paginacion.pagina);
            that.QueryData(that.GetId(), that.GetName(), that.GetEmail(), that.GetRole(), that.paginacion, that.ordenamiento);
        });
        // ordenamiento por id
        page.find("#colId").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == "id") ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = "id";
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(that.GetId(), that.GetName(), that.GetEmail(), that.GetRole(), that.paginacion, that.ordenamiento);
        });
        // ordenamiento por name
        page.find("#colName").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == "name") ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = "name";
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(that.GetId(), that.GetName(), that.GetEmail(), that.GetRole(), that.paginacion, that.ordenamiento);
        });
        // ordenamiento por email
        page.find("#colEmail").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == "email") ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = "email";
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(that.GetId(), that.GetName(), that.GetEmail(), that.GetRole(), that.paginacion, that.ordenamiento);
        });
        // ordenamiento por role
        page.find("#colRole").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == "role") ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = "role";
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(that.GetId(), that.GetName(), that.GetEmail(), that.GetRole(), that.paginacion, that.ordenamiento);
        });
    }
    ClientesSearch.prototype.QueryData = function (id, name, email, role, paginado, orden) {
        var that = this;
        var url = "/api/clients?id=" + id +
            "&name=" + name +
            "&email=" + email +
            "&role=" + role +
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
            that.DisableFilters(true);
            // TODO: mostrar bien el error
            that.SetMessage('Error: ' + textStatus);
            console.log(data);
            console.log(textStatus);
            console.log(xhr);
        });
    };
    ClientesSearch.prototype.GetId = function () {
        return this.page.find('#filterId').val();
    };
    ClientesSearch.prototype.GetName = function () {
        return this.page.find('#filterName').val();
    };
    ClientesSearch.prototype.GetEmail = function () {
        return this.page.find('#filterEmail').val();
    };
    ClientesSearch.prototype.GetRole = function () {
        return (this.page.find('#filterRole').prop("selectedIndex") < 2) ? '' : this.page.find('#filterRole').val();
    };
    ClientesSearch.prototype.GetFilasPorPagina = function () {
        return Number(this.page.find('#filasPorPagina').val());
    };
    ClientesSearch.prototype.SetTotalRegistros = function (tot) {
        this.page.find('#totalregistros').text(tot);
    };
    ClientesSearch.prototype.SetCantidadPaginas = function (tot) {
        this.page.find('#totalpaginas').text(tot);
    };
    ClientesSearch.prototype.SetPaginaActual = function (tot) {
        this.page.find('#paginaactual').text(tot);
    };
    ClientesSearch.prototype.HideLoader = function (hide) {
        if (hide) {
            this.loader.hide();
        }
        else {
            this.loader.show();
        }
    };
    ClientesSearch.prototype.DisableFilters = function (disable) {
        this.page.find('.filtros :input').prop("disabled", disable);
    };
    ClientesSearch.prototype.SetMessage = function (message) {
        this.page.find('#seachMessages').text(message);
    };
    ClientesSearch.prototype.SetTableBody = function (html) {
        this.page.find('#tClientes > tbody').append(html);
    };
    ClientesSearch.prototype.EmptyTable = function () {
        this.page.find('#tClientes > tbody').empty();
    };
    ClientesSearch.prototype.HideTable = function (hide) {
        if (hide) {
            this.page.find('#tClientes').hide();
        }
        else {
            this.page.find('#tClientes').show();
        }
    };
    ClientesSearch.prototype.DisableNext = function (disable) {
        this.page.find('#nextPage').prop("disabled", disable);
    };
    ClientesSearch.prototype.DisablePrev = function (disable) {
        this.page.find('#prevPage').prop("disabled", disable);
    };
    ClientesSearch.prototype.RenderRow = function (data) {
        var trHTML = '';
        var urlPolicy = 'clients/' + data.Id + '/policies';
        trHTML = '<tr class="tr"><td class="td"><a target="_blank" href="' + urlPolicy + '">' + data.Id + '</a></td><td class="td">' + data.Name + '</td><td class="td">' + data.Email + '</td><td class="td">' + data.Role + '</td></tr>';
        return trHTML;
    };
    ClientesSearch.prototype.RemoveOrderIcons = function () {
        TableHeaderRemoveOrderIcons(this.page.find('#tClientes .th i'));
    };
    ClientesSearch.prototype.SetDefaultOrder = function () {
        var that = this;
        that.RemoveOrderIcons();
        TableHeaderOrderIconAsc(this.page.find('#tClientes #colId i'));
        this.ordenamiento.columnaOrdenamiento = "id";
        this.paginacion.pagina = 1;
    };
    return ClientesSearch;
}());
//# sourceMappingURL=Clientes.js.map