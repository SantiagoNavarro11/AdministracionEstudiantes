function encriptarSHA256(texto) {
    const encoder = new TextEncoder();
    const data = encoder.encode(texto);

    return crypto.subtle.digest("SHA-256", data).then(hashBuffer => {
        const hashArray = Array.from(new Uint8Array(hashBuffer));
        return hashArray.map(byte => byte.toString(16).padStart(2, "0")).join("");
    });
}

function cargarMenu()
{
    document.getElementById("lblNombreUsuario").innerText = NombreUsuario() + ' (' + NombreRol() + ')';
    
    var idRolUsuario = IdRol();

    if (idRolUsuario == ROL_ADMINISTRADOR){
        document.getElementById("liGestionUsuarios").style.display = "block";
        document.getElementById("liGestionMaterias").style.display = "block";        
    }
    else if (idRolUsuario == ROL_ESTUDIANTE || idRolUsuario == ROL_PROFESOR){        
        document.getElementById("liGestionarMisMaterias").style.display = "block"; 
    }   
}