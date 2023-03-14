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
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
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
