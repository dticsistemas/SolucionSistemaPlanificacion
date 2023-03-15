const MODELO_BASE = {
    idEmpresa: 0,
    codigo: "",
    nombre: "",
    esActivo: 1,
}

let tablaData;

$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Carpeta/ListaMisCarpetas',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idCarpeta", "visible": true, "searchable": true },
            { "data": "numeroCarpeta" },
            { "data": "citeUnidadPlanificacion" },
            { "data": "unidadSolicitante" },
            { "data": "unidadResponsable" },
            { "data": "montoTotal" },
            {
                "data": "estadoCarpeta", render: function (data) {
                    if (data == 1)
                        return '<span class="badge badge-info">Activo</span>';
                    else
                        return '<span class="badge badge-info">Registrado</span>';
                }
            },           
           //{ "data": "unidadSolicitante" },
            { "data": "fechaRegistro" },
            {
                "defaultContent":'<div class="form-inline">'+
                    '<button class="btn btn-info btn-ver btn-sm"><i class="fas fa-eye"></i></button>' +
                    '<button class="btn btn-primary btn-editar btn-sm"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>' +
                    '</div>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Listado Mis Carpetas',
                exportOptions: {
                    columns: [1, 2, 3]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
})


let filaSeleccionada;

$("#tbdata tbody").on("click", ".btn-ver", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }

    const data = tablaData.row(filaSeleccionada).data();
   
    $("#txtFechaRegistro").val(data.fechaRegistro)
    $("#txtNumVenta").val(data.numeroCarpeta)
    $("#txtUsuarioRegistro").val(data.idRegional)
    $("#txtTipoDocumento").val(data.citeUnidadPlanificacion)
    $("#txtDocumentoCliente").val(data.operacion)
    $("#txtNombreCliente").val(data.unidadResponsable)
    $("#txtSubTotal").val(data.tipo)
    $("#txtIGV").val(data.estado)
    $("#txtTotal").val(data.montoTotal)    
    $("#tbProductos tbody").html("")
    cont = 0;
    data.detalleCarpeta.forEach((item) => {
        cont++;
        $("#tbProductos tbody").append(
            $("<tr>").append(
                $("<td>").text(cont),
                $("<td>").text(item.detalle),
                $("<td>").text(item.partida),
                $("<td>").text(item.unidadMedida),
                $("<td>").text(item.precioTotal)
            )
        )
    })
    $("#linkImprimir").attr("href", `/Carpeta/MostrarPDFCarpeta?numeroCarpeta=${data.numeroCarpeta}`);
    $("#modalData").modal("show");

})


