document.addEventListener("DOMContentLoaded", function () {
    cargarMenu();
    cargarRoles();
    cargarUsuarios()

    document.getElementById("registroForm").addEventListener("submit", function (event) {
        event.preventDefault();
        guardarUsuario();
    });
});

// Funcion para cargar los roles.
function cargarRoles() {
    fetch(URL_API_ROLES + "Rol")
        .then(response => response.json())
        .then(data => {
            console.log("Roles recibidos:", data);

            let rolesArray = Array.isArray(data) ? data : data.datos;
            if (!rolesArray || !Array.isArray(rolesArray)) {
                console.error("No se recibió un array de roles.");
                return;
            }

            const selectRoles = document.getElementById("IdRoles");
            selectRoles.innerHTML = "";

            let defaultOption = document.createElement("option");
            defaultOption.value = "";
            defaultOption.textContent = "Seleccione un rol";
            defaultOption.disabled = true;
            defaultOption.selected = true;
            selectRoles.appendChild(defaultOption);

            rolesArray.forEach(rol => {
                let option = document.createElement("option");
                option.value = rol.idRol;
                option.text = rol.nombre;
                selectRoles.appendChild(option);
            });
        })
        .catch(error => {
            alertaError(error);
        });
}

// Carga los usuarios.
function cargarUsuarios() {
    fetch(URL_API_USUARIOS + "usuarios")
        .then(response => response.json())
        .then(data => {
            console.log("Usuarios recibidos:", data);

            let usuariosArray = Array.isArray(data) ? data : data.datos;
            if (!usuariosArray || !Array.isArray(usuariosArray)) {
                console.error("No se recibió un array de usuarios.");
                return;
            }

            const tablaUsuarios = document.getElementById("tablaUsuarios");
            tablaUsuarios.innerHTML = "";

            usuariosArray.forEach(usuario => {
                let fila = document.createElement("tr");
                fila.innerHTML = `
                    <td>${usuario.idUsuario}</td>
                    <td>${usuario.nombres}</td>
                    <td>${usuario.apellidos}</td>
                    <td>${usuario.edad}</td>
                    <td>${usuario.correoElectronico}</td>
                    <td>${usuario.roles.nombre}</td>
                    <td>
                        <button onclick="cargarUsuarioParaEditar(${usuario.idUsuario})" data-bs-toggle="modal" data-bs-target="#modalRegistroUsuario" >Editar</button>
                    </td>
                `;
                tablaUsuarios.appendChild(fila);
            });
        })
        .catch(error => {
            alertaError(error);
        });
}

function cargarUsuarioParaEditar(idUsuario) {
    
    document.getElementById("campoContrasena").style.display = 'none';

    fetch(URL_API_USUARIOS + `usuarios/${idUsuario}`)
        .then(response => response.json())
        .then(resultado => {
            document.getElementById("idUsuario").value = resultado.datos.idUsuario;
            document.getElementById("nombres").value = resultado.datos.nombres;
            document.getElementById("apellidos").value = resultado.datos.apellidos;
            document.getElementById("edad").value = resultado.datos.edad;
            document.getElementById("correo").value = resultado.datos.correoElectronico;
            document.getElementById("IdRoles").value = resultado.datos.roles.idRol;
        })
        .catch(error => {
            alertaError(error);
        });
}

async function guardarUsuario() {
    let idUsuario = document.getElementById("idUsuario").value;

    if (!idUsuario) {        
        registrarUsuario();
    }
    else {
        actualizarUsuario();
    }
}

async function registrarUsuario() {
    
    let usuario = {
        nombres: document.getElementById("nombres").value,
        apellidos: document.getElementById("apellidos").value,
        edad: parseInt(document.getElementById("edad").value),
        correoElectronico: document.getElementById("correo").value,
        contrasena: '',
        roles: {
            idRol: parseInt(document.getElementById("IdRoles").value),
        },
        fecha: new Date().toISOString().split("T")[0]
    };

    encriptarSHA256(document.getElementById("contrasena").value).then(hash => {
        usuario.contrasena = hash;
        console.log("Usuario a registrar:", usuario);

        fetch(URL_API_USUARIOS + "usuarios", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(usuario)
        })
            .then(response => response.json())
            .then(data => {
                document.getElementById("registroForm").reset();
                cerrarModal();
                alertaConfirmacion("Usuario creado con exito.");
                cargarUsuarios(); // Recargar la lista de usuarios
            })
            .catch(error => {
                cerrarModal();
                alertaError(error);
            });
    });
}

//Actualiza algun usuario.
function actualizarUsuario() {
    let idUsuario = document.getElementById("idUsuario").value;

    let usuario = {
        idUsuario: parseInt(idUsuario),
        nombres: document.getElementById("nombres").value,
        apellidos: document.getElementById("apellidos").value,
        edad: parseInt(document.getElementById("edad").value),
        correoElectronico: document.getElementById("correo").value,
        roles: {
            idRol: parseInt(document.getElementById("IdRoles").value),
        }
    };

    fetch(URL_API_USUARIOS + `usuarios`, {
        method: "PATCH",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(usuario)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Error al actualizar el usuario.");
            }
            return response.json();
        })
        .then(data => {
            // Restablecer el formulario
            document.getElementById("registroForm").reset();
            document.getElementById("idUsuario").value = "";

            cerrarModal();
            alertaAdvertencia("Usuario actualizado exitosamente.");
            cargarUsuarios(); // Recargar la lista de usuarios
        })
        .catch(error => {
            cerrarModal();
            alertaError(error);
        });
}

function nuevo() {
    document.getElementById("campoContrasena").style.display = 'block';
}

function cerrarModal() {

    var modalElement = document.getElementById('modalRegistroUsuario');
    var modalInstance = bootstrap.Modal.getInstance(modalElement); // Obtener la instancia del modal

    if (modalInstance) {
        modalInstance.hide(); // Cierra la modal
    }
}
