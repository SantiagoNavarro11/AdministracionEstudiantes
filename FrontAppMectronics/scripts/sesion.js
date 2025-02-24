function IdUsuario() {
    var usuarioJson = localStorage.getItem("usuarioactual");

    if (usuarioJson != undefined) {
        var usuario = JSON.parse(usuarioJson);
        return usuario.idUsuario;
    }
}

function NombreUsuario() {
    var usuarioJson = localStorage.getItem("usuarioactual");

    if (usuarioJson != undefined) {
        var usuario = JSON.parse(usuarioJson);
        return usuario.nombres + ' ' + usuario.apellidos;
    }
}

function IdRol() {
    var usuarioJson = localStorage.getItem("usuarioactual");

    if (usuarioJson != undefined) {
        var usuario = JSON.parse(usuarioJson);
        return usuario.roles.idRol;
    }
}  

function NombreRol() {
    var usuarioJson = localStorage.getItem("usuarioactual");

    if (usuarioJson != undefined) {
        var usuario = JSON.parse(usuarioJson);
        return usuario.roles.nombre;
    }
}  


function CerrarSesion(){
    localStorage.clear();
    window.location.href = "/";
}
