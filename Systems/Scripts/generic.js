function get(id) {
    return document.getElementById(id).value;
}

function Error(texto = "Ocurrio un error") {
    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: texto
    })
}

function Correcto(texto = "Se realizo correctamente") {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: texto,
        showConfirmButton: false,
        timer: 1500
    })
}

function Confirmacion(texto = "Desea guardar los cambios?", title = "Confirmacion",
    callback) {
    return Swal.fire({
        title: title,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    })
}

function set(id, valor) {
    document.getElementById(id).value = valor;
}
// Para la barra de cargar
function setD(id, valor) {
    document.getElementById(id).style.display = valor;
}

//No sirve por que los input radio tienen check
function setN(id, valor) {
    document.getElementsByName(id)[0].value = valor;
}
//IMG
function setSRC(id, valor) {
    document.getElementsByName(id)[0].src = valor;
}

function setC(selector) {
    document.querySelector(selector).checked = true;
}

//Para las validaciones
function getN(id, valor) {
    return document.getElementsByName(id)[0].value;
}

function ClickNuevo() {
    objConfiguracionGlobal.callbacknuevo();
}

function ClickRegresar() {
    objFormularioGlobal.callbackregresar();
}

function ClickExcel() {
    objConfiguracionGlobal.callbackexcel();
}




