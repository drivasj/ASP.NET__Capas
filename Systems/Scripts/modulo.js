window.onload = function () {
    listarModulo();
}


function listarModulo() {
    pintar({
        popup: true,
        editar: true,
        eliminar: false,
        idpopup: "defaultModal",
        sizepopup: "modal-md",
        url: "Modulo/listarModulos",
        id: "divTabla",
        cabeceras: ["ID Módulo", "Nombre", "Controlador", "Acción"],
        //  name: "listaModulo",
        propiedades: ["iidmodulo", "nombreModulo", "controllerModulo", "accionModulo"],
        //Para modificar   
        urlEliminar: "Modulo/DeshabilitarModulo",
        parametroEliminar: "iidmenuD",
        urlRecuperar: "Modulo/recuperarModulo",
        // recuperarexcepcion: ["nombreproducto"], //ingrese el name que no se quiere recuperar
        iscallbackeditar: true,
        callbackeditar: function (res) {
            //Cosas Adicionales que queramos hacer 
            // document.getElementById("lblTituloForm").innerHTML = "Producto";
        },
        parametroRecuperar: "iidmoduloR",
        propiedadId: "iidmodulo"
    }, null,
        //{
        //    busqueda: true,
        //    //filtro
        //    url: "Producto/filtrarProductoPorCategoria",
        //    nombreparametro: "iidcategoria",
        //    type: "combobox",
        //    name: "listaCategoria",
        //    displaymember: "nombre",
        //    valuemember: "iidcategoria",
        //    button: true,
        //    id: "cboCategoriaFormulario"
        //    //  placeholder:"Ingrese nombre cama"
        //},
        {
            urlGuardar: "Modulo/GuardarModulo",
            type: "popup",
            titulo: "Módulo",
            tituloconfirmacionguardar: "Desea guardar el Modulo?",
            id: "frmModulo",

            //limpiarexepcion:[""],  //["iidproducto"], //Recibimos el array con los que no queramos limpiar
            formulario: [
                [
                    {
                        class: "mb-3 col-md-6",
                        label: "ID Módulo",
                        name: "iidmodulo",
                        value: 0,
                        readonly: true
                    },
                    {
                        class: "mb-3 col-md-6",
                        label: "Nombre",
                        name: "nombreModulo",
                        classControl: "o max-50 min-3"
                    },

                ],
                [

                    {
                        class: "mb-3 col-md-6",
                        label: "Controlador",
                        name: "controllerModulo",
                        classControl: "o max-50 min-3"
                    },
                    {
                        class: "mb-3 col-md-6",
                        label: "Acción",
                        name: "accionModulo",
                        classControl: "o max-50 min-3"
                    }
                ]

            ]
        }
    )
}



//function Buscar() {
//    var nombreproducto = get("txtnombreproducto")
//    pintar({
//        url: "Producto/FiltarProductoPorNombre/?nombreproducto=" + nombreproducto,
//        id: "divTabla",
//        cabeceras: ["ID Producto", "Nombre Producto ", "Nombre Marca", "Precio Venta", "Stock", "Denominación"],
//        propiedades: ["iidproducto", "nombreproducto", "nombremarca", "precioventa", "stock", "denominacion"]
//    }
//        //,
//        //{
//        //busqueda: true,
//        //url: "Producto/FiltrarProducto",
//        //nombreparametro:"nombre",
//        //type: "text",
//        //id: "txtnombrecama",
//        //placeholder:"Ingrese nombre cama"
//        //}
//    )
//}

function BuscarProductoMarca() {
    var iidmarca = get("cboMarca")
    pintar({
        url: "Producto/filtrarProductoPorMarca/?iidmarca=" + iidmarca,
        id: "divTabla",
        cabeceras: ["Id Producto", "Nombre", "Nombre Marca", "Precio",
            "Stock", "Denominacion"],
        propiedades: ["iidproducto", "nombreproducto", "nombremarca",
            "precioventa", "stock", "denominacion"]
    })
}
