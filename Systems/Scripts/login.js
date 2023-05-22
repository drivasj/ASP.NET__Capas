


function login() {
    //capturamos el usuario y la contraseña
    var nombreusuario = getN("nombreusuario")
    var contra = getN("contra")
    fetchGet("Login/uspLogin/?usuario=" + nombreusuario + "&contra=" + contra, function (data) {

        if (data.iidususario == 0) Error("Usuario o Contraseña incorrecta")
        else {
            Correcto("Bienvenido");
            //Navegar a la pagina de inico elemplo: http://localhost:53410/Marca/Index
            document.location.href = setUrl("Rol/RolPaginaMenu");
        }

    })
}