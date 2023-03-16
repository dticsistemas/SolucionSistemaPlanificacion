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
    var count = $('#tbl_solicitudes').children().length;
   if(count>=0){
     if(confirm('Confirmar el registro de la Certificacion POA?')){  
        if($('#txtCertificadoPoa').val()!=''){
          var tbl = document.getElementById("tbl_solicitudes");
    var solicitudes='';
    var separator='';
    for (var i = 0, row; row = tbl.rows[i]; i++){
        col0 = row.cells[0].innerText;
    col1  = row.cells[1].innerText;
    col2  = row.cells[2].innerText;
    col3  = row.cells[3].innerText;
    col4  = row.cells[4].innerText;
    var monto= $('#txtPresupuesto_'+col0).val();

    var dataItem= col0+"║"+col1+"║"+col2+"║"+col3+"║"+col4+"║"+monto+"";
    solicitudes = solicitudes+separator+dataItem;
    separator='╬';


    montoTotal = montoTotal+parseInt(monto);  
              
          }

    $("#dat_textMontoTotalPresupuesto").val(montoTotal);
    $("#dat_certificado").val($("#txtCertificadoPoa").val());
    $("#data_solicitudes").val(solicitudes);
    $("#form_solicitudes" ).submit();  

        }else{
        alert('Ingrese Codigo de certificación');
        }
      } 
    }else{
        alert('Asigne una solicitud para editar');
    }

}

