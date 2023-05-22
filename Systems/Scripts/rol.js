window.onload = function () {
    listarRol();

}


function listarRol() {
    pintar({
        url: "Rol/listarRol",
        id: "divTabla",
        cabeceras: ["ID Rol", "Nombre", "Descripcion"],
        propiedades: ["iidrol", "nombre", "descripcion"],
        editar: true,
        eliminar: false,
        urlEliminar: "Cama/eliminarCama",
        parametroEliminar: "iidcama",
        //    urlRecuperar: "Cama/recuperarCama",
        // parametroRecuperar: "idcamita",
        propiedadId: "iidrol",
        nuevo: true,
        callbacknuevo: function () {
            agregarRol();
        }
    });
}

function agregarRol(id = 0) {
    fetchGet("Pagina/listarPaginas", function (res) {
        pintar({
            id: "divTabla",
            // editar: true,
        }, null, {
            id: "frmRol",
            callbackOnload: function () {
                if (id > 0) {
                    //  alert("Editar")
                    recuperarGenericoEspecifico("Rol/RecuperarRol/?iidrolR=" + id, "frmRol")
                }
            },
            type: "fieldset",
            urlGuardar: "Rol/GuardarRol",
            legend: "",
            regresar: true,
            callbackregresar: function () {
                listarRol()
            },
            callbackGuardarDatos: function () {
                listarRol()
            },
            formulario: [
                [
                    {
                        class: "mb-3 col-md-5",
                        label: "ID Rol",
                        name: "iidrol",
                        value: 0,

                        readonly: true
                    },
                    {
                        class: "mb-3 col-md-7",
                        label: "Nombre Rol",
                        name: "nombre",
                        classControl: "o max-50 min-3"
                    }

                ],
                [
                    {
                        class: "col-md-12",
                        type: "textarea",
                        label: "Descripcion Rol",
                        name: "descripcion",
                        rows: "5",
                        cols: "20",
                        classControl: "o max-70 min-10" // Validacion de campo

                    }
                ],
                [
                    {
                        class: "col-md-12",
                        type: "list",
                        label: "Seleccione Paginas",
                        id: "divPintado",

                        name: "idpaginas",
                        cabeceras: ["ID Página", "Nombre"],
                        propiedades: ["iidpagina", "nm_pagina"],
                        propiedadId: "iidpagina",
                        data: res
                    }
                ]
            ]
        })
    })
}

function Editar(id) {
    // alert(id);
    agregarRol(id);
}

