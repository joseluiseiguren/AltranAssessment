class ClientesSearch {

    private ordenamiento: OrdenamientoDto = new OrdenamientoDto();
    private paginacion: PaginacionDto = new PaginacionDto();
    private page: JQuery;
    private loader: JQuery;

    constructor(page: JQuery, loader: JQuery) {
        let that = this;

        this.page = page;
        this.loader = loader;

        this.ordenamiento.asc = true;
        this.ordenamiento.columnaOrdenamiento = "id";
        this.paginacion.pagina = 1;
        this.paginacion.filasPorPagina = that.GetFilasPorPagina();

        // inicio de la pagina
        page.ready(function () {
            that.SetDefaultOrder();
            that.QueryData(
                that.GetId(),
                that.GetName(),
                that.GetEmail(),
                that.GetRole(),
                that.paginacion,
                that.ordenamiento);
        });

        // submit de filtros
        page.find("#frmFiltros").submit(function (event) {
            event.preventDefault();
            that.SetDefaultOrder();
            that.QueryData(
                that.GetId(),
                that.GetName(),
                that.GetEmail(),
                that.GetRole(),
                that.paginacion,
                that.ordenamiento);
        });

        // cambio en el combo de filas por pagina
        page.find("#filasPorPagina").change(function () {
            that.paginacion.pagina = 1;
            that.SetPaginaActual(that.paginacion.pagina);
            that.paginacion.filasPorPagina = that.GetFilasPorPagina();
            that.QueryData(
                that.GetId(),
                that.GetName(),
                that.GetEmail(),
                that.GetRole(),
                that.paginacion,
                that.ordenamiento);
        });

        // siguiente pagina
        page.find("#nextPage").click(function () {
            that.paginacion.pagina += 1;
            that.SetPaginaActual(that.paginacion.pagina);
            that.QueryData(
                that.GetId(),
                that.GetName(),
                that.GetEmail(),
                that.GetRole(),
                that.paginacion,
                that.ordenamiento);
        });

        // pagina anterior
        page.find("#prevPage").click(function () {
            that.paginacion.pagina -= 1;
            that.SetPaginaActual(that.paginacion.pagina);
            that.QueryData(
                that.GetId(),
                that.GetName(),
                that.GetEmail(),
                that.GetRole(),
                that.paginacion,
                that.ordenamiento);
        });

        // ordenamiento por id
        page.find("#colId").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == "id") ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = "id";
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(
                that.GetId(),
                that.GetName(),
                that.GetEmail(),
                that.GetRole(),
                that.paginacion,
                that.ordenamiento);
        });

        // ordenamiento por name
        page.find("#colName").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == "name") ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = "name";
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(
                that.GetId(),
                that.GetName(),
                that.GetEmail(),
                that.GetRole(),
                that.paginacion,
                that.ordenamiento);
        });

        // ordenamiento por email
        page.find("#colEmail").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == "email") ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = "email";
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(
                that.GetId(),
                that.GetName(),
                that.GetEmail(),
                that.GetRole(),
                that.paginacion,
                that.ordenamiento);
        });

        // ordenamiento por role
        page.find("#colRole").click(function () {
            that.paginacion.pagina = 1;
            that.ordenamiento.asc = (that.ordenamiento.columnaOrdenamiento == "role") ? !that.ordenamiento.asc : true;
            that.ordenamiento.columnaOrdenamiento = "role";
            that.RemoveOrderIcons();
            (that.ordenamiento.asc) ? TableHeaderOrderIconAsc($(this).find('i')) : TableHeaderOrderIconDesc($(this).find('i'));
            that.QueryData(
                that.GetId(),
                that.GetName(),
                that.GetEmail(),
                that.GetRole(),
                that.paginacion,
                that.ordenamiento);
        });

    }

    private QueryData(
        id: string,
        name: string,
        email: string,
        role: string,
        paginado: PaginacionDto,
        orden: OrdenamientoDto): void {
        let that = this;
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

            // TODO: mostrar bien el error
            that.SetMessage('Error: ' + data.status);

            // se valida si el error fue por unauthorized
            ValidateUnauthorized(data.status);

            console.log("data status: " + data.status);
            console.log("text: " + textStatus);
            console.log("xhr: " + xhr);
        });
    }

    private GetId(): string {
        return this.page.find('#filterId').val();
    }

    private GetName(): string {
        return this.page.find('#filterName').val();
    }

    private GetEmail(): string {
        return this.page.find('#filterEmail').val();
    }

    private GetRole(): string {
        return (this.page.find('#filterRole').prop("selectedIndex") < 2) ? '' : this.page.find('#filterRole').val();
    }

    private GetFilasPorPagina(): number {
        return Number(this.page.find('#filasPorPagina').val());
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

    private HideLoader(hide: boolean): void {
        if (hide) {
            this.loader.hide();
        } else {
            this.loader.show();
        }
    }

    private DisableFilters(disable: boolean): void {
        this.page.find('.filtros :input').prop("disabled", disable);
    }

    private SetMessage(message: string): void {
        this.page.find('#seachMessages').text(message);
    }

    private SetTableBody(html: string): void {
        this.page.find('#tClientes > tbody').append(html);
    }

    private EmptyTable(): void {
        this.page.find('#tClientes > tbody').empty();
    }

    private HideTable(hide: boolean): void {
        if (hide){
            this.page.find('#tClientes').hide();
        } else {
            this.page.find('#tClientes').show();
        }
    }

    private DisableNext(disable: boolean): void {
        this.page.find('#nextPage').prop("disabled", disable);
    }

    private DisablePrev(disable: boolean): void {
        this.page.find('#prevPage').prop("disabled", disable);
    }

    private RenderRow(data): string{
        var trHTML = '';
        var urlPolicy = 'clients/' + data.Id + '/policies';
        trHTML = '<tr class="tr"><td class="td"><a target="_blank" href="' + urlPolicy + '">' + data.Id + '</a></td><td class="td">' + data.Name + '</td><td class="td">' + data.Email + '</td><td class="td">' + data.Role + '</td></tr>';
        return trHTML;
    }

    private RemoveOrderIcons(): void {
        TableHeaderRemoveOrderIcons(this.page.find('#tClientes .th i'));
    }

    private SetDefaultOrder(): void {
        var that = this;
        that.RemoveOrderIcons();
        TableHeaderOrderIconAsc(this.page.find('#tClientes #colId i'));
        this.ordenamiento.columnaOrdenamiento = "id";
        this.paginacion.pagina = 1;
    }

}