function ConfigurarDatos(res,objConfiguracion, objFormulario, objBusqueda) {
    var contenido = "";
    //Configuracion del formulario
    if (objConfiguracion != undefined) {
        if (objConfiguracion.editar == undefined)
            objConfiguracion.editar = false;
        if (objConfiguracion.eliminar == undefined)
            objConfiguracion.eliminar = false;
        if (objConfiguracion.propiedadId == undefined)
            objConfiguracion.propiedadId = "id";
        if (objConfiguracion.callbackEliminar == undefined)
            objConfiguracion.callbackEliminar = "Eliminar";
        if (objConfiguracion.callbackEditar == undefined)
            objConfiguracion.callbackEditar = "Editar";
        if (objConfiguracion.popup == undefined)
            objConfiguracion.popup = false;
        if (objConfiguracion.sizepopup == undefined)
            objConfiguracion.sizepopup = "";
        if (objConfiguracion.recuperarexcepcion == undefined)
            objConfiguracion.recuperarexcepcion = [];
        if (objConfiguracion.iscallbackeditar == undefined)
            objConfiguracion.iscallbackeditar = false;
        if (objConfiguracion.columnaimg == undefined)
            objConfiguracion.columnaimg = [];

        if (objConfiguracion.nuevo == undefined)
            objConfiguracion.nuevo = false;
        if (objConfiguracion.callbacknuevo == undefined)
            objConfiguracion.callbacknuevo = () => { };
        if (objConfiguracion.nuevo == true) {
            contenido += "<button type ='button' onclick='ClickNuevo()'class='btn btn-outline-secondary btn-rounded btn-icon mb-4 ml-4' > <i class='typcn typcn-edit text-primary'></i> </button>";
                
           
        }
        //Reporte Excel
        if (objConfiguracion.excel == undefined)
            objConfiguracion.excel = false;
        if (objConfiguracion.callbackexcel == undefined)
            objConfiguracion.callbackexcel = () => { };
        if (objConfiguracion.excel == true) {
            contenido += "<button class='btn btn-outline-success  mb-3' onclick='ClickExcel()'>Exportar Excel</button>"
        }
          

        objConfiguracionGlobal = objConfiguracion;
    }
    if (objFormulario != undefined) {
        objFormularioGlobal = objFormulario;
        if (objFormulario.guardar == undefined)
            objFormulario.guardar = true
        //-------------------tipo usuario--------///
        if (objFormulario.regresar == undefined)
            objFormulario.regresar = false
        if (objFormulario.callbackregresar == undefined)
            objFormulario.callbackregresar = () => {}
        //----------------------------------------//
      
        if (objFormulario.limpiarexcepcion == undefined)
            objFormulario.limpiarexcepcion = []
        if (objFormulario.limpiar == undefined)
            objFormulario.limpiar = true
        if (objFormulario.formulariogenerico == undefined)
            objFormulario.formulariogenerico = true
        if (objFormulario.callbackGuardar == undefined)
            objFormulario.callbackGuardar = "GuardarDatos"
        if (objFormulario.id == undefined)
            objFormulario.id = "frmFormulario"
        if (objFormulario.tituloconfirmacionguardar == undefined)
            objFormulario.tituloconfirmacionguardar = "Desea guardar los cambios?"
        var type = objFormulario.type;
        if (type == "fieldset") {
            contenido += "<fieldset>";
            if (objFormulario.legend != undefined) {
                contenido += "<legend>" + objFormulario.legend + "</legend>"
            }

            if (objFormulario != undefined) {
                contenido += construirFormulario(objFormulario)
            }
           
            contenido += `
                     ${objFormulario.guardar == true ?
                    `<button class="btn btn-round btn-primary"
                          onclick="${(objFormulario.formulariogenerico == undefined
                        || objFormulario.formulariogenerico == false) ? `${objFormulario.callbackGuardar}()`
                        : `GuardarGenerico
                          ('${objFormulario.id}', '${objFormulario.urlGuardar}')`}">
                                Aceptar</button>` :
                    ''}    
                        ${objFormulario.limpiar == true ?
                    `<button class="btn btn-danger"
                                  onclick="${(objFormulario.formulariogenerico == undefined
                        || objFormulario.formulariogenerico == false) ? "Limpiar" :
                        "LimpiarGenerico"}('${objFormulario == undefined ? "" : objFormulario.id}')">
                                   Limpiar</button>`
                : ''}

                        ${objFormulario.regresar == true ?
                `<button class="btn btn-secondary" onclick='ClickRegresar()'>Regresar</button>` :""  }
                       `
            contenido += "</fieldset>";
        } else if (type == "popup") {
            contenido += `
                                       
                        <button type ='button' class='btn btn-outline-secondary btn-rounded btn-icon mb-4 ml-4'
                            onclick="EditarGenerico(0,'${objFormulario.id}')"
                                data-toggle="modal"
                                data-target="#${objConfiguracion.idpopup}">
                            <i class='typcn typcn-edit text-primary'></i>
                        </button>
                      `
            contenido += `<div class="modal fade"
                                id="${objConfiguracion.idpopup}"
                                tabindex="-1"
                                role="dialog"
                                aria-labelledby="exampleModalLabel"
                                aria-hidden="true">
                              <!--  data-bs-backdrop="static"
                                data-bs-keyboard="false"   -->
                        <div class="modal-dialog ${objConfiguracion.sizepopup}">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="lbl${objConfiguracion.idpopup}"></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                </div>
                                <div class="modal-body">`;
            if (objFormulario != undefined) {
                contenido += construirFormulario(objFormulario)
            }
            
            contenido += `
                                </div>
                                <div class="modal-footer">

                                    <button type="button" class="btn btn-secondary" 
                                  data-dismiss="modal" id='btnCerrar${objConfiguracionGlobal.idpopup}'>
                                    Cerrar</button>

                                    <button type="button" class="btn btn-primary"
                                    onclick="${(objFormulario.formulariogenerico == undefined
                    || objFormulario.formulariogenerico == false) ? `${objFormulario.callbackGuardar}()`
                    : `GuardarGenerico
                          ('${objFormulario.id}', '${objFormulario.urlGuardar}')`}"
                                >Guardar</button>

                                </div>
                            </div>
                        </div>
                    </div>`
        }

    }



    if (objBusqueda != undefined && objBusqueda.busqueda == true) {
        if (objBusqueda.placeholder == undefined)
            objBusqueda.placeholder = "Ingrese un valor"
        if (objBusqueda.id == undefined)
            objBusqueda.id = "txtbusqueda"
        if (objBusqueda.type == undefined)
            objBusqueda.type = "text"
        if (objConfiguracion.id == undefined)
            objConfiguracion.id = "divTabla";
        if (objBusqueda.button == undefined)
            objBusqueda.button = true;

        //Asignar los valores

        objBusquedaGlobal = objBusqueda;
        var type = objBusqueda.type;
        contenido += `
                 <div class="input-group mb-3">`
        if (type == "text") {
            contenido += `
                           <input type="${objBusqueda.type}" class="form-control"
                           id="${objBusqueda.id}"
                         ${objBusqueda.button == true ? "" : "onkeyup='Buscar()'"}  
                       placeholder="${objBusqueda.placeholder}"
                               />`
        } else if (type == "combobox") {
            contenido += `
                            <select class="form-control"
                        ${objBusqueda.button == true ? "" : "onchange='Buscar()'"}  
                            id="${objBusqueda.id}"></select>
                              `
        }

        if (objBusqueda.button == true) {
            contenido += `
                  <button class="btn btn-primary" 
                     onclick="Buscar()"
                      type="button" >
                    Buscar</button>`
        }

        contenido += ` </div>`
    }
    if (objConfiguracion != null && objConfiguracion.url != undefined) {

        contenido += "<div id='divContenedor'>";
        contenido += generarTabla(objConfiguracion, res, objFormulario, true);
        contenido += "</div>";
    }


    document.getElementById(objConfiguracion.id).innerHTML = contenido;
    //---------Datatable---------//
    if (objConfiguracion.paginar == true) {
        $("#tablita").DataTable();
    }
    //---------Datatable---------//


    if (objBusqueda != null) {
        llenarComboBusqueda(res);
    }
    //Aqui llenamos los combos
    if (combosLlenar != undefined && combosLlenar.length > 0) {
        var item;
        for (var i = 0; i < combosLlenar.length; i++) {
            item = combosLlenar[i];
            llenarCombo(res[item.datasource], item.id, item.propiedadMostrar,
                item.propiedadId)
        }
    }
    //--Editar tipo usuario--//
    if (objFormulario != undefined && objFormulario.callbackOnload != undefined) {
        objFormulario.callbackOnload();
    }
    //--Editar tipo usuario-fin-//
}

var objConfiguracionGlobal;
var objBusquedaGlobal;
var objFormularioGlobal;

function pintar(objConfiguracion, objBusqueda, objFormulario) {

    if (objConfiguracion.url != undefined) {
        //URL Absolute  https://localhos
        var raiz = document.getElementById("hdfOculto").value;
        var urlAbsoluta = window.location.protocol + "//" +
            window.location.host + raiz + objConfiguracion.url;
        // alert(urlAbsoluta)
        //Controles//accion --> llena la tabla
        fetch(urlAbsoluta)
            .then(res => res.json())
            .then(res => {

                ConfigurarDatos(res, objConfiguracion, objFormulario, objBusqueda)

            })

    } else {
               ConfigurarDatos([], objConfiguracion, objFormulario, objBusqueda)
    }
}

