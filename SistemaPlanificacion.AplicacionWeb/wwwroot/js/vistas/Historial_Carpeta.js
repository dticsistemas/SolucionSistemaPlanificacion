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
        if ($("txtFechaInicio").val().trim == "" || $("#txtFechaFin").val().trim() == "") {
            toastr.warning("", "Debe ingresar fecha inicio y fin")
            return;
        }
    } else {
        if ($("txtNUmeroVenta").val().trim == "") {
            toastr.warning("", "Debe ingresar el numero de Carpeta")
            return;
        }
    }
    let numeroVenta = $("txtNUmeroVenta").val()
    let fechaInicio = $("txtFechaInicio").val()
    let fechaFin = $("txtFechaFin").val()


    $(".card-body").find("div.row").LoadingOverlay("show");
    fetch('Venta/Historial_Busqueda?numeroCarpeta=$(numeroCarpeta)')
        .then(response => {

        })

})

