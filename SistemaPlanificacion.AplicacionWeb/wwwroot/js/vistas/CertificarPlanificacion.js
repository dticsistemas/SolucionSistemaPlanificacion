    var idFila=1;
    var nro=0;
    var montoTotal=0;

 

    function clear_data_solicitud(){
    $('#textPartida').val(null);
    $('#textDetalle').val(null);
    $('#textMedida').val(null);
    $('#textPrecio').val(null);
    $('#textPresupuesto').val(null);
    }

function guardarCertificacionPOA(){
   var montoTotal=0;
   var count = $('#tbdatos').children().length;          
   
    if ($('#textCodigoCertificacion').val() != '') {
        var tbl = $("#tbdatos").children();           
        var contador = 0;
        var montosVacios = false;
        $("#tbdatos").children().each(function () {
            console.log("Fila :"+contador + "columna 4: ");
            var idDetalle = $(this)[0].cells[0].firstChild.data;
            console.log("IdDetalle: " + idDetalle);
            var txtMontoPresupuesto = $("#txtMontoPresupuesto_" + idDetalle).val();
            console.log("Monto: " + txtMontoPresupuesto);
            if (txtMontoPresupuesto == "")
                montosVacios = true;                
        });
        if (montosVacios) {
            swal("Lo Sentimos", "Ingrese los montos Presupuestarios", "warning")
        } else {
            //--------------------------------------           
            alert("enviando datos");
           /* const vmDetalleCarpeta = PartidasParaCarpeta;
            //  alert(vmDetalleCarpeta);
            console.log(vmDetalleCarpeta);
            const carpetaRequerimiento = {
                IdCarpeta: 1,// $("#txtNombre").val(), default
                NumeroCarpeta: "1200",//$("#txtNombre").val(),
                IdRegional: 1, //$("#txtNombre").val(),
                Lugar: 'SANTA CRUZ',
                CiteUnidadPlanificacion: $("#txtCiteUnidadSolicitante").val(), //ok
                TipoSolicitante: 1,//$("#txtNombre").val(),
                UnidadSolicitante: 1, //$("#txtNombre").val(),
                CertificadoPoa: "", //$("#txtNombre").val(),
                UnidadResponsable: 1, //$("#txtNombre").val(),
                CodOperacion: 1, //ok
                Operacion: "Operacion", //ok
                IdActividad: 3,// $("#txtIdActividad").val(),//ok
                MontoTotal: parseFloat($("#txtTotal").val()),//ok
                MontoTotalPlanificacion: 0,//"", //$("#txtNombre").val(),
                MontoTotalPresupuesto: 0,
                MontoTotalCompras: 0,
                Tipo: "A", // $("#txtNombre").val(),
                EstadoCarpeta: "A",//$("#txtNombre").val(),
                IdUnidadProceso: 1,
                DetalleCarpeta: vmDetalleCarpeta
            }
            console.log(carpetaRequerimiento);
            $("#btnTerminarCarpeta").closest("div.card-body").LoadingOverlay("show");

            fetch("/Carpeta/RegistrarCarpeta", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(carpetaRequerimiento)
            }).then(response => {
                console.log("----response: ");
                console.log(response);
                $("#btnTerminarCarpeta").closest("div.card-body").LoadingOverlay("hide");
                //  if (response.ok) {
                console.log("-Carpeta Registrada-");
                PartidasParaCarpeta = [];
                mostrarItemDetalle();
                $("#txtCiteUnidadSolicitante").val("");
                swal("Registrado!!", 'Carpeta Registrada', "success")

                //} else {
                console.log("-No se pudo registrar Carpeta -");
                //} 

                return response.ok ? response.json() : Promise.reject(response);
            }).then(responseJson => {
                console.log("--respuesta Json-");
                console.log(responseJson);
                if (responseJson.estado) {
                    swal("Registrado!", `Numero ID Carpeta : ${responseJason.objeto.idCarpeta}`, "success")
                } else {
                    swal("Lo sentimos!", 'No se pudo registrar la Carpeta', "error")
                }
            })*/
            //--------------------------------------

        }
              
    }else{
            swal("Lo Sentimos", "Ingrese Codigo de Certificación", "warning")
    }
    

}

