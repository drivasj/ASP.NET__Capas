window.onload = function () {
    listarPersona();
    listarCombo();
   // previewImagen();

}

//function previewImagen() {

//    // Capturamos el Element
//    var fupFoto = document.getElementById("fupFoto");
//    var imgFoto = document.getElementById("imgFoto");

//    //Cuando se seleccione una imagen la mostramos
//    fupFoto.onchange = function () {
//        var file = fupFoto.files[0];
//        var reader = new FileReader();
//        reader.onloadend = function () {
//            document.getElementById("imgFoto").src = reader.result;
//        }
//        reader.readAsDataURL(file)
//    }
//}

function listarPersona() {
    pintar({
        popup: true,
        idpopup: "defaultModal",
        url: "Persona/listarPersona", id: "divTabla",
        cabeceras: ["ID Persona", "Nombre Completo", "Rol","Estado"],
        propiedades: ["iidpersona", "nombreCompleto", "nombreRol","bhabilitado"],
        editar: true,
        // sizepopup: "modal-lg",
        eliminar: true,
        propiedadId: "iidpersona",
       // columnaimg: ["fotobase64"],
        excel: false,
        callbackexcel: function () {
            var urlAction = "GenerarExcel"
            window.location.href = urlAction;
        },
    })
}

function listarCombo() {
    fetchGet("Rol/listarRol", function (data) {
        llenarCombo(data, "cboTipoUsuario", "nombre", "iidrol", "0")
        llenarCombo(data, "cboTipousuarioForm", "nombre", "iidrol")
    })
}

function filtrarPersonaTipousuario() {
    var iidrol = get("cboTipoUsuario");
    pintar({
        url: "Persona/FiltrarPersonaRol/?iidrol=" + iidrol, id: "divTabla",
        cabeceras: ["ID Persona", "Nombre Completo", "Rol","Estado"],
        propiedades: ["iidpersona", "nombreCompleto", "nombreRol","bhabilitado"],
        editar: true,
        eliminar: true,
        propiedadId: "iidpersona"
    })
    // alert(iidtipousuario);
}

function Editar(id) {
    Limpiar();
    //Nuevo
    if (id == 0) {
        document.getElementById("exampleModalLabel").innerHTML = "Nueva Persona";
        document.getElementById("divcontra").style.display = "block"; // mostrar divcontra al crear 
        //Editar
    } else {
        document.getElementById("exampleModalLabel").innerHTML = "Editar Persona";
        document.getElementById("divcontra").style.display = "none"; // Ocultar divcontra al editar

        recuperarGenericoEspecifico("Persona/recuperarPersona/?iidpersona=" + id,
            "frmPersona", [], true);
    }
}

function recuperarEspecifico(res) {
    if (res.nombreusuario == "") document.getElementById("divcontra").style.display = "block";
}

/**
 * 
 * @param {any} res
 * function recuperarEspecifico(res) {

    var iidsexo = res.iidsexo;
    //Masculino
    if (iidsexo == 1) {
        document.getElementById("rbMas").checked = true;
    }
    //femenino
    else {
        document.getElementById("rbFem").checked = true;
    }
}
 * 
 * 
 * 
 * function recuperarEspecifico(res) {
    document.getElementById("imgFoto").src = res.fotobase64;
}
 */


function Guardar() {

    // LLamo a  ValidarObligatorios
    var error = ValidarObligatorios("frmPersona")
    if (error != "") {
        Error(error);
        return;
    }

    // Validar solo números enteros
    var error = ValidarSoloNumerosEnteros("frmPersona")
    if (error != "") {
        Error(error);
        return;
    }

    Confirmacion("Desea guardar el registro ?", "Confirmar Guardar Datos", function (res) {
        var frmPersona = document.getElementById("frmPersona");
        var frm = new FormData(frmPersona);
        fetchPostText("Persona/Guardar", frm, function (res) {
            if (res == "1") {
                document.getElementById("btnCerrar").click();
                listarPersona();
                Limpiar();
                // Correcto("Ok");
            }
        })
    })
}

function Limpiar() {

    LimpiarDatos("frmPersona", ["iidsexo"]);

}

function Eliminar(id) {
    Confirmacion("Desea eliminar el tipo habitacion?", "Confirmar eliminación", function (res) {

        fetchGetText("Persona/eliminarPersona/?iidpersona=" + id, function (rpta) {
            if (rpta == "1") {
                Correcto("Se elimino correctamente");
                listarPersona();
            }
        })
    })
}

function ClickNuevo() {
    objConfiguracionGlobal.callbacknuevo();
}