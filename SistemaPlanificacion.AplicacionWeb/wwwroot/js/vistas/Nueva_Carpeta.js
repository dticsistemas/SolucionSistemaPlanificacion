﻿/*const MODELO_BASE = {
    idUsuario: 0,
    codigo: "",
    nombre: "",
    correo: "",
    telefono: "",
    idRol: 0,
    esActivo: 1,
    urlFoto: ""
}

let tablaData;
*/

let PartidasParaCarpeta = [];

$(document).ready(function () {

    fetch("/Carpeta/ListaTipoDocumentoCarpeta")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboTipoDocumentoVenta").append(
                        $("<option>").val(item.idRol).text(item.descripcion)
                    )
                })
            }
        })

    $('#cboBuscarProducto').select2({
        ajax: {
            url: "/Carpeta/ObtenerPartidasPresupuestarias",
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            delay: 250,
            data: function (params) {
                return {
                    busqueda: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.map((item) => (
                        {
                            id: item.idPartida,
                            text: item.nombre,
                            codigo: item.codigo
                        }
                    ))
                };
            },
            cache: true
        },
        languaje: "es",
        placeholder: "Buscar Partida Presupuestaria",
        minimumInputLength: 1,
        templateResult: formatoResultados,
        //  templateSelection: formatRepoSelection
    });
    function formatoResultados(data) {
        if (data.loading)
            return data.text;

        //var contenedor = $('<table width="100%"><tr><tr><td style="width:60px">  CODIGO:'+$(data.codigo)+' </td>$(data.id) <p>  $(data.text) </td> </tr> </table>');   

        var $container = $(
            "<div class='select2-result-repository clearfix'>" +
            "<div class='select2-result-repository__title'></div>" +
            "<div class='select2-result-repository__description'></div>" +
            "</div>"
        );
        $container.find(".select2-result-repository__title").text(data.id);
        $container.find(".select2-result-repository__description").text(data.text);
        return $container;
        //return contenedor;
    }


    $(document).on("select2:open", function () {
        document.querySelector(".select2-search__field").focus();
    })





    $("#cboBuscarProducto").on("select2:select", function (e) {
        const data = e.params.data;
        let producto_encontrado = PartidasParaCarpeta.filter(p => p.id == data.id)
        if (producto_encontrado.lebgth > 0) {
            $("#cboBuscarProducto").val("").trigger("change")
            toast.warning("", "La partida ya fue agregada")
            return false
        }
        // console.log(data);

        swal({
            title: `Partida:[${data.codigo}] : ${data.text} `,
            html: true,
            text: '<hr><table width="100% ">' +
                "<tr> <td> Detalle Requerimiento</td> </tr>" +
                '<tr><td><textarea id="txtSwalDetalle" class="form- control" rows="3"  style=" width:100% !important "></textarea></td></tr>' +
                "<tr> <td> Unidad de Medida</td> </tr>" +
                '<tr><td><input type="text" id="txtSwalUnidadMedida" style=" width: 100% !important "/></td></tr>' +
                "<tr> <td> Cantidad</td> </tr>" +
                '<tr><td><input type="text" id="txtSwalCantidad" style=" width: 100% !important "/></td></tr>' +
                "<tr> <td> Precio Unitario</td> </tr>" +
                '<tr><td><input type="text" id="txtSwalPrecioUnitario" style=" width: 100% !important "/></td></tr>' +

                "</table> ",
            showCancelButton: true,
            closeOnCancel: true,
            confirmButtonText: "Guardar",
        }, function (respuesta) {
            if (respuesta === false) return false;

            var uDetalle = $('#txtSwalDetalle').val();
            var uMedida = $('#txtSwalUnidadMedida').val();
            var uCantidad = $('#txtSwalCantidad').val();
            var uPrecioUnitario = $('#txtSwalPrecioUnitario').val();

            let item = {
                /*
                id:data.id,
                partida: data.codigo,
                detalle:uDetalle,
                unidadMedida:uMedida,
                cantidad: uCantidad,
                precioUnitario: uPrecioUnitario,
                total: (parseFloat(uCantidad) * parseFloat(uPrecioUnitario))
                */
               // idDetalle:214,
                idCarpeta: 5,
                partida: data.codigo,
                detalle: uDetalle,
                unidadMedida: uMedida,
                precioTotal: (parseFloat(uCantidad) * parseFloat(uPrecioUnitario)),
                montoPlanificado: 0,
                montoPresupuestado: 0,
                montoAdjudicado: 0
            }
            PartidasParaCarpeta.push(item)

            mostrarItemDetalle();
            $("#cboBuscarProducto").val("").trigger("change")
            swal.close()
        }

        );

    })
    function mostrarItemDetalle() {


        cont = 1;
        mTotal = 0;
        $("#tbProducto tbody").html("");
        console.log("---Mostrando Items Detallados----");
        console.log(PartidasParaCarpeta);
        console.log("---end show----");
        PartidasParaCarpeta.forEach((item) => {
            cont++;
            mTotal = (mTotal + parseFloat(item.precioTotal));
            $("#tbProducto tbody").append(
                $("<tr>").append(
                    $("<td>").append(
                        $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                            $("<i>").addClass("fas fa-trash-alt")
                        ).data("idDetalle", item.idCarpeta)
                    ),
                    $("<td>").text(item.partida),
                    $("<td>").text(item.detalle),
                    $("<td>").text(item.unidadMedida),
                    $("<td>").text(item.unidadMedida),
                    $("<td>").text(item.precioTotal),
                    $("<td>").text(item.montoPlanificado)
                )
            )

        })

        $("#txtTotal").val(mTotal);
        //--logica para calcular Total
    }
    $(document).on("click", "button.btn-eliminar", function () {
        const _idDetalle = $(this).data("idDetalle");
        alert(_idDetalle);
        PartidasParaCarpeta = PartidasParaCarpeta.filter(p => p.idCarpeta != _idDetalle);
        mostrarItemDetalle();
    })
    $("#btnTerminarCarpeta").click(function () {
        if (PartidasParaCarpeta.length < 1) {
            toastr.warning("", "Debe ingresar requerimientos")
            return;
        }
        const vmDetalleCarpeta = PartidasParaCarpeta;
      //  alert(vmDetalleCarpeta);
        console.log(vmDetalleCarpeta);
        const carpetaRequerimiento = {

            IdCarpeta: 1,// $("#txtNombre").val(), default
            NumeroCarpeta: "1200",//$("#txtNombre").val(),
            IdRegional: 1, //$("#txtNombre").val(),
            CiteUnidadPlanificacion: $("#txtCiteUnidadSolicitante").val(), //ok
            TipoSolicitante: 1,//$("#txtNombre").val(),
            UnidadSolicitante: 1, //$("#txtNombre").val(),
            CertificadoPoa: "", ///$("#txtNombre").val(),
            UnidadResponsable: 1, //$("#txtNombre").val(),
            CodOperacion: $("#txtCodOperacion").val(), //ok
            Operacion: $("#txtOperacion").val(), //ok
            IdActividad: 1,// $("#txtIdActividad").val(),//ok
            MontoTotal: $("#txtTotal").val(),//ok
            MontoTotalPlanificacion: 0,//"", //$("#txtNombre").val(),
            Tipo: "A", //$("#txtNombre").val(),
            Estado: "",//$("#txtNombre").val(),
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
         })


    })

})

