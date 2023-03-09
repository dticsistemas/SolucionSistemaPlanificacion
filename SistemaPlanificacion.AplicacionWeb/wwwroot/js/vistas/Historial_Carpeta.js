const VISTA_BUSQUEDA = {
    busquedaFecha: () => {
        $("txtFechaInicio").val("");
        $("txtFechaFin").val("");
        $("txtNumeroVenta").val("");

        $(".busqueda-fecha").show();
        $(".busqueda-venta").hide();
    }, busquedaVenta: () => {
        $("txtFechaInicio").val("");
        $("txtFechaFin").val("");
        $("txtNumeroVenta").val("");

        $(".busqueda-fecha").hide();
        $(".busqueda-venta").show();
    }
}


$(document).ready(function () {


    VISTA_BUSQUEDA["busquedaFecha"]()

    $.datepicker.setDefaults($.datepicker.regional["es"])

    $("#txtFechaInicio").datepicker({ dateFormat: " dd/mm/yy" })
    $("#txtFechaFin").datepicker({ dateFormat: " dd/mm/yy" })

   
})

$("#cboBuscarPor").change(function () {

    if ($("#cboBuscarPor").val() == "fecha") {
        VISTA_BUSQUEDA["busquedaFecha"]()
    } else {
        VISTA_BUSQUEDA["busquedaVenta"]()
    }
})

$("#btnBuscar").click(function () {


    if ($("#cboBuscarPor").val() == "fecha") {
        if ( $("#txtFechaInicio").val().trim == "" || $("#txtFechaFin").val().trim() == "") {
            toastr.warning("", "Debe ingresar fecha inicio y fin")
            return;
        }
    } else {
        if ($("#txtNumeroVenta").val().trim == "") {
            toastr.warning("", "Debe ingresar el numero de Carpeta")
            return;
        }
    }
    let numeroCarpeta = $("#txtNumeroVenta").val()
    let fechaInicio = $("#txtFechaInicio").val()
    let fechaFin = $("#txtFechaFin").val()



    
    $(".card-body").find("div.row").LoadingOverlay("show");

    fetch(`/Carpeta/Historial_Busqueda?numeroCarpeta=${numeroCarpeta}&fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)
        .then(response => {
            console.log(response);
            $(".card-body").find("div.row").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            //-------------------------
            console.log(responseJson);
            $("#tbventa tbody").html("");
            if (responseJson.length > 0) {
                responseJson.forEach((venta) => {
                    $("#tbventa tbody").append(
                        $("<tr>").append(
                            $("<td>").text(venta.fechaRegistro),
                            $("<td>").text(venta.numeroCarpeta),
                            $("<td>").text(venta.citeUnidadPlanificacion),
                            $("<td>").text(venta.idRegional),
                            $("<td>").text(venta.idActividad),
                            $("<td>").text(venta.montoTotal),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-info btn-sm").append(
                                    $("<i>").addClass("fas fa-eye")
                                ).data("venta", venta)
                            )


                        )
                    )
                })
            }

            //---------------------------
        })
})

$("#tbventa tbody").on("click", ".btn-info", function () {
    let d = $(this).data("venta")

    
    
    $("#txtFechaRegistro").val(d.fechaRegistro)
    $("#txtNumVenta").val(d.numeroCarpeta)
    $("#txtUsuarioRegistro").val(d.idRegional)
    $("#txtTipoDocumento").val(d.citeUnidadPlanificacion)
    $("#txtDocumentoCliente").val(d.operacion)
    $("#txtNombreCliente").val(d.unidadResponsable)
    $("#txtSubTotal").val(d.tipo)
    $("#txtIGV").val(d.estado)
    $("#txtTotal").val(d.montoTotal)

    $("#tbProductos tbody").html("")
    cont = 0;
    d.detalleCarpeta.forEach((item) => {
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
    $("#linkImprimir").attr("href",`/Carpeta/MostrarPDFCarpeta?numeroCarpeta=${d.numeroCarpeta}`);
    $("#modalData").modal("show");
})

