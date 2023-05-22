window.onload = function () {
    listarRolPaginaMenu();
}

function listarRolPaginaMenu() {
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
        nuevo: false,
        callbacknuevo: function () {
            agregarRolPaginaMenu();
        }
    });
}

function agregarRolPaginaMenu(id = 0) {
    fetchGet("Pagina/listarPaginas", function (res) {
        pintar({
            id: "divTabla",
            // editar: true,
        }, null, {
            id: "frmRolPaginaMenu",
            callbackOnload: function () {
                if (id > 0) {
                    //  alert("Editar")
                    recuperarGenericoEspecifico("Rol/RecuperarRolMenu/?iidrolMenu=" + id, "frmRolPaginaMenu")
                }
            },
            type: "fieldset",
            urlGuardar: "Rol/GuardarRolMenu",
            legend: "Detalles del Rol",
            regresar: true,
            callbackregresar: function () {
                listarRolPaginaMenu()
            },
            callbackGuardarDatos: function () {
                listarRolPaginaMenu()
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
                        classControl: "o max-50 min-3",
                        readonly: true
                    }

                ],
                [
                    {
                        class: "col-md-12",
                        type: "list",
                        label: "Seleccione las Páginas del Menu",
                        id: "divPintado",

                        name: "idpaginas",
                        cabeceras: ["ID Página", "Nombre"],
                        propiedades: ["iidpagina", "nm_pagina"],
                        propiedadId: "iidpagina",
                        data: res,

                        //name: "nameP",
                        //cabeceras: ["ID Página", "Nombre", "accion"],
                        //propiedades: ["iidpagina", "mensaje", "accion"],
                        //propiedadId: "iidpagina",
                        //data: res
                    }
                    //{
                    //    class: "col-md-6",
                    //    type: "list",
                    //    label: "Seleccione las Páginas del Menu",
                    //    id: "divPintado",

                    //    name: "nameP",
                    //    cabeceras: ["ID Página", "Nombre", "accion"],
                    //    propiedades: ["iidpagina", "mensaje", "accion"],
                    //    propiedadId: "iidpagina",
                    //    data: res,

                    //    //name: "nameP",
                    //    //cabeceras: ["ID Página", "Nombre", "accion"],
                    //    //propiedades: ["iidpagina", "mensaje", "accion"],
                    //    //propiedadId: "iidpagina",
                    //    //data: res
                    //}
                ]
            ]
        })
    })
}

function Editar(id) {
    // alert(id);
    agregarRolPaginaMenu(id);
}