function llenarComboBusqueda(res) {
    if (objBusquedaGlobal.type == "combobox") {
        var id = objBusquedaGlobal.id;
        var propiedadMostrar = objBusquedaGlobal.displaymember;
        var propiedadId = objBusquedaGlobal.valuemember;
        var name = objBusquedaGlobal.name;
        var data = res[name]
        llenarCombo(data, id, propiedadMostrar, propiedadId)
    }

}
//Limpiar Datos
function LimpiarDatos(idFormulario, excepciones = []) {

    //Limpiar todos radio
    var elementos = document.querySelectorAll("#" + idFormulario + " [name]")
    for (var j = 0; j < radioNames.length; j++) {
          document.querySelectorAll("[name=" + radioNames[j] + "]").forEach(x => x.checked = false);
      //  document.querySelectorAll("[name=" + radioNames[j] + "]").forEach(x => x.checked = false);
    }

    // Limpiar radio (Agarra el (checked) y los vuelve asignar cuando los limpia)
   // var elementos = document.querySelectorAll("#" + idFormulario + " [name]")
    for (var j = 0; j < radioLimpiar.length; j++) {
        document.getElementById(radioLimpiar[j]).checked = true;
    }

    // Limpiar check
    var checkbos = document.querySelectorAll("#" + idFormulario + " [type*='checkbox']")
    for (var j = 0; j < checkbos.length; j++) {
        checkbos[j].checked = false;
    }

    //Limpiar imagen
    for (var i = 0; i < elementos.length; i++) {
        //Si esta incluido no se hace nada

        if (!excepciones.includes(elementos[i].name)) {
            if (elementos[i].tagName.toUpperCase() == "IMG") {
                elementos[i].src = "";
            } else {
                elementos[i].value = "";
            }
        }
    }
}

//Validar longitud maxima
function ValidarLongitudMaxima(idFormulario) {
    var error = "";
    var controles = document.querySelectorAll("#" + idFormulario + " [class*='max-']")
    //  console.log(elementos);

    for (var i = 0; i < controles.length; i++) {
        control = controles[i]

        var arrayClase = control.className.split(" ");
        //max-40
        var claseMax = arrayClase.filter(p => p.includes("max-"))[0]
        //40
        var valorMax = claseMax.replace("max-", "") * 1;
        //console.log(valorMax);
        if (control.value.length > valorMax) {
            error = "El campo " + control.name + " tiene una longitud de " + control.value.length +
                ", no puede ser mayor a  " + valorMax + " por favor corregir";
            return error;
        }
    }
    return error;
}

//Validar longitud minima
function ValidarLongitudMinima(idFormulario) {
    var error = "";
    var controles = document.querySelectorAll("#" + idFormulario + " [class*='min-']")
    var control;
    //  console.log(elementos);

    for (var i = 0; i < controles.length; i++) {
        control = controles[i]
        //for-control
        var arrayClase = control.className.split(" ");
        //max-40
        var claseMin = arrayClase.filter(p => p.includes("min-"))[0]
        //40
        var valorMin = claseMin.replace("min-", "") * 1;
        if (control.value.length < valorMin) {
            error = "El campo " + control.name + " tiene una longitud de " + control.value.length +
                ", no puede ser menor a  " + valorMin + " por favor corregir";
            return error;
        }
    }
    return error;
}

//Validar números enteros
function ValidarSoloNumerosEnteros(idFormulario) {
    var error = "";
    var controles = document.querySelectorAll("#" + idFormulario + " [class*='snc']")
    var control;
    var caracter;
    for (var i = 0; i < controles.length; i++) {
        control = controles[i]
        var valor = control.value;
        var longitud = valor.length;
        for (var j = 0; j < controles.length; j++) {
            caracter = valor[j];
            if (caracter != "0" && caracter != "1" && caracter != "2" &&
                caracter != "3" && caracter != "4" && caracter != "5" &&
                caracter != "6" && caracter != "7" && caracter != "8" &&
                caracter != "9") {
                error = "El control " + control.name + " Solo debe tener números enteros";
                return error;
            }
        }
    }
    return error;
}

//Validar números enteros
function ValidarSoloNumerosDecimalesControl(idFormulario) {
    var error = "";
    var controles = document.querySelectorAll("#" + idFormulario + " [class*='sndc']")
    var control;
    var caracter;
    for (var i = 0; i < controles.length; i++) {
        control = controles[i]
        var valor = control.value;
        var longitud = valor.length;

        // Validación  No puede iniciar con un punto
        if (valor[0] == ".") {
            error = "El control " + control.name + " No puede iniciar con un punto(.)"
            return error;
        }
        // Validación  No puede terminar con un punto
        if (valor[longitud - 1] == ".") {
            error = "El control " + control.name + " No puede terminar con un punto(.)"
            return error;
        }

        // No permitir mas de un punto
        var numeroVeces = [...valor].filter(p => p.includes(".")).length
        if (numeroVeces > 1) {
            error = "El control " + control.name + " Solo debe tener un punto decimal"
            return error;
        }

        for (var j = 0; j < controles.length; j++) {
            caracter = valor[j];
            if (caracter != "0" && caracter != "1" && caracter != "2" &&
                caracter != "3" && caracter != "4" && caracter != "5" &&
                caracter != "6" && caracter != "7" && caracter != "8" &&
                caracter != "9" && caracter != ".") {
                error = "El control " + control.name + " Solo debe tener números enteros";
                return error;
            }
        }
    }
    return error;
}

