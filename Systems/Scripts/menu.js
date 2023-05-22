window.onload = function () {
    listarMenu();
    // llenarComboMarca();
}
//function llenarComboMarca() {
//    fetchGet("Marca/listarMarca", function (data) {
//        llenarCombo(data, "cboMarca", "nombreMarca", "iidMarca")

//    })
//}


function listarMenu() {
    pintar({
        popup: true,
        url: "Rol/ListarMenuLista",
        id: "divTabla",
        cabeceras: ["ID Menu", "Rol", "Página", "Modulo"],
        propiedades: ["iidmenu", "rol", "nombreMenu", "ds_modulo"],
        editar: true,
        sizepopup: "modal-md",
        eliminar: false,
        urlEliminar: "Producto/eliminarProducto",
        parametroEliminar: "iidproducto",
        urlRecuperar: "Rol/recuperarTMenu",
        //
        idpopup: "staticBackdrop1",
        name: "listaMenu",
        paginar: false,
        nuevo: false,
        excel: false,
        callbackexcel: function () {
            alert('Exportar Excel');
        },
        parametroRecuperar: "iidmenuR",
        propiedadId: "iidmenu"





        // recuperarexcepcion: ["nombreproducto"], //ingrese el name que no se quiere recuperar
        //iscallbackeditar: true,
        //callbackeditar: function (res) {
        //    //Cosas Adicionales que queramos hacer 
        //    // document.getElementById("lblTituloForm").innerHTML = "Producto";
        //},

    }, null,
        //{ //Busqueda
        //    busqueda: false,
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
            urlGuardar: "Rol/GuardarDatos",
            type: "popup",
            titulo: "Menu",
            tituloconfirmacionguardar: "Desea Asignar el modulo seleccionado ?",
            id: "frmTMenu",
            paginar: true,
            //limpiarexepcion:[""],  //["iidproducto"], //Recibimos el array con los que no queramos limpiar
            formulario: [
                [
                    {
                        class: "mb-3 col-md-4",
                        label: "Id Menu",
                        name: "iidmenu",
                        value: 0,
                        readonly: true
                    },
                    {
                        // "Nombre Hotel",

                        class: "mb-3 col-md-4",
                        label: "Nombre Página",
                        name: "nombreMenu",
                        readonly: true,
                        classControl: "o max-50 min-3"
                    },
                    {
                        class: "col-md-4",
                        type: "combobox",
                        label: "Seleccionar Módulo",
                        datasource: "listaModulo",  // listaModulo  mensajeM listaMenu
                        name: "iidmoduloTM",
                        id: "cboMenuM",
                        propiedadMostrar: "nombreModulo",
                        propiedadId: "iidmodulomenu",
                        classControl: "o"
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

//function BuscarProductoMarca() {
//    var iidmarca = get("cboMarca")
//    pintar({
//        url: "Producto/filtrarProductoPorMarca/?iidmarca=" + iidmarca,
//        id: "divTabla",
//        cabeceras: ["Id Producto", "Nombre", "Nombre Marca", "Precio",
//            "Stock", "Denominacion"],
//        propiedades: ["iidproducto", "nombreproducto", "nombremarca",
//            "precioventa", "stock", "denominacion"]
//    })
//}
