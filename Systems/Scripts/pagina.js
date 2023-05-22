window.onload = function () {
    listarPaginas();
}

function listarPaginas() {
    pintar({
        url: "Pagina/listarPaginas",
        id: "divTabla",
        cabeceras: ["ID", "Nombre", "Accion", "Controlador"],
        propiedades: ["iidpagina", "nm_pagina", "actionP", "controllerP"],
        editar: true,
        eliminar: false,
        urlEliminar: "Pagina/eliminarPagina",
        parametroEliminar: "iidPaginaE",
        //   urlRecuperar: "Pagina/recuperarPagina",
        //  parametroRecuperar: "iidpagina",
        propiedadId: "iidpagina",
        paginar: true,
        nuevo: true,
        callbacknuevo: function () {
            agregarPagina();
        }
    });
}

function agregarPagina(id = 0) {
    fetchGet("Pagina/listarPaginas", function (res) {
        pintar({
            id: "divTabla",
        }, null, {
            id: "frmPagina",
            callbackOnload: function () {
                if (id > 0) {
                    //  alert("Editar " + id)
                    recuperarGenericoEspecifico("Pagina/recuperarPagina/?iidpagina=" + id, "frmPagina")
                }
            },
            type: "fieldset",
            urlGuardar: "Pagina/guardarPagina",
            legend: "",
            regresar: true,
            callbackregresar: function () {
                listarPaginas()
            },
            callbackGuardarDatos: function () {
                listarPaginas()
            },
            formulario: [
                [
                    {
                        class: "mb-3 col-md-6",
                        label: "ID Página",
                        name: "iidpagina",
                        value: 0,

                        readonly: true
                    },
                    {
                        class: "mb-3 col-md-6",
                        label: "Nombre",
                        name: "nm_pagina",
                        classControl: "o max-50 min-3"
                    },


                ],
                [
                    {
                        class: "mb-3 col-md-6",
                        label: "Acción",
                        name: "actionP",
                        classControl: "o max-50 min-3"
                    },
                    {
                        class: "mb-3 col-md-6",
                        label: "Controlador",
                        name: "controllerP",
                        classControl: "o max-50 min-3"
                    }
                ]
            ]
        })
    })
}

function Editar(id) {
    // alert(id);
    agregarPagina(id);
}


function Eliminar(id) {
    Confirmacion("Desea deshabilitar la pagina?", "Confirmar", function (res) {

        fetchGetText("Pagina/eliminarPagina/?iidPaginaE=" + id, function (rpta) {
            if (rpta == "1") {
                Correcto("Se Deshabilito correctamente");
                listarPaginas()
            }
        })
    })
}