//Validacion de campos 
function ValidarObligatorios(idFormulario) {
    //No hay error
    var error = "";
    var elementos = document.querySelectorAll("#" + idFormulario + " .o")
    var contenedorcheckbox = document.querySelectorAll("#" + idFormulario + " [class*='o-']") // CHECKBOX
   // console.log(contenedorcheckbox)

    for (var i = 0; i < contenedorcheckbox.length; i++) {
        //Contenedor div <div class"o-1">
        var contenedor = contenedorcheckbox[i];
        // todas las clases "o-2 input pantalla"
        var arrayClase = contenedor.className.split(" ");
        //clase maxima (o-1)
        var claseMaxima = arrayClase.filter(p => p.includes("o-"))[0];
        //Saco el numero 1234
        var minimoselecionable = claseMaxima.replace("o-", "") * 1;
       // console.log(minimoselecionable)
        //Inicializo cero
        var numerosmarcados = 0;
        var hijos = contenedor.children;
        var hijo;
     //   console.log(hijos);
        var nhijos = hijos.length;
        for (var j = 0; j < nhijos; j++) {
            hijo = hijos[j];
            if (hijo.type != undefined && (hijo.type.toLocaleUpperCase() == "CHECKBOX" || hijo.type.toLocaleUpperCase() == "RADIO")) {
                if (hijo.checked == true) {
                    numerosmarcados++;    // contamos solo los  CHECKBOX
                   // alert(numerosmarcados);
                }
            }
        }
        if (minimoselecionable > numerosmarcados) {
            error = "Debe seleccionar al menos " + minimoselecionable + " opción con un check";
            return error;
        }

    }

    for (var i = 0; i < elementos.length; i++) {
        //Si esta incluido no se hace nada (Input controles de entrada)
        if (elementos[i].parentNode.style.display != "none") { // Los campos ocultos no se validan
            if (elementos[i].tagName.toLocaleUpperCase() == "INPUT" && elementos[i].value == "") {
                error = "Debe ingresar el " + elementos[i].name;
                return error;
                // imagenes
            } else if (elementos[i].tagName.toLocaleUpperCase() == "IMG" && elementos[i].src == window.location.href) {
                error = "Debe ingresar la " + elementos[i].name.replace("base64", "");
                return error;

            }
        }
       
    }
    return error;
}

function generarTabla(objConfiguracion, res, objFormulario,primeravez=false,tienecheck=false, namecheck="") {
    // objFormulario.formulariogenerico = true
    var listaPintar = res;
    if (objConfiguracion != null && objConfiguracion.name != undefined && primeravez == true) {
        var nombrePropiedad = objConfiguracion.name;
        listaPintar = res[nombrePropiedad];
    }
    var contenido = "";
    contenido += "<div class='col-lg-12 grid-margin stretch-card'>";
    contenido += "<div class='card'>";
    contenido += "<div class='card-body'>";
   // contenido += "<h4 class='card-title'>Striped Table</h4>";
    contenido += "<p class='card - description'>  </p >";
    contenido += "<div class='table-responsive'>";

    contenido += "<table class='table table-striped' id='tablita'>";
    contenido += "<thead>";
    contenido += "<tr>";
       //Pintamos  check
    if (tienecheck==true) {
        contenido += "<th>Check</th>";
    }
 


    for (var j = 0; j < objConfiguracion.cabeceras.length; j++) {
        contenido += "<th>" + objConfiguracion.cabeceras[j] + "</th>"
    }
    if (objConfiguracion.editar == true || objConfiguracion.eliminar == true) {

        contenido += "<th>Acciones</th>";
    }
    contenido += "</tr>";
    contenido += "</thead>";
    var fila;
    var propiedadActual;
    for (var i = 0; i < listaPintar.length; i++) {
        fila = listaPintar[i]
        contenido += "<tr>";
        //llenamos los check
        if (tienecheck == true) {
            contenido += "<td><input type='checkbox' name='" + namecheck + "[]' value='" + fila[objConfiguracion.propiedadId] + "' /></td>"
        }
        for (var j = 0; j < objConfiguracion.propiedades.length; j++) {
            propiedadActual = objConfiguracion.propiedades[j]

            if (objConfiguracion.columnaimg != undefined && objConfiguracion.columnaimg.includes(propiedadActual))
                contenido += "<td> <img width='100px' height'100px' src='" + fila[propiedadActual] + "'/></td>";
            else
                contenido += "<td>" + fila[propiedadActual] + "</td>";
        }
        ////contenido += "<td>" + fila.id + "</td>";  //fila["id"]
        ////contenido += "<td>" + fila.nombre + "</td>";
        ////contenido += "<td>" + fila.descripcion + "</td>";
        if (objConfiguracion.editar == true || objConfiguracion.eliminar == true) {
            contenido += "<td>";
            if (objConfiguracion.editar == true) {

                contenido += ` <i
             ${objConfiguracion.popup == true ?
                        `data-toggle="modal" data-target="#${objConfiguracion.idpopup}"` : ""}    
              class="btn btn-outline-secondary btn-sm btn-icon-text"
               onclick='${(objFormulario != undefined &&
                        objFormulario.formulariogenerico != undefined &&
                        objFormulario.formulariogenerico == true) ? "EditarGenerico"
                        : objConfiguracion.callbackEditar
                    }(${fila[objConfiguracion.propiedadId]} , 
                     "${objFormulario == undefined ? "" : objFormulario.id} " ) ' >
               <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-eyedropper" viewBox="0 0 16 16">
                    <path d="M13.354.646a1.207 1.207 0 0 0-1.708 0L8.5 3.793l-.646-.647a.5.5 0 1 0-.708.708L8.293 5l-7.147 7.146A.5.5 0 0 0 1 12.5v1.793l-.854.854a.5.5 0 1 0 .708.707L1.707 15H3.5a.5.5 0 0 0 .354-.146L11 7.707l1.146 1.147a.5.5 0 0 0 .708-.708l-.647-.646 3.147-3.146a1.207 1.207 0 0 0 0-1.708l-2-2zM2 12.707l7-7L10.293 7l-7 7H2v-1.293z" />
                </svg> </i>`
            }

            if (objConfiguracion.eliminar == true) {
                contenido += `<i class="btn btn-outline-danger btn-sm btn-icon-text"
                onclick='${(objFormulario != undefined &&
                        objFormulario.formulariogenerico != undefined
                        &&
                        objFormulario.formulariogenerico == true) ? "EliminarGenerico"
                        : objConfiguracion.callbackEliminar
                    }(${fila[objConfiguracion.propiedadId]}) '  ><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16">
                       <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
                       </svg></i>`
            }

            contenido += "</td>";

        }

        contenido += "</tr>";
    }
    contenido += "</table>";
    contenido += "</div>";
    contenido += "</div>";
    contenido += "</div>";
    contenido += "</div>";
    return contenido;
}
// Para navegar entre ventanas (login)
function setUrl(url) {
    var raiz = document.getElementById("hdfOculto").value;
    var urlAbsoluta = window.location.protocol + "//" +
        window.location.host + raiz + url;
    return urlAbsoluta;
}
// fin Para navegar entre ventanas (login)

function fetchGet(url, callback) {
    var raiz = document.getElementById("hdfOculto").value;
    var urlAbsoluta = window.location.protocol + "//" +
        window.location.host + raiz + url;
    setD("cargando", "block");

    fetch(urlAbsoluta).then(res => res.json())
        .then(res => {
            setD("cargando", "none");
            callback(res)
        }).catch(err => {
            setD("cargando", "none");
            console.log(err)
        })

}

function fetchGetText(url, callback) {
    var raiz = document.getElementById("hdfOculto").value;
    var urlAbsoluta = window.location.protocol + "//" +
        window.location.host + raiz + url;
    setD("cargando", "block");
    fetch(urlAbsoluta).then(res => res.text())
        .then(res => {
            callback(res)
            setD("cargando", "none");
        }).catch(err => {
            console.log(err)
            setD("cargando", "none");
        })

}

function fetchPostText(url, frm, callback) {
    var raiz = document.getElementById("hdfOculto").value;
    var urlAbsoluta = window.location.protocol + "//" +
        window.location.host + raiz + url;
    setD("cargando", "block");
    fetch(urlAbsoluta, {
        method: "POST",
        body: frm
    }).then(res => res.text())
        .then(res => {
            callback(res)
            setD("cargando", "none");
        }).catch(err => {
            console.log(err)
            setD("cargando", "none");
        })
    /*
    fetch(urlAbsoluta).then(res => res.json())
        .then(res => {
            callback(res)
        }).catch(err => {
            console.log(err)
        })
        */
}

function Buscar() {
    var objConf = objConfiguracionGlobal;
    var objBus = objBusquedaGlobal;
    //Id del control
    var valor = get(objBus.id)
    fetchGet(`${objBus.url}/?${objBus.nombreparametro}=` + valor, function (res) {
        var rpta = generarTabla(objConf, res, objFormularioGlobal);
        document.getElementById("divContenedor").innerHTML = rpta;
        // paginado
        $("#tablita").DataTable();
    })
    /*
    fetch(`${objBus.url}/?${objBus.nombreparametro}=` + valor)
        .then(res => res.json())
        .then(res => {
            var rpta = generarTabla(objConf, res);
            document.getElementById("divContenedor").innerHTML = rpta;
        })
        */
    /*
    pintar({
        url: `${objBus.url}/?${objBus.nombreparametro}=` + valor,
        id: objConf.id,
        cabeceras: objConf.cabeceras,
        propiedades: objConf.propiedades
    }, objBus)*/
}
/*
function recuperarGenerico(url, idFormulario, excepciones = [], adicional = false) {
    var elementos = document.querySelectorAll("#" + idFormulario + " [name]")
    var nombreName;
    fetchGet(url, function (res) {
        for (var i = 0; i < elementos.length; i++) {
            nombreName = elementos[i].name
            if (!excepciones.includes(elementos[i].name)) {
                if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "RADIO") {
                    setC("[type='radio'][value='" + res[nombreName] + "']")
                } else {
                    if (elementos[i].type != undefined && elementos[i].type.toUpperCase() != "FILE")
                        setN(nombreName, res[nombreName])
                    else if (elementos[i].tagName.toUpperCase() == "IMG") {
                        setSRC(nombreName, res[nombreName])
                    }
                }
            }
        }
        if (adicional == true) {
            objConfiguracionGlobal.callbackeditar(res);
            // recuperarEspecifico(res);
            //alert("Se ejecuto");
        }
    });
}
*/
// Recueperar fotos
function recuperarGenerico(url, idFormulario, excepciones = [], adicional = false) {
    var elementos = document.querySelectorAll("#" + idFormulario + " [name]")
    var nombreName;
    fetchGet(url, function (res) {
        for (var i = 0; i < elementos.length; i++) {
            nombreName = elementos[i].name
            if (!excepciones.includes(elementos[i].name)) {
                if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "RADIO") {
                    setC("[type='radio'][name='" + nombreName + "'][value='" + res[nombreName] + "']")
                } else if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "CHECKBOX") {
                    // Recuperamos
                    var propiedad = nombreName.replace("[]", "");
                    //[1,3]
                    var valores = res[propiedad];
                    var valor;
                    for (var j = 0; j < valores.length; j++) {
                        valor = valores[j];
                        setC("[type='checkbox'][value='" + valor + "']")
                    }
                }

             else {
                if (elementos[i].type != undefined && elementos[i].type.toUpperCase() != "FILE")
                    setN(nombreName, res[nombreName])
                else if (elementos[i].tagName.toUpperCase() == "IMG") {
                    setSRC(nombreName, res[nombreName])
                }
            }
        }
    }
 
        if (adicional == true) {
            objConfiguracionGlobal.callbackeditar(res);
          //  recuperarEspecifico(res);
            //alert("Se ejecuto");
        }
    });
}

function recuperarGenericoEspecifico(url, idFormulario, excepciones = [], adicional = false) {
    var elementos = document.querySelectorAll("#" + idFormulario + " [name]")
    var nombreName;
    fetchGet(url, function (res) {
        for (var i = 0; i < elementos.length; i++) {
            nombreName = elementos[i].name
            if (!excepciones.includes(elementos[i].name)) {
                if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "RADIO") {
                    setC("[type='radio'][name='" + nombreName + "'][value='" + res[nombreName] + "']")
                }
                else if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "CHECKBOX") {
                    //RECUPERAMOS (valor)
                    var propiedad = nombreName.replace("[]", "");
                    //[1,3]
                    var valores = res[propiedad];
                    var valor;
                    for (var j = 0; j < valores.length; j++) {
                        valor = valores[j];
                        setC("[type='checkbox'][value='" + valor + "']")
                    }
                }
                else {
                    if (elementos[i].type != undefined && elementos[i].type.toUpperCase() != "FILE")
                        setN(nombreName, res[nombreName] == undefined ? "" : res[nombreName])
                    else if (elementos[i].tagName.toUpperCase() == "IMG") {
                        setSRC(nombreName, res[nombreName])
                    }


                }

            }

        }
        if (adicional == true) {
            //objConfiguracionGlobal.callbackeditar(res);
            recuperarEspecifico(res);
        }
    });


}

//Validar solo numeros
function validarSoloNumeros(e) {
    var codigoAscii = e.keyCode;
    if (codigoAscii < 48 || codigoAscii > 57) {
        //No mostrarse
        e.preventDefault();
    }
}

// Validar solo numeros decimales

function validarSoloNumerosDecimales(e) {
    var codigoAscii = e.keyCode;
    if ((codigoAscii < 48 && codigoAscii != 46) || codigoAscii > 57) {
        //No mostrarse
        e.preventDefault();
    }

    // Solo nos permite ingresar un numero 
    if (String.fromCharCode(e.keyCode) == ".") {
        if (e.target.value.includes(".")) e.preventDefault();

        // Impide que el primer caracter sea un punto
    }
    if (e.target.value.length == 0 && String.fromCharCode(e.keyCode) == ".") {
        e.preventDefault();
    }
}

function encontroClase(clase, ClaseBuscar = "snc") {

    var arrayClase = clase.split(" ");
    //max-40
    var numeroEncontradas = arrayClase.filter(p => p.includes(ClaseBuscar)).length;
    if (numeroEncontradas == 0) return false
    else return true;

}

var combosLlenar = [];
var radioLimpiar = [];
var radioNames = [];

function construirFormulario(objFormulario) {
    // console.log(objFormulario)
    var type = objFormulario.type;
    var contenido = "";
    if (objFormulario.formulario != undefined) {

    var elementos = objFormulario.formulario;

    contenido += "<div class='mt-3 mb-3'>";
    contenido += `<form id='${objFormulario.id}'  method='POST'>`;
    //FILAS
    var arrayelemento;
    var numeroarrayelemento;
    for (var i = 0; i < elementos.length; i++) {
        arrayelemento = elementos[i];
        numeroarrayelemento = arrayelemento.length;
        contenido += "<div class='row'>";
        for (var j = 0; j < numeroarrayelemento; j++) {
            var hijosArray = arrayelemento[j]
            if (hijosArray.class == undefined) {
                hijosArray.class = "mb-3";
            }
            if (hijosArray.type == undefined) {
                hijosArray.type = "text";
            }
            if (hijosArray.readonly == undefined) {
                hijosArray.readonly = false;
            }
            if (hijosArray.value == undefined) {
                hijosArray.value = "";
            }
            if (hijosArray.label == undefined) {
                hijosArray.label = hijosArray.name;
            }
            if (hijosArray.cols == undefined) {
                hijosArray.cols = "50";
            }
            if (hijosArray.rows == undefined) {
                hijosArray.rows = "10";
            }
            if (hijosArray.id == undefined) {
                hijosArray.id = "cboPrueba";
            }
            if (hijosArray.propiedadMostrar == undefined) {
                hijosArray.propiedadMostrar = "nombre";
            }
            if (hijosArray.propiedadId == undefined) {
                hijosArray.propiedadId = "id";
            }
            if (hijosArray.classControl == undefined) {
                hijosArray.classControl = "";
            }
            if (hijosArray.className == undefined) {
                hijosArray.className = "mb-3";
            }
       

            var encontroSNC = encontroClase(hijosArray.classControl, "snc")
            var encontroSNDC = encontroClase(hijosArray.classControl, "sndc")


            var typelemento = hijosArray.type;
            var classControl = hijosArray.classControl;
            contenido += `<div class="${hijosArray.class}">`
            contenido += `<label>${hijosArray.label}</label>`
            if (typelemento == "text" || typelemento == "number" || typelemento == "date") {

                contenido += `  <input type="text" class="form-control ${classControl}"
                          ${encontroSNC == false ? "" : "onkeypress='validarSoloNumeros(event)'"}
                ${encontroSNDC == false ? "" : "onkeypress='validarSoloNumerosDecimales(event)'"}

                       name="${hijosArray.name}" value="${hijosArray.value}"
                   ${hijosArray.readonly == true ? "readonly" : ""}  />`
            } else if (typelemento == "textarea") {
                contenido += `<textarea name="${hijosArray.name}" 
                     class="form-control ${classControl}"
                     rows="${hijosArray.rows}" cols="${hijosArray.cols}"
                       >${hijosArray.value}</textarea>`

            } else if (typelemento == "combobox") {
                contenido += `
                      <select name="${hijosArray.name}" class="form-control ${classControl}"
                                    id="${hijosArray.id}"></select>
                   `
                combosLlenar.push(hijosArray)
                //Construir radio y checked
            } else if (typelemento == "radio" || typelemento == "checkbox") {
                contenido += "<div class='" + classControl + "'>";
                for (var z = 0; z < hijosArray.labels.length; z++) {
                    contenido += `
                         <input type="${typelemento}"
                            ${hijosArray.ids != undefined && hijosArray.ids[z] == hijosArray.checked ? "checked" : ""}
                }
                         id="${hijosArray.ids == undefined ? z : hijosArray.ids[z]}"
                        name="${hijosArray.name}${typelemento == "checkbox" ? '[]' : ''}" value="${hijosArray.values[z]}" />
                            <label> ${hijosArray.labels[z]}</label >
                      `
                }
                if (typelemento == "radio" && hijosArray.checked != undefined)
                    radioLimpiar.push(hijosArray.checked) // Limpiar  checked
                if (typelemento == "radio")
                    radioNames.push(hijosArray.name);
                contenido += "</div>";
            } else if (typelemento == "file") {
                if (hijosArray.imgwidth == undefined) {
                    hijosArray.imgwidth = "100";
                }
                if (hijosArray.imgheight == undefined) {
                    hijosArray.imgheight = "100";
                }
               
                if (hijosArray.preview == undefined) {
                    hijosArray.preview = true;
                }
                // imgClass
                if (hijosArray.imgClass == undefined) {
                    hijosArray.imgClass = "";
                }
                if (hijosArray.preview == true) {
                    contenido += `
                       <img whidth="${hijosArray.imgwidth}" class="${hijosArray.imgClass}"
                            height="${hijosArray.imgheight}" id="img${hijosArray.name}"
                            name="${hijosArray.namefoto}" style="display:block" />
                      `
                }
                contenido += `
                            <input type="file"
                                    id="fup${hijosArray.name}" 
                                    name="${hijosArray.name}"
                              ${hijosArray.preview == true ?
                        `onchange='previewImage(this,"img${hijosArray.name}")'` : ""}
                                />
                      `
            }   //-----------Inicio--list Generico----------------------//
            else if (typelemento == "list") {
                contenido += "<div id='" + hijosArray.id + "'>"
                contenido += generarTabla({
                    cabeceras: hijosArray.cabeceras,
                    propiedades: hijosArray.propiedades,
                    propiedadId: hijosArray.propiedadId
                }, hijosArray.data, null, false, true, hijosArray.name)
                  contenido += "</div>"
            }
                //-----------Fin--list Generico----------------------//
            contenido += "</div>";
        }
        contenido += "</div>";
    }
    contenido += "</form>";
        contenido += "</div>"
    } // cierro validacion
    return contenido;
}

function previewImage(control,img){
    var file = control.files[0];
    var imgFoto = document.getElementById(img);
    var reader = new FileReader();
    reader.onloadend = function () {
        imgFoto.src = reader.result;
    }
    reader.readAsDataURL(file)
}


//Guardar Generico
function GuardarGenerico(idformulario, urlguardar) {
    // alert(idformulario);
    // alert(urlguardar);

    var error = ValidarObligatorios(idformulario)
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMaxima(idformulario);
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMinima(idformulario);
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarSoloNumerosEnteros(idformulario)
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarSoloNumerosDecimalesControl(idformulario)
    if (error != "") {
        Error(error);
        return;
    }

    Confirmacion("" + objFormularioGlobal.tituloconfirmacionguardar, "Confirmar Guardar Datos",
        function (res) {
            var tipoform = objFormularioGlobal.type;
            var idpopup = objConfiguracionGlobal.idpopup;
            var frmGenerico = document.getElementById(idformulario);
            var frm = new FormData(frmGenerico);
            fetchPostText(urlguardar, frm, function (res) {
                if (res == "1") {
                    if (objFormularioGlobal.callbackGuardarDatos != undefined) {
                        objFormularioGlobal.callbackGuardarDatos();
                    } else {
                        var objConf = objConfiguracionGlobal;
                        var objBus = objBusquedaGlobal;
                        //Id del control
                        if (objBus != undefined && objBus != null) {
                            var valor = get(objBus.id)
                            fetchGet(`${objBus.url}/?${objBus.nombreparametro}=` + valor, function (res) {
                                var rpta = generarTabla(objConf, res, objFormularioGlobal);
                                document.getElementById("divContenedor").innerHTML = rpta;

                            })
                        } else {
                            if (objConf.url != undefined) {
                                fetchGet(`${objConf.url}`, function (res) {
                                    if (objConf.name != undefined && objConf.name != "")
                                        res = res[objConf.name]
                                    var rpta = generarTabla(objConf, res, objFormularioGlobal);
                                    document.getElementById("divContenedor").innerHTML = rpta;

                                })
                            }

                        }

                        if (tipoform == "popup") {
                            document.getElementById("btnCerrar" + objConfiguracionGlobal.idpopup)
                                .click();
                        }
                      //   window.onload();
                        Correcto();                       
                        LimpiarDatos(idformulario, objFormularioGlobal.limpiarexcepcion.concat(radioNames))
                       
                    //listarTipoHabitacion();
                        
                    //Limpiar();
                    }
                   
                }
            })
        });
}

function EditarGenerico(id, idFormulario) {
    //var idFormulario = "frmCama";
    // var idformulario = objConfiguracionGlobal 
    //alert(idFormulario)
    var url = objConfiguracionGlobal.urlRecuperar;
    var nombreparametro = objConfiguracionGlobal.parametroRecuperar
    if (objFormularioGlobal.type == "popup") {
        LimpiarGenerico(objConfiguracionGlobal.id)
        if (id == 0) {
            document.getElementById("lbl" + objConfiguracionGlobal.idpopup).innerHTML
                = "Agregar " + objFormularioGlobal.titulo;
        }
        //editar
        else {
            document.getElementById("lbl" + objConfiguracionGlobal.idpopup).innerHTML
                = "Editar " + objFormularioGlobal.titulo;
            recuperarGenerico(`${url}/?${nombreparametro}=` + id,
                idFormulario, objConfiguracionGlobal.recuperarexcepcion, objConfiguracionGlobal.iscallbackeditar);
        }
    } else { // en caso de no ser popup

        recuperarGenerico(`${url}/?${nombreparametro}=` + id,
            idFormulario, objConfiguracionGlobal.recuperarexcepcion, objConfiguracionGlobal.iscallbackeditar);
    }
}

function EliminarGenerico(id) {
    var url = objConfiguracionGlobal.urlEliminar;
    var nombreparametro = objConfiguracionGlobal.parametroEliminar;
    var objConf = objConfiguracionGlobal;
    var objBus = objBusquedaGlobal;


    Confirmacion("Desea eliminar el tipo habitacion?", "Confirmar eliminaciòn",
        function (res) {
            fetchGetText(`${url}/?${nombreparametro}=` + id,
                function (rpta) {
                    if (rpta == "1") {
                        Correcto("Se elimino correctamente");

                        if (objBus != null && objBus != undefined) {
                            var valor = get(objBus.id)
                            fetchGet(`${objBus.url}/?${objBus.nombreparametro}=` + valor, function (res) {
                                var rpta = generarTabla(objConf, res, objFormularioGlobal);
                                document.getElementById("divContenedor").innerHTML = rpta;
                            })

                        } else {
                            fetchGet(`${objConf.url}`, function (res) {
                                if (objConf.name != undefined && objConf.name != "")
                                    res = res[objConf.name]
                                var rpta = generarTabla(objConf, res, objFormularioGlobal);
                                document.getElementById("divContenedor").innerHTML = rpta;
                            })
                        }
                    }
                })
        })
}

function LimpiarGenerico(idFormulario) {
    LimpiarDatos(idFormulario, objFormularioGlobal.limpiarexcepcion.concat(radioNames))
}

function llenarCombo(data, id, propiedadMostrar, propiedadId, valueDefecto = "") {
    var contenido = ""
    var elemento;
    contenido += "<option value='" + valueDefecto + "'>--Seleccione--</option>"
    for (var j = 0; j < data.length; j++) {
        elemento = data[j];
        contenido +=
            "<option value='" + elemento[propiedadId] + "' >" + elemento[propiedadMostrar] + "</option>"
    }

    contenido += "";
    document.getElementById(id).innerHTML = contenido;

}

function diasRangoFecha(fecha1, fecha2) {
    var fechaInicio = new Date(fecha1).getTime();
    var fechaFin = new Date(fecha2).getTime();

    var diff = fechaFin - fechaInicio;

    return (diff / (1000 * 60 * 60 * 24));
